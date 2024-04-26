using FontAwesome.Sharp;
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
using System.Windows.Media;
using XinViec.Resources;
using XinViec.XinViec;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class TimViecLam : Form
    {
        private bool clicklan1 = true, hienThi = true;
        int value = 0;
        String sqlStr;
        ucHienThiCV uc;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        public TimViecLam()
        {
            InitializeComponent();
            Load_cbbTinhChatLoc();
            plThongKe.Visible = false;
        }

        private void DongForm_Load(object sender, EventArgs e)
        {
            dao.MoFormCon(new TimViecLam(), plFormCha);
        }
        private void XemCongTy(string Email)
        {
            FCongTy ct = new FCongTy(Email);
            ct.DongForm += DongForm_Load;
            dao.MoFormCon(ct, plFormCha);
        }

        private void XemCongViec(string Email, string TieuDe)
        {
            FThongTinTuyenDung tttd = new FThongTinTuyenDung(Email, TieuDe);
            tttd.DongForm += DongForm_Load;
            dao.MoFormCon(tttd, plFormCha);
        }

        private void HienThi_BtnYeuThich(string TieuDe, string EmailCongTy)
        {
            sqlStr = string.Format("SELECT COUNT(*) FROM YeuThich WHERE EmailUngVien = @EmailUngVien AND TieuDe = @TieuDe AND EmailCongTy = @EmailCongTy");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@TieuDe", TieuDe);
                        command.Parameters.AddWithValue("@EmailUngVien ", EmailDangNhap);
                        command.Parameters.AddWithValue("@EmailCongTy", EmailCongTy);

                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            uc.btnYeuThich.Image = Properties.Resources.YeuThich;
                        }
                        else
                        {
                            uc.btnYeuThich.Image = Properties.Resources.ThemYeuThich;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                dao.ThongBao("Lỗi lấy dữ liệu từ cơ sở dữ liệu: " + ex);
            }

        }

        private void HienThiCV(string TieuDe, string LoaiHinhCongViec, string Email, 
            string MucLuong, string YC_KinhNghiem, string Ten, string TenCongViec, string diaChi)
        {
            uc = new ucHienThiCV();
            uc.ClickTenCTy += XemCongTy;
            uc.ClickTieuDe += XemCongViec;
            uc.lblTieuDe.Text = TieuDe.ToUpper();
            uc.txbLoaiHinhCongViec.Text = LoaiHinhCongViec;
            uc.txbEmailCongTy.Text = Email;
            uc.txbEmailCongTy.Visible = false;
            uc.txbMucLuong.Text = MucLuong;
            uc.txbKinhNghiem.Text = YC_KinhNghiem;
            uc.lblTenCongTy.Text = Ten.ToUpper();
            uc.txbViTriTuyenDung.Text = TenCongViec;
            uc.txbKhuVuc.Text = diaChi;
            HienThi_BtnYeuThich(TieuDe, Email);
            plChuaHoSo.Controls.Add(uc);
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
        private void ViecLamDangTuyen()
        {
            sqlStr = string.Format("SELECT COUNT(TieuDe) AS ViecLamDangTuyen FROM DangTinTuyenDung WHERE GETDATE() <= NgayHetHan");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStr, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txbViecLamDangTuyen.Text = reader["ViecLamDangTuyen"].ToString();
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

        private void TenUngVien()
        {
            sqlStr = string.Format("SELECT hoTen FROM UngVien WHERE EmailDangNhap = @EmailDangNhap");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStr, connection);
                    command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTenUV.Text = reader["hoTen"].ToString();
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

        private void ptbMoThongKe_Click(object sender, EventArgs e)
        {
            if (clicklan1 == true)
            {
                plThongKe.Visible = true;
                ptbMoThongKe.Image = Properties.Resources.CaiDatMatKhau_Dong;
                clicklan1 = false;
            }
            else
            {
                plThongKe.Visible = false;
                ptbMoThongKe.Image = Properties.Resources.CaiDatMatKhau_Mo;
                clicklan1 = true;
            }
            txbNgayHT.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ViecLamDangTuyen();
            TenUngVien();
        }

        private void TimViecLam_Load(object sender, EventArgs e)
        {
            value = 0;
            hienThi = true;
            plChuaHoSo.Controls.Clear();
            sqlStr = "SELECT TieuDe, TenCongViec, LoaiHinhCongViec, dt.Email, MucLuong, YC_KinhNghiem, Ten, DiaChi " +
                "FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email";
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
                                string TieuDe = reader["TieuDe"].ToString();
                                string LoaiHinhCongViec = reader["LoaiHinhCongViec"].ToString();
                                string Email = reader["Email"].ToString();
                                string MucLuong = reader["MucLuong"].ToString();
                                string YC_KinhNghiem = reader["YC_KinhNghiem"].ToString();
                                string Ten = reader["Ten"].ToString();
                                string tenCongViec = reader["TenCongViec"].ToString();
                                string DiaChi = reader["DiaChi"].ToString();
                                if (!string.IsNullOrEmpty(DiaChi))
                                {
                                    string[] diaChiParts = DiaChi.Split(',');
                                    if (diaChiParts.Length >= 4)
                                    {
                                        HienThiCV(TieuDe, LoaiHinhCongViec, Email, MucLuong, YC_KinhNghiem, Ten, tenCongViec, diaChiParts[3]);
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

        private void Load_cbbTinhChatLoc()
        {
            cbbTinhChatLoc.Items.Clear();
            cbbDanhSachLoc.Items.Clear();
            foreach (string x in DanhSachCbbLoc.listTimViecLam)
            {
                cbbTinhChatLoc.Items.Add(x);
            }
            if (cbbTinhChatLoc.Items.Count > 0)
            {
                cbbTinhChatLoc.Text = cbbTinhChatLoc.Items[0].ToString();
            }
        }

        private void Load_cbbDanhSachLoc()
        {
            cbbDanhSachLoc.Items.Clear();
            if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listTimViecLam[1])
            {
                sqlStr = "SELECT DiaChi FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email";
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                HashSet<string> diaChiUnique = new HashSet<string>();
                                while (reader.Read())
                                {
                                    string DiaChi = reader["DiaChi"].ToString();
                                    if (!string.IsNullOrEmpty(DiaChi))
                                    {
                                        string[] diaChiParts = DiaChi.Split(',');
                                        if (diaChiParts.Length >= 4)
                                        {
                                            string diaChiTrongComboBox = diaChiParts[3].Trim(); // Loại bỏ khoảng trắng ở đầu và cuối chuỗi
                                            if (!diaChiUnique.Contains(diaChiTrongComboBox))
                                            {
                                                cbbDanhSachLoc.Items.Add(diaChiTrongComboBox);
                                                diaChiUnique.Add(diaChiTrongComboBox); // Thêm địa chỉ vào HashSet để đảm bảo không thêm trùng lặp
                                            }
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
            else if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listTimViecLam[2])
            {
                sqlStr = "SELECT DISTINCT TenCongViec FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email";
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
            else if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listTimViecLam[3])
            {
                sqlStr = "SELECT DISTINCT Ten FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email";
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
            else if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listTimViecLam[4])
            {
                cbbDanhSachLoc.Items.Add("Đang tuyển dụng");
                cbbDanhSachLoc.Items.Add("Đã hết hạn");
            }
            if (cbbDanhSachLoc.Items.Count == 0)
            {
                cbbDanhSachLoc.Items.Add("Danh sách lọc trống");
            }

            cbbDanhSachLoc.Text = cbbDanhSachLoc.Items[0].ToString();
        }

        private void cbbTinhChatLoc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Load_cbbDanhSachLoc();
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            value = 0;
            hienThi = true;
            plChuaHoSo.Controls.Clear();
            if (cbbTinhChatLoc.Text == "Tất cả")
            {
                TimViecLam_Load(sender, e);
            }
            else if (cbbTinhChatLoc.Text == "Khu vực")
            {
                sqlStr = "SELECT TieuDe, TenCongViec, LoaiHinhCongViec, dt.Email, MucLuong, YC_KinhNghiem, Ten, DiaChi " +
                    "FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email WHERE DiaChi LIKE CONCAT('%', @KhuVuc)";
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.Add("@KhuVuc", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string TieuDe = reader["TieuDe"].ToString();
                                    string LoaiHinhCongViec = reader["LoaiHinhCongViec"].ToString();
                                    string Email = reader["Email"].ToString();
                                    string MucLuong = reader["MucLuong"].ToString();
                                    string YC_KinhNghiem = reader["YC_KinhNghiem"].ToString();
                                    string Ten = reader["Ten"].ToString();
                                    string tenCongViec = reader["TenCongViec"].ToString();
                                    string DiaChi = reader["DiaChi"].ToString();
                                    if (!string.IsNullOrEmpty(DiaChi))
                                    {
                                        string[] diaChiParts = DiaChi.Split(',');
                                        if (diaChiParts.Length >= 4)
                                        {
                                            HienThiCV(TieuDe, LoaiHinhCongViec, Email, MucLuong, YC_KinhNghiem, Ten, tenCongViec, diaChiParts[3]);
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
            else if (cbbTinhChatLoc.Text == "Vị trí tuyển dụng")
            {
                sqlStr = "SELECT TieuDe, TenCongViec, LoaiHinhCongViec, dt.Email, MucLuong, YC_KinhNghiem, Ten, DiaChi " +
                    "FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email WHERE TenCongViec = @TenCongViec";
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.Add("@TenCongViec", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string TieuDe = reader["TieuDe"].ToString();
                                    string LoaiHinhCongViec = reader["LoaiHinhCongViec"].ToString();
                                    string Email = reader["Email"].ToString();
                                    string MucLuong = reader["MucLuong"].ToString();
                                    string YC_KinhNghiem = reader["YC_KinhNghiem"].ToString();
                                    string Ten = reader["Ten"].ToString();
                                    string tenCongViec = reader["TenCongViec"].ToString();
                                    string DiaChi = reader["DiaChi"].ToString();
                                    if (!string.IsNullOrEmpty(DiaChi))
                                    {
                                        string[] diaChiParts = DiaChi.Split(',');
                                        if (diaChiParts.Length >= 4)
                                        {
                                            HienThiCV(TieuDe, LoaiHinhCongViec, Email, MucLuong, YC_KinhNghiem, Ten, tenCongViec, diaChiParts[3]);
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
            else if (cbbTinhChatLoc.Text == "Tên công ty")
            {
                sqlStr = "SELECT TieuDe, TenCongViec, LoaiHinhCongViec, dt.Email, MucLuong, YC_KinhNghiem, Ten, DiaChi " +
                    "FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email WHERE Ten = @Ten";
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.Add("@Ten", cbbDanhSachLoc.Text);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string TieuDe = reader["TieuDe"].ToString();
                                    string LoaiHinhCongViec = reader["LoaiHinhCongViec"].ToString();
                                    string Email = reader["Email"].ToString();
                                    string MucLuong = reader["MucLuong"].ToString();
                                    string YC_KinhNghiem = reader["YC_KinhNghiem"].ToString();
                                    string Ten = reader["Ten"].ToString();
                                    string tenCongViec = reader["TenCongViec"].ToString();
                                    string DiaChi = reader["DiaChi"].ToString();
                                    if (!string.IsNullOrEmpty(DiaChi))
                                    {
                                        string[] diaChiParts = DiaChi.Split(',');
                                        if (diaChiParts.Length >= 4)
                                        {
                                            HienThiCV(TieuDe, LoaiHinhCongViec, Email, MucLuong, YC_KinhNghiem, Ten, tenCongViec, diaChiParts[3]);
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
            else if (cbbTinhChatLoc.Text == "Vị trí tuyển dụng")
            {
                sqlStr = "SELECT DISTINCT TenCongViec FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email";
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
                if (cbbDanhSachLoc.Text == "Đang tuyển dụng")
                {
                    sqlStr = "SELECT TieuDe, TenCongViec, LoaiHinhCongViec, dt.Email, MucLuong, YC_KinhNghiem, Ten, DiaChi " +
                    "FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email WHERE GETDATE() <= NgayHetHan";
                }
                else
                {
                    sqlStr = "SELECT TieuDe, TenCongViec, LoaiHinhCongViec, dt.Email, MucLuong, YC_KinhNghiem, Ten, DiaChi " +
                    "FROM DangTinTuyenDung dt INNER JOIN ThongTinCTy tt ON dt.Email = tt.Email WHERE GETDATE() > NgayHetHan";
                }
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
                                    string TieuDe = reader["TieuDe"].ToString();
                                    string LoaiHinhCongViec = reader["LoaiHinhCongViec"].ToString();
                                    string Email = reader["Email"].ToString();
                                    string MucLuong = reader["MucLuong"].ToString();
                                    string YC_KinhNghiem = reader["YC_KinhNghiem"].ToString();
                                    string Ten = reader["Ten"].ToString();
                                    string tenCongViec = reader["TenCongViec"].ToString();
                                    string DiaChi = reader["DiaChi"].ToString();
                                    if (!string.IsNullOrEmpty(DiaChi))
                                    {
                                        string[] diaChiParts = DiaChi.Split(',');
                                        if (diaChiParts.Length >= 4)
                                        {
                                            HienThiCV(TieuDe, LoaiHinhCongViec, Email, MucLuong, YC_KinhNghiem, Ten, tenCongViec, diaChiParts[3]);
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
        }

        private void btnBoLoc_Click_1(object sender, EventArgs e)
        {
            cbbTinhChatLoc_SelectedIndexChanged_1(sender, e);
            TimViecLam_Load(sender, e);
        }
        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            Load_cbbTinhChatLoc();
            TimViecLam_Load(sender, e);
        }
    }
}
