using SchPeoManageWeb.Models;

namespace SchPeoManageWeb.Utils
{
    public static class AddBasicInfo
    {
        /// <summary>
        /// 添加创建时需要的基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T AddCreateBasicInfo<T>(T model)where T : BasicModel
        {
            model.ReqTime= DateTime.Now;
            model.UpdateTimestamp= DateTime.Now;
            model.CreateTimestamp= DateTime.Now;
            //目前写死只有管理
            model.UpdateBy = "admin";
            model.CreateBy="admin";
            return model;
        }

        /// <summary>
        /// 添加更新时需要的基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T AddUpdateBasicInfo<T>(T model) where T : BasicModel
        {
            model.ReqTime = DateTime.Now;
            model.UpdateTimestamp = DateTime.Now;
            //目前写死只有管理
            model.UpdateBy = "admin";
            return model;
        }

        /// <summary>
        /// 添加删除时需要的基础信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static T AddDeleteBasicInfo<T>(T model) where T : BasicModel
        {
            model.ReqTime = DateTime.Now;
            model.UpdateTimestamp = DateTime.Now;
            model.IsDeleted = true;
            model.DeleteTimestamp = DateTime.Now;
            //目前写死只有管理
            model.UpdateBy = "admin";
            model.DeleteBy = "admin";
            return model;
        }
    }
}
