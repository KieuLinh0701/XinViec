using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.XinViec;
using static System.Net.Mime.MediaTypeNames;

namespace XinViec
{
    public partial class FCaiDatEmail : Form
    {
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString();
        string vaiTro;
        DAO dao = new DAO();
        public FCaiDatEmail(string vaiTro)
        {
            InitializeComponent();
            this.vaiTro = vaiTro;
        }

        private void btnDoiTenTK_Click(object sender, EventArgs e)
        {
            dao.MoFormCon(new FDoiEmailDangNhap(vaiTro), plFormCha);
        }

        private void CaiDatEmail_Load(object sender, EventArgs e)
        {
            txbEmailDangNhap.Text = EmailDangNhap;
        }
    }
}
