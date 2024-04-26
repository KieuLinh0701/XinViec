using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class ThongTinTuyenDung : Form
    {
        string tieuDe;
        string Email;
        string sqlStr;
        public event EventHandler DongForm;
        int t;
        bool ungTuyen;
        DAO dao = new DAO();

        public ThongTinTuyenDung(string Email, string tieuDe, int a)
        {
            InitializeComponent();
            this.Email = Email;
            this.tieuDe = tieuDe;
            MoFormUngTuyen();
        }

        public ThongTinTuyenDung(string Email, string tieuDe)
        {
            InitializeComponent();
            this.Email = Email;
            this.tieuDe = tieuDe;
        }

       
        private void MoFormUngTuyen()
        {
            if (ungTuyen == false)
            {
                ChonHoSoUngTuyen chs = new ChonHoSoUngTuyen(Email, tieuDe, 2);
                chs.Show();
            }
            else
            {
                dao.BaoLoi("Đã hết hạn đăng ký");
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
           
            DongForm?.Invoke(this, e);
        }

        private void btnUngTuyen1_Click(object sender, EventArgs e)
        {
            MoFormUngTuyen();
        }

        private void btnUngTuyen2_Click(object sender, EventArgs e)
        {
            MoFormUngTuyen();
        }

        private void btnUngTuyen3_Click(object sender, EventArgs e)
        {
            MoFormUngTuyen();
        }

        private void TrangThaiUngTuyen(String Email, string tieuDe)
        {
            sqlStr = string.Format("UPDATE DangTinTuyenDung SET TrangThai = N'Đã hết hạn' WHERE GETDATE() > NgayHetHan AND Email = @Email AND TieuDe = @TieuDe");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStr, connection);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@TieuDe", tieuDe);
                    t = command.ExecuteNonQuery();
                    if (t>0)
                    {
                        ungTuyen = true;
                    }
                    else
                    {
                        ungTuyen = false;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }
        private void ThongTinTuyenDung_Load(object sender, EventArgs e)
        {
            lblTieuDe.Text = tieuDe;
            lblEmail.Text = Email;
            TrangThaiUngTuyen(Email, tieuDe);
            sqlStr = "SELECT * FROM ThongTinCTy INNER JOIN DangTinTuyenDung on ThongTinCTy.Email = DangTinTuyenDung.Email WHERE ThongTinCTy.Email = @Email AND TieuDe = @TieuDe";
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
                                lblTenCongTy.Text = reader["Ten"].ToString().ToUpper();
                                string diaChi = reader["DiaChi"].ToString();
                                string[] diachiparts = diaChi.Split(',');
                                if (diachiparts.Length >= 4)
                                {
                                    lblDiaDiem.Text = diachiparts[3].Trim();
                                }
                                lblKinhNghiem.Text = reader["YC_KinhNghiem"].ToString();
                                lblTrangThaiTD.Text = reader["TrangThai"].ToString();
                                lblMucLuong.Text = reader["MucLuong"].ToString();
                                lblCapBac.Text = reader["CapBac"].ToString();
                                lblLoaiHinhCV.Text = reader["LoaiHinhCongViec"].ToString();
                                lblTenCV.Text = reader["TenCongViec"].ToString();
                                lblSoLuongTD.Text = reader["SoLuongTuyen"].ToString();
                                DateTime ngayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"));
                                string ngayHetHanText = ngayHetHan.ToString("dd/MM/yyyy"); 
                                lblNgayHetHan.Text = ngayHetHanText;
                                lblNgayHetHan1.Text = ngayHetHanText;

                                string yeucaugioitinh = reader["yc_gioitinh"].ToString();
                                string yeucaungoaingu = reader["yc_ngoaingu"].ToString();
                                string yeucautrinhdovanhoa = reader["yc_trinhdovanhoa"].ToString();
                                string yeucautrinhdochuyenmon = reader["yc_trinhdochuyenmon"].ToString();
                                string kinhnghiem = reader["yc_kinhnghiem"].ToString();
                                string kynang = reader["yc_kynang"].ToString();
                                string yeucaukhac = reader["yeucaukhac"].ToString();
                                string rtxtyeucautext = $"- Giới tính: {yeucaugioitinh}\n\n" +
                                $"- Trình độ ngoại ngữ: {yeucaungoaingu}\n\n" +
                                $"- Trình độ văn hóa: {yeucautrinhdovanhoa}\n\n" +
                                $"- Trình độ chuyên môn: {yeucautrinhdochuyenmon}\n\n" +
                                $"- Kinh nghiệm: {kinhnghiem}\n\n" +
                                $"- Kỹ năng: {kynang}\n\n" +
                                $"- Yêu cầu khác: {yeucaukhac}";
                                rtxtYeuCau.Text = rtxtyeucautext;

                                rtxtMoTa.Text = reader["motacongviec"].ToString();
                                rtxtChinhSach.Text = reader["quyenloi_daingo"].ToString();
                                rtxtDiaDiem.Text = reader["diachi"].ToString();

                                lblNguoiLienHe.Text = reader["NguoiLienLac"].ToString();
                                lblSDT.Text = reader["SDT"].ToString();
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
