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
    public partial class FXacNhanThongTinCTy : Form
    {
        public FXacNhanThongTinCTy()
        {
            InitializeComponent();
        }
        public static bool btnCoClicked = false;

        public void btnCo_Click(object sender, EventArgs e)
        {
            btnCoClicked = true;
            this.Close();
        }

        private void btnKhong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTroChuyen_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
