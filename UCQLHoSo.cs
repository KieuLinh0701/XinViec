using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    public partial class UCQLHoSo : UserControl
    {
        public event ButtonClickEventHandler ChonButtonXemCV;
        public event ButtonClickEventHandler ChonButtonXoa;
        public event ButtonClickEventHandler ChonButtonDuyet;
        public event ButtonClickEventHandler1 ChonButtonXemTTCN;

        public delegate void ButtonClickEventHandler1(string EmailUngVien);
        public delegate void ButtonClickEventHandler(string EmailUngVien, string TenHoSo);
        public UCQLHoSo()
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            hideCaiDat();
            ChonButtonXoa?.Invoke(txtEmailUV.Text, btnXemTTCN.Text);
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            ChonButtonXemCV?.Invoke(txtEmailUV.Text, btnXemTTCN.Text);
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            hideCaiDat();
            ChonButtonDuyet?.Invoke(txtEmailUV.Text, btnXemTTCN.Text);
            

        }

        private void btnXemTTCN_Click(object sender, EventArgs e)
        {
            ChonButtonXemTTCN?.Invoke(txtEmailUV.Text);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            showCaiDat(plMenu);
        }
    }
}
