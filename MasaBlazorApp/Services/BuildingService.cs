using SchPeoManageWeb.DAO;
using SchPeoManageWeb.Models;

namespace SchPeoManageWeb.Services
{
    /// <summary>
    /// 建筑信息服务
    /// </summary>
    public static class BuildingService
    {
        private static readonly PM_Building_DAO _buildingDAO = new PM_Building_DAO();

        /// <summary>
        /// 获取所有建筑信息
        /// </summary>
        /// <returns></returns>
        public static List<MBuilding> GetBuildings()
        {
            List<MBuilding> mBuildings = new List<MBuilding>();
            try
            {
                mBuildings = _buildingDAO.GetBuildings();
                mBuildings.ForEach(building => {building.Classrooms = new List<MBuilding>();});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mBuildings;
        }
    }
}
