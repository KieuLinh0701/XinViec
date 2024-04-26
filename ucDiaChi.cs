using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinViec
{
    public partial class ucDiaChi : UserControl
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.stringConn);
        string sqlStr;
        public ucDiaChi()
        {
            InitializeComponent();
        }

        private void Load_Huyen(string tenTinh)
        {
            cbbHuyen.Items.Clear(); // Xóa dữ liệu cũ trước khi load dữ liệu mới
            string sqlStr = "SELECT DISTINCT tenHuyen FROM Huyen WHERE tenTinh = @tenTinh";

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@tenTinh", tenTinh);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string value = reader["tenHuyen"].ToString();
                                cbbHuyen.Items.Add(value);
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

        private void Load_Tinh()
        {
            cbbTinh.Items.Clear();
            string sqlStr = "SELECT DISTINCT tenTinh FROM Tinh"; // Thay đổi YourTableName thành tên bảng thích hợp

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string value = reader["tenTinh"].ToString(); 
                                cbbTinh.Items.Add(value);
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

        private void ucDiaChi_Load(object sender, EventArgs e)
        {
            Load_Tinh();
        }

        private void cbbTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTinh = cbbTinh.SelectedItem.ToString();
            Load_Huyen(selectedTinh);
        }

        private void Load_Xa(string tenHuyen)
        {
            cbbXa.Items.Clear(); // Xóa dữ liệu cũ trước khi load dữ liệu mới
            sqlStr = "SELECT DISTINCT tenXa FROM Xa INNER JOIN Huyen ON Xa.maHuyen = Huyen.maHuyen WHERE tenHuyen = @tenHuyen";

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.stringConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlStr, connection))
                    {
                        command.Parameters.AddWithValue("@tenHuyen", tenHuyen);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string value = reader["tenXa"].ToString();
                                cbbXa.Items.Add(value);
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

        private void cbbHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHuyen = cbbHuyen.SelectedItem.ToString();
            Load_Xa(selectedHuyen);
        }
    }
}
