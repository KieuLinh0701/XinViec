namespace XinViec
{
    partial class FXacNhanThongTinCTy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FXacNhanThongTinCTy));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnKhong = new Guna.UI2.WinForms.Guna2Button();
            this.btnCo = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTroChuyen = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.btnKhong);
            this.panel1.Controls.Add(this.btnCo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnTroChuyen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 236);
            this.panel1.TabIndex = 1;
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
            this.btnKhong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKhong.ForeColor = System.Drawing.Color.Black;
            this.btnKhong.Location = new System.Drawing.Point(270, 151);
            this.btnKhong.Name = "btnKhong";
            this.btnKhong.Size = new System.Drawing.Size(109, 45);
            this.btnKhong.TabIndex = 45;
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
            this.btnCo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCo.ForeColor = System.Drawing.Color.Black;
            this.btnCo.Location = new System.Drawing.Point(62, 151);
            this.btnCo.Name = "btnCo";
            this.btnCo.Size = new System.Drawing.Size(109, 45);
            this.btnCo.TabIndex = 44;
            this.btnCo.Text = "Có";
            this.btnCo.Click += new System.EventHandler(this.btnCo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 27);
            this.label1.TabIndex = 43;
            this.label1.Text = "Bạn có chắc chắn?";
            // 
            // btnTroChuyen
            // 
            this.btnTroChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTroChuyen.FlatAppearance.BorderSize = 0;
            this.btnTroChuyen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTroChuyen.Image = ((System.Drawing.Image)(resources.GetObject("btnTroChuyen.Image")));
            this.btnTroChuyen.Location = new System.Drawing.Point(401, 0);
            this.btnTroChuyen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTroChuyen.Name = "btnTroChuyen";
            this.btnTroChuyen.Size = new System.Drawing.Size(51, 36);
            this.btnTroChuyen.TabIndex = 42;
            this.btnTroChuyen.UseVisualStyleBackColor = true;
            this.btnTroChuyen.Click += new System.EventHandler(this.btnTroChuyen_Click);
            // 
            // FXacNhanThongTinCTy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 236);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FXacNhanThongTinCTy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FXacNhanCongTy";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2Button btnKhong;
        private Guna.UI2.WinForms.Guna2Button btnCo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTroChuyen;
    }
}