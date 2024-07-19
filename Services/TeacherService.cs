using Masa.Blazor;
using SchPeoManageWeb.DAO;
using SchPeoManageWeb.Models;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace SchPeoManageWeb.Services
{
    public static class TeacherService
    {
        private static readonly PM_Teacher_DAO _teacherDAO = new PM_Teacher_DAO();


        /// <summary>
        /// 得到所有教师信息
        /// </summary>
        /// <returns>教师信息列表MTeacher（List）</returns>
        public static List<MTeacher> GetAllTeachers()
        {
            List<MTeacher> mTeachers = null;
            try
            {
                mTeachers = _teacherDAO.GetAllTeachers();
                //mTeachers.ForEach(t =>
                //{
                //    t.JobTitle.Trim();
                //    t.JobPosition.Trim();
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mTeachers;
        }

        /// <summary>
        /// 得到未被删除、未办理离职的教师的信息
        /// </summary>
        /// <returns>教师信息列表MTeacher（List）</returns>
        public static List<MTeacher> GetAllActiveTeachers()
        {
            List<MTeacher> mTeachers = null;
            try
            {
                mTeachers = _teacherDAO.GetAllActiveTeachers();
                //mTeachers.ForEach(t =>
                //{
                //    t.JobTitle.Trim();
                //    t.JobPosition.Trim();
                //});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mTeachers;
        }

        /// <summary>
        /// 输入教师信息进行检索
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <param name="School"></param>
        /// <param name="Job"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public static List<MTeacher> GetTeacherByRules(string SearchStr,int SchoolId)
        {
            List<MTeacher> mTeachers = null;
            try
            {
                mTeachers = _teacherDAO.GetTeacherByRules(SearchStr, SchoolId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mTeachers;
        }

        /// <summary>
        /// 传入MTeacher，将其信息注册到教师数据库
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string? AddTeacherInfo(MTeacher mTeacher)
        {
            bool res = false;
            try
            {
                // 插入信息
                mTeacher.CreateTimestamp = DateTime.Now;
                mTeacher.ReqTime = DateTime.Now;
                mTeacher.CreateBy = "Admin";
                // 计算age
                string year = mTeacher.IdNumber.Substring(6, 4);
                string month = mTeacher.IdNumber.Substring(10, 2);
                string day = mTeacher.IdNumber.Substring(12, 2);
                string birthdateString = $"{year}-{month}-{day}";
                DateTime birthdate;
                DateTime.TryParse(birthdateString, out birthdate);
                int age = DateTime.Now.Year - birthdate.Year;
                if (DateTime.Now < birthdate.AddYears(age))
                {
                    age--;
                }
                mTeacher.Age = age;
                // 去除字符串空格
                if(!string.IsNullOrEmpty(mTeacher.JobPosition)) mTeacher.JobPosition = mTeacher.JobPosition.Trim();
                if (!string.IsNullOrEmpty(mTeacher.JobTitle)) mTeacher.JobTitle = mTeacher.JobTitle.Trim();
                // 插入数据库
                res = _teacherDAO.InsertTeacherInfo(mTeacher);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY")) return "该教职工号已被占用";
                else return (ex.Message);
            }
            if (res == true) { return null; }
            else return "发生未知错误";
        }

        /// <summary>
        /// 传入MTeacher，将其信息更新到教师数据库
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string? UpdateTeacherInfo(MTeacher mTeacher)
        {
            bool res = false;
            try
            {
                // 插入信息
                mTeacher.UpdateTimestamp = DateTime.Now;
                mTeacher.ReqTime = DateTime.Now;
                mTeacher.UpdateBy = "Admin";
                // 计算age
                string year = mTeacher.IdNumber.Substring(6, 4);
                string month = mTeacher.IdNumber.Substring(10, 2);
                string day = mTeacher.IdNumber.Substring(12, 2);
                string birthdateString = $"{year}-{month}-{day}";
                DateTime birthdate;
                DateTime.TryParse(birthdateString, out birthdate);
                int age = DateTime.Now.Year - birthdate.Year;
                if (DateTime.Now < birthdate.AddYears(age))
                {
                    age--;
                }
                mTeacher.Age = age;
                // 插入数据库
                res = _teacherDAO.UpdateTeacherInfo(mTeacher);

            }
            catch (Exception )
            {
                return "在写入数据库时发生错误";
            }
            if (res == true) { return null; }
            else return "发生未知错误";
        }

        /// <summary>
        /// 设置教师离职信息
        /// </summary>
        /// <param name="mTeacher"></param>
        /// <returns></returns>
        public static string SetResignTeacher(MTeacher mTeacher)
        {
            try
            {
                _teacherDAO.SetResignTeacher(mTeacher);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return "在修改数据库时出错";
            }
            return null; 
        }

        /// <summary>
        /// 批量设置教师离职信息
        /// </summary>
        /// <param name="mTeacher"></param>
        /// <returns></returns>
        public static string SetGroupResignTeacher(List<MTeacher> mTeachers)
        {
            try
            {
                _teacherDAO.SetGroupResignTeacher(mTeachers);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return "在修改数据库时出错";
            }
            return null;
        }

        /// <summary>
        /// 传入List，删除其匹配的教师信息
        /// </summary>
        /// <param name="mTeacher"></param>
        /// <returns></returns>
        public static string DeleteTeacher(List<MTeacher> mTeacher)
        {
            try
            {
                _teacherDAO.DeleteTeacher(mTeacher);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return "在删除教师信息时出错";
            }
            return null;
        }

        /// <summary>
        /// 获取一个可用的教职工号
        /// </summary>
        /// <returns></returns>
        public static int GetEmployeeID()
        {
            return 1;
        }
    }
}
