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
    public partial class UCCVYeuThich : UserControl
    {

        public event ButtonClickEventHandler ChonButtonXemChiTiet;
        public event ButtonClickEventHandler1 ChonButtonBoLuu;


        public delegate void ButtonClickEventHandler(string emailUV);
        public delegate void ButtonClickEventHandler1(string emailUngVien, string emailCongTy);

        public UCCVYeuThich()
        {
            InitializeComponent();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            ChonButtonXemChiTiet?.Invoke(lblEmailUV.Text);
        }

        private void btnBoYeuThich_Click(object sender, EventArgs e)
        {
            ChonButtonBoLuu?.Invoke(lblEmailUV.Text, lblEmailCongTy.Text);
        }
    }
}
