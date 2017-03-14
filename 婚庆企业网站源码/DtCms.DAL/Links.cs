using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
	/// <summary>
	/// 数据访问类Links。
	/// </summary>
	public class Links
	{
		public Links()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Links");
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
            strSql.Append(" from Links ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Links model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Links(");
			strSql.Append("Title,UserName,UserTel,UserMail,WebUrl,ImgUrl,IsImage,IsRed,IsLock,SortId,AddTime)");
			strSql.Append(" values (");
            strSql.Append("@Title,@UserName,@UserTel,@UserMail,@WebUrl,@ImgUrl,@IsImage,@IsRed,@IsLock,@SortId,@AddTime)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@UserName", OleDbType.VarWChar,20),
					new OleDbParameter("@UserTel", OleDbType.VarWChar,30),
					new OleDbParameter("@UserMail", OleDbType.VarWChar,30),
					new OleDbParameter("@WebUrl", OleDbType.VarWChar,250),
					new OleDbParameter("@ImgUrl", OleDbType.VarWChar,250),
					new OleDbParameter("@IsImage", OleDbType.Integer,4),
                    new OleDbParameter("@IsRed", OleDbType.Integer,4),
					new OleDbParameter("@IsLock", OleDbType.Integer,4),
					new OleDbParameter("@SortId", OleDbType.Integer,4),
					new OleDbParameter("@AddTime", OleDbType.Date)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.UserTel;
			parameters[3].Value = model.UserMail;
			parameters[4].Value = model.WebUrl;
			parameters[5].Value = model.ImgUrl;
			parameters[6].Value = model.IsImage;
            parameters[7].Value = model.IsRed;
			parameters[8].Value = model.IsLock;
			parameters[9].Value = model.SortId;
			parameters[10].Value = model.AddTime;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Links model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Links set ");
			strSql.Append("Title=@Title,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserTel=@UserTel,");
			strSql.Append("UserMail=@UserMail,");
			strSql.Append("WebUrl=@WebUrl,");
			strSql.Append("ImgUrl=@ImgUrl,");
			strSql.Append("IsImage=@IsImage,");
            strSql.Append("IsRed=@IsRed,");
			strSql.Append("IsLock=@IsLock,");
			strSql.Append("SortId=@SortId,");
			strSql.Append("AddTime=@AddTime");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@UserName", OleDbType.VarWChar,20),
					new OleDbParameter("@UserTel", OleDbType.VarWChar,30),
					new OleDbParameter("@UserMail", OleDbType.VarWChar,30),
					new OleDbParameter("@WebUrl", OleDbType.VarWChar,250),
					new OleDbParameter("@ImgUrl", OleDbType.VarWChar,250),
					new OleDbParameter("@IsImage", OleDbType.Integer,4),
                    new OleDbParameter("@IsRed", OleDbType.Integer,4),
					new OleDbParameter("@IsLock", OleDbType.Integer,4),
					new OleDbParameter("@SortId", OleDbType.Integer,4),
					new OleDbParameter("@AddTime", OleDbType.Date),
                    new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.UserTel;
			parameters[3].Value = model.UserMail;
			parameters[4].Value = model.WebUrl;
			parameters[5].Value = model.ImgUrl;
			parameters[6].Value = model.IsImage;
            parameters[7].Value = model.IsRed;
			parameters[8].Value = model.IsLock;
			parameters[9].Value = model.SortId;
			parameters[10].Value = model.AddTime;
            parameters[11].Value = model.Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Links ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Links GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Title,UserName,UserTel,UserMail,WebUrl,ImgUrl,IsImage,IsRed,IsLock,SortId,AddTime from Links ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DtCms.Model.Links model=new DtCms.Model.Links();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				model.UserTel=ds.Tables[0].Rows[0]["UserTel"].ToString();
				model.UserMail=ds.Tables[0].Rows[0]["UserMail"].ToString();
				model.WebUrl=ds.Tables[0].Rows[0]["WebUrl"].ToString();
				model.ImgUrl=ds.Tables[0].Rows[0]["ImgUrl"].ToString();
				if(ds.Tables[0].Rows[0]["IsImage"].ToString()!="")
				{
					model.IsImage=int.Parse(ds.Tables[0].Rows[0]["IsImage"].ToString());
				}
                if (ds.Tables[0].Rows[0]["IsRed"].ToString() != "")
                {
                    model.IsRed = int.Parse(ds.Tables[0].Rows[0]["IsRed"].ToString());
                }
				if(ds.Tables[0].Rows[0]["IsLock"].ToString()!="")
				{
					model.IsLock=int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SortId"].ToString()!="")
				{
					model.SortId=int.Parse(ds.Tables[0].Rows[0]["SortId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
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
			strSql.Append("select Id,Title,UserName,UserTel,UserMail,WebUrl,ImgUrl,IsImage,IsRed,IsLock,SortId,AddTime ");
			strSql.Append(" FROM Links ");
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
			strSql.Append(" Id,Title,UserName,UserTel,UserMail,WebUrl,ImgUrl,IsImage,IsRed,IsLock,SortId,AddTime ");
			strSql.Append(" FROM Links ");
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
                strSql.Append("select top " + pageSize + " Id,Title,UserName,UserTel,UserMail,WebUrl,ImgUrl,IsImage,IsRed,IsLock,SortId,AddTime from Links");
                strSql.Append(" where Id not in(select top " + topNum + " Id from Links");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder + ")");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);//5|1|a|s|p|x
                }
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append("select top " + pageSize + " Id,Title,UserName,UserTel,UserMail,WebUrl,ImgUrl,IsImage,IsRed,IsLock,SortId,AddTime from Links");
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

