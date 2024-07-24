using SchPeoManageWeb.Components.Pages.UniversityAdmin;
using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data;
using System.Data.SqlClient;

namespace SchPeoManageWeb.DAO
{
    /// <summary>
    /// 课程DAO
    /// </summary>
    public class PM_Course_DAO:BASE_DAO<MCourse>
    {
        /// <summary>
        /// 获取数据库中所有课程信息
        /// </summary>
        /// <returns></returns>
        public List<MCourse> GetAllCourses()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            List<MCourse> courses = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_Course pc LEFT JOIN PM_Major mj ON pc.major_id=mj.major_id";
                command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                courses = ConvertToList(dt);
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
            return courses;
        }

        /// <summary>
        /// 获取数据库中所有未删除的课程信息
        /// </summary>
        /// <returns></returns>
        public List<MCourse> GetAllActiveCourses()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            List<MCourse> courses = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_Course pc LEFT JOIN PM_Major mj ON pc.major_id=mj.major_id WHERE pc.is_deleted=0";
                command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                courses = ConvertToList(dt);
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
            return courses;
        }

        /// <summary>
        /// 根据PMID查询课程信息
        /// </summary>
        /// <returns></returns>
        public MCourse GetCourseByPMID(string  pmid)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            MCourse course = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT TOP 1 * FROM PM_Course pc WHERE pc.course_pmid=@pmid";
                command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("@pmid",pmid);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                if(dt.Rows.Count!=0)course = ConvertToModel(dt);
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
            return course;
        }

        /// <summary>
        /// 添加课程信息
        /// </summary>
        /// <param name="course"></param>
        /// <returns>错误信息</returns>
        public int AddCourse(MCourse course)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int rows=0;
            try
            {
                connection = SqlConnectionFactory.GetSession();

                List<SqlParameter> parameters;
                string sqlstr = BASE_DAO<MCourse>.GenerateInsertQuery(course, out parameters,"PM_Course");
                command = new SqlCommand(sqlstr, connection);
                // 添加填充后的参数
                command.Parameters.AddRange(parameters.ToArray());
                rows = command.ExecuteNonQuery();
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
            return rows;
        }

        /// <summary>
        /// 删除课程信息【标记is_delete=1】
        /// </summary>
        /// <param name="mCourses"></param>
        /// <returns>受影响的行数</returns>
        public int DeleteCourse(List<MCourse> mCourses)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            int res = 0;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                transaction = connection.BeginTransaction();
                string sqlstr = "UPDATE PM_Course " +
                "SET update_by=@updateBy,update_timestamp=@updateTime,is_deleted=1,delete_by=@deleteBy,delete_timestamp=@deleteTime,delete_reason=@deleteReason,description=@des " +
                "WHERE course_id=@courseID";


                foreach (MCourse m in mCourses)
                {
                    SqlCommand command = new SqlCommand(sqlstr, connection, transaction);
                    command.Parameters.AddWithValue("@courseID", m.CourseID);
                    command.Parameters.AddWithValue("@des", m.Description ?? (Object)DBNull.Value);
                    command.Parameters.AddWithValue("@updateBy", "Admin");
                    command.Parameters.AddWithValue("@updateTime", DateTime.Now);
                    command.Parameters.AddWithValue("@deleteBy", "Admin");
                    command.Parameters.AddWithValue("@deleteTime", DateTime.Now);
                    command.Parameters.AddWithValue("@deleteReason", m.DeleteReason);
                    res += command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null) transaction.Rollback();
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
            return res;
        }

        /// <summary>
        /// 更新课程信息
        /// </summary>
        /// <param name="mCourse"></param>
        public void UpdateCourseInfo(MCourse mCourse)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlTransaction transaction = null;
            int res = 0;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                List<SqlParameter> parameters;
                string sqlstr = GenerateUpdateQuery(mCourse, out parameters, "PM_Course");
                transaction = connection.BeginTransaction();
                command = new SqlCommand(sqlstr, connection,transaction);
                command.Parameters.AddRange(parameters.ToArray());
                res = command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception) 
            {
                if(transaction!=null)transaction.Rollback();
                throw; 
            }
            finally
            {
                
                if(connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return;
        }
    }
}
