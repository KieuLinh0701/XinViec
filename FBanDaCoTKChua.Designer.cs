namespace XinViec
{
    partial class FBanDaCoTKChua
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
            this.btnKhong = new Guna.UI2.WinForms.Guna2Button();
            this.btnCo = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnKhong
            // 
            this.btnKhong.BorderRadius = 18;
            this.btnKhong.BorderThickness = 1;
            this.btnKhong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKhong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKhong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKhong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKhong.FillColor = System.Drawing.Color.MintCream;
            this.btnKhong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhong.ForeColor = System.Drawing.Color.Black;
            this.btnKhong.Location = new System.Drawing.Point(285, 133);
            this.btnKhong.Name = "btnKhong";
            this.btnKhong.Size = new System.Drawing.Size(109, 45);
            this.btnKhong.TabIndex = 11;
            this.btnKhong.Text = "Không";
            this.btnKhong.Click += new System.EventHandler(this.btnKhong_Click);
            // 
            // btnCo
            // 
            this.btnCo.BorderRadius = 18;
            this.btnCo.BorderThickness = 1;
            this.btnCo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCo.FillColor = System.Drawing.Color.Honeydew;
            this.btnCo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCo.ForeColor = System.Drawing.Color.Black;
            this.btnCo.Location = new System.Drawing.Point(77, 133);
            this.btnCo.Name = "btnCo";
            this.btnCo.Size = new System.Drawing.Size(109, 45);
            this.btnCo.TabIndex = 10;
            this.btnCo.Text = "Có";
            this.btnCo.Click += new System.EventHandler(this.btnCo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(124, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.TabIndex = 9;
            this.label1.Text = "Bạn đã có tài khoản ?";
            // 
            // FBanDaCoTKChua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(481, 234);
            this.Controls.Add(this.btnKhong);
            this.Controls.Add(this.btnCo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FBanDaCoTKChua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bạn đã có tài khoản?";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnKhong;
        private Guna.UI2.WinForms.Guna2Button btnCo;
        private System.Windows.Forms.Label label1;
    }
}