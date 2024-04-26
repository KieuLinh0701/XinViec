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
    public partial class ChonHoSoUngTuyen : Form
    {
        public event EventHandler DaLuu;
        public event EventHandler DaDang;
        private UngTuyen ut;
        int action;
        string sqlStr, Email, tieuDe, tenHS;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        public ChonHoSoUngTuyen(String Email, string tieuDe, int action)
        {
            InitializeComponent();
            this.Email = Email;
            this.tieuDe = tieuDe;
            this.action = action;
            Load_cbbTinhChatLoc();
        }

        private void Load_cbbTinhChatLoc()
        {
            cbbTinhChatLoc.Items.Clear();
            cbbDanhSachLoc.Items.Clear();
            cbbTinhChatLoc.Items.Add("Tất cả");
            cbbTinhChatLoc.Items.Add("Tên hồ sơ");
            cbbTinhChatLoc.Items.Add("Vị trí tuyển dụng");
            cbbTinhChatLoc.Text = cbbTinhChatLoc.Items[0].ToString();
        }

        private void DongForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UngTuyenDatabase(object sender, EventArgs e)
        {
            ut.Close();
            sqlStr = "INSERT INTO UngTuyen (EmailUngVien, TenHoSo, EmailCongTy, TieuDe, NgayUngTuyen, TrangThaiDuyet) " +
                "VALUES (@EmailUngVien, @TenHoSo, @EmailCongTy, @TieuDe, @NgayUngTuyen, @TrangThaiDuyet)";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailUngVien", EmailDangNhap);
                        command.Parameters.AddWithValue("@TenHoSo", tenHS);
                        command.Parameters.AddWithValue("@EmailCongTy", Email);
                        command.Parameters.AddWithValue("@TieuDe", tieuDe);
                        command.Parameters.AddWithValue("@NgayUngTuyen", DateTime.Now);
                        string trangThai = "Chưa duyệt";
                        command.Parameters.AddWithValue("@TrangThaiDuyet", trangThai);
                        // Execute the INSERT query
                        int k = command.ExecuteNonQuery();

                        // Check if rows were affected
                        if (k > 0)
                        {
                            this.Close();
                            dao.ThongBao("Ứng tuyển thành công");
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Ứng tuyển thất bại: " + ex.Message);
            }
        }

        private void UngTuyenCV(string tenHS)
        {
            this.tenHS = tenHS;
            ut = new UngTuyen();
            sqlStr = "SELECT Ten, TenCongViec FROM ThongTinCTy tt INNER JOIN DangTinTuyenDung dt ON tt.Email = dt.Email  WHERE tt.Email = @Email AND TieuDe = @tieuDe";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@TieuDe", tieuDe);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ut.rtbTenCongTy.Text = reader["Ten"].ToString().ToUpper() + "\n\n Công việc: " + reader["TenCongViec"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            ut.ChonButtonXacNhan += UngTuyenDatabase;
            ut.Show();
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
            else if (cbbTinhChatLoc.Text == "Vị trí tuyển dụng")
            {
                sqlStr = "SELECT DISTINCT viTriUngTuyen FROM HoSoXinViec WHERE EmailDangNhap = @EmailDangNhap";
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
                                    cbbDanhSachLoc.Items.Add(reader["viTriUngTuyen"].ToString());
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
                sqlStr = "SELECT tenHoSo FROM HoSoXinViec WHERE EmailDangNhap = @EmailDangNhap ORDER BY ngayCapNhat ASC";
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
                                    cbbDanhSachLoc.Items.Add(reader["tenHoSo"].ToString());
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
            if (cbbDanhSachLoc.Items.Count > 0)
            {
                cbbDanhSachLoc.Text = cbbDanhSachLoc.Items[0].ToString();
            }
        }

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            dao.MoFormCon(new ChonHoSoUngTuyen(Email, tieuDe, action), plFormCha);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            plChuaHoSo.Controls.Clear();
            if (cbbTinhChatLoc.Text == "Tất cả")
            {
                ChonHoSoUngTuyen_Load(sender, e);
            }
            else if (cbbTinhChatLoc.Text == "Vị trí tuyển dụng")
            {
                sqlStr = "SELECT tenHoSo, viTriUngTuyen, ngayCapNhat FROM HoSoXinViec WHERE EmailDangNhap = @EmailDangNhap AND viTriUngTuyen = @viTriUngTuyen ORDER BY ngayCapNhat ASC";
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            command.Parameters.AddWithValue("@viTriUngTuyen", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string tenHoSo = reader["tenHoSo"].ToString();
                                    string viTriUngTuyen = reader["viTriUngTuyen"].ToString();
                                    HienThiHS(tenHoSo, viTriUngTuyen);
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
                sqlStr = "SELECT tenHoSo, viTriUngTuyen FROM HoSoXinViec WHERE EmailDangNhap = @EmailDangNhap AND tenHoSo = @tenHoSo ORDER BY ngayCapNhat ASC";
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            command.Parameters.AddWithValue("@tenHoSo", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string tenHoSo = reader["tenHoSo"].ToString();
                                    string viTriUngTuyen = reader["viTriUngTuyen"].ToString();
                                    HienThiHS(tenHoSo, viTriUngTuyen);
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

        private void DangTinTimViec(string tenHS)
        {
            sqlStr = String.Format("INSERT INTO DangTinTimViec (EmailUngVien, NoiDung, NgayDang, TenHoSo) VALUES (@EmailUngVien, @NoiDung, @NgayDang, @TenHoSo)");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailUngVien", EmailDangNhap);
                        command.Parameters.AddWithValue("@TenHoSo", tenHS);
                        command.Parameters.AddWithValue("@NgayDang", DateTime.Now.ToString("dd/MM/yyyy" + " lúc " + "HH:mm:ss"));
                        command.Parameters.AddWithValue("@NoiDung", tieuDe);

                        int k = command.ExecuteNonQuery();

                        if (k > 0)
                        {
                            dao.ThongBao("Đăng tin thành công");
                            this.Close();
                            DaDang?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Đăng thất bại: " + ex.Message);
            }
        }

        private void XemHoSo(String tenHS)
        {
            HoSo hs = new HoSo(EmailDangNhap, tenHS);
            hs.ShowDialog();
        }

        private void HienThiHS(string tenHoSo, string viTriUngTuyen)
        {
            ucChonHoSoUngTuyen uc = new ucChonHoSoUngTuyen();
            uc.ChonButtonXem += XemHoSo;
            if (action == 1)
            {
                uc.ChonButtonUngTuyen += DangTinTimViec;
            } 
            else if (action == 2)
            {
                uc.ChonButtonUngTuyen += UngTuyenCV; 
            }
            uc.Dock = DockStyle.Top;
            uc.lblTenHoSo.Text = tenHoSo;
            uc.lblViTriUngTuyen.Text = viTriUngTuyen;
            plChuaHoSo.Controls.Add(uc);
        }

        private void ChonHoSoUngTuyen_Load(object sender, EventArgs e)
        {
            sqlStr = "SELECT tenHoSo, viTriUngTuyen, ngayCapNhat FROM HoSoXinViec WHERE EmailDangNhap = @EmailDangNhap ORDER BY ngayCapNhat ASC";
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
                                string tenHoSo = reader["tenHoSo"].ToString();
                                string viTriUngTuyen = reader["viTriUngTuyen"].ToString();
                                HienThiHS(tenHoSo, viTriUngTuyen);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }
    }
}
