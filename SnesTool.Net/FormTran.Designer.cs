namespace SnesTool.Net
{
    partial class FormTran
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
            this.lblTBL_ENG = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnPhanTich = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtTextEng = new System.Windows.Forms.RichTextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.txtTextViet = new System.Windows.Forms.RichTextBox();
            this.btnRom_eng = new System.Windows.Forms.Button();
            this.txtROM_ENG = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNoChar = new System.Windows.Forms.Label();
            this.lblLessUse = new System.Windows.Forms.Label();
            this.lblSelectionStart = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnPhanTich2 = new System.Windows.Forms.Button();
            this.txtSearchENG = new System.Windows.Forms.TextBox();
            this.btnSearchEng = new System.Windows.Forms.Button();
            this.btnSearchVi = new System.Windows.Forms.Button();
            this.txtSearchVI = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTBL_ENG
            // 
            this.lblTBL_ENG.AutoSize = true;
            this.lblTBL_ENG.Location = new System.Drawing.Point(5, 32);
            this.lblTBL_ENG.Name = "lblTBL_ENG";
            this.lblTBL_ENG.Size = new System.Drawing.Size(56, 13);
            this.lblTBL_ENG.TabIndex = 0;
            this.lblTBL_ENG.Text = "TBL_ENG";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 136);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(1149, 391);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // btnPhanTich
            // 
            this.btnPhanTich.Location = new System.Drawing.Point(12, 88);
            this.btnPhanTich.Name = "btnPhanTich";
            this.btnPhanTich.Size = new System.Drawing.Size(124, 23);
            this.btnPhanTich.TabIndex = 2;
            this.btnPhanTich.Text = "Phân tích";
            this.btnPhanTich.UseVisualStyleBackColor = true;
            this.btnPhanTich.Click += new System.EventHandler(this.btnPhanTich_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(142, 88);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(454, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(142, 72);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // txtTextEng
            // 
            this.txtTextEng.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextEng.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextEng.Location = new System.Drawing.Point(12, 557);
            this.txtTextEng.Name = "txtTextEng";
            this.txtTextEng.ReadOnly = true;
            this.txtTextEng.Size = new System.Drawing.Size(1071, 85);
            this.txtTextEng.TabIndex = 1;
            this.txtTextEng.Text = "TBL_ENG.TBL";
            this.txtTextEng.SelectionChanged += new System.EventHandler(this.txtTextEng_SelectionChanged);
            this.txtTextEng.Click += new System.EventHandler(this.txtTextEng_SelectionChanged);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(1086, 667);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Áp dụng";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAs.Location = new System.Drawing.Point(1086, 696);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.TabIndex = 2;
            this.btnSaveAs.Text = "Lưu file";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // txtTextViet
            // 
            this.txtTextViet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTextViet.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextViet.Location = new System.Drawing.Point(12, 670);
            this.txtTextViet.Name = "txtTextViet";
            this.txtTextViet.Size = new System.Drawing.Size(1071, 85);
            this.txtTextViet.TabIndex = 1;
            this.txtTextViet.Text = "TBL_ENG.TBL";
            this.txtTextViet.SelectionChanged += new System.EventHandler(this.txtTextEng_SelectionChanged);
            this.txtTextViet.Click += new System.EventHandler(this.txtTextEng_SelectionChanged);
            this.txtTextViet.TextChanged += new System.EventHandler(this.txtTextViet_TextChanged);
            this.txtTextViet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEdit_KeyPress);
            // 
            // btnRom_eng
            // 
            this.btnRom_eng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRom_eng.Location = new System.Drawing.Point(610, 32);
            this.btnRom_eng.Name = "btnRom_eng";
            this.btnRom_eng.Size = new System.Drawing.Size(75, 23);
            this.btnRom_eng.TabIndex = 10;
            this.btnRom_eng.Text = "Chọn";
            this.btnRom_eng.UseVisualStyleBackColor = true;
            this.btnRom_eng.Click += new System.EventHandler(this.btnRom_eng_Click);
            // 
            // txtROM_ENG
            // 
            this.txtROM_ENG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtROM_ENG.Location = new System.Drawing.Point(82, 6);
            this.txtROM_ENG.Name = "txtROM_ENG";
            this.txtROM_ENG.Size = new System.Drawing.Size(1079, 20);
            this.txtROM_ENG.TabIndex = 8;
            this.txtROM_ENG.Text = "ROM.NES";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ROM_ENG";
            // 
            // lblNoChar
            // 
            this.lblNoChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNoChar.AutoSize = true;
            this.lblNoChar.Location = new System.Drawing.Point(13, 654);
            this.lblNoChar.Name = "lblNoChar";
            this.lblNoChar.Size = new System.Drawing.Size(53, 13);
            this.lblNoChar.TabIndex = 11;
            this.lblNoChar.Text = "lblNoChar";
            // 
            // lblLessUse
            // 
            this.lblLessUse.AutoSize = true;
            this.lblLessUse.Location = new System.Drawing.Point(743, 98);
            this.lblLessUse.Name = "lblLessUse";
            this.lblLessUse.Size = new System.Drawing.Size(58, 13);
            this.lblLessUse.TabIndex = 12;
            this.lblLessUse.Text = "lblLessUse";
            // 
            // lblSelectionStart
            // 
            this.lblSelectionStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectionStart.BackColor = System.Drawing.Color.Red;
            this.lblSelectionStart.Location = new System.Drawing.Point(667, 528);
            this.lblSelectionStart.Name = "lblSelectionStart";
            this.lblSelectionStart.Size = new System.Drawing.Size(5, 227);
            this.lblSelectionStart.TabIndex = 13;
            this.lblSelectionStart.Text = "|";
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(691, 32);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 14;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnPhanTich2
            // 
            this.btnPhanTich2.Location = new System.Drawing.Point(602, 88);
            this.btnPhanTich2.Name = "btnPhanTich2";
            this.btnPhanTich2.Size = new System.Drawing.Size(124, 23);
            this.btnPhanTich2.TabIndex = 2;
            this.btnPhanTich2.Text = "Phân tích 2";
            this.btnPhanTich2.UseVisualStyleBackColor = true;
            this.btnPhanTich2.Click += new System.EventHandler(this.btnPhanTich2_Click);
            // 
            // txtSearchENG
            // 
            this.txtSearchENG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchENG.Location = new System.Drawing.Point(12, 114);
            this.txtSearchENG.Name = "txtSearchENG";
            this.txtSearchENG.Size = new System.Drawing.Size(138, 20);
            this.txtSearchENG.TabIndex = 15;
            this.txtSearchENG.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchENG_KeyDown);
            // 
            // btnSearchEng
            // 
            this.btnSearchEng.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchEng.Location = new System.Drawing.Point(167, 112);
            this.btnSearchEng.Name = "btnSearchEng";
            this.btnSearchEng.Size = new System.Drawing.Size(75, 23);
            this.btnSearchEng.TabIndex = 16;
            this.btnSearchEng.Text = "Tìm ENG";
            this.btnSearchEng.UseVisualStyleBackColor = true;
            this.btnSearchEng.Click += new System.EventHandler(this.btnSearchEng_Click);
            // 
            // btnSearchVi
            // 
            this.btnSearchVi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchVi.Location = new System.Drawing.Point(432, 111);
            this.btnSearchVi.Name = "btnSearchVi";
            this.btnSearchVi.Size = new System.Drawing.Size(75, 23);
            this.btnSearchVi.TabIndex = 18;
            this.btnSearchVi.Text = "Tìm VI";
            this.btnSearchVi.UseVisualStyleBackColor = true;
            this.btnSearchVi.Click += new System.EventHandler(this.btnSearchVi_Click);
            // 
            // txtSearchVI
            // 
            this.txtSearchVI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchVI.Location = new System.Drawing.Point(277, 113);
            this.txtSearchVI.Name = "txtSearchVI";
            this.txtSearchVI.Size = new System.Drawing.Size(138, 20);
            this.txtSearchVI.TabIndex = 17;
            this.txtSearchVI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchVI_KeyDown);
            // 
            // FormTran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 754);
            this.Controls.Add(this.btnSearchVi);
            this.Controls.Add(this.txtSearchVI);
            this.Controls.Add(this.btnSearchEng);
            this.Controls.Add(this.txtSearchENG);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.lblLessUse);
            this.Controls.Add(this.lblNoChar);
            this.Controls.Add(this.btnRom_eng);
            this.Controls.Add(this.txtROM_ENG);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPhanTich2);
            this.Controls.Add(this.btnPhanTich);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtTextViet);
            this.Controls.Add(this.txtTextEng);
            this.Controls.Add(this.lblTBL_ENG);
            this.Controls.Add(this.lblSelectionStart);
            this.Name = "FormTran";
            this.Text = "FormTran";
            this.Load += new System.EventHandler(this.FormTran_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTBL_ENG;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnPhanTich;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.RichTextBox txtTextEng;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.RichTextBox txtTextViet;
        private System.Windows.Forms.Button btnRom_eng;
        private System.Windows.Forms.TextBox txtROM_ENG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNoChar;
        private System.Windows.Forms.Label lblLessUse;
        private System.Windows.Forms.Label lblSelectionStart;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnPhanTich2;
        private System.Windows.Forms.TextBox txtSearchENG;
        private System.Windows.Forms.Button btnSearchEng;
        private System.Windows.Forms.Button btnSearchVi;
        private System.Windows.Forms.TextBox txtSearchVI;
    }
}