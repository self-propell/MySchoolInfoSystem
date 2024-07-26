using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;

namespace SchPeoManageWeb.DAL
{
    /// <summary>
    /// 专业DAO
    /// </summary>
    public class PM_Major_DAO:BASE_DAO<MMajor>
    {
        /// <summary>
        /// 获取所有启用的专业列表
        /// </summary>
        /// <returns></returns>
        public List<MMajor> GetAllActiveMajors()
        {
            SqlConnection connection = null;
            List<MMajor> majors = new List<MMajor>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_Major";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                majors = ConvertToList(dt);
            }
            catch (Exception) { throw; }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return majors;
        }
    }
}
