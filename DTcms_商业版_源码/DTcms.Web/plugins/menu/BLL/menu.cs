using System;
using System.Collections.Generic;
using System.Data;

namespace DTcms.Web.Plugin.Menu.BLL
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public class menu
    {
        private readonly DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.menu dal;

        public menu()
        {
            dal = new DAL.menu(siteConfig.sysdatabaseprefix);
        }

        #region 按ID号查询是否存在记录
        /// <summary>
        /// 按ID号查询是否存在记录
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }
        #endregion

        #region 按名称查询是否存在记录
        /// <summary>
        /// 按名称查询是否存在记录
        /// </summary>
        /// <param name="title">名称</param>
        /// <returns></returns>
        public bool Exists(string title)
        {
            return dal.Exists(title);
        }
        #endregion

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.menu model)
        {
            return dal.Add(model);
        }
        #endregion

        #region 修改一列数据
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.menu model)
        {
            return dal.Update(model);
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
        #endregion

        #region 按ID号查询标题
        /// <summary>
        /// 按ID号查询标题
        /// </summary>
        /// <param name="id">ID号</param>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }
        #endregion

        #region 按条件查询数据总数
        /// <summary>
        /// 按条件查询数据总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }
        #endregion

        #region 返回一个实体
        /// <summary>
        /// 返回一个实体
        /// </summary>
        /// <returns>Modle</returns>
        public Model.menu GetModel(int id)
        {
            return dal.GetModel(id);
        }
        #endregion

        #region 获得前几行数据
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top">数量</param>
        /// <param name="strWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int Top, string strWhere, string filedOrder, int parent_id)
        {
            return dal.GetList(Top, strWhere, filedOrder, parent_id);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        public DataTable GetList(int Top, int parent_id, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, parent_id, strWhere, filedOrder);
        }
        #endregion
    }
}