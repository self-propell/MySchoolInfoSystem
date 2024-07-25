using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;

namespace SchPeoManageWeb.DAO
{
    /// <summary>
    /// 教室信息DAO
    /// </summary>
    public class PM_Classroom_DAO:BASE_DAO<MClassroom>
    {
        /// <summary>
        /// 获取PM_classroom中所有建筑信息
        /// </summary>
        /// <returns></returns>
        public List<MClassroom> GetClassrooms()
        {
            SqlConnection connection = null;
            List<MClassroom> _classroom = new List<MClassroom>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_Classroom";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                _classroom = ConvertToList(dt);
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
            return _classroom;
        }
    }
}
