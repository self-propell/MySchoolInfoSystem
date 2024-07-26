using SchPeoManageWeb.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchPeoManageWeb.Services.Tests
{
    [TestClass()]
    public class DictServiceTests
    {
        [TestMethod()]
        public void GetJobTitlesTest()
        {

            System.Console.Write(DictService.GetJobTitles());
        }

        [TestMethod()]
        public void GetDataByNameTest()
        {
            foreach (var item in DictService.GetDataByName("课程性质"))
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}