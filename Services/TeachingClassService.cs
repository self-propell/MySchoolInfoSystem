using SchPeoManageWeb.DAL;
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
        /// 获取所有教学班级[以课程为KEY分类]
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<MTeachingClass>> GetAllTeachingClassGroupByCourse()
        {
            Dictionary<string, List<MTeachingClass>> classes = new Dictionary<string, List<MTeachingClass>>();
            List<MTeachingClass> mClass = new List<MTeachingClass>();
            try
            {
                mClass = _classDAO.GetAllTeachingClass();
                foreach (var m in mClass)
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
        /// 获取所有教学班级[以教师为KEY分类]
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, List<MTeachingClass>> GetAllTeachingClassGroupByTeacher()
        {
            Dictionary<string, List<MTeachingClass>> classes = new Dictionary<string, List<MTeachingClass>>();
            List<MTeachingClass> mClass = new List<MTeachingClass>();
            try
            {
                mClass = _classDAO.GetAllTeachingClass();
                foreach (var m in mClass)
                {
                    if (!classes.ContainsKey(m.TeacherID))
                    {
                        classes[m.TeacherID] = new List<MTeachingClass>();
                    }
                    classes[m.TeacherID].Add(m);
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
            if (_classDAO.CheckClassByPMID(mTeachingClass.ClassPMID)!=null)
            {
                return "讲台编号出现冲突";
            }
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

        /// <summary>
        /// 删除讲台信息【list】
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
        public static string DeleteTeachingClass(List<MTeachingClass> classes)
        {
            string msg = null;
            foreach (MTeachingClass m in classes)
            {
                AddBasicInfo.AddDeleteBasicInfo(m);
            }
            try
            {
                _classDAO.DeleteTeachingClass(classes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// 更新讲台信息
        /// </summary>
        /// <param name="mTeachingClass"></param>
        /// <returns></returns>
        public static string UpdateTeachingClassInfo (MTeachingClass mTeachingClass)
        {
            string msg = null;
            mTeachingClass = AddBasicInfo.AddUpdateBasicInfo(mTeachingClass);
            if (_classDAO.CheckClassByPMID(mTeachingClass.ClassPMID).ClassID!=mTeachingClass.ClassID)
            {
                return "讲台编号出现冲突";
            }
            try
            {
                _classDAO.UpdateTeachingClassInfo(mTeachingClass);
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
