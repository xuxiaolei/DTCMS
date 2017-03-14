using System;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.Web.Plugin.Service.BLL
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public partial class install
    {
        private readonly DAL.install dal;

        public install()
        {
            dal = new DAL.install();
        }

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.install loadConfig(string config_path)
        {
            string cacheName = "gs_cache_service_config";
            Model.install model = CacheHelper.Get<Model.install>(cacheName);
            if (model == null)
            {
                CacheHelper.Insert(cacheName, dal.loadConfig(Utils.GetMapPath(config_path)), Utils.GetMapPath(config_path));
                model = CacheHelper.Get<Model.install>(cacheName);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.install saveConifg(Model.install model, string config_path)
        {
            return dal.saveConifg(model, Utils.GetMapPath(config_path));
        }
    }
}
