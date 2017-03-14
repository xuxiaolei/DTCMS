using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 图片列表
    /// </summary>
    public partial class article_albums
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.article_albums dal;
        public article_albums()
        {
            dal = new DAL.article_albums(siteConfig.sysdatabaseprefix);
        }

        #region 方法
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetImagesList(int article_id)
        {
            return dal.GetImagesList(article_id);
        }
        #endregion
    }
}
