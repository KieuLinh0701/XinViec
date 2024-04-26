using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FGDNguoiXinViec : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString();
        DAO dao = new DAO();
        Button oldBtn = new Button(); 
        FThongBao_LuaChon tb = new FThongBao_LuaChon();
        public FGDNguoiXinViec()
        {
            InitializeComponent();
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.btnThongTinCaNhan, "Trang cá nhân");
            toolTip1.SetToolTip(this.btnTroChuyen, "Trò chuyện");
            toolTip1.SetToolTip(this.btnDangXuat, "Đăng xuất");
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
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnHoSoXinViec, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new FHoSoXinViec(), plFormCha);
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnCaiDat, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new FCaiDat(), plFormCha);
        }

        private void btnLichSuCongViec_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnLichSuCongViec, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new LichSuCongViec(), plFormCha);
        }
        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnThongTinCaNhan, oldBtn, Color.Teal, Color.Gainsboro);
            FTrangCaNhanNguoiDung tcn = new FTrangCaNhanNguoiDung(EmailDangNhap);
            tcn.btnQuayLai.Visible = false;
            tcn.btnLuuYeuThich.Visible = false;
            tcn.lblTenUV.Location = new Point(165, 120);
            dao.MoFormCon(tcn, plFormCha);
        }

        private void btnTroChuyen_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnTroChuyen, oldBtn, Color.Teal, Color.Gainsboro);
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
            btnLichHenPhongVan_Click(sender, e);
        }

        private void btnTimViecLam_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnTimViecLam, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new TimViecLam(), plFormCha);
        }

        private void btnThongKeCongViec_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnThongKeCongViec, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new ThongKeCongViec(), plFormCha);
        }

        private void DangXuat(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        { 
            tb = new FThongBao_LuaChon();
            tb.rtbThongBao.Text = "Bạn muốn đăng xuất?";
            tb.Show();
            tb.ChonButtonCo += DangXuat;
        }

        private void btnViecLamYeuThich_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnViecLamYeuThich, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new ViecLamYeuThich(), plFormCha);
        }

        private void btnLichHenPhongVan_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnLichHenPhongVan, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new FLichPhongVan(), plFormCha);
        }

        private void btnTimCongTy_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnTimCongTy, oldBtn, Color.Teal, Color.Gainsboro);
            dao.MoFormCon(new FTimCongTy(), plFormCha);
        }
    }
}
