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
    public partial class FCongTy : Form
    {
        public event EventHandler DongForm;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        FGioiThieuCongTy gioiThieu;
        FNhanPhanHoi phanHoi;
        FTinTuyenDung tinTuyenDung;
        string EmailCongTy;
        public FCongTy(String EmailCongTy)
        {
            InitializeComponent();
            this.EmailCongTy = EmailCongTy;
        }

        private void MoForm(Form form)
        {
            form.TopLevel = false;
            plFormCha.Controls.Clear();
            plFormCha.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();
        }
        private void FCongTy_Load(object sender, EventArgs e)
        {
            gioiThieu = new FGioiThieuCongTy();
            MoForm(gioiThieu);

            sqlStr = string.Format("SELECT * FROM ThongTinCTy WHERE Email = @Email");

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlStr, conn);
                command.Parameters.AddWithValue("@Email", EmailCongTy);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        btnDiaChi.Text = "  " + reader["DiaChi"].ToString();
                        btnSDT.Text = "  " + reader["SDT"].ToString();
                        btnEmail.Text = "  " + reader["Email"].ToString();
                        lblTenCongTy.Text = "  " + reader["Ten"].ToString().ToUpper();

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally { conn.Close(); }

        }

        private void btnGioiThieu_Click(object sender, EventArgs e)
        {
            gioiThieu = new FGioiThieuCongTy();
            MoForm(gioiThieu);
        }

        private void btnPhanHoi_Click(object sender, EventArgs e)
        {
            phanHoi = new FNhanPhanHoi();
            MoForm(phanHoi);
        }

        private void btnTinTuyenDung_Click(object sender, EventArgs e)
        {
            tinTuyenDung = new FTinTuyenDung(EmailCongTy);
            MoForm(tinTuyenDung);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
            DongForm?.Invoke(this, e);
        }
    }
}
