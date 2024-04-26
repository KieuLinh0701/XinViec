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
using System.Windows.Media.Media3D;
using XinViec.Resources;
using XinViec.XinViec;

namespace XinViec
{
    public partial class FChonHoSoUngTuyen : Form
    {
        public event EventHandler DaLuu;
        public event EventHandler DaDang;
        private FUngTuyen fut;
        int action;
        string noiDung, ngayDang;
        string sqlStr, Email, tieuDe, tenHS;
        string EmailDangNhap = StateStorage.GetInstance().SharedValue;
        DAO dao = new DAO();
        UngTuyenDAO ungTuyenDAO = new UngTuyenDAO();
        public FChonHoSoUngTuyen(string Email, string tieuDe, int action)
        {
            InitializeComponent();
            this.Email = Email;
            this.tieuDe = tieuDe;
            this.action = action;
            Load_cbbTinhChatLoc();
        }

        public FChonHoSoUngTuyen(string Email, string noiDung, string ngayDang)
        {
            InitializeComponent();
            this.Email = Email;
            this.noiDung = noiDung;
            this.ngayDang = ngayDang;
            action = 3;
            Load_cbbTinhChatLoc();
        }

        private void Load_cbbTinhChatLoc()
        {
            cbbTinhChatLoc.Items.Clear();
            cbbDanhSachLoc.Items.Clear();
            foreach (string x in UngTuyen.listTinhChatLoc)
            {
                cbbTinhChatLoc.Items.Add(x);
            }
            cbbTinhChatLoc.Text = cbbTinhChatLoc.Items[0].ToString();
        }

        private void DongForm(object sender, EventArgs e)
        {
            this.Close();
        }

        private UngTuyen TaoDoiTuongUngTuyen()
        {
            UngTuyen ut = new UngTuyen(EmailDangNhap, tenHS, Email, tieuDe, DateTime.Now.ToString("dd/MM/yyyy - hh:mm:ss"));
            return ut;
        }

        private void UngTuyenDatabase(object sender, EventArgs e)
        {
            fut.Close();
            UngTuyen ut = TaoDoiTuongUngTuyen();
            if (ungTuyenDAO.ThemUngTuyen(ut) > 0)
            {
                this.Close();
                dao.ThongBao("Ứng tuyển thành công");
            }
        }

        private void UngTuyenCV(string tenHS)
        {
            this.tenHS = tenHS;
            fut = new FUngTuyen();
            ungTuyenDAO.MoFormUngTuyen(Email, tieuDe, fut.rtbTenCongTy);
            fut.ChonButtonXacNhan += UngTuyenDatabase;
            fut.Show();
        }

        private void cbbTinhChatLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_cbbDanhSachLoc();
        }

        private void Load_cbbDanhSachLoc()
        {
            cbbDanhSachLoc.Items.Clear();
            if (cbbTinhChatLoc.Text == UngTuyen.listTinhChatLoc[0])
            {
                cbbDanhSachLoc.Items.Add("Danh sách lọc trống");
            }
            else if (cbbTinhChatLoc.Text == UngTuyen.listTinhChatLoc[1])
            {
                List<string> list = ungTuyenDAO.LocViTriTuyenDung(EmailDangNhap);
                foreach (string x in list)
                {
                    cbbDanhSachLoc.Items.Add(x);
                }
            }
            else if(cbbTinhChatLoc.Text == UngTuyen.listTinhChatLoc[2])
            {
                List<string> list = ungTuyenDAO.LocTenHoSo(EmailDangNhap);
                foreach (string x in list)
                {
                    cbbDanhSachLoc.Items.Add(x);
                }
            }
            if (cbbDanhSachLoc.Items.Count > 0)
            {
                cbbDanhSachLoc.Text = cbbDanhSachLoc.Items[0].ToString();
            }
        }

