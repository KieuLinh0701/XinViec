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
    public partial class FBanDaCoTKChua : Form
    {
        public FBanDaCoTKChua()
        {
            InitializeComponent();
        }

        private void btnCo_Click(object sender, EventArgs e)
        {
            FDangNhap fDangNhap = new FDangNhap();
            this.Close();
            fDangNhap.Show();
        }

        private void btnKhong_Click(object sender, EventArgs e)
        {
            FNhanBiet fNhanBiet = new FNhanBiet();
            this.Close();
            fNhanBiet.Show();
        }
    }
}
