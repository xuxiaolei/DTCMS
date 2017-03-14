using System;
using System.Data;

namespace DtCms.BLL
{
    /// <summary>
    /// 广告位业务逻辑类
    /// </summary>
    public class Advertising
    {
        private readonly DtCms.DAL.Advertising dal = new DtCms.DAL.Advertising();
        public Advertising()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

        /// <summary>
        /// 返回查询数据总数（分页用到）
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(DtCms.Model.Advertising model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(DtCms.Model.Advertising model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            dal.Delete(Id);
        }

        //5*1*a*s*p*x
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DtCms.Model.Advertising GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}

		#endregion  成员方法
    }
}
