using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;

namespace SchPeoManageWeb.DAL
{
    /// <summary>
    /// 学院DAO
    /// </summary>
    public class PM_School_DAO:BASE_DAO<MSchool>
    {
        /// <summary>
        /// 获取所有学院信息
        /// </summary>
        /// <returns></returns>
        public List<MSchool> GetAllSchools()
        {
            SqlConnection connection = null;
            List<MSchool> schools = new List<MSchool>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_School";
             
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                schools = ConvertToList(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection is not null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return schools;
        }
    }
}
