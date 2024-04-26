namespace XinViec
{
    partial class FUngVienKhongPV
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
            this.plFormCha = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plChua = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.plFormCha.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plFormCha
            // 
            this.plFormCha.Controls.Add(this.plChua);
            this.plFormCha.Controls.Add(this.panel1);
            this.plFormCha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFormCha.Location = new System.Drawing.Point(0, 0);
            this.plFormCha.Name = "plFormCha";
            this.plFormCha.Size = new System.Drawing.Size(1247, 732);
            this.plFormCha.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1247, 90);
            this.panel1.TabIndex = 0;
            // 
            // plChua
            // 
            this.plChua.AutoScroll = true;
            this.plChua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plChua.Location = new System.Drawing.Point(0, 90);
            this.plChua.Name = "plChua";
            this.plChua.Size = new System.Drawing.Size(1247, 642);
            this.plChua.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Black", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Teal;
            this.label3.Location = new System.Drawing.Point(227, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(792, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "DANH SÁCH ỨNG VIÊN KHÔNG THAM GIA PHỎNG VẤN";
            // 
            // FUngVienKhongPV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 732);
            this.Controls.Add(this.plFormCha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FUngVienKhongPV";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FUngVienKhongPV_Load);
            this.plFormCha.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plFormCha;
        private System.Windows.Forms.Panel plChua;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
    }
}