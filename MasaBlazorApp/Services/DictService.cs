using SchPeoManageWeb.DAO;
using SchPeoManageWeb.Models;
using SchPeoManageWeb.Utils;
using System.Data.SqlClient;
using System.Data;

namespace SchPeoManageWeb.Services
{
    public static class DictService
    {
        private static readonly Dict_Data_DAO _dataDAO = new Dict_Data_DAO();

        /// <summary>
        /// 获取职称信息
        /// </summary>
        /// <returns></returns>
        public static List<MData> GetJobTitles()
        {
            List<MData> mDatas = new List<MData>();
            //List<string> _jobTitles = new List<string>();
            try
            {
                mDatas = _dataDAO.GetJobTitles();
                foreach (MData data in mDatas)
                {
                    data.Name = data.Name.Trim();
                }
            }
            catch(Exception ex) {System.Console.WriteLine(ex.Message);}
            return mDatas;
        }

        //
        public static List<MData> GetDataByName(string name)
        {
            name = name.Trim();
            List<MData> mDatas=new List<MData>();
            try
            {
                mDatas = _dataDAO.GetDataByName(name);
                foreach (MData data in mDatas)
                {
                    data.Name = data.Name.Trim();
                }
            }
            catch (Exception ex) { System.Console.WriteLine(ex.Message); }
            return mDatas;
        }
    }
}
