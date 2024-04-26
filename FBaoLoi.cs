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
    public partial class FBaoLoi : Form
    {
        DAO dao = new DAO();
        public FBaoLoi()
        {
            InitializeComponent();
        }

        private void BaoLoi_Load(object sender, EventArgs e)
        {
            dao.ApplyCenterAlignment(rtbThongBao);
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
