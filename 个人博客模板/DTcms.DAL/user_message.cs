using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 用户短信息
    /// </summary>
    public partial class user_message
    {
        private string databaseprefix; //数据库表名前缀
        public user_message(string _databaseprefix)
        {
            databaseprefix = _databaseprefix; 
        }

        #region 基本方法==============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "user_message order by id desc";
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
            strSql.Append("select count(1) from " + databaseprefix + "user_message");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_message model)
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
                        strSql.Append("insert into " + databaseprefix + "user_message(");
                        strSql.Append("[type],post_user_name,accept_user_name,is_read,title,content,post_time,read_time)");
                        strSql.Append(" values (");
                        strSql.Append("@type,@post_user_name,@accept_user_name,@is_read,@title,@content,@post_time,@read_time)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@type", OleDbType.Integer,4),
					            new OleDbParameter("@post_user_name", OleDbType.VarChar,100),
					            new OleDbParameter("@accept_user_name", OleDbType.VarChar,100),
					            new OleDbParameter("@is_read", OleDbType.Integer,4),
					            new OleDbParameter("@title", OleDbType.VarChar,100),
					            new OleDbParameter("@content", OleDbType.VarChar),
					            new OleDbParameter("@post_time", OleDbType.Date),
					            new OleDbParameter("@read_time", OleDbType.Date)};
                        parameters[0].Value = model.type;
                        parameters[1].Value = model.post_user_name;
                        parameters[2].Value = model.accept_user_name;
                        parameters[3].Value = model.is_read;
                        parameters[4].Value = model.title;
                        parameters[5].Value = model.content;
                        parameters[6].Value = model.post_time;
                        if (model.read_time != null)
                        {
                            parameters[7].Value = model.read_time;
                        }
                        else
                        {
                            parameters[7].Value = DBNull.Value;
                        }
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
        public bool Update(Model.user_message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_message set ");
            strSql.Append("[type]=@type,");
            strSql.Append("post_user_name=@post_user_name,");
            strSql.Append("accept_user_name=@accept_user_name,");
            strSql.Append("is_read=@is_read,");
            strSql.Append("title=@title,");
            strSql.Append("content=@content,");
            strSql.Append("post_time=@post_time,");
            strSql.Append("read_time=@read_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@type", OleDbType.Integer,4),
					new OleDbParameter("@post_user_name", OleDbType.VarChar,100),
					new OleDbParameter("@accept_user_name", OleDbType.VarChar,100),
					new OleDbParameter("@is_read", OleDbType.Integer,4),
					new OleDbParameter("@title", OleDbType.VarChar,100),
					new OleDbParameter("@content", SqlDbType.NText),
					new OleDbParameter("@post_time", OleDbType.Date),
					new OleDbParameter("@read_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.type;
            parameters[1].Value = model.post_user_name;
            parameters[2].Value = model.accept_user_name;
            parameters[3].Value = model.is_read;
            parameters[4].Value = model.title;
            parameters[5].Value = model.content;
            parameters[6].Value = model.post_time;
            if (model.read_time != null)
            {
                parameters[7].Value = model.read_time;
            }
            else
            {
                parameters[7].Value = DBNull.Value;
            }
            parameters[8].Value = model.id;

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
            strSql.Append("delete from " + databaseprefix + "user_message ");
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
        /// 根据用户名删除一条数据
        /// </summary>
        public bool Delete(int id, string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_message ");
            strSql.Append(" where id=@id and (post_user_name=@post_user_name or accept_user_name=@accept_user_name)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4),
                    new OleDbParameter("@post_user_name", OleDbType.VarChar,100),
                    new OleDbParameter("@accept_user_name", OleDbType.VarChar,100)};
            parameters[0].Value = id;
            parameters[1].Value = user_name;
            parameters[2].Value = user_name;

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
        public Model.user_message GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,[type],post_user_name,accept_user_name,is_read,title,content,post_time,read_time from " + databaseprefix + "user_message ");
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
            strSql.Append(" id,[type],post_user_name,accept_user_name,is_read,title,content,post_time,read_time ");
            strSql.Append(" FROM " + databaseprefix + "user_message ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_message");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法==============================
        /// <summary>
        /// 返回记录总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "user_message ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_message set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperOleDb.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        public Model.user_message DataRowToModel(DataRow row)
        {
            Model.user_message model = new Model.user_message();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["type"] != null && row["type"].ToString() != "")
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (row["post_user_name"] != null)
                {
                    model.post_user_name = row["post_user_name"].ToString();
                }
                if (row["accept_user_name"] != null)
                {
                    model.accept_user_name = row["accept_user_name"].ToString();
                }
                if (row["is_read"] != null && row["is_read"].ToString() != "")
                {
                    model.is_read = int.Parse(row["is_read"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["post_time"] != null && row["post_time"].ToString() != "")
                {
                    model.post_time = DateTime.Parse(row["post_time"].ToString());
                }
                if (row["read_time"] != null && row["read_time"].ToString() != "")
                {
                    model.read_time = DateTime.Parse(row["read_time"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}