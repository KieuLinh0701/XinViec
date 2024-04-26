using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using XinViec.XinViec;


namespace XinViec
{
    public partial class FdkUngVien : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        public FdkUngVien()
        {
            InitializeComponent();
        }

        private void lblDangNhap_Click(object sender, EventArgs e)
        {
            this.Close();
            FDangNhap fDangNhap = new FDangNhap();
            fDangNhap.Show();
        }

        private void lbldkNTD_Click(object sender, EventArgs e)
        {
            this.Close();
            FdkNhaTuyenDung fdkNhaTuyenDung = new FdkNhaTuyenDung();
            fdkNhaTuyenDung.Show();
        }

        private void ApplyCenterAlignment()
        {
            rtbDieuKhoan.SelectAll();
            rtbDieuKhoan.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void FdkUngVien_Load(object sender, EventArgs e)
        {
            ApplyCenterAlignment();
            try
            {
                conn.Open();
                string sqlStr = string.Format("SELECT * FROM UngVien");

                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                DataTable dtUngVien = new DataTable();
                adapter.Fill(dtUngVien);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }
        public bool KiemTraTaiKhoan(string tk)//ktra mat khau va ho ten
        {
            return Regex.IsMatch(tk, "^[a-zA-Z0-9]{6,24}$");
        }
        public bool KiemTraEmail(string em)
        {
            return Regex.IsMatch(em,@"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }    
        ModifyTK modify = new ModifyTK();   
        private void btnDangKyUngVien_Click(object sender, EventArgs e)
        {
          
            string matKhau = txtMatKhau.Text;
            string xacNhanMatKhau = txtXacNhanMatKhau.Text;
            string email = txtEmail.Text;
            string vaiTro = "Ung Vien";
            string tenUngVien = txtHoTen.Text;
            DateTime ngaySinh = dtpNgayThangNamSinh.Value;
            string sdt = txtSDT.Text;

            if (!KiemTraTaiKhoan(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 ký tự, với các ký tự chữ và số, chữ hoa và chữ thường!");
                return;
            } 
            if(xacNhanMatKhau!=matKhau)
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu chính xác!");
                return;
            }
            if(!KiemTraEmail(email))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng email!");
                return;
            }   
            if(modify.TKhoans("Select * from TaiKhoan where Email = ' " + email+"'").Count != 0)
            {
                MessageBox.Show("Email này đã được đăng ký, vui lòng đăng kí email khác!");
                return; 
            }    
            try
            {
                //Linh
                conn.Open();
                string query = "Insert into TaiKhoan values ('" + email + "', '" + matKhau + "','" + vaiTro + "'  )";
                modify.Command(query);
                query = "Insert into UngVien (EmailDangNhap) values ('" + email + "')";
                modify.Command(query);
                MessageBox.Show("Đăng ký thành công. Vui lòng nhập thông tin cá nhân");
                StateStorage.GetInstance().SharedValue = email;
                ThongTinCaNhan ttcn = new ThongTinCaNhan(txtHoTen.Text, txtSDT.Text);
                ttcn.FormBorderStyle = FormBorderStyle.FixedSingle;
                ttcn.StartPosition = FormStartPosition.CenterScreen;
                ttcn.Text = "Thông tin cá nhân";
                this.Close();
                ttcn.Show();
                //Linh

            }
            catch
            {
                MessageBox.Show("Tài khoản này đã tồn tại");
            }
            finally
            {
                conn.Close();


            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '*')
            {
                txtMatKhau.PasswordChar = '\0'; // Show password

            }
            else
            {
                txtMatKhau.PasswordChar = '*'; // Hide password

            }
        }
        private void btnHienThi1_Click(object sender, EventArgs e)
        {
            if (txtXacNhanMatKhau.PasswordChar == '*')
            {
                txtXacNhanMatKhau.PasswordChar = '\0'; // Show password

            }
            else
            {
                txtXacNhanMatKhau.PasswordChar = '*'; // Hide password

            }
        }
    }
}
