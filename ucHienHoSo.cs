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
    public partial class ucHienHoSo : UserControl
    {
        public event ButtonClickEventHandler ChonButtonXem;
        public event ButtonClickEventHandler ChonButtonSua;
        public event ButtonClickEventHandler ChonButtonXoa;
        public delegate void ButtonClickEventHandler(string tenHS);
        bool click = true;
        public ucHienHoSo()
        {
            InitializeComponent();
        }

        private void lblTenHoSo_Click(object sender, EventArgs e)
        {
            ChonButtonXem?.Invoke(lblTenHoSo.Text);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ChonButtonXoa?.Invoke(lblTenHoSo.Text);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ChonButtonSua?.Invoke(lblTenHoSo.Text);
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

        private void ucHienHoSo_Load(object sender, EventArgs e)
        {
            plCaiDat.Visible = false;
        }
    }
}
