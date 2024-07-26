using SchPeoManageWeb.DAL;
using SchPeoManageWeb.Models;

namespace SchPeoManageWeb.Services
{
    public class SchoolService
    {
        private static readonly PM_School_DAO _schoolDAO = new PM_School_DAO();
        private SchoolService() { }

        public static List<MSchool> GetAllSchools()
        {
            List<MSchool> mSchools = new List<MSchool>();
            try
            {
                mSchools = _schoolDAO.GetAllSchools();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mSchools;

        }
    }
}
