using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
	/// <summary>
	/// 数据访问类Feedback。
	/// </summary>
	public class Feedback
	{
		public Feedback()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Feedback");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 返回长查询数据总数 （分页用到）
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from Feedback ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Feedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Feedback(");
			strSql.Append("UserName,UserTel,UserQQ,Title,Content,IsLock,AddTime,ReContent,ReTime)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@UserTel,@UserQQ,@Title,@Content,@IsLock,@AddTime,@ReContent,@ReTime)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", OleDbType.VarWChar,20),
					new OleDbParameter("@UserTel", OleDbType.VarWChar,30),
					new OleDbParameter("@UserQQ", OleDbType.VarWChar,20),
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@Content", OleDbType.VarWChar),
					new OleDbParameter("@IsLock", OleDbType.Integer,4),
					new OleDbParameter("@AddTime", OleDbType.Date),
					new OleDbParameter("@ReContent", OleDbType.VarWChar),
					new OleDbParameter("@ReTime", OleDbType.Date)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.UserTel;
			parameters[2].Value = model.UserQQ;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.IsLock;
			parameters[6].Value = model.AddTime;
			parameters[7].Value = model.ReContent;
			parameters[8].Value = model.ReTime;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
		}
        /// <summary>
        /// 修改一条数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Feedback set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperOleDb.ExecuteSql(strSql.ToString());
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Feedback model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Feedback set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserTel=@UserTel,");
			strSql.Append("UserQQ=@UserQQ,");
			strSql.Append("Title=@Title,");
			strSql.Append("Content=@Content,");
			strSql.Append("IsLock=@IsLock,");
			strSql.Append("AddTime=@AddTime,");
			strSql.Append("ReContent=@ReContent,");
			strSql.Append("ReTime=@ReTime");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", OleDbType.VarWChar,20),
					new OleDbParameter("@UserTel", OleDbType.VarWChar,30),
					new OleDbParameter("@UserQQ", OleDbType.VarWChar,20),
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@Content", OleDbType.VarWChar),
					new OleDbParameter("@IsLock", OleDbType.Integer,4),
					new OleDbParameter("@AddTime", OleDbType.Date),
					new OleDbParameter("@ReContent", OleDbType.VarWChar),
					new OleDbParameter("@ReTime", OleDbType.Date),
                    new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.UserTel;
			parameters[2].Value = model.UserQQ;
			parameters[3].Value = model.Title;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.IsLock;
			parameters[6].Value = model.AddTime;
			parameters[7].Value = model.ReContent;
			parameters[8].Value = model.ReTime;
            parameters[9].Value = model.Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Feedback ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Feedback GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,UserName,UserTel,UserQQ,Title,Content,IsLock,AddTime,ReContent,ReTime from Feedback ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DtCms.Model.Feedback model=new DtCms.Model.Feedback();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				model.UserTel=ds.Tables[0].Rows[0]["UserTel"].ToString();
				model.UserQQ=ds.Tables[0].Rows[0]["UserQQ"].ToString();
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				if(ds.Tables[0].Rows[0]["IsLock"].ToString()!="")
				{
					model.IsLock=int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
				}
				model.ReContent=ds.Tables[0].Rows[0]["ReContent"].ToString();
				if(ds.Tables[0].Rows[0]["ReTime"].ToString()!="")
				{
					model.ReTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReTime"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,UserName,UserTel,UserQQ,Title,Content,IsLock,AddTime,ReContent,ReTime ");
			strSql.Append(" FROM Feedback ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,UserName,UserTel,UserQQ,Title,Content,IsLock,AddTime,ReContent,ReTime ");
			strSql.Append(" FROM Feedback ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetPageList(int pageSize, int currentPage, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            if (currentPage > 0)
            {
                int topNum = pageSize * currentPage;
                strSql.Append("select top " + pageSize + " Id,UserName,UserTel,UserQQ,Title,Content,IsLock,AddTime,ReContent,ReTime from Feedback");
                strSql.Append(" where Id not in(select top " + topNum + " Id from Feedback");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder + ")");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);
                }
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append("select top " + pageSize + " Id,UserName,UserTel,UserQQ,Title,Content,IsLock,AddTime,ReContent,ReTime from Feedback");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder);
            }
            

            return DbHelperOleDb.Query(strSql.ToString());
        }

		#endregion  成员方法
	}
}

