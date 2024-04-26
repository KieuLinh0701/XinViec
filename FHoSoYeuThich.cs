using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FHoSoYeuThich : Form
    {
        string emailUV;
        string sqlStr;
        string Email = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        public FHoSoYeuThich()
        {
            InitializeComponent();
        }

        private void themUCHoSoYeuThich(string tenUV, string emailCongTy, string gioiTinh, string emailUV, DateTime ngayLuu, Image anh)
        {
            UCCVYeuThich uc = new UCCVYeuThich();
            uc.Dock = DockStyle.Top;
            uc.lblTenUV.Text = tenUV;
            uc.pBAnh.Image = anh;
            uc.lblGioiTinh.Text = gioiTinh;
            uc.lblEmailUV.Text = emailUV;
            uc.lblEmailCongTy.Text = emailCongTy;
            uc.txtNgayLuu.Text = "Ngày lưu: " + ngayLuu.Day + "/" + ngayLuu.Month + "/" + ngayLuu.Year;
            uc.ChonButtonXemChiTiet += XemChiTiet;
            uc.ChonButtonBoLuu += BoLuu;

            plUC.Controls.Add(uc);
        }

        private void BoLuu(string emailUV, string emailCongTy)
        {
            sqlStr = "DELETE FROM HoSoYeuThich WHERE EmailUngVien = @EmailUV AND EmailCongTy = @EmailCongTy";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        // Assuming you have parameters to set for your query
                        command.Parameters.AddWithValue("@EmailUV", emailUV);
                        command.Parameters.AddWithValue("@EmailCongTy", emailCongTy);


                        // Check if rows were affected
                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Đã xóa hồ sơ yêu thích thành công.");
                            plUC.Controls.Clear();
                            FHoSoYeuThich_Load(this, EventArgs.Empty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hồ sơ yêu thích: " + ex.Message);
            }

        }

        private void QuayLai(object sender, EventArgs e)
        {
            MoFormCon(new FHoSoYeuThich(), plFormCha);
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

        private void XemChiTiet(string emailUV)
        {
            FTrangCaNhanNguoiDung trangCaNhan = new FTrangCaNhanNguoiDung(emailUV);
            trangCaNhan.btnQuayLai.Visible = true;
            trangCaNhan.btnPostCongViec.Visible = false;
            trangCaNhan.QuayLai += QuayLai;
            MoFormCon(trangCaNhan, plFormCha);
        }

        private void FHoSoYeuThich_Load(object sender, EventArgs e)
        {
            sqlStr = @"SELECT HoSoYeuThich.*, UngVien.hoTen, UngVien.gioiTinh, UngVien.Anh
                                FROM HoSoYeuThich
                                INNER JOIN UngVien ON HoSoYeuThich.EmailUngVien = UngVien.EmailDangNhap";
            try
            {
                // Kết nối cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();

                    // Truy vấn dữ liệu từ bảng HoSoYeuThich và kết hợp với bảng UngVien để lấy thông tin tên ứng viên

                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Lấy thông tin từ bảng HoSoYeuThich
                                string tenUngVien = reader["hoTen"].ToString();
                                string gioiTinh = reader["gioiTinh"].ToString();
                                string emailUV = reader["EmailUngVien"].ToString();
                                string emailCongTy = reader["EmailCongTy"].ToString();
                                string ngayThem = reader["NgayThem"].ToString();
                                Image anh = null;
                                if (reader["Anh"] != DBNull.Value && reader["Anh"] != null)
                                {
                                    byte[] img = (byte[])reader["Anh"];
                                    anh = dao.ByteArrayToImage(img);
                                }
                                else
                                {
                                    anh = Properties.Resources.anhttcn_macdinh;
                                }

                                // Tạo và thêm UserControl vào Form
                                themUCHoSoYeuThich(tenUngVien, emailCongTy, gioiTinh, emailUV, DateTime.Parse(ngayThem), anh);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi truy xuất dữ liệu
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }
    }

}
