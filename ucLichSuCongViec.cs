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
    public partial class ucLichSuCongViec : UserControl
    {
        public event ButtonClickEventHandler ChonButtonXemCongViec;
        public event ButtonClickEventHandler1 ChonButtonXemHoSo;
        public event ButtonClickEventHandler1 ChonButtonXemCongTy;
        public event ButtonClickEventHandler ChonButtonRutHoSo;
        public event ButtonClickEventHandler ChonButtonPhanHoi;
        public delegate void ButtonClickEventHandler(string param1, string param2);
        public delegate void ButtonClickEventHandler1(string param);
        public ucLichSuCongViec()
        {
            InitializeComponent();
        }
        private void btnPhanHoi_Click(object sender, EventArgs e)
        {
            ChonButtonPhanHoi?.Invoke(txbEmailCongTy.Text, txbTieuDe.Text);
        }

        private void lblTenHoSo_Click(object sender, EventArgs e)
        {
            ChonButtonXemHoSo?.Invoke(lblTenHoSo.Text);
        }

        private void lblViTriUngTuyen_Click(object sender, EventArgs e)
        {
            ChonButtonXemCongViec?.Invoke(txbEmailCongTy.Text, txbTieuDe.Text);
        }

        private void btnRutHoSo_Click(object sender, EventArgs e)
        {
            ChonButtonRutHoSo?.Invoke(txbEmailCongTy.Text, txbTieuDe.Text);
        }

        private void lblTenCongTy_Click(object sender, EventArgs e)
        {
            ChonButtonXemCongTy?.Invoke(txbEmailCongTy.Text);
        }
    }
}
