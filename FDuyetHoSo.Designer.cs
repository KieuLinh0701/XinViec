namespace XinViec
{
    partial class FDuyetHoSo
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
            this.btnDapUng = new System.Windows.Forms.Button();
            this.btnKhongDapUng = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDapUng
            // 
            this.btnDapUng.Location = new System.Drawing.Point(12, 63);
            this.btnDapUng.Name = "btnDapUng";
            this.btnDapUng.Size = new System.Drawing.Size(138, 41);
            this.btnDapUng.TabIndex = 0;
            this.btnDapUng.Text = "Đáp ứng";
            this.btnDapUng.UseVisualStyleBackColor = true;
            this.btnDapUng.Click += new System.EventHandler(this.btnDapUng_Click);
            // 
            // btnKhongDapUng
            // 
            this.btnKhongDapUng.Location = new System.Drawing.Point(192, 63);
            this.btnKhongDapUng.Name = "btnKhongDapUng";
            this.btnKhongDapUng.Size = new System.Drawing.Size(130, 41);
            this.btnKhongDapUng.TabIndex = 1;
            this.btnKhongDapUng.Text = "Không đáp ứng";
            this.btnKhongDapUng.UseVisualStyleBackColor = true;
            this.btnKhongDapUng.Click += new System.EventHandler(this.btnKhongDapUng_Click);
            // 
            // FDuyetHoSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(349, 183);
            this.Controls.Add(this.btnKhongDapUng);
            this.Controls.Add(this.btnDapUng);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FDuyetHoSo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xét duyệt hồ sơ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDapUng;
        private System.Windows.Forms.Button btnKhongDapUng;
    }
}