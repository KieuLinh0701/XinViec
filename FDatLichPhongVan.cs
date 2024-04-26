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

namespace XinViec
{
    public partial class FDatLichPhongVan : Form
    {

        private string EmailUngVien;
        private string TenHoSo;


        //public DateTime lichHen { get; set; }
        public FDatLichPhongVan(string EmailUngVien, string TenHoSo)
        {
            InitializeComponent();
            this.EmailUngVien = EmailUngVien;
            this.TenHoSo = TenHoSo;
        }

        private void btnDongForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"EmailUngVien: {EmailUngVien}, TenHoSo: {TenHoSo}");
            string sqlStr = "UPDATE UngTuyen SET LichHen = @LichHen WHERE EmailUngVien = @EmailUngVien AND TenHoSo = @TenHoSo";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@LichHen", dtpLichHen.Value.Date);
                        command.Parameters.AddWithValue("@EmailUngVien", EmailUngVien);
                        command.Parameters.AddWithValue("@TenHoSo", TenHoSo);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật lịch hẹn thành công.");
                            // Cập nhật lại giao diện
                            
                        }
                        else
                        {
                            MessageBox.Show("Không có hồ sơ nào được cập nhật.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật lịch hẹn: " + ex.Message);
            }



            this.Close();
        }
    }
}
