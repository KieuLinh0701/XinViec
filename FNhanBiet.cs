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
    public partial class FNhanBiet : Form
    {
        public FNhanBiet()
        {
            InitializeComponent();
        }

        private void btnNTD_Click(object sender, EventArgs e)
        {
            FdkNhaTuyenDung fDkNTD = new FdkNhaTuyenDung();
            fDkNTD.ShowDialog();
            this.Close();
        }

        private void btnUV_Click(object sender, EventArgs e)
        {
            FdkUngVien fDkUV = new FdkUngVien();
            fDkUV.ShowDialog();
            this.Close();
        }
    }
}
