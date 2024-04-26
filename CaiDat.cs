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
using XinViec.XinViec;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class CaiDat : Form
    {
        DAO dao = new DAO();
        string EmailDangNhap = StateStorage.GetInstance().SharedValue.ToString();
        public CaiDat()
        {
            InitializeComponent();
        }
        
        private void DoiMauButtonKhiDuocCho(Button btn)
        {
            btnCaiDatEmail.BackColor = SystemColors.Control;
            btnCaiDatMatKhau.BackColor = SystemColors.Control;
            btnThongTinCaNhan.BackColor = SystemColors.Control;
            btn.BackColor = Color.Gainsboro;
        }
        private void MoThongTinCaNhan(object sender, EventArgs e)
        {
            ThongTinCaNhan ttcn = new ThongTinCaNhan();
            ttcn.MoXemThongTinCaNhan += MoXemThongTinCaNhan;
            dao.MoFormCon(ttcn, plFormCha);
        }

        private void MoXemThongTinCaNhan(object sender, EventArgs e)
        {
            FXemThongTinCaNhan xem = new FXemThongTinCaNhan(EmailDangNhap);
            xem.MoThongTinCaNhan += MoThongTinCaNhan;
            dao.MoFormCon(xem, plFormCha);

        }
        private void btnThongTinCaNhan_Click_1(object sender, EventArgs e)
        {
            DoiMauButtonKhiDuocCho(btnThongTinCaNhan);
            MoXemThongTinCaNhan(sender, e);
        }

        private void btnCaiDatMatKhau_Click(object sender, EventArgs e)
        {
            DoiMauButtonKhiDuocCho(btnCaiDatMatKhau);
            dao.MoFormCon(new CaiDatMatKhau(), plFormCha);
        }


        private void CaiDat_Load(object sender, EventArgs e)
        {
            btnThongTinCaNhan_Click_1(sender, e);
        }

        private void btnCaiDatEmail_Click(object sender, EventArgs e)
        {
            DoiMauButtonKhiDuocCho(btnCaiDatEmail);
            dao.MoFormCon(new CaiDatEmail("Ung Vien"), plFormCha);
        }
    } 
}
