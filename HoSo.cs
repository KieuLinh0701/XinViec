using FontAwesome.Sharp;
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
using XinViec.Resources;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class HoSo : Form
    {
        string EmailDangNhap, tenHoSo;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        DAO dao = new DAO();
        public event EventHandler QuayLai;

        public void LoadHS()
        {
            sqlStr = string.Format("SELECT * FROM UngVien uv INNER JOIN HoSoXinViec hs " +
                "ON uv.EmailDangNhap = hs.EmailDangNhap WHERE uv.EmailDangNhap = @EmailDangNhap AND tenHoSo = @tenHoSo");
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                command.Parameters.AddWithValue("@tenHoSo", tenHoSo);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader["Anh"] != DBNull.Value && reader["Anh"] != null)
                        {
                            byte[] img = (byte[])reader["Anh"];
                            ptbAnh.Image = dao.ByteArrayToImage(img);
                        }
                        else
                        {
                            ptbAnh.Image = Properties.Resources.anhttcn_macdinh;
                        }
                        txbHoTen.Text = reader["hoTen"].ToString();
                        DateTime ngaySinh = reader.GetDateTime(reader.GetOrdinal("ngaySinh"));
                        txbNgaySinh.Text = ngaySinh.Day.ToString() + "/" + ngaySinh.Month.ToString() + "/" + ngaySinh.Year.ToString();
                        txbGioiTinh.Text = reader["gioiTinh"].ToString();
                        txbSDT.Text = reader["SDT"].ToString();
                        txbEmail.Text = reader["Email"].ToString();
                        rtbDiaChi.Text = reader["Duong_HienNay"].ToString() + ", " + reader["Xa_HienNay"].ToString() +
                                        ", " + reader["Huyen_HienNay"].ToString() + ", " + reader["Tinh_HienNay"].ToString();
                        rtbKyNang.Text = reader["kyNang"].ToString();
                        rtbTrinhDoNgoaiNgu.Text = reader["trinhDoNgoaiNgu"].ToString();
                        lblTrinhDoHocVan.Text = reader["trinhDoVanHoa"].ToString();
                        if (lblTrinhDoHocVan.Text == "Đại học")
                        {
                            rtbTruongDH.Text = reader["truong"].ToString();
                            lblChuyenNganh.Visible = true;
                            rtbChuyenNganh.Text = reader["chuyenNganh"].ToString();
                            rtbKhac.Text = reader["khac"].ToString();
                            if (rtbKhac.Text == null)
                            {
                                lblKhac.Visible = false;
                                rtbKhac.Visible = false;
                            }
                        }
                        else
                        {
                            rtbChuyenNganh.Visible = false;
                            rtbTruongDH.Visible = false;
                            lblChuyenNganh.Visible = false;
                            rtbTruongDH.Visible = false;
                            lblKhac.Visible = false;
                            rtbKhac.Visible = false;
                        }
                        rtbMucTieuNgheNghiep.Text = reader["mucTieuNgheNghiep"].ToString();
                        rtbKinhNghiemLamViec.Text = reader["kinhNghiemLamViec"].ToString();
                        rtbSoThich.Text = reader["soThich"].ToString();
                        rtbTrinhDoChuyenMon.Text = reader["trinhDoChuyenMon"].ToString();
                        txbViTriUngTuyen.Text = reader["viTriUngTuyen"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public Panel GetChildPanel()
        {
            return plHoSo;
        }

        private void HoSo_Load_1(object sender, EventArgs e)
        {
            LoadHS();
        }

        private void btnDongForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TroLai(object sender, EventArgs e)
        {
            //TrangCaNhanNguoiDung trangCaNhan = new TrangCaNhanNguoiDung(EmailDangNhap);
            ////MoFormCon(new HoSo(EmailDangNhap, tenHoSo), plFormCha);
            //trangCaNhan.Close();
            this.Close();
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
            //pl.Controls.Clear();
            pl.Controls.Add(fCon);
            pl.Tag = fCon;
            fCon.BringToFront();
            fCon.Show();
        }


        private void btnXemTrangCaNhan_Click(object sender, EventArgs e)
        {
            FTrangCaNhanNguoiDung trangCaNhan = new FTrangCaNhanNguoiDung(EmailDangNhap);
            // trangCaNhan.QuayLai += TroLai;
            //MoFormCon(trangCaNhan, plFormCha);
            trangCaNhan.FormBorderStyle = FormBorderStyle.Sizable;
            trangCaNhan.Show();
           
        }

        public HoSo(string EmailDangNhap, string tenHoSo)
        {
            InitializeComponent();
            this.EmailDangNhap = EmailDangNhap;
            this.tenHoSo = tenHoSo;
        }
    }
}
