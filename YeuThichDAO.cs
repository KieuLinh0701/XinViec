using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    internal class YeuThichDAO
    {
        DBConnection dBConnection = new DBConnection();
        public bool XoaYeuThich(YeuThich yt)
        {
            string sqlStr = String.Format("DELETE FROM YeuThich WHERE EmailUngVien = '{0}' AND TieuDe = '{1}' AND EmailCongTy = '{2}'", yt.EmailUngVien, yt.tieuDe, yt.EmailCongTy);
            if (dBConnection.ThucThiCauLenh(sqlStr) > 0)
            {
                return true;
            }
            return false;
        }

        public int HienThiSoLuongCV(string EmailUngVien)
        {
            string sqlStr = string.Format("SELECT COUNT(*) FROM YeuThich WHERE EmailUngVien = '{0}'", EmailUngVien);
            return dBConnection.ThucThiTraVeDong(sqlStr);
        }
    }
}