        private void btnBoLoc_Click(object sender, EventArgs e)
        {
            dao.MoFormCon(new FChonHoSoUngTuyen(Email, tieuDe, action), plFormCha);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            plChuaHoSo.Controls.Clear();
            if (cbbTinhChatLoc.Text == UngTuyen.listTinhChatLoc[0])
            {
                ChonHoSoUngTuyen_Load(sender, e);
            }
            else if (cbbTinhChatLoc.Text == UngTuyen.listTinhChatLoc[1])
            {
                sqlStr = string.Format("SELECT tenHoSo, viTriUngTuyen, ngayCapNhat FROM HoSoXinViec WHERE EmailDangNhap = '{0}' AND viTriUngTuyen = '{1}' ORDER BY ngayCapNhat ASC", EmailDangNhap, cbbDanhSachLoc.Text);
                DocDuLieuTimKiem(sqlStr);
            }
            else if(cbbTinhChatLoc.Text == UngTuyen.listTinhChatLoc[2])
            {
                sqlStr = string.Format("SELECT tenHoSo, viTriUngTuyen FROM HoSoXinViec WHERE EmailDangNhap = '{0}' AND tenHoSo = '{1}' ORDER BY ngayCapNhat ASC", EmailDangNhap, cbbDanhSachLoc.Text);
                DocDuLieuTimKiem(sqlStr);
            }
        }

        private void DocDuLieuTimKiem(string query) 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string tenHoSo = reader["tenHoSo"].ToString();
                                string viTriUngTuyen = reader["viTriUngTuyen"].ToString();
                                HienThiHS(tenHoSo, viTriUngTuyen);
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

        private void DangTinTimViec(string tenHS)
        {
            string ngayDang = DateTime.Now.ToString("dd/MM/yyyy" + " lúc " + "HH:mm:ss");
            if (ungTuyenDAO.DangTinTimViec(EmailDangNhap, tieuDe, ngayDang, tenHS) > 0)
            {
                dao.ThongBao("Đăng tin thành công");
                this.Close();
                DaDang?.Invoke(this, EventArgs.Empty);
            }
        }

        private void XemHoSo(string tenHS)
        {
            HoSo hs = new HoSo(EmailDangNhap, tenHS);
            hs.ShowDialog();
        }

        private void ChinhSuaDangTinTimViec(string tenHS)
        {
            DangTinTimViecDAO dangTinTimViecDAO = new DangTinTimViecDAO();
            if (dangTinTimViecDAO.ChinhSuaTinTimViec(noiDung, ngayDang, EmailDangNhap, tenHS) > 0)
            {
                dao.ThongBao("Sửa thành công");
                this.Close();
                DaDang?.Invoke(this, EventArgs.Empty);
            }
        }

        private void HienThiHS(string tenHoSo, string viTriUngTuyen)
        {
            ucChonHoSoUngTuyen uc = new ucChonHoSoUngTuyen();
            uc.ChonButtonXem += XemHoSo;
            if (action == 1)
            {
                uc.ChonButtonUngTuyen += DangTinTimViec;
            } 
            else if (action == 2)
            {
                uc.ChonButtonUngTuyen += UngTuyenCV; 
            }
            else if (action == 3)
            {
                uc.ChonButtonUngTuyen += ChinhSuaDangTinTimViec;
            }
            uc.Dock = DockStyle.Top;
            uc.lblTenHoSo.Text = tenHoSo;
            uc.lblViTriUngTuyen.Text = viTriUngTuyen;
            plChuaHoSo.Controls.Add(uc);
        }

        private void ChonHoSoUngTuyen_Load(object sender, EventArgs e)
        {
            sqlStr = string.Format("SELECT tenHoSo, viTriUngTuyen, ngayCapNhat FROM HoSoXinViec WHERE EmailDangNhap = '{0}' ORDER BY ngayCapNhat ASC", EmailDangNhap);
            DocDuLieuTimKiem(sqlStr);
        }
    }
}
