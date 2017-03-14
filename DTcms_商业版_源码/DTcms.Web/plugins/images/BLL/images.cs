using System;
using System.Collections.Generic;
using System.Data;

namespace DTcms.Web.Plugin.Images.BLL
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public class images
    {
        private readonly DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.images dal;

        public images()
        {
            dal = new DAL.images(siteConfig.sysdatabaseprefix);
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
        public int Add(Model.images model)
        {
            return dal.Add(model);
        }
        #endregion

        #region 更新一列数据
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.images model)
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
        public Model.images GetModel(int id)
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <param name="page">页码</param>
        /// <param name="pagesize">分页数</param>
        /// <param name="recordCount">返回 数据总数</param>
        /// <returns>DataTable</returns>
        public DataSet GetList(string strWhere, string filedOrder, int pageIndex, int pageSize, out int recordCount)
        {
            return dal.GetList(strWhere, filedOrder, pageIndex, pageSize, out recordCount);
        }
        #endregion

        #region 扩展
        /// <summary>
        /// 获取标记列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetSign()
        {
            return dal.GetSign();
        }
        #endregion
    }
}