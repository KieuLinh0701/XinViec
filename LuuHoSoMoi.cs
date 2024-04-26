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

namespace XinViec
{
    public partial class LuuHoSoMoi : Form
    {
        public event EventHandler DaLuu;
        DAO dao = new DAO();
        HoSoXinViec hs;
        HoSoXinViecDAO hoSoXinViecDAO = new HoSoXinViecDAO();
        public LuuHoSoMoi(HoSoXinViec hs)
        {
            InitializeComponent();
            this.hs = hs;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool KiemTraTenHoSo(string tenHoSo)
        {
            return hoSoXinViecDAO.KiemTraTenHoSo(tenHoSo);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            hs.TenHoSo = txbTenHoSo.Text;
            if (KiemTraTenHoSo(hs.TenHoSo))
            {
                dao.BaoLoi("Tên hồ sơ đã tồn tại. Vui lòng nhập tên khác");
            }
            else if (hoSoXinViecDAO.TaoHoSoXinViec(hs) > 0)
            {
                dao.ThongBao("Lưu thành công");
                DaLuu?.Invoke(this, e);
                this.Close();
            }
        }
    }
}
