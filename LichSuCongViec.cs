using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using XinViec.Resources;
using XinViec.XinViec;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static XinViec.ucLichSuCongViec;

namespace XinViec
{
    public partial class LichSuCongViec : Form
    {
        string sqlStr, EmailCongTy, TieuDe;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        int t = 0;
        bool hienThi = true;
        int value = 0;
        DAO dao = new DAO();
        public LichSuCongViec()
        {
            InitializeComponent();
            Load_cbbTinhChatLoc();
        }


        private void Load_cbbTinhChatLoc()
        {
            cbbTinhChatLoc.Items.Clear();
            cbbDanhSachLoc.Items.Clear();
            cbbTinhChatLoc.Items.Add("Tất cả");
            cbbTinhChatLoc.Items.Add("Công ty");
            cbbTinhChatLoc.Items.Add("Vị trí tuyển dụng");
            cbbTinhChatLoc.Items.Add("Hồ sơ");
            cbbTinhChatLoc.Text = cbbTinhChatLoc.Items[0].ToString();
        }
        private void XemCongViec(string EmailCongTy, string TieuDe)
        {
            FThongTinTuyenDung tttd = new FThongTinTuyenDung(EmailCongTy, TieuDe);
            tttd.DongForm += QuayLai;
            dao.MoFormCon(tttd, plFormCha);
        }

        private void QuayLai(object sender, EventArgs e)
        {
            dao.MoFormCon(new LichSuCongViec(), plFormCha);
        }

        private void XemHoSo(string tenHoSo)
        {
            XemHoSo xem = new XemHoSo(EmailDangNhap, tenHoSo);
            xem.QuayLai += QuayLai;
            dao.MoFormCon(xem, plFormCha);
        }

        private bool TrangThaiUngTuyen()
        {
            bool ungTuyen = false;
            string sqlStr = "SELECT COUNT(*) FROM DangTinTuyenDung WHERE GETDATE() <= NgayHetHan AND Email = @Email AND TieuDe = @TieuDe";

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@Email", EmailCongTy);
                        command.Parameters.AddWithValue("@TieuDe", TieuDe);

                        int count = (int)command.ExecuteScalar();
                        ungTuyen = (count > 0);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }

            return ungTuyen;
        }

        private void RutHS(object sender, EventArgs e)
        {
            dao.tb.Close();
            if (TrangThaiUngTuyen() == false)
            {
                dao.ThongBao("Rút hồ sơ thất bại vì đã qua thời gian điều chỉnh");
            }
            else
            {
                sqlStr = String.Format("DELETE FROM UngTuyen WHERE EmailUngVien = @EmailUngVien AND TieuDe = @TieuDe AND EmailCongTy = @EmailCongTy");
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@TieuDe", TieuDe);
                            command.Parameters.AddWithValue("@EmailUngVien ", StateStorage.GetInstance().SharedValue);
                            command.Parameters.AddWithValue("@EmailCongTy", EmailCongTy);
                            int k = command.ExecuteNonQuery();
                            if (k > 0)
                            {
                                dao.ThongBao("Rút hồ sơ thành công");
                                plChuaHoSo.Controls.Clear();
                                if (t == 0)
                                {
                                    rbtnChoDuyet.Checked = true;
                                    rbtnChoDuyet_CheckedChanged(this, e);
                                }
                                else if (t == 1)
                                {
                                    rbtnDatYeuCau.Checked = true;
                                    rbtnDatYeuCau_CheckedChanged(this, e);
                                }
                                else
                                {
                                    rbtnTuChoi.Checked = true;
                                    rbtnTuChoi_CheckedChanged(this, e);
                                }
                            }
                            else
                            {
                                dao.ThongBao("Rút hồ sơ thất bại");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    dao.ThongBao("Rút hồ sơ thất bại: " + ex);
                }
            }
        }

        private void RutHoSo(string EmailCongTy, string TieuDe)
        {
            dao.ThongBao_LuaChon("Bạn muốn rút hồ sơ?", RutHS);
            this.EmailCongTy = EmailCongTy;
            this.TieuDe = TieuDe;
        }

