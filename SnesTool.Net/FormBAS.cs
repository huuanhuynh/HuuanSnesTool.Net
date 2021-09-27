using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SnesTool.Net
{
	public class FormBAS : Form
	{
		private const int MEGABIT = 131072;

		private OpenFileDialog ofd = new OpenFileDialog
		{
			Filter = "Tất cả    |*.*|Snes/Sfc|*.smc;*.sfc|Bin          |*.bin",
			Title = "Chọn rom"
		};

		private bool E;

		private string _location = "";

		private string _name = "";
		private string _ext = "";

		private IContainer components = null;

		private Button button1;

		private TextBox textBox1;

        private Button button2;

		private RichTextBox richTextBox1;
        private NumericUpDown numericUpDown2;

		private Label label2;

		public FormBAS()
		{
			this.InitializeComponent();
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
			this.label2.Text = "The program is written based on the results cut from SnesTool (snestl12) software on DOS\r\nso the results may not be accurate.";
			this.button1.Text = "Choose rom";
			this.button2.Text = "Cut";
		}

		private void Vietnamese()
		{
			this.E = false;
			this.label2.Text = "Chương trình được viết dựa theo kết quả cắt ra từ phần mềm SnesTool (snestl12) trên DOS\r\nnên kết quả có thể không chính xác.";
			this.button1.Text = "Chọn rom";
			this.button2.Text = "Cắt";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.ofd.ShowDialog(this) == DialogResult.OK)
			{
				this.textBox1.Text = this.ofd.FileName;
				this.GetName();
			}
		}

		private void GetName()
		{
			this._location = Path.GetDirectoryName(this.textBox1.Text);
			this._name = Path.GetFileNameWithoutExtension(this.textBox1.Text);
            this._ext = Path.GetExtension(this.textBox1.Text);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (File.Exists(this.textBox1.Text))
			{
                this.richTextBox1.Clear();
				this.DoSwapF4MtoPRG(textBox1.Text);
			}
		}

		private void DoSwapF4MtoPRG(string fileName)
		{
			//FileStream fileStream = new FileStream(fileName, FileMode.Open);
			//BinaryReader binaryReader = new BinaryReader(fileStream);
            string writeFileName = Path.Combine(this._location, this._name + "F4PRG" + _ext);
            FileStream fileStreamW = new FileStream(writeFileName, FileMode.OpenOrCreate);
			try
			{
                
                byte[] bin = File.ReadAllBytes(fileName);
                fileStreamW.SetLength(524288); // 4MBit
                for (int i = 0; i < bin.Length; i++)
                {
                    int newPos = SwapPostF4MtoPRG(i);
                    fileStreamW.Position = newPos;
                    fileStreamW.WriteByte(bin[i]);
                }
                
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

        private int SwapPostF4MtoPRG(int i)
        {
            i = SwapBit(i, 16, 17);
            i = SwapBit(i, 16, 18);
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(456, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Chọn rom";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(438, 20);
            this.textBox1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(169, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Đảo Flash 4M cho Nes PRG";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 161);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(512, 149);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Đảo địa chỉ đang nghiên cứu";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(54, 135);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown2.TabIndex = 7;
            // 
            // FormBAS
            // 
            this.ClientSize = new System.Drawing.Size(535, 322);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBAS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Address swap (Press E for English)";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}
