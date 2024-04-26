using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    internal class UngVien
    {
        private string hoTen;
        private string gioiTinh;
        private DateTime ngaySinh;
        private string noiSinh;
        private string nguyenQuan;
        private string Tinh_ThuongTru;
        private string Huyen_ThuongTru;
        private string Xa_ThuongTru;
        private string Duong_ThuongTru;
        private string Tinh_HienNay;
        private string Huyen_HienNay;
        private string Xa_HienNay;
        private string Duong_HienNay;
        private string SDT;
        private string Email;
        private string danToc;
        private string tonGiao;
        private string CCCD;
        private DateTime ngayCapCCCD;
        private string noiCapCCCD;
        private string trinhDoVanHoa;
        private string truong;
        private string chuyenNganh;
        private string khac;
        private string trinhDoNgoaiNgu;
        private string trinhDoChuyenMon;
        private string hoTenNT;
        private string SDTNT;
        private string EmailNT;
        private string Tinh_NguoiThan;
        private string Huyen_NguoiThan;
        private string Xa_NguoiThan;
        private string Duong_NguoiThan;
        private string EmailDangNhap;

        public UngVien(string hoTen, string gioiTinh, DateTime ngaySinh, 
            string noiSinh, string nguyenQuan, string tinh_ThuongTru, 
            string huyen_ThuongTru, string xa_ThuongTru, string duong_ThuongTru, 
            string tinh_HienNay, string huyen_HienNay, string xa_HienNay, string duong_HienNay, 
            string sDT, string email, string danToc, string tonGiao, string cCCD, 
            DateTime ngayCapCCCD, string noiCapCCCD, string trinhDoVanHoa, string truong, 
            string chuyenNganh, string khac, string trinhDoNgoaiNgu, string trinhDoChuyenMon, 
            string hoTenNT, string sDTNT, string emailNT, string tinh_NguoiThan, string huyen_NguoiThan, 
            string xa_NguoiThan, string duong_NguoiThan, string emailDangNhap)
        {
            this.hoTen = hoTen;
            this.gioiTinh = gioiTinh;
            this.ngaySinh = ngaySinh;
            this.noiSinh = noiSinh;
            this.nguyenQuan = nguyenQuan;
            Tinh_ThuongTru = tinh_ThuongTru;
            Huyen_ThuongTru = huyen_ThuongTru;
            Xa_ThuongTru = xa_ThuongTru;
            Duong_ThuongTru = duong_ThuongTru;
            Tinh_HienNay = tinh_HienNay;
            Huyen_HienNay = huyen_HienNay;
            Xa_HienNay = xa_HienNay;
            Duong_HienNay = duong_HienNay;
            SDT = sDT;
            Email = email;
            this.danToc = danToc;
            this.tonGiao = tonGiao;
            CCCD = cCCD;
            this.ngayCapCCCD = ngayCapCCCD;
            this.noiCapCCCD = noiCapCCCD;
            this.trinhDoVanHoa = trinhDoVanHoa;
            this.truong = truong;
            this.chuyenNganh = chuyenNganh;
            this.khac = khac;
            this.trinhDoNgoaiNgu = trinhDoNgoaiNgu;
            this.trinhDoChuyenMon = trinhDoChuyenMon;
            this.hoTenNT = hoTenNT;
            SDTNT = sDTNT;
            EmailNT = emailNT;
            Tinh_NguoiThan = tinh_NguoiThan;
            Huyen_NguoiThan = huyen_NguoiThan;
            Xa_NguoiThan = xa_NguoiThan;
            Duong_NguoiThan = duong_NguoiThan;
            EmailDangNhap = emailDangNhap;
        }

        public string HoTen
        {
            get { return hoTen; }
            set { hoTen = value; }
        }

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        public string NoiSinh
        {
            get { return noiSinh; }
            set { noiSinh = value; }
        }

        public string NguyenQuan
        {
            get { return nguyenQuan; }
            set { nguyenQuan = value; }
        }

        public string TinhThuongTru
        {
            get { return Tinh_ThuongTru; }
            set { Tinh_ThuongTru = value; }
        }

        public string HuyenThuongTru
        {
            get { return Huyen_ThuongTru; }
            set { Huyen_ThuongTru = value; }
        }

        public string XaThuongTru
        {
            get { return Xa_ThuongTru; }
            set { Xa_ThuongTru = value; }
        }

        public string DuongThuongTru
        {
            get { return Duong_ThuongTru; }
            set { Duong_ThuongTru = value; }
        }

        public string TinhHienNay
        {
            get { return Tinh_HienNay; }
            set { Tinh_HienNay = value; }
        }

        public string HuyenHienNay
        {
            get { return Huyen_HienNay; }
            set { Huyen_HienNay = value; }
        }

        public string XaHienNay
        {
            get { return Xa_HienNay; }
            set { Xa_HienNay = value; }
        }

        public string DuongHienNay
        {
            get { return Duong_HienNay; }
            set { Duong_HienNay = value; }
        }

        public string sDT
        {
            get { return SDT; }
            set { SDT = value; }
        }

        public string email
        {
            get { return Email; }
            set { Email = value; }
        }

        public string DanToc
        {
            get { return danToc; }
            set { danToc = value; }
        }

        public string TonGiao
        {
            get { return tonGiao; }
            set { tonGiao = value; }
        }

        public string cCCD
        {
            get { return CCCD; }
            set { CCCD = value; }
        }

        public DateTime NgayCapCCCD
        {
            get { return ngayCapCCCD; }
            set { ngayCapCCCD = value; }
        }

        public string NoiCapCCCD
        {
            get { return noiCapCCCD; }
            set { noiCapCCCD = value; }
        }

        public string TrinhDoVanHoa
        {
            get { return trinhDoVanHoa; }
            set { trinhDoVanHoa = value; }
        }

        public string Truong
        {
            get { return truong; }
            set { truong = value; }
        }

        public string ChuyenNganh
        {
            get { return chuyenNganh; }
            set { chuyenNganh = value; }
        }

        public string Khac
        {
            get { return khac; }
            set { khac = value; }
        }

        public string TrinhDoNgoaiNgu
        {
            get { return trinhDoNgoaiNgu; }
            set { trinhDoNgoaiNgu = value; }
        }

        public string TrinhDoChuyenMon
        {
            get { return trinhDoChuyenMon; }
            set { trinhDoChuyenMon = value; }
        }

        public string HoTenNT
        {
            get { return hoTenNT; }
            set { hoTenNT = value; }
        }

        public string sDTNT
        {
            get { return SDTNT; }
            set { SDTNT = value; }
        }

        public string emailNT
        {
            get { return EmailNT; }
            set { EmailNT = value; }
        }

        public string TinhNguoiThan
        {
            get { return Tinh_NguoiThan; }
            set { Tinh_NguoiThan = value; }
        }

        public string HuyenNguoiThan
        {
            get { return Huyen_NguoiThan; }
            set { Huyen_NguoiThan = value; }
        }

        public string XaNguoiThan
        {
            get { return Xa_NguoiThan; }
            set { Xa_NguoiThan = value; }
        }

        public string DuongNguoiThan
        {
            get { return Duong_NguoiThan; }
            set { Duong_NguoiThan = value; }
        }

        public string emailDangNhap
        {
            get { return EmailDangNhap; }
            set { EmailDangNhap = value; }
        }
    }
}
    