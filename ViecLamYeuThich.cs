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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XinViec
{
    public partial class ViecLamYeuThich : Form
    {
        DAO dao = new DAO();
        string sqlStr;
        int value = 0;
        bool hienThi = true;
        ucViecLamYeuThich uc;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        YeuThichDAO yeuThichDAO = new YeuThichDAO();

        public ViecLamYeuThich()
        {
            InitializeComponent();
        }

        private YeuThich TaoYeuThich(string EmailCongTy, string tieuDe, string ngayThem)
        {
            YeuThich yt = new YeuThich(EmailCongTy, tieuDe, EmailDangNhap, ngayThem);
            return yt;
        }

        private void BoYeuThich(string EmailCongTy, string tieuDe)
        {
            YeuThich yt = TaoYeuThich(EmailCongTy, tieuDe, "");
            if (yeuThichDAO.XoaYeuThich(yt) == true)
            {
                if (rbtnLuuGanNhat.Checked == true)
                {
                    rbtnLuuGanNhat_CheckedChanged(this, EventArgs.Empty);
                }
                else
                {
                    radioLuongCaoNhat_CheckedChanged(this, EventArgs.Empty);
                }
            }
        }

        private void UngTuyen(string EmailCongTy, string tieuDe)
        {
            FThongTinTuyenDung tttd = new FThongTinTuyenDung(EmailCongTy, tieuDe,1);
            tttd.DongForm += DongForm_Load;
            dao.MoFormCon(tttd, plFormCha);
        }

        private void DongForm_Load(object sender, EventArgs e)
        {
            dao.MoFormCon(new ViecLamYeuThich(), plFormCha);
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

        private void HienThi_LichSu(string tieuDe, string tenCongTy, string Email, string mucLuong, string viTriTuyenDung, string ngayLuu, string ngayHetHan, string khuVuc)
        {
            uc = new ucViecLamYeuThich();
            uc.ChonBtnBoYeuThich += BoYeuThich;
            uc.ChonBtnUngTuyen += UngTuyen;
            uc.ChonlblTieuDe += XemCongViec;
            uc.ChonlblTenCongTy += XemCongTy;
            uc.txbEmailCongTy.Text = Email;
            uc.txbNgayHetHan.Text = khuVuc;
            uc.txbMucLuong.Text = mucLuong.ToUpper();
            uc.lblTieuDe.Text = tieuDe.ToUpper();
            uc.lblTenCongTy.Text = tenCongTy.ToUpper();
            uc.lblViTriTuyenDung.Text = viTriTuyenDung.ToUpper();
            uc.lblNgayLuu.Text = ngayLuu;
            uc.txbNgayHetHan.Text = "Ngày hết hạn: " + ngayHetHan;
            uc.txbKhuVuc.Text = khuVuc;
            plChuaHoSo.Controls.Add(uc);
            if (hienThi == true)
            {
                uc.Location = new Point(45, value);
                hienThi = false;
            }
            else
            {
                uc.Location = new Point(460, value);
                value += 150;
                hienThi |= true;
            }
            uc.Show();
        }

        private void ViecLamYeuThich_Load(object sender, EventArgs e)
        {
            rbtnLuuGanNhat.Checked = true;
            rbtnLuuGanNhat_CheckedChanged(sender, e);
        }

        private void rbtnLuuGanNhat_CheckedChanged(object sender, EventArgs e)
        {
            sqlStr = "SELECT * FROM YeuThich y INNER JOIN DangTinTuyenDung d ON d.Email = y.EmailCongTy AND d.TieuDe = y.TieuDe INNER JOIN ThongTinCTy T ON y.EmailCongTy = t.Email AND y.TieuDe = d.TieuDe WHERE EmailUngVien = @EmailUngVien ORDER BY NgayThem DESC";
            Load_DuLieu(sqlStr);
        }

        private void radioLuongCaoNhat_CheckedChanged(object sender, EventArgs e)
        {
            sqlStr = "SELECT * FROM YeuThich y INNER JOIN DangTinTuyenDung d ON d.Email = y.EmailCongTy AND d.TieuDe = y.TieuDe INNER JOIN ThongTinCTy T ON y.EmailCongTy = t.Email AND y.TieuDe = d.TieuDe WHERE EmailUngVien = @EmailUngVien ORDER BY MucLuong DESC";
            Load_DuLieu(sqlStr);
        }

        private void Load_DuLieu(string query)
        {
            HienThiSoLuongCV();
            value = 0;
            hienThi = true;
            plChuaHoSo.Controls.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmailUngVien", EmailDangNhap);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tieuDe = reader["TieuDe"].ToString();
                                string tenCongTy = reader["Ten"].ToString();
                                string Email = reader["EmailCongTy"].ToString();
                                string mucLuong = reader["MucLuong"].ToString();
                                string viTriTuyenDung = reader["TenCongViec"].ToString();
                                string ngayLuu = reader["NgayThem"].ToString();
                                string DiaChi = reader["DiaChi"].ToString();
                                DateTime ngayHetHanDateTime = (DateTime)reader["NgayHetHan"];
                                string ngayHetHan = ngayHetHanDateTime.ToString("dd/MM/yyyy - hh:mm");

                                if (!string.IsNullOrEmpty(DiaChi))
                                {
                                    string[] diaChiParts = DiaChi.Split(',');
                                    if (diaChiParts.Length >= 4)
                                    {
                                        HienThi_LichSu(tieuDe, tenCongTy, Email, mucLuong, viTriTuyenDung, ngayLuu, ngayHetHan, diaChiParts[3]);
                                    }
                                }
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

        private void HienThiSoLuongCV()
        {
            lblSoCVDuocLuu.Text = yeuThichDAO.HienThiSoLuongCV(EmailDangNhap).ToString();
        }
    }
}
