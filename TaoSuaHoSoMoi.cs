using Guna.UI2.WinForms;
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
using XinViec.XinViec;

namespace XinViec
{
    public partial class TaoSuaHoSoMoi : Form
    {
        private HoSoXinViec hs;
        string sqlStr;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        string tenHS;
        DAO dao = new DAO();

        public TaoSuaHoSoMoi(HoSoXinViec parentForm)
        {
            InitializeComponent();
            hs = parentForm;
            hs.TaoHSMoi += TaoMoi;
            hs.SuaHS += HandleSuaHS;
        }

        private void TaoMoi()
        {
            TaoHoSoMoi();
        }

        private void TaoHoSoMoi()
        {
            btnLuu.Visible = true;
            btnLuuThayDoi.Visible = false;
            btnLuuHoSoMoi.Visible = false;
            btnHuyThayDoi.Visible = false;
        }

        private void LuuHoSoMoi()
        {
            btnLuu.Visible = false;
            btnLuuThayDoi.Visible = true;
            btnLuuHoSoMoi.Visible = true;
            btnHuyThayDoi.Visible = true;
        }

        private void HandleSuaHS(string tenHS)
        {
            this.tenHS = tenHS;
            LuuHoSoMoi();
            sqlStr = "SELECT viTriUngTuyen, mucTieuNgheNghiep, kinhNghiemLamViec, soThich, kyNang FROM HoSoXinViec WHERE EmailDangNhap = @EmailDangNhap AND tenHoSo = @TenHS";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                        command.Parameters.AddWithValue("@TenHS", tenHS);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                txbViTriUngTuyen.Text = reader["viTriUngTuyen"].ToString();
                                txbMucTieuNgheNghiep.Text = reader["mucTieuNgheNghiep"].ToString();
                                txbKinhNghiemLamViec.Text = reader["kinhNghiemLamViec"].ToString();
                                txbSoThich.Text = reader["soThich"].ToString();
                                txbKyNang.Text = reader["kyNang"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lỗi khi lấy dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void DaLuu (object sender, EventArgs e)
        {
            LuuHoSoMoi();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Trước khi mở LuuHoSoMoi kiểm tra xem điều kiện của việc nhập của người dùng đã đúng chưa 
            LuuHoSoMoi luu = new LuuHoSoMoi(txbViTriUngTuyen, txbMucTieuNgheNghiep, txbKinhNghiemLamViec, txbSoThich, txbKyNang);
            luu.Show();
            luu.DaLuu += DaLuu;
        }

        private void LuuThayDoi(object sender, EventArgs e)
        {
            dao.tb.Close();
            sqlStr = "UPDATE HoSoXinViec SET ngayCapNhat = @ngayCapNhat, viTriUngTuyen = @viTriUngTuyen, " +
                "mucTieuNgheNghiep = @mucTieuNgheNghiep, kinhNghiemLamViec = @kinhNghiemLamViec, " +
                "kyNang = @kyNang, soThich = @soThich " +
                "WHERE tenHoSo = @tenHoSo AND EmailDangNhap = @EmailDangNhap";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@EmailDangNhap", EmailDangNhap);
                        command.Parameters.AddWithValue("@TenHoSo", tenHS);
                        command.Parameters.AddWithValue("@ngayCapNhat", DateTime.Now);
                        command.Parameters.AddWithValue("@viTriUngTuyen", txbViTriUngTuyen.Text);
                        command.Parameters.AddWithValue("@mucTieuNgheNghiep", txbMucTieuNgheNghiep.Text);
                        command.Parameters.AddWithValue("@kinhNghiemLamViec", txbKinhNghiemLamViec.Text);
                        command.Parameters.AddWithValue("@kyNang", txbKyNang.Text);
                        command.Parameters.AddWithValue("@soThich", txbSoThich.Text);
                        // Execute the INSERT query
                        int k = command.ExecuteNonQuery();

                        // Check if rows were affected
                        if (k > 0)
                        {
                            dao.ThongBao("Lưu thành công");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dao.BaoLoi("Lưu thất bại: " + ex.Message);
            }
        }

        private void btnLuuThayDoi_Click(object sender, EventArgs e)
        {
            dao.ThongBao_LuaChon("Bạn muốn lưu thay đổi?", LuuThayDoi);
        }

        private void HuyThayDoi(object sender, EventArgs e)
        {
            dao.tb.Close();
            HandleSuaHS(tenHS);
        }

        private void btnHuyThayDoi_Click(object sender, EventArgs e)
        {
            dao.ThongBao_LuaChon("Bạn muốn hủy thay đổi?", HuyThayDoi);
        }

        private void btnLuuHoSoMoi_Click(object sender, EventArgs e)
        {
            // Trước khi mở LuuHoSoMoi kiểm tra xem điều kiện của việc nhập của người dùng đã đúng chưa 
            LuuHoSoMoi luu = new LuuHoSoMoi(txbViTriUngTuyen, txbMucTieuNgheNghiep, txbKinhNghiemLamViec, txbSoThich, txbKyNang);
            luu.Show();
            // Nếu điều kiện là lưu thành công thì sẽ gửi từ luuHoSoMoi và kích hoạt bên dưới
            LuuHoSoMoi();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            dao.MoFormCon(new HoSoXinViec(), plFormCha);
        }
    }
}
