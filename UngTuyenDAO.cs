using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;

namespace XinViec
{
    internal class UngTuyenDAO
    {
        DBConnection dBConnection = new DBConnection();
        DAO dao = new DAO();
        FUngTuyen fut;
        public int ThemUngTuyen(UngTuyen ut)
        {
            string sqlStr = string.Format("INSERT INTO UngTuyen (EmailUngVien, TenHoSo, EmailCongTy, TieuDe, NgayUngTuyen, TrangThaiDuyet) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", ut.EmailUngVien, ut.tenHoSo, ut.EmailCongTy, ut.tieuDe, ut.NgayUngTuyen, ut.trangThaiDuyet);
            int k = dBConnection.ThucThiCauLenh(sqlStr);
            return k;
        }

        public void MoFormUngTuyen(string Email, string tieuDe, RichTextBox txb)
        {
            string sqlStr = string.Format("SELECT Ten, TenCongViec FROM ThongTinCTy tt INNER JOIN DangTinTuyenDung dt ON tt.Email = dt.Email  WHERE tt.Email = '{0}' AND TieuDe = '{1}'", Email, tieuDe);
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
                                txb.Text = reader["Ten"].ToString().ToUpper() + "\n\n Công việc: " + reader["TenCongViec"].ToString();
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        public List<string> LocViTriTuyenDung(string EmailDangNhap)
        {
            List<string> list = new List<string>();
            string sqlStr = string.Format("SELECT DISTINCT viTriUngTuyen FROM HoSoXinViec WHERE EmailDangNhap = '{0}'", EmailDangNhap);
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
                                list.Add(reader["viTriUngTuyen"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            return list;
        }

        public List<string> LocTenHoSo(string EmailDangNhap)
        {
            List<string> list = new List<string>();
            string sqlStr = string.Format("SELECT tenHoSo FROM HoSoXinViec WHERE EmailDangNhap = '{0}' ORDER BY ngayCapNhat ASC", EmailDangNhap);
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
                                list.Add(reader["tenHoSo"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            return list;
        }

        public int DangTinTimViec(string EmailDangNhap, string tieuDe, string ngayDang, string tenHS)
        {
            string sqlStr = string.Format("INSERT INTO DangTinTimViec (EmailUngVien, NoiDung, NgayDang, TenHoSo) VALUES ('{0}', '{1}', '{2}', '{3}')", EmailDangNhap, tieuDe, ngayDang, tenHS);
            return  dBConnection.ThucThiCauLenh(sqlStr);
        }
    }
}
