namespace MultiArchIDE
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ComboArch = new System.Windows.Forms.ToolStripComboBox();
            this.ComboMCU = new System.Windows.Forms.ToolStripComboBox();
            this.BtnSelectCompiler = new System.Windows.Forms.ToolStripButton();
            this.BtnOpen = new System.Windows.Forms.ToolStripButton();
            this.BtnNewC = new System.Windows.Forms.ToolStripButton();
            this.BtnNewH = new System.Windows.Forms.ToolStripButton();
            this.BtnBuild = new System.Windows.Forms.ToolStripButton();
            this.BtnDeleteFile = new System.Windows.Forms.ToolStripButton();
            this.help = new System.Windows.Forms.ToolStripButton();
            this.ComboFontSize = new System.Windows.Forms.ToolStripComboBox();
            this.ComboBgColor = new System.Windows.Forms.ToolStripComboBox();
            this.ComboForeColor = new System.Windows.Forms.ToolStripComboBox();
            this.ListFiles1 = new System.Windows.Forms.ListBox();
            this.Memo1 = new System.Windows.Forms.TextBox();
            this.PageControl1 = new System.Windows.Forms.TabControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComboArch,
            this.ComboMCU,
            this.BtnSelectCompiler,
            this.BtnOpen,
            this.BtnNewC,
            this.BtnNewH,
            this.BtnBuild,
            this.BtnDeleteFile,
            this.help,
            this.ComboFontSize,
            this.ComboBgColor,
            this.ComboForeColor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1251, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ComboArch
            // 
            this.ComboArch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboArch.Name = "ComboArch";
            this.ComboArch.Size = new System.Drawing.Size(121, 25);
            this.ComboArch.SelectedIndexChanged += new System.EventHandler(this.ComboArch_SelectedIndexChanged);
            // 
            // ComboMCU
            // 
            this.ComboMCU.Name = "ComboMCU";
            this.ComboMCU.Size = new System.Drawing.Size(121, 25);
            this.ComboMCU.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ComboMCU_KeyPress);
            // 
            // BtnSelectCompiler
            // 
            this.BtnSelectCompiler.Name = "BtnSelectCompiler";
            this.BtnSelectCompiler.Size = new System.Drawing.Size(77, 22);
            this.BtnSelectCompiler.Text = "Derleyici Seç";
            this.BtnSelectCompiler.Click += new System.EventHandler(this.BtnSelectCompiler_Click);
            // 
            // BtnOpen
            // 
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(60, 22);
            this.BtnOpen.Text = "Dosya Aç";
            this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // BtnNewC
            // 
            this.BtnNewC.Name = "BtnNewC";
            this.BtnNewC.Size = new System.Drawing.Size(44, 22);
            this.BtnNewC.Text = "Yeni C";
            this.BtnNewC.Click += new System.EventHandler(this.BtnNewC_Click);
            // 
            // BtnNewH
            // 
            this.BtnNewH.Name = "BtnNewH";
            this.BtnNewH.Size = new System.Drawing.Size(45, 22);
            this.BtnNewH.Text = "Yeni H";
            this.BtnNewH.Click += new System.EventHandler(this.BtnNewH_Click);
            // 
            // BtnBuild
            // 
            this.BtnBuild.Name = "BtnBuild";
            this.BtnBuild.Size = new System.Drawing.Size(38, 22);
            this.BtnBuild.Text = "Derle";
            this.BtnBuild.Click += new System.EventHandler(this.BtnBuild_Click);
            // 
            // BtnDeleteFile
            // 
            this.BtnDeleteFile.Name = "BtnDeleteFile";
            this.BtnDeleteFile.Size = new System.Drawing.Size(58, 22);
            this.BtnDeleteFile.Text = "Dosya Sil";
            this.BtnDeleteFile.Click += new System.EventHandler(this.BtnDeleteFile_Click);
            // 
            // help
            // 
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(48, 22);
            this.help.Text = "Yardım";
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // ComboFontSize
            // 
            this.ComboFontSize.Items.AddRange(new object[] {
            "9",
            "10",
            "11",
            "12",
            "14",
            "16",
            "18",
            "20"});
            this.ComboFontSize.MergeIndex = 0;
            this.ComboFontSize.Name = "ComboFontSize";
            this.ComboFontSize.Size = new System.Drawing.Size(121, 25);
            this.ComboFontSize.SelectedIndexChanged += new System.EventHandler(this.ComboFontSize_SelectedIndexChanged);
            // 
            // ComboBgColor
            // 
            this.ComboBgColor.Items.AddRange(new object[] {
            "Koyu",
            "Kömür",
            "Siyah",
            "Lacivert",
            "Klasik Gri",
            "Siyah",
            "Lacivert",
            "Klasik Gri",
            "Koyu Kömür",
            "One Dark (Atom) ",
            "VS Dark",
            "Dracula",
            "Nord Dark",
            " Gruvbox Dark",
            " Monokai ",
            "Gündüz Işığı"});
            this.ComboBgColor.Name = "ComboBgColor";
            this.ComboBgColor.Size = new System.Drawing.Size(121, 25);
            this.ComboBgColor.SelectedIndexChanged += new System.EventHandler(this.ComboBgColor_SelectedIndexChanged);
            // 
            // ComboForeColor
            // 
            this.ComboForeColor.Items.AddRange(new object[] {
            "Beyaz",
            "Açık Yeşil",
            "Açık Sarı",
            "Turkuaz",
            " Soft Turuncu",
            "Pastel Pembe",
            "Koyu Gri",
            " Siyah",
            " Koyu Füme",
            "Gece Mavisi",
            "Kiremit Kırmızısı"});
            this.ComboForeColor.MergeIndex = 0;
            this.ComboForeColor.Name = "ComboForeColor";
            this.ComboForeColor.Size = new System.Drawing.Size(121, 25);
            this.ComboForeColor.SelectedIndexChanged += new System.EventHandler(this.ComboForeColor_SelectedIndexChanged);
            // 
            // ListFiles1
            // 
            this.ListFiles1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ListFiles1.FormattingEnabled = true;
            this.ListFiles1.Location = new System.Drawing.Point(0, 25);
            this.ListFiles1.Name = "ListFiles1";
            this.ListFiles1.Size = new System.Drawing.Size(180, 536);
            this.ListFiles1.TabIndex = 1;
            this.ListFiles1.SelectedIndexChanged += new System.EventHandler(this.ListFiles1_SelectedIndexChanged);
            // 
            // Memo1
            // 
            this.Memo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Memo1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Memo1.Font = new System.Drawing.Font("Consolas", 10F);
            this.Memo1.ForeColor = System.Drawing.Color.LightGray;
            this.Memo1.Location = new System.Drawing.Point(180, 441);
            this.Memo1.Multiline = true;
            this.Memo1.Name = "Memo1";
            this.Memo1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Memo1.Size = new System.Drawing.Size(1071, 120);
            this.Memo1.TabIndex = 2;
            // 
            // PageControl1
            // 
            this.PageControl1.AllowDrop = true;
            this.PageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PageControl1.Location = new System.Drawing.Point(180, 25);
            this.PageControl1.Name = "PageControl1";
            this.PageControl1.SelectedIndex = 0;
            this.PageControl1.Size = new System.Drawing.Size(1071, 416);
            this.PageControl1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 561);
            this.Controls.Add(this.PageControl1);
            this.Controls.Add(this.Memo1);
            this.Controls.Add(this.ListFiles1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "   KOD YAZ elektronikcierol@hotmail.com ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox ComboArch;
        private System.Windows.Forms.ToolStripComboBox ComboMCU;
        private System.Windows.Forms.ToolStripButton BtnSelectCompiler;
        private System.Windows.Forms.ToolStripButton BtnOpen;
        private System.Windows.Forms.ToolStripButton BtnNewC;
        private System.Windows.Forms.ToolStripButton BtnNewH;
        private System.Windows.Forms.ToolStripButton BtnBuild;
        private System.Windows.Forms.ToolStripButton BtnDeleteFile;
        private System.Windows.Forms.ToolStripButton help;
        private System.Windows.Forms.ListBox ListFiles1;
        private System.Windows.Forms.TextBox Memo1;
        private System.Windows.Forms.TabControl PageControl1;
        private System.Windows.Forms.ToolStripComboBox ComboFontSize;
        private System.Windows.Forms.ToolStripComboBox ComboBgColor;
        private System.Windows.Forms.ToolStripComboBox ComboForeColor;
    }
}