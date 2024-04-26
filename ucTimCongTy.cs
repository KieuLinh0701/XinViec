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
    public partial class ucTimCongTy : UserControl
    {
        public event ClickEventHandler1 ClickTenCTy;
        public delegate void ClickEventHandler1(string param);
        public ucTimCongTy()
        {
            InitializeComponent();
        }

        private void lblTenCongTy_Click(object sender, EventArgs e)
        {
            ClickTenCTy?.Invoke(txbEmailCongTy.Text);
        }
    }
}
