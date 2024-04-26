using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XinViec
{
    class TKhoan
    {
        private string email;
        private string matKhau;
        private string vaiTro;


        public TKhoan() { }

        public TKhoan(string email, string matKhau, string vaiTro)
        {
            this.email = email;
            this.matKhau = matKhau;
            this.vaiTro = vaiTro;

        }
        public string Email
        { 
            get { return email; }
            set { email = value; }
        }
        public string MatKhau
        {
            get { return matKhau; }
            set { matKhau = value; }
        }
        public string VaiTro
        {
            get { return vaiTro; }
            set { vaiTro = value; }
        }

    }

}
