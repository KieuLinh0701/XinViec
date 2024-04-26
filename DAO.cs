using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.Resources;
using System.Drawing;
using System.Data.SqlClient;
using Guna.UI2.WinForms;

namespace XinViec
{
    internal class DAO
    {
        public Form activeForm = null;
        public FThongBao_LuaChon tb;
        public FThongBao tbao;

        public Image ByteArrayToImage(Byte[] b)
        {
            MemoryStream m = new MemoryStream(b);
            return Image.FromStream(m);
        }

        public Button DoiMauButtonKhiDuocChon(Button btn, Button oldBtn, Color mauMacDinh, Color mauDuocChon)
        {
            oldBtn.BackColor = mauMacDinh;
            btn.BackColor = mauDuocChon;
            return btn;
        }

        public void TaoTextBox(string noiDung, int locationx, int locationy, int sizeChu, int sizex, int sizey, Panel pl)
        {
            Guna2TextBox txb = new Guna2TextBox();
            txb.Text = noiDung;
            txb.Font = new Font("Cambria", sizeChu, FontStyle.Bold);
            txb.Location = new Point(locationx, locationy);
            txb.Size = new System.Drawing.Size(sizex, sizey);
            txb.ForeColor = System.Drawing.Color.DimGray;
            txb.FillColor = SystemColors.Control;
            txb.Anchor = AnchorStyles.Left & AnchorStyles.Right;
            txb.BorderThickness = 0;
            txb.ReadOnly = true;
            pl.Controls.Add(txb);
            txb.Show();
        }

        public void MoFormCon(Form fCon, Panel pl)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = fCon;
            fCon.TopLevel = false;
            fCon.FormBorderStyle = FormBorderStyle.None;
            fCon.Dock = DockStyle.Fill;
            pl.Controls.Clear();
            pl.Controls.Add(fCon);
            pl.Tag = fCon;
            fCon.BringToFront();
            fCon.Show();
        }

        public void ApplyCenterAlignment(RichTextBox rtb)
        {
            rtb.SelectAll();
            rtb.SelectionAlignment = HorizontalAlignment.Center;
        }

        public void BaoLoi(string str)
        {
            FBaoLoi bl = new FBaoLoi();
            bl.rtbThongBao.Text = str;
            bl.Show();
        }
        public void ThongBao_LuaChon(string str, EventHandler tenPhuongThuc)
        {
            tb = new FThongBao_LuaChon();
            tb.rtbThongBao.Text = str;
            tb.ChonButtonCo += tenPhuongThuc;
            tb.Show();
        }
        public void ThongBao(string str)
        {
            tbao = new FThongBao();
            tbao.rtbThongBao.Text = str;
            tbao.Show();
        }
    }
}
