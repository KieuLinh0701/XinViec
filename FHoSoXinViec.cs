using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FHoSoXinViec : Form
    { 
        private FTaoSuaHoSoMoi thsm;
        public delegate void TaoHSEventHandler();
        public delegate void SuaEventHandler(string tenHS);
        public event TaoHSEventHandler TaoHSMoi;
        public event SuaEventHandler SuaHS;
        public ucHienHoSo uc;
        string tenHS;
        int value = 0;
        bool hienThi = true;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        HoSoXinViecDAO hoSoXinViecDao = new HoSoXinViecDAO();

        public FHoSoXinViec()
        {
            InitializeComponent();
            thsm = new FTaoSuaHoSoMoi(this);
            Load_cbbTinhChatLoc();
        }

        private void Load_cbbTinhChatLoc()
        {
            cbbTinhChatLoc.Items.Clear();
            cbbDanhSachLoc.Items.Clear();
            foreach (string x in DanhSachCbbLoc.listHoSoXinViec)
            {
                cbbTinhChatLoc.Items.Add(x);
            }
            cbbTinhChatLoc.Text = cbbTinhChatLoc.Items[0].ToString();
        }

        private void Xoa(object sender, EventArgs e)
        {
            dao.tb.Close();
            if (hoSoXinViecDao.XoaHoSo(tenHS, EmailDangNhap) > 0) 
            {
                dao.ThongBao("Xóa thành công");
                plChuaHoSo.Controls.Clear();
                HoSoXinViec_Load(sender, e);
            }
        }

        private void XoaHoSo(string tenHS)
        {
            dao.ThongBao_LuaChon("Bạn muốn xóa hồ sơ?", Xoa);
            this.tenHS = tenHS;
        }
        private void SuaHoSo(string tenHS)
        {
            dao.MoFormCon(new FTaoSuaHoSoMoi(this), plFormCha);
            SuaHS?.Invoke(tenHS);
        }

        private void QuayLai(object sender, EventArgs e)
        {
            dao.MoFormCon(new FHoSoXinViec(), plFormCha);
        }

        private void XemHoSo(string tenHS)
        {
            XemHoSo xem = new XemHoSo(EmailDangNhap, tenHS);
            xem.QuayLai += QuayLai;
            dao.MoFormCon(xem, plFormCha);
        }
        private void HienThiHS(List<string> list)
        {
            ucHienHoSo uc = new ucHienHoSo();
            uc.ChonButtonSua += SuaHoSo;
            uc.ChonButtonXem += XemHoSo;
            uc.ChonButtonXoa += XoaHoSo;
            if (hienThi == true)
            {
                uc.Location = new Point(45, value);
                hienThi = false;
            }
            else
            {
                uc.Location = new Point(460, value);
                value += 100;
                hienThi = true;
            }
            uc.lblTenHoSo.Text = list[0].ToUpper();
            uc.lblViTriUngTuyen.Text = list[1].ToUpper();
            uc.lblNgayCapNhat.Text = list[2];
            plChuaHoSo.Controls.Add(uc);
        }

        private void SoHoSoDaTao()
        {
            txbSoHoSoDuocTao.Text = hoSoXinViecDao.SoHoSoDaTao(EmailDangNhap).ToString();
        }

        private void HienThiDanhSach(List<List<string>> list)
        {
            if (list.Count == 0)
            {
                dao.TaoTextBox("Không có hồ sơ nào để hiển thị", 350, 170, 12, 400, 50, plChuaHoSo);
            }
            foreach (List<string> hs in list)
            {
                HienThiHS(hs);
            }
        }

        private void TaiDanhSachHoSoDaTao()
        {
            List<List<string>> list = hoSoXinViecDao.TaiDanhSachHoSoDaTao(EmailDangNhap);
            HienThiDanhSach(list);
        }

        private void HoSoXinViec_Load(object sender, EventArgs e)
        {
            hienThi = true;
            value = 0;
            TaiDanhSachHoSoDaTao();
            SoHoSoDaTao();
        }
        private void cbbTinhChatLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_cbbDanhSachLoc();
        }

        private void ThemVaoCbbDanhSachLoc(List<object> list)
        {
            foreach (object item in list)
            {
                cbbDanhSachLoc.Items.Add(item.ToString());
            }
        }

        private void LocTheoTenHoSo()
        {
            List<object> list = hoSoXinViecDao.LocTheoTenHoSo(EmailDangNhap, "tenHoSo");
            ThemVaoCbbDanhSachLoc(list);
        }

        private void LocTheoViTriTuyenDung()
        {
            List<object> list = hoSoXinViecDao.LocTheoViTriTuyenDung(EmailDangNhap, "viTriUngTuyen");
            ThemVaoCbbDanhSachLoc(list);
        }

        private void Load_cbbDanhSachLoc()
        {
            cbbDanhSachLoc.Items.Clear();
            if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listHoSoXinViec[1])
            {
                LocTheoTenHoSo();
            }
            else if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listHoSoXinViec[2])
            {
                LocTheoViTriTuyenDung();
            }

            if (cbbDanhSachLoc.Items.Count == 0)
            {
                cbbDanhSachLoc.Items.Add("Danh sách lọc trống");
            }

            cbbDanhSachLoc.Text = cbbDanhSachLoc.Items[0].ToString();
        }

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            dao.MoFormCon(new FHoSoXinViec(), plFormCha);
        }

        private void TimKiemTheoTenHoSo()
        {
            List<List<string>> list = hoSoXinViecDao.TimKiemTheoTenHoSo(EmailDangNhap, cbbDanhSachLoc.Text);
            HienThiDanhSach(list);
        }

        private void TimKiemTheoViTriTuyenDung()
        {
            List<List<string>> list = hoSoXinViecDao.TimKiemTheoViTriTuyenDung(EmailDangNhap, cbbDanhSachLoc.Text);
            HienThiDanhSach(list);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            value = 0;
            hienThi = true;
            plChuaHoSo.Controls.Clear();
            if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listHoSoXinViec[0])
            {
                HoSoXinViec_Load(sender, e);
            }
            else if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listHoSoXinViec[1])
            {
                TimKiemTheoTenHoSo();
            }
            else if (cbbTinhChatLoc.Text == DanhSachCbbLoc.listHoSoXinViec[2])
            {
                TimKiemTheoViTriTuyenDung();
            }
            
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            dao.MoFormCon(new FTaoSuaHoSoMoi(this), plFormCha);
            TaoHSMoi?.Invoke();
        }
    }
}
