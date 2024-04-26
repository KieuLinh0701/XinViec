using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    internal class UngVienDAO
    {
        DBConnection dBConnection = new DBConnection();
        public int CapNhatThongTinUngVien(UngVien uv, byte[] imageData)
        {
            string sqlStr = string.Format("UPDATE UngVien SET Anh = '{0}', hoTen = '{1}', gioiTinh = '{2}', ngaySinh = '{3}', " +
        "noiSinh = '{4}', nguyenQuan = '{5}', Tinh_ThuongTru = '{6}', Huyen_ThuongTru = '{7}', " +
        "Xa_ThuongTru = '{8}', Duong_ThuongTru = '{9}', Tinh_HienNay = '{10}', Huyen_HienNay = '{11}', " +
        "Xa_HienNay = '{12}', Duong_HienNay = '{13}', SDT = '{14}', Email = '{15}', " +
        "danToc = '{16}', tonGiao = '{17}', CCCD = '{18}', ngayCapCCCD = '{19}', " +
        "noiCapCCCD = '{20}', trinhDoVanHoa = '{21}', Truong = '{22}', ChuyenNganh = '{23}', " +
        "Khac = '{24}', trinhDoNgoaiNgu = '{25}', trinhDoChuyenMon = '{26}', hoTenNT = '{27}', " +
        "SDTNT = '{28}', EmailNT = '{29}', Tinh_NguoiThan = '{30}', Huyen_NguoiThan = '{31}', " +
        "Xa_NguoiThan = '{32}', Duong_NguoiThan = '{33}' WHERE EmailDangNhap = '{34}'",
        /* 0 */ imageData,
        /* 1 */ uv.HoTen,
        /* 2 */ uv.GioiTinh,
        /* 3 */ uv.NgaySinh,
        /* 4 */ uv.NoiSinh,
        /* 5 */ uv.NguyenQuan,
        /* 6 */ uv.TinhThuongTru,
        /* 7 */ uv.HuyenThuongTru,
        /* 8 */ uv.XaThuongTru,
        /* 9 */ uv.DuongThuongTru,
        /* 10 */ uv.TinhHienNay,
        /* 11 */ uv.HuyenHienNay,
        /* 12 */ uv.XaHienNay,
        /* 13 */ uv.DuongHienNay,
        /* 14 */ uv.sDT,
        /* 15 */ uv.email,
        /* 16 */ uv.DanToc,
        /* 17 */ uv.TonGiao,
        /* 18 */ uv.cCCD,
        /* 19 */ uv.NgayCapCCCD,
        /* 20 */ uv.NoiCapCCCD,
        /* 21 */ uv.TrinhDoVanHoa,
        /* 22 */ uv.Truong,
        /* 23 */ uv.ChuyenNganh,
        /* 24 */ uv.Khac,
        /* 25 */ uv.TrinhDoNgoaiNgu,
        /* 26 */ uv.TrinhDoChuyenMon,
        /* 27 */ uv.HoTenNT,
        /* 28 */ uv.sDTNT,
        /* 29 */ uv.emailNT,
        /* 30 */ uv.TinhNguoiThan,
        /* 31 */ uv.HuyenNguoiThan,
        /* 32 */ uv.XaNguoiThan,
        /* 33 */ uv.DuongNguoiThan,
        /* 34 */ uv.emailDangNhap);

            return dBConnection.ThucThiCauLenh(sqlStr);
        }
    }
}
