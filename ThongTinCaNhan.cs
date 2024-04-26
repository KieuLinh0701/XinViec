using Guna.UI2.WinForms;
using System;
using System.Collections;
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
using System.Xml.Linq;
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class ThongTinCaNhan : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString(); 
        public event EventHandler MoXemThongTinCaNhan;
        string sqlStr;
        string hoTen, SDT;
        int active = 1;
        DAO dao = new DAO();

        public ThongTinCaNhan(string hoTen, string SDT)
        {
            InitializeComponent();
            this.hoTen = hoTen;
            this.SDT = SDT;
            ptbAnh.Image = Properties.Resources.anhttcn_macdinh;
        }

        public ThongTinCaNhan()
        {
            active = 0;
            InitializeComponent();
        }
        private string NhapDiaChi(ucDChi ucdc)
        {
            string dc;
            dc = ucdc.txbDuong.Text + ", " + ucdc.cbbXa.Text + ", " + ucdc.cbbHuyen.Text + ", " + ucdc.cbbTinh.Text;
            return dc;
        }

        private void Load_TonGiao()
        {
            cbbTonGiao.Items.Clear();
            sqlStr = "SELECT DISTINCT tenTonGiao FROM TonGiao";

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
                                string value = reader["tenTonGiao"].ToString();
                                cbbTonGiao.Items.Add(value);
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

        private void Load_DanToc()
        {
            cbbDanToc.Items.Clear();
            sqlStr = "SELECT DISTINCT tenDanToc FROM DanToc"; 

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
                                string value = reader["tenDanToc"].ToString();
                                cbbDanToc.Items.Add(value);
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


        private void Load_TrinhDoVanHoa()
        {
            cbbTrinhDoVanHoa.Items.Clear(); // Xóa dữ liệu cũ trước khi load dữ liệu mới
            sqlStr = "SELECT DISTINCT ten FROM TrinhDoVanHoa";

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
                                string value = reader["ten"].ToString();
                                cbbTrinhDoVanHoa.Items.Add(value);
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

        private void ThongTinCaNhan_Load(object sender, EventArgs e)
        {
            Load_TrinhDoVanHoa();
            Load_NoiCapCCCD();
            Load_DanToc();
            Load_TonGiao();
            Load_Tinh(cbbNoiSinh);
            Load_Tinh(cbbNguyenQuan);
            ucDiaChi_ThuongTru.groupBox.Text = "Nơi đăng ký hộ khẩu thường trú";
            if (active == 0)
            {
                sqlStr = string.Format("SELECT * FROM UngVien WHERE EmailDangNhap = '{0}'", EmailDangNhap);
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sqlStr, conn);
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
                            txbHoTen.Text = reader["hoTen"].ToString();
                            cbbGioiTinh.Text = reader["gioiTinh"].ToString();
                            dtpNgaySinh.Value = reader.GetDateTime(reader.GetOrdinal("ngaySinh"));
                            cbbNoiSinh.Text = reader["noiSinh"].ToString();
                            cbbNguyenQuan.Text = reader["nguyenQuan"].ToString();
                            txbSDT.Text = reader["SDT"].ToString();
                            txbEmail.Text = reader["Email"].ToString();
                            cbbDanToc.Text = reader["danToc"].ToString();
                            cbbTonGiao.Text = reader["tonGiao"].ToString();
                            cbbTrinhDoVanHoa.Text = reader["trinhDoVanHoa"].ToString();
                            txbtenTruong.Text = reader["truong"].ToString();
                            txbChuyenNganh.Text = reader["chuyenNganh"].ToString();
                            rtbKhac.Text = reader["khac"].ToString();
                            rtbTrinhDoNgoaiNgu.Text = reader["trinhDoNgoaiNgu"].ToString();
                            rtbTrinhDoChuyenMon.Text = reader["trinhDoChuyenMon"].ToString();
                            txbCCCD.Text = reader["CCCD"].ToString();
                            dtpNgayCapCCCD.Value = reader.GetDateTime(reader.GetOrdinal("ngayCapCCCD"));
                            cbbNoiCapCCCD.Text = reader["noiCapCCCD"].ToString();


                            ucDiaChi_ThuongTru.cbbTinh.Text = reader["Tinh_ThuongTru"].ToString();
                            ucDiaChi_ThuongTru.cbbHuyen.Text = reader["Huyen_ThuongTru"].ToString();
                            ucDiaChi_ThuongTru.cbbXa.Text = reader["Xa_ThuongTru"].ToString();
                            ucDiaChi_ThuongTru.txbDuong.Text = reader["Duong_ThuongTru"].ToString();
                            ucDiaChi_HienNay.cbbTinh.Text = reader["Tinh_HienNay"].ToString();
                            ucDiaChi_HienNay.cbbHuyen.Text = reader["Huyen_HienNay"].ToString();
                            ucDiaChi_HienNay.cbbXa.Text = reader["Xa_HienNay"].ToString();
                            ucDiaChi_HienNay.txbDuong.Text = reader["Duong_HienNay"].ToString();


                            txbHoTen_NT.Text = reader["hoTenNT"].ToString();
                            txbSDT_NT.Text = reader["SDTNT"].ToString();
                            txbEmail_NT.Text = reader["EmailNT"].ToString();


                            ucDiaChi_HienNay_NguoiThan.cbbTinh.Text = reader["Tinh_NguoiThan"].ToString();
                            ucDiaChi_HienNay_NguoiThan.cbbHuyen.Text = reader["Huyen_NguoiThan"].ToString();
                            ucDiaChi_HienNay_NguoiThan.cbbXa.Text = reader["Xa_NguoiThan"].ToString();
                            ucDiaChi_HienNay_NguoiThan.txbDuong.Text = reader["Duong_NguoiThan"].ToString();
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
            else
            {
                txbHoTen.Text = hoTen;
                txbSDT.Text = SDT;
            }
            plTrinhDoDaiHoc.Visible = false;
        }

        private void Load_NoiCapCCCD()
        {
            cbbNoiCapCCCD.Items.Clear();
            sqlStr = "SELECT DISTINCT noiCap FROM NoiCapCCCD"; 

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
                                string value = reader["noiCap"].ToString();
                                cbbNoiCapCCCD.Items.Add(value);
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

        private void Load_Tinh(ComboBox cbbTinh)
        {
            cbbTinh.Items.Clear();
            sqlStr = "SELECT DISTINCT tenTinh FROM Tinh"; // Thay đổi YourTableName thành tên bảng thích hợp

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
                                string value = reader["tenTinh"].ToString();
                                cbbTinh.Items.Add(value);
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
        private void cbbTrinhDoVanHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTrinhDoVanHoa.Text == "Đại học")
            {
                plTrinhDoDaiHoc.Visible = true;
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            plTrinhDoDaiHoc.Visible = false;
        }

        private UngVien TaoUngVien()
        {
            UngVien uv = new UngVien(txbHoTen.Text, cbbGioiTinh.Text, dtpNgaySinh.Value,
                cbbNoiSinh.Text, cbbNguyenQuan.Text, ucDiaChi_ThuongTru.cbbTinh.Text,
                ucDiaChi_ThuongTru.cbbHuyen.Text, ucDiaChi_ThuongTru.cbbXa.Text,
                ucDiaChi_ThuongTru.txbDuong.Text, ucDiaChi_HienNay.cbbTinh.Text,
                ucDiaChi_HienNay.cbbHuyen.Text, ucDiaChi_HienNay.cbbXa.Text,
                ucDiaChi_HienNay.txbDuong.Text, txbSDT.Text, txbEmail.Text, cbbDanToc.Text,
                cbbTonGiao.Text, txbCCCD.Text, dtpNgayCapCCCD.Value, cbbNoiCapCCCD.Text,
                cbbTrinhDoVanHoa.Text, txbtenTruong.Text, txbChuyenNganh.Text, rtbKhac.Text,
                rtbTrinhDoNgoaiNgu.Text, rtbTrinhDoChuyenMon.Text, txbHoTen_NT.Text, txbSDT_NT.Text,
                txbEmail_NT.Text, ucDiaChi_HienNay_NguoiThan.cbbTinh.Text,
                ucDiaChi_HienNay_NguoiThan.cbbHuyen.Text, ucDiaChi_HienNay_NguoiThan.cbbXa.Text,
                ucDiaChi_HienNay_NguoiThan.txbDuong.Text, EmailDangNhap);
            return uv;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            //UngVien uv = TaoUngVien();
            //UngVienDAO ungVienDAO = new UngVienDAO();
            //byte[] imageData = ImageToByteArray(ptbAnh.Image);
            //if (ungVienDAO.CapNhatThongTinUngVien(uv, imageData) > 0)
            //{
            //    if (active == 0)
            //    {
            //        dao.ThongBao("Thay đổi thành công");
            //        ThongTinCaNhan_Load(sender, e);
            //    }
            //    else
            //    {
            //        this.Close();
            //        GDNguoiXinViec gDNguoiXinViec = new GDNguoiXinViec();
            //        gDNguoiXinViec.Show();
            //    }
            //}
            //else
            //{
            //    dao.BaoLoi("Thay đổi thất bại");
            //}

            if (KiemTraCacGiaTriDaDienDu() == true)
            {
                sqlStr = string.Format("UPDATE UngVien SET Anh = @Anh, hoTen = @hoTen, gioiTinh = @gioiTinh, ngaySinh = @ngaySinh, " +
                    "noiSinh = @noiSinh, nguyenQuan = @nguyenQuan, Tinh_ThuongTru = @Tinh_ThuongTru, Huyen_ThuongTru = @Huyen_ThuongTru, " +
                    "Xa_ThuongTru = @Xa_ThuongTru, Duong_ThuongTru = @Duong_ThuongTru, Tinh_HienNay = @Tinh_HienNay, Huyen_HienNay = @Huyen_HienNay, " +
                    "Xa_HienNay = @Xa_HienNay, Duong_HienNay = @Duong_HienNay, SDT = @SDT, Email = @Email, " +
                    "danToc = @danToc, tonGiao = @tonGiao, CCCD = @CCCD, ngayCapCCCD = @ngayCapCCCD, " +
                    "noiCapCCCD = @noiCapCCCD, trinhDoVanHoa = @trinhDoVanHoa, Truong = @Truong, ChuyenNganh = @ChuyenNganh, " +
                    "Khac = @Khac, trinhDoNgoaiNgu = @trinhDoNgoaiNgu, trinhDoChuyenMon = @trinhDoChuyenMon, hoTenNT = @hoTenNT, " +
                    "SDTNT = @SDTNT, EmailNT = @EmailNT, Tinh_NguoiThan = @Tinh_NguoiThan, Huyen_NguoiThan = @Huyen_NguoiThan, " +
                    "Xa_NguoiThan = @Xa_NguoiThan, Duong_NguoiThan = @Duong_NguoiThan WHERE EmailDangNhap = @EmailDangNhap");
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            byte[] imageData = ImageToByteArray(ptbAnh.Image);
                            command.Parameters.AddWithValue("@Anh", imageData);
                            command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                            command.Parameters.AddWithValue("@hoTen", txbHoTen.Text);
                            command.Parameters.AddWithValue("@gioiTinh", cbbGioiTinh.Text);
                            command.Parameters.AddWithValue("@ngaySinh", dtpNgaySinh.Value);
                            command.Parameters.AddWithValue("@noiSinh", cbbNoiSinh.Text);
                            command.Parameters.AddWithValue("@nguyenQuan", cbbNguyenQuan.Text);


                            command.Parameters.AddWithValue("@Tinh_ThuongTru", ucDiaChi_ThuongTru.cbbTinh.Text);
                            command.Parameters.AddWithValue("@Huyen_ThuongTru", ucDiaChi_ThuongTru.cbbHuyen.Text);
                            command.Parameters.AddWithValue("@Xa_ThuongTru", ucDiaChi_ThuongTru.cbbXa.Text);
                            command.Parameters.AddWithValue("@Duong_ThuongTru", ucDiaChi_ThuongTru.txbDuong.Text);
                            command.Parameters.AddWithValue("@Tinh_HienNay", ucDiaChi_HienNay.cbbTinh.Text);
                            command.Parameters.AddWithValue("@Huyen_HienNay", ucDiaChi_HienNay.cbbHuyen.Text);
                            command.Parameters.AddWithValue("@Xa_HienNay", ucDiaChi_HienNay.cbbXa.Text);
                            command.Parameters.AddWithValue("@Duong_HienNay", ucDiaChi_HienNay.txbDuong.Text);


                            command.Parameters.AddWithValue("@SDT", txbSDT.Text);
                            command.Parameters.AddWithValue("@Email", txbEmail.Text);
                            command.Parameters.AddWithValue("@danToc", cbbDanToc.Text);
                            command.Parameters.AddWithValue("@tonGiao", cbbTonGiao.Text);
                            command.Parameters.AddWithValue("@CCCD", txbCCCD.Text);
                            command.Parameters.AddWithValue("@ngayCapCCCD", dtpNgayCapCCCD.Value);
                            command.Parameters.AddWithValue("@noiCapCCCD", cbbNoiCapCCCD.Text);
                            command.Parameters.AddWithValue("@trinhDoVanHoa", cbbTrinhDoVanHoa.Text);
                            command.Parameters.AddWithValue("@Truong", txbtenTruong.Text);
                            command.Parameters.AddWithValue("@ChuyenNganh", txbChuyenNganh.Text);
                            command.Parameters.AddWithValue("@Khac", rtbKhac.Text);
                            command.Parameters.AddWithValue("@trinhDoNgoaiNgu", rtbTrinhDoNgoaiNgu.Text);
                            command.Parameters.AddWithValue("@trinhDoChuyenMon", rtbTrinhDoChuyenMon.Text);
                            command.Parameters.AddWithValue("@hoTenNT", txbHoTen_NT.Text);
                            command.Parameters.AddWithValue("@SDTNT", txbSDT_NT.Text);
                            command.Parameters.AddWithValue("@EmailNT", txbEmail_NT.Text);


                            command.Parameters.AddWithValue("@Tinh_NguoiThan", ucDiaChi_HienNay_NguoiThan.cbbTinh.Text);
                            command.Parameters.AddWithValue("@Huyen_NguoiThan", ucDiaChi_HienNay_NguoiThan.cbbHuyen.Text);
                            command.Parameters.AddWithValue("@Xa_NguoiThan", ucDiaChi_HienNay_NguoiThan.cbbXa.Text);
                            command.Parameters.AddWithValue("@Duong_NguoiThan", ucDiaChi_HienNay_NguoiThan.txbDuong.Text);


                            // Execute the INSERT query
                            int k = command.ExecuteNonQuery();

                            // Check if rows were affected
                            if (k > 0)
                            {
                                if (active == 0)
                                {
                                    dao.ThongBao("Thay đổi thành công");
                                    ThongTinCaNhan_Load(sender, e);
                                }
                                else
                                {
                                    this.Close();
                                    FGDNguoiXinViec gDNguoiXinViec = new FGDNguoiXinViec();
                                    gDNguoiXinViec.Show();
                                }
                            }
                            else
                            {
                                dao.BaoLoi("Thay đổi thất bại");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    dao.BaoLoi("Thay đổi thất bại: " + ex.Message);
                }
            }
            else
            {
                dao.BaoLoi("Vui lòng điền đầy đủ thông tin");
            }
            

        }

        private byte[] ImageToByteArray(Image img)
        {
            MemoryStream m = new MemoryStream(); 
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            return m.ToArray();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            MoXemThongTinCaNhan?.Invoke(this, e);
        }

        private void btnTaiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ptbAnh.Image = Image.FromFile(openFileDialog.FileName);
                this.Text = openFileDialog.FileName;
            }
        }

        public bool KiemTraCacGiaTriDaDienDu()
        {
            if (
                string.IsNullOrWhiteSpace(txbHoTen.Text) ||
                string.IsNullOrWhiteSpace(cbbGioiTinh.Text) ||
                string.IsNullOrWhiteSpace(cbbNoiSinh.Text) ||
                string.IsNullOrWhiteSpace(cbbNguyenQuan.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_ThuongTru.cbbTinh.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_ThuongTru.cbbHuyen.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_ThuongTru.cbbXa.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_ThuongTru.txbDuong.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay.cbbTinh.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay.cbbHuyen.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay.cbbXa.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay.txbDuong.Text) ||
                string.IsNullOrWhiteSpace(txbSDT.Text) ||
                string.IsNullOrWhiteSpace(txbEmail.Text) ||
                string.IsNullOrWhiteSpace(cbbDanToc.Text) ||
                string.IsNullOrWhiteSpace(cbbTonGiao.Text) ||
                string.IsNullOrWhiteSpace(txbCCCD.Text) ||
                string.IsNullOrWhiteSpace(cbbNoiCapCCCD.Text) ||
                string.IsNullOrWhiteSpace(cbbTrinhDoVanHoa.Text) ||
                string.IsNullOrWhiteSpace(txbtenTruong.Text) ||
                string.IsNullOrWhiteSpace(txbChuyenNganh.Text) ||
                string.IsNullOrWhiteSpace(rtbKhac.Text) ||
                string.IsNullOrWhiteSpace(rtbTrinhDoNgoaiNgu.Text) ||
                string.IsNullOrWhiteSpace(rtbTrinhDoChuyenMon.Text) ||
                string.IsNullOrWhiteSpace(txbHoTen_NT.Text) ||
                string.IsNullOrWhiteSpace(txbSDT_NT.Text) ||
                string.IsNullOrWhiteSpace(txbEmail_NT.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay_NguoiThan.cbbTinh.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay_NguoiThan.cbbHuyen.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay_NguoiThan.cbbXa.Text) ||
                string.IsNullOrWhiteSpace(ucDiaChi_HienNay_NguoiThan.txbDuong.Text)
                )
            {
                return false; 
            }
            else
                return true;
        }
    }
}
