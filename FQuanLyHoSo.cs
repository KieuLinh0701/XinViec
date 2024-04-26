using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FQuanLyHoSo : Form
    {
        string EmailUngVien;
        string TieuDe;
        DAO dao = new DAO();
        public event EventHandler QuayLai;
        public UCQLHoSo uc;
        FThongBao_LuaChon tb;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        string Email = StateStorage.GetInstance().SharedValue;
        

        public FQuanLyHoSo(string Email, string TieuDe)
        { 
            InitializeComponent();
            this.Email = Email;
            this.TieuDe = TieuDe;
        }

        private void XoaHS(string EmailUngVien, string TenHoSo)
        {
            tb = new FThongBao_LuaChon();
            tb.rtbThongBao.Text = "Bạn muốn xóa tin này?";
            tb.Show();
            this.EmailUngVien = EmailUngVien;
            tb.ChonButtonCo += Xoa;
        }

        private void Xoa(object sender, EventArgs e)
        {
            tb.Close();
            sqlStr = "DELETE FROM UngTuyen WHERE EmailCongTy = @Email AND EmailUngVien = @EmailUngVien";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        // Assuming you have parameters to set for your query
                        command.Parameters.AddWithValue("@EmailUngVien", EmailUngVien );
                        command.Parameters.AddWithValue("@Email", Email);


                        // Check if rows were affected
                        if (command.ExecuteNonQuery() > 0)
                        {
                            FThongBao tbao = new FThongBao();
                            tbao.rtbThongBao.Text = "Xóa thành công";
                            tbao.Show();
                            plUC.Controls.Clear();
                            FQuanLyHoSo_Load(sender, e); // Gọi sự kiện load dữ liệu sau khi xóa
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FThongBao tbao = new FThongBao();
                tbao.rtbThongBao.Text = "Xóa thất bại: " + ex;
                tbao.Show();
            }
        }

        private void themUCHSCongViec(string emailUV,string tenHS, string tenCV, string viTriUT, DateTime ngayUngTuyen, string trangThaiDuyet, string tinhTrangHS )
        {
            UCQLHoSo uc = new UCQLHoSo();
            uc.txtEmailUV.Text = emailUV;
            uc.btnXemTTCN.Text = tenHS;
            uc.txtTenCV.Text = tenCV;
            uc.txtViTriUT.Text = viTriUT;   
            uc.txtNgayUT.Text = ngayUngTuyen.ToString("dd/MM/yyyy");
            uc.txtTrangThaiDuyet.Text = trangThaiDuyet;
            uc.txtTinhTrangHS.Text = tinhTrangHS;
            uc.ChonButtonXemCV += XemHS;
            uc.ChonButtonXemTTCN += XemTTCN;
            uc.ChonButtonXoa += XoaHS;
            uc.ChonButtonDuyet += DuyetHS;
            uc.Dock = DockStyle.Top;
            plUC.Controls.Add(uc);
            uc.Show();
        }

        private void DuyetHS(string EmailUngVien, string TenHoSo)
        {
            FDuyetHoSo formDuyet = new FDuyetHoSo();
            //FDatLichPhongVan fDatLichPhongVan = new FDatLichPhongVan();
            if (formDuyet.ShowDialog() == DialogResult.OK)
            {
                // Người dùng đã chọn một lựa chọn, bạn có thể thực hiện các hành động cần thiết ở đây
                string trangThai = formDuyet.TrangThai; // Trạng thái mà người dùng đã chọn
                                                        // Cập nhật Trạng thái hồ sơ trong cơ sở dữ liệu

                //DateTime lichHen = fDatLichPhongVan.lichHen;
                    CapNhatTrangThaiHoSo(EmailUngVien, TenHoSo, trangThai);
                    CapNhatTrangThaiDuyet();
                if(trangThai == "Đáp ứng")
                        {
                    FDatLichPhongVan fDatLichPhongVan = new FDatLichPhongVan(EmailUngVien, TenHoSo); // Truyền giá trị EmailUngVien và TenHoSo
                                                                                                     // Thêm các xử lý tiếp theo nếu cần
                    fDatLichPhongVan.ShowDialog(); // Hiển thị form FDatLichPhongVan
                                                   // CapNhatLichHen(EmailUngVien, TenHoSo, lichHen);
                }

            }
        }

        private void CapNhatTrangThaiDuyet()
        {
            // Viết câu lệnh SQL để cập nhật dữ liệu
            string sqlStr = "UPDATE UngTuyen SET TrangThaiDuyet = N'Đã duyệt' WHERE TinhTrangHS IS NOT NULL";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            //MessageBox.Show("Cập nhật trạng thái duyệt thành công.");
                            // Cập nhật lại giao diện
                            CapNhatGiaoDien();
                        }
                        else
                        {
                            MessageBox.Show("Không có bản ghi nào được cập nhật.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái duyệt: " + ex.Message);
            }
        }

       

       

        private void CapNhatTrangThaiHoSo(string emailUngVien, string tenHoSo, string trangThai)
        {
            // Viết mã SQL để cập nhật trạng thái hồ sơ trong cơ sở dữ liệu
            string sqlStr = "UPDATE UngTuyen SET TinhTrangHS = @TrangThai WHERE EmailUngVien = @EmailUngVien AND TenHoSo = @TenHoSo";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@TrangThai", trangThai);
                        command.Parameters.AddWithValue("@EmailUngVien", emailUngVien);
                        command.Parameters.AddWithValue("@TenHoSo", tenHoSo);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật trạng thái hồ sơ thành công.");
                            // Cập nhật lại giao diện
                            CapNhatGiaoDien();
                        }
                        else
                        {
                            MessageBox.Show("Không có hồ sơ nào được cập nhật.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái hồ sơ: " + ex.Message);
            }
        }

        private void CapNhatGiaoDien()
        {
            // Xóa tất cả các điều khiển hiện tại trên panel hiển thị hồ sơ
            plUC.Controls.Clear();
            // Load lại dữ liệu hồ sơ từ cơ sở dữ liệu
            FQuanLyHoSo_Load(this, EventArgs.Empty);
        }


        public Form activeForm = null;
        public void MoFormCon(Form fCon, Panel pl)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = fCon;
            fCon.TopLevel = false;
            fCon.FormBorderStyle = FormBorderStyle.None;
            fCon.Dock = DockStyle.Fill;
            //pl.Controls.Clear();
            pl.Controls.Add(fCon);
            pl.Tag = fCon;
            fCon.BringToFront();
            fCon.Show();
        }



     

        private void XemTTCN(string emailUngVien)
        {
            FXemThongTinCaNhan fXemThongTinCaNhan = new FXemThongTinCaNhan(emailUngVien);
            fXemThongTinCaNhan.btnChinhSua.Visible = false;
            fXemThongTinCaNhan.FormBorderStyle = FormBorderStyle.FixedSingle;
            fXemThongTinCaNhan.Text = "Thông tin cá nhân";
            fXemThongTinCaNhan.btnDongForm.Visible = true;
            MoFormCon(fXemThongTinCaNhan, plFormCha);
        }

      

        private void XemHS(string emailUngVien, string tenHoSo)
        {
            HoSo hoSo = new HoSo(emailUngVien, tenHoSo);
            hoSo.btnDongForm.Visible=true;
            hoSo.btnXemTrangCaNhan.Visible=true;
            MoFormCon(hoSo, plFormCha);
        }

        private void FQuanLyHoSo_Load(object sender, EventArgs e)
        {

            //  sqlStr = "SELECT * FROM DangTinTuyenDung INNER JOIN UngTuyen ON DangTinTuyenDung.Email = UngTuyen.EmailCongTy INNER JOIN UngVien ON UngTuyen.EmailUngVien = UngVien.EmailDangNhap WHERE DangTinTuyenDung.Email = @Email";
            sqlStr = "select * From UngTuyen u " +
                "inner join DangTinTuyenDung d on u.EmailCongTy = d.Email AND u.TieuDe = d.TieuDe " +
                "where d.Email = @Email AND d.TieuDe = @TieuDe";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@TieuDe", TieuDe);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string emailUV = reader["EmailUngVien"].ToString();
                                string tenHS = reader["TenHoSo"].ToString();
                                string tenCV = reader["TenCongViec"].ToString();
                                string viTri = reader["CapBac"].ToString();
                              
                                DateTime ngayUngTuyen = reader.GetDateTime(reader.GetOrdinal("NgayUngTuyen"));
                                string trangThaiDuyet = reader["TrangThaiDuyet"].ToString() ;
                                string tinhTrangHS = reader["TinhTrangHS"].ToString();
                                themUCHSCongViec(emailUV, tenHS, tenCV, viTri, ngayUngTuyen, trangThaiDuyet, tinhTrangHS);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            int thangLoc = Convert.ToInt32(cbbThang.SelectedItem);
            int namLoc = Convert.ToInt32(cbbNam.SelectedItem);

            // Call the method to filter data based on selected month and year
            LocTheoThangVaNam(thangLoc, namLoc);
        }
        private void LocTheoThangVaNam(int thangLoc, int namLoc)
        {
            // Clear existing controls before populating with filtered data
            plUC.Controls.Clear();

            // Check if both month and year are selected
            bool locTheoThang = (cbbThang.SelectedIndex != -1);
            bool locTheoNam = (cbbNam.SelectedIndex != -1);

            // Construct the SQL query based on the selected filters
            
            if (locTheoThang && locTheoNam)
            {
                // Filter by both month and year
                sqlStr = "SELECT UngTuyen.EmailUngVien, UngTuyen.TenHoSo, DangTinTuyenDung.TenCongViec, DangTinTuyenDung.CapBac, UngTuyen.NgayUngTuyen, UngTuyen.TrangThaiDuyet, UngTuyen.TinhTrangHS " +
                         "FROM UngTuyen " +
                         "INNER JOIN DangTinTuyenDung ON UngTuyen.EmailCongTy = DangTinTuyenDung.Email AND UngTuyen.TieuDe = DangTinTuyenDung.TieuDe " +
                         "WHERE DangTinTuyenDung.Email = @Email AND MONTH(UngTuyen.NgayUngTuyen) = @ThangLoc AND YEAR(UngTuyen.NgayUngTuyen) = @NamLoc";
            }
            else if (locTheoThang && !locTheoNam)
            {
                // Filter only by month
                sqlStr = "SELECT UngTuyen.EmailUngVien, UngTuyen.TenHoSo, DangTinTuyenDung.TenCongViec, DangTinTuyenDung.CapBac, UngTuyen.NgayUngTuyen, UngTuyen.TrangThaiDuyet, UngTuyen.TinhTrangHS " +
                         "FROM UngTuyen " +
                         "INNER JOIN DangTinTuyenDung ON UngTuyen.EmailCongTy = DangTinTuyenDung.Email AND UngTuyen.TieuDe = DangTinTuyenDung.TieuDe " +
                         "WHERE DangTinTuyenDung.Email = @Email AND MONTH(UngTuyen.NgayUngTuyen) = @ThangLoc";
            }
            else if (!locTheoThang && locTheoNam)
            {
                // Filter only by year
                sqlStr = "SELECT UngTuyen.EmailUngVien, UngTuyen.TenHoSo, DangTinTuyenDung.TenCongViec, DangTinTuyenDung.CapBac, UngTuyen.NgayUngTuyen, UngTuyen.TrangThaiDuyet, UngTuyen.TinhTrangHS " +
                         "FROM UngTuyen " +
                         "INNER JOIN DangTinTuyenDung ON UngTuyen.EmailCongTy = DangTinTuyenDung.Email AND UngTuyen.TieuDe = DangTinTuyenDung.TieuDe " +
                         "WHERE DangTinTuyenDung.Email = @Email AND YEAR(UngTuyen.NgayUngTuyen) = @NamLoc";
            }
            else
            {
                // No filter selected, retrieve all records
                sqlStr = "SELECT UngTuyen.EmailUngVien, UngTuyen.TenHoSo, DangTinTuyenDung.TenCongViec, DangTinTuyenDung.CapBac, UngTuyen.NgayUngTuyen, UngTuyen.TrangThaiDuyet, UngTuyen.TinhTrangHS " +
                         "FROM UngTuyen " +
                         "INNER JOIN DangTinTuyenDung ON UngTuyen.EmailCongTy = DangTinTuyenDung.Email AND UngTuyen.TieuDe = DangTinTuyenDung.TieuDe " +
                         "WHERE DangTinTuyenDung.Email = @Email";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);
                        if (locTheoThang)
                        {
                            command.Parameters.AddWithValue("@ThangLoc", thangLoc);
                        }
                        if (locTheoNam)
                        {
                            command.Parameters.AddWithValue("@NamLoc", namLoc);
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string emailUV = reader["EmailUngVien"].ToString();
                                string tenHS = reader["TenHoSo"].ToString();
                                string tenCV = reader["TenCongViec"].ToString();
                                string viTri = reader["CapBac"].ToString();
                                DateTime ngayUngTuyen = reader.GetDateTime(reader.GetOrdinal("NgayUngTuyen"));
                                string trangThaiDuyet = reader["TrangThaiDuyet"].ToString();
                                string tinhTrangHS = reader["TinhTrangHS"].ToString();
                                themUCHSCongViec(emailUV, tenHS, tenCV, viTri, ngayUngTuyen, trangThaiDuyet, tinhTrangHS);
                            }
                            
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void cbbThang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                while (cbbThang.SelectedItem != null)
                {
                    cbbThang.Items.Remove(cbbThang.SelectedItem);
                }
            }
        }

        private void cbbNam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                while (cbbNam.SelectedItem != null)
                {
                    cbbNam.Items.Remove(cbbNam.SelectedItem);
                }
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayLai?.Invoke(this, EventArgs.Empty);
        }

        private void rbtnCVTot_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdMoiNhat_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    
}


    
