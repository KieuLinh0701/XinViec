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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class FDoiEmailDangNhap : Form
    {
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString();
        string vaiTro;
        ModifyTK modifyTK = new ModifyTK();
        bool click = true;
        DAO dao = new DAO();
        public FDoiEmailDangNhap(string vaiTro)
        {
            InitializeComponent();
            this.vaiTro = vaiTro;
        }

        private void DoiTenTK(object sender, EventArgs e)
        {
            dao.tb.Close();
            if (txbEmail.Text == "")
            {
                dao.ThongBao("Vui lòng nhập email muốn đổi!");
            }
            else if (txbMatKhau.Text == "")
            {
                dao.ThongBao("Vui lòng nhập mật khẩu!");
            }
            else
            {
                SqlCommand Command;
                string query = "Select * from TaiKhoan where Email = '" + EmailDangNhap + "' and MatKhau = '" + txbMatKhau.Text + "' and VaiTro = '" + vaiTro + "' ";
                if (modifyTK.TKhoans(query).Count != 0)
                {
                    string sqlStr = "UPDATE TaiKhoan SET Email = @Email WHERE Email = @EmailDangNhap";
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(sqlStr, connection))
                            {
                                command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                                command.Parameters.AddWithValue("@Email", txbEmail.Text);
                                // Execute the INSERT query
                                int k = command.ExecuteNonQuery();

                                // Check if rows were affected
                                if (k > 0)
                                {
                                    dao.ThongBao("Đổi email thành công");
                                    StateStorage.GetInstance().SharedValue = txbEmail.Text;
                                    if (vaiTro == "Ung Vien")
                                    {
                                        query = "UPDATE UngVien SET EmailDangNhap = '" + txbEmail.Text + "' WHERE EmailDangNhap = '" + EmailDangNhap + "' ";
                                        Command = new SqlCommand(query, connection);
                                        Command.ExecuteNonQuery();

                                        query = "UPDATE HoSoXinViec SET EmailDangNhap = '" + txbEmail.Text + "' WHERE EmailDangNhap = '" + EmailDangNhap + "' ";
                                        Command = new SqlCommand(query, connection);
                                        Command.ExecuteNonQuery();

                                        query = "UPDATE UngTuyen SET EmailUngVien = '" + txbEmail.Text + "' WHERE EmailUngVien = '" + EmailDangNhap + "' ";
                                        Command = new SqlCommand(query, connection);
                                        Command.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        query = "UPDATE DangTinTuyenDung SET Email = '" + txbEmail.Text + "' WHERE Email = '" + EmailDangNhap + "' ";
                                        Command = new SqlCommand(query, connection);
                                        Command.ExecuteNonQuery();

                                        query = "UPDATE UngTuyen SET EmailCongty = '" + txbEmail.Text + "' WHERE EmailCongty = '" + EmailDangNhap + "' ";
                                        Command = new SqlCommand(query, connection);
                                        Command.ExecuteNonQuery();

                                        query = "UPDATE ThongTinCTy SET Email = '" + txbEmail.Text + "' WHERE Email = '" + EmailDangNhap + "' ";
                                        Command = new SqlCommand(query, connection);
                                        Command.ExecuteNonQuery();
                                    }

                                    dao.MoFormCon(new FCaiDatEmail(vaiTro), plFormCha);
                                }
                                else
                                {
                                    dao.BaoLoi("Đổi email thất bại");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dao.BaoLoi("Đổi email thất bại: " + ex);
                    }
                }
                else
                {
                    dao.BaoLoi("Sai mật khẩu!");
                }
            }
        }

        private void Huy(object sender, EventArgs e)
        {
            dao.tb.Close();
            dao.MoFormCon(new FCaiDatEmail(vaiTro), plFormCha);
        }
        private void btnDoiTenTK_Click_1(object sender, EventArgs e)
        {
            dao.ThongBao_LuaChon("Bạn muốn đổi Email?", DoiTenTK);
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            dao.ThongBao_LuaChon("Bạn muốn hủy thay đổi?", Huy);
        }

        private void btnXemMK_Click(object sender, EventArgs e)
        {
            if (click == true)
            {
                txbMatKhau.UseSystemPasswordChar = false;
                txbMatKhau.PasswordChar = '\0';
                btnXemMK.Image = Properties.Resources.AnMK_DoiTenTK;
                click = false;
            }
            else
            {
                txbMatKhau.UseSystemPasswordChar = true;
                btnXemMK.Image = Properties.Resources.XemMK_DoiTenTK;
                click = true;
            }
        }
    }
}
