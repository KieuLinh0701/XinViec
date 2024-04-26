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

namespace XinViec
{
    public partial class TrangCaNhanNguoiDung : Form
    {
        string EmailUV;
        string hoTen;
        string sqlStr;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        DAO dao = new DAO();
        public TrangCaNhanNguoiDung(string EmailUV)
        {
            InitializeComponent();
            this.EmailUV = EmailUV;
        }

        private void DoiMauButtonDuocChon(Button btn)
        {
            btnXemCV.BackColor = Color.Teal;
            btnTimViecLam.BackColor = Color.Teal;
            btn.BackColor = Color.Silver;
        }

        private string LayTenHSMoiNhat()
        {
            string tenHS = "";
            sqlStr = string.Format("Select top 1 tenHoSo from HoSoXinViec where EmailDangNhap = @EmailDangNhap ORDER BY ngayCapNhat DESC");
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@EmailDangNhap", EmailUV);
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
                Guna2TextBox txb = new Guna2TextBox();
                txb.Text = "Ứng viên chưa có hồ sơ để hiển thị";
                txb.Font = new Font("Cambria", 12, FontStyle.Bold);
                txb.Location = new Point(300, 15);
                txb.Size = new System.Drawing.Size(500, 50);
                txb.ForeColor = System.Drawing.Color.DimGray;
                txb.FillColor = SystemColors.Control;
                txb.Anchor = AnchorStyles.Left & AnchorStyles.Right;
                txb.BorderThickness = 0;
                txb.ReadOnly = true;
                plChuaCV.Visible = false;
                plHienThi.Controls.Add(txb);
                txb.Show();
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
            DoiMauButtonDuocChon(btnXemCV);
            plHienThi.Controls.Clear();
            plHienThi.Controls.Add(plChuaCV);
            plHienThi.Dock = DockStyle.Top;
            plChuaCV.Visible = true;
            plChuaCV.Location = new Point(150, 24);
            plPostCongViec.Visible = false;
            GoiHoSo();
        }

        private void ChinhSuaBaiDang(string ngayDang)
        {
            FDangTinTimViec fDangTinTimViec = new FDangTinTimViec(ngayDang);
            fDangTinTimViec.DangThanhCong += btnTimViecLam_Click;
            fDangTinTimViec.ShowDialog();
        }

        private void XemCV(string tenHoSo)
        {
            HoSo hs = new HoSo(EmailUV, tenHoSo);
            hs.ShowDialog();
        }

        private void XoaBaiDang(string ngayDang)
        {
            sqlStr = "DELETE FROM DangTinTimViec WHERE EmailUngVien = @EmailUngVien AND NgayDang = @NgayDang";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        // Assuming you have parameters to set for your query
                        command.Parameters.AddWithValue("@EmailUngVien", EmailUV);
                        command.Parameters.AddWithValue("@NgayDang ", ngayDang);
                        int k = command.ExecuteNonQuery();

                        // Check if rows were affected
                        if (k > 0)
                        {
                            dao.ThongBao("Xóa thành công");
                            btnTimViecLam_Click(this, EventArgs.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.ThongBao("Xóa thất bại: " + ex);
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
            DoiMauButtonDuocChon(btnTimViecLam);
            plHienThi.Controls.Clear();
            plHienThi.Controls.Add(plChuaCV);
            plHienThi.Dock = DockStyle.Fill;
            plChuaCV.Visible = false;
            plPostCongViec.Visible = true;
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
                                string  ngayDang = reader["NgayDang"].ToString();
                                HienThi_TinTimViecLam(tenHoSo, noiDung, ngayDang);
                                t++;
                            }
                            if (t==0)
                            {
                                Guna2TextBox txb = new Guna2TextBox();
                                txb.Text = "Ứng viên chưa có bài đăng nào để hiển thị";
                                txb.Font = new Font("Cambria", 12, FontStyle.Bold);
                                txb.Location = new Point(280, 15);
                                txb.Size = new System.Drawing.Size(500, 50);
                                txb.ForeColor = System.Drawing.Color.DimGray;
                                txb.FillColor = SystemColors.Control;
                                txb.Anchor = AnchorStyles.Left & AnchorStyles.Right;
                                txb.BorderThickness = 0;
                                txb.ReadOnly = true;
                                plChuaCV.Visible = false;
                                plHienThi.Controls.Add(txb);
                                txb.Show();
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
    }
}
