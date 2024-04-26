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
using XinViec.XinViec;

namespace XinViec
{
    public partial class ucHienThiCV : UserControl
    {
        DAO dao = new DAO();
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        public event ClickEventHandler1 ClickTenCTy;
        public event ClickEventHandler2 ClickTieuDe;
        public delegate void ClickEventHandler1(string param);
        public delegate void ClickEventHandler2(string param1, string param2);
        string sqlStr;
        public ucHienThiCV()
        {
            InitializeComponent();
        }

        private void btnYeuThich_Click(object sender, EventArgs e)
        {
            if (ImageComparer.ImagesAreEqual(btnYeuThich.Image, Properties.Resources.ThemYeuThich))
            {
                sqlStr = String.Format("INSERT INTO YeuThich (EmailCongTy, TieuDe, EmailUngVien, NgayThem) " +
                    "VALUES (@EmailCongTy, @TieuDe, @EmailUngVien, @NgayThem)");
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@EmailCongTy", txbEmailCongTy.Text);
                            command.Parameters.AddWithValue("@TieuDe", lblTieuDe.Text);
                            command.Parameters.AddWithValue("@EmailUngVien", EmailDangNhap);
                            command.Parameters.AddWithValue("@NgayThem", DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss"));
                            int k = command.ExecuteNonQuery();
                            if (k > 0)
                            {
                                btnYeuThich.Image = Properties.Resources.YeuThich;
                                btnYeuThich.Invalidate();
                                btnYeuThich.Update();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    dao.BaoLoi("Lưu công việc thất bại: " + ex.Message);
                }
            }
            else
            {
                sqlStr = String.Format("DELETE FROM YeuThich WHERE EmailUngVien = @EmailUngVien AND TieuDe = @TieuDe AND EmailCongTy = @EmailCongTy");
                try
                {
                    using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlStr, connection))
                        {
                            command.Parameters.AddWithValue("@TieuDe", lblTieuDe.Text);
                            command.Parameters.AddWithValue("@EmailUngVien ", EmailDangNhap);
                            command.Parameters.AddWithValue("@EmailCongTy", txbEmailCongTy.Text);
                            int k = command.ExecuteNonQuery();
                            if (k > 0)
                            {
                                btnYeuThich.Image = Properties.Resources.ThemYeuThich;
                                btnYeuThich.Invalidate();
                                btnYeuThich.Update();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    dao.ThongBao("Xóa công việc thất bại: " + ex);
                }
            }
        }

        private void lblTenCongTy_Click(object sender, EventArgs e)
        {
            ClickTenCTy?.Invoke(txbEmailCongTy.Text);
        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {
            ClickTieuDe?.Invoke(txbEmailCongTy.Text, lblTieuDe.Text);
        }
    }
}
