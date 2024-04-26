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
    public partial class DoiTenTK : Form
    {
        public DoiTenTK()
        {
            InitializeComponent();
        }

        private void btnDoiTenTK_Click(object sender, EventArgs e)
        {
            ThongBao tb = new ThongBao();
            tb.rtbThongBao.Text = "Bạn muốn đổi tên tài khoản?";
            tb.Show();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        { 
            ThongBao tb = new ThongBao();
            tb.rtbThongBao.Text = "Bạn muốn hủy thay đổi?";
            tb.Show();

        }
    }
}
