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
    public partial class FGiaoDienCongTy : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string Email = StateStorage.GetInstance().SharedValue.ToString();
        string sqlStr;
        FThongBao_LuaChon tb = new FThongBao_LuaChon();
        FThongTinCongTy fTTCTy = new FThongTinCongTy();
        public FGiaoDienCongTy()
        {
            InitializeComponent();
            customizeDesing();
            
        }

      

        private void customizeDesing()
        {
            plCaiDat.Visible = false;
        }
        private void hideCaiDat()
        {
            if (plCaiDat.Visible == true)
                plCaiDat.Visible = false;
        }
        private void showCaiDat(Panel subCaiDat)
        {
            if (subCaiDat.Visible == false)
            {
                hideCaiDat();
                subCaiDat.Visible = true;
            }
            else
                subCaiDat.Visible = false;
        }
        //.

        // gọi FormCon trên plFormCha
        private Form activeForm = null;
        public void openFormCon(Form fCon)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = fCon;
            fCon.TopLevel = false;
            fCon.FormBorderStyle = FormBorderStyle.None;
            fCon.Dock = DockStyle.Fill;
            plFormCha.Controls.Add(fCon);
            plFormCha.Tag = fCon;
            fCon.BringToFront();
            fCon.Show();
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            if (IsCompanyInfoValid(Email))
            {
                showCaiDat(plCaiDat);
            }
            else
            {
                // Hiển thị thông báo nếu có một trong các thuộc tính cần thiết rỗng
                MessageBox.Show("Vui lòng điền đầy đủ thông tin công ty!");
            }
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            hideCaiDat();
            //
            openFormCon(new FCaiDatEmail("Nha Tuyen Dung"));
        }

        private void btnSuaThongTin_Click(object sender, EventArgs e)
        {
            
            openFormCon(new FThongTinCongTy());
            hideCaiDat();
        }

        private void btnMatKhau_Click(object sender, EventArgs e)
        {
            hideCaiDat();
            openFormCon(new FCaiDatMatKhau());
        }

        public bool IsCompanyInfoValid(string email)
        {
            // Truy vấn để lấy dữ liệu từ bảng ThongTinCTy
            string query = "SELECT * FROM ThongTinCTy WHERE Email = @Email";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Email", email);

            try
            {
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Lấy giá trị của các thuộc tính cần thiết từ cơ sở dữ liệu
                    string tenCongTy = reader["Ten"].ToString();
                    string ngayThanhLap = reader["NgayThanhLap"] != DBNull.Value ? reader["NgayThanhLap"].ToString() : "";
                    string quyMo = reader["QuyMo"] != DBNull.Value ? reader["QuyMo"].ToString() : "";
                    string sdt = reader["SDT"].ToString();
                    string diaChi = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : "";
                    string moTa = reader["MoTa"] != DBNull.Value ? reader["MoTa"].ToString() : "";
                    string nguoiDungDau = reader["NguoiDungDau"].ToString();
                    string maSoThue = reader["MaSoThue"] != DBNull.Value ? reader["MaSoThue"].ToString() : "";
                    byte[] giayPhepKinhDoanh = reader["GiayPhepKinhDoanh"] != DBNull.Value ? (byte[])reader["GiayPhepKinhDoanh"] : null;
                    string chinhSachPT = reader["ChinhSachPT"] != DBNull.Value ? reader["ChinhSachPT"].ToString() : "";
                    string coHoiTT = reader["CoHoiTT"] != DBNull.Value ? reader["CoHoiTT"].ToString() : "";
                    string csLuongThuong = reader["CSLuongThuong"] != DBNull.Value ? reader["CSLuongThuong"].ToString() : "";
                    string nguoiLienLac = reader["NguoiLienLac"] != DBNull.Value ? reader["NguoiLienLac"].ToString() : "";
                    // Kiểm tra các thuộc tính cần thiết
                    if (string.IsNullOrWhiteSpace(tenCongTy) || string.IsNullOrWhiteSpace(nguoiDungDau) ||
                     string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(email) ||
                         string.IsNullOrWhiteSpace(ngayThanhLap) || string.IsNullOrWhiteSpace(quyMo) ||
                         string.IsNullOrWhiteSpace(diaChi) || string.IsNullOrWhiteSpace(moTa) ||
                            string.IsNullOrWhiteSpace(maSoThue) || giayPhepKinhDoanh == null ||
                             string.IsNullOrWhiteSpace(chinhSachPT) || string.IsNullOrWhiteSpace(coHoiTT) ||
                                     string.IsNullOrWhiteSpace(csLuongThuong) || string.IsNullOrWhiteSpace(nguoiLienLac))
                    {
                        // Một trong các thuộc tính cần thiết không có giá trị, trả về false
                        return false;
                    }
                    else
                    {
                        // Tất cả các thuộc tính cần thiết có giá trị, trả về true
                        return true;
                    }
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnDangTin_Click(object sender, EventArgs e)
        {
            if (IsCompanyInfoValid(Email))
            {
                FDangTinTuyenDung fDangTinTuyenDung = new FDangTinTuyenDung();
                // Các thuộc tính cần thiết có giá trị, tiến hành mở form FDangTinTuyenDung
                fDangTinTuyenDung.btnQuayLai.Visible = false;
                openFormCon(fDangTinTuyenDung);
                plLocCongViec.Visible = false;
                plLocHoSo.Visible = false;


            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin công ty!");
            }
        
        }

        //private void btnQLHoSo_Click(object sender, EventArgs e)
        //{
        //   openFormCon(new FQuanLyHoSo(tieuDe));
        //    //
        //    plLocCongViec.Visible = false; 
        //    plLocHoSo.Visible = true; 

        //}

        private void QuayLaiTuFQuanLyHoSo(object sender, EventArgs e)
        {
            // Thực hiện các hành động cần thiết khi quay lại từ FQuanLyHoSo
            // Ví dụ: Hiển thị lại form công việc (nếu cần)
            btnQLCongViec_Click(sender, e); // Gọi lại phương thức xử lý sự kiện btnQLCongViec_Click
        }

        public void btnQLCongViec_Click(object sender, EventArgs e)
        {
            if (IsCompanyInfoValid(Email))
            {
                openFormCon(new FQLCongViec());
                //
                plLocCongViec.Visible = true;
                plLocHoSo.Visible = false;
            }
            else
            {
                // Hiển thị thông báo nếu có một trong các thuộc tính cần thiết rỗng
                MessageBox.Show("Vui lòng điền đầy đủ thông tin công ty!");
            }
        }

        private void lblTenCongTy_Click(object sender, EventArgs e)
        {
            //
            if (IsCompanyInfoValid(Email))
            {
                openFormCon(new FCongTy(Email));
            }
            else
            {
                // Hiển thị thông báo nếu có một trong các thuộc tính cần thiết rỗng
                MessageBox.Show("Vui lòng điền đầy đủ thông tin công ty!");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (IsCompanyInfoValid(Email))
            {
                TroChuyen tc = new TroChuyen();
                tc.TopLevel = false;
                plFormCha.Controls.Clear();
                plFormCha.Controls.Add(tc);
                tc.Dock = DockStyle.Fill;
                tc.Show();
            }
            else
            {
                // Hiển thị thông báo nếu có một trong các thuộc tính cần thiết rỗng
                MessageBox.Show("Vui lòng điền đầy đủ thông tin công ty!");
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FGiaoDienCongTy_Load(object sender, EventArgs e)
        {
            sqlStr = string.Format("SELECT * FROM ThongTinCTy WHERE Email = @Email");

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@Email", Email);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        lblTenCongTy.Text = reader["Ten"].ToString();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { conn.Close(); }
            CountAllApplications();
            demHoSo();
            demHoSoDaDuyet();
            lblSLHoSoChuaDuyet.Text = (demHoSo() - demHoSoDaDuyet()).ToString();
            //slCVDaDu();


        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản không?", "Xác nhận xóa tài khoản", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng chọn Yes, tiến hành xóa tài khoản
            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    // Xóa dữ liệu từ bảng DangTinTuyenDung
                    string deleteQueryDangTin = "DELETE FROM DangTinTuyenDung WHERE Email = @Email";
                    SqlCommand commandDangTin = new SqlCommand(deleteQueryDangTin, conn);
                    commandDangTin.Parameters.AddWithValue("@Email", Email);
                    commandDangTin.ExecuteNonQuery();

                    // Xóa dữ liệu từ bảng ThongTinCTy
                    string deleteQueryThongTinCTy = "DELETE FROM ThongTinCTy WHERE Email = @Email";
                    SqlCommand commandThongTinCTy = new SqlCommand(deleteQueryThongTinCTy, conn);
                    commandThongTinCTy.Parameters.AddWithValue("@Email", Email);
                    commandThongTinCTy.ExecuteNonQuery();

                    // Xóa dữ liệu từ bảng TaiKhoan
                    string deleteQueryTaiKhoan = "DELETE FROM TaiKhoan WHERE Email = @Email";
                    SqlCommand commandTaiKhoan = new SqlCommand(deleteQueryTaiKhoan, conn);
                    commandTaiKhoan.Parameters.AddWithValue("@Email", Email);
                    commandTaiKhoan.ExecuteNonQuery();

                    MessageBox.Show("Xóa tài khoản thành công.");
                    // Đóng form hoặc thực hiện các bước tiếp theo sau khi xóa tài khoản thành công.
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }


        }

        



        // dem tong so luong cong viec da dang
        private void CountAllApplications()
        {
            // Tạo truy vấn SQL để đếm số lượng công việc được ứng tuyển vào công ty
            string countQuery = "SELECT COUNT(*) FROM DangTinTuyenDung WHERE Email = @Email";

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(countQuery, conn);
                command.Parameters.AddWithValue("@Email", Email);
                int count = (int)command.ExecuteScalar(); // Đếm số lượng và gán vào biến count

                // Hiển thị số lượng công việc trên nhãn lblSLTatCa
                lbSLTatCa.Text = count.ToString();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
           
        }

        

        private int demHoSo()
        {
            // Tạo truy vấn SQL để đếm số lượng công việc được ứng tuyển vào công ty
            string countQuery = "SELECT COUNT(*) FROM UngTuyen WHERE EmailCongTy = @Email";

            int count = 0;
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(countQuery, conn);
                command.Parameters.AddWithValue("@Email", Email);
                count = (int)command.ExecuteScalar(); // Đếm số lượng và gán vào biến count

                // Hiển thị số lượng công việc trên nhãn lblSLTatCa
                lblTatCaHS.Text = count.ToString();
               
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        private int demHoSoDaDuyet()
        {
            int count = 0;
            // Tạo truy vấn SQL để đếm số lượng công việc được ứng tuyển vào công ty
            string countQuery = "SELECT COUNT(*) FROM UngTuyen WHERE EmailCongTy = @Email AND TrangThaiDuyet = N'Đã duyệt' ";

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(countQuery, conn);
                command.Parameters.AddWithValue("@Email", Email);
                count = (int)command.ExecuteScalar(); // Đếm số lượng và gán vào biến count

                // Hiển thị số lượng công việc trên nhãn lblSLTatCa
                lblSlHoSoDaDuyet.Text = count.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        private void lblTatCaHS_Click(object sender, EventArgs e)
        {

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

        private void btnCVYeuThich_Click(object sender, EventArgs e)
        {
            if(IsCompanyInfoValid(Email))
            {
                
                openFormCon(new FHoSoYeuThich());
                plLocCongViec.Visible = false;
                plLocHoSo.Visible = false;


            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin công ty!");
            }
        }

        private void btnUngVienKhongPV_Click(object sender, EventArgs e)
        {
            DAO dao = new DAO();
            dao.MoFormCon(new FUngVienKhongPV(), plFormCha);
        }
    }
}
