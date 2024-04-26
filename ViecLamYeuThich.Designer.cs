namespace XinViec
{
    partial class ViecLamYeuThich
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
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnLuuGanNhat = new System.Windows.Forms.RadioButton();
            this.radioLuongCaoNhat = new System.Windows.Forms.RadioButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.plChuaHoSo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSoCVDuocLuu = new System.Windows.Forms.Label();
            this.plFormCha.SuspendLayout();
            this.panel1.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plFormCha
            // 
            this.plFormCha.Controls.Add(this.plChuaHoSo);
            this.plFormCha.Controls.Add(this.guna2Panel1);
            this.plFormCha.Controls.Add(this.panel1);
            this.plFormCha.Controls.Add(this.panel2);
            this.plFormCha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFormCha.Location = new System.Drawing.Point(0, 0);
            this.plFormCha.Name = "plFormCha";
            this.plFormCha.Size = new System.Drawing.Size(1247, 738);
            this.plFormCha.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSoCVDuocLuu);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1247, 97);
            this.panel1.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Black", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Teal;
            this.label11.Location = new System.Drawing.Point(450, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(277, 38);
            this.label11.TabIndex = 1;
            this.label11.Text = "VIỆC LÀM ĐÃ LƯU ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 713);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1247, 25);
            this.panel2.TabIndex = 17;
            // 
            // rbtnLuuGanNhat
            // 
            this.rbtnLuuGanNhat.AutoSize = true;
            this.rbtnLuuGanNhat.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnLuuGanNhat.ForeColor = System.Drawing.Color.DimGray;
            this.rbtnLuuGanNhat.Location = new System.Drawing.Point(275, 9);
            this.rbtnLuuGanNhat.Name = "rbtnLuuGanNhat";
            this.rbtnLuuGanNhat.Size = new System.Drawing.Size(132, 24);
            this.rbtnLuuGanNhat.TabIndex = 24;
            this.rbtnLuuGanNhat.TabStop = true;
            this.rbtnLuuGanNhat.Text = "Lưu gần nhất";
            this.rbtnLuuGanNhat.UseVisualStyleBackColor = true;
            this.rbtnLuuGanNhat.CheckedChanged += new System.EventHandler(this.rbtnLuuGanNhat_CheckedChanged);
            // 
            // radioLuongCaoNhat
            // 
            this.radioLuongCaoNhat.AutoSize = true;
            this.radioLuongCaoNhat.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLuongCaoNhat.ForeColor = System.Drawing.Color.DimGray;
            this.radioLuongCaoNhat.Location = new System.Drawing.Point(433, 9);
            this.radioLuongCaoNhat.Name = "radioLuongCaoNhat";
            this.radioLuongCaoNhat.Size = new System.Drawing.Size(152, 24);
            this.radioLuongCaoNhat.TabIndex = 25;
            this.radioLuongCaoNhat.TabStop = true;
            this.radioLuongCaoNhat.Text = "Lương cao nhất";
            this.radioLuongCaoNhat.UseVisualStyleBackColor = true;
            this.radioLuongCaoNhat.CheckedChanged += new System.EventHandler(this.radioLuongCaoNhat_CheckedChanged);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.radioLuongCaoNhat);
            this.guna2Panel1.Controls.Add(this.rbtnLuuGanNhat);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 97);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1247, 43);
            this.guna2Panel1.TabIndex = 21;
            // 
            // plChuaHoSo
            // 
            this.plChuaHoSo.AutoScroll = true;
            this.plChuaHoSo.BackColor = System.Drawing.SystemColors.Control;
            this.plChuaHoSo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plChuaHoSo.Location = new System.Drawing.Point(0, 140);
            this.plChuaHoSo.Name = "plChuaHoSo";
            this.plChuaHoSo.Size = new System.Drawing.Size(1247, 573);
            this.plChuaHoSo.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(86, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "ƯU TIÊN HIỂN THỊ:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(95, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Công việc được lưu";
            // 
            // lblSoCVDuocLuu
            // 
            this.lblSoCVDuocLuu.AutoSize = true;
            this.lblSoCVDuocLuu.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoCVDuocLuu.ForeColor = System.Drawing.Color.DarkKhaki;
            this.lblSoCVDuocLuu.Location = new System.Drawing.Point(21, 6);
            this.lblSoCVDuocLuu.Name = "lblSoCVDuocLuu";
            this.lblSoCVDuocLuu.Size = new System.Drawing.Size(64, 69);
            this.lblSoCVDuocLuu.TabIndex = 11;
            this.lblSoCVDuocLuu.Text = "0";
            // 
            // ViecLamYeuThich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 738);
            this.Controls.Add(this.plFormCha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViecLamYeuThich";
            this.Text = "ViecLamYeuThich";
            this.Load += new System.EventHandler(this.ViecLamYeuThich_Load);
            this.plFormCha.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plFormCha;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel plChuaHoSo;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.RadioButton radioLuongCaoNhat;
        private System.Windows.Forms.RadioButton rbtnLuuGanNhat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSoCVDuocLuu;
    }
}