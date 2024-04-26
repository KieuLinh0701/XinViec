using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static XinViec.ucHienHoSo;

namespace XinViec
{
    public partial class UCQLCongViec : UserControl
    {
        public event ButtonClickEventHandler ChonButtonXemTin;
        public event ButtonClickEventHandler ChonButtonXoa;
        public event ButtonClickEventHandler ChonButtonXemHS;
        public event ButtonClickEventHandler ChonButtonSua;
        public string NutTieuDe
        {
            get { return btnTieuDe.Name; }
        }

        // public event EventHandler<string> ChonButtonXem;
        public delegate void ButtonClickEventHandler(string tieuDe);
        public UCQLCongViec()
        {
            InitializeComponent();
            customizeDesing();

        }

        private void customizeDesing()
        {
            plMenu.Visible = false;
        }
        private void hideCaiDat()
        {
            if (plMenu.Visible == true)
                plMenu.Visible = false;
        }
        private void showCaiDat(Panel subCaiDat)
        {
            if (subCaiDat.Visible == false)
            {
                hideCaiDat();
                subCaiDat.Visible = true;
            }
            else
                subCaiDat.Visible = false;
        }

       
        private void btnMenu_Click(object sender, EventArgs e)
        {
            showCaiDat(plMenu);
        }

        private void btnXoaTin_Click(object sender, EventArgs e)
        {
            hideCaiDat();
            ChonButtonXoa?.Invoke(btnTieuDe.Text);
        }

        private void btnSuaTin_Click(object sender, EventArgs e)
        {
            hideCaiDat();
            ChonButtonSua?.Invoke(btnTieuDe.Text);
        }
        
        private void btnTieuDe_Click(object sender, EventArgs e)
        {
            ChonButtonXemTin?.Invoke(btnTieuDe.Text);
        }

        private void btnXemHS_Click(object sender, EventArgs e)
        {
            ChonButtonXemHS?.Invoke(btnTieuDe.Text);
        }
    } 

       
}
