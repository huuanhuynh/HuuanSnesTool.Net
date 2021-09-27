namespace SnesTool.Net
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btnF040Swap = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnCutUFO = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnHi2Lo = new System.Windows.Forms.Button();
            this.btnSnesSwap = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnC801Swap1618 = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.btnTran = new System.Windows.Forms.Button();
            this.btnBinSwapTest = new System.Windows.Forms.Button();
            this.btnSplit322 = new System.Windows.Forms.Button();
            this.btn1617NES = new System.Windows.Forms.Button();
            this.btnSegaSwap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnF040Swap
            // 
            this.btnF040Swap.Location = new System.Drawing.Point(24, 208);
            this.btnF040Swap.Name = "btnF040Swap";
            this.btnF040Swap.Size = new System.Drawing.Size(252, 23);
            this.btnF040Swap.TabIndex = 8;
            this.btnF040Swap.Text = "Đảo bin F040 to MASKROM";
            this.btnF040Swap.UseVisualStyleBackColor = true;
            this.btnF040Swap.Click += new System.EventHandler(this.btnF040Swap_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 333);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(512, 149);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Megabit (tối đa 11)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(263, 75);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // btnCutUFO
            // 
            this.btnCutUFO.Location = new System.Drawing.Point(24, 72);
            this.btnCutUFO.Name = "btnCutUFO";
            this.btnCutUFO.Size = new System.Drawing.Size(233, 23);
            this.btnCutUFO.TabIndex = 3;
            this.btnCutUFO.Text = "Cắt rom cho UFO";
            this.toolTip1.SetToolTip(this.btnCutUFO, "Chương trình được viết dựa theo kết quả cắt ra từ phần mềm SnesTool (snestl12) tr" +
        "ên DOS");
            this.btnCutUFO.UseVisualStyleBackColor = true;
            this.btnCutUFO.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(438, 20);
            this.textBox1.TabIndex = 0;
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
            // btnHi2Lo
            // 
            this.btnHi2Lo.Location = new System.Drawing.Point(24, 174);
            this.btnHi2Lo.Name = "btnHi2Lo";
            this.btnHi2Lo.Size = new System.Drawing.Size(252, 23);
            this.btnHi2Lo.TabIndex = 7;
            this.btnHi2Lo.Text = "Đảo bin SNES HiRom to LoBoard";
            this.btnHi2Lo.UseVisualStyleBackColor = true;
            this.btnHi2Lo.Click += new System.EventHandler(this.btnHi2Lo_Click);
            // 
            // btnSnesSwap
            // 
            this.btnSnesSwap.Location = new System.Drawing.Point(24, 145);
            this.btnSnesSwap.Name = "btnSnesSwap";
            this.btnSnesSwap.Size = new System.Drawing.Size(252, 23);
            this.btnSnesSwap.TabIndex = 6;
            this.btnSnesSwap.Text = "SNES Split and Swap";
            this.toolTip1.SetToolTip(this.btnSnesSwap, "Cắt rom phục vụ cho reproduction.\r\nFile nạp cho M27C801 đã đảo 1 số chân địa chỉ." +
        "\r\nFile nạp cho SST39SF040 đã đảo 1 số chân địa chỉ.");
            this.btnSnesSwap.UseVisualStyleBackColor = true;
            this.btnSnesSwap.Click += new System.EventHandler(this.btnSnesSwap_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.Location = new System.Drawing.Point(537, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(316, 470);
            this.propertyGrid1.TabIndex = 10;
            // 
            // btnC801Swap1618
            // 
            this.btnC801Swap1618.Location = new System.Drawing.Point(24, 237);
            this.btnC801Swap1618.Name = "btnC801Swap1618";
            this.btnC801Swap1618.Size = new System.Drawing.Size(252, 23);
            this.btnC801Swap1618.TabIndex = 6;
            this.btnC801Swap1618.Text = "Đảo bin C801 A16<->A18";
            this.btnC801Swap1618.UseVisualStyleBackColor = true;
            this.btnC801Swap1618.Click += new System.EventHandler(this.btnC801Swap1618_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(12, 35);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(51, 13);
            this.lblFileName.TabIndex = 5;
            this.lblFileName.Text = "FileName";
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(12, 48);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(43, 13);
            this.lblFileSize.TabIndex = 5;
            this.lblFileSize.Text = "FileSize";
            // 
            // btnTran
            // 
            this.btnTran.Location = new System.Drawing.Point(282, 237);
            this.btnTran.Name = "btnTran";
            this.btnTran.Size = new System.Drawing.Size(252, 23);
            this.btnTran.TabIndex = 6;
            this.btnTran.Text = "TRANG";
            this.btnTran.UseVisualStyleBackColor = true;
            this.btnTran.Click += new System.EventHandler(this.btnTran_Click);
            // 
            // btnBinSwapTest
            // 
            this.btnBinSwapTest.Location = new System.Drawing.Point(24, 283);
            this.btnBinSwapTest.Name = "btnBinSwapTest";
            this.btnBinSwapTest.Size = new System.Drawing.Size(252, 23);
            this.btnBinSwapTest.TabIndex = 6;
            this.btnBinSwapTest.Text = "Đảo bin Test";
            this.btnBinSwapTest.UseVisualStyleBackColor = true;
            this.btnBinSwapTest.Click += new System.EventHandler(this.btnBinSwapTest_Click);
            // 
            // btnSplit322
            // 
            this.btnSplit322.Location = new System.Drawing.Point(282, 145);
            this.btnSplit322.Name = "btnSplit322";
            this.btnSplit322.Size = new System.Drawing.Size(252, 23);
            this.btnSplit322.TabIndex = 6;
            this.btnSplit322.Text = "SNES Split 322 160";
            this.btnSplit322.UseVisualStyleBackColor = true;
            this.btnSplit322.Click += new System.EventHandler(this.btnSplit322160_Click);
            // 
            // btn1617NES
            // 
            this.btn1617NES.Location = new System.Drawing.Point(24, 116);
            this.btn1617NES.Name = "btn1617NES";
            this.btn1617NES.Size = new System.Drawing.Size(252, 23);
            this.btn1617NES.TabIndex = 6;
            this.btn1617NES.Text = "040 Swap 16<->17 16<->18 for NES";
            this.btn1617NES.UseVisualStyleBackColor = true;
            this.btn1617NES.Click += new System.EventHandler(this.btn1617Swap_Click);
            // 
            // btnSegaSwap
            // 
            this.btnSegaSwap.Location = new System.Drawing.Point(282, 116);
            this.btnSegaSwap.Name = "btnSegaSwap";
            this.btnSegaSwap.Size = new System.Drawing.Size(252, 23);
            this.btnSegaSwap.TabIndex = 6;
            this.btnSegaSwap.Text = "SEGA Split Swap 0<->1";
            this.btnSegaSwap.UseVisualStyleBackColor = true;
            this.btnSegaSwap.Click += new System.EventHandler(this.btnSEGA_Swap_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 494);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.btnHi2Lo);
            this.Controls.Add(this.btnTran);
            this.Controls.Add(this.btnBinSwapTest);
            this.Controls.Add(this.btnC801Swap1618);
            this.Controls.Add(this.btnSplit322);
            this.Controls.Add(this.btnSegaSwap);
            this.Controls.Add(this.btn1617NES);
            this.Controls.Add(this.btnSnesSwap);
            this.Controls.Add(this.btnF040Swap);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lblFileSize);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnCutUFO);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "SnesTool.Net2021 Press [E] for English, Bấm nút [V] chuyển về Tiếng Việt.";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnF040Swap;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnCutUFO;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnHi2Lo;
        private System.Windows.Forms.Button btnSnesSwap;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnC801Swap1618;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Button btnTran;
        private System.Windows.Forms.Button btnBinSwapTest;
        private System.Windows.Forms.Button btnSplit322;
        private System.Windows.Forms.Button btn1617NES;
        private System.Windows.Forms.Button btnSegaSwap;
    }
}