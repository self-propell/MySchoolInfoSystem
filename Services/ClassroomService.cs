using SchPeoManageWeb.DAO;
using SchPeoManageWeb.Models;

namespace SchPeoManageWeb.Services
{
    /// <summary>
    /// 建筑信息服务
    /// </summary>
    public static class ClassroomService
    {
        private static readonly PM_Classroom_DAO _classroomDAO = new PM_Classroom_DAO();

        /// <summary>
        /// 获取所有建筑信息
        /// </summary>
        /// <returns></returns>
        public static List<MClassroom> GetClassrooms()
        {
            List<MClassroom> mClassrooms = new List<MClassroom>();
            try
            {
                mClassrooms = _classroomDAO.GetClassrooms();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mClassrooms;
        }
    }
}
