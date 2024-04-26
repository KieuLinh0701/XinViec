using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    internal class DangTinTimViecDAO
    {
        DAO dao = new DAO();
        DBConnection dBConnection = new DBConnection();

        public int SetTrangThaiUngTuyen(string Email, string tieuDe)
        {
            string sqlStr = string.Format("UPDATE DangTinTuyenDung SET TrangThai = N'Da het han' WHERE GETDATE() > NgayHetHan AND Email = '{0}' AND TieuDe = '{1}'", Email, tieuDe);
            return dBConnection.ThucThiCauLenh(sqlStr);
        }

        public int DaUngTuyen(string EmailUngVien, string EmailCongTy, string tieuDe)
        {
            string sqlStr = string.Format("Select Count(*) FROM UngTuyen WHERE EmailUngVien = '{0}' AND EmailCongTy = '{1}' AND TieuDe = '{2}'", EmailUngVien, EmailCongTy, tieuDe);
            return dBConnection.ThucThiTraVeDong(sqlStr);
        }
        public void Load_ThongTinDaDang(string EmailDangNhap, string ngayDang, Guna2TextBox txb)
        {
            string sqlStr = string.Format("SELECT * FROM DangTinTimViec WHERE EmailUngVien = '{0}' AND NgayDang = '{1}'", EmailDangNhap, ngayDang);
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txb.Text = reader["NoiDung"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
        }

        public int XoaTinTimViec(string EmailUV, string ngayDang)
        {
            string sqlStr = string.Format("DELETE FROM DangTinTimViec WHERE EmailUngVien = '{0}' AND NgayDang = '{1}'", EmailUV, ngayDang);
            return dBConnection.ThucThiCauLenh(sqlStr);
        }

        public int ChinhSuaTinTimViec(string noiDung, string ngayDang, string EmailDangNhap, string tenHoSo)
        {
            string sqlStr = string.Format("UPDATE DangTinTimViec SET NoiDung = '{0}' WHERE NgayDang = '{1}' AND EmailUngVien = '{2}' AND TenHoSo = '{3}'", noiDung, ngayDang, EmailDangNhap, tenHoSo);
            return dBConnection.ThucThiCauLenh(sqlStr);
        }
    }
}
