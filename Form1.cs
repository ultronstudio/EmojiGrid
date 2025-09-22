using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ultron.WinForms.HelpAbout;
using Newtonsoft.Json.Linq;
using System.Runtime.Versioning; // CA1416

namespace DvourozmernePole
{
    [SupportedOSPlatform("windows")] // CA1416
    public partial class oknoProgramu : Form
    {
        private readonly Random _rng = new Random();

        private readonly HttpClient _http = new HttpClient(
            new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            })
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        private readonly Dictionary<string, Image> _cache =
            new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);

        private const string TwemojiCdn = "https://twemoji.maxcdn.com/v/latest/72x72/{0}.png";
        private const string EmojiJsonUrl = "https://raw.githubusercontent.com/chalda-pnuzig/emojis.json/refs/heads/master/src/list.json";

        private static List<string> EmojiHex = new List<string>
        {
            "1f600","1f602","1f603","1f604","1f60d","1f62e","1f621","1f923","1f680","1f4a5","1f525","1f389"
        };

        public oknoProgramu()
        {
            InitializeComponent();

            // .NET Framework 4.8: spolehni se na TLS 1.2. (TLS 1.3 není garantovaný)
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            if (!(panelGrid is TableLayoutPanel))
            {
                var tlp = new TableLayoutPanel { Dock = DockStyle.Fill, Name = "panelGrid" };
                Controls.Remove(panelGrid);
                panelGrid.Dispose();
                panelGrid = tlp;
                Controls.Add(panelGrid);
                panelGrid.BringToFront();
            }

            this.UseStandardHelpAbout(new HelpAboutOptions
            {
                Category = TaskCategory.PraceVHodine,
                Author = "Petr Vurm",
                ExtraLines = "SPŠ Hradební — PROGRAMOVÁNÍ"
            });

            this.Load += oknoProgramu_Load;
            this.Click += oknoProgramu_Click;
        }

        int[,] poleCisel;

        private async void oknoProgramu_Load(object sender, EventArgs e)
        {
            var ok = await TryLoadEmojiFromUrlAsync(EmojiJsonUrl);
            if (!ok)
            {
                MessageBox.Show("Nepodařilo se načíst list emoji. Zkontrolujte připojení k internetu.", "Chyba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        private async void oknoProgramu_Click(object sender, EventArgs e)
        {
            txtCisla.Text = "";

            int r = Math.Max(1, int.Parse(txtRadky.Text));
            int c = Math.Max(1, int.Parse(txtSloupce.Text));
            poleCisel = new int[r, c];

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    int idx = _rng.Next(EmojiHex.Count);
                    poleCisel[i, j] = idx;
                    txtCisla.Text += EmojiHex[idx] + (j == c - 1 ? "" : ", ");
                }
                txtCisla.Text += Environment.NewLine;
            }

            var grid = panelGrid;

            grid.SuspendLayout();
            grid.Controls.Clear();
            grid.ColumnStyles.Clear();
            grid.RowStyles.Clear();

            grid.AutoScroll = true;
            grid.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            grid.Padding = Padding.Empty;
            grid.Margin = Padding.Empty;
            grid.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            grid.RowCount = r;
            grid.ColumnCount = c;

            float colPct = 100f / c;
            float rowPct = 100f / r;
            for (int i = 0; i < c; i++)
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, colPct));
            for (int i = 0; i < r; i++)
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, rowPct));

            var tasks = new List<Task>();
            for (int row = 0; row < r; row++)
            {
                for (int col = 0; col < c; col++)
                {
                    int idx = poleCisel[row, col];
                    string hex = EmojiHex[idx];

                    var pb = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Margin = Padding.Empty,
                        BackColor = Color.Transparent
                    };

                    grid.Controls.Add(pb, col, row);
                    tasks.Add(AssignEmojiAsync(pb, hex));
                }
            }
            grid.ResumeLayout();
            await Task.WhenAll(tasks);
        }

        private async Task AssignEmojiAsync(PictureBox pb, string hex)
        {
            try
            {
                var img = await GetEmojiImageAsync(hex);
                pb.Image = img;
            }
            catch
            {
                // Placeholder – bez GDI+ kreslení = méně CA1416 spotů,
                // ale nechám ti současnou variantu, jen ji označí [SupportedOSPlatform]
                var bmp = new Bitmap(72, 72);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.LightGray);
                    using (var pen = new Pen(Color.DarkGray, 2))
                    {
                        g.DrawRectangle(pen, 1, 1, 70, 70);
                    }
                }
                pb.Image = bmp;
            }
        }

        private async Task<Image> GetEmojiImageAsync(string hexJoined)
        {
            // kandidáti: původní → bez fe0f → bez fe0f a bez 200d
            var candidates = new List<string>
            {
                hexJoined,
                RemoveFe0f(hexJoined),
                ReduceSequence(hexJoined)
            }.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            foreach (var candidate in candidates)
            {
                if (_cache.TryGetValue(candidate, out var cached))
                    return cached;

                var url = string.Format(TwemojiCdn, candidate.ToLowerInvariant());
                try
                {
                    // NET48-friendly: ručně GetAsync + kontrola kódu
                    using (var resp = await _http.GetAsync(url))
                    {
                        if (resp.StatusCode == HttpStatusCode.NotFound)
                            continue; // zkus další variantu

                        resp.EnsureSuccessStatusCode(); // vyhodí obecný HttpRequestException na jiné chyby
                        var data = await resp.Content.ReadAsByteArrayAsync();
                        using (var ms = new MemoryStream(data))
                        {
                            var img = Image.FromStream(ms);
                            _cache[candidate] = img;
                            return img;
                        }
                    }
                }
                catch
                {
                    // ignoruj, zkus další variantu
                }
            }

            throw new InvalidOperationException("Emoji PNG not found on Twemoji for: " + hexJoined);
        }

        private static string RemoveFe0f(string hex)
        {
            var parts = hex.Split('-');
            var kept = new List<string>(parts.Length);
            for (int i = 0; i < parts.Length; i++)
            {
                var p = parts[i];
                if (!p.Equals("fe0f", StringComparison.OrdinalIgnoreCase))
                    kept.Add(p);
            }
            return string.Join("-", kept);
        }

        private static string ReduceSequence(string hex)
        {
            var parts = hex.Split('-');
            var kept = new List<string>(parts.Length);
            for (int i = 0; i < parts.Length; i++)
            {
                var p = parts[i];
                if (p.Equals("fe0f", StringComparison.OrdinalIgnoreCase)) continue;
                if (p.Equals("200d", StringComparison.OrdinalIgnoreCase)) continue;
                kept.Add(p);
            }
            return kept.Count > 0 ? string.Join("-", kept) : hex;
        }

        private async Task<bool> TryLoadEmojiFromUrlAsync(string url)
        {
            try
            {
                using (var resp = await _http.GetAsync(url))
                {
                    if (!resp.IsSuccessStatusCode) return false;
                    var json = await resp.Content.ReadAsStringAsync();
                    var list = ParseChaldaListJson(json);
                    if (list.Count > 0)
                    {
                        EmojiHex = list;
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        /// <summary>
        /// Parser pro chalda-pnuzig/emojis.json:
        /// { "emojis": [ { "code": ["1F600", ...] }, ... ] }
        /// -> "1f600" nebo "1f441-200d-1f5e8-fe0f"
        /// </summary>
        private static List<string> ParseChaldaListJson(string json)
        {
            var result = new List<string>();
            var root = JObject.Parse(json);
            var arr = root["emojis"] as JArray;
            if (arr == null) return result;

            foreach (var e in arr)
            {
                var obj = e as JObject;
                if (obj == null) continue;
                var codeArr = obj["code"] as JArray;
                if (codeArr == null || codeArr.Count == 0) continue;

                var parts = new List<string>(codeArr.Count);
                foreach (var c in codeArr)
                {
                    var s = (c == null) ? null : c.ToString();
                    if (!string.IsNullOrWhiteSpace(s))
                        parts.Add(s.Trim().ToLowerInvariant());
                }
                if (parts.Count > 0)
                    result.Add(string.Join("-", parts));
            }

            // deduplikace
            var dedup = new HashSet<string>(result, StringComparer.OrdinalIgnoreCase);
            return dedup.ToList();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
    }
}
