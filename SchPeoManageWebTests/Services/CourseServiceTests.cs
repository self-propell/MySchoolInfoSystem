using Masa.Blazor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchPeoManageWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchPeoManageWeb.Models;

namespace SchPeoManageWeb.Services.Tests
{
    [TestClass()]
    public class CourseServiceTests
    {
        [TestMethod()]
        public void AddCourseTest()
        {
            MCourse course = new MCourse();
            course.CourseNature = "专业选修";
            course.CoursePMID = "C10001";
            course.CourseName = "Test";
            course.MajorID= 1;
            course.ClassHour = 48;
            course.OpenSemester = "2024-2025-1";
            course.CreateTimestamp = DateTime.Now;
            course.UpdateTimestamp = DateTime.Now;
            course.UpdateBy = "system";
            course.CreateBy = "system";
            CourseService.AddCourse(course);
        }
    }
}