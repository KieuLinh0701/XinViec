using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec.Resources
{
    public partial class BaoLoi : Form
    {
        DAO dao = new DAO();
        public BaoLoi()
        {
            InitializeComponent();
        }

        private void BaoLoi_Load(object sender, EventArgs e)
        {
            dao.ApplyCenterAlignment(rtbThongBao);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
