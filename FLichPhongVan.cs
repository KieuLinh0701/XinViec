using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using XinViec.XinViec;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class FLichPhongVan : Form
    {
        string sqlStr;
        ucLichHenPhongVan uc;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        System.Windows.Forms.Button oldBtn = new System.Windows.Forms.Button();
        int demCuocHen;

        #region Peoperties
        private List<List<System.Windows.Forms.Button>> matrix;

        public List<List<System.Windows.Forms.Button>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        private List<String> dateOffWeek = new List<string>() {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
        #endregion
        public FLichPhongVan()
        {
            InitializeComponent();
        }
        private void LichHenTuanNay(DateTime dt)
        {
            sqlStr = "SELECT COUNT(*) AS SoLichHen FROM UngTuyen u " +
                "INNER JOIN ThongTinCTy t ON u.EmailCongTy = t.Email " +
                "INNER JOIN DangTinTuyenDung d ON u.EmailCongTy = d.Email AND u.TieuDe = d.TieuDe " +
                "WHERE EmailUngVien = @EmailUngVien AND DATEPART(WEEK, LichHen) = DATEPART(WEEK, @Ngay)";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStr, connection);
                    command.Parameters.Add("@EmailUngVien", EmailDangNhap);
                    command.Parameters.Add("@Ngay", dt);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txbTuanNay.Text = reader["SoLichHen"].ToString();
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void LichHenHomNay(DateTime dt)
        {
            sqlStr = "SELECT COUNT(*) AS SoLichHen FROM UngTuyen u " +
                "INNER JOIN ThongTinCTy t ON u.EmailCongTy = t.Email " +
                "INNER JOIN DangTinTuyenDung d ON u.EmailCongTy = d.Email AND u.TieuDe = d.TieuDe " +
                "WHERE EmailUngVien = @EmailUngVien AND " +
                "YEAR(LichHen) = @Nam AND MONTH(LichHen) = @Thang AND DAY(LichHen) = @Day";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStr, connection);
                    command.Parameters.Add("@EmailUngVien", EmailDangNhap);
                    command.Parameters.Add("@Nam", dt.Year);
                    command.Parameters.Add("@Thang", dt.Month);
                    command.Parameters.Add("@Day", dt.Day);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txbHomNay.Text = reader["SoLichHen"].ToString();
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (oldBtn.BackColor != System.Drawing.Color.CadetBlue) 
            {
                oldBtn.BackColor = System.Drawing.Color.Honeydew;
            }
            System.Windows.Forms.Button clickedButton = sender as System.Windows.Forms.Button;
            oldBtn = clickedButton as System.Windows.Forms.Button;
            if (clickedButton != null)
            {
                int ngayChon;
                if (clickedButton.BackColor != System.Drawing.Color.CadetBlue)
                {
                    clickedButton.BackColor = System.Drawing.Color.SlateGray;
                }
                if (int.TryParse(clickedButton.Text, out ngayChon))
                {
                    if (clickedButton.ForeColor == System.Drawing.Color.DimGray)
                    {
                        if (ngayChon > 28 - Lich.cotNgay)
                        {
                            DateTime ngay = dtpNgay.Value.AddMonths(-1);
                            ngay = new DateTime(ngay.Year, ngay.Month, ngayChon, ngay.Hour, ngay.Minute, ngay.Second);
                            dtpNgay.Value = ngay;
                            LichTheoNgay(ngay);
                        }
                        else
                        {
                            DateTime ngay = dtpNgay.Value.AddMonths(1);
                            ngay = new DateTime(ngay.Year, ngay.Month, ngayChon, ngay.Hour, ngay.Minute, ngay.Second);
                            dtpNgay.Value = ngay;
                            LichTheoNgay(ngay);
                        }
                    }
                    else
                    {
                        DateTime ngay = dtpNgay.Value;
                        ngay = new DateTime(ngay.Year, ngay.Month, ngayChon, ngay.Hour, ngay.Minute, ngay.Second);
                        dtpNgay.Value = ngay;
                        LichTheoNgay(ngay);
                    }
                }
                
                
                
            }
        }
        
        private void LoadMatrix()
        {
            matrix = new List<List<System.Windows.Forms.Button>>();
            System.Windows.Forms.Button oldBtn = new System.Windows.Forms.Button() { Width = 0, Height = 0, Location = new Point(-Lich.width,0)};
            for (int i = 0; i < Lich.cotNgay; i++)
            {
                matrix.Add(new List<System.Windows.Forms.Button>());
                for (int j = 0; j < Lich.soNgay; j++)
                {
                    System.Windows.Forms.Button btn = new System.Windows.Forms.Button() { Width = Lich.width, Height = Lich.height};
                    btn.Location = new Point(oldBtn.Location.X + Lich.width, oldBtn.Location.Y);
                    btn.BackColor = System.Drawing.Color.Honeydew;
                    btn.Font = new Font("Cambria", 10, FontStyle.Bold);
                    btn.Click += btn_Click;
                    plHienLich.Controls.Add(btn);
                    matrix[i].Add(btn);
                    oldBtn = btn;
                }
                oldBtn = new System.Windows.Forms.Button() { Width = 0, Height = 0, Location = new Point(-Lich.width, oldBtn.Location.Y + Lich.height) };
            }
            SetDefaultDate();
        }

        private int ngayTrongTuan(DateTime ngay)
        {
            switch(ngay.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 2:
                    if ((ngay.Year % 4 == 0 && ngay.Year % 100 != 0) || ngay.Year % 400 == 0)
                        return 29;
                    else
                        return 28;
                default:
                    return 30;
            }
        }

        private void Xoa_Matrix()
        {
            for (int i = 0; i < Matrix.Count; i++)
            {
                for (int j = 0; j < Matrix[i].Count; j++)
                {
                    System.Windows.Forms.Button btn = Matrix[i][j];
                    btn.Text = "";
                    btn.BackColor = System.Drawing.Color.Honeydew;
                }
            }
        }

        private bool SoSanhNgay(DateTime a, DateTime b)
        {
            return a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;
        }

        private void BoSungNgay(DateTime date)
        {
            // Bổ sung các ngày của tháng trước và tháng sau vào các ô button còn trống
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime firstDayOfPreviousMonth = firstDayOfMonth.AddMonths(-1);
            int daysInPreviousMonth = DateTime.DaysInMonth(firstDayOfPreviousMonth.Year, firstDayOfPreviousMonth.Month);

            // Bổ sung các ngày của tháng trước
            int dayCounter = daysInPreviousMonth;
            for (int i = dateOffWeek.IndexOf(firstDayOfMonth.DayOfWeek.ToString()) - 1; i >= 0; i--)
            {
                matrix[0][i].Text = dayCounter.ToString();
                matrix[0][i].ForeColor = System.Drawing.Color.DimGray; // Đặt màu chữ cho ngày của tháng trước
                dayCounter--;
            }

            // Bổ sung các ngày của tháng sau
            int count = 1;
            for (int i = 0; i < Lich.cotNgay; i++)
            {
                for (int j = 0; j < Lich.soNgay; j++)
                {
                    if (matrix[i][j].Text == "")
                    {
                        matrix[i][j].Text = ((int)count).ToString();
                        matrix[i][j].ForeColor = System.Drawing.Color.DimGray; // Đặt màu chữ cho ngày của tháng sau
                        count++;
                    }
                }
            }
        }

        private void ThemSoLieuNgayThangNam(DateTime date)
        {
            Xoa_Matrix();
            DateTime useDate = new DateTime(date.Year, date.Month, 1);
            int hang = 0;
            for (int i = 1; i <= ngayTrongTuan(date); i++)
            {
                int cot = dateOffWeek.IndexOf(useDate.DayOfWeek.ToString());
                System.Windows.Forms.Button btn = Matrix[hang][cot];
                btn.Text = i.ToString();
                btn.ForeColor = System.Drawing.Color.Black;
                if (SoSanhNgay(useDate, date))
                {
                    btn.BackColor = System.Drawing.Color.SlateGray;
                }
                if (SoSanhNgay(useDate, DateTime.Now))
                {
                    oldBtn = btn;
                    btn.BackColor = System.Drawing.Color.CadetBlue;
                }
                
                if (cot >= 6)
                    hang++;
                useDate = useDate.AddDays(1);
            }
            BoSungNgay(date);
        }

        private void DongForm_Load(object sender, EventArgs e)
        {
            dao.MoFormCon(new FLichPhongVan(), plFormCha);
        }

        private void XemCongTy(string Email)
        {
            FCongTy ct = new FCongTy(Email);
            ct.DongForm += DongForm_Load;
            dao.MoFormCon(ct, plFormCha);
        }

        private void XemCongViec(string Email, string TieuDe)
        {
            FThongTinTuyenDung tttd = new FThongTinTuyenDung(Email, TieuDe);
            tttd.DongForm += DongForm_Load;
            dao.MoFormCon(tttd, plFormCha);
        }

        private void HienThiLich(string tieuDe, string EmailCongTy, string tenCongTy, string lichHen)
        {
            uc = new ucLichHenPhongVan();
            uc.Click_TenCongTy += XemCongTy;
            uc.Click_TenTieuDe += XemCongViec;
            uc.lblTieuDe.Text = tieuDe.ToUpper();
            uc.txbEmailCongTy.Text = EmailCongTy;
            uc.lblTenCongTy.Text = tenCongTy.ToUpper();
            uc.lblThoiGianHen.Text = lichHen;
            uc.Dock = DockStyle.Top;
            plChuaLichHen.Controls.Add(uc);
            uc.Show();
        }

        private void FLichPhongVan_Load(object sender, EventArgs e)
        {
            txbNgayHT.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LichHenHomNay(DateTime.Now);
            LichHenTuanNay(DateTime.Now);
            LoadMatrix(); // Gọi hàm LoadMatrix để tạo ma trận nút cho lịch
            SetDefaultDate(); // Đảm bảo rằng ngày mặc định được thiết lập
        }

        private void LichTheoNgay(DateTime dt)
        {
            demCuocHen = 0;
            plChuaLichHen.Controls.Clear();
            sqlStr = "SELECT * FROM UngTuyen u " +
                "INNER JOIN ThongTinCTy t ON u.EmailCongTy = t.Email " +
                "INNER JOIN DangTinTuyenDung d ON u.EmailCongTy = d.Email AND u.TieuDe = d.TieuDe " +
                "WHERE EmailUngVien = @EmailUngVien AND " +
                "YEAR(LichHen) = @Nam AND MONTH(LichHen) = @Thang AND DAY(LichHen) = @Day";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlStr, connection);
                    command.Parameters.Add("@EmailUngVien", EmailDangNhap);
                    command.Parameters.Add("@Nam", dt.Year);
                    command.Parameters.Add("@Thang", dt.Month);
                    command.Parameters.Add("@Day", dt.Day);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            demCuocHen++;
                            string tieuDe = reader["TieuDe"].ToString();
                            string EmailCongTy = reader["EmailCongTy"].ToString();
                            string tenCongTy = reader["Ten"].ToString();
                            DateTime lichHen = reader.GetDateTime(reader.GetOrdinal("LichHen"));
                            string lichHenText = lichHen.ToString("dd/MM/yyyy - hh:mm");
                            HienThiLich(tieuDe, EmailCongTy, tenCongTy, lichHenText);
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
            if (demCuocHen == 0)
            {
                KhongCoCuocHen();
            }
        }

        private void KhongCoCuocHen()
        {
            Guna2TextBox txb = new Guna2TextBox();
            txb.Text = "Bạn không có cuộc hẹn nào trong thời gian này";
            txb.Font = new Font("Cambria", 12, FontStyle.Bold);
            txb.Location = new Point(50, 15);
            txb.Size = new System.Drawing.Size(400, 50);
            txb.ForeColor = System.Drawing.Color.DimGray;
            txb.Anchor = AnchorStyles.Left & AnchorStyles.Right;
            txb.BorderThickness = 0;
            txb.ReadOnly = true;
            plChuaLichHen.Controls.Add(txb);
            txb.Show();
        }

        private void btnHomNay_Click(object sender, EventArgs e)
        {
            SetDefaultDate();
        }

        private void SetDefaultDate()
        {
            dtpNgay.Value = DateTime.Now;
        }

        private void dtpNgay_ValueChanged(object sender, EventArgs e)
        {
            Guna2DateTimePicker datePicker = sender as Guna2DateTimePicker;

            if (datePicker != null)
            {
                ThemSoLieuNgayThangNam(datePicker.Value);
                LichTheoNgay(dtpNgay.Value);
            }
        }

        private void btnThangTruoc_Click(object sender, EventArgs e)
        {
            dtpNgay.Value = dtpNgay.Value.AddMonths(-1);
        }

        private void btnThangSau_Click(object sender, EventArgs e)
        {
            dtpNgay.Value = dtpNgay.Value.AddMonths(1);
        }
    }
}
