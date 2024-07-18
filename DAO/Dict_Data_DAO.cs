using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;

namespace SchPeoManageWeb.DAO
{
    public class Dict_Data_DAO:BASE_DAO<MData>
    {
        /// <summary>
        /// 获取所有学院信息
        /// </summary>
        /// <returns></returns>
        public List<MData> GetJobTitles()
        {
            SqlConnection connection = null;
            List<MData> _titles = new List<MData>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM DICT_DATA WHERE type=(SELECT type FROM DICT_TYPE WHERE name LIKE '%job_title%')";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                _titles = ConvertToList(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (connection is not null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return _titles;
        }
    }
}
