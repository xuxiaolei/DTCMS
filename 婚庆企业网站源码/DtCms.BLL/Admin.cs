using System;
using System.Data;
using System.Collections.Generic;
using DtCms.Model;
using DtCms.Common;
namespace DtCms.BLL
{
	/// <summary>
	/// 业务逻辑类Admin 的摘要说明。
	/// </summary>
	public class Admin
	{
		private readonly DtCms.DAL.Admin dal=new DtCms.DAL.Admin();
		public Admin()
		{}
		#region  成员方法

        /// <summary>
        /// 检查登录是否成功
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public bool chkAdminLogin(string userName, string userPwd)
        {
            string a=DESEncrypt.Encrypt(userPwd);
            return dal.chkAdminLoign(userName,a );
        }

        /// <summary>
        /// 获得数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}
        public bool Exists(string UserName)
        {
            return dal.Exists(UserName);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Admin model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Admin model)
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
		public DtCms.Model.Admin GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

        /// <summary>
        /// 根据用户名取得一行数据
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DtCms.Model.Admin GetModel(string userName)
        {
            return dal.GetModel(userName);
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
		/// 获得数据列表
		/// </summary>
		public List<DtCms.Model.Admin> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<DtCms.Model.Admin> DataTableToList(DataTable dt)
		{
			List<DtCms.Model.Admin> modelList = new List<DtCms.Model.Admin>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				DtCms.Model.Admin model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new DtCms.Model.Admin();
					if(dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					model.UserName=dt.Rows[n]["UserName"].ToString();
					model.UserPwd=dt.Rows[n]["UserPwd"].ToString();
					model.ReadName=dt.Rows[n]["ReadName"].ToString();
					model.UserEmail=dt.Rows[n]["UserEmail"].ToString();
					if(dt.Rows[n]["UserType"].ToString()!="")
					{
						model.UserType=int.Parse(dt.Rows[n]["UserType"].ToString());
					}
					model.UserLevel=dt.Rows[n]["UserLevel"].ToString();
					if(dt.Rows[n]["IsLock"].ToString()!="")
					{
						model.IsLock=int.Parse(dt.Rows[n]["IsLock"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  成员方法
	}
}

