using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FThongTinCongTy : Form
    {

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string Email = StateStorage.GetInstance().SharedValue.ToString();
        string sqlStr;
        public FThongTinCongTy()
        {
            InitializeComponent();
        }


        //Kiểm tra các control đã đầy
        public bool AreAllControlsFilled()
        {


            // Kiểm tra xem tất cả các TextBox có dữ liệu không
            if (
                string.IsNullOrWhiteSpace(txtNguoiDungDau.Text) ||
                string.IsNullOrWhiteSpace(txtMaSoThue.Text) ||
                string.IsNullOrWhiteSpace(txtTenCongTy.Text) ||
                string.IsNullOrWhiteSpace(cbbQuyMo.Text) ||
                string.IsNullOrWhiteSpace(txtSDT.Text) ||
                string.IsNullOrWhiteSpace(txtDuong.Text) ||
                string.IsNullOrWhiteSpace(cbbXa.Text) ||
                string.IsNullOrWhiteSpace(cbbHuyen.Text) ||
                string.IsNullOrWhiteSpace(cbbTinh.Text) ||
                //string.IsNullOrWhiteSpace(txtDiaChi.Text) ||
                string.IsNullOrWhiteSpace(txtMoTa.Text) ||
                string.IsNullOrWhiteSpace(txtChinhSach.Text) ||
                string.IsNullOrWhiteSpace(txtCoHoi.Text) ||
                string.IsNullOrWhiteSpace(txtLuong.Text) ||
                string.IsNullOrWhiteSpace(txtNguoiLienHe.Text)
                )
            { return false; }
            else
                return true;

        }

     

        private void FThongTinCongTy_Load(object sender, EventArgs e)
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
                        string diaChi = reader["DiaChi"].ToString();
                        string[] diaChiParts = diaChi.Split(',');
                        txtTenCongTy.Text = reader["Ten"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        dtpNgayThanhLap.Value = Convert.ToDateTime(reader["NgayThanhLap"]);
                        txtSDT.Text = reader["SDT"].ToString();
                        cbbQuyMo.Text = reader["QuyMo"].ToString();


                        // Gán giá trị cho các control tương ứng

                        if (diaChiParts.Length >= 4)
                        {
                            txtDuong.Text = diaChiParts[0].ToString().Trim();
                            cbbXa.Text = diaChiParts[1].ToString().Trim();
                            cbbHuyen.Text = diaChiParts[2].ToString().Trim();
                            cbbTinh.Text = diaChiParts[3].ToString().Trim();
                        }

                        ////
                        txtDiaChi.Text = reader["DiaChi"].ToString();
                        //
                        txtMoTa.Text = reader["MoTa"].ToString();
                        txtNguoiDungDau.Text = reader["NguoiDungDau"].ToString();
                        txtMaSoThue.Text = reader["MaSoThue"].ToString();
                        //GiayPhep
                        if (!(reader["GiayPhepKinhDoanh"] is DBNull))
                        {
                            byte[] imageData = (byte[])reader["GiayPhepKinhDoanh"];
                            pBGiayPhep.Image = ByteArrayToImage(imageData);
                            //lblXemGiayPhep.Visible = true;
                        }
                        else
                        {
                            // Hiển thị lblXemGiayPhep nếu không có hình ảnh
                            //lblXemGiayPhep.Visible = false;
                        }
                        //
                        txtChinhSach.Text = reader["ChinhSachPT"].ToString();
                        txtCoHoi.Text = reader["CoHoiTT"].ToString();
                        txtLuong.Text = reader["CSLuongThuong"].ToString();
                        txtNguoiLienHe.Text = reader["NguoiLienLac"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { conn.Close(); }

        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

       
        private Image cachedImage = null;


        private byte[] ImageToByteArray(Image img)
        {
            MemoryStream m = new MemoryStream();
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            return m.ToArray();
        }

        // Phương thức chuyển đổi hình ảnh sang mảng byte
      
        private void UpdateOtherInformation()
        {
            // Cập nhật thông tin khác ở đây

            // Kiểm tra nếu có ảnh đã được chọn trước đó
            if (cachedImage != null)
            {
                // Sử dụng ảnh đã lưu trong cachedImage mà không cần cập nhật lại
                // Do cachedImage vẫn giữ ảnh nguyên trạng
                // Ví dụ: Update thông tin khác mà không làm thay đổi ảnh
            }
        }

        private void cbbHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbbTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTinh = cbbTinh.SelectedItem.ToString();

            // Tạo một danh sách tạm thời để lưu các quận/huyện tương ứng với tỉnh/thành phố được chọn
            List<string> filteredHuyen = new List<string>();

            // Lọc các quận/huyện từ danh sách đã có trong cbbHuyen
            switch (selectedTinh)
            {
                case "Hà Nội":
                    filteredHuyen.AddRange(new string[] { "Ba Đình", "Hoàn Kiếm", "Hai Bà Trưng", "Đống Đa", "Tây Hồ" });
                    break;
                case "TP HCM":
                    filteredHuyen.AddRange(new string[] { "Quận 1", "Quận 2", "Quận 3", "Quận 4", "Quận 5" });
                    break;
                case "Hải Phòng":
                    filteredHuyen.AddRange(new string[] { "Hồng Bàng", "Ngô Quyền", "Lê Chân", "Hải An", "Kiến An" });
                    break;
                case "Đà Nẵng":
                    filteredHuyen.AddRange(new string[] { "Hải Châu", "Thanh Khê", "Liên Chiểu", "Ngũ Hành Sơn", "Sơn Trà" });
                    break;
                // Thêm các case khác cho các tỉnh/thành phố khác ở đây
                default:
                    break;
            }

            // Xóa tất cả các mục hiện có trong cbbHuyen

            // Thêm các quận/huyện đã lọc vào cbbHuyen
            cbbHuyen.Items.AddRange(filteredHuyen.ToArray());
        }

        private void pBGiayPhep_Click(object sender, EventArgs e)
        {
            /// Kiểm tra xem PictureBox có hình ảnh không
            if (pBGiayPhep.Image != null)
            {

                // Tạo một form mới để hiển thị hình ảnh to ra
                Form viewImageForm = new Form();
                viewImageForm.Text = "Hình ảnh Giấy phép kinh doanh";
                viewImageForm.StartPosition = FormStartPosition.CenterScreen;
                viewImageForm.Size = pBGiayPhep.Image.Size; // Thiết lập kích thước của form mới bằng kích thước của hình ảnh

                // Tạo PictureBox trên form mới và gán hình ảnh từ pBGiayPhep
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = pBGiayPhep.Image;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // Hiển thị hình ảnh tự động tỉ lệ
                pictureBox.Dock = DockStyle.Fill; // Đặt PictureBox để lấp đầy toàn bộ kích thước của form
                pictureBox.Click += (s, ev) => { viewImageForm.Close(); }; // Đóng form khi click vào hình ảnh

                // Thêm PictureBox vào form
                viewImageForm.Controls.Add(pictureBox);

                // Hiển thị form mới
                viewImageForm.ShowDialog();
            }
        }

        private void btnTaiGiayPhep_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lưu ảnh vào biến cachedImage
                cachedImage = Image.FromFile(openFileDialog.FileName);
                pBGiayPhep.Image = cachedImage;
                this.Text = openFileDialog.FileName;
            }
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            if (!AreAllControlsFilled())
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin công ty trước khi cập nhật.");
                return;
            }
            if (AreAllControlsFilled())
            {


                FXacNhanThongTinCTy fXacNhanThongTinCTy = new FXacNhanThongTinCTy();
                fXacNhanThongTinCTy.Show();
                if (FXacNhanThongTinCTy.btnCoClicked)
                {
                    try
                    {
                        conn.Open();
                        byte[] imageData = ImageToByteArray(pBGiayPhep.Image); // Chuyển đổi hình ảnh sang mảng byte

                        //    string sqlStr = string.Format("UPDATE ThongTinCTy SET Ten='{0}', NgayThanhLap = '{1}', QuyMo = '{2}', " +
                        //"SDT = '{3}',  DiaChi = '{4}', MoTa = '{5}',NguoiDungDau = '{6}', MaSoThue = '{7}', " +
                        //"GiayPhepKinhDoanh = @ImageData, ChinhSachPT = '{8}', CoHoiTT = '{9}', CSLuongThuong = '{10}', " +
                        //"NguoiLienLac = '{11}'  WHERE Email = '{12}'  ", txtTenCongTy.Text, new DateTime(dtpNgayThanhLap.Value.Year,
                        //dtpNgayThanhLap.Value.Month, dtpNgayThanhLap.Value.Day), cbbQuyMo.Text, txtSDT.Text, txtDuong.Text
                        //+ ", " + cbbXa.Text + ", " + cbbHuyen.Text + ", " + cbbTinh.Text, txtMoTa.Text, txtNguoiDungDau.Text,
                        //txtMaSoThue.Text, txtChinhSach.Text, txtCoHoi.Text, txtLuong.Text, txtNguoiLienHe.Text, txtEmail.Text);
                        //    SqlCommand cmd = new SqlCommand(sqlStr, conn);
                        //    cmd.Parameters.AddWithValue("@ImageData", imageData); // Thêm dữ liệu hình ảnh vào tham số SQL
                        //    //SqlCommand cmd = new SqlCommand(sqlStr, conn);
                        //    if (cmd.ExecuteNonQuery() > 0)
                        //        MessageBox.Show("Cap nhat thanh cong");

                        string sqlStr = "UPDATE ThongTinCTy SET " +
                                            "Ten = @Ten, " +
                                            "NgayThanhLap = @NgayThanhLap, " +
                                            "QuyMo = @QuyMo, " +
                                            "SDT = @SDT, " +
                                            "DiaChi = @DiaChi, " +
                                            "MoTa = @MoTa, " +
                                            "NguoiDungDau = @NguoiDungDau, " +
                                            "MaSoThue = @MaSoThue, " +
                                            "GiayPhepKinhDoanh = @GiayPhepKinhDoanh, " +
                                            "ChinhSachPT = @ChinhSachPT, " +
                                            "CoHoiTT = @CoHoiTT, " +
                                            "CSLuongThuong = @CSLuongThuong, " +
                                            "NguoiLienLac = @NguoiLienLac " +
                                            "WHERE Email = @Email";


                        SqlCommand cmd = new SqlCommand(sqlStr, conn);

                        //cmd.Parameters.AddWithValue("@ImageData", imageData);

                        cmd.Parameters.AddWithValue("@Ten", txtTenCongTy.Text);
                        cmd.Parameters.AddWithValue("@NgayThanhLap", dtpNgayThanhLap.Value.Date);
                        cmd.Parameters.AddWithValue("@QuyMo", cbbQuyMo.Text);
                        cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                        cmd.Parameters.AddWithValue("@DiaChi", txtDuong.Text + ", " + cbbXa.Text + ", " + cbbHuyen.Text + ", " + cbbTinh.Text);
                        cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                        cmd.Parameters.AddWithValue("@NguoiDungDau", txtNguoiDungDau.Text);
                        cmd.Parameters.AddWithValue("@MaSoThue", txtMaSoThue.Text);
                        cmd.Parameters.AddWithValue("@GiayPhepKinhDoanh", imageData); // Giả sử imageData là mảng byte của hình ảnh

                        cmd.Parameters.AddWithValue("@ChinhSachPT", txtChinhSach.Text);
                        cmd.Parameters.AddWithValue("@CoHoiTT", txtCoHoi.Text);
                        cmd.Parameters.AddWithValue("@CSLuongThuong", txtLuong.Text);
                        cmd.Parameters.AddWithValue("@NguoiLienLac", txtNguoiLienHe.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật thành công!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cập nhật thất bại!" + ex);
                    }
                    finally
                    {
                        conn.Close();

                    }

                    FThongTinCongTy_Load(sender, e);
                }
            }
        }
    }
}


