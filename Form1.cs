using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiArchIDE
{
    public partial class Form1 : Form
    {
        private string CompilerPath = "";
        private string ProjectFolder = "";
        private bool isInitializing = true;

        // Varsayılan Editör Ayarları
        private Color CurrentEditorBg = Color.FromArgb(0x2B, 0x2B, 0x2B);
        private Color CurrentForeColor = Color.White;
        private int CurrentFontSize = 11;

        public Form1()
        {
            InitializeComponent();
        }

        // Delphi: GetProjectFolder
        private string GetProjectFolder(string arch)
        {
            string baseFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyIDEProjects");
            if (!Directory.Exists(baseFolder)) Directory.CreateDirectory(baseFolder);

            string archFolder = Path.Combine(baseFolder, arch);
            if (!Directory.Exists(archFolder)) Directory.CreateDirectory(archFolder);

            return archFolder;
        }

        // Delphi: PopulateMCUByArch
        private void PopulateMCUByArch(string arch)
        {
            ComboMCU.Items.Clear();
            if (arch == "XC8")
                ComboMCU.Items.AddRange(new string[] { "PIC16F883", "PIC16F886", "PIC18F4520", "PIC18F4550", "PIC18F24j10", "PIC32MX250F128B" });
            else if (arch == "8051")
                ComboMCU.Items.AddRange(new string[] { "AT89C51", "AT89S52", "STC89C52", "DS89C450" });
            else if (arch == "STM8")
                ComboMCU.Items.AddRange(new string[] { "STM8S003F3", "STM8S105C6", "STM8L151C8", "STM8AF6266" });
            else if (arch == "MPASM")
                ComboMCU.Items.AddRange(new string[] { "PIC16F84A", "PIC16F628A", "PIC16F877A", "PIC18F4520", "PIC18F4550", "PIC18F46K22" });
            else if (arch == "AVR")
                ComboMCU.Items.AddRange(new string[] { "atmega328p", "atmega16", "atmega32", "attiny85" });
            else if (arch == "STM32")
                ComboMCU.Items.AddRange(new string[] { "cortex-m3", "cortex-m4", "stm32f103c8", "stm32f407vg" });
            else if (arch == "ESP32")
                ComboMCU.Items.AddRange(new string[] { "esp32", "esp32s2", "esp32c3" });
            else if (arch == "8051_ASM")
                ComboMCU.Items.AddRange(new string[] { "8051_Generic", "AT89C51_Asm" });

            if (ComboMCU.Items.Count > 0)
                ComboMCU.SelectedIndex = 0;
        }

        // Delphi: SaveConfig
        private void SaveConfig()
        {
            string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ide_settings.ini");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Paths]");
            sb.AppendLine($"CompilerPath={CompilerPath}");
            sb.AppendLine("[LastState]");
            sb.AppendLine($"Arch={ComboArch.Text}");
            sb.AppendLine($"MCU={ComboMCU.Text}");
            sb.AppendLine("[EditorCustom]");
            sb.AppendLine($"EditorBgColor={CurrentEditorBg.ToArgb()}");
            sb.AppendLine($"EditorForeColor={CurrentForeColor.ToArgb()}");
            sb.AppendLine($"EditorFontSize={CurrentFontSize}");

            File.WriteAllText(iniPath, sb.ToString(), Encoding.UTF8);
        }

        // Delphi: LoadConfig
        private void LoadConfig()
        {
            ComboArch.Items.Clear();
            ComboArch.Items.AddRange(new string[] { "XC8", "STM8", "8051", "MPASM", "AVR", "STM32", "ESP32", "8051_ASM" });

            string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ide_settings.ini");
            string lastArch = "XC8";
            string lastMcu = "";

            if (File.Exists(iniPath))
            {
                var lines = File.ReadAllLines(iniPath);
                foreach (var line in lines)
                {
                    if (line.StartsWith("CompilerPath=")) CompilerPath = line.Split('=')[1];
                    if (line.StartsWith("Arch=")) lastArch = line.Split('=')[1];
                    if (line.StartsWith("MCU=")) lastMcu = line.Split('=')[1];
                    if (line.StartsWith("EditorBgColor=")) CurrentEditorBg = Color.FromArgb(Convert.ToInt32(line.Split('=')[1]));
                    if (line.StartsWith("EditorForeColor=")) CurrentForeColor = Color.FromArgb(Convert.ToInt32(line.Split('=')[1]));
                    if (line.StartsWith("EditorFontSize=")) CurrentFontSize = Convert.ToInt32(line.Split('=')[1]);
                }
            }

            ComboArch.Text = lastArch;
            PopulateMCUByArch(lastArch);

            if (!string.IsNullOrEmpty(lastMcu) && ComboMCU.Items.Contains(lastMcu))
                ComboMCU.Text = lastMcu;
            else if (ComboMCU.Items.Count > 0)
                ComboMCU.SelectedIndex = 0;

            if (ComboFontSize.Items.Contains(CurrentFontSize.ToString()))
                ComboFontSize.Text = CurrentFontSize.ToString();
            else
                ComboFontSize.Text = "11";

            // Genişletilmiş Arka Plan Senkronizasyonu
            if (CurrentEditorBg.ToArgb() == Color.FromArgb(0x1E, 0x1E, 0x1E).ToArgb()) ComboBgColor.Text = "Siyah";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(15, 24, 42).ToArgb()) ComboBgColor.Text = "Lacivert";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(64, 64, 64).ToArgb()) ComboBgColor.Text = "Klasik Gri";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(40, 44, 52).ToArgb()) ComboBgColor.Text = "One Dark (Atom)";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(30, 30, 30).ToArgb()) ComboBgColor.Text = "VS Dark";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(40, 42, 54).ToArgb()) ComboBgColor.Text = "Dracula";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(46, 52, 64).ToArgb()) ComboBgColor.Text = "Nord Dark";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(40, 40, 40).ToArgb()) ComboBgColor.Text = "Gruvbox Dark";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(39, 40, 34).ToArgb()) ComboBgColor.Text = "Monokai";
            else if (CurrentEditorBg.ToArgb() == Color.FromArgb(245, 245, 245).ToArgb()) ComboBgColor.Text = "Gündüz Işığı";
            else ComboBgColor.Text = "Koyu Kömür";

            // Yazı Rengi Senkronizasyonu
            if (CurrentForeColor.ToArgb() == Color.White.ToArgb()) ComboForeColor.Text = "Beyaz";
            else if (CurrentForeColor.ToArgb() == Color.LightGreen.ToArgb()) ComboForeColor.Text = "Açık Yeşil";
            else if (CurrentForeColor.ToArgb() == Color.Khaki.ToArgb()) ComboForeColor.Text = "Açık Sarı";
            else if (CurrentForeColor.ToArgb() == Color.Cyan.ToArgb()) ComboForeColor.Text = "Turkuaz";
            else if (CurrentForeColor.ToArgb() == Color.FromArgb(255, 180, 100).ToArgb()) ComboForeColor.Text = "Soft Turuncu";
            else if (CurrentForeColor.ToArgb() == Color.FromArgb(255, 150, 180).ToArgb()) ComboForeColor.Text = "Pastel Pembe";
            else if (CurrentForeColor.ToArgb() == Color.FromArgb(50, 50, 50).ToArgb()) ComboForeColor.Text = "Koyu Gri";
            else ComboForeColor.Text = "Beyaz";

            ProjectFolder = GetProjectFolder(ComboArch.Text);
            UpdateButtonCaptions();
        }

        // Delphi: UpdateButtonCaptions
        private void UpdateButtonCaptions()
        {
            if (ComboArch.Text == "MPASM" || ComboArch.Text == "8051_ASM")
            {
                BtnNewC.Text = "Yeni ASM";
                BtnNewH.Text = "Yeni INC";
            }
            else if (ComboArch.Text == "ESP32")
            {
                BtnNewC.Text = "Yeni CPP";
                BtnNewH.Text = "Yeni H";
            }
            else
            {
                BtnNewC.Text = "Yeni C";
                BtnNewH.Text = "Yeni H";
            }
        }

        // Delphi: CreateDefaultSourceFile
        private void CreateDefaultSourceFile()
        {
            string ext = ".c";
            if (ComboArch.Text == "MPASM" || ComboArch.Text == "8051_ASM") ext = ".asm";
            else if (ComboArch.Text == "ESP32") ext = ".cpp";

            string fileName = Path.Combine(ProjectFolder, "main" + ext);
            StringBuilder sb = new StringBuilder();

            if (ComboArch.Text == "XC8")
            {
                sb.AppendLine("#include <xc.h>");
                sb.AppendLine("void main(void) {\n    while(1) { }\n}");
            }
            else if (ComboArch.Text == "8051")
            {
                sb.AppendLine("#include <8051.h>");
                sb.AppendLine("void main(void) {\n    while(1) { }\n}");
            }
            else if (ComboArch.Text == "STM8")
            {
                sb.AppendLine("#include <stdint.h>");
                sb.AppendLine("void main(void) {\n    while(1) { }\n}");
            }
            else if (ComboArch.Text == "AVR")
            {
                sb.AppendLine("#include <avr/io.h>");
                sb.AppendLine("#include <util/delay.h>");
                sb.AppendLine("int main(void) {\n    while(1) { }\n    return 0;\n}");
            }
            else if (ComboArch.Text == "STM32")
            {
                sb.AppendLine("#include \"stm32f10x.h\"");
                sb.AppendLine("int main(void) {\n    while(1) { }\n    return 0;\n}");
            }
            else if (ComboArch.Text == "ESP32")
            {
                sb.AppendLine("#include <stdio.h>");
                sb.AppendLine("#include \"freertos/FreeRTOS.h\"");
                sb.AppendLine("extern \"C\" void app_main(void) {\n    while(1) { }\n}");
            }
            else if (ComboArch.Text == "MPASM")
            {
                sb.AppendLine($"\r\nLIST  P=16F628A\n #INCLUDE <P16F628A.INC> \n __CONFIG   _INTRC_OSC_NOCLKOUT & _WDT_OFF & _PWRTE_OFF & _MCLRE_OFF & _CP_OFF\n ORG   0x0000\n\r\n GOTO  BASLA  \r\n\r\nBASLA:\n\r\n\r\n END \n ");
            }
            else if (ComboArch.Text == "8051_ASM")
            {
                sb.AppendLine("; 8051 Assembly Template\n    ORG 0000H\n    LJMP MAIN\n\n    ORG 0030H\nMAIN:\n    SJMP MAIN\n    END");
            }

            File.WriteAllText(fileName, sb.ToString(), Encoding.UTF8);
            CreateNewTab(fileName);
            RefreshFileList();
        }

        // Delphi: RefreshFileList
        private void RefreshFileList()
        {
            ListFiles1.Items.Clear();
            if (Directory.Exists(ProjectFolder))
            {
                string[] validExtensions = { ".c", ".h", ".asm", ".inc", ".cpp" };
                var files = Directory.GetFiles(ProjectFolder, "*.*")
                                     .Where(f => validExtensions.Contains(Path.GetExtension(f).ToLower()));
                foreach (var file in files)
                {
                    ListFiles1.Items.Add(Path.GetFileName(file));
                }
            }
        }

        private void SetupHighlighting(FastColoredTextBox edt)
        {
            if (edt == null) return;
            edt.TextChanged -= Editor_TextChanged;

            if (ComboArch.Text == "MPASM" || ComboArch.Text == "8051_ASM")
            {
                edt.Language = Language.Custom;
                edt.ClearStylesBuffer();
                edt.Range.ClearStyle();
            }
            else
            {
                edt.Language = Language.CSharp;
            }
        }

        // Delphi: CreateNewTab
        private FastColoredTextBox CreateNewTab(string fileName)
        {
            foreach (TabPage page in PageControl1.TabPages)
            {
                if (page.ToolTipText.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    PageControl1.SelectedTab = page;
                    return page.Controls[0] as FastColoredTextBox;
                }
            }

            TabPage tab = new TabPage(Path.GetFileName(fileName));
            tab.ToolTipText = fileName;

            FastColoredTextBox editor = new FastColoredTextBox();
            editor.Dock = DockStyle.Fill;

            editor.BackColor = CurrentEditorBg;
            editor.Font = new Font("Consolas", CurrentFontSize);
            editor.ForeColor = CurrentForeColor;
            editor.IndentBackColor = Color.FromArgb(CurrentEditorBg.R + 6, CurrentEditorBg.G + 6, CurrentEditorBg.B + 6);
            editor.LineNumberColor = Color.LightGray;

            if (File.Exists(fileName))
                editor.Text = File.ReadAllText(fileName, Encoding.UTF8);

            editor.TextChanged -= Editor_TextChanged;
            SetupHighlighting(editor);

            tab.Controls.Add(editor);
            PageControl1.TabPages.Add(tab);
            PageControl1.SelectedTab = tab;

            return editor;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e) { }

        private FastColoredTextBox GetActiveSynEdit()
        {
            if (PageControl1.SelectedTab != null && PageControl1.SelectedTab.Controls.Count > 0)
            {
                return PageControl1.SelectedTab.Controls[0] as FastColoredTextBox;
            }
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfig();
            isInitializing = false;
            PageControl1.TabPages.Clear();
            CreateDefaultSourceFile();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        private void ComboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;
            if (int.TryParse(ComboFontSize.Text, out int newSize))
            {
                CurrentFontSize = newSize;
                ApplySettingsToAllOpenTabs();
                SaveConfig();
            }
        }

        // 🎨 GELİŞMİŞ VE ZENGİNLEŞTİRİLMİŞ ZEMİN RENKLERİ
        private void ComboBgColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            switch (ComboBgColor.Text)
            {
                case "Siyah":
                    CurrentEditorBg = Color.FromArgb(0x1E, 0x1E, 0x1E);
                    break;
                case "Lacivert":
                    CurrentEditorBg = Color.FromArgb(15, 24, 42);
                    break;
                case "Klasik Gri":
                    CurrentEditorBg = Color.FromArgb(64, 64, 64);
                    break;
                case "One Dark (Atom)":
                    CurrentEditorBg = Color.FromArgb(40, 44, 52);
                    break;
                case "VS Dark":
                    CurrentEditorBg = Color.FromArgb(30, 30, 30);
                    break;
                case "Dracula": // 👈 YENİ: Efsanevi Mor/Koyu Tema
                    CurrentEditorBg = Color.FromArgb(40, 42, 54);
                    break;
                case "Nord Dark": // 👈 YENİ: Gözü Sıfır Yoran Pastel Kutup Teması
                    CurrentEditorBg = Color.FromArgb(46, 52, 64);
                    break;
                case "Gruvbox Dark": // 👈 YENİ: Retro Çamursu/Yeşil Koyu Ton
                    CurrentEditorBg = Color.FromArgb(40, 40, 40);
                    break;
                case "Monokai": // 👈 YENİ: Yüksek Kontrast Canlı Koyu Gri (Sublime Text tarzı)
                    CurrentEditorBg = Color.FromArgb(39, 40, 34);
                    break;
                case "Gündüz Işığı": // 👈 YENİ: Güneşli Ortamlar İçin Temiz Açık Tema
                    CurrentEditorBg = Color.FromArgb(245, 245, 245);
                    // Gündüz ışığında yazı beyaz kalırsa okunmaz, otomatik koyu griye çekelim abi
                    if (ComboForeColor.Text == "Beyaz" || ComboForeColor.Text == "Açık Sarı")
                    {
                        CurrentForeColor = Color.FromArgb(50, 50, 50);
                        ComboForeColor.Text = "Koyu Gri";
                    }
                    break;
                default: // Koyu Kömür
                    CurrentEditorBg = Color.FromArgb(0x2B, 0x2B, 0x2B);
                    break;
            }

            ApplySettingsToAllOpenTabs();
            SaveConfig();
        }

        private void ComboForeColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            switch (ComboForeColor.Text)
            {
                case "Açık Yeşil": CurrentForeColor = Color.LightGreen; break;
                case "Açık Sarı": CurrentForeColor = Color.Khaki; break;
                case "Turkuaz": CurrentForeColor = Color.Cyan; break;
                case "Soft Turuncu": CurrentForeColor = Color.FromArgb(255, 180, 100); break;
                case "Pastel Pembe": CurrentForeColor = Color.FromArgb(255, 150, 180); break;
                case "Koyu Gri": CurrentForeColor = Color.FromArgb(50, 50, 50); break; // 👈 Açık temalar için ideal
                default: CurrentForeColor = Color.White; break;
            }

            ApplySettingsToAllOpenTabs();
            SaveConfig();
        }

        private void ApplySettingsToAllOpenTabs()
        {
            foreach (TabPage page in PageControl1.TabPages)
            {
                if (page.Controls.Count > 0 && page.Controls[0] is FastColoredTextBox targetEditor)
                {
                    targetEditor.BackColor = CurrentEditorBg;
                    targetEditor.ForeColor = CurrentForeColor;
                    targetEditor.Font = new Font("Consolas", CurrentFontSize);
                    targetEditor.IndentBackColor = Color.FromArgb(CurrentEditorBg.R + 6, CurrentEditorBg.G + 6, CurrentEditorBg.B + 6);
                    SetupHighlighting(targetEditor);
                }
            }
        }

        private void ComboArch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            if (string.IsNullOrEmpty(CompilerPath))
            {
                if (MessageBox.Show("Derleyici yolu boş. Şimdi otomatik taratmak veya manuel seçmek ister misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BtnSelectCompiler_Click(this, EventArgs.Empty);
                }
                return;
            }

            if (MessageBox.Show($"{ComboArch.Text} mimarisine geçiliyor. Tüm sekmeler temizlenecek. Derleyici yolunu kontrol ediniz! Devam?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PopulateMCUByArch(ComboArch.Text);
                ProjectFolder = GetProjectFolder(ComboArch.Text);
                UpdateButtonCaptions();
                PageControl1.TabPages.Clear();
                CreateDefaultSourceFile();
                SaveConfig();
            }
            else
            {
                isInitializing = true;
                LoadConfig();
                isInitializing = false;
            }
        }

        private void ComboMCU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!ComboMCU.Items.Contains(ComboMCU.Text)) ComboMCU.Items.Add(ComboMCU.Text);
                SaveConfig();
                e.Handled = true;
                var activeEditor = GetActiveSynEdit();
                if (activeEditor != null) SetupHighlighting(activeEditor);
            }
        }

        private string AutoDetectCompiler(string arch)
        {
            List<string> searchPaths = new List<string>
            {
                @"C:\Program Files",
                @"C:\Program Files (x86)",
                @"C:\Microchip",
                @"C:\",
                AppDomain.CurrentDomain.BaseDirectory
            };

            string targetExe = "";
            if (arch == "XC8") targetExe = "xc8-cc.exe";
            else if (arch == "8051" || arch == "STM8") targetExe = "sdcc.exe";
            else if (arch == "MPASM") targetExe = "mpasmx.exe";
            else if (arch == "AVR") targetExe = "avr-gcc.exe";
            else if (arch == "STM32") targetExe = "arm-none-eabi-gcc.exe";
            else if (arch == "ESP32") targetExe = "xtensa-esp32-elf-gcc.exe";
            else if (arch == "8051_ASM") targetExe = "asema.exe";

            if (string.IsNullOrEmpty(targetExe)) return "";

            foreach (var basePath in searchPaths)
            {
                if (!Directory.Exists(basePath)) continue;
                try
                {
                    var files = Directory.GetFiles(basePath, targetExe, SearchOption.AllDirectories);
                    if (files.Length > 0) return files[0];
                }
                catch { }
            }
            return "";
        }

        private void BtnSelectCompiler_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Şu an seçili olan '{ComboArch.Text}' mimarisi için derleyiciyi otomatik aratıp bulmak ister misiniz?\n\n(Hayır'a basarsanız geleneksel manuel seçim ekranı açılır.)",
                "Derleyici Bulucu",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;
                string detected = AutoDetectCompiler(ComboArch.Text);
                Cursor.Current = Cursors.Default;

                if (!string.IsNullOrEmpty(detected) && File.Exists(detected))
                {
                    CompilerPath = detected;
                    SaveConfig();
                    MessageBox.Show($"Bulundu ve Kaydedildi:\n{detected}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Sistemde otomatik olarak '{ComboArch.Text}' uyumlu derleyici bulunamadı. Lütfen manuel seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SelectCompilerManually();
                }
            }
            else if (result == DialogResult.No)
            {
                SelectCompilerManually();
            }
        }

        private void SelectCompilerManually()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Compiler|*.exe";
                dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    CompilerPath = dialog.FileName;
                    SaveConfig();
                    MessageBox.Show("Derleyici yolu manuel olarak kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Source|*.c;*.h;*.asm;*.inc;*.cpp";
                dialog.InitialDirectory = ProjectFolder;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ProjectFolder = Path.GetDirectoryName(dialog.FileName);
                    CreateNewTab(dialog.FileName);
                    RefreshFileList();
                }
            }
        }

        private void BtnNewC_Click(object sender, EventArgs e)
        {
            PageControl1.TabPages.Clear();
            CreateDefaultSourceFile();
        }

        private void BtnNewH_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                if (ComboArch.Text == "MPASM" || ComboArch.Text == "8051_ASM")
                {
                    dialog.FileName = "include.inc";
                    dialog.Filter = "Include|*.inc";
                }
                else
                {
                    dialog.FileName = "header.h";
                    dialog.Filter = "Header|*.h";
                }
                dialog.InitialDirectory = ProjectFolder;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    StringBuilder sb = new StringBuilder();

                    if (ComboArch.Text == "MPASM" || ComboArch.Text == "8051_ASM")
                    {
                        sb.AppendLine($"; Include file for {ComboMCU.Text}\n#ifdef __INC_DEFS\n  ; sabitler buraya\n#endif");
                    }
                    else
                    {
                        sb.AppendLine("#ifndef HEADER_H\n#define HEADER_H\n\n#endif");
                    }

                    File.WriteAllText(fileName, sb.ToString(), Encoding.UTF8);
                    CreateNewTab(fileName);
                    RefreshFileList();
                }
            }
        }

        private void ListFiles1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListFiles1.SelectedIndex > -1 && !string.IsNullOrEmpty(ProjectFolder))
            {
                string path = Path.Combine(ProjectFolder, ListFiles1.SelectedItem.ToString());
                CreateNewTab(path);
            }
        }

        private void BtnDeleteFile_Click(object sender, EventArgs e)
        {
            if (ListFiles1.SelectedIndex == -1) return;

            string path = Path.Combine(ProjectFolder, ListFiles1.SelectedItem.ToString());
            if (MessageBox.Show("Silinsin mi?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int i = PageControl1.TabPages.Count - 1; i >= 0; i--)
                {
                    if (PageControl1.TabPages[i].ToolTipText.Equals(path, StringComparison.OrdinalIgnoreCase))
                    {
                        PageControl1.TabPages.RemoveAt(i);
                    }
                }
                if (File.Exists(path)) File.Delete(path);
                RefreshFileList();
            }
        }

        private void help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("                    OKU                        \r\n" +
                            "SDCC   : C:\\Program Files\\SDCC\\bin\\sdcc.exe\r\n" +
                            "XC8    : C:\\Program Files\\Microchip\\xc8\\v2.50\\bin\\xc8-cc.exe\r\n" +
                            "AVR    : C:\\avr-gcc\\bin\\avr-gcc.exe\r\n" +
                            "STM32  : C:\\gcc-arm\\bin\\arm-none-eabi-gcc.exe\r\n" +
                            "ESP32  : C:\\esp-idf\\tools\\xtensa-esp32-elf\\...gcc.exe\r\n" +
                            "ASEMA  : C:\\asema\\asema.exe\r\n", "Yardım", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBuild_Click(object sender, EventArgs e)
        {
            string[] delExts = { ".p1", ".d", ".pre", ".obj", ".as", ".s", ".lst", ".rlf", ".cmf", ".sym", ".o", ".sdb", ".elf", ".hxl", ".cod", ".err", ".asm~", ".map" };

            Memo1.Clear();
            var activeEditor = GetActiveSynEdit();

            if (string.IsNullOrEmpty(CompilerPath) || PageControl1.SelectedTab == null || activeEditor == null)
            {
                Memo1.AppendText("⚠️ Hata: Derleyici seçilmemiş veya dosya açık değil!\r\n");
                return;
            }

            string currentFile = PageControl1.SelectedTab.ToolTipText;
            string workingDir = Path.GetDirectoryName(currentFile);

            File.WriteAllText(currentFile, activeEditor.Text, Encoding.UTF8);

            string outExt = ".hex";
            if (ComboArch.Text == "8051" || ComboArch.Text == "STM8") outExt = ".ihx";
            else if (ComboArch.Text == "STM32" || ComboArch.Text == "ESP32") outExt = ".bin";

            string targetOutputDevicePath = Path.ChangeExtension(currentFile, outExt);
            if (File.Exists(targetOutputDevicePath)) File.Delete(targetOutputDevicePath);

            Memo1.AppendText($"🚀 Derleniyor ({ComboArch.Text}): {Path.GetFileName(currentFile)}\r\n");

            string cmdArgs = "";
            if (ComboArch.Text == "XC8")
                cmdArgs = $"-mcpu={ComboMCU.Text} \"{currentFile}\"";
            else if (ComboArch.Text == "STM8")
                cmdArgs = $"-mstm8 --std-c99 \"{currentFile}\"";
            else if (ComboArch.Text == "8051")
                cmdArgs = $"-mmcs51 --model-small \"{currentFile}\"";
            else if (ComboArch.Text == "MPASM")
                cmdArgs = $"/q /p{ComboMCU.Text} \"{currentFile}\"";
            else if (ComboArch.Text == "AVR")
                cmdArgs = $"-mmcu={ComboMCU.Text} -O2 -Wall \"{currentFile}\" -o \"{Path.ChangeExtension(currentFile, ".elf")}\"";
            else if (ComboArch.Text == "STM32")
                cmdArgs = $"-mcpu={ComboMCU.Text} -mthumb -O2 \"{currentFile}\" -o \"{Path.ChangeExtension(currentFile, ".elf")}\"";
            else if (ComboArch.Text == "ESP32")
                cmdArgs = $"-target xtensa-esp32-elf \"{currentFile}\" -o \"{Path.ChangeExtension(currentFile, ".elf")}\"";
            else if (ComboArch.Text == "8051_ASM")
                cmdArgs = $"\"{currentFile}\"";

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = CompilerPath,
                    Arguments = cmdArgs,
                    WorkingDirectory = workingDir,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    StringBuilder outputBuilder = new StringBuilder();
                    StringBuilder errorBuilder = new StringBuilder();

                    process.OutputDataReceived += (s, args) => { if (args.Data != null) outputBuilder.AppendLine(args.Data); };
                    process.ErrorDataReceived += (s, args) => { if (args.Data != null) errorBuilder.AppendLine(args.Data); };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    if (!process.WaitForExit(10000)) { try { process.Kill(); } catch { } }

                    if (outputBuilder.Length > 0) Memo1.AppendText(outputBuilder.ToString());
                    if (errorBuilder.Length > 0) Memo1.AppendText(errorBuilder.ToString());
                }

                if (ComboArch.Text == "8051" && File.Exists(targetOutputDevicePath))
                {
                    string sdccBinDir = Path.GetDirectoryName(CompilerPath);
                    string pureFileNameWithoutExt = Path.GetFileNameWithoutExtension(currentFile);
                    string generatedIhxPath = Path.Combine(workingDir, pureFileNameWithoutExt + ".ihx");
                    string targetHexPath = Path.Combine(workingDir, pureFileNameWithoutExt + ".hex");
                    string targetBinPath = Path.Combine(workingDir, pureFileNameWithoutExt + ".bin");

                    if (File.Exists(generatedIhxPath))
                    {
                        File.Copy(generatedIhxPath, targetHexPath, true);
                        Memo1.AppendText($"⚙️ Dönüştürülüyor: {pureFileNameWithoutExt}.hex oluşturuldu.\r\n");

                        string objCopyExe = Path.Combine(sdccBinDir, "sdobjcopy.exe");
                        if (File.Exists(objCopyExe))
                        {
                            Process startBin = new Process();
                            startBin.StartInfo.FileName = objCopyExe;
                            startBin.StartInfo.Arguments = $"-I ihex -O binary \"{generatedIhxPath}\" \"{targetBinPath}\"";
                            startBin.StartInfo.WorkingDirectory = workingDir;
                            startBin.StartInfo.CreateNoWindow = true;
                            startBin.StartInfo.UseShellExecute = false;
                            startBin.Start();
                            startBin.WaitForExit();
                            Memo1.AppendText($"⚙️ Dönüştürülüyor: {pureFileNameWithoutExt}.bin oluşturuldu.\r\n");
                        }
                    }
                }

                string elfPath = Path.ChangeExtension(currentFile, ".elf");
                if (File.Exists(elfPath) && (ComboArch.Text == "AVR" || ComboArch.Text == "STM32"))
                {
                    string gnuBinDir = Path.GetDirectoryName(CompilerPath);
                    string objCopyName = ComboArch.Text == "AVR" ? "avr-objcopy.exe" : "arm-none-eabi-objcopy.exe";
                    string objCopyExe = Path.Combine(gnuBinDir, objCopyName);

                    if (File.Exists(objCopyExe))
                    {
                        string hexOut = Path.ChangeExtension(currentFile, ".hex");
                        Process conv = new Process();
                        conv.StartInfo.FileName = objCopyExe;
                        conv.StartInfo.Arguments = $"-O ihex \"{elfPath}\" \"{hexOut}\"";
                        conv.StartInfo.WorkingDirectory = workingDir;
                        conv.StartInfo.CreateNoWindow = true;
                        conv.StartInfo.UseShellExecute = false;
                        conv.Start();
                        conv.WaitForExit();
                        Memo1.AppendText($"⚙️ Toolchain: {Path.GetFileName(hexOut)} başarıyla üretildi.\r\n");
                    }
                }

                bool success = File.Exists(targetOutputDevicePath) || File.Exists(Path.ChangeExtension(currentFile, ".hex"));
                if (success)
                {
                    Memo1.AppendText($"\r\n✅ Başarılı: {Path.GetFileNameWithoutExtension(currentFile)} çıktıları üretildi.\r\n");
                    var tempFiles = Directory.GetFiles(workingDir);
                    foreach (var file in tempFiles)
                    {
                        string extL = Path.GetExtension(file).ToLower();
                        if (delExts.Contains(extL) && extL != ".hex" && extL != ".bin")
                        {
                            try { File.Delete(file); } catch { }
                        }
                    }
                    Memo1.AppendText("✨ Klasör temizlendi.\r\n");
                }
                else
                {
                    Memo1.AppendText("\r\n❌ Hata: Derleme başarısız veya çıktı oluşmadı.\r\n");
                }
            }
            catch (Exception ex)
            {
                Memo1.AppendText($"❌ Proses Başlatma Hatası: {ex.Message}\r\n");
            }
        }
    }
}