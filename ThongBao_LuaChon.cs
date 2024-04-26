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
    public partial class ThongBao_LuaChon : Form
    {
        public event EventHandler ChonButtonCo;
        DAO dao = new DAO();
        public ThongBao_LuaChon()
        {
            InitializeComponent();
        }

        private void ThongBao_Load(object sender, EventArgs e)
        {
            dao.ApplyCenterAlignment(rtbThongBao);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCo_Click(object sender, EventArgs e)
        {
            ChonButtonCo?.Invoke(this, e);
            this.Close();
        }
    }
}
