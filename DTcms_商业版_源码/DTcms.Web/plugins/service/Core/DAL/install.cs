using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.Plugin.Service.DAL
{
    /// <summary>
    /// 数据访问类:配置文件
    /// </summary>
    public partial class install
    {
        private static object lockHelper = new object();

        public install()
        {
        }

        #region 扩展设置参数
        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.install loadConfig(string configFilePath)
        {
            return (Model.install)SerializationHelper.Load(typeof(Model.install), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.install saveConifg(Model.install model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }
        #endregion
    }
}
