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
    public partial class FDangTinTuyenDung : Form
    {
        string tieuDe;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string Email = StateStorage.GetInstance().SharedValue.ToString();
        string sqlStr;


        public FDangTinTuyenDung()
        {
            InitializeComponent();

        }
        public FDangTinTuyenDung(string Email, string tieuDe)
        {
            //InitializeComponent();
            //this.Email = Email;
            //this.tieuDe = tieuDe;
        }

        

        private void btnDangTin_Click(object sender, EventArgs e)
        {
            //string maCV;
            string tieuDe = txtTieuDe.Text.ToUpper();
            string tenCongViec = txtTenCongViec.Text;
            string loaiHinhCV = cbbLoaiHinhCV.Text;
            string luong = txtLuong.Text;
            string capBac = txtCapBac.Text;
            int soLuongTuyen = int.Parse(txtSoLuongTuyen.Text); // Chuyển đổi về kiểu số nguyên
            DateTime ngayHetHan = dtpNgayHetHan.Value;
            string quyenLoi = txtQuyenLoi.Text;
            string moTa = txtMoTa.Text;
            string yeuCauGioiTinh = cbbYC_GioiTinh.Text;
            string yeuCauNgoaiNgu = cbbYC_NgoaiNgu.Text;
            string yeuCauTrinhDoVanHoa = cbbYC_TrinhDoVanHoa.Text;
            string yeuCauTrinhDoChuyenMon = cbbYC_TrinhDoChuyenMon.Text;
            string yeuCauKinhNghiem = txtYC_KinhNghiem.Text;
            string yeuCauKhac = txtYeuCauKhac.Text;

            // Xử lý danh sách các kỹ năng yêu cầu
            List<string> kyNangList = new List<string>();
            foreach (string item in clbYC_KyNang.CheckedItems)
            {
                kyNangList.Add(item.ToString());
            }
            string kyNang = string.Join(", ", kyNangList);

            try
            {
                conn.Open();

                // Tạo truy vấn SQL để chèn dữ liệu vào bảng DangTinTuyenDung
                sqlStr = "INSERT INTO DangTinTuyenDung (TieuDe, TenCongViec, LoaiHinhCongViec, MucLuong, CapBac, SoLuongTuyen, NgayHetHan, QuyenLoi_DaiNgo, MoTaCongViec, YC_GioiTinh, YC_NgoaiNgu, YC_TrinhDoVanHoa, YC_TrinhDoChuyenMon, YC_KyNang, YC_KinhNghiem, YeuCauKhac, Email, NgayDang, TrangThai) VALUES (@TieuDe, @TenCongViec, @LoaiHinhCongViec, @MucLuong, @CapBac, @SoLuongTuyen, @NgayHetHan, @QuyenLoi_DaiNgo, @MoTaCongViec, @YC_GioiTinh, @YC_NgoaiNgu, @YC_TrinhDoVanHoa, @YC_TrinhDoChuyenMon, @YC_KyNang, @YC_KinhNghiem, @YeuCauKhac, @Email, @NgayDang, @TrangThai)";
                SqlCommand cmd = new SqlCommand(sqlStr, conn);

                // Thêm tham số và gán giá trị cho các tham số
                cmd.Parameters.AddWithValue("@TieuDe", tieuDe);
                cmd.Parameters.AddWithValue("@TenCongViec", tenCongViec);
                cmd.Parameters.AddWithValue("@LoaiHinhCongViec", loaiHinhCV);
                cmd.Parameters.AddWithValue("@MucLuong", luong);
                cmd.Parameters.AddWithValue("@CapBac", capBac);
                cmd.Parameters.AddWithValue("@SoLuongTuyen", soLuongTuyen);
                cmd.Parameters.AddWithValue("@NgayHetHan", ngayHetHan);
                cmd.Parameters.AddWithValue("@QuyenLoi_DaiNgo", quyenLoi);
                cmd.Parameters.AddWithValue("@MoTaCongViec", moTa);
                cmd.Parameters.AddWithValue("@YC_GioiTinh", yeuCauGioiTinh);
                cmd.Parameters.AddWithValue("@YC_NgoaiNgu", yeuCauNgoaiNgu);
                cmd.Parameters.AddWithValue("@YC_TrinhDoVanHoa", yeuCauTrinhDoVanHoa);
                cmd.Parameters.AddWithValue("@YC_TrinhDoChuyenMon", yeuCauTrinhDoChuyenMon);
                cmd.Parameters.AddWithValue("@YC_KyNang", kyNang);
                cmd.Parameters.AddWithValue("@YC_KinhNghiem", yeuCauKinhNghiem);
                cmd.Parameters.AddWithValue("@YeuCauKhac", yeuCauKhac);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@NgayDang", DateTime.Now);
                string trangThai = "Đang tuyển dụng";
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                // Thực thi truy vấn
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đã đăng tin tuyển dụng thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        

        private void FDangTinTuyenDung_Load(object sender, EventArgs e)
        {
            dtpNgayHetHan.Value = DateTime.Now;
            UCQLCongViec uCQLCongViec = new UCQLCongViec();
            string btnName = uCQLCongViec.NutTieuDe;
            tieuDe = btnName;

            try
            {
                conn.Open();
                sqlStr = string.Format("SELECT * FROM DangTinTuyenDung WHERE Email = @Email AND TieuDe = @TieuDe");
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@TieuDe", tieuDe);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtTieuDe.Text = reader["TieuDe"].ToString();
                        txtTenCongViec.Text = reader["TenCongViec"].ToString();
                        cbbLoaiHinhCV.Text = reader["LoaiHinhCongViec"].ToString();
                        txtLuong.Text = reader["MucLuong"].ToString();
                        txtCapBac.Text = reader["CapBac"].ToString();
                        txtSoLuongTuyen.Text = reader["SoLuongTuyen"].ToString();
                        dtpNgayHetHan.Value = Convert.ToDateTime(reader["NgayHetHan"]);
                        txtQuyenLoi.Text = reader["QuyenLoi_DaiNgo"].ToString();
                        txtMoTa.Text = reader["MoTaCongViec"].ToString();
                        cbbYC_GioiTinh.Text = reader["YC_GioiTinh"].ToString();
                        cbbYC_NgoaiNgu.Text = reader["YC_NgoaiNgu"].ToString();
                        cbbYC_TrinhDoVanHoa.Text = reader["YC_TrinhDoVanHoa"].ToString();
                        cbbYC_TrinhDoChuyenMon.Text = reader["YC_TrinhDoChuyenMon"].ToString();
                        txtYC_KinhNghiem.Text = reader["YC_KinhNghiem"].ToString();
                        txtYeuCauKhac.Text = reader["YeuCauKhac"].ToString();

                        // Xử lý danh sách các kỹ năng yêu cầu
                        string kyNang = reader["YC_KyNang"].ToString();
                        string[] kyNangArr = kyNang.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string skill in kyNangArr)
                        {
                            int index = clbYC_KyNang.FindStringExact(skill.Trim());
                            if (index != -1)
                            {
                                clbYC_KyNang.SetItemChecked(index, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
