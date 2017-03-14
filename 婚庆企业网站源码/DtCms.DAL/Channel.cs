using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
	/// <summary>
	/// 数据访问类Channel。
	/// </summary>
	public class Channel
	{
		public Channel()
		{}
		#region  成员方法
        /// <summary>
        /// 取得最新插入的ID
        /// </summary>
        public int GetMaxID(string FieldName)
        {
            return DbHelperOleDb.GetMaxID(FieldName, "Channel");
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Channel");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			return DbHelperOleDb.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 返回栏目名称
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public string GetChannelTitle(int classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 Title from Channel");
            strSql.Append(" where Id=" + classId);
            return Convert.ToString(DbHelperOleDb.GetSingle(strSql.ToString()));
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Channel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Channel(");
            strSql.Append("Title,ParentId,ClassList,ClassLayer,ClassOrder,PageUrl,KindId)");
			strSql.Append(" values (");
            strSql.Append("@Title,@ParentId,@ClassList,@ClassLayer,@ClassOrder,@PageUrl,@KindId)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,50),
					new OleDbParameter("@ParentId", OleDbType.Integer,4),
					new OleDbParameter("@ClassList", OleDbType.VarWChar,300),
					new OleDbParameter("@ClassLayer", OleDbType.Integer,4),
					new OleDbParameter("@ClassOrder", OleDbType.Integer,4),
                    new OleDbParameter("@PageUrl",OleDbType.VarWChar,250),
					new OleDbParameter("@KindId", OleDbType.Integer,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.ParentId;
			parameters[2].Value = model.ClassList;
			parameters[3].Value = model.ClassLayer;
            parameters[4].Value = model.ClassOrder;
            parameters[5].Value = model.PageUrl;
			parameters[6].Value = model.KindId;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Channel model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Channel set ");
			strSql.Append("Title=@Title,");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("ClassList=@ClassList,");
			strSql.Append("ClassLayer=@ClassLayer,");
			strSql.Append("ClassOrder=@ClassOrder,");
            strSql.Append("PageUrl=@PageUrl,");
			strSql.Append("KindId=@KindId");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,50),
					new OleDbParameter("@ParentId", OleDbType.Integer,4),
					new OleDbParameter("@ClassList", OleDbType.VarWChar,300),
					new OleDbParameter("@ClassLayer", OleDbType.Integer,4),
					new OleDbParameter("@ClassOrder", OleDbType.Integer,4),
                    new OleDbParameter("@PageUrl", OleDbType.VarWChar,250),
					new OleDbParameter("@KindId", OleDbType.Integer,4),
                    new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.ParentId;
			parameters[2].Value = model.ClassList;
			parameters[3].Value = model.ClassLayer;
			parameters[4].Value = model.ClassOrder;
            parameters[5].Value = model.PageUrl;
			parameters[6].Value = model.KindId;
            parameters[7].Value = model.Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除该类别及所有属下分类数据
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Channel ");
            strSql.Append(" where ClassList like '%,"+Id+",%' ");

			DbHelperOleDb.Query(strSql.ToString());
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Channel GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Title,ParentId,ClassList,ClassLayer,ClassOrder,PageUrl,KindId from Channel ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DtCms.Model.Channel model=new DtCms.Model.Channel();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				if(ds.Tables[0].Rows[0]["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
				}
				model.ClassList=ds.Tables[0].Rows[0]["ClassList"].ToString();
				if(ds.Tables[0].Rows[0]["ClassLayer"].ToString()!="")
				{
					model.ClassLayer=int.Parse(ds.Tables[0].Rows[0]["ClassLayer"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ClassOrder"].ToString()!="")
				{
					model.ClassOrder=int.Parse(ds.Tables[0].Rows[0]["ClassOrder"].ToString());
				}
                model.PageUrl = ds.Tables[0].Rows[0]["PageUrl"].ToString();
				if(ds.Tables[0].Rows[0]["KindId"].ToString()!="")
				{
					model.KindId=int.Parse(ds.Tables[0].Rows[0]["KindId"].ToString());
				}
				return model;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// 取得所有栏目列表
        /// </summary>
        /// <param name="PId">父ID</param>
        /// <param name="KId">种类ID</param>
        /// <returns></returns>
        public DataTable GetList(int PId, int KId)
        {
            DataTable data = new DataTable();
            //创建列
            DataColumn Id = new DataColumn("Id", typeof(int));
            DataColumn Title = new DataColumn("Title", typeof(string));
            DataColumn ParentId = new DataColumn("ParentId", typeof(int));
            DataColumn ClassList = new DataColumn("ClassList", typeof(string));
            DataColumn ClassLayer = new DataColumn("ClassLayer", typeof(int));
            DataColumn ClassOrder = new DataColumn("ClassOrder", typeof(int));
            DataColumn PageUrl = new DataColumn("PageUrl", typeof(string));
            DataColumn KindId = new DataColumn("KindId", typeof(int));
            //添加列
            data.Columns.Add(Id);
            data.Columns.Add(Title);
            data.Columns.Add(ParentId);
            data.Columns.Add(ClassList);
            data.Columns.Add(ClassLayer);
            data.Columns.Add(ClassOrder);
            data.Columns.Add(PageUrl);
            data.Columns.Add(KindId);
            //调用迭代组合成DAGATABLE
            GetChannelChild(data, PId, KId);
            return data;
        }

        /// <summary>
        /// 取得该栏目的所有下级栏目列表
        /// </summary>
        /// <param name="data">DATATABLE名</param>
        /// <param name="PId">父栏目ID</param>
        /// <param name="KId">种类ID</param>
        private void GetChannelChild(DataTable data, int PId, int KId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,ParentId,ClassList,ClassLayer,ClassOrder,PageUrl,KindId from Channel");
            strSql.Append(" where ParentId=" + PId + " and KindId=" + KId + " order by ClassOrder asc,Id desc");
            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    //添加一行数据
                    DataRow row = data.NewRow();
                    row[0] = int.Parse(dr["Id"].ToString());
                    row[1] = dr["Title"].ToString();
                    row[2] = int.Parse(dr["ParentId"].ToString());
                    row[3] = dr["ClassList"].ToString();
                    row[4] = int.Parse(dr["ClassLayer"].ToString());
                    row[5] = int.Parse(dr["ClassOrder"].ToString());
                    row[6] = dr["PageUrl"].ToString();
                    row[7] = int.Parse(dr["KindId"].ToString());
                    data.Rows.Add(row);
                    //调用自身迭代
                    this.GetChannelChild(data, int.Parse(dr["Id"].ToString()), KId);
                }
            }
        }

        /// <summary>
        /// 取得该栏目下的所有子栏目的ID
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public DataSet GetChannelListByClassId(int classId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ClassList,ClassLayer from Channel");
            strSql.Append(" where Id=" + classId + " ");
            return DbHelperOleDb.Query(strSql.ToString());
        }

		#endregion  成员方法
	}
}

