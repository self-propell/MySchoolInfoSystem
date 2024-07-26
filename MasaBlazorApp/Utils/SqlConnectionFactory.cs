using System;
using System.Data.SqlClient;

namespace SchPeoManageWeb.Utils
{
    public class SqlConnectionFactory
    {
        private static readonly string _connectionString;

        static SqlConnectionFactory()
        {
            // 这里设置你的数据库连接字符串
            // 例如: "Server=your_server_name;Database=your_database_name;User Id=your_username;Password=your_password;"
            _connectionString = "Server=MSI;Database=SchoolPersonnelManagementSystem;Integrated Security=True;";
        }

        public static SqlConnection GetSession()
        {
            // 创建一个新的SqlConnection对象
            var connection = new SqlConnection(_connectionString);
            try
            {
                // 尝试打开连接
                connection.Open();
            }
            catch (SqlException ex)
            {
                // 处理连接错误
                Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
                throw;
            }

            return connection;
        }
    }


}
