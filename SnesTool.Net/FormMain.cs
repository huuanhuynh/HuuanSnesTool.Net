using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SnesTool.Net
{
    public partial class FormMain : Form
    {
        private bool E;
        private const int MEGABIT_withHeader = 131072;
        /// <summary>
        /// 8Megabit
        /// </summary>
        private const int MEGABYTE = 0x100000;
        private const int _4Megabit = 0x80000;
        private const int Megabit = 0x20000;
        private OpenFileDialog ofd = new OpenFileDialog
        {
            Filter = "Tất cả    |*.*|Snes/Sfc|*.smc;*.sfc|Bin          |*.bin",
            Title = "Chọn rom"
        };

        private string _location = "";
        private string _file_name = "";
        private string _name = "";
        private string _ext = "";

        public FormMain()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool result;
            if (keyData == Keys.E)
            {
                this.English();
                result = true;
            }
            else if (keyData == Keys.V)
            {
                this.Vietnamese();
                result = true;
            }
            else
            {
                result = base.ProcessCmdKey(ref msg, keyData);
            }
            return result;
        }

        private void English()
        {
            this.E = true;
            this.label1.Text = "Megabit (max 11)";
            toolTip1.SetToolTip(btnCutUFO, "The program is written based on the results cut from SnesTool (snestl12) software on DOS\r\nso the results may not be accurate.");
            this.button1.Text = "Choose rom";
            if (lblFileName.Text == "Tên tập tin") lblFileName.Text = "FileName";
            this.btnCutUFO.Text = "Cut";
            this.btnBinSwapTest.Text = "Swap Binary (test)";
        }

        private void Vietnamese()
        {
            this.E = false;
            this.label1.Text = "Megabit (tối đa 11)";
            toolTip1.SetToolTip(btnCutUFO, "Chương trình được viết dựa theo kết quả cắt ra từ phần mềm SnesTool (snestl12) trên DOS\r\nnên kết quả có thể không chính xác.");
            this.button1.Text = "Chọn rom";
            if (lblFileName.Text == "FileName") lblFileName.Text = "Tên tập tin";
            this.btnCutUFO.Text = "Cắt";
            this.btnBinSwapTest.Text = "Đảo Binary (thử nghiệm)";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ofd.ShowDialog(this) == DialogResult.OK)
            {
                this.textBox1.Text = this.ofd.FileName;
                this.GetName();
                this.GetSize();
                richTextBox1.Clear();
            }
        }

        private SnesRomDump _snesRomInfomation = null;
        private void GetName()
        {
            try
            {
                _file_name = textBox1.Text;
                this._location = Path.GetDirectoryName(this.textBox1.Text);
                this._name = Path.GetFileNameWithoutExtension(this.textBox1.Text);
                this._ext = Path.GetExtension(this.textBox1.Text);
                lblFileName.Text = _name + _ext;
                byte[] bin = File.ReadAllBytes(_file_name);
                _snesRomInfomation = new SnesRomDump(bin);
                propertyGrid1.SelectedObject = _snesRomInfomation;

                btnSnesSwap.Enabled = true;
            }
            catch (Exception ex)
            {
                btnSnesSwap.Enabled = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void GetSize()
        {
            try
            {
                lblFileSize.Text = "" + _snesRomInfomation.Size_Megabyte;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Chuyển số int qua 4 ký tự bin
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string To4Bin(int i)
        {
            if(i>15) throw new Exception("Number>15");
            string result = "000" + Convert.ToString(i, 2);
            while (result.Length > 4)
            {
                result = result.Substring(1);
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                this.DoCut(textBox1.Text);
            }
        }

        private void btnF040Swap_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                this.DoSwapF4MtoMASKROM_forF040(textBox1.Text);
            }
        }
        
        private void btnC801Swap1618_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                this.DoSwap1618MtoMASKROM_forC801(textBox1.Text);
            }
        }
        
        private void btnBinSwapTest_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();

                string writeFileName = Path.Combine(this._location, this._name + "SWAP_TEST" + _ext + ".BIN");
                FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                try
                {
                    byte[] bin = _snesRomInfomation.Data_NoHeader;

                    //byte[] swaped_bin = Swap1618Bin8MtoMask_For_27C801(bin);
                    byte[] result = new byte[MEGABYTE];
                    for (int i = 0; i < result.Length; i++)
                    {
                        result[i] = 0xFF;
                    }
                    //if (bin.Length != MEGABYTE)
                    //{
                    //    throw new Exception("Sai kích thước 8 Megabit (1 Megabyte 0x100000).");
                    //}
                    for (int i = 0; i < _4Megabit; i++)
                    {
                        int newPos = SwapBit(i, 1, 19);
                        result[newPos] = bin[i];
                    }
                    //return result;

                    
                    fileStreamW.Write(result, 0, result.Length);
                    richTextBox1.AppendText(this.E ? "Completed." : "Đã chuyển xong.");
                    richTextBox1.AppendText("\n");
                    fileStreamW.Close();
                }
                catch (Exception ex)
                {
                    fileStreamW.Close();
                    this.richTextBox1.AppendText(ex.Message);
                    this.richTextBox1.AppendText("\n");
                }
            }
        }

        private void btn1617Swap_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                this.Do16171618SwapF4MtoMASKROM_forF040(textBox1.Text);
            }
        }

        private void btnSEGA_Swap_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                this.DoSeGaSwap(textBox1.Text);
            }
        }

        private void btnSnesSwap_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                DoSwap8MtoMASKROM_for27C801(_snesRomInfomation.Data_NoHeader);
            }
        }

        private void btnSplit322160_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                DoSplit_322_160(_snesRomInfomation.Data_NoHeader);
            }
        }

        private void btnHi2Lo_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.textBox1.Text))
            {
                this.richTextBox1.Clear();
                this.DoSwapSnes2MHiLo(textBox1.Text);
            }
        }

        private void Do16171618SwapF4MtoMASKROM_forF040(string fileName)
        {
            //FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //BinaryReader binaryReader = new BinaryReader(fileStream);
            string writeFileName = Path.Combine(this._location, this._name + "F040SWAP1617.1618" + _ext + ".BIN");
            FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
            try
            {
                //byte[] bin = File.ReadAllBytes(fileName);
                byte[] bin = _snesRomInfomation.Data_NoHeader;
                byte[] swaped_bin = new byte[_4Megabit];
                if (bin.Length != _4Megabit)
                {
                    throw new Exception("Sai kích thước 4 Megabit (0.5 Megabyte 0x80000).");
                }
                for (int i = 0; i < _4Megabit; i++)
                {
                    int newPos = SwapBit(i, 16, 17);
                    newPos = SwapBit(newPos, 16, 18);
                    swaped_bin[newPos] = bin[i];
                }
                //return result;

                //fileStreamW.SetLength(524288); // 4MBit
                fileStreamW.Write(swaped_bin, 0, swaped_bin.Length);
                //for (int i = 0; i < bin.Length; i++) // Đã thay thế bởi dòng trên.
                //{
                //    int newPos = SwapPostF4MtoMASK(i);
                //    fileStreamW.Position = newPos;
                //    fileStreamW.WriteByte(bin[i]);
                //}

                this.richTextBox1.AppendText(this.E ? "Completed." : "Đã chuyển xong.");
                this.richTextBox1.AppendText("\n" + writeFileName);
                fileStreamW.Close();
            }
            catch (Exception ex)
            {
                fileStreamW.Close();
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
        }


        private void DoSeGaSwap(string fileName)
        {
            //FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //BinaryReader binaryReader = new BinaryReader(fileStream);
            //string writeFileName = Path.Combine(this._location, this._name + "_SEGA_SWAPPED" + _ext + ".BIN");
            //FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
            try
            {
                //byte[] bin = File.ReadAllBytes(fileName);
                byte[] bin = _snesRomInfomation.Data_NoHeader;

                int full_block_count = bin.Length / _4Megabit;
                for (int i = 0; i < full_block_count; i++)
                {
                    byte[] one_block = new byte[_4Megabit];
                    Buffer.BlockCopy(bin, i * _4Megabit, one_block, 0, _4Megabit);
                    //byte[] swapped_bin = SwapBin8MtoMask_For_27C801(one_block);
                    byte[] swapped_bin = new byte[_4Megabit];

                    for (int i2 = 0; i2 < _4Megabit; i2++)
                    {
                        int newPos = XorBit(i2, 0);
                        swapped_bin[newPos] = one_block[i2];
                    }

                    string writeFileName = Path.Combine(this._location, this._name + "_" + (i + 1) + "_SEGA_SWAPPED" + To4Bin(i) + ".BIN");
                    FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                    fileStreamW.Write(swapped_bin, 0, swapped_bin.Length);
                    fileStreamW.Close();
                    this.richTextBox1.AppendText((E ? "Wrote:" : "Đã ghi: ") + writeFileName);
                    this.richTextBox1.AppendText("\n");
                }


                //byte[] swaped_bin = new byte[bin.Length];
                
                //for (int i = 0; i < bin.Length; i++)
                //{
                //    int newPos = XorBit(i, 0);
                //    swaped_bin[newPos] = bin[i];
                //}
                //return result;

                //fileStreamW.SetLength(524288); // 4MBit
                //fileStreamW.Write(swaped_bin, 0, swaped_bin.Length);
                //for (int i = 0; i < bin.Length; i++) // Đã thay thế bởi dòng trên.
                //{
                //    int newPos = SwapPostF4MtoMASK(i);
                //    fileStreamW.Position = newPos;
                //    fileStreamW.WriteByte(bin[i]);
                //}

                //this.richTextBox1.AppendText(this.E ? "Completed." : "Đã chuyển xong.");
                //this.richTextBox1.AppendText("\n" + writeFileName);
                //fileStreamW.Close();
            }
            catch (Exception ex)
            {
                //fileStreamW.Close();
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
        }


        private void DoSwapF4MtoMASKROM_forF040(string fileName)
        {
            //FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //BinaryReader binaryReader = new BinaryReader(fileStream);
            string writeFileName = Path.Combine(this._location, this._name + "F040SWAP" + _ext + ".BIN");
            FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
            try
            {
                //byte[] bin = File.ReadAllBytes(fileName);
                byte[] bin = _snesRomInfomation.Data_NoHeader;
                byte[] swaped_bin = SwapBin4MtoMask_For_SST39SF040(bin);

                //fileStreamW.SetLength(524288); // 4MBit
                fileStreamW.Write(swaped_bin, 0, swaped_bin.Length);
                //for (int i = 0; i < bin.Length; i++) // Đã thay thế bởi dòng trên.
                //{
                //    int newPos = SwapPostF4MtoMASK(i);
                //    fileStreamW.Position = newPos;
                //    fileStreamW.WriteByte(bin[i]);
                //}

                this.richTextBox1.AppendText(this.E ? "Completed." : "Đã chuyển xong.");
                this.richTextBox1.AppendText("\n");
                fileStreamW.Close();
            }
            catch (Exception ex)
            {
                fileStreamW.Close();
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
        }
        
        private void DoSwap1618MtoMASKROM_forC801(string fileName)
        {
            //FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //BinaryReader binaryReader = new BinaryReader(fileStream);
            string writeFileName = Path.Combine(this._location, this._name + "SWAP1618" + _ext + ".BIN");
            FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
            try
            {
                //byte[] bin = File.ReadAllBytes(fileName);
                byte[] bin = _snesRomInfomation.Data_NoHeader;
                byte[] swaped_bin = Swap1618Bin8MtoMask_For_27C801(bin);

                //fileStreamW.SetLength(524288); // 4MBit
                fileStreamW.Write(swaped_bin, 0, swaped_bin.Length);
                //for (int i = 0; i < bin.Length; i++) // Đã thay thế bởi dòng trên.
                //{
                //    int newPos = SwapPostF4MtoMASK(i);
                //    fileStreamW.Position = newPos;
                //    fileStreamW.WriteByte(bin[i]);
                //}

                this.richTextBox1.AppendText(this.E ? "Completed." : "Đã chuyển xong.");
                this.richTextBox1.AppendText("\n");
                fileStreamW.Close();
            }
            catch (Exception ex)
            {
                fileStreamW.Close();
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
        }

        private void DoSwap8MtoMASKROM_for27C801(byte[] bin)
        {
            try
            {
                // phần nguyên MEGABYTE
                int full_block_count = bin.Length / MEGABYTE;
                for (int i = 0; i < full_block_count; i++)
                {
                    byte[] one_block = new byte[MEGABYTE];
                    Buffer.BlockCopy(bin, i * MEGABYTE, one_block, 0, MEGABYTE);
                    byte[] swapped_bin = SwapBin8MtoMask_For_27C801(one_block);

                    string writeFileName = Path.Combine(this._location, this._name + "_" + (i + 1) + "_C801SWAP_" + _snesRomInfomation .BankType + _ext + ".BIN");
                    FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                    fileStreamW.Write(swapped_bin, 0, swapped_bin.Length);
                    fileStreamW.Close();
                    this.richTextBox1.AppendText((E ? "Wrote:" : "Đã ghi: ") + writeFileName );
                    this.richTextBox1.AppendText("\n");
                }

                // phần còn lại 0.5MEGABYTE nếu có
                // ghi luôn 2 loại
                var rest = bin.Length % MEGABYTE;
                // Ghi 8Mbit cho C801
                if (rest > 0)
                {
                    byte[] one_block = new byte[MEGABYTE];
                    Buffer.BlockCopy(bin, full_block_count * MEGABYTE, one_block, 0, rest);
                    byte[] swapped_bin = SwapBin8MtoMask_For_27C801(one_block);

                    string writeFileName = Path.Combine(this._location, this._name + "_" + (full_block_count + 1) + "_C801SWAP_" + _snesRomInfomation.BankType + _ext + ".BIN");
                    FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                    fileStreamW.Write(swapped_bin, 0, swapped_bin.Length);
                    fileStreamW.SetLength(MEGABYTE);
                    fileStreamW.Close();
                    this.richTextBox1.AppendText((E ? "Wrote:" : "Đã ghi: ") + writeFileName);
                    this.richTextBox1.AppendText("\n");
                }
                // Ghi 4Mbit cho F040
                if (rest > 0 && rest <= _4Megabit)
                {
                    byte[] half_block = new byte[_4Megabit];
                    Buffer.BlockCopy(bin, full_block_count * MEGABYTE, half_block, 0, rest);
                    byte[] swapped_bin = SwapBin4MtoMask_For_SST39SF040(half_block);

                    string writeFileName = Path.Combine(this._location, this._name + "_" + (full_block_count + 1) + "_F040SWAP_" + _snesRomInfomation.BankType + _ext + ".BIN");
                    FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                    fileStreamW.Write(swapped_bin, 0, swapped_bin.Length);
                    fileStreamW.SetLength(_4Megabit);
                    fileStreamW.Close();
                    this.richTextBox1.AppendText((E ? "Wrote:" : "Đã ghi: ") + writeFileName);
                    this.richTextBox1.AppendText("\n");
                }
                
                this.richTextBox1.AppendText(this.E ? "Completed." : "Đã cắt xong.");
                this.richTextBox1.AppendText("\n");
                
            }
            catch (Exception ex)
            {
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
        }

        private void DoSplit_322_160(byte[] bin)
        {
            try
            {
                // phần nguyên _4Megabit
                int full_block_count = bin.Length / _4Megabit;
                for (int i = 0; i < full_block_count; i++)
                {
                    byte[] one_block = new byte[_4Megabit];
                    Buffer.BlockCopy(bin, i * _4Megabit, one_block, 0, _4Megabit);
                    byte[] swapped_bin = one_block;

                    string writeFileName = Path.Combine(this._location, this._name + "_" + To4Bin(i) + "_322_160SPLIT_" + _snesRomInfomation.BankType + _ext + ".BIN");
                    FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                    fileStreamW.Write(swapped_bin, 0, swapped_bin.Length);
                    fileStreamW.Close();
                    this.richTextBox1.AppendText((E ? "Wrote:" : "Đã ghi: ") + writeFileName);
                    this.richTextBox1.AppendText("\n");
                }

                // phần còn dư lại nếu có
                // ghi luôn 2 loại
                var rest = bin.Length % _4Megabit;
                // Ghi 8Mbit cho C801
                if (rest > 0)
                {
                    byte[] one_block = new byte[_4Megabit];
                    Buffer.BlockCopy(bin, full_block_count * _4Megabit, one_block, 0, rest);
                    byte[] swapped_bin = one_block;

                    string writeFileName = Path.Combine(this._location, this._name + "_" + To4Bin(full_block_count) + "_322_160SPLIT_" + _snesRomInfomation.BankType + _ext + ".BIN");
                    FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                    fileStreamW.Write(swapped_bin, 0, swapped_bin.Length);
                    //fileStreamW.SetLength(_4Megabit);
                    fileStreamW.Close();
                    this.richTextBox1.AppendText((E ? "Wrote:" : "Đã ghi: ") + writeFileName);
                    this.richTextBox1.AppendText("\n");
                }
                
                this.richTextBox1.AppendText(this.E ? "Completed." : "Đã cắt xong.");
                this.richTextBox1.AppendText("\n");

            }
            catch (Exception ex)
            {
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
        }

        private byte[] SwapBin4MtoMask_For_SST39SF040(byte[] bin)
        {
            byte[] result = new byte[_4Megabit];
            if (bin.Length != _4Megabit)
            {
                throw new Exception("Sai kích thước 4 Megabit (0.5 Megabyte 0x80000).");
            }
            for (int i = 0; i < _4Megabit; i++)
            {
                int newPos = SwapPostF040MtoMASK(i);
                result[newPos] = bin[i];
            }
            return result;
        }

        private byte[] SwapBin8MtoMask_For_27C801(byte[] bin_8M)
        {
            byte[] result = new byte[MEGABYTE];
            if (bin_8M.Length != MEGABYTE)
            {
                throw new Exception("Sai kích thước 8 Megabit (1 Megabyte 0x100000).");
            }
            for (int i = 0; i < MEGABYTE; i++)
            {
                int newPos = SwapPostC801MtoMASK(i);
                result[newPos] = bin_8M[i];
            }
            return result;
        }
        
        /// <summary>
        /// Dùng cho 0.5 double
        /// </summary>
        /// <param name="bin_4M"></param>
        /// <returns></returns>
        private byte[] Swap1618Bin8MtoMask_For_27C801(byte[] bin_4M)
        {
            byte[] result = new byte[MEGABYTE];
            if (bin_4M.Length != MEGABYTE)
            {
                throw new Exception("Sai kích thước 8 Megabit (1 Megabyte 0x100000).");
            }
            for (int i = 0; i < MEGABYTE; i++)
            {
                int newPos = Swap1618PostC801MtoMASK(i);
                result[newPos] = bin_4M[i];
            }
            return result;
        }

        private int SwapPostF040MtoMASK(int i)
        {
            i = SwapBit(i, 16, 17);
            i = SwapBit(i, 16, 18);
            return i;
        }

        /// <summary>
        /// Đảo các chân 17-19, 16-18.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int SwapPostC801MtoMASK(int i)
        {
            i = SwapBit(i, 17, 19);
            i = SwapBit(i, 16, 18);
            return i;
        }
        
        /// <summary>
        /// Đảo chân 16-18.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private int Swap1618PostC801MtoMASK(int i)
        {
            i = SwapBit(i, 16, 18);
            return i;
        }

        private void DoSwapSnes2MHiLo(string fileName)
        {
            //FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //BinaryReader binaryReader = new BinaryReader(fileStream);
            
            
            try
            {
                //byte[] bin = File.ReadAllBytes(fileName);
                byte[] bin = _snesRomInfomation.Data_NoHeader;
                int mega_byte = bin.Length / MEGABYTE;
                if (mega_byte * MEGABYTE < bin.Length) mega_byte++;
                byte[] hilo_bin = new byte[mega_byte * MEGABYTE];
                if (mega_byte == 3) hilo_bin = new byte[4*MEGABYTE];

                for (int i = 0; i < bin.Length; i++)
                {
                    int newPos = SwapPostSnes_HiLo(i, mega_byte);
                    hilo_bin[newPos] = bin[i];
                }

                _name += "_HiLo";
                // ghi file đảo tổng
                string writeFileName = Path.Combine(this._location, this._name + _ext);
                FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
                fileStreamW.Write(hilo_bin, 0, hilo_bin.Length);
                fileStreamW.Close();
                // cắt như 801
                DoSwap8MtoMASKROM_for27C801(hilo_bin);

                //fileStreamW.SetLength(mega_byte * MEGABYTE);
                //for (int i = 0; i < bin.Length; i++)
                //{
                //    int newPos = SwapPostSnes2M_HiLo(i, mega_byte);
                //    fileStreamW.Position = newPos;
                //    fileStreamW.WriteByte(bin[i]);
                //}

                //this.richTextBox1.AppendText(this.E ? "Completed." : "Đã chuyển xong.");
                //this.richTextBox1.AppendText("\n");
                //fileStreamW.Close();
            }
            catch (Exception ex)
            {
                //fileStreamW.Close();
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
        }
        private int SwapPostSnes_HiLo(int i, int size_Mbyte)
        {
            i = SwapBit(i, 15, 16);
            i = SwapBit(i, 16, 17);
            i = SwapBit(i, 17, 18);
            i = SwapBit(i, 18, 19);
            if (size_Mbyte > 1) i = SwapBit(i, 19, 20);
            if (size_Mbyte > 2) i = SwapBit(i, 20, 21);
            return i;
        }
        

        private int SwapBit(int i, int A1, int A2)
        {
            // nếu 2 bít ở 2 vị trí bằng nhau thì thôi
            if (((i >> A1) & 1) == ((i >> A2) & 1)) return i;
            // còn không thì đảo 2 bit đó
            i = i ^ (1 << A1);
            i = i ^ (1 << A2);
            return i;
        }
        
        private int XorBit(int i, int A)
        {
            i = i ^ (1 << A);
            return i;
        }

        /// <summary>
        /// Cắt rom snes dùng cho UFO pro8 ...
        /// </summary>
        /// <param name="fileName"></param>
        private void DoCut(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            try
            {
                long length = fileStream.Length;
                int num = (int)(this.numericUpDown1.Value * 131072m);
                int num2 = (int)(length / (long)num);
                if (length % (long)num > 0L)
                {
                    num2++;
                }
                for (int i = 1; i <= num2; i++)
                {
                    byte[] oneBlock = binaryReader.ReadBytes(num);
                    string text = Path.Combine(this._location, this._name + "." + i);
                    this.WriteFile(oneBlock, text, i == num2);
                    this.richTextBox1.AppendText((this.E ? "Write to: " : "Đã ghi: ") + text);
                    this.richTextBox1.AppendText("\n");
                }
                this.richTextBox1.AppendText(this.E ? "Completed." : "Đã cắt xong.");
                this.richTextBox1.AppendText("\n");
            }
            catch (Exception ex)
            {
                this.richTextBox1.AppendText(ex.Message);
                this.richTextBox1.AppendText("\n");
            }
            fileStream.Close();
        }

        private void WriteFile(byte[] oneBlock, string fileName, bool is_last)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
            try
            {
                int num = oneBlock.Length / 131072;
                if (num == 0)
                {
                    num = 1;
                }
                fileStream.SetLength((long)num);
                num <<= 4;
                fileStream.WriteByte((byte)num);
                if (!is_last)
                {
                    fileStream.Position = 2L;
                    int num2 = 64;
                    fileStream.WriteByte((byte)num2);
                }
                fileStream.Position = 512L;
                fileStream.Write(oneBlock, 0, oneBlock.Length);
            }
            catch (Exception ex)
            {
                this.richTextBox1.AppendText((this.E ? "Write error: " : "Ghi lỗi: ") + fileName);
                this.richTextBox1.AppendText("\n" + ex.Message);
                this.richTextBox1.AppendText("\n");
            }
            fileStream.Close();
        }

        private void btnTran_Click(object sender, EventArgs e)
        {
            try
            {
                //new FormTran(_snesRomInfomation.Data_NoHeader).ShowDialog(this);
                new FormTran().ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
