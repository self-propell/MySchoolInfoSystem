using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data;
using System.Data.SqlClient;

namespace SchPeoManageWeb.DAL
{
    /// <summary>
    /// 教学班级DAO
    /// </summary>
    public class PM_Teaching_Class_DAO:BASE_DAO<MTeachingClass>
    {
        /// <summary>
        /// 获取所有教学班级[joined teacher、course，获取了Course_name/Teacher_name]
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
        
        /// <summary>
        /// 添加教学班级
        /// </summary>
        /// <param name="teachingClass"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 删除教学班级
        /// </summary>
        /// <param name="teachingClass"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int DeleteTeachingClass(List<MTeachingClass> teachingClass)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            int rows = 0;
            SqlTransaction transaction = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                transaction = connection.BeginTransaction();
                foreach (var item in teachingClass)
                {
                    List<SqlParameter> parameters;
                    string sqlstr = GenerateDeleteQuery(item, out parameters, "PM_Teaching_Class");
                    command = new SqlCommand(sqlstr, connection,transaction);
                    command.Parameters.AddRange(parameters.ToArray());
                    rows += command.ExecuteNonQuery();
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
            if (rows != teachingClass.Count)
            {
                transaction.Rollback();
                throw new Exception("执行数据库操作时出错，操作已回滚");
            }
            return rows;
        }

        /// <summary>
        /// 查找传入的PMID是否已经使用过
        /// </summary>
        /// <param name="classPMID"></param>
        /// <returns></returns>
        public MTeachingClass CheckClassByPMID(string classPMID)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            MTeachingClass mTeachingClass = null;
            int find;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT TOP 1 * FROM PM_Teaching_Class tc WHERE tc.class_pmid=@pmid";
                command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("@pmid", classPMID);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                if(dt.Rows.Count!=0)mTeachingClass = ConvertToModel(dt);

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
            return mTeachingClass;
        }

        /// <summary>
        /// 根据传入的对象更新数据库
        /// </summary>
        /// <param name="teachingClass"></param>
        public void UpdateTeachingClassInfo(MTeachingClass teachingClass)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlTransaction transaction = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                transaction = connection.BeginTransaction();
                List<SqlParameter> parameters;
                string sqlstr = GenerateUpdateQuery(teachingClass, out parameters, "PM_Teaching_Class");
                command = new SqlCommand(sqlstr, connection, transaction);
                command.Parameters.AddRange(parameters.ToArray());
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
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
