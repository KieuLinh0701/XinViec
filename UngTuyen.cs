using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    internal class UngTuyen
    {
        public static List<string> listTinhChatLoc = new List<string>(){"Tất cả", "Vị trí tuyển dụng", "Tên hồ sơ" };
        public string EmailUngVien;
        public string tenHoSo;
        public string EmailCongTy;
        public string tieuDe;
        public string ngayUngTuyen;
        public string trangThaiDuyet;
        public string tinhTrangHS;
        public string lichHen;

        public UngTuyen(string EmailUngVien, string tenHoSo, string EmailCongTy, string tieuDe, string ngayUngTuyen)
        {
            this.EmailUngVien = EmailUngVien;
            this.tenHoSo = tenHoSo;
            this.EmailCongTy = EmailCongTy;
            this.tieuDe = tieuDe;
            this.ngayUngTuyen = ngayUngTuyen;
            trangThaiDuyet = "Chua duyet";
        }

        public string emailUngVien { get; set; }
        public string TenHoSo { get; set; }
        public string emailCongTy { get; set; }
        public string TieuDe { get; set; }
        public string NgayUngTuyen { get; set; }
        public string TrangThaiDuyet { get; set; }
        public string TinhTrangHS { get; set; }
        public string LichHen { get; set; }
    }
}
