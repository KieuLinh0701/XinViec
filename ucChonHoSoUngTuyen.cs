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
    public partial class ucChonHoSoUngTuyen : UserControl
    {
        public event ButtonClickEventHandler ChonButtonXem;
        public event ButtonClickEventHandler ChonButtonUngTuyen;
        public delegate void ButtonClickEventHandler(string tenHS);
        public ucChonHoSoUngTuyen()
        {
            InitializeComponent();
        }

        private void lblTenHoSo_Click(object sender, EventArgs e)
        {
            ChonButtonXem?.Invoke(lblTenHoSo.Text);
        }

        private void btnUngTuyen_Click_1(object sender, EventArgs e)
        {
            ChonButtonUngTuyen?.Invoke(lblTenHoSo.Text);
        }
    }
}
