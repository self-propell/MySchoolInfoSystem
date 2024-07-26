using SchPeoManageWeb.DAL;
using SchPeoManageWeb.Models;

namespace SchPeoManageWeb.Services
{
    /// <summary>
    /// 账号服务
    /// </summary>
    public static class AccountService
    {
        private static readonly PM_Account_DAO _accountDAO = new PM_Account_DAO();
        /// <summary>
        /// 得到所有账号的List
        /// </summary>
        /// <returns>账号MAccount（List）</returns>
        public static List<MAccount> GetAllAccounts()
        {
            return _accountDAO.GetAllAccounts();
        }

        public static List<MAccount> GetAllUndeletedAccounts()
        {
            return _accountDAO.GetAllUndeletedAccounts();
        }

        /// <summary>
        /// 获取输入的登录账号ID对应的账号
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns>账号（如出错则携带Message返回）</returns>
        public static MAccount GetAccountByLoginId(string loginId)
        {
            return _accountDAO.GetAccountByLoginId(loginId);
        }
        /// <summary>
        /// 传入登陆账号和密码，检查是否合法
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static MAccount? CheckUsernameAndPassword(string username, string password)
        {
            MAccount acc = new MAccount();
            // 账号密码传入值判空，空则中断
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // 进行账号密码检查
                acc = _accountDAO.GetAccountByLoginId(username);
                // 合法，存在该账号且密码正确
                if (acc.Message == null &&
                    acc.Password.Equals(password))
                {
                    return acc;
                }
                // 密码错误时调用
                else
                {
                    if (acc.Message == null) acc.Message = "密码错误";
                    return acc;
                }
            }
            // 传入账号或密码为空
            else
            {
                acc.Message = "请输入有效的用户和密码";
                return acc;
            }
        }
        /// <summary>
        /// 组合条件搜索账号
        /// </summary>
        /// <param name="AccId"></param>
        /// <param name="Status"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public static List<MAccount> GetAccountsBySearch(string SearchStr, string Status)
        {
            List<MAccount> mAccounts = null;
            try
            {
                mAccounts = _accountDAO.GetAccountsByRules(SearchStr, Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mAccounts;
        }

        /// <summary>
        /// 根据输入的账号ID重置密码
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public static bool ResetAccPasswd(string accId)
        {
            try
            {
                _accountDAO.ResetAccPasswd(accId);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            return true;
        }
        /// <summary>
        /// 根据输入的账号ID切换冻结/解冻状态
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public static bool FreezeAcc(string accId)
        {
            try
            {
                _accountDAO.FreezeAcc(accId);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            return true;
        }
        /// <summary>
        /// 根据输入的账号ID和操作人ID进行删除操作
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public static bool DeleteAcc(string accId)
        {
            try
            {
                _accountDAO.DeleteAcc(accId);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }
            return true;
        }
    }
}
