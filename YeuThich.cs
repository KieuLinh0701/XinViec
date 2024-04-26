using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    internal class YeuThich
    {
        public string emailCongTy;
        public string tieuDe;
        public string emailUngVien;
        public string ngayThem;

        public YeuThich(string emailCongTy, string tieuDe, string emailUngVien, string ngayThem) 
        { 
            this.emailCongTy = emailCongTy;
            this.tieuDe = tieuDe;
            this.emailUngVien = emailUngVien;
            this.ngayThem = ngayThem;
        }

        public string EmailCongTy
        {
            get { return emailCongTy; }
            set { emailCongTy = value; }
        }

        public string TieuDe
        {
            get { return tieuDe; }
            set { tieuDe = value; }
        }

        public string EmailUngVien
        {
            get { return emailUngVien; }
            set { emailUngVien = value; }
        }

        public string NgayThem
        {
            get { return ngayThem; }
            set { ngayThem = value; }
        }
    }
}
