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
    internal class TaiKhoanDao
    {
        DBConnection dBConnection = new DBConnection();
        public bool KiemTraMatKhauCoDung(TKhoan tk)
        {
            string query = string.Format("SELECT COUNT(*) FROM TaiKhoan WHERE Email = '{0}' AND MatKhau = '{1}'", tk.Email, tk.MatKhau);
            if (dBConnection.ThucThiTraVeDong(query) > 0)
            {
                return true;
            }
            return false;
        }

        public int ThayDoiMatKhau(TKhoan tk)
        {
            string query = string.Format("UPDATE TaiKhoan SET MatKhau = '{0}' WHERE Email = '{1}'", tk.MatKhau, tk.Email);
            return dBConnection.ThucThiCauLenh(query);
        }
    }
}
