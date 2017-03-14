using System;
using System.Collections.Generic;
using System.Text;
using DtCms.Common;
using cmsModel = DtCms.Model;

namespace DtCms.DAL
{
    public class WebSet
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        /// <param name="configFilePath"></param>
        /// <returns></returns>
        public cmsModel.WebSet loadConfig(string configFilePath)
        {
            return (cmsModel.WebSet)SerializationHelper.Load(typeof(cmsModel.WebSet), configFilePath);
        }

        public cmsModel.WebSet saveConifg(cmsModel.WebSet mode, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(mode, configFilePath);
                //DtCms.Dal.Providers.webSetProvider.SetInstance(mode);
            }
            return mode;
        }
    }
}
