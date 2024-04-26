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
    public partial class FThongBao : Form
    {
        DAO dao = new DAO();
        public FThongBao()
        {
            InitializeComponent();
        }

        private void FThongBao_Load(object sender, EventArgs e)
        {
            dao.ApplyCenterAlignment(rtbThongBao);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
