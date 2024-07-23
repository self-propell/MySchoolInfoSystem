using Masa.Blazor;
using Newtonsoft.Json.Converters;
using SchPeoManageWeb.DAO;
using SchPeoManageWeb.Models;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace SchPeoManageWeb.Services
{
    public static class CourseService
    {
        private static readonly PM_Course_DAO _courseDAO = new PM_Course_DAO();


        /// <summary>
        /// 得到所有课程信息
        /// </summary>
        /// <returns>课程信息列表MCourse（List）</returns>
        public static List<MCourse> GetAllCourses()
        {
            List<MCourse> mCourses = null;
            try
            {
                mCourses = _courseDAO.GetAllCourses();
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
            return mCourses;
        }

        /// <summary>
        /// 得到所有未删除的课程信息
        /// </summary>
        /// <returns>课程信息列表MCourse（List）</returns>
        public static List<MCourse> GetAllActiveCourses()
        {
            List<MCourse> mCourses = null;
            try
            {
                mCourses = _courseDAO.GetAllActiveCourses();
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
            return mCourses;
        }

        /// <summary>
        /// 添加课程信息
        /// </summary>
        /// <param name="mCourse"></param>
        /// <returns>报错信息【无报错为null】</returns>
        public static string AddCourse(MCourse mCourse)
        {
            if (_courseDAO.GetCourseByPMID(mCourse.CoursePMID))
            {
                return "课程号出现冲突";
            }
            int res = 0;
            string msg = null;
            mCourse.CreateTimestamp = DateTime.Now;
            mCourse.UpdateTimestamp = DateTime.Now;
            mCourse.CreateBy = "admin";
            mCourse.UpdateBy = "admin";
            try
            {
                res = _courseDAO.AddCourse(mCourse);
                if (res == 0)
                {
                    msg = "添加失败";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            return msg;
        }

        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="mCourse"></param>
        /// <returns>报错信息【无报错信息则为null】</returns>
        public static string DeleteCourse(List<MCourse> mCourse)
        {
            string msg = null;
            try
            {
                _courseDAO.DeleteCourse(mCourse);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            return msg;
        }

        
        public static string UpdateCourseInfo(MCourse mCourse)
        {
            string msg = null;
            try
            {
                mCourse.UpdateBy = "admin";
                mCourse.UpdateTimestamp = DateTime.Now;
                _courseDAO.UpdateCourseInfo(mCourse);
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
