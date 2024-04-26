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
    public partial class FGioiThieuCongTy : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string Email = StateStorage.GetInstance().SharedValue.ToString();
        string sqlStr;
        public FGioiThieuCongTy()
        {
            InitializeComponent();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FGioiThieuCongTy_Load(object sender, EventArgs e)
        {
            sqlStr = string.Format("SELECT * FROM ThongTinCTy WHERE Email = @Email");

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@Email", Email);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        txtNamThanhLap.Text = DateTime.TryParse(reader["NgayThanhLap"].ToString(), out DateTime ngayThanhLap) ? ngayThanhLap.Year.ToString() : "Ngày không hợp lệ";

                        txtQuyMo.Text = reader["QuyMo"].ToString();
                        txtMoTa.Text = reader["MoTa"].ToString();
                        txtChinhSachPT.Text = reader["ChinhSachPT"].ToString();
                        txtCoHoiTT.Text = reader["CoHoiTT"].ToString();
                        txtCSLuongThuong.Text = reader["CSLuongThuong"].ToString();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void guna2TextBox10_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
