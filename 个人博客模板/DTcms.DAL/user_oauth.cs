using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// OAuth授权用户信息
    /// </summary>
    public partial class user_oauth
    {
        private string databaseprefix; //数据库表名前缀
        public user_oauth(string _databaseprefix)
        {
            databaseprefix = _databaseprefix; 
        }

        #region 基本方法===========================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "user_oauth order by id desc";
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
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_oauth");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;
            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_oauth model)
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
                        strSql.Append("insert into " + databaseprefix + "user_oauth(");
                        strSql.Append("user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@user_id,@user_name,@oauth_name,@oauth_access_token,@oauth_openid,@add_time)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@user_id", OleDbType.Integer,4),
					            new OleDbParameter("@user_name", OleDbType.VarChar,100),
					            new OleDbParameter("@oauth_name", OleDbType.VarChar,50),
					            new OleDbParameter("@oauth_access_token", OleDbType.VarChar,500),
					            new OleDbParameter("@oauth_openid", OleDbType.VarChar,255),
					            new OleDbParameter("@add_time", OleDbType.Date)};
                        parameters[0].Value = model.user_id;
                        parameters[1].Value = model.user_name;
                        parameters[2].Value = model.oauth_name;
                        parameters[3].Value = model.oauth_access_token;
                        parameters[4].Value = model.oauth_openid;
                        parameters[5].Value = model.add_time;
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
        public bool Update(Model.user_oauth model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_oauth set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("oauth_name=@oauth_name,");
            strSql.Append("oauth_access_token=@oauth_access_token,");
            strSql.Append("oauth_openid=@oauth_openid,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@user_id", OleDbType.Integer,4),
					new OleDbParameter("@user_name", OleDbType.VarChar,100),
					new OleDbParameter("@oauth_name", OleDbType.VarChar,50),
					new OleDbParameter("@oauth_access_token", OleDbType.VarChar,500),
					new OleDbParameter("@oauth_openid", OleDbType.VarChar,255),
					new OleDbParameter("@add_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.oauth_name;
            parameters[3].Value = model.oauth_access_token;
            parameters[4].Value = model.oauth_openid;
            parameters[5].Value = model.add_time;
            parameters[6].Value = model.id;

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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_oauth ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

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
        /// 得到一个对象实体
        /// </summary>
        public Model.user_oauth GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time");
            strSql.Append(" from " + databaseprefix + "user_oauth ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据开放平台和openid返回一个实体
        /// </summary>
        public Model.user_oauth GetModel(string oauth_name, string oauth_openid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time");
            strSql.Append(" from " + databaseprefix + "user_oauth");
            strSql.Append(" where oauth_name=@oauth_name and oauth_openid=@oauth_openid");
            OleDbParameter[] parameters = {
					new OleDbParameter("@oauth_name", OleDbType.VarChar,100),
                    new OleDbParameter("@oauth_openid", OleDbType.VarChar,100)};
            parameters[0].Value = oauth_name;
            parameters[1].Value = oauth_openid;

            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time ");
            strSql.Append(" FROM " + databaseprefix + "user_oauth ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "user_oauth");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法===========================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_oauth DataRowToModel(DataRow row)
        {
            Model.user_oauth model = new Model.user_oauth();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(row["user_id"].ToString());
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["oauth_name"] != null)
                {
                    model.oauth_name = row["oauth_name"].ToString();
                }
                if (row["oauth_access_token"] != null)
                {
                    model.oauth_access_token = row["oauth_access_token"].ToString();
                }
                if (row["oauth_openid"] != null)
                {
                    model.oauth_openid = row["oauth_openid"].ToString();
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
            }
            return model;
        }

        #endregion
    }
}