using SchPeoManageWeb.DAO;
using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;

namespace SchPeoManageWeb.Services
{
    /// <summary>
    /// 教学班级服务
    /// </summary>
    public static class TeachingClassService
    {
        private static readonly PM_Teaching_Class_DAO _classDAO = new PM_Teaching_Class_DAO();

        
        /// <summary>
        /// 获取所有教学班级
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<MTeachingClass>> GetAllTeachingClass()
        {
            Dictionary<string, List<MTeachingClass>> classes = new Dictionary<string, List<MTeachingClass>>();
            List<MTeachingClass> mClass = new List<MTeachingClass>();
            try
            {
                mClass = _classDAO.GetAllTeachingClass();
                foreach(var m in mClass)
                {
                    if (!classes.ContainsKey(m.CourseID))
                    {
                        classes[m.CourseID] = new List<MTeachingClass>();
                    }
                    classes[m.CourseID].Add(m);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return classes;
        }

        /// <summary>
        /// 插入讲台信息
        /// </summary>
        /// <param name="mTeachingClass"></param>
        /// <returns></returns>
        public static string AddTeachingClass(MTeachingClass mTeachingClass)
        {
            string msg = null;
            mTeachingClass = AddBasicInfo.AddCreateBasicInfo(mTeachingClass);
            try
            {
                int row = _classDAO.AddTeachingClass(mTeachingClass);
                if (row != 1) msg = "插入数据库时出现错误";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            return msg;
        }
    }
}
