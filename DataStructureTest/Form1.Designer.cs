namespace DataStructureTest
{
    /// <summary>
    /// WinForm 程式, 讀取CSV 轉成list並作查詢
    /// </summary>
    public partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dgv3 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(457, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "讀取檔案";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(33, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(392, 23);
            this.txtFilePath.TabIndex = 1;
            // 
            // dgv1
            // 
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Location = new System.Drawing.Point(14, 97);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowTemplate.Height = 25;
            this.dgv1.Size = new System.Drawing.Size(688, 256);
            this.dgv1.TabIndex = 2;
            // 
            // dgv2
            // 
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Location = new System.Drawing.Point(12, 359);
            this.dgv2.Name = "dgv2";
            this.dgv2.RowTemplate.Height = 25;
            this.dgv2.Size = new System.Drawing.Size(690, 217);
            this.dgv2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(586, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "讀檔狀態";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(708, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(354, 194);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // dgv3
            // 
            this.dgv3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv3.Location = new System.Drawing.Point(708, 227);
            this.dgv3.Name = "dgv3";
            this.dgv3.RowTemplate.Height = 25;
            this.dgv3.Size = new System.Drawing.Size(363, 349);
            this.dgv3.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(457, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "股票查詢";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(551, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(132, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "買賣超Top50";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(33, 56);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(392, 23);
            this.comboBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 588);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dgv3);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv2);
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>
        /// Button1-讀取檔案並顯示全部於dgv1
        /// </summary>
        private System.Windows.Forms.Button button1;

        /// <summary>
        /// 跳出讀取檔案框
        /// </summary>
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        /// <summary>
        /// 顯示Filepath之textbox
        /// </summary>
        private System.Windows.Forms.TextBox txtFilePath;

        /// <summary>
        /// Dgv1, 顯示sdata 型式內容(基本資訊)
        /// </summary>
        private System.Windows.Forms.DataGridView dgv1;

        /// <summary>
        /// Dgv2, 顯示SDetail 型式內容(查詢細節)
        /// </summary>
        private System.Windows.Forms.DataGridView dgv2;

        /// <summary>
        /// label1 顯示讀取狀態
        /// </summary>
        private System.Windows.Forms.Label label1;

        /// <summary>
        /// richTextBox1 顯示跑完的時間
        /// </summary>
        private System.Windows.Forms.RichTextBox richTextBox1;

        /// <summary>
        /// Dgv3, 顯示Top50 型式內容(Top50超買賣)
        /// </summary>
        private System.Windows.Forms.DataGridView dgv3;

        /// <summary>
        /// Button2-根據combobox內容顯示細節資訊於dgv1,2
        /// </summary>
        private System.Windows.Forms.Button button2;

        /// <summary>
        /// Button3-根據combobox內容顯示Top50資訊於dgv3
        /// </summary>
        private System.Windows.Forms.Button button3;

        /// <summary>
        /// comboBox1-會顯示"ID - 股名"選單, 也可以手動輸入ID
        /// </summary>
        private System.Windows.Forms.ComboBox comboBox1;
    }
}