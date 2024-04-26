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
    public partial class ThongBao : Form
    {
        public ThongBao()
        {
            InitializeComponent();
        }
        private void ApplyCenterAlignment()
        {
            rtbThongBao.SelectAll();
            rtbThongBao.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void ThongBao_Load(object sender, EventArgs e)
        {
            ApplyCenterAlignment();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
