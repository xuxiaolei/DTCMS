using System;
using System.Data;
using System.Collections.Generic;
using DtCms.Model;
namespace DtCms.BLL
{
	/// <summary>
	/// 业务逻辑类Feedback 的摘要说明。
	/// </summary>
	public class Feedback
	{
		private readonly DtCms.DAL.Feedback dal=new DtCms.DAL.Feedback();
		public Feedback()
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
		public void Add(DtCms.Model.Feedback model)
		{
			dal.Add(model);
		}

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            dal.UpdateField(Id, strValue);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Feedback model)
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

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Feedback GetModel(int Id)
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

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetPageList(int pageSize, int currentPage, string strWhere, string filedOrder)
        {
            return dal.GetPageList(pageSize, currentPage, strWhere, filedOrder);
        }

		#endregion  成员方法
	}
}

