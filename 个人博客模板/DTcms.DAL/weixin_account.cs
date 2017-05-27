using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
	/// <summary>
	/// 数据访问类:公众平台账户
	/// </summary>
	public partial class weixin_account
	{
        private string databaseprefix; //数据库表名前缀
        public weixin_account(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "weixin_account order by id desc";
            object obj = DbHelperOleDb.GetSingle(conn, trans, strSql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "weixin_account");
            return DbHelperOleDb.Exists(strSql.ToString());
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.weixin_account model)
		{
            int newId;
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "weixin_account(");
                        strSql.Append("name,originalid,wxcode,token,appid,appsecret,is_push,sort_id,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@name,@originalid,@wxcode,@token,@appid,@appsecret,@is_push,@sort_id,@add_time)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@name", OleDbType.VarChar,100),
					            new OleDbParameter("@originalid", OleDbType.VarChar,50),
					            new OleDbParameter("@wxcode", OleDbType.VarChar,50),
					            new OleDbParameter("@token", OleDbType.VarChar,300),
					            new OleDbParameter("@appid", OleDbType.VarChar,100),
					            new OleDbParameter("@appsecret", OleDbType.VarChar,150),
					            new OleDbParameter("@is_push", OleDbType.Integer,4),
					            new OleDbParameter("@sort_id", OleDbType.Integer,4),
					            new OleDbParameter("@add_time", OleDbType.Date)};
                        parameters[0].Value = model.name;
                        parameters[1].Value = model.originalid;
                        parameters[2].Value = model.wxcode;
                        parameters[3].Value = model.token;
                        parameters[4].Value = model.appid;
                        parameters[5].Value = model.appsecret;
                        parameters[6].Value = model.is_push;
                        parameters[7].Value = model.sort_id;
                        parameters[8].Value = model.add_time;
                        DbHelperOleDb.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        newId = GetMaxId(conn, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return newId;
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.weixin_account model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "weixin_account set ");
            strSql.Append("name=@name,");
            strSql.Append("originalid=@originalid,");
            strSql.Append("wxcode=@wxcode,");
            strSql.Append("token=@token,");
            strSql.Append("appid=@appid,");
            strSql.Append("appsecret=@appsecret,");
            strSql.Append("is_push=@is_push,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@name", OleDbType.VarChar,100),
					new OleDbParameter("@originalid", OleDbType.VarChar,50),
					new OleDbParameter("@wxcode", OleDbType.VarChar,50),
					new OleDbParameter("@token", OleDbType.VarChar,300),
					new OleDbParameter("@appid", OleDbType.VarChar,100),
					new OleDbParameter("@appsecret", OleDbType.VarChar,150),
					new OleDbParameter("@is_push", OleDbType.Integer,4),
					new OleDbParameter("@sort_id", OleDbType.Integer,4),
					new OleDbParameter("@add_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.originalid;
            parameters[2].Value = model.wxcode;
            parameters[3].Value = model.token;
            parameters[4].Value = model.appid;
            parameters[5].Value = model.appsecret;
            parameters[6].Value = model.is_push;
            parameters[7].Value = model.sort_id;
            parameters[8].Value = model.add_time;
            parameters[9].Value = model.id;

            int rows = DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool Delete()
        {
            int rows = DbHelperOleDb.ExecuteSql("delete from " + databaseprefix + "weixin_account");

            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.weixin_account GetModel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,name,originalid,wxcode,token,appid,appsecret,is_push,sort_id,add_time");
            strSql.Append(" from " + databaseprefix + "weixin_account ");

            DataSet ds = DbHelperOleDb.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
		#endregion

        #region 扩展方法================================
        /// <summary>
        /// 返回账户名称
        /// </summary>
        public string GetName()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 name from " + databaseprefix + "weixin_account");
            string title = Convert.ToString(DbHelperOleDb.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.weixin_account DataRowToModel(DataRow row)
        {
            Model.weixin_account model = new Model.weixin_account();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["originalid"] != null)
                {
                    model.originalid = row["originalid"].ToString();
                }
                if (row["wxcode"] != null)
                {
                    model.wxcode = row["wxcode"].ToString();
                }
                if (row["token"] != null)
                {
                    model.token = row["token"].ToString();
                }
                if (row["appid"] != null)
                {
                    model.appid = row["appid"].ToString();
                }
                if (row["appsecret"] != null)
                {
                    model.appsecret = row["appsecret"].ToString();
                }
                if (row["is_push"] != null && row["is_push"].ToString() != "")
                {
                    model.is_push = int.Parse(row["is_push"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetToken()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 token from " + databaseprefix + "weixin_account");

            object obj = DbHelperOleDb.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return obj.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 公众账户和原始ID是否对应
        /// </summary>
        public bool ExistsOriginalId(string originalid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "weixin_account");
            strSql.Append(" where originalid=@originalid");
            OleDbParameter[] parameters = {
                    new OleDbParameter("@originalid", OleDbType.VarChar,50)};
            parameters[0].Value = originalid;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }
        #endregion
    }
}

