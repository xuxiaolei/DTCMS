using System;
using System.Data;
using System.Collections.Generic;

namespace DTcms.BLL
{
    // <summary>
    /// 扩展属性表
    /// </summary>
    public partial class article_attribute_value
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.article_attribute_value dal;

        public article_attribute_value()
        {
            dal = new DAL.article_attribute_value(siteConfig.sysdatabaseprefix);
        }

        /// <summary>
        /// 获得指定分类下所有文章扩展内容
        /// </summary>
        /// <param name="category_id">分类ID</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public DataSet GetList(int category_id, string field)
        {
            return dal.GetList(category_id, field);
        }
    }
}
