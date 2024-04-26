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
    
    public partial class FTinTuyenDung : Form
    {

        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        string Email = StateStorage.GetInstance().SharedValue;
        string EmailCongTy;
        int kiemTra = 1;
        public FTinTuyenDung(string EmailCongTy)
        {
            InitializeComponent();
            this.EmailCongTy = EmailCongTy;
        }

        private void themUCTinTuyenDung(Panel plUCHienTai, string tieuDe, string loaiHinhCV, string mucLuong, DateTime ngayHetHan)
        {
            UCTinTuyenDung uc = new UCTinTuyenDung();
            uc.Dock = DockStyle.Top;
            uc.lblTieuDe.Text = tieuDe;
            uc.lblLoaiHinhCongViec.Text = loaiHinhCV;
            uc.lblMucLuong.Text = mucLuong;
            uc.lblNgayHetHan.Text = ngayHetHan.Day + "/" + ngayHetHan.Month + "/" + ngayHetHan.Year;

            plUCHienTai.Controls.Add(uc);
        }

        private void FTinTuyenDung_Load(object sender, EventArgs e)
        {
            sqlStr = "Select TieuDe, NgayHetHan, LoaiHinhCongViec, MucLuong from DangTinTuyenDung Where Email = @Email Order by NgayDang DESC";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@Email", EmailCongTy);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tieuDe = reader["TieuDe"].ToString();
                                string loaiHinhCV = reader["LoaiHinhCongViec"].ToString();
                                string mucLuong = reader["MucLuong"].ToString();
                                DateTime ngayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"));
                                if (kiemTra == 1)
                                {
                                    themUCTinTuyenDung(plUCTrai, tieuDe, loaiHinhCV, mucLuong, ngayHetHan);
                                    kiemTra = 0;
                                }
                                else
                                {
                                    themUCTinTuyenDung(plUCPhai, tieuDe, loaiHinhCV, mucLuong, ngayHetHan);
                                    kiemTra = 1;
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                plUCTrai.VerticalScroll.Value = e.NewValue;
                plUCPhai.VerticalScroll.Value = e.NewValue;
            }
        }
    }
    

    
}
