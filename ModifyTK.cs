using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace XinViec
{
    class ModifyTK
    {
        public ModifyTK() 
        { 
            
        }
       
        SqlCommand sqlCommand;  //dùng để truy vấn các câu lệnh insert, update, delete,.....
        SqlDataReader dataReader; //dùng để đọc dữ liệu trong bảng
        public List<TKhoan> TKhoans(string query ) 
        {
            List<TKhoan> tKhoans = new List<TKhoan>();

            using(SqlConnection sqlConn = ConnectionTK.GetSqlConnection())
            {
                sqlConn.Open();
                sqlCommand = new SqlCommand(query, sqlConn);
                dataReader = sqlCommand.ExecuteReader();
                while( dataReader.Read()) 
                {
                 
                    tKhoans.Add(new TKhoan(dataReader.GetString(0), dataReader.GetString(1), dataReader.GetString(2)));

                }

                sqlConn.Close();
            }    

            return tKhoans;
        }
        public void Command(string query)//dung de dang ky tai khoan
        {
            using (SqlConnection sqlConn = ConnectionTK.GetSqlConnection())
            {
                sqlConn.Open();
                sqlCommand = new SqlCommand(query, sqlConn);
                sqlCommand.ExecuteNonQuery();//thuc thi cau truy van
                sqlConn.Close();
            }    
        }
    }
}
