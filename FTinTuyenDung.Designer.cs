namespace XinViec
{
    partial class FTinTuyenDung
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
            this.plUC = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plUCTrai = new System.Windows.Forms.Panel();
            this.plUCPhai = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.plUC.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // plUC
            // 
            this.plUC.AutoScroll = true;
            this.plUC.Controls.Add(this.plUCPhai);
            this.plUC.Controls.Add(this.panel2);
            this.plUC.Controls.Add(this.plUCTrai);
            this.plUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plUC.Location = new System.Drawing.Point(0, 32);
            this.plUC.Name = "plUC";
            this.plUC.Size = new System.Drawing.Size(1205, 657);
            this.plUC.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1205, 32);
            this.panel1.TabIndex = 2;
            // 
            // plUCTrai
            // 
            this.plUCTrai.Dock = System.Windows.Forms.DockStyle.Left;
            this.plUCTrai.Location = new System.Drawing.Point(0, 0);
            this.plUCTrai.Name = "plUCTrai";
            this.plUCTrai.Size = new System.Drawing.Size(571, 657);
            this.plUCTrai.TabIndex = 3;
            // 
            // plUCPhai
            // 
            this.plUCPhai.Dock = System.Windows.Forms.DockStyle.Right;
            this.plUCPhai.Location = new System.Drawing.Point(596, 0);
            this.plUCPhai.Name = "plUCPhai";
            this.plUCPhai.Size = new System.Drawing.Size(559, 657);
            this.plUCPhai.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.vScrollBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1155, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(50, 657);
            this.panel2.TabIndex = 4;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(29, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(21, 657);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll_1);
            // 
            // FTinTuyenDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 689);
            this.Controls.Add(this.plUC);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FTinTuyenDung";
            this.Text = "FTinTuyenDung";
            this.Load += new System.EventHandler(this.FTinTuyenDung_Load);
            this.plUC.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plUC;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel plUCTrai;
        private System.Windows.Forms.Panel plUCPhai;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.VScrollBar vScrollBar1;
    }
}