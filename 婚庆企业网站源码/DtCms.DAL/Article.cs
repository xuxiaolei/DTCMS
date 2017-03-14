using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
	/// <summary>
	/// 数据访问类Article。
	/// </summary>
	public class Article
	{
		public Article()
		{}
		#region  成员方法
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Article");
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
            strSql.Append(" from Article ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 返回该类别下的所有记录总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="classId">类别</param>
        /// <param name="kindId">种类</param>
        /// <returns></returns>
        public int GetCount(string strWhere, int classId, int kindId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from Article ");
            strSql.Append(" where ClassId in(select Id from Channel where KindId=" + kindId + " and ClassList like '%," + classId + ",%')");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Article model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Article(");
			strSql.Append("Title,Author,Form,Keyword,Zhaiyao,ClassId,ImgUrl,Daodu,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime)");
			strSql.Append(" values (");
			strSql.Append("@Title,@Author,@Form,@Keyword,@Zhaiyao,@ClassId,@ImgUrl,@Daodu,@Content,@Click,@IsMsg,@IsTop,@IsRed,@IsHot,@IsSlide,@IsLock,@AddTime)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,200),
					new OleDbParameter("@Author", OleDbType.VarWChar,50),
					new OleDbParameter("@Form", OleDbType.VarWChar,100),
					new OleDbParameter("@Keyword", OleDbType.VarWChar,100),
					new OleDbParameter("@Zhaiyao", OleDbType.VarWChar,250),
					new OleDbParameter("@ClassId", OleDbType.Integer,4),
					new OleDbParameter("@ImgUrl", OleDbType.VarWChar,250),
					new OleDbParameter("@Daodu", OleDbType.VarWChar,250),
					new OleDbParameter("@Content", OleDbType.VarWChar),
					new OleDbParameter("@Click", OleDbType.Integer,4),
					new OleDbParameter("@IsMsg", OleDbType.Integer,4),
					new OleDbParameter("@IsTop", OleDbType.Integer,4),
					new OleDbParameter("@IsRed", OleDbType.Integer,4),
					new OleDbParameter("@IsHot", OleDbType.Integer,4),
					new OleDbParameter("@IsSlide", OleDbType.Integer,4),
					new OleDbParameter("@IsLock", OleDbType.Integer,4),
					new OleDbParameter("@AddTime", OleDbType.Date)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Author;
			parameters[2].Value = model.Form;
			parameters[3].Value = model.Keyword;
			parameters[4].Value = model.Zhaiyao;
			parameters[5].Value = model.ClassId;
			parameters[6].Value = model.ImgUrl;
			parameters[7].Value = model.Daodu;
			parameters[8].Value = model.Content;
			parameters[9].Value = model.Click;
			parameters[10].Value = model.IsMsg;
			parameters[11].Value = model.IsTop;
			parameters[12].Value = model.IsRed;
			parameters[13].Value = model.IsHot;
			parameters[14].Value = model.IsSlide;
			parameters[15].Value = model.IsLock;
			parameters[16].Value = model.AddTime;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
		}
        /// <summary>
        /// 修改一条数据
        /// </summary>
        public void UpdateField(int Id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Article set " + strValue);
            strSql.Append(" where Id=" + Id);
            DbHelperOleDb.ExecuteSql(strSql.ToString());
        }
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Article model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Article set ");
			strSql.Append("Title=@Title,");
			strSql.Append("Author=@Author,");
			strSql.Append("Form=@Form,");
			strSql.Append("Keyword=@Keyword,");
			strSql.Append("Zhaiyao=@Zhaiyao,");
			strSql.Append("ClassId=@ClassId,");
			strSql.Append("ImgUrl=@ImgUrl,");
			strSql.Append("Daodu=@Daodu,");
			strSql.Append("Content=@Content,");
			strSql.Append("Click=@Click,");
			strSql.Append("IsMsg=@IsMsg,");
			strSql.Append("IsTop=@IsTop,");
			strSql.Append("IsRed=@IsRed,");
			strSql.Append("IsHot=@IsHot,");
			strSql.Append("IsSlide=@IsSlide,");
			strSql.Append("IsLock=@IsLock,");
			strSql.Append("AddTime=@AddTime");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,200),
					new OleDbParameter("@Author", OleDbType.VarWChar,50),
					new OleDbParameter("@Form", OleDbType.VarWChar,100),
					new OleDbParameter("@Keyword", OleDbType.VarWChar,100),
					new OleDbParameter("@Zhaiyao", OleDbType.VarWChar,250),
					new OleDbParameter("@ClassId", OleDbType.Integer,4),
					new OleDbParameter("@ImgUrl", OleDbType.VarWChar,250),
					new OleDbParameter("@Daodu", OleDbType.VarWChar,250),
					new OleDbParameter("@Content", OleDbType.VarWChar),
					new OleDbParameter("@Click", OleDbType.Integer,4),
					new OleDbParameter("@IsMsg", OleDbType.Integer,4),
					new OleDbParameter("@IsTop", OleDbType.Integer,4),
					new OleDbParameter("@IsRed", OleDbType.Integer,4),
					new OleDbParameter("@IsHot", OleDbType.Integer,4),
					new OleDbParameter("@IsSlide", OleDbType.Integer,4),
					new OleDbParameter("@IsLock", OleDbType.Integer,4),
					new OleDbParameter("@AddTime", OleDbType.Date),
                    new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.Author;
			parameters[2].Value = model.Form;
			parameters[3].Value = model.Keyword;
			parameters[4].Value = model.Zhaiyao;
			parameters[5].Value = model.ClassId;
			parameters[6].Value = model.ImgUrl;
			parameters[7].Value = model.Daodu;
			parameters[8].Value = model.Content;
			parameters[9].Value = model.Click;
			parameters[10].Value = model.IsMsg;
			parameters[11].Value = model.IsTop;
			parameters[12].Value = model.IsRed;
			parameters[13].Value = model.IsHot;
			parameters[14].Value = model.IsSlide;
			parameters[15].Value = model.IsLock;
			parameters[16].Value = model.AddTime;
            parameters[17].Value = model.Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Article ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Article GetModel(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Title,Author,Form,Keyword,Zhaiyao,ClassId,ImgUrl,Daodu,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime from Article ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DtCms.Model.Article model=new DtCms.Model.Article();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				model.Author=ds.Tables[0].Rows[0]["Author"].ToString();
				model.Form=ds.Tables[0].Rows[0]["Form"].ToString();
				model.Keyword=ds.Tables[0].Rows[0]["Keyword"].ToString();
				model.Zhaiyao=ds.Tables[0].Rows[0]["Zhaiyao"].ToString();
				if(ds.Tables[0].Rows[0]["ClassId"].ToString()!="")
				{
					model.ClassId=int.Parse(ds.Tables[0].Rows[0]["ClassId"].ToString());
				}
				model.ImgUrl=ds.Tables[0].Rows[0]["ImgUrl"].ToString();
				model.Daodu=ds.Tables[0].Rows[0]["Daodu"].ToString();
				model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				if(ds.Tables[0].Rows[0]["Click"].ToString()!="")
				{
					model.Click=int.Parse(ds.Tables[0].Rows[0]["Click"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsMsg"].ToString()!="")
				{
					model.IsMsg=int.Parse(ds.Tables[0].Rows[0]["IsMsg"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsTop"].ToString()!="")
				{
					model.IsTop=int.Parse(ds.Tables[0].Rows[0]["IsTop"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsRed"].ToString()!="")
				{
					model.IsRed=int.Parse(ds.Tables[0].Rows[0]["IsRed"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsHot"].ToString()!="")
				{
					model.IsHot=int.Parse(ds.Tables[0].Rows[0]["IsHot"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsSlide"].ToString()!="")
				{
					model.IsSlide=int.Parse(ds.Tables[0].Rows[0]["IsSlide"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsLock"].ToString()!="")
				{
					model.IsLock=int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
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
			strSql.Append(" Id,Title,Author,Form,Keyword,Zhaiyao,ClassId,ImgUrl,Daodu,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime ");
			strSql.Append(" FROM Article ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得指定的类别下所有文章
        /// </summary>
        public DataSet GetList(int classId, int kindId, int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,Title,Author,Form,Keyword,Zhaiyao,ClassId,ImgUrl,Daodu,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime ");
            strSql.Append(" FROM Article");
            strSql.Append(" where ClassId in(select Id from Channel where KindId=" + kindId + " and ClassList like '%," + classId + ",%')");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
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
                strSql.Append("select top " + pageSize + " Id,Title,Author,Form,Keyword,Zhaiyao,ClassId,ImgUrl,Daodu,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime from Article");
                strSql.Append(" where Id not in(select top " + topNum + " Id from Article");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(" order by " + filedOrder + ")");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" and " + strWhere);
                }
                //5%1+a+s+p+x
                strSql.Append(" order by " + filedOrder);
            }
            else
            {
                strSql.Append("select top " + pageSize + " Id,Title,Author,Form,Keyword,Zhaiyao,ClassId,ImgUrl,Daodu,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime from Article");
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