using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    internal class NhaTuyenDung
    {
        private int maNTD;
        private string ten;
        private DateTime ngayThanhLap;
        private string quyMo;
        private string email;
        private string sdt;
        private string diaChi;
        private string moTa;

        public NhaTuyenDung(int maNTD, string ten, DateTime ngayThanhLap, string quyMo, string email, string sdt, string diaChi, string moTa   )
        {
            this.maNTD = maNTD;
            this.ten = ten;
            this.ngayThanhLap = ngayThanhLap;
            this.quyMo = quyMo;
            this.email = email;
            this.sdt = sdt;
            this.diaChi = diaChi;
            this.moTa = moTa;
        }

        public int MaNTD
        {
            get { return maNTD; }
            set { maNTD = value; }
        }

        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }

        public DateTime NgayThanhLap
        {
            get { return ngayThanhLap; }
            set { ngayThanhLap = value; }
        }

        public string QuyMo
        {
            get { return quyMo; }
            set {  quyMo = value; }
        }

        public string Email
        { 
            get { return email; } 
            set {  email = value; } 
        }
        public string Sdt
        {
            get { return sdt; }
            set { sdt = value; }
        }
        public string DiaChi
        { 
            get { return diaChi; } 
            set { diaChi = value; } 
        }
        public string MoTa
        { 
            get { return moTa; } 
            set {  moTa = value; } 
        }
    }
}
