using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace SnesTool.Net
{
    public partial class FormTran : Form
    {
        private string ROM_NES = "ROM.NES";
        private string ROM_TBL
        {
            get { return ROM_NES.Substring(0, ROM_NES.Length - 4) + ".TBL"; }
        }
        private string ROM_TBL2
        {
            get
            {
                string fileName = ROM_NES.Substring(0, ROM_NES.Length - 4) + ".TBL2";
                if (!File.Exists(fileName)) File.Copy(ROM_TBL, fileName);
                return fileName;
            }
        }
        private string ROM_VI_NES
        {
            get
            {
                string fileName = ROM_NES.Substring(0, ROM_NES.Length - 4) + "_VI.NES";
                if (!File.Exists(fileName)) File.Copy(ROM_NES, fileName);
                return fileName;
            }
        }
        private string ROM_VI_TBL
        {
            get
            {
                string fileName = ROM_NES.Substring(0, ROM_NES.Length - 4) + "_VI.TBL"; 
                if (!File.Exists(fileName)) File.Copy(ROM_TBL, fileName);
                return fileName;
            }
        }
        private string ROM_VI_TBL2
        {
            get
            {
                string fileName = ROM_NES.Substring(0, ROM_NES.Length - 4) + "_VI.TBL2";
                if (!File.Exists(fileName)) File.Copy(ROM_VI_TBL, fileName);
                return fileName;
            }
        }

        private string _last_rom = "ROM.NES";


        private string _directory = "";
        private byte[] _bin, _binV;
        private SortedDictionary<byte, char> _TBL_ENG;
        private SortedDictionary<byte, char> _TBL_VIE;
        private SortedDictionary<string, string> _ENG_DIC = new SortedDictionary<string, string>();
        public FileDialog fileDialog = new OpenFileDialog();
        

        public FormTran()
        {
            InitializeComponent();
            MyInit();
        }
        
        //public FormTran(byte[] bin)
        //{
        //    InitializeComponent();
        //    _bin = bin;
        //    MyInit();
        //}

        private void MyInit()
        {
            LoadLastRom();
            _directory = Path.GetDirectoryName(_last_rom);
            fileDialog.InitialDirectory = _directory;
            ROM_NES = _last_rom;
            Load_ENG_DIC();
        }

        private void LoadLastRom()
        {
            string file = Path.Combine(Application.StartupPath, "lastrom.txt");
            if (File.Exists(file)) _last_rom = File.ReadAllText(file);
        }
        private void SaveLastRom()
        {
            string file = Path.Combine(Application.StartupPath, "lastrom.txt");
            FileStream fileStreamW = new FileStream(file, FileMode.OpenOrCreate);
            var bytes = Encoding.ASCII.GetBytes(_last_rom);
            fileStreamW.Write(bytes, 0, bytes.Length);
            fileStreamW.Close();
        }

        private void FormTran_Load(object sender, EventArgs e)
        {
            ReloadAll(ROM_NES);
        }

        private void ReloadAll(string file_nes)
        {
            ROM_NES = file_nes;
            _directory = Path.GetDirectoryName(ROM_NES);
            
            _TBL_ENG = LoadTBL(ROM_TBL); // txtTBL_ENG.Text);
            _TBL_VIE = LoadTBL(ROM_VI_TBL);
            ROM_NES = Path.GetFullPath(ROM_NES);
            _bin = LoadRom(ROM_NES);
            _binV = LoadRom(ROM_VI_NES);

            lblTBL_ENG.Text = string.Format(
@"ROM_VI = {0}
TBL_ENG = {1}
TBL_VI = {2}
", ROM_VI_NES, ROM_TBL, ROM_VI_TBL);
        }


        //private void btnTBL_VIE_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fileDialog.ShowDialog(this) == DialogResult.OK)
        //        {
        //            txtTBL_VIE.Text = fileDialog.FileName;
        //            _TBL_VIE = LoadTBL(txtTBL_VIE.Text);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:" + ex.Message);
        //    }
        //}

        private int percent = 0;
        private int _bin_length = 0;
        private int _current_i = 0;
        private void btnPhanTich_Click(object sender, EventArgs e)
        {
            if (percent > 0 && percent < 100)
            {
                MessageBox.Show("Đang phân ... tích!");
                return;
            }

            _TBL_ENG = LoadTBL(ROM_TBL);
            _TBL_VIE = LoadTBL(ROM_VI_TBL);

            percent = 0;
            _bin_length = _bin.Length;
            _current_i = 0;
            Timer t1 = new Timer();
            t1.Tick += t1_Tick;
            Thread phantich = new Thread(PhanTich);
            phantich.Start();
            t1.Start();
        }
        private void btnPhanTich2_Click(object sender, EventArgs e)
        {
            if (percent > 0 && percent < 100)
            {
                MessageBox.Show("Đang phân ... tích2!");
                return;
            }

            _TBL_ENG = LoadTBL(ROM_TBL2);
            _TBL_VIE = LoadTBL(ROM_VI_TBL2);

            percent = 0;
            _bin_length = _bin.Length;
            _current_i = 0;
            Timer t1 = new Timer();
            t1.Tick += t1_Tick;
            Thread phantich = new Thread(PhanTich);
            phantich.Start();
            t1.Start();
        }

        void t1_Tick(object sender, EventArgs e)
        {
            if (percent == 100)
            {
                ((Timer)sender).Stop();
                dataGridView1.DataSource = table;
                percent = 0;
                _current_i = 0;
            }
            else
            {
                lblStatus.Text = _current_i + "/" + _bin_length;
                percent = _current_i * 100 / _bin_length;
                progressBar1.Value = percent;
            }
        }

        private DataTable table = null;
        private void PhanTich()
        {
            try
            {
                table = new DataTable();
                table.Columns.Add("TEXT");
                table.Columns.Add("TEXTV");
                table.Columns.Add("START");
                table.Columns.Add("START_HEX");
                table.Columns.Add("END_HEX");
                table.Columns.Add("END");

                int start = 0, textLength = 0;
                string text = "";

                for (int i = 0; i < _bin.Length; i++)
                {
                    _current_i = i;

                    byte hex = _bin[i];

                    if (_TBL_ENG.ContainsKey(hex))
                    {
                        textLength++;
                        text += _TBL_ENG[hex];
                    }
                    else
                    {
                        if (textLength > 4 && HaveWord(text.ToLower()))
                        {
                            DataRow newRow = table.NewRow();
                            newRow["TEXT"] = text;
                            newRow["TEXTV"] = GetTextV(start, i-1);
                            newRow["START"] = start;
                            newRow["START_HEX"] = NumberToHex(start);
                            newRow["END_HEX"] = NumberToHex(i-1);
                            newRow["END"] = i-1;
                            table.Rows.Add(newRow);

                            textLength = 0;
                            text = "";
                            start = i+1; // new start
                        }
                        else
                        {
                            start = i + 1;
                            textLength = 0;
                            text = "";
                        }
                    }
                }

                _current_i = _bin_length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetTextV(int start, int end)
        {
            byte[] bin = new byte[end-start+1];
            Buffer.BlockCopy(_binV, start, bin, 0, bin.Length);
            string text = "";
            foreach (byte b in bin)
            {
                if (_TBL_VIE.ContainsKey(b)) text += _TBL_VIE[b];
                else text += "_";
            }
            
            return text;
        }

        private string NumberToHex(int i)
        {
            return Convert.ToString(i, 16).ToUpper();
        }

        private char _space = ' ';
        private bool HaveWord(string s)
        {
            var ss = s.ToLower().Split(_space, '~', ',', '.','!');//,'1','2','3','4');
            for (int i = 0; i < ss.Length; i++)
            {
                if (ss[i].Length == 0) continue;
                if (_ENG_DIC.ContainsKey(ss[i])) return true;
                if (_ENG_DIC.ContainsKey(ss[i].Substring(1))) return true;
            }
            return false;
        }

        private SortedDictionary<byte, char> LoadTBL(string file)
        {
            SortedDictionary<byte, char> tbl_dic = new SortedDictionary<byte, char>();
            try
            {
                // Mỗi dòng là 1 số HEX AA và 1 ký tự X, dấu = ở giữa
                // AA=X
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    var ss = line.Split('=');
                    if (ss.Length == 2)
                    {
                        tbl_dic.Add(HexToByte(ss[0]), ss[1][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return tbl_dic;
        }

        private byte HexToByte(string s)
        {
            return Convert.ToByte(s, 16);
        }

        private void Load_ENG_DIC()
        {
            try
            {
                _ENG_DIC = new SortedDictionary<string, string>();
                string[] lines = System.IO.File.ReadAllLines("words_alpha.txt");
                foreach (string line in lines)
                {
                    _ENG_DIC[line] = line;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridView1.CurrentRow;
                if (row == null) return;
                txtTextEng.Text = row.Cells["TEXT"].Value.ToString();
                txtTextViet.Text = row.Cells["TEXTV"].Value.ToString();
                txtTextEng.MaxLength = txtTextEng.Text.Length;
                txtTextViet.MaxLength = txtTextViet.Text.Length;
                txt_start = Convert.ToInt32(row.Cells["START"].Value);
                txt_end = Convert.ToInt32(row.Cells["END"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(txtTextEng.Text.Length == txtTextEng.MaxLength && txtTextEng.SelectedText == "")
            {
                txtTextEng.SelectionLength = 1;
            }
        }

        private int txt_start = 0, txt_end = 0;

        private byte CharToVieByte(char c)
        {
            if (_TBL_VIE.ContainsValue(c))
            {
                foreach (KeyValuePair<byte, char> item in _TBL_VIE)
                {
                    if (item.Value == c) return item.Key;
                }
            }

            return 0;
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < txtTextViet.Text.Length; i++)
                {
                    _binV[txt_start + i] = CharToVieByte(txtTextViet.Text[i]);
                }

                dataGridView1.CurrentRow.Cells["TEXTV"].Value = txtTextViet.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Ghi đè " + ROM_VI_NES) == DialogResult.OK)
                {
                    FileStream fileStreamW = new FileStream(ROM_VI_NES, FileMode.OpenOrCreate);
                    fileStreamW.Write(_binV, 0, _binV.Length);
                    fileStreamW.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRom_eng_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    ReloadAll(fileDialog.FileName);
                    _last_rom = ROM_NES;
                    SaveLastRom();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadAll(ROM_NES);
        }

        private byte[] LoadRom(string file)
        {
            _directory = Path.GetDirectoryName(ROM_NES);
            byte[] bin = File.ReadAllBytes(file);
            return bin;
        }

        //private void btnRom_vie_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fileDialog.ShowDialog(this) == DialogResult.OK)
        //        {
        //            txtTBL_ENG.Text = fileDialog.FileName;
        //            _binV = LoadRom(txtRom_Vie.Text);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error:" + ex.Message);
        //    }
        //}

        private void txtTextViet_TextChanged(object sender, EventArgs e)
        {
            if (_TBL_VIE != null && _TBL_VIE.Count > 1)
            {
                CountNoChar(txtTextViet.Text);
            }
        }

        private void CountNoChar(string s)
        {
            lblNoChar.Text = "No char:";
            foreach (char c in s)
            {
                if (!_TBL_VIE.ContainsValue(c))
                {
                    lblNoChar.Text += " " + c;
                }
            }
        }

        private void txtTextEng_SelectionChanged(object sender, EventArgs e)
        {
            RichTextBox txt = sender as RichTextBox;
            if (txt == null) return;
            //int width = CalculatePosition(txt.Text.Substring(0, txt.SelectionStart), txt.Font);
            var point = txt.GetPositionFromCharIndex(txt.SelectionStart);
            lblSelectionStart.Left = 2 + (int)(txt.Left + point.X);
        }

        private int CalculatePosition(string s, Font font)
        {
            while (s.Length > 140)
            {
                s = s.Substring(140);
            }
            var size = txtTextEng.CreateGraphics().MeasureString("A" + s + "A", font);
            int i = (int) (size.Width - 2*(size.Width/(s.Length+2)));
            return 2 + i;
        }

        private void txtSearchENG_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) btnSearchEng.PerformClick();
        }

        private void btnSearchEng_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Columns.Contains("TEXT") && dataGridView1.CurrentRow != null)
                {
                    int i = dataGridView1.CurrentRow.Index + 1;
                    if (i == dataGridView1.RowCount) i = 0;
                    for (; i < dataGridView1.RowCount; i++)
                    {
                        var nextRow = dataGridView1.Rows[i];
                        if (nextRow.Cells["TEXT"].Value.ToString().ToUpper().Contains(txtSearchENG.Text.ToUpper()))
                        {
                            dataGridView1.CurrentCell = nextRow.Cells["TEXT"];
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSearchVi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Columns.Contains("TEXTV") && dataGridView1.CurrentRow != null)
                {
                    int i = dataGridView1.CurrentRow.Index + 1;
                    if (i == dataGridView1.RowCount) i = 0;

                    for (; i < dataGridView1.RowCount; i++)
                    {
                        var nextRow = dataGridView1.Rows[i];
                        if (nextRow.Cells["TEXTV"].Value.ToString().ToUpper().Contains(txtSearchVI.Text.ToUpper()))
                        {
                            dataGridView1.CurrentCell = nextRow.Cells["TEXTV"];
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void txtSearchVI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearchVi.PerformClick();
        }

        

        

    }
}
