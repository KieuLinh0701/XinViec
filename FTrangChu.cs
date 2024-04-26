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
    public partial class FTrangChu : Form
    {
        public FTrangChu()
        {
            InitializeComponent();
        }
        private void DongForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimNgay_Click(object sender, EventArgs e)
        {
            FBanDaCoTKChua fBanDaCoTKChua = new FBanDaCoTKChua();
            fBanDaCoTKChua.Show();
        }

        private void btnTaoCV_Click(object sender, EventArgs e)
        {
            FBanDaCoTKChua fBanDaCoTKChua = new FBanDaCoTKChua();
            fBanDaCoTKChua.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FDangNhap fDangNhap = new FDangNhap();
            fDangNhap.Show();
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            FNhanBiet fNhanBiet = new FNhanBiet();
            fNhanBiet.Show();
        }

        private void btnDangTin_Click_1(object sender, EventArgs e)
        {
            FBanDaCoTKChua fBanDaCoTKChua = new FBanDaCoTKChua();
            fBanDaCoTKChua.Show();
        }
    }
}
