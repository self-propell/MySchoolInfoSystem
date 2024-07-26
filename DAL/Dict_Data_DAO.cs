using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;

namespace SchPeoManageWeb.DAL
{
    /// <summary>
    /// 数据字典DAO
    /// </summary>
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
                string sqlstr = "SELECT * FROM DICT_DATA WHERE type=(SELECT type FROM DICT_TYPE WHERE name LIKE '%职称%')";
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

        /// <summary>
        /// 根据类型名查询DictData信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<MData> GetDataByName(string name)
        {
            SqlConnection connection = null;
            List<MData> _data = new List<MData>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM Dict_Data dd LEFT JOIN Dict_Type dt on dt.type=dd.type where dt.Name LIKE @name";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                command.Parameters.AddWithValue("@name", "%" + name + "%");
                new SqlDataAdapter(command).Fill(dt);
                _data = ConvertToList(dt);
            }
            catch (Exception) { throw; }
            finally
            {
                if (connection is not null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return _data;
        }
    }
}
