using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    internal class DangTinTimViec
    {
        public string emailUngVien;
        public string noiDung;
        public string ngayDang;
        public string tenHoSo;

        public string EmailUngVien { get => emailUngVien; set => emailUngVien = value; }
        public string NoiDung { get => noiDung; set => noiDung = value; }
        public string NgayDang { get => ngayDang; set => ngayDang = value; }
        public string TenHoSo { get => tenHoSo; set => tenHoSo = value; }

        public DangTinTimViec(string emailUngVien, string noiDung, string ngayDang, string tenHoSo)
        {
            this.emailUngVien = emailUngVien;
            this.noiDung = noiDung;
            this.ngayDang = ngayDang;
            this.tenHoSo = tenHoSo;
        }
    }
}
