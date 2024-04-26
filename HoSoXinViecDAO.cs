using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    internal class HoSoXinViecDAO
    {
        DBConnection dBConnection = new DBConnection();
        DAO dao = new DAO();
        public int XoaHoSo(string tenHoSo, string EmailDangNhap)
        {
            string sqlStr = string.Format("DELETE FROM HoSoXinViec WHERE tenHoSo = '{0}' AND EmailDangNhap = '{1}'", tenHoSo, EmailDangNhap);
            return dBConnection.ThucThiCauLenh(sqlStr);
        }

        public int SoHoSoDaTao(string EmailDangNhap)
        {
            string sqlStr = string.Format("SELECT COUNT(tenHoSo) AS soHoSoDaTao FROM HoSoXinViec WHERE EmailDangNhap = '{0}'", EmailDangNhap);
            return dBConnection.ThucThiTraVeDong(sqlStr);
        }
        
        public List<List<string>> TaiDanhSachHoSoDaTao(string EmailDangNhap)
        {
            string sqlStr = string.Format("SELECT tenHoSo, viTriUngTuyen, ngayCapNhat FROM HoSoXinViec WHERE EmailDangNhap = '{0}' ORDER BY ngayCapNhat DESC", EmailDangNhap);
            return TaiDanhSach(sqlStr);
        }

        public List<List<string>> TaiDanhSach(string sqlStr)
        {
            List<List<string>> list = new List<List<string>>();
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
                                string tenHoSo = reader["tenHoSo"].ToString();
                                string viTriUngTuyen = reader["viTriUngTuyen"].ToString();
                                string ngayCapNhat = reader.GetDateTime(reader.GetOrdinal("ngayCapNhat")).ToString("dd/MM/yyyy   HH:mm:ss");
                                List<string> listPhu = new List<string> { tenHoSo, viTriUngTuyen, ngayCapNhat };
                                list.Add(listPhu);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi: " + ex.Message);
            }
            return list;
        }

        public List<List<string>> TimKiemTheoViTriTuyenDung(string EmailDangNhap, string viTriTuyenDung)
        {
            string sqlStr = string.Format("SELECT tenHoSo, viTriUngTuyen, ngayCapNhat FROM HoSoXinViec WHERE EmailDangNhap = '{0}' AND viTriUngTuyen = '{1}' ORDER BY ngayCapNhat DESC", EmailDangNhap, viTriTuyenDung);
            return TaiDanhSach(sqlStr);
        }

        public List<List<string>> TimKiemTheoTenHoSo(string EmailDangNhap, string tenHoSo) 
        {
            string sqlStr = string.Format("SELECT tenHoSo, viTriUngTuyen, ngayCapNhat FROM HoSoXinViec WHERE EmailDangNhap = '{0}' AND tenHoSo = '{1}' ORDER BY ngayCapNhat DESC", EmailDangNhap, tenHoSo);
            return TaiDanhSach(sqlStr);
        }

        public List<object> LocTheoTenHoSo(string EmailDangNhap, string tenThuocTinh)
        {
            string sqlStr = string.Format("SELECT tenHoSo FROM HoSoXinViec WHERE EmailDangNhap = '{0}' ORDER BY ngayCapNhat DESC", EmailDangNhap);
            List<object> list = dBConnection.TraVeMotList(sqlStr, tenThuocTinh);
            return list;
        }

        public List<object> LocTheoViTriTuyenDung(string EmailDangNhap, string tenThuocTinh)
        {
            string sqlStr = string.Format("SELECT DISTINCT viTriUngTuyen FROM HoSoXinViec WHERE EmailDangNhap = '{0}'", EmailDangNhap);
            List<object> list = dBConnection.TraVeMotList(sqlStr, tenThuocTinh);
            return list;
        }

        public int CapNhatHoSoXinViec(HoSoXinViec hs)
        {
            string sqlStr = string.Format("UPDATE HoSoXinViec SET ngayCapNhat = GETDATE(), viTriUngTuyen = '{0}', " +
                "mucTieuNgheNghiep = '{1}', kinhNghiemLamViec = '{2}', " +
                "kyNang = '{3}', soThich = '{4}' " +
                "WHERE tenHoSo = '{5}' AND EmailDangNhap = '{6}'", hs.ViTriUngTuyen, hs.MucTieuNgheNghiep, hs.KinhNghiemLamViec, hs.KyNang, hs.SoThich, hs.TenHoSo, hs.EmailDangNhap);
            return dBConnection.ThucThiCauLenh(sqlStr);
        }

        public int TaoHoSoXinViec(HoSoXinViec hs)
        {
            string sqlStr = string.Format("INSERT INTO HoSoXinViec (tenHoSo, ngayCapNhat, viTriUngTuyen, mucTieuNgheNghiep, kinhNghiemLamViec, kyNang, soThich, EmailDangNhap) " +
                "VALUES ('{0}', GETDATE(), '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", hs.TenHoSo, hs.ViTriUngTuyen, hs.MucTieuNgheNghiep, hs.KinhNghiemLamViec, hs.KyNang, hs.SoThich, hs.EmailDangNhap);
            return dBConnection.ThucThiCauLenh(sqlStr);
        }

        public bool KiemTraTenHoSo(string tenHoSo)
        {
            string sqlStr = string.Format("SELECT COUNT(*) FROM HoSoXinViec WHERE tenHoSo = '{0}'", tenHoSo);
            if (dBConnection.ThucThiTraVeDong(sqlStr) > 0)
            {
                return true;
            }
            return false;
        }

        public List<string> TaiHoSoXinViec(string EmailDangNhap, string tenHoSo)
        {
            List<string> list = new List<string>();
            string sqlStr = string.Format("SELECT viTriUngTuyen, mucTieuNgheNghiep, kinhNghiemLamViec, soThich, " +
                "kyNang FROM HoSoXinViec WHERE EmailDangNhap = '{0}' AND tenHoSo = '{1}'", EmailDangNhap, tenHoSo);
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
                                list.Add(reader["mucTieuNgheNghiep"].ToString());
                                list.Add(reader["kinhNghiemLamViec"].ToString());
                                list.Add(reader["soThich"].ToString());
                                list.Add(reader["kyNang"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi: " + ex.Message);
            }
            return list;
        }
    }
}
