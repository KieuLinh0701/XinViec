using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FDangNhap : Form
    {
        public FDangNhap()
        {
            InitializeComponent();
        }
        ModifyTK modifyTK = new ModifyTK();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string matKhau = txtMatKhau.Text;
            string vaiTro = "";

            if (rbtnUngVien.Checked == true && rbtnNhaTuyenDung.Checked == false)
            {
                vaiTro = "Ung Vien";

            }
            else if (rbtnNhaTuyenDung.Checked && rbtnUngVien.Checked == false)
            {
                vaiTro = "Nha Tuyen Dung";
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại vai trò!");
                return;
            }


            if (email.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập email!");
            }
            else if (matKhau.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
            }
            else
            {
                string query = "Select * from TaiKhoan where Email = '" + email + "' and MatKhau = '" + matKhau + "' and VaiTro = '" + vaiTro + "' ";
                if (modifyTK.TKhoans(query).Count != 0)
                {
                    StateStorage.GetInstance().SharedValue = txtEmail.Text; //Lưu thông tin của email đăng nhập
                    //MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (vaiTro == "Ung Vien")
                    {
                        FGDNguoiXinViec gDNguoiXinViec = new FGDNguoiXinViec();
                        gDNguoiXinViec.Show();
                    }
                    else
                    {
                        FGiaoDienCongTy fGiaoDienCongTy = new FGiaoDienCongTy();
                        if (fGiaoDienCongTy.IsCompanyInfoValid(email))
                        {



                            fGiaoDienCongTy.openFormCon(new FCongTy(email));
                            fGiaoDienCongTy.Show();
                        }
                        else
                        {
                            fGiaoDienCongTy.openFormCon(new FThongTinCongTy());
                            fGiaoDienCongTy.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu hoặc vai trò không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //txtEmail.Clear();
            txtMatKhau.Clear();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            FNhanBiet f1 = new FNhanBiet();

            f1.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FQuenMK fQuenMK = new FQuenMK();
            fQuenMK.ShowDialog();
        }

        private void rbtnUngVien_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void btnHienThi_Click(object sender, EventArgs e)
        {
            //string password = txtMatKhau.Text;
            //string revealedPassword = "";

            //foreach (char c in password)
            //{
            //    if (c == ' ') // Keep spaces as they are
            //    {
            //        revealedPassword += ' ';
            //    }
            //    else // Decrement each character's ASCII value by 1 to reveal
            //    {
            //        char revealedChar = (char)(c - 1);
            //        revealedPassword += revealedChar;
            //    }
            //}

            //MessageBox.Show("Revealed Password: " + revealedPassword, "Revealed Password");
            if (txtMatKhau.PasswordChar == '*')
            {
                txtMatKhau.PasswordChar = '\0'; // Show password

            }
            else
            {
                txtMatKhau.PasswordChar = '*'; // Hide password

            }
        }

        
    }
}
