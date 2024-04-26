using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FXemThongTinCaNhan : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string EmailUV;
        string sqlStr;
        DAO dao = new DAO();
        public event EventHandler MoThongTinCaNhan;
      
       public FXemThongTinCaNhan()
        {

        }
        public FXemThongTinCaNhan(string EmailUV)
        {
            InitializeComponent();
            this.EmailUV = EmailUV;
        }

        private void FXemThongTinCaNhan_Load(object sender, EventArgs e)
        {
            sqlStr = string.Format("SELECT * FROM UngVien WHERE EmailDangNhap = @EmailDangNhap");
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@EmailDangNhap", EmailUV);
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
                        lblHoTen.Text = reader["hoTen"].ToString();
                        lblGioiTinh.Text = reader["gioiTinh"].ToString();
                        DateTime ngaySinh = reader.GetDateTime(reader.GetOrdinal("ngaySinh"));
                        lblNgaySinh.Text = ngaySinh.Day.ToString();
                        lblThangSinh.Text = ngaySinh.Month.ToString();
                        lblNamSinh.Text = ngaySinh.Year.ToString();
                        lblNoiSinh.Text = reader["noiSinh"].ToString();
                        lblNguyenQuan.Text = reader["nguyenQuan"].ToString();
                        lblSDT.Text = reader["SDT"].ToString();
                        lblEmail.Text = reader["Email"].ToString();
                        lblDanToc.Text = reader["danToc"].ToString();
                        lblTonGiao.Text = reader["tonGiao"].ToString();
                        lblTrinhDoVanHoa.Text = reader["trinhDoVanHoa"].ToString();
                        lblCCCD.Text = reader["CCCD"].ToString();
                        DateTime ngayCap = reader.GetDateTime(reader.GetOrdinal("ngayCapCCCD"));
                        lblNgayCapCCCD.Text = ngayCap.Day.ToString();
                        lblThangCapCCCD.Text = ngayCap.Month.ToString();
                        lblNamCapCCCD.Text = ngayCap.Year.ToString();
                        lblNoiCapCCCD.Text = reader["noiCapCCCD"].ToString();
                        lblDiaChi_ThuongTru.Text = reader["Duong_ThuongTru"].ToString() + ", " + reader["Xa_ThuongTru"].ToString()
                                            + ", " + reader["Huyen_ThuongTru"].ToString() + ", " + reader["Tinh_ThuongTru"].ToString();
                        lblDiaChi_HienNay.Text = reader["Duong_HienNay"].ToString() + ", " + reader["Xa_HienNay"].ToString()
                                            + ", " + reader["Huyen_HienNay"].ToString() + ", " + reader["Tinh_HienNay"].ToString();
                        lblHoTenNT.Text = reader["hoTenNT"].ToString();
                        lblSDTNT.Text = reader["SDTNT"].ToString();
                        lblEmailNT.Text = reader["EmailNT"].ToString();
                        lblDiaChiNT.Text = reader["Duong_NguoiThan"].ToString() + ", " + reader["Xa_NguoiThan"].ToString()
                                            + ", " + reader["Huyen_NguoiThan"].ToString() + ", " + reader["Tinh_NguoiThan"].ToString();
                    }
                }
                conn.Close();
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
        private void btnChinhSua_Click_1(object sender, EventArgs e)
        {
            MoThongTinCaNhan?.Invoke(this, e);
        }

        private void btnDongForm_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }
    }
}
