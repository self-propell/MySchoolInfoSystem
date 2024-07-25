using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;

namespace SchPeoManageWeb.DAO
{
    /// <summary>
    /// 建筑信息DAO
    /// </summary>
    public class PM_Building_DAO:BASE_DAO<MBuilding>
    {
        /// <summary>
        /// 获取PM_Building中所有建筑信息
        /// </summary>
        /// <returns></returns>
        public List<MBuilding> GetBuildings()
        {
            SqlConnection connection = null;
            List<MBuilding> _building = new List<MBuilding>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_Building";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                _building = ConvertToList(dt);
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
            return _building;
        }
    }
}
