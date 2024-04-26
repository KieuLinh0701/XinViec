using Guna.UI2.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class FTrangCaNhanNguoiDung : Form
    {
        string EmailUV;
        string hoTen;
        string sqlStr;
        public event EventHandler QuayLai;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string Email = StateStorage.GetInstance().SharedValue.ToString();
        DAO dao = new DAO();
        Button oldBtn;
        DangTinTimViecDAO dangTinTimViecDAO = new DangTinTimViecDAO();

        public FTrangCaNhanNguoiDung(string EmailUV)
        {
            InitializeComponent();
            this.EmailUV = EmailUV;
            oldBtn = btnXemCV;
            btnXemCV.BackColor = Color.Silver;
        }

        private string LayTenHSMoiNhat()
        {
            string tenHS = "";
            sqlStr = string.Format("Select top 1 tenHoSo from HoSoXinViec where EmailDangNhap = '{0}' ORDER BY ngayCapNhat DESC", EmailUV);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tenHS = reader["tenHoSo"].ToString();
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
            return tenHS;
        }

        private void GoiHoSo()
        {
            if (LayTenHSMoiNhat() == "") 
            {
                plChuaCV.Visible = false;
                dao.TaoTextBox("Ứng viên chưa có hồ sơ để hiển thị", 300, 15, 12, 500, 50, plHienThi);
            }
            else
            {
                HoSo hs = new HoSo(EmailUV, LayTenHSMoiNhat());
                hs.LoadHS();
                Panel pl = hs.GetChildPanel();
                pl.Dock = DockStyle.Fill;
                plChuaCV.Controls.Clear();
                plChuaCV.Controls.Add(pl);
                pl.Show();
            }
        }

        private void Load_ThongTinUV()
        {
            sqlStr = string.Format("Select * from UngVien where EmailDangNhap = @EmailDangNhap");
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@EmailDangNhap", EmailUV);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["Anh"] != DBNull.Value && reader["Anh"] != null)
                        {
                            byte[] img = (byte[])reader["Anh"];
                            ptbAnh.Image = dao.ByteArrayToImage(img);
                        }
                        else
                        {
                            ptbAnh.Image = Properties.Resources.anhttcn_macdinh;
                        }
                        hoTen = reader["hoTen"].ToString();
                        lblTenUV.Text = hoTen;
                        DateTime ngaySinh = reader.GetDateTime(reader.GetOrdinal("ngaySinh"));
                        txbNgaySinh.Text = ngaySinh.ToString("dd/MM/yyyy");
                        txbEmail.Text = reader["Email"].ToString();
                        txbSDT.Text = reader["SDT"].ToString();
                        txbGioiTinh.Text = reader["gioiTinh"].ToString();
                        rtbDiaChi.Text = reader["Duong_HienNay"].ToString() + ", " + reader["Xa_HienNay"].ToString() +
                                        ", " + reader["Huyen_HienNay"].ToString() + ", " + reader["Tinh_HienNay"].ToString();
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

        private void btnXemCV_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnXemCV, oldBtn, Color.Teal, Color.Silver);
            plHienThi.Controls.Clear();
            plHienThi.Controls.Add(plChuaCV);
            plHienThi.Dock = DockStyle.Top;
            plChuaCV.Visible = true;
            plChuaCV.Location = new Point(150, 24);
            plPostCongViec.Visible = false;
            GoiHoSo();
        }

        //Chưa chuyển về dạng gọi class DangTinTimViec
        private void ChinhSuaBaiDang(string ngayDang, string tenHoSo)
        {
            FDangTinTimViec fDangTinTimViec = new FDangTinTimViec(ngayDang, tenHoSo);
            fDangTinTimViec.DangThanhCong += btnTimViecLam_Click;
            fDangTinTimViec.ShowDialog();
        }

        private DangTinTimViec TaoDangTinTimViec(string ngayDang, string noiDung, string tenHoSo)
        {
            DangTinTimViec dt = new DangTinTimViec(EmailUV, ngayDang, noiDung, tenHoSo);
            return dt;
        }

        private void XemCV(string tenHoSo)
        {
            HoSo hs = new HoSo(EmailUV, tenHoSo);
            hs.ShowDialog();
        }

        //Chưa chuyển về dạng gọi class DangTinTimViec
        private void XoaBaiDang(string ngayDang)
        {
            if (dangTinTimViecDAO.XoaTinTimViec(EmailUV, ngayDang) > 0)
            {
                dao.ThongBao("Xóa thành công");
                btnTimViecLam_Click(this, EventArgs.Empty);
            }
        }
        private void HienThi_TinTimViecLam(string tenHoSo, string noiDung, string ngayDang)
        {
            ucDangTinTimViec uc = new ucDangTinTimViec();
            uc.ChonBtnChinhSua += ChinhSuaBaiDang;
            uc.ChonBtnXemCV += XemCV;
            uc.ChonBtnXoa += XoaBaiDang;
            uc.Dock = DockStyle.Top;
            uc.txbTenHoSo.Text = tenHoSo;
            uc.lblHoTen.Text = hoTen;
            uc.txbNoiDung.Text = noiDung;
            uc.lblNgayDang.Text = ngayDang;
            plHienThi.Controls.Add(uc);
        }

        private void btnTimViecLam_Click(object sender, EventArgs e)
        {
            oldBtn = dao.DoiMauButtonKhiDuocChon(btnTimViecLam, oldBtn, Color.Teal, Color.Silver);
            plHienThi.Controls.Clear();
            plHienThi.Controls.Add(plChuaCV);
            plHienThi.Dock = DockStyle.Fill;
            plChuaCV.Visible = false;
            plPostCongViec.Visible = true;
            Load_ThongTin();
        }

        private void Load_ThongTin()
        {
            sqlStr = "SELECT * FROM DangTinTimViec WHERE EmailUngVien = @EmailUngVien ORDER BY NgayDang ASC";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailUngVien", EmailUV);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int t = 0;
                            while (reader.Read())
                            {
                                string tenHoSo = reader["TenHoSo"].ToString();
                                string noiDung = reader["NoiDung"].ToString();
                                string ngayDang = reader["NgayDang"].ToString();
                                HienThi_TinTimViecLam(tenHoSo, noiDung, ngayDang);
                                t++;
                            }
                            if (t == 0)
                            {
                                plChuaCV.Visible = false;
                                dao.TaoTextBox("Ứng viên chưa có bài đăng nào để hiển thị", 280, 15, 12, 500, 50, plHienThi);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu: " + ex);
            }
        }

        private void btnPostCongViec_Click(object sender, EventArgs e)
        {
            if (LayTenHSMoiNhat() == "")
            {
                dao.BaoLoi("Bạn chưa có hồ sơ xin việc nào. Nên không thể đăng tin tuyển dụng");
            }
            else
            {
                FDangTinTimViec fDangTinTimViec = new FDangTinTimViec();
                fDangTinTimViec.DangThanhCong += btnTimViecLam_Click;
                fDangTinTimViec.ShowDialog();
            }
        }

        private void TrangCaNhanNguoiDung_Load(object sender, EventArgs e)
        {
            Load_ThongTinUV();
            btnXemCV_Click(sender, e);
        }

        //Chưa chuyển về oop
        private void btnLuuYeuThich_Click(object sender, EventArgs e)
        {
            string ngayHienTai = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Chuẩn bị câu lệnh INSERT
            string sqlInsert = "INSERT INTO HoSoYeuThich (EmailUngVien, EmailCongTy, NgayThem) VALUES (@EmailUngVien, @EmailCongTy, @NgayThem)";

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        // Đặt các tham số
                        command.Parameters.AddWithValue("@EmailUngVien", EmailUV);
                        command.Parameters.AddWithValue("@EmailCongTy", Email); // Sử dụng địa chỉ email của công ty từ biến Email
                        command.Parameters.AddWithValue("@NgayThem", ngayHienTai);

                        // Thực thi câu lệnh INSERT
                        int rowsAffected = command.ExecuteNonQuery();

                        // Kiểm tra số hàng bị ảnh hưởng để xác nhận lưu trữ thành công
                        if (rowsAffected > 0)
                        {
                            dao.ThongBao("Đã lưu vào danh sách yêu thích.");
                        }
                        else
                        {
                            dao.BaoLoi("Lưu vào danh sách yêu thích không thành công.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi thêm vào danh sách yêu thích: " + ex.Message);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayLai?.Invoke(this, EventArgs.Empty);
        }
    }
}
