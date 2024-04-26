using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    public partial class FDuyetHoSo : Form
    {

        public string TrangThai { get; private set; }
        //FDatLichPhongVan fDatLichPhongVan = new FDatLichPhongVan();
        public FDuyetHoSo()
        {
            InitializeComponent();
        }

        

        private void btnDapUng_Click(object sender, EventArgs e)
        {
            // Người dùng chọn Đáp ứng
            TrangThai = "Đáp ứng";
            DialogResult = DialogResult.OK;
            
            this.Close();
           // fDatLichPhongVan.ShowDialog();
            
        }

        private void btnKhongDapUng_Click(object sender, EventArgs e)
        {

            // Người dùng chọn Không đáp ứng
            TrangThai = "Không đáp ứng";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
