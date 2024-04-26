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

namespace XinViec
{
    public partial class FDanhGia : Form
    {
        DAO dao = new DAO();
        string EmailCongTy, EmailUngVien, TieuDe, sqlStr;
        public FDanhGia(string EmailCongTy, string EmailUngVien, string TieuDe)
        {
            InitializeComponent();
            this.EmailCongTy = EmailCongTy;
            this.EmailUngVien = EmailUngVien;
            this.TieuDe = TieuDe;
        }

        private void QuayLaiLichSuCongViec()
        {
            dao.MoFormCon(new LichSuCongViec(), plFormCha);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            QuayLaiLichSuCongViec();
        }

        private void PhanHoi(object sender, EventArgs e)
        {
            sqlStr = String.Format("INSERT INTO PhanHoi (EmailUngVien, EmailCongTy, TieuDe, PhanHoi, NgayPhanHoi) VALUES (@EmailUngVien, @EmailCongTy, @TieuDe, @PhanHoi, @NgayPhanHoi)");
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailCongTy", EmailCongTy);
                        command.Parameters.AddWithValue("@EmailUngVien", EmailUngVien);
                        command.Parameters.AddWithValue("@TieuDe", TieuDe);
                        command.Parameters.AddWithValue("@PhanHoi", rtbPhanHoi.Text);
                        command.Parameters.AddWithValue("@NgayPhanHoi", DateTime.Now.ToString("dd/MM/yyyy   hh:mm:ss"));
                        int k = command.ExecuteNonQuery();

                        // Check if rows were affected
                        if (k > 0)
                        {
                            dao.ThongBao("Gửi phản hồi thành công");
                            QuayLaiLichSuCongViec();
                        }
                        else
                        {
                            dao.BaoLoi("Gửi phản hồi thất bại");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Gửi phản hồi thất bại: " + ex.Message);
            }
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            dao.ThongBao_LuaChon("Xác nhận gửi phản hồi, góp ý?", PhanHoi);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            QuayLaiLichSuCongViec();
        }
    }
}
