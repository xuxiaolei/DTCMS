using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
	/// <summary>
	/// 数据访问类Admin。
	/// </summary>
	public class Admin
	{
		public Admin()
		{}
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Administrator");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
		}
        public bool Exists(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Administrator");
            strSql.Append(" where UserName=@UserName ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", OleDbType.VarWChar,50)};
            parameters[0].Value = userName;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回长查询数据总数 （分页用到）
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from Administrator ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 检查登录用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        public bool chkAdminLoign(string UserName, string UserPwd)
        {
            string strSql = "select count(*) from Administrator where UserName=@UserName and UserPwd=@UserPwd and isLock=0";
            OleDbParameter[] parameters = {
                new OleDbParameter("@UserName",OleDbType.VarWChar,30),
                new OleDbParameter("@UserPwd", OleDbType.VarWChar,50)};
            parameters[0].Value = UserName;
            parameters[1].Value = UserPwd;
            return DbHelperOleDb.Exists(strSql, parameters);
        }

        /// <summary>
        /// 检查用户名是否重复
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool chkExists(string UserName)
        {
            string strSql = "select count(*) from Administrator where UserName=@UserName";
            OleDbParameter[] parameters = {
                new OleDbParameter("@UserName",OleDbType.VarWChar,30)};
            parameters[0].Value = UserName;
            return DbHelperOleDb.Exists(strSql, parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(DtCms.Model.Admin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Administrator(");
			strSql.Append("UserName,UserPwd,ReadName,UserEmail,UserType,UserLevel,IsLock)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@UserPwd,@ReadName,@UserEmail,@UserType,@UserLevel,@IsLock)");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", OleDbType.VarWChar,30),
					new OleDbParameter("@UserPwd", OleDbType.VarWChar,50),
					new OleDbParameter("@ReadName", OleDbType.VarWChar,30),
					new OleDbParameter("@UserEmail", OleDbType.VarWChar,50),
					new OleDbParameter("@UserType", OleDbType.Integer,4),
					new OleDbParameter("@UserLevel", SqlDbType.NText),
					new OleDbParameter("@IsLock", OleDbType.Integer,4)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.UserPwd;
			parameters[2].Value = model.ReadName;
			parameters[3].Value = model.UserEmail;
			parameters[4].Value = model.UserType;
			parameters[5].Value = model.UserLevel;
			parameters[6].Value = model.IsLock;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(DtCms.Model.Admin model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Administrator set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("UserPwd=@UserPwd,");
			strSql.Append("ReadName=@ReadName,");
			strSql.Append("UserEmail=@UserEmail,");
			strSql.Append("UserType=@UserType,");
			strSql.Append("UserLevel=@UserLevel,");
			strSql.Append("IsLock=@IsLock");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", OleDbType.VarWChar,30),
					new OleDbParameter("@UserPwd", OleDbType.VarWChar,50),
					new OleDbParameter("@ReadName", OleDbType.VarWChar,30),
					new OleDbParameter("@UserEmail", OleDbType.VarWChar,50),
					new OleDbParameter("@UserType", OleDbType.Integer,4),
					new OleDbParameter("@UserLevel", SqlDbType.NText),
					new OleDbParameter("@IsLock", OleDbType.Integer,4),
                    new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.UserPwd;
			parameters[2].Value = model.ReadName;
			parameters[3].Value = model.UserEmail;
			parameters[4].Value = model.UserType;
			parameters[5].Value = model.UserLevel;
			parameters[6].Value = model.IsLock;
            parameters[7].Value = model.Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Administrator ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public DtCms.Model.Admin GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,UserName,UserPwd,ReadName,UserEmail,UserType,UserLevel,IsLock from Administrator ");
			strSql.Append(" where Id=@Id ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
			parameters[0].Value = Id;

			DtCms.Model.Admin model=new DtCms.Model.Admin();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				model.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				model.UserPwd=ds.Tables[0].Rows[0]["UserPwd"].ToString();
				model.ReadName=ds.Tables[0].Rows[0]["ReadName"].ToString();
				model.UserEmail=ds.Tables[0].Rows[0]["UserEmail"].ToString();
				if(ds.Tables[0].Rows[0]["UserType"].ToString()!="")
				{
					model.UserType=int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
				}
				model.UserLevel=ds.Tables[0].Rows[0]["UserLevel"].ToString();
				if(ds.Tables[0].Rows[0]["IsLock"].ToString()!="")
				{
					model.IsLock=int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
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
			strSql.Append("select Id,UserName,UserPwd,ReadName,UserEmail,UserType,UserLevel,IsLock ");
			strSql.Append(" FROM Administrator ");
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
			strSql.Append(" Id,UserName,UserPwd,ReadName,UserEmail,UserType,UserLevel,IsLock ");
			strSql.Append(" FROM Administrator ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}

        /// <summary>
        /// 根据用户名取得一行数据给MODEL
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DtCms.Model.Admin GetModel(string UserName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from Administrator");
            strSql.Append(" where UserName=@UserName ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", OleDbType.VarWChar,30)};
            parameters[0].Value = UserName;

            DtCms.Model.Admin model = new DtCms.Model.Admin();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                model.UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
                model.ReadName = ds.Tables[0].Rows[0]["ReadName"].ToString();
                model.UserEmail = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                model.UserType = int.Parse(ds.Tables[0].Rows[0]["UserType"].ToString());
                model.UserLevel = ds.Tables[0].Rows[0]["UserLevel"].ToString();
                model.IsLock = int.Parse(ds.Tables[0].Rows[0]["IsLock"].ToString());
            }
            return model;
        }

		#endregion  成员方法
	}
}

