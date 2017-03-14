using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
	/// <summary>
	/// 数据访问类Contents。
	/// </summary>
	public class Contents
	{
		public Contents()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Contents");
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
            strSql.Append(" from Contents ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Contents model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Contents(");
			strSql.Append("Title,ClassId,Content,SortId)");
			strSql.Append(" values (");
			strSql.Append("@Title,@ClassId,@Content,@SortId)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@ClassId", OleDbType.Integer,4),
					new OleDbParameter("@Content", OleDbType.VarWChar),
					new OleDbParameter("@SortId", OleDbType.Integer,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.ClassId;
			parameters[2].Value = model.Content;
			parameters[3].Value = model.SortId;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Contents model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Contents set ");
			strSql.Append("Title=@Title,");
			strSql.Append("ClassId=@ClassId,");
			strSql.Append("Content=@Content,");
			strSql.Append("SortId=@SortId");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@ClassId", OleDbType.Integer,4),
					new OleDbParameter("@Content", OleDbType.VarWChar),
					new OleDbParameter("@SortId", OleDbType.Integer,4),
                    new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.ClassId;
			parameters[2].Value = model.Content;
			parameters[3].Value = model.SortId;
            parameters[4].Value = model.Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Contents ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Contents GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Title,ClassId,Content,SortId from Contents ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DtCms.Model.Contents model=new DtCms.Model.Contents();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				if(ds.Tables[0].Rows[0]["ClassId"].ToString()!="")
				{
					model.ClassId=int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
				}
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				if(ds.Tables[0].Rows[0]["SortId"].ToString()!="")
				{
					model.SortId=int.Parse(ds.Tables[0].Rows[0]["SortId"].ToString());
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
			strSql.Append("select Id,Title,ClassId,Content,SortId ");
			strSql.Append(" FROM Contents ");
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
			strSql.Append(" Id,Title,ClassId,Content,SortId ");
			strSql.Append(" FROM Contents ");
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
                strSql.Append("select top " + pageSize + " Id,Title,ClassId,Content,SortId from Contents");
                strSql.Append(" where Id not in(select top " + topNum + " Id from Contents");
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
                strSql.Append("select top " + pageSize + " Id,Title,ClassId,Content,SortId from Contents");
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

