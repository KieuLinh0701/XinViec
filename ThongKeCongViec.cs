using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using XinViec.XinViec;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XinViec
{
    public partial class ThongKeCongViec : Form
    {
        string sqlStr;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        Chart chart;
        public ThongKeCongViec()
        {
            InitializeComponent();
            Load_cbbNam();
            Load_cbbThang();
        }

        private void Load_cbbThang()
        {
            cbbThang.Items.Clear();
            cbbThang.Items.Add("Tháng");
            for (int i = 1; i <= 12; i++)
            {
                cbbThang.Items.Add(i.ToString());
            }
            cbbThang.Text = cbbThang.Items[0].ToString();
        }

        private void Load_cbbNam()
        {
            cbbNam.Items.Clear();
            cbbNam.Items.Add("Năm");
            for (int i = DateTime.Now.Year; i >= 2010; i--)
            {
                cbbNam.Items.Add(i.ToString());
            }
            cbbNam.Text = cbbNam.Items[0].ToString();
        }

        private string Load_SoLuong(string query)
        {
            string count = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                    if (cbbNam.Text == "Năm") 
                    {
                        command.Parameters.AddWithValue("@Nam", 0);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Nam", Int32.Parse(cbbNam.Text));
                    }
                    if (cbbThang.Text == "Tháng")
                    {
                        command.Parameters.AddWithValue("@Thang", 0);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Thang", Int32.Parse(cbbThang.Text));
                    }
                    count = command.ExecuteScalar().ToString();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            return count;
        }

        private void SoHoSoKhongDat(string query)
        {
            txbSoHSKhongDat.Text = Load_SoLuong(query);
        }

        private void SoHoSoThongQua(string query)
        {
            txbSoHSThongQua.Text = Load_SoLuong(query);
        }

        private void SoHoSoUngTuyen(string query)
        {
            txbSoHSUngTuyen.Text = Load_SoLuong(query);
        }

        private void VeBieuDo()
        {
            plChuaBieuDo.Controls.Clear();
            // Thêm các điểm dữ liệu
            int a = int.Parse(txbSoHSKhongDat.Text);
            int b = int.Parse(txbSoHSThongQua.Text);
            int c = int.Parse(txbSoHSUngTuyen.Text);

            if (a == b && b == c && a == 0)
            {
                Guna2TextBox txb = new Guna2TextBox();
                txb.Text = "Bạn không có hoạt động gì trong thời gian này";
                txb.Font = new Font("Cambria", 12, FontStyle.Bold);
                txb.Location = new Point(250, 25);
                txb.Size = new System.Drawing.Size(500, 50);
                txb.ForeColor = System.Drawing.Color.DimGray;
                txb.Anchor = AnchorStyles.Left & AnchorStyles.Right;
                txb.BorderThickness = 0;
                txb.ReadOnly = true;
                plChuaBieuDo.Controls.Add(txb);
                txb.Show();
            }
            else
            {
                // Tạo biểu đồ
                chart = new Chart();
                chart.Size = new System.Drawing.Size(550, 300);
                chart.Location = new Point(180, 25);
                chart.Anchor = AnchorStyles.Left & AnchorStyles.Right;
                chart.ChartAreas.Add(new ChartArea());

                // Thêm loại biểu đồ và tên của loại dữ liệu
                Series series = new Series("Doughnut");
                series.ChartType = SeriesChartType.Doughnut;

                series.Points.AddXY("", a); //Hồ sơ không đạt yêu cầu
                series.Points.AddXY("", c - a - b); //Hồ sơ chưa duyệt
                series.Points.AddXY("", b); //Hồ sơ được thông qua

                // Thiết lập màu sắc 
                series.Points[0].Color = Color.FromArgb(192, 0, 0);
                series.Points[1].Color = Color.Teal;
                series.Points[2].Color = Color.DarkKhaki;

                Legend legend = new Legend();
                legend.Title = "BIỂU ĐỒ THỐNG KÊ LỊCH SỬ CÔNG VIỆC";
                legend.TitleForeColor = Color.FromArgb(192, 0, 0);
                legend.TitleFont = new Font("Cambria", 9, FontStyle.Bold);
                legend.Docking = Docking.Left;
                //legend.Alignment = StringAlignment.Center;
                legend.Font = new Font("Cambria", 9, FontStyle.Bold);
                legend.ForeColor = Color.DimGray;
                chart.Legends.Add(legend);
                series.Points[0].LegendText = "Hồ sơ không đạt yêu cầu";
                series.Points[1].LegendText = "Hồ sơ chưa được duyệt";
                series.Points[2].LegendText = "Hồ sơ được thông qua";

                // Thêm series vào biểu đồ
                chart.Series.Add(series);

                // Thêm biểu đồ vào form
                plChuaBieuDo.Controls.Add(chart);
            }
        }

        private void ThongKeCongViec_Load(object sender, EventArgs e)
        {
            SoHoSoUngTuyen(string.Format("SELECT COUNT(*) FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap " +
                        "AND ((@Nam = 0) OR (YEAR(NgayUngTuyen) = @Nam)) " +
                        "AND ((@Thang = 0) OR (MONTH(NgayUngTuyen) = @Thang))"));
            SoHoSoKhongDat(string.Format("SELECT COUNT(*) FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap " +
                        "AND TinhTrangHS = N'Khong dap ung' " +
                        "AND ((@Nam = 0) OR (YEAR(NgayUngTuyen) = @Nam)) " +
                        "AND ((@Thang = 0) OR (MONTH(NgayUngTuyen) = @Thang))"));
            SoHoSoThongQua(string.Format("SELECT COUNT(*) FROM UngTuyen WHERE EmailUngVien = @EmailDangNhap " +
                        "AND TinhTrangHS = N'Dap ung' " +
                        "AND ((@Nam = 0) OR (YEAR(NgayUngTuyen) = @Nam)) " +
                        "AND ((@Thang = 0) OR (MONTH(NgayUngTuyen) = @Thang))"));
            VeBieuDo();
        }

        private void cbbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThongKeCongViec_Load(sender, e);
        }

        private void cbbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThongKeCongViec_Load(sender, e);
        }

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            Load_cbbNam();
            Load_cbbThang();
            ThongKeCongViec_Load(sender, e);
        }
    }
}
