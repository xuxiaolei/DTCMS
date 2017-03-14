using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Web.Plugin.Lable.DAL
{
    /// <summary>
    /// 数据库访问层
    /// </summary>
    public partial class lable
    {
        private string column;
        private string databaseprefix;
        public lable(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
            this.column = "id,call_index,title,type,sort_id,content,user_name,is_lock,add_time";
        }

        #region 基本方法
        /// <summary>
        /// 按ID号查询是否存在记录
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns>True or False</returns>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [" + databaseprefix + "lable] where id=@id");
            SqlParameter[] parameters = {
            	new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 按名称查询是否存在记录
        /// </summary>
        /// <param name="title">标题</param>
        /// <returns>True or False</returns>
        public bool Exists(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [" + databaseprefix + "lable] where call_index=@call_index");
            SqlParameter[] parameters = {
                new SqlParameter("@call_index", SqlDbType.VarChar,100)
            };
            parameters[0].Value = call_index;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回标题
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns>标题</returns>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title from [" + databaseprefix + "lable] where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (null != obj)
            {
                return obj.ToString();
            }
            return "";
        }
        /// <summary>
        /// 返回数据总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>总数</returns>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H [" + databaseprefix + "lable]");
            if ("" != strWhere.Trim())
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        #endregion

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">Model.lable</param>
        /// <returns>ID</returns>
        public int Add(Model.lable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [" + databaseprefix + "lable](");
            strSql.Append("call_index,title,type,sort_id,content,user_name,is_lock,add_time");
            strSql.Append(") values(");
            strSql.Append("@call_index,@title,@type,@sort_id,@content,@user_name,@is_lock,@add_time)");
            SqlParameter[] parameters = {
                new SqlParameter("@call_index", SqlDbType.NVarChar,50),
                new SqlParameter("@title", SqlDbType.NVarChar,100),
                new SqlParameter("@type", SqlDbType.TinyInt,1),
                new SqlParameter("@sort_id", SqlDbType.Int,4),
                new SqlParameter("@content", SqlDbType.NText),
                new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
				new SqlParameter("@add_time", SqlDbType.DateTime)
            };
            parameters[0].Value = model.call_index;
            parameters[1].Value = model.title;
            parameters[2].Value = model.type;
            parameters[3].Value = model.sort_id;
            parameters[4].Value = model.content;
            parameters[5].Value = model.user_name;
            parameters[6].Value = model.is_lock;
            parameters[7].Value = model.add_time;
            object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (null != obj)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 修改一列数据
        /// <summary>
        /// 修改一列数据
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="strValue"></param>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [" + databaseprefix + "lable] set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">Model.lable</param>
        /// <returns>True or False</returns>
        public bool Update(Model.lable model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [" + databaseprefix + "lable] set ");
            strSql.Append("call_index=@call_index,");
            strSql.Append("title=@title,");
            strSql.Append("type=@type,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("content=@content,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@call_index", SqlDbType.NVarChar,50),
                new SqlParameter("@title", SqlDbType.NVarChar,100),
                new SqlParameter("@type", SqlDbType.TinyInt,1),
                new SqlParameter("@sort_id", SqlDbType.Int,4),
                new SqlParameter("@content", SqlDbType.NText),
                new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
				new SqlParameter("@add_time", SqlDbType.DateTime),
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = model.call_index;
            parameters[1].Value = model.title;
            parameters[2].Value = model.type;
            parameters[3].Value = model.sort_id;
            parameters[4].Value = model.content;
            parameters[5].Value = model.user_name;
            parameters[6].Value = model.is_lock;
            parameters[7].Value = model.add_time;
            parameters[8].Value = model.id;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns>True or False</returns>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [" + databaseprefix + "lable] where id=@id");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 返回一个实体
        /// <summary>
        /// 按ID返回一个实体
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns>Model.lable</returns>
        public Model.lable GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + this.column + " from [" + databaseprefix + "lable] where id=@id");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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

        #region 获得前几行数据
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        /// <param name="Top">数量</param>
        /// <param name="strWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <returns>DataTable</returns>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString() + " ");
            }
            strSql.Append(this.column + " from [" + databaseprefix + "lable]");
            if ("" != strWhere.Trim())
            {
                strSql.Append(" where " + strWhere);
            }
            if ("" != filedOrder.Trim())
            {
                strSql.Append(" order by " + filedOrder);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 获得查询分页数据
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        /// <param name="pageSize">分页数量</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="strWhere">条件</param>
        /// <param name="filedOrder">排序</param>
        /// <param name="recordCount">返回数据总数</param>
        /// <returns>DataTable</returns>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [" + databaseprefix + "lable]");
            if ("" != strWhere.Trim())
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 组合成对象实体
        /// </summary>
        /// <param name="row">一行数据</param>
        /// <returns>Model.lable</returns>
        private Model.lable DataRowToModel(DataRow row)
        {
            Model.lable model = new Model.lable();
            if (row != null)
            {
                if (null != row["id"] && "" != row["id"].ToString())
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (null != row["call_index"])
                {
                    model.call_index = row["call_index"].ToString();
                }
                if (null != row["title"])
                {
                    model.title = row["title"].ToString();
                }
                if (null != row["type"] && "" != row["type"].ToString())
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (null != row["sort_id"] && "" != row["sort_id"].ToString())
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (null != row["content"])
                {
                    model.content = row["content"].ToString();
                }
                if (null != row["user_name"])
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (null != row["is_lock"] && "" != row["is_lock"].ToString())
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (null != row["add_time"] && "" != row["add_time"].ToString())
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
            }
            return model;
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 根据ID返回内容
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns></returns>
        public string GetContent(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 content from [" + databaseprefix + "lable] where is_lock=0 and id=@id");
            SqlParameter[] parameters = {
            	new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            string result = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据别名返回内容
        /// </summary>
        /// <param name="call_index">调用名称</param>
        /// <returns></returns>
        public string GetContent(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 content from [" + databaseprefix + "lable] where is_lock=0 and call_index=@call_index");
            SqlParameter[] parameters = {
            	new SqlParameter("@call_index", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = call_index;
            string result = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            return string.Empty;
        }
        #endregion
    }
}