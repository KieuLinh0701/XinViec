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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class FTimCongTy : Form
    {
        ucTimCongTy uc;
        private bool hienThi = true;
        int value = 0;
        String sqlStr;
        DAO dao = new DAO();
        public FTimCongTy()
        {
            InitializeComponent();
        }

        private void DongForm_Load(object sender, EventArgs e)
        {
            dao.MoFormCon(new FTimCongTy(), plFormCha);
        }

        private void XemCongTy(string EmailCongty)
        {
            FCongTy ct = new FCongTy(EmailCongty);
            ct.DongForm += DongForm_Load;
            dao.MoFormCon(ct, plFormCha);
        }

        private void HienThiCTy(string Email, string Ten, string khuVuc, string soLuong)
        {
            uc = new ucTimCongTy();
            uc.ClickTenCTy += XemCongTy;
            uc.lblTenCongTy.Text = Ten.ToUpper();
            uc.txbEmailCongTy.Text = Email;
            if (rdbtnCongTyCheDoTot.Checked == true)
            {
                uc.lblMieuTa.Text = "Số lượng ứng viên thêm yêu thích:";
                uc.lblSoNhanVienTuyen.Location = new Point(278, 57);
            }
            uc.lblKhuVuc.Text = khuVuc;
            uc.lblSoNhanVienTuyen.Text = soLuong;
            uc.txbEmailCongTy.Visible = false;
            pLChuaCongTy.Controls.Add(uc);
            if (hienThi == true)
            {
                uc.Location = new Point(45, value);
                hienThi = false;
            }
            else
            {
                uc.Location = new Point(460, value);
                value += 100;
                hienThi |= true;
            }
            uc.Show();
        }

        private void rbtnCTyTuyenNhieuNhat_CheckedChanged(object sender, EventArgs e)
        {
            value = 0;
            hienThi = true;
            pLChuaCongTy.Controls.Clear();
            sqlStr = string.Format("SELECT tt.Email, tt.Ten, tt.DiaChi, a.TongTuyen " +
                "FROM (SELECT Email, SUM(SoLuongTuyen) AS TongTuyen " +
                "FROM DangTinTuyenDung " +
                "WHERE YEAR(NgayDang) = YEAR(GETDATE()) " +
                "GROUP BY Email) AS a " +
                "INNER JOIN ThongTinCTy tt " +
                "ON a.Email = tt.Email " +
                "ORDER BY a.TongTuyen DESC");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Email = reader["Email"].ToString();
                                string Ten = reader["Ten"].ToString();
                                string DiaChi = reader["DiaChi"].ToString();
                                string soLuong = reader["TongTuyen"].ToString();
                                if (!string.IsNullOrEmpty(DiaChi))
                                {
                                    string[] diaChiParts = DiaChi.Split(',');
                                    if (diaChiParts.Length >= 4)
                                    {
                                        HienThiCTy(Email, Ten, diaChiParts[3], soLuong);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void rdbtnCongTyCheDoTot_CheckedChanged(object sender, EventArgs e)
        {
            value = 0;
            hienThi = true;
            pLChuaCongTy.Controls.Clear();
            sqlStr = string.Format("SELECT tt.Email, tt.Ten, tt.DiaChi, a.SoLuongLuu " +
                "FROM (SELECT COUNT(EmailCongTy) AS SoLuongLuu, EmailCongTy " +
                "FROM YeuThich " +
                "GROUP BY EmailCongTy) AS a " +
                "INNER JOIN ThongTinCTy tt " +
                "ON a.EmailCongTy = tt.Email " +
                "ORDER BY a.SoLuongLuu DESC");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Email = reader["Email"].ToString();
                                string Ten = reader["Ten"].ToString();
                                string DiaChi = reader["DiaChi"].ToString();
                                string soLuong = reader["SoLuongLuu"].ToString();
                                if (!string.IsNullOrEmpty(DiaChi))
                                {
                                    string[] diaChiParts = DiaChi.Split(',');
                                    if (diaChiParts.Length >= 4)
                                    {
                                        HienThiCTy(Email, Ten, diaChiParts[3], soLuong);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void FTimCongTy_Load(object sender, EventArgs e)
        {
            rbtnCTyTuyenNhieuNhat_CheckedChanged(sender, e);
            rbtnCTyTuyenNhieuNhat.Checked = true;
        }
    }
}
