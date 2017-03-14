using System;
using System.Data;
using System.Collections.Generic;
using DtCms.Model;
namespace DtCms.BLL
{
	/// <summary>
	/// 业务逻辑类Channel 的摘要说明。
	/// </summary>
	public class Channel
	{
		private readonly DtCms.DAL.Channel dal=new DtCms.DAL.Channel();
		public Channel()
		{}
		#region  成员方法
        /// <summary>
        /// 取得最新插入的ID
        /// </summary>
        public int GetMaxID(string FieldName)
        {
            return dal.GetMaxID(FieldName);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

        /// <summary>
        /// 返回栏目名称
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public string GetChannelTitle(int classId)
        {
            return dal.GetChannelTitle(classId);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(DtCms.Model.Channel model)
		{
			dal.Add(model);
            return dal.GetMaxID("Id");
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Channel model)
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
		public DtCms.Model.Channel GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataTable GetList(int PId, int KId)
		{
            return dal.GetList(PId, KId);
		}

        /// <summary>
        /// 取得该栏目下的所有子栏目
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public DataSet GetChannelListByClassId(int classId)
        {
            return dal.GetChannelListByClassId(classId);
        }

		#endregion  成员方法
	}
}

