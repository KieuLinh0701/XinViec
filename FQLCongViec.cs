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
using System.Windows.Input;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FQLCongViec : Form
    {
        string tieuDe;
        public UCQLCongViec uc;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        string Email = StateStorage.GetInstance().SharedValue;
        FThongBao_LuaChon tb;
        FDangTinTuyenDung fDangTinTuyenDung;
        FGiaoDienCongTy fGiaoDienCongTy;

        public FQLCongViec()
        {
            InitializeComponent();
 

        }

        private void XemTin(string tieuDe)
        {
            
            FThongTinTuyenDung tttd = new FThongTinTuyenDung(Email, tieuDe);
            tttd.DongForm += QuayLai;
            MoFormCon(tttd, plFormCha);
            
        }
       

        private void FQLCongViec_Load(object sender, EventArgs e)
        {
            //sqlStr = "Select TieuDe, TenCongViec, CapBac, NgayDang, NgayHetHan, SoLuongTuyen from DangTinTuyenDung Where Email = @Email Order by NgayDang DESC";

            sqlStr = "SELECT TieuDe, TenCongViec, CapBac, NgayDang, NgayHetHan, SoLuongTuyen, (SELECT COUNT(*) FROM UngTuyen WHERE TieuDe = DangTinTuyenDung.TieuDe) AS SoHoSo FROM DangTinTuyenDung WHERE Email = @Email ORDER BY NgayDang DESC";

            
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tieuDe = reader["TieuDe"].ToString();
                                string tenCV = reader["TenCongViec"].ToString();
                                string viTri = reader["CapBac"].ToString(); // Đây có phải là vị trí công việc không? Nếu không, bạn cần sửa lại tên cột
                                DateTime ngayDang = reader.GetDateTime(reader.GetOrdinal("NgayDang"));
                                DateTime ngayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"));
                                
                                int slConLai = Convert.ToInt32(1);
                                int slHoSo = Convert.ToInt32(reader["SoHoSo"]);
                                themUCQLCongViec(tieuDe,tenCV, viTri, ngayDang, ngayHetHan,slConLai, slHoSo);
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

        private void Xoa(object sender, EventArgs e)
        {
            tb.Close();
            sqlStr = "DELETE FROM DangTinTuyenDung WHERE Email = @Email AND TieuDe = @TieuDe";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        // Assuming you have parameters to set for your query
                        command.Parameters.AddWithValue("@tieuDe", tieuDe);
                        command.Parameters.AddWithValue("@Email", Email);
                       

                        // Check if rows were affected
                        if (command.ExecuteNonQuery() > 0)
                        {
                            FThongBao tbao = new FThongBao();
                            tbao.rtbThongBao.Text = "Xóa thành công";
                            tbao.Show();
                            plUC.Controls.Clear();
                            FQLCongViec_Load(sender, e); // Gọi sự kiện load dữ liệu sau khi xóa
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

        private void XoaTin(string tieuDe)
        {
            tb = new FThongBao_LuaChon();
            tb.rtbThongBao.Text = "Bạn muốn xóa tin này?";
            tb.Show();
            this.tieuDe = tieuDe;
            tb.ChonButtonCo += Xoa;
        }

        
        private void themUCQLCongViec (string tieuDe, string tenCV, string viTri, DateTime ngayDang, DateTime ngayHetHan,int slConLai, int slHoSo )
        {
            UCQLCongViec uc = new UCQLCongViec();
            uc.Dock = DockStyle.Top;
            uc.btnTieuDe.Text = tieuDe;
            uc.txtTenCV.Text = tenCV;
            uc.txtViTri.Text = viTri;
            uc.txtNgayDang.Text = ngayDang.Day + "/" + ngayDang.Month + "/" + ngayDang.Year;
            uc.txtNgayHetHan.Text = ngayHetHan.Day + "/" + ngayHetHan.Month + "/" + ngayHetHan.Year;
            uc.txtSLConLai.Text = slConLai.ToString();
            uc.txtSLHoSo .Text = slHoSo.ToString();
            uc.ChonButtonXemTin += XemTin;
            uc.ChonButtonXoa += XoaTin;
            uc.ChonButtonXemHS += XemHS;
            uc.ChonButtonSua += SuaTin;

            //uc.ChonButtonXemHS += (sender, e) => XemHS(tieuDe);
            plUC.Controls.Add(uc);

        }
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
        private void SuaTin(string tieuDe)
        {
            openFormCon(new FDangTinTuyenDung());

        }
        private void QuayLai(object sender, EventArgs e)
        {
            MoFormCon(new FQLCongViec(), plFormCha);
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
            pl.Controls.Clear();
            pl.Controls.Add(fCon);
            pl.Tag = fCon;
            fCon.BringToFront();
            fCon.Show();
        }

        private void XemHS(string tieuDe)
        {

            FQuanLyHoSo fQuanLyHoSo = new FQuanLyHoSo(Email, tieuDe);
            fQuanLyHoSo.QuayLai += QuayLai;
            MoFormCon(fQuanLyHoSo, plFormCha);
        }

        //Tìm Kiếm theo tên CV
        private void LoadUCQLCongViecTheoTenCV(string tenCongViec)
        {
            // Xóa tất cả các UserControl hiện có trước khi load UserControl mới
            plUC.Controls.Clear();

            sqlStr = "SELECT TieuDe, TenCongViec, CapBac, NgayDang, NgayHetHan, SoLuongTuyen FROM DangTinTuyenDung WHERE Email = @Email AND TenCongViec LIKE @TenCongViec";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@TenCongViec", "%" + tenCongViec + "%"); // Tìm kiếm một phần của tên công việc

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                string tieuDe = reader["TieuDe"].ToString();
                                string tenCV = reader["TenCongViec"].ToString();
                                string viTri = reader["CapBac"].ToString(); // Đây có phải là vị trí công việc không? Nếu không, bạn cần sửa lại tên cột
                                DateTime ngayDang = reader.GetDateTime(reader.GetOrdinal("NgayDang"));
                                DateTime ngayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"));
                                int slTuyen = Convert.ToInt32(reader["SoLuongTuyen"]);
                                int slConLai = Convert.ToInt32(1);
                                themUCQLCongViec(tieuDe, tenCV, viTri, ngayDang, ngayHetHan, slTuyen, slConLai);
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

            // Gọi phương thức lọc với tháng và năm đã chọn
            LocTheoThangVaNam(thangLoc, namLoc);
        }
        private void LocTheoThangVaNam(int thangLoc, int namLoc)
        {
            // Xóa tất cả các UserControl hiện có trên Form trước khi lọc
            plUC.Controls.Clear();

            // Kiểm tra xem combobox tháng và năm có dữ liệu không
            bool locTheoThang = (cbbThang.SelectedIndex != -1);
            bool locTheoNam = (cbbNam.SelectedIndex != -1);

            // Thực hiện truy vấn để lấy các tin đăng thỏa mãn điều kiện
            string sqlStr = "";
            if (locTheoThang && locTheoNam)
            {
                // Lọc theo cả tháng và năm
                sqlStr = "SELECT TieuDe, TenCongViec, CapBac, NgayDang, NgayHetHan, SoLuongTuyen FROM DangTinTuyenDung WHERE Email = @Email AND MONTH(NgayDang) = @ThangLoc AND YEAR(NgayDang) = @NamLoc ORDER BY NgayDang DESC";
            }
            else if (locTheoThang && !locTheoNam)
            {
                // Lọc theo tháng
                sqlStr = "SELECT TieuDe, TenCongViec, CapBac, NgayDang, NgayHetHan, SoLuongTuyen FROM DangTinTuyenDung WHERE Email = @Email AND MONTH(NgayDang) = @ThangLoc ORDER BY NgayDang DESC";
            }
            else if (!locTheoThang && locTheoNam)
            {
                // Lọc theo năm
                sqlStr = "SELECT TieuDe, TenCongViec, CapBac, NgayDang, NgayHetHan, SoLuongTuyen FROM DangTinTuyenDung WHERE Email = @Email AND YEAR(NgayDang) = @NamLoc ORDER BY NgayDang DESC";
            }
            else
            {

                sqlStr = "SELECT TieuDe, TenCongViec, CapBac, NgayDang, NgayHetHan, SoLuongTuyen FROM DangTinTuyenDung WHERE Email = @Email ORDER BY NgayDang DESC";
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
                                string tieuDe = reader["TieuDe"].ToString();
                                string tenCV = reader["TenCongViec"].ToString();
                                string viTri = reader["CapBac"].ToString();
                                DateTime ngayDang = reader.GetDateTime(reader.GetOrdinal("NgayDang"));
                                DateTime ngayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"));
                                int slTuyen = Convert.ToInt32(reader["SoLuongTuyen"]);
                                int slConLai = Convert.ToInt32(1);
                                int slHoSo = Convert.ToInt32(1);
                                themUCQLCongViec(tieuDe,tenCV,  viTri, ngayDang, ngayHetHan, slConLai, slHoSo);
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

        private void cbbThang_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                while (cbbThang.SelectedItem != null)
                {
                    cbbThang.Items.Remove(cbbThang.SelectedItem);
                }
            }
        }

        private void cbbNam_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                while (cbbNam.SelectedItem != null)
                {
                    cbbNam.Items.Remove(cbbNam.SelectedItem);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            string tenCVTimKiem = txtTimKiemCV.Text;
            LoadUCQLCongViecTheoTenCV(tenCVTimKiem);
        }

       
    }
}
