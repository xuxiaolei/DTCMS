using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
	/// <summary>
	/// 数据访问类Products。
	/// </summary>
	public class Products
	{
		public Products()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Products");
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
            strSql.Append(" from Products ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int Id, string strField)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Products set " + strField);
            strSql.Append(" where Id=@Id ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
            parameters[0].Value = Id;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Products model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Products(");
			strSql.Append("Title,ClassId,Guige,Xinghao,Price,ImgUrl,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime)");
			strSql.Append(" values (");
			strSql.Append("@Title,@ClassId,@Guige,@Xinghao,@Price,@ImgUrl,@Content,@Click,@IsMsg,@IsTop,@IsRed,@IsHot,@IsSlide,@IsLock,@AddTime)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@ClassId", OleDbType.Integer,4),
					new OleDbParameter("@Guige", OleDbType.VarWChar,50),
					new OleDbParameter("@Xinghao", OleDbType.VarWChar,50),
					new OleDbParameter("@Price", OleDbType.Decimal,9),
					new OleDbParameter("@ImgUrl", OleDbType.VarWChar,250),
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
			parameters[1].Value = model.ClassId;
			parameters[2].Value = model.Guige;
			parameters[3].Value = model.Xinghao;
			parameters[4].Value = model.Price;
			parameters[5].Value = model.ImgUrl;
			parameters[6].Value = model.Content;
			parameters[7].Value = model.Click;
			parameters[8].Value = model.IsMsg;
			parameters[9].Value = model.IsTop;
			parameters[10].Value = model.IsRed;
			parameters[11].Value = model.IsHot;
			parameters[12].Value = model.IsSlide;
			parameters[13].Value = model.IsLock;
			parameters[14].Value = model.AddTime;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Products model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Products set ");
			strSql.Append("Title=@Title,");
			strSql.Append("ClassId=@ClassId,");
			strSql.Append("Guige=@Guige,");
			strSql.Append("Xinghao=@Xinghao,");
			strSql.Append("Price=@Price,");
			strSql.Append("ImgUrl=@ImgUrl,");
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
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@ClassId", OleDbType.Integer,4),
					new OleDbParameter("@Guige", OleDbType.VarWChar,50),
					new OleDbParameter("@Xinghao", OleDbType.VarWChar,50),
					new OleDbParameter("@Price", OleDbType.Decimal,9),
					new OleDbParameter("@ImgUrl", OleDbType.VarWChar,250),
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
			parameters[1].Value = model.ClassId;
			parameters[2].Value = model.Guige;
			parameters[3].Value = model.Xinghao;
			parameters[4].Value = model.Price;
			parameters[5].Value = model.ImgUrl;
			parameters[6].Value = model.Content;
			parameters[7].Value = model.Click;
			parameters[8].Value = model.IsMsg;
			parameters[9].Value = model.IsTop;
			parameters[10].Value = model.IsRed;
			parameters[11].Value = model.IsHot;
			parameters[12].Value = model.IsSlide;
			parameters[13].Value = model.IsLock;
			parameters[14].Value = model.AddTime;
            parameters[15].Value = model.Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Products ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Products GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Title,ClassId,Guige,Xinghao,Price,ImgUrl,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime from Products ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DtCms.Model.Products model=new DtCms.Model.Products();
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
				model.Guige=ds.Tables[0].Rows[0]["Guige"].ToString();
				model.Xinghao=ds.Tables[0].Rows[0]["Xinghao"].ToString();
				if(ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				model.ImgUrl=ds.Tables[0].Rows[0]["ImgUrl"].ToString();
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
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,Title,ClassId,Guige,Xinghao,Price,ImgUrl,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime ");
			strSql.Append(" FROM Products ");
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
			strSql.Append(" Id,Title,ClassId,Guige,Xinghao,Price,ImgUrl,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime ");
			strSql.Append(" FROM Products ");
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
                strSql.Append("select top " + pageSize + " Id,Title,ClassId,Guige,Xinghao,Price,ImgUrl,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime from Products");
                strSql.Append(" where Id not in(select top " + topNum + " Id from Products");
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
                strSql.Append("select top " + pageSize + " Id,Title,ClassId,Guige,Xinghao,Price,ImgUrl,Content,Click,IsMsg,IsTop,IsRed,IsHot,IsSlide,IsLock,AddTime from Products");
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

