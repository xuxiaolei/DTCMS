using System;
using System.Collections.Generic;
using System.Data;

namespace DTcms.BLL
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public class manager_role_value
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
        private readonly DAL.manager_role_value dal;

        public manager_role_value()
        {
            dal = new DAL.manager_role_value(siteConfig.sysdatabaseprefix);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">Model.manager_role_value</param>
        /// <returns>ID</returns>
        public int Add(Model.manager_role_value model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top">数量</param>
        /// <param name="strWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>DataTable</returns>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
    }
}
