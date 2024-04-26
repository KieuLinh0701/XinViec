using Guna.UI2.WinForms;
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
using XinViec.Resources;
using XinViec.XinViec;
using static System.Net.Mime.MediaTypeNames;

namespace XinViec
{
    public partial class CaiDatMatKhau : Form
    {
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString();
        private bool clicklan1 = true, click1 = true, click2 = true, click3 = true;
        DAO dao = new DAO();
        public CaiDatMatKhau()
        {
            InitializeComponent();
        }
        private void ptbDoiMK_Click(object sender, EventArgs e)
        {
            if (clicklan1 == true)
            {
                plDoiMK.Visible = true;
                ptbDoiMK.Image = Properties.Resources.CaiDatMatKhau_Dong;
                clicklan1 = false;
            }
            else
            {
                plDoiMK.Visible = false;
                ptbDoiMK.Image = Properties.Resources.CaiDatMatKhau_Mo;
                clicklan1 = true;
                TatplDoiMatKhau();
            }
        }

        private void CaiDatMatKhau_Load(object sender, EventArgs e)
        {
            plDoiMK.Visible = false;
        }

        private void Huy(object sender, EventArgs e)
        {
            dao.tb.Close();
            TatplDoiMatKhau();
        }

        private void TatplDoiMatKhau()
        {
            plDoiMK.Visible = false;
            ptbDoiMK.Image = Properties.Resources.CaiDatMatKhau_Mo;
            clicklan1 = true;
            txbMK_HienTai.Text = "";
            txbMK_Moi.Text = "";
            txbMK_MoiLai.Text = "";
            click1 = true;
            click2 = true;
            click3 = true;
            btnXem1.Image = Properties.Resources.XemMK_DoiTenTK;
            btnXem2.Image = Properties.Resources.XemMK_DoiTenTK;
            btnXem3.Image = Properties.Resources.XemMK_DoiTenTK;
            txbMK_HienTai.UseSystemPasswordChar = true;
            txbMK_Moi.UseSystemPasswordChar = true;
            txbMK_MoiLai.UseSystemPasswordChar = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dao.ThongBao_LuaChon("Bạn muốn hủy thay đổi?", Huy);
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            if (txbMK_HienTai.Text == "" || txbMK_Moi.Text == "" || txbMK_MoiLai.Text == "")
            {
                dao.ThongBao("Vui lòng nhập đầy đủ thông tin");
            }
            else if (txbMK_Moi.Text != txbMK_MoiLai.Text)
            {
                dao.ThongBao("Mật khẩu xác nhận không giống mật khẩu muốn đổi");
            }
            else if (txbMK_HienTai.Text == txbMK_Moi.Text)
            {
                dao.ThongBao("Mật khẩu mới phải khác với mật khẩu hiện tại");
            }
            else
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Email = @Email AND MatKhau = @MatKhau";
                string updateQuery = "UPDATE TaiKhoan SET MatKhau = @MatKhauMoi WHERE Email = @Email";

                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();

                        // Kiểm tra mật khẩu hiện tại
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Email", EmailDangNhap);
                            command.Parameters.AddWithValue("@MatKhau", txbMK_HienTai.Text);
                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                // Mật khẩu hiện tại đúng, thực hiện cập nhật mật khẩu mới
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Email", EmailDangNhap);
                                    updateCommand.Parameters.AddWithValue("@MatKhauMoi", txbMK_Moi.Text);

                                    int rowsAffected = updateCommand.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        dao.ThongBao("Đổi mật khẩu thành công");
                                        TatplDoiMatKhau();
                                    }
                                    else
                                    {
                                        dao.BaoLoi("Đổi mật khẩu thất bại");
                                    }
                                }
                            }
                            else
                            {
                                dao.BaoLoi("Sai mật khẩu hiện tại");
                            }
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    dao.BaoLoi("Đổi mật khẩu thất bại: " + ex.Message);
                }
            }
        }

        private bool HienMK(Guna2TextBox txt, Button btn, bool click)
        {
            if (click == true)
            {
                txt.UseSystemPasswordChar = false;
                txt.PasswordChar = '\0';
                btn.Image = Properties.Resources.AnMK_DoiTenTK;
                click = false;
            }
            else
            {
                txt.UseSystemPasswordChar = true;
                btn.Image = Properties.Resources.XemMK_DoiTenTK;
                click = true;
            }
            return click;
        }

        private void btnXem1_Click(object sender, EventArgs e)
        {
            click1 = HienMK(txbMK_HienTai, btnXem1, click1);
        }

        private void btnXem2_Click(object sender, EventArgs e)
        {
            click2 = HienMK(txbMK_Moi, btnXem2, click2);
        }

        private void btnXem3_Click(object sender, EventArgs e)
        {
            click3 =HienMK(txbMK_MoiLai, btnXem3, click3);
        }
    }
}
