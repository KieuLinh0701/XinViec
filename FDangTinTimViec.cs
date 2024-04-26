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

namespace XinViec
{
    public partial class FDangTinTimViec : Form
    {
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString();
        string sqlStr;
        int action;
        string ngayDang, tenHoSo;
        DAO dao = new DAO();
        public event EventHandler DangThanhCong;
        DangTinTimViecDAO dangTinTimViecDAO = new DangTinTimViecDAO();
        public FDangTinTimViec()
        {
            InitializeComponent();
            action = 1;
        }
        public FDangTinTimViec(string ngayDang, string tenHoSo)
        {
            InitializeComponent();
            action = 2;
            btnDangTin.Image = Properties.Resources.save;
            btnDangTin.Text = string.Empty;
            this.tenHoSo = tenHoSo;
            this.ngayDang = ngayDang;
        }

        private void DaDang(object sender, EventArgs e)
        {
            this.Close();
            DangThanhCong?.Invoke(sender, e);
        }

        private void btnDangTin_Click(object sender, EventArgs e)
        {
            if (action == 1)
            {
                FChonHoSoUngTuyen f = new FChonHoSoUngTuyen(EmailDangNhap, txbNoiDung.Text, 1);
                f.DaDang += DaDang;
                f.ShowDialog();
            }
            else if (action == 2)
            {
                if (dangTinTimViecDAO.ChinhSuaTinTimViec(txbNoiDung.Text, ngayDang, EmailDangNhap, tenHoSo) > 0)
                {
                    FChonHoSoUngTuyen f = new FChonHoSoUngTuyen(EmailDangNhap, txbNoiDung.Text, ngayDang);
                    f.DaDang += DaDang;
                    f.ShowDialog();
                }
            }
        }

        private void FDangTinTimViec_Load(object sender, EventArgs e)
        {
            if (action == 2)
            {
                dangTinTimViecDAO.Load_ThongTinDaDang(EmailDangNhap, ngayDang, txbNoiDung);
            }
        }
    }
}
