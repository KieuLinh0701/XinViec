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
    public partial class ucViecLamYeuThich : UserControl
    {
        public event ClickEventHandler2 ChonBtnUngTuyen;
        public event ClickEventHandler2 ChonBtnBoYeuThich;
        public event ClickEventHandler2 ChonlblTieuDe;
        public event ClickEventHandler1 ChonlblTenCongTy;
        public delegate void ClickEventHandler2(string param1, string param2);
        public delegate void ClickEventHandler1(string param);

        public ucViecLamYeuThich()
        {
            InitializeComponent();
        }

        private void btnUngTuyen_Click(object sender, EventArgs e)
        {
            ChonBtnUngTuyen?.Invoke(txbEmailCongTy.Text, lblTieuDe.Text);
        }

        private void btnBoYeuThich_Click(object sender, EventArgs e)
        {
            ChonBtnBoYeuThich?.Invoke(txbEmailCongTy.Text, lblTieuDe.Text);
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
            ChonlblTieuDe?.Invoke(txbEmailCongTy.Text, lblTieuDe.Text);
        }

        private void lblTenCongTy_Click(object sender, EventArgs e)
        {
            ChonlblTenCongTy?.Invoke(txbEmailCongTy.Text);
        }
    }
}