        private void PhanHoi(string EmailCongTy, string TieuDe)
        {
            sqlStr = String.Format("SELECT COUNT(*) FROM PhanHoi WHERE EmailUngVien = @EmailDangNhap AND TieuDe = @TieuDe AND EmailCongTy = @EmailCongTy");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                        command.Parameters.AddWithValue("@TieuDe", TieuDe);
                        command.Parameters.AddWithValue("@EmailCongTy", EmailCongTy);

                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count > 0)
                        {
                            dao.ThongBao("Bạn đã gửi góp ý về công việc này");
                        }
                        else
                        {
                            dao.MoFormCon(new FDanhGia(EmailCongTy, EmailDangNhap, TieuDe), plFormCha);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.ThongBao("Lỗi lấy dữ liệu từ cơ sở dữ liệu: " + ex);
            }
            
        }

        private bool KiemTraPhanHoi(string emailCongTy, string tieuDe)
        {
            bool check = false;
            sqlStr = @"SELECT COUNT(*) FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a 
                INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe 
                INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email 
                WHERE TinhTrangHS = N'Đáp ứng' AND a.TieuDe = @TieuDe AND a.EmailCongTy = @EmailCongTy";

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                        command.Parameters.AddWithValue("@TieuDe", tieuDe);
                        command.Parameters.AddWithValue("@EmailCongTy", emailCongTy);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        check = (count > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                dao.ThongBao("Lỗi lấy dữ liệu từ cơ sở dữ liệu: " + ex);
            }

            return check;
        }

        private void XemCongTy(string EmailCongTy)
        {
            FCongTy ct = new FCongTy(EmailCongTy);
            ct.DongForm += QuayLai;
            dao.MoFormCon(ct, plFormCha);
        }

        private void HienThiLS(string tenCongTy, string emailCongTy, string viTriUngTuyen,
            string tieuDe, string ngayUngTuyen, string tenHoSo)
        {
            ucLichSuCongViec uc = new ucLichSuCongViec();
            uc.ChonButtonXemCongViec += XemCongViec;
            uc.ChonButtonXemHoSo += XemHoSo;
            uc.ChonButtonRutHoSo += RutHoSo;
            uc.ChonButtonPhanHoi += PhanHoi;
            uc.ChonButtonXemCongTy += XemCongTy;
            if (KiemTraPhanHoi(emailCongTy, tieuDe) == true)
            {
                uc.btnPhanHoi.Enabled = true;
            }
            else
            {
                uc.btnPhanHoi.Enabled = false;
            }
            uc.lblTenCongTy.Text = tenCongTy;
            uc.txbEmailCongTy.Text = emailCongTy;
            uc.lblViTriUngTuyen.Text = viTriUngTuyen.ToUpper();
            uc.txbTieuDe.Text = tieuDe;
            uc.lblNgayUngTuyen.Text = ngayUngTuyen;
            uc.lblTenHoSo.Text = tenHoSo;
            if (hienThi == true)
            {
                uc.Location = new Point(40, value);
                hienThi = false;
            }
            else
            {
                uc.Location = new Point(460, value);
                value += 110;
                hienThi = true;
            }
            plChuaHoSo.Controls.Add(uc);
            uc.Show();
        }

        private void LichSuCongViec_Load(object sender, EventArgs e)
        {
            Load_cbbTinhChatLoc();
            rbtnChoDuyet_CheckedChanged(this, e);
            rbtnChoDuyet.Checked = true;
        }

        private void DayDataVaoUC(string query)
        {
            value = 0;
            hienThi = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tenCongTy = reader["Ten"].ToString();
                                string emailCongTy = reader["EmailCongTy"].ToString();
                                string viTriUngTuyen = reader["TenCongViec"].ToString();
                                string tieuDe = reader["TieuDe"].ToString();
                                string ngayUngTuyen = reader["NgayUngTuyen"].ToString(); 
                                string tenHoSo = reader["TenHoSo"].ToString();
                                HienThiLS(tenCongTy, emailCongTy, viTriUngTuyen, tieuDe, ngayUngTuyen, tenHoSo);
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

        private void cbbTinhChatLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_cbbDanhSachLoc();
        }

        private void Load_cbbDanhSachLoc()
        {
            cbbDanhSachLoc.Items.Clear();
            if (cbbTinhChatLoc.Text == "Tất cả")
            {
                cbbDanhSachLoc.Items.Add("Danh sách lọc trống");
            }
            else if (cbbTinhChatLoc.Text == "Công ty")
            {
                if (t == 0)
                {
                    sqlStr = String.Format("SELECT DISTINCT tt.Ten FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email WHERE TrangThaiDuyet = N'Chưa duyệt'");
                }
                else if (t == 1)
                {
                    sqlStr = String.Format("SELECT DISTINCT tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Đáp ứng'");
                }
                else
                {
                    sqlStr = String.Format("SELECT DISTINCT tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Không đáp ứng'");
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cbbDanhSachLoc.Items.Add(reader["Ten"].ToString());
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
            else if (cbbTinhChatLoc.Text == "Vị trí tuyển dụng")
            {
                if (t == 0)
                {
                    sqlStr = String.Format("SELECT DISTINCT td.TenCongViec FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email WHERE TrangThaiDuyet = N'Chưa duyệt'");
                }
                else if (t == 1)
                {
                    sqlStr = String.Format("SELECT DISTINCT td.TenCongViec " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Đáp ứng'");
                }
                else
                {
                    sqlStr = String.Format("SELECT DISTINCT td.TenCongViec " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Không đáp ứng'");
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cbbDanhSachLoc.Items.Add(reader["TenCongViec"].ToString());
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
            else
            {
                if (t == 0)
                {
                    sqlStr = String.Format("SELECT DISTINCT a.TenHoSo FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email WHERE TrangThaiDuyet = N'Chưa duyệt'");
                }
                else if (t == 1)
                {
                    sqlStr = String.Format("SELECT DISTINCT a.TenHoSo " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Đáp ứng'");
                }
                else
                {
                    sqlStr = String.Format("SELECT DISTINCT a.TenHoSo " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Không đáp ứng'");
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    cbbDanhSachLoc.Items.Add(reader["TenHoSo"].ToString());
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
            cbbDanhSachLoc.Text = cbbDanhSachLoc.Items[0].ToString();
        }

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            Load_cbbTinhChatLoc();
            if (t == 0)
            {
                rbtnChoDuyet_CheckedChanged(sender, e);
            }
            else if (t == 1)
            {
                rbtnDatYeuCau_CheckedChanged(sender, e);
            }
            else
            {
                rbtnTuChoi_CheckedChanged(sender, e);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            hienThi = true;
            value = 0;
            plChuaHoSo.Controls.Clear();
            if (cbbTinhChatLoc.Text == "Tất cả")
            {
                if (t==0)
                {
                    rbtnChoDuyet_CheckedChanged(sender, e);
                }
                else if (t==1)
                {
                    rbtnDatYeuCau_CheckedChanged(sender, e);
                }
                else
                {
                    rbtnTuChoi_CheckedChanged(sender, e);
                }
            }
            else if (cbbTinhChatLoc.Text == "Công ty")
            {
                if (t == 0)
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email WHERE TrangThaiDuyet = N'Chưa duyệt' AND tt.Ten = @Ten");
                }
                else if (t == 1)
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Đáp ứng' AND tt.Ten = @Ten");
                }
                else
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Không đáp ứng' AND tt.Ten = @Ten");
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            command.Parameters.AddWithValue("@Ten", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string tenCongTy = reader["Ten"].ToString();
                                    string emailCongTy = reader["EmailCongTy"].ToString();
                                    string viTriUngTuyen = reader["TenCongViec"].ToString();
                                    string tieuDe = reader["TieuDe"].ToString();
                                    DateTime ngayUngTuyen = reader.GetDateTime(reader.GetOrdinal("NgayUngTuyen"));
                                    string ngayUngTuyenText = ngayUngTuyen.ToString("dd/MM/yyyy hh:mm:ss");
                                    string tenHoSo = reader["TenHoSo"].ToString();
                                    HienThiLS(tenCongTy, emailCongTy, viTriUngTuyen, tieuDe, ngayUngTuyenText, tenHoSo);
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
            else if (cbbTinhChatLoc.Text == "Vị trí tuyển dụng")
            {
                if (t == 0)
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email WHERE TrangThaiDuyet = N'Chưa duyệt' AND td.TenCongViec = @TenCongViec");
                }
                else if (t == 1)
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Đáp ứng' AND td.TenCongViec = @TenCongViec");
                }
                else
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Không đáp ứng' AND td.TenCongViec = @TenCongViec");
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            command.Parameters.AddWithValue("@TenCongViec", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string tenCongTy = reader["Ten"].ToString();
                                    string emailCongTy = reader["EmailCongTy"].ToString();
                                    string viTriUngTuyen = reader["TenCongViec"].ToString();
                                    string tieuDe = reader["TieuDe"].ToString();
                                    DateTime ngayUngTuyen = reader.GetDateTime(reader.GetOrdinal("NgayUngTuyen"));
                                    string ngayUngTuyenText = ngayUngTuyen.ToString("dd/MM/yyyy hh:mm:ss");
                                    string tenHoSo = reader["TenHoSo"].ToString();
                                    HienThiLS(tenCongTy, emailCongTy, viTriUngTuyen, tieuDe, ngayUngTuyenText, tenHoSo);
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
            else
            {
                if (t == 0)
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email WHERE TrangThaiDuyet = N'Chưa duyệt' AND a.TenHoSo = @TenHoSo");
                }
                else if (t == 1)
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Đáp ứng' AND a.TenHoSo = @TenHoSo");
                }
                else
                {
                    sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Không đáp ứng' AND a.TenHoSo = @TenHoSo");
                }
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            command.Parameters.AddWithValue("@TenHoSo", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string tenCongTy = reader["Ten"].ToString();
                                    string emailCongTy = reader["EmailCongTy"].ToString();
                                    string viTriUngTuyen = reader["TenCongViec"].ToString();
                                    string tieuDe = reader["TieuDe"].ToString();
                                    DateTime ngayUngTuyen = reader.GetDateTime(reader.GetOrdinal("NgayUngTuyen"));
                                    string ngayUngTuyenText = ngayUngTuyen.ToString("dd/MM/yyyy hh:mm:ss");
                                    string tenHoSo = reader["TenHoSo"].ToString();
                                    HienThiLS(tenCongTy, emailCongTy, viTriUngTuyen, tieuDe, ngayUngTuyenText, tenHoSo);
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
        }

        private void rbtnDatYeuCau_CheckedChanged(object sender, EventArgs e)
        {
            t = 1;
            Load_cbbTinhChatLoc();
            plChuaHoSo.Controls.Clear();
            sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Dap ung'");
            DayDataVaoUC(sqlStr);
        }

        private void rbtnTuChoi_CheckedChanged(object sender, EventArgs e)
        {
            t = 2;
            Load_cbbTinhChatLoc();
            plChuaHoSo.Controls.Clear();
            sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten " +
                "FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a " +
                "INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe " +
                "INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email " +
                "WHERE TinhTrangHS = N'Khong dap ung'");
            DayDataVaoUC(sqlStr);
        }

        private void rbtnChoDuyet_CheckedChanged(object sender, EventArgs e)
        {
            t = 0;
            Load_cbbTinhChatLoc();
            plChuaHoSo.Controls.Clear();
            sqlStr = String.Format("SELECT a.*, td.TenCongViec, tt.Ten FROM (SELECT * FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap) AS a INNER JOIN DangTinTuyenDung td ON a.EmailCongTy = td.Email AND a.TieuDe = td.TieuDe INNER JOIN ThongTinCty tt ON a.EmailCongTy = tt.Email WHERE TrangThaiDuyet = N'Chua duyet'");
            DayDataVaoUC(sqlStr);
        }
    }
}
