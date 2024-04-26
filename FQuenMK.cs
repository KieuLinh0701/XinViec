using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    public partial class FQuenMK : Form
    {
        public FQuenMK()
        {
            InitializeComponent();
            lblKetQua.Text = "";
        }
        ModifyTK modify = new ModifyTK();

        private void btnLayLaiMatKhau_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if(email.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập email đăng kí!");
            }
            else
            {
                string query = "Select * from TaiKhoan where Email = '" + email + "'";
                if(modify.TKhoans(query).Count() != 0 ) 
                {
                    lblKetQua.ForeColor = Color.Blue;
                    lblKetQua.Text = "Mật khẩu: "+modify.TKhoans(query)[0].MatKhau;
                }
                else
                {
                    lblKetQua.ForeColor = Color.Red;
                    lblKetQua.Text = "Email này chưa được đăng kí";
                }
            }    
        }

        private void FQuenMK_Load(object sender, EventArgs e)
        {

        }
    }
}
