namespace XinViec
{
    partial class XemHoSo
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
            this.plFormCha = new Guna.UI2.WinForms.Guna2Panel();
            this.plKhungHS = new Guna.UI2.WinForms.Guna2Panel();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.plChuaHoSo = new Guna.UI2.WinForms.Guna2Panel();
            this.plFormCha.SuspendLayout();
            this.plKhungHS.SuspendLayout();
            this.SuspendLayout();
            // 
            // plFormCha
            // 
            this.plFormCha.AutoScroll = true;
            this.plFormCha.Controls.Add(this.plKhungHS);
            this.plFormCha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFormCha.Location = new System.Drawing.Point(0, 0);
            this.plFormCha.Name = "plFormCha";
            this.plFormCha.Size = new System.Drawing.Size(1247, 738);
            this.plFormCha.TabIndex = 0;
            // 
            // plKhungHS
            // 
            this.plKhungHS.Controls.Add(this.btnQuayLai);
            this.plKhungHS.Controls.Add(this.plChuaHoSo);
            this.plKhungHS.Dock = System.Windows.Forms.DockStyle.Top;
            this.plKhungHS.Location = new System.Drawing.Point(0, 0);
            this.plKhungHS.Name = "plKhungHS";
            this.plKhungHS.Size = new System.Drawing.Size(1226, 1210);
            this.plKhungHS.TabIndex = 0;
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.FlatAppearance.BorderSize = 0;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Image = global::XinViec.Properties.Resources._134226_back_arrow_left_icon__9_;
            this.btnQuayLai.Location = new System.Drawing.Point(0, 0);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(59, 40);
            this.btnQuayLai.TabIndex = 3;
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // plChuaHoSo
            // 
            this.plChuaHoSo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.plChuaHoSo.AutoScroll = true;
            this.plChuaHoSo.Location = new System.Drawing.Point(205, 0);
            this.plChuaHoSo.Name = "plChuaHoSo";
            this.plChuaHoSo.Size = new System.Drawing.Size(816, 1210);
            this.plChuaHoSo.TabIndex = 4;
            // 
            // XemHoSo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 738);
            this.Controls.Add(this.plFormCha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "XemHoSo";
            this.Text = "XemHoSo";
            this.Load += new System.EventHandler(this.XemHoSo_Load);
            this.plFormCha.ResumeLayout(false);
            this.plKhungHS.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel plFormCha;
        private Guna.UI2.WinForms.Guna2Panel plKhungHS;
        private System.Windows.Forms.Button btnQuayLai;
        private Guna.UI2.WinForms.Guna2Panel plChuaHoSo;
    }
}