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
    public partial class ucLichHenPhongVan : UserControl
    {
        public event ClickEventHandler1 Click_TenCongTy;
        public event ClickEventHandler2 Click_TenTieuDe;
        public delegate void ClickEventHandler1(string param);
        public delegate void ClickEventHandler2(string param1, string param2);
        public ucLichHenPhongVan()
        {
            InitializeComponent();
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
            Click_TenCongTy?.Invoke(txbEmailCongTy.Text);
        }

        private void lblTenCongTy_Click(object sender, EventArgs e)
        {
            Click_TenTieuDe?.Invoke(txbEmailCongTy.Text, lblTieuDe.Text);
        }
    }
}
