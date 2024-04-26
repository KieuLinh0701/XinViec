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

namespace XinViec
{
    public partial class FUngVienKhongPV : Form
    {
        string sqlStr;
        DAO dao = new DAO();
        public FUngVienKhongPV()
        {
            InitializeComponent();
        }

        public Form activeForm = null;
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
        private void themUCHoSoYeuThich(string tenUV, string gioiTinh, string emailUV)
        {
            UCCVYeuThich uc = new UCCVYeuThich();
            uc.ChonButtonXemChiTiet += XemChiTiet;
            uc.btnBoYeuThich.Visible = false;
            uc.Dock = DockStyle.Top;
            uc.lblTenUV.Text = tenUV;
            uc.lblGioiTinh.Text = gioiTinh;
            uc.lblEmailUV.Text = emailUV;
            plChua.Controls.Add(uc);
        }

        private void QuayLai(object sender, EventArgs e)
        {
            dao.MoFormCon(new FUngVienKhongPV(), plFormCha);
        }
        private void XemChiTiet(string emailUV)
        {
            FTrangCaNhanNguoiDung trangCaNhan = new FTrangCaNhanNguoiDung(emailUV);
            trangCaNhan.btnQuayLai.Visible = true;
            trangCaNhan.btnPostCongViec.Visible = false;
            trangCaNhan.QuayLai += QuayLai;
            MoFormCon(trangCaNhan, plFormCha);
        }

        private void FUngVienKhongPV_Load(object sender, EventArgs e)
        {
            sqlStr = @"SELECT *
                FROM (SELECT EmailUngVien
                FROM UngTuyen
                WHERE TrangThaiPhongVan = 'Khong') AS a
                INNER JOIN UngVien u
                ON a.EmailUngVien = u.EmailDangNhap";


            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tenUngVien = reader["hoTen"].ToString();
                                string gioiTinh = reader["gioiTinh"].ToString();
                                string emailUV = reader["EmailUngVien"].ToString();
                                themUCHoSoYeuThich(tenUngVien, gioiTinh, emailUV);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi truy xuất dữ liệu
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
    }
}
