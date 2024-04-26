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
using System.Windows.Media.Media3D;
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FTaoSuaHoSoMoi : Form
    {
        private FHoSoXinViec hs;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        string tenHS;
        DAO dao = new DAO();
        HoSoXinViecDAO hoSoXinViecDAO = new HoSoXinViecDAO();

        public FTaoSuaHoSoMoi(FHoSoXinViec parentForm)
        {
            InitializeComponent();
            hs = parentForm;
            hs.TaoHSMoi += TaoMoi;
            hs.SuaHS += HandleSuaHS;
        }

        private void TaoMoi()
        {
            TaoHoSoMoi();
        }

        private void TaoHoSoMoi()
        {
            btnLuu.Visible = true;
            btnLuuThayDoi.Visible = false;
            btnLuuHoSoMoi.Visible = false;
            btnHuyThayDoi.Visible = false;
        }

        private void LuuHoSoMoi()
        {
            btnLuu.Visible = false;
            btnLuuThayDoi.Visible = true;
            btnLuuHoSoMoi.Visible = true;
            btnHuyThayDoi.Visible = true;
        }

        private void TaiHoSoXinViec()
        {
            List<string> list = hoSoXinViecDAO.TaiHoSoXinViec(EmailDangNhap, tenHS);
            txbViTriUngTuyen.Text = list[0];
            txbMucTieuNgheNghiep.Text = list[1];
            txbKinhNghiemLamViec.Text = list[2];
            txbSoThich.Text = list[3];
            txbKyNang.Text = list[4];
        }

        private void HandleSuaHS(string tenHoSo)
        {
            this.tenHS = tenHoSo;
            LuuHoSoMoi();
            TaiHoSoXinViec();
        }

        private HoSoXinViec TaoHoSoXinViec()
        {
            HoSoXinViec hs = new HoSoXinViec(tenHS, 
                                            txbViTriUngTuyen.Text, 
                                            txbMucTieuNgheNghiep.Text, 
                                            txbKinhNghiemLamViec.Text, 
                                            txbKyNang.Text, txbSoThich.Text, 
                                            EmailDangNhap);
            return hs;
        }

        private void DaLuu (object sender, EventArgs e)
        {
            LuuHoSoMoi();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (KiemTraCacGiaTriDaDienDu() == true)
            {
                HoSoXinViec hs = TaoHoSoXinViec();
                LuuHoSoMoi luu = new LuuHoSoMoi(hs);
                luu.Show();
                luu.DaLuu += DaLuu;
            }
            else
            {
                dao.BaoLoi("Vui lòng điền đầy đủ thông tin");
            }
            
        }

        private void LuuThayDoi(object sender, EventArgs e)
        {
            dao.tb.Close();
            HoSoXinViec hs = TaoHoSoXinViec();
            if (hoSoXinViecDAO.CapNhatHoSoXinViec(hs) > 0)
            {
                dao.ThongBao("Lưu thành công");
            }
        }

        private void btnLuuThayDoi_Click(object sender, EventArgs e)
        {
            if (KiemTraCacGiaTriDaDienDu() == true)
            {
                dao.ThongBao_LuaChon("Bạn muốn lưu thay đổi?", LuuThayDoi);
            }
            else
            {
                dao.BaoLoi("Vui lòng điền đầy đủ thông tin");
            }
        }

        private void HuyThayDoi(object sender, EventArgs e)
        {
            dao.tb.Close();
            HandleSuaHS(tenHS);
        }

        private void btnHuyThayDoi_Click(object sender, EventArgs e)
        {
            dao.ThongBao_LuaChon("Bạn muốn hủy thay đổi?", HuyThayDoi);
        }

        private void btnLuuHoSoMoi_Click(object sender, EventArgs e)
        {
            if (KiemTraCacGiaTriDaDienDu() == true)
            {
                HoSoXinViec hs = TaoHoSoXinViec();
                LuuHoSoMoi luu = new LuuHoSoMoi(hs);
                luu.Show();
                LuuHoSoMoi();
            }
            else
            {
                dao.BaoLoi("Vui lòng điền đầy đủ thông tin");
            }
                
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            dao.MoFormCon(new FHoSoXinViec(), plFormCha);
        }

        public bool KiemTraCacGiaTriDaDienDu()
        {
            if (
                string.IsNullOrWhiteSpace(txbViTriUngTuyen.Text) ||
                string.IsNullOrWhiteSpace(txbKinhNghiemLamViec.Text) ||
                string.IsNullOrWhiteSpace(txbKyNang.Text) ||
                string.IsNullOrWhiteSpace(txbMucTieuNgheNghiep.Text) ||
                string.IsNullOrWhiteSpace(txbSoThich.Text)
                )
            {
                return false;
            }
            else
                return true;
        }
    }
}
