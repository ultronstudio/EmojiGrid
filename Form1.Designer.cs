namespace DvourozmernePole
{
    partial class oknoProgramu
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRadky = new System.Windows.Forms.NumericUpDown();
            this.txtSloupce = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCisla = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.txtRadky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSloupce)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRadky
            // 
            this.txtRadky.Location = new System.Drawing.Point(243, 33);
            this.txtRadky.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtRadky.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRadky.Name = "txtRadky";
            this.txtRadky.Size = new System.Drawing.Size(75, 23);
            this.txtRadky.TabIndex = 1;
            this.txtRadky.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // txtSloupce
            // 
            this.txtSloupce.Location = new System.Drawing.Point(243, 63);
            this.txtSloupce.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSloupce.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSloupce.Name = "txtSloupce";
            this.txtSloupce.Size = new System.Drawing.Size(75, 23);
            this.txtSloupce.TabIndex = 2;
            this.txtSloupce.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Počet řádků";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Počet sloupců";
            // 
            // txtCisla
            // 
            this.txtCisla.AutoSize = true;
            this.txtCisla.Location = new System.Drawing.Point(9, 0);
            this.txtCisla.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.txtCisla.Name = "txtCisla";
            this.txtCisla.Size = new System.Drawing.Size(46, 15);
            this.txtCisla.TabIndex = 6;
            this.txtCisla.Text = "txtCisla";
            this.txtCisla.Visible = false;
            // 
            // panelGrid
            // 
            this.panelGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelGrid.ColumnCount = 10;
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGrid.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.panelGrid.Location = new System.Drawing.Point(0, 129);
            this.panelGrid.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.RowCount = 10;
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panelGrid.Size = new System.Drawing.Size(887, 300);
            this.panelGrid.TabIndex = 7;
            // 
            // oknoProgramu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 429);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.txtCisla);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSloupce);
            this.Controls.Add(this.txtRadky);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "oknoProgramu";
            this.Text = "Emoji Grid";
            this.Load += new System.EventHandler(this.oknoProgramu_Load);
            this.Click += new System.EventHandler(this.oknoProgramu_Click);
            ((System.ComponentModel.ISupportInitialize)(this.txtRadky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSloupce)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown txtRadky;
        private System.Windows.Forms.NumericUpDown txtSloupce;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtCisla;
        private System.Windows.Forms.TableLayoutPanel panelGrid;
    }
}

