using SchPeoManageWeb.DAO;
using SchPeoManageWeb.Models;

namespace SchPeoManageWeb.Services
{
    public static class MajorService
    {
        private static readonly PM_Major_DAO _majorDAO = new PM_Major_DAO();

        /// <summary>
        /// 获取所有可用的专业列表
        /// </summary>
        /// <returns></returns>
        public static List<MMajor> GetAllActiveMajors()
        {
            List<MMajor> mMajors = new List<MMajor>();
            try
            {
                mMajors = _majorDAO.GetAllActiveMajors();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mMajors;
        }
    }
}
