namespace XinViec
{
    partial class FNhanPhanHoi
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
            this.btnDanhGia = new Guna.UI2.WinForms.Guna2Button();
            this.plFormCha = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.ucNhanPhanHoi3 = new ucNhanPhanHoi();
            this.ucNhanPhanHoi2 = new ucNhanPhanHoi();
            this.ucNhanPhanHoi1 = new ucNhanPhanHoi();
            this.plFormCha.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDanhGia
            // 
            this.btnDanhGia.BorderRadius = 18;
            this.btnDanhGia.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhGia.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhGia.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDanhGia.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDanhGia.FillColor = System.Drawing.Color.OliveDrab;
            this.btnDanhGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDanhGia.ForeColor = System.Drawing.Color.White;
            this.btnDanhGia.Location = new System.Drawing.Point(179, 157);
            this.btnDanhGia.Name = "btnDanhGia";
            this.btnDanhGia.Size = new System.Drawing.Size(146, 45);
            this.btnDanhGia.TabIndex = 5;
            this.btnDanhGia.Text = "Gửi đánh giá";
            // 
            // plFormCha
            // 
            this.plFormCha.Controls.Add(this.panel2);
            this.plFormCha.Controls.Add(this.btnDanhGia);
            this.plFormCha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFormCha.Location = new System.Drawing.Point(0, 0);
            this.plFormCha.Name = "plFormCha";
            this.plFormCha.Size = new System.Drawing.Size(1205, 689);
            this.plFormCha.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.guna2Panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1205, 689);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ucNhanPhanHoi3);
            this.panel1.Controls.Add(this.ucNhanPhanHoi2);
            this.panel1.Controls.Add(this.ucNhanPhanHoi1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1205, 679);
            this.panel1.TabIndex = 4;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1205, 10);
            this.guna2Panel1.TabIndex = 1;
            // 
            // ucNhanPhanHoi3
            // 
            this.ucNhanPhanHoi3.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucNhanPhanHoi3.Location = new System.Drawing.Point(0, 356);
            this.ucNhanPhanHoi3.Name = "ucNhanPhanHoi3";
            this.ucNhanPhanHoi3.Size = new System.Drawing.Size(1205, 178);
            this.ucNhanPhanHoi3.TabIndex = 2;
            // 
            // ucNhanPhanHoi2
            // 
            this.ucNhanPhanHoi2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucNhanPhanHoi2.Location = new System.Drawing.Point(0, 178);
            this.ucNhanPhanHoi2.Name = "ucNhanPhanHoi2";
            this.ucNhanPhanHoi2.Size = new System.Drawing.Size(1205, 178);
            this.ucNhanPhanHoi2.TabIndex = 1;
            // 
            // ucNhanPhanHoi1
            // 
            this.ucNhanPhanHoi1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucNhanPhanHoi1.Location = new System.Drawing.Point(0, 0);
            this.ucNhanPhanHoi1.Name = "ucNhanPhanHoi1";
            this.ucNhanPhanHoi1.Size = new System.Drawing.Size(1205, 178);
            this.ucNhanPhanHoi1.TabIndex = 0;
            // 
            // FNhanPhanHoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 689);
            this.Controls.Add(this.plFormCha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FNhanPhanHoi";
            this.Text = "Phản hồi, góp ý";
            this.plFormCha.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnDanhGia;
        private System.Windows.Forms.Panel plFormCha;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Panel panel1;
        private ucNhanPhanHoi ucNhanPhanHoi1;
        private ucNhanPhanHoi ucNhanPhanHoi3;
        private ucNhanPhanHoi ucNhanPhanHoi2;
    }
}