using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace XinViec
{
    public partial class TaiKhoan : Form
    {
        public TaiKhoan()
        {
            InitializeComponent();
        }

        private void btnDoiTenTK_Click(object sender, EventArgs e)
        {
            DoiTenTK tenTK = new DoiTenTK();
            tenTK.TopLevel = false;
            plFormCha.Controls.Clear();
            plFormCha.Controls.Add(tenTK);
            tenTK.Dock = DockStyle.Fill;
            tenTK.Show();
        }
    }
}
