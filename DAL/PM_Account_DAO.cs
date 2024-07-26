using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Principal;
using System.Text;

namespace SchPeoManageWeb.DAL
{
    /// <summary>
    /// 账号DAO
    /// </summary>
    public class PM_Account_DAO:BASE_DAO<MAccount>
    {
        /// <summary>
        /// 获取所有账号信息
        /// </summary>
        /// <returns></returns>
        public List<MAccount> GetAllAccounts()
        {
            SqlConnection connection = null;
            List <MAccount> accounts = new List<MAccount>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_Account";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                accounts = ConvertToList(dt);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
            finally{
                if (connection is not null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return accounts;
        }

        /// <summary>
        /// 获取启用所有账号信息
        /// </summary>
        /// <returns></returns>
        public List<MAccount> GetAllUndeletedAccounts()
        {
            SqlConnection connection = null;
            List<MAccount> accounts = new List<MAccount>();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT acc.*,COALESCE(stu.name, '') + COALESCE(tea.name, '') + COALESCE(stf.name, '') as name " +
                    "FROM PM_Account acc " +
                    "LEFT JOIN PM_Student stu ON acc.login_id = ('U'+cast(stu.student_id AS NVARCHAR)) " +
                    "LEFT JOIN PM_Teacher tea ON acc.login_id = ('T'+cast(tea.teacher_id AS NVARCHAR)) " +
                    "LEFT JOIN PM_Staff stf ON acc.login_id = ('S'+cast(stf.staff_id AS NVARCHAR)) " +
                    "WHERE acc.is_deleted=0";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                accounts = ConvertToList(dt);
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
            return accounts;
        }
        /// <summary>
        /// 根据登录账号获取账号信息【未进行密码判断】
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public MAccount GetAccountByLoginId(string loginId)
        {
            SqlConnection connection = null;
            MAccount account = new MAccount();
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "SELECT * FROM PM_Account WHERE login_id=@loginId";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("@loginId", loginId);
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                account = ConvertToModel(dt);
            }
            // 当DataTable获取到0个对象时会报异常，捕获异常并处理
            catch (Exception)
            {
                account.Message = "请输入有效的用户";
                return account;
            }
            // 关闭try中建立的连接
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return account;
        }

        /// <summary>
        /// 输入ID/状态/描述进行账号搜索【模糊搜索】
        /// </summary>
        /// <returns></returns>
        public List<MAccount> GetAccountsByRules(string? SearchStr, string Status)
        {
            List<MAccount> accounts = new List<MAccount>();
            SqlConnection connection = null;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                // 根据传入参数的判空进行语句的拼接
                string sqlstr = "SELECT acc.*,COALESCE(stu.name, '') + COALESCE(tea.name, '') + COALESCE(stf.name, '') as name " +
                    "FROM PM_Account acc " +
                    "LEFT JOIN PM_Student stu ON acc.login_id = ('U'+cast(stu.student_id AS NVARCHAR)) " +
                    "LEFT JOIN PM_Teacher tea ON acc.login_id = ('T'+cast(tea.teacher_id AS NVARCHAR)) " +
                    "LEFT JOIN PM_Staff stf ON acc.login_id = ('S'+cast(stf.staff_id AS NVARCHAR)) "+
                "WHERE ";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                // freezed永远有值所以放在最开始
                switch (Status)
                {
                    case "unselected":
                        sqlstr += "1=1 ";
                        break;
                    case "active":
                        sqlstr += "acc.is_freezed=0 AND is_deleted=0 ";
                        break;
                    case "freeze":
                        sqlstr += "acc.is_freezed=1 AND is_deleted=0 ";
                        break;
                    case "deleted":
                        sqlstr += "acc.is_deleted=1 ";
                        break;
                    default:throw new NotImplementedException();
                }
                // 如果传入了搜索内容则进行追加
                if (!string.IsNullOrEmpty(SearchStr)) 
                {
                    sqlstr += "AND acc.login_id LIKE @SearchStr OR acc.description LIKE @SearchStr ";
                    command.Parameters.AddWithValue("@SearchStr", "%" + SearchStr + "%");
                    
                }
                command.CommandText = sqlstr;
                DataTable dt = new DataTable();
                new SqlDataAdapter(command).Fill(dt);
                accounts = ConvertToList(dt);

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
            return accounts; 
        }

        public bool ResetAccPasswd(string accId)
        {
            SqlConnection connection = null;
            int affectedRows = 0;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                // 根据传入参数的判空进行语句的拼接
                string sqlstr = "UPDATE PM_Account SET password=@newPasswd,update_by='admin',update_timestamp=@datetime WHERE login_id=@accId";
                string newPasswd = accId.Length >= 5 ? accId.Substring(accId.Length - 5, 5) : accId;
                SqlCommand command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("accId", accId);
                command.Parameters.AddWithValue("newPasswd", newPasswd);
                command.Parameters.AddWithValue("datetime", DateTime.Now);
                affectedRows = command.ExecuteNonQuery();
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
            return affectedRows > 0;
        }
        /// <summary>
        /// 冻结账号的DAO层操作，输入账号ID将其is_freezed字段取反
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public bool FreezeAcc(string accId)
        {
            SqlConnection connection = null;
            int affectedRows = 0;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "UPDATE PM_Account SET is_freezed=~is_freezed,update_timestamp=@datetime WHERE login_id=@accId";
                SqlCommand command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("accId", accId);
                command.Parameters.AddWithValue("datetime", DateTime.Now);
                affectedRows = command.ExecuteNonQuery();
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
            return affectedRows > 0;
        }

        public bool DeleteAcc(string accId)
        {
            SqlConnection connection = null;
            int affectedRows = 0;
            try
            {
                connection = SqlConnectionFactory.GetSession();
                string sqlstr = "UPDATE PM_Account SET is_deleted=1,delete_by='admin',delete_timestamp=@deleteTime WHERE login_id=@accId";
                
                SqlCommand command = new SqlCommand(sqlstr, connection);
                command.Parameters.AddWithValue("accId", accId);
                command.Parameters.AddWithValue("deleteTime",DateTime.Now);

                affectedRows = command.ExecuteNonQuery();
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
            return affectedRows > 0;
        }
    }
}
