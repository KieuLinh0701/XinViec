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
    public partial class ucDangTinTimViec : UserControl
    {
        public delegate void ButtonClickEventHandler2(string param1, string param2);
        public delegate void ButtonClickEventHandler1(string param1);
        public event ButtonClickEventHandler1 ChonBtnXemCV;
        public event ButtonClickEventHandler2 ChonBtnChinhSua;
        public event ButtonClickEventHandler1 ChonBtnXoa;

        bool click = true;
        public ucDangTinTimViec()
        {
            InitializeComponent();
            plCaiDat.Visible = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ChonBtnXoa?.Invoke(lblNgayDang.Text);
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            ChonBtnChinhSua?.Invoke(lblNgayDang.Text, txbTenHoSo.Text);
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            if (click == true)
            {
                plCaiDat.Visible = true;
                click = false;
            }
            else
            {
                plCaiDat.Visible = false;
                click = true;
            }
        }

        private void btnXemCV_Click(object sender, EventArgs e)
        {
            ChonBtnXemCV?.Invoke(txbTenHoSo.Text);
        }
    }
}
