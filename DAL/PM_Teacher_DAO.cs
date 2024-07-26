using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;
using SchPeoManageWeb.Components.Pages.UniversityAdmin;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.FileSystemGlobbing;
using Masa.Blazor;

namespace SchPeoManageWeb.DAO
{
    /// <summary>
    /// 教师DAO
    /// </summary>
    public class PM_Teacher_DAO : BASE_DAO<MTeacher>
    {
        /// <summary>
        /// 获取所有教师信息
        /// </summary>
        /// <returns></returns>
        public List<MTeacher> GetAllTeachers()
        {
            SqlConnection connection = null;
            List<MTeacher> teachers = new List<MTeacher>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                // 这条语句获取教师信息【学院信息保存为ID，没有进行连接查询】
                string sqlstr = "SELECT * FROM PM_Teacher tc LEFT JOIN PM_School sc ON tc.school_id=sc.school_id";
                ////获取教师信息，同时获取学院名称【左连接学院表】
                //string sqlstr = "SELECT t.*,s.school_name FROM PM_Teacher t LEFT JOIN PM_School s ON t.school_id=s.school_id WHERE t.is_deleted=0 AND t.is_departed=0";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                teachers = ConvertToList(dt);
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
            return teachers;
        }


        /// <summary>
        /// 获取所有未离职、未删除教师信息
        /// </summary>
        /// <returns></returns>
        public List<MTeacher> GetAllActiveTeachers()
        {
            SqlConnection connection = null;
            List<MTeacher> teachers = new List<MTeacher>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                // 这条语句获取教师信息【学院信息保存为ID，没有进行连接查询】
                string sqlstr = "SELECT * FROM PM_Teacher tc LEFT JOIN PM_School sc ON tc.school_id=sc.school_id WHERE tc.is_deleted=0 AND tc.is_departed=0";
                ////获取教师信息，同时获取学院名称【左连接学院表】
                //string sqlstr = "SELECT t.*,s.school_name FROM PM_Teacher t LEFT JOIN PM_School s ON t.school_id=s.school_id WHERE t.is_deleted=0 AND t.is_departed=0";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                teachers = ConvertToList(dt);
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
            return teachers;
        }

        /// <summary>
        /// 根据搜索信息获取教师列表
        /// </summary>
        /// <param name="SearchStr"></param>
        /// <param name="SchoolId"></param>
        /// <returns></returns>
        public List<MTeacher> GetTeacherByRules(string SearchStr,int SchoolId)
        {
            SqlConnection connection = null;
            List<MTeacher> mTeachers = new List<MTeacher>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                
                string sqlstr = "SELECT tec.*,sch.school_name FROM PM_Teacher tec LEFT JOIN PM_SCHOOL sch " +
                    "ON tec.school_id=sch.school_id WHERE 1=1";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                if (SchoolId != 0)
                {
                    sqlstr += "AND sch.school_id=@SchoolId";
                    command.Parameters.AddWithValue("@SchoolId", SchoolId);

                }
                if (!string.IsNullOrEmpty(SearchStr))
                {
                    sqlstr+= "AND (tec.name LIKE @param " +
                    "OR tec.phone_number LIKE @param " +
                    "OR tec.email LIKE @param " +
                    "OR sch.school_name LIKE @param)";
                    command.Parameters.AddWithValue("@param", "%" + SearchStr + "%");
                }

                command.CommandText = sqlstr;
                
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                mTeachers = ConvertToList(dt);
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
            return mTeachers;
        }

        /// <summary>
        /// 插入教师信息
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public bool InsertTeacherInfo(MTeacher teacher)
        {
            SqlConnection connection = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "INSERT INTO PM_Teacher (employee_id,name,sex,id_number,age,phone_number,email," +
                    "enrollment_date,expiry_date,job_title,job_position,school_id,is_budgeted_posts,is_departed," +
                    "description,create_by,create_timestamp,update_by,update_timestamp,is_deleted) " +
                    "VALUES (@EmployeeID,@name,@sex,@id_number,@age,@phone_number,@email," +
                    "@enrollment_date,@expiry_date,@job_title,@job_position,@school_id,@is_budgeted_posts,@is_departed," +
                    "@description,@create_by,@create_timestamp,@update_by,@update_timestamp,0);";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("@EmployeeID", teacher.EmployeeID);
                command.Parameters.AddWithValue("@name", teacher.Name);
                command.Parameters.AddWithValue("@sex", teacher.Sex ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id_number", teacher.IdNumber);
                command.Parameters.AddWithValue("@age", teacher.Age);
                command.Parameters.AddWithValue("@phone_number", teacher.PhoneNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@email", teacher.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@enrollment_date", teacher.EnrollmentDate==DateTime.MinValue? (object)DBNull.Value : teacher.EnrollmentDate);
                command.Parameters.AddWithValue("@expiry_date", teacher.ExpiryDate == DateTime.MinValue ? (object)DBNull.Value : teacher.ExpiryDate);
                command.Parameters.AddWithValue("@job_title", teacher.JobTitle?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@job_position", teacher.JobPosition ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@school_id", teacher.SchoolId);
                command.Parameters.AddWithValue("@is_budgeted_posts", teacher.IsBudgetedPosts);
                command.Parameters.AddWithValue("@is_departed", teacher.IsDeparted);
                command.Parameters.AddWithValue("@description", teacher.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@create_by", teacher.CreateBy);
                command.Parameters.AddWithValue("@create_timestamp", teacher.CreateTimestamp);
                command.Parameters.AddWithValue("@update_by", teacher.CreateBy);
                command.Parameters.AddWithValue("@update_timestamp", teacher.CreateTimestamp);
                command.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// 传入MTeacher，将其信息更新到数据库中
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public bool UpdateTeacherInfo(MTeacher teacher)
        {
            SqlConnection connection = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "UPDATE PM_Teacher " +
                "SET " +
                " employee_id=@EmployeeID, " +
                " name = @name, " +
                " sex = @sex, " +
                " id_number = @id_number, " +
                " age = @age, " +
                " phone_number = @phone_number, " +
                " email = @email, " +
                " enrollment_date = @enrollment_date, " +
                " expiry_date = @expiry_date, " +
                " job_title = @job_title, " +
                " job_position = @job_position, " +
                " school_id = @school_id, " +
                " is_budgeted_posts = @is_budgeted_posts, " +
                " is_departed = @is_departed, " +
                " description = @description, " +
                " update_by = @update_by, " +
                " update_timestamp = @update_timestamp" +
                " WHERE teacher_id=@teacher_id;";

                SqlCommand command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("@EmployeeID", teacher.EmployeeID);
                command.Parameters.AddWithValue("@name", teacher.Name);
                command.Parameters.AddWithValue("@sex", teacher.Sex ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@id_number", teacher.IdNumber);
                command.Parameters.AddWithValue("@age", teacher.Age);
                command.Parameters.AddWithValue("@phone_number", teacher.PhoneNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@email", teacher.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@enrollment_date", teacher.EnrollmentDate == DateTime.MinValue ? (object)DBNull.Value : teacher.EnrollmentDate);
                command.Parameters.AddWithValue("@expiry_date", teacher.ExpiryDate == DateTime.MinValue ? (object)DBNull.Value : teacher.ExpiryDate);
                command.Parameters.AddWithValue("@job_title", teacher.JobTitle ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@job_position", teacher.JobPosition ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@school_id", teacher.SchoolId);
                command.Parameters.AddWithValue("@is_budgeted_posts", teacher.IsBudgetedPosts);
                command.Parameters.AddWithValue("@is_departed", teacher.IsDeparted);
                command.Parameters.AddWithValue("@description", teacher.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@update_by", teacher.UpdateBy);
                command.Parameters.AddWithValue("@update_timestamp", teacher.UpdateTimestamp);
                command.Parameters.AddWithValue("@teacher_id", teacher.TeacherId);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        /// <summary>
        /// 传入教师信息设置教师离职
        /// </summary>
        /// <param name="mTeacher"></param>
        /// <returns></returns>
        public bool SetResignTeacher(MTeacher mTeacher)
        {
            SqlConnection connection = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "UPDATE PM_Teacher " +
                "SET departed_date=@depDate,departed_reason=@depReason,description=@des,update_by=@updateBy,update_timestamp=@updateTime";


                if (mTeacher.DepartedDate <= DateTime.Now)
                {
                    sqlstr += ",is_departed=1 ";
                }
                sqlstr += " WHERE teacher_id=@teacherID";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("@depDate", mTeacher.DepartedDate);
                command.Parameters.AddWithValue("@des", mTeacher.Description??(Object)DBNull.Value);
                command.Parameters.AddWithValue("@depReason", mTeacher.DepartedReason);
                command.Parameters.AddWithValue("@teacherID", mTeacher.TeacherId);
                command.Parameters.AddWithValue("@updateBy","Admin");
                command.Parameters.AddWithValue("@updateTime", DateTime.Now);
                command.ExecuteNonQuery();
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
            return true;
        }

        /// <summary>
        /// 批量传入教师信息设置教师离职
        /// </summary>
        /// <param name="mTeacher"></param>
        /// <returns></returns>
        public bool SetGroupResignTeacher(List<MTeacher> mTeachers)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                transaction = connection.BeginTransaction();
                string sqlstr = "UPDATE PM_Teacher " +
                "SET departed_date=@depDate,departed_reason=@depReason,description=@des,update_by=@updateBy,update_timestamp=@updateTime";
                foreach (MTeacher mt in mTeachers)
                {
                    string psqlstr = sqlstr;
                    if (mt.DepartedDate <= DateTime.Now)
                    {
                        psqlstr += ",is_departed=1 ";
                    }
                    psqlstr += " WHERE teacher_id=@teacherID";
                    SqlCommand command = new SqlCommand(psqlstr, connection,transaction);
                    command.Parameters.AddWithValue("@depDate", mt.DepartedDate);
                    command.Parameters.AddWithValue("@depReason", mt.DepartedReason);
                    command.Parameters.AddWithValue("@teacherID", mt.TeacherId);
                    command.Parameters.AddWithValue("@des", mt.Description??(Object)DBNull.Value);
                    command.Parameters.AddWithValue("@updateBy", "Admin");
                    command.Parameters.AddWithValue("@updateTime", DateTime.Now);
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                if(transaction!=null) transaction.Rollback();
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
            return true;
        }

        /// <summary>
        /// 删除教师信息【标记is_delete=1】
        /// </summary>
        /// <param name="mTeachers"></param>
        /// <returns></returns>
        public int DeleteTeacher(List<MTeacher> mTeachers)
        {
            SqlConnection connection = null;
            SqlTransaction transaction = null;
            int res = 0;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                transaction = connection.BeginTransaction();
                string sqlstr = "UPDATE PM_Teacher " +
                "SET is_deleted=1,delete_by=@deleteBy,delete_timestamp=@deleteTime,delete_reason=@deleteReason,description=@des " +
                "WHERE teacher_id=@teacherID";
                
                
                foreach(MTeacher m in mTeachers)
                {
                    SqlCommand command = new SqlCommand(sqlstr, connection, transaction);
                    command.Parameters.AddWithValue("@teacherID", m.TeacherId);
                    command.Parameters.AddWithValue("@deleteBy","Admin");
                    command.Parameters.AddWithValue("@des", m.Description ?? (Object)DBNull.Value);
                    
                    command.Parameters.AddWithValue("@deleteTime", DateTime.Now);
                    command.Parameters.AddWithValue("@deleteReason", m.DeleteReason);
                    res+=command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                if(transaction != null) transaction.Rollback();
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
    }
}
