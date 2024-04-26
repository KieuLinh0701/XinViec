namespace XinViec
{
    partial class FHoSoYeuThich
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
            this.plUC = new System.Windows.Forms.Panel();
            this.plFormCha.SuspendLayout();
            this.SuspendLayout();
            // 
            // plFormCha
            // 
            this.plFormCha.Controls.Add(this.plUC);
            this.plFormCha.Controls.Add(this.panel1);
            this.plFormCha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFormCha.Location = new System.Drawing.Point(0, 0);
            this.plFormCha.Name = "plFormCha";
            this.plFormCha.Size = new System.Drawing.Size(1402, 915);
            this.plFormCha.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1402, 49);
            this.panel1.TabIndex = 0;
            // 
            // plUC
            // 
            this.plUC.AutoScroll = true;
            this.plUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plUC.Location = new System.Drawing.Point(0, 49);
            this.plUC.Name = "plUC";
            this.plUC.Size = new System.Drawing.Size(1402, 866);
            this.plUC.TabIndex = 1;
            // 
            // FHoSoYeuThich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1402, 915);
            this.Controls.Add(this.plFormCha);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FHoSoYeuThich";
            this.Text = "FHoSoYeuThich";
            this.Load += new System.EventHandler(this.FHoSoYeuThich_Load);
            this.plFormCha.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plFormCha;
        private System.Windows.Forms.Panel plUC;
        private System.Windows.Forms.Panel panel1;
    }
}