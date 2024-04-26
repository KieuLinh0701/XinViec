using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    public class HoSoXinViec
    {
        public string tenHoSo;
        public DateTime ngayCapNhat;
        public string viTriUngTuyen;
        public string mucTieuNgheNghiep;
        public string kinhNghiemLamViec;
        public string kyNang;
        public string soThich;
        public string emailDangNhap;

        public HoSoXinViec(string tenHoSo,
                            string viTriUngTuyen,
                            string mucTieuNgheNghiep,
                            string kinhNghiemLamViec,
                            string kyNang,
                            string soThich,
                            string emailDangNhap)
        {
            this.tenHoSo = tenHoSo;
            ngayCapNhat = DateTime.Now;
            this.viTriUngTuyen = viTriUngTuyen;
            this.mucTieuNgheNghiep = mucTieuNgheNghiep;
            this.kinhNghiemLamViec = kinhNghiemLamViec;
            this.kyNang = kyNang;
            this.soThich = soThich;
            this.emailDangNhap = emailDangNhap;
        }

        public string TenHoSo
        {
            get { return tenHoSo; }
            set { tenHoSo = value; }
        }

        public DateTime NgayCapNhat
        {
            get { return ngayCapNhat; }
            set { ngayCapNhat = value; }
        }

        public string ViTriUngTuyen
        {
            get { return viTriUngTuyen; }
            set { viTriUngTuyen = value; }
        }

        public string MucTieuNgheNghiep
        {
            get { return mucTieuNgheNghiep; }
            set { mucTieuNgheNghiep = value; }
        }

        public string KinhNghiemLamViec
        {
            get { return kinhNghiemLamViec; }
            set { kinhNghiemLamViec = value; }
        }

        public string KyNang
        {
            get { return kyNang; }
            set { kyNang = value; }
        }

        public string SoThich
        {
            get { return soThich; }
            set { soThich = value; }
        }

        public string EmailDangNhap
        {
            get { return emailDangNhap; }
            set { emailDangNhap = value; }
        }
    }
}
