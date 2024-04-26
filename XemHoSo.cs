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
    public partial class XemHoSo : Form
    {
        string EmailDangNhap;
        string tenHoSo;
        DAO dao = new DAO();
        public event EventHandler QuayLai;
        public XemHoSo(string EmailDangNhap, string tenHoSo)
        {
            InitializeComponent();
            this.EmailDangNhap = EmailDangNhap;
            this.tenHoSo = tenHoSo;
        }

        private void XemHoSo_Load(object sender, EventArgs e)
        {
            HoSo hs = new HoSo(EmailDangNhap, tenHoSo);
            hs.LoadHS();
            Panel pl = hs.GetChildPanel();
            pl.Dock = DockStyle.Fill;
            plChuaHoSo.Controls.Clear();
            plChuaHoSo.Controls.Add(pl);
            pl.Show();
        }
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayLai?.Invoke(this, e);
        }
    }
}

