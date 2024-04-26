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
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class GDNguoiXinViec : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString();
        DAO dao = new DAO();
        ThongBao_LuaChon tb = new ThongBao_LuaChon();
        public GDNguoiXinViec()
        {
            InitializeComponent();
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.btnThongTinCaNhan, "Trang cá nhân");
            toolTip1.SetToolTip(this.btnTroChuyen, "Trò chuyện");
            toolTip1.SetToolTip(this.btnDangXuat, "Đăng xuất");
        }

        private void DoiMauButtonDuocChon(Button btn)
        {
            btnLichSuCongViec.BackColor = Color.Teal;
            btnTimViecLam.BackColor = Color.Teal;
            btnViecLamYeuThich.BackColor = Color.Teal;
            btnThongTinCaNhan.BackColor = Color.Teal;
            btnTroChuyen.BackColor = Color.Teal;
            btnDangXuat.BackColor = Color.Teal;
            btnHoSoXinViec.BackColor = Color.Teal;
            btnThongKeCongViec.BackColor= Color.Teal;
            btnCaiDat.BackColor = Color.Teal;
            btn.BackColor = Color.Gainsboro;
        }

        private void LayThongTin(string sqlStr)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        btnThongTinCaNhan.Text = reader["hoTen"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnHoSoXinViec_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnHoSoXinViec);
            dao.MoFormCon(new HoSoXinViec(), plFormCha);
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnCaiDat);
            dao.MoFormCon(new CaiDat(), plFormCha);
        }

        private void btnLichSuCongViec_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnLichSuCongViec);
            dao.MoFormCon(new LichSuCongViec(), plFormCha);
        }
        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnThongTinCaNhan);
            TrangCaNhanNguoiDung tcn = new TrangCaNhanNguoiDung(EmailDangNhap);
            tcn.btnLuuYeuThich.Visible = false;
            tcn.lblTenUV.Location = new Point(165, 120);
            dao.MoFormCon(tcn, plFormCha);
        }

        private void btnTroChuyen_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnTroChuyen);
            dao.MoFormCon(new TroChuyen(), plFormCha);
        }

        private void Load_TenNguoiDung()
        {
            sqlStr = string.Format("SELECT * FROM UngVien WHERE EmailDangNhap = @EmailDangNhap");
            LayThongTin(sqlStr);
        }

        private void GDNguoiXinViec_Load(object sender, EventArgs e)
        {
            Load_TenNguoiDung();
            btnTimViecLam_Click(sender, e);
        }

        private void btnTimViecLam_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnTimViecLam);
            dao.MoFormCon(new TimViecLam(), plFormCha);
        }

        private void btnThongKeCongViec_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnThongKeCongViec);
            dao.MoFormCon(new ThongKeCongViec(), plFormCha);
        }

        private void DangXuat(object sender, EventArgs e)
        {
            this.Close();
            FDangNhap dn = new FDangNhap();
            dn.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        { 
            tb = new ThongBao_LuaChon();
            tb.rtbThongBao.Text = "Bạn muốn đăng xuất?";
            tb.Show();
            tb.ChonButtonCo += DangXuat;
        }

        private void btnViecLamYeuThich_Click(object sender, EventArgs e)
        {
            DoiMauButtonDuocChon(btnViecLamYeuThich);
            dao.MoFormCon(new ViecLamYeuThich(), plFormCha);
        }
    }
}
