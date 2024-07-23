
using SchPeoManageWeb.Components.Pages.UniversityAdmin;
using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data;
using System.Data.SqlClient;

namespace SchPeoManageWeb.DAO
{
    /// <summary>
    /// 教学班级DAO
    /// </summary>
    public class PM_Teaching_Class_DAO:BASE_DAO<MTeachingClass>
    {
        /// <summary>
        /// 获取所有教学班级
        /// </summary>
        /// <returns></returns>
        public List<MTeachingClass> GetAllTeachingClass()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            List<MTeachingClass> list = new List<MTeachingClass>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SElECT class.*,teacher_name=teacher.name,course_name=course.course_name FROM PM_Teaching_Class class " +
                    "LEFT JOIN PM_Course course ON class.course_id=course.course_pmid " +
                    "LEFT JOIN PM_Teacher teacher ON class.teacher_id=teacher.employee_id";
                command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                list = ConvertToList(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return list;
        }
        public int AddTeachingClass(MTeachingClass teachingClass)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int rows = 0;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                List<SqlParameter> parameters;
                string sqlstr = GenerateInsertQuery(teachingClass, out parameters, "PM_Teaching_Class");
                command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddRange(parameters.ToArray());
                command.ExecuteNonQuery();
                rows = command.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally
            {
                if(connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return rows;
        }
    }
}
