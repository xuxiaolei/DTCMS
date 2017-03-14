using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.Common;
using DTcms.DBUtility;

namespace DTcms.Web.Plugin.Advert.DAL
{
    /// <summary>
    /// 数据访问类:广告Banner
    /// </summary>
    public partial class advert_banner
    {
        private string column;
        private string databaseprefix;
        public advert_banner(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
            this.column = "id,aid,title,start_time,end_time,file_path,link_url,content,target,sort_id,is_lock,add_time";
        }

        #region 基本方法
        /// <summary>
        /// 按ID号查询是否存在记录
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [" + databaseprefix + "advert_banner] where id=@id");
            SqlParameter[] parameters = {
            	new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 按名称查询是否存在记录
        /// </summary>
        /// <param name="title">名称</param>
        /// <returns></returns>
        public bool Exists(string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [" + databaseprefix + "advert_banner] where title=@title");
            SqlParameter[] parameters = {
                new SqlParameter("@title", SqlDbType.VarChar,200)
            };
            parameters[0].Value = title;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回标题
        /// </summary>
        /// <param name="id">ID号</param>
        /// <returns></returns>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title from [" + databaseprefix + "advert_banner] where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }
        /// <summary>
        /// 返回数据总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H [" + databaseprefix + "advert_banner]");
            if (strWhere.Trim() != "")
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
        public int Add(Model.advert_banner model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [" + databaseprefix + "advert_banner](");
            strSql.Append("aid,title,start_time,end_time,file_path,link_url,content,target,sort_id,is_lock,add_time");
            strSql.Append(") values(");
            strSql.Append("@aid,@title,@start_time,@end_time,@file_path,@link_url,@content,@target,@sort_id,@is_lock,@add_time)");
            SqlParameter[] parameters = {
                new SqlParameter("@aid", SqlDbType.Int,6),
                new SqlParameter("@title", SqlDbType.NVarChar,100),
                new SqlParameter("@start_time", SqlDbType.DateTime),
                new SqlParameter("@end_time", SqlDbType.DateTime),
                new SqlParameter("@file_path", SqlDbType.NVarChar,255),
                new SqlParameter("@link_url", SqlDbType.NVarChar,255),
                new SqlParameter("@content", SqlDbType.NText),
                new SqlParameter("@target", SqlDbType.NVarChar,30),
                new SqlParameter("@sort_id", SqlDbType.Int,6),
                new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
				new SqlParameter("@add_time", SqlDbType.DateTime)
            };
            parameters[0].Value = model.aid;
            parameters[1].Value = model.title;
            parameters[2].Value = model.start_time;
            parameters[3].Value = model.end_time;
            parameters[4].Value = model.file_path;
            parameters[5].Value = model.link_url;
            parameters[6].Value = model.content;
            parameters[7].Value = model.target;
            parameters[8].Value = model.sort_id;
            parameters[9].Value = model.is_lock;
            parameters[10].Value = model.add_time;
            object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion

        #region 修改一列数据
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [" + databaseprefix + "advert_banner] set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.advert_banner model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [" + databaseprefix + "advert_banner] set ");
            strSql.Append("aid=@aid,");
            strSql.Append("title=@title,");
            strSql.Append("start_time=@start_time,");
            strSql.Append("end_time=@end_time,");
            strSql.Append("file_path=@file_path,");
            strSql.Append("link_url=@link_url,");
            strSql.Append("content=@content,");
            strSql.Append("target=@target,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@aid", SqlDbType.Int,6),
                new SqlParameter("@title", SqlDbType.NVarChar,100),
                new SqlParameter("@start_time", SqlDbType.DateTime),
                new SqlParameter("@end_time", SqlDbType.DateTime),
                new SqlParameter("@file_path", SqlDbType.NVarChar,255),
                new SqlParameter("@link_url", SqlDbType.NVarChar,255),
                new SqlParameter("@content", SqlDbType.NText),
                new SqlParameter("@target", SqlDbType.NVarChar,30),
                new SqlParameter("@sort_id", SqlDbType.Int,6),
                new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
				new SqlParameter("@add_time", SqlDbType.DateTime),
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = model.aid;
            parameters[1].Value = model.title;
            parameters[2].Value = model.start_time;
            parameters[3].Value = model.end_time;
            parameters[4].Value = model.file_path;
            parameters[5].Value = model.link_url;
            parameters[6].Value = model.content;
            parameters[7].Value = model.target;
            parameters[8].Value = model.sort_id;
            parameters[9].Value = model.is_lock;
            parameters[10].Value = model.add_time;
            parameters[11].Value = model.id;
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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [" + databaseprefix + "advert_banner] where id=@id");
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
        public Model.advert_banner GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + this.column + " from [" + databaseprefix + "advert_banner] where id=@id");
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString() + " ");
            }
            strSql.Append(this.column + " from [" + databaseprefix + "advert_banner]");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
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
        public DataSet GetList(string strWhere, string filedOrder, int pageIndex, int pageSize, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from [" + databaseprefix + "advert_banner]");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 私有方法===============================
        /// <summary>
        /// 组合成对象实体
        /// </summary>
        private Model.advert_banner DataRowToModel(DataRow row)
        {
            Model.advert_banner model = new Model.advert_banner();
            if (row != null)
            {
                if (row["id"].ToString() != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["aid"].ToString() != null && row["aid"].ToString() != "")
                {
                    model.aid = int.Parse(row["aid"].ToString());
                }
                if (row["title"].ToString() != "")
                {
                    model.title = row["title"].ToString();
                }
                if (row["start_time"].ToString() != null && row["start_time"].ToString() != "")
                {
                    model.start_time = DateTime.Parse(row["start_time"].ToString());
                }
                if (row["end_time"].ToString() != null && row["end_time"].ToString() != "")
                {
                    model.end_time = DateTime.Parse(row["end_time"].ToString());
                }
                if (row["file_path"].ToString() != "")
                {
                    model.file_path = row["file_path"].ToString();
                }
                if (row["link_url"].ToString() != "")
                {
                    model.link_url = row["link_url"].ToString();
                }
                if (row["content"].ToString() != "")
                {
                    model.content = row["content"].ToString();
                }
                if (row["target"].ToString() != "")
                {
                    model.target = row["target"].ToString();
                }
                if (row["sort_id"].ToString() != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["is_lock"].ToString() != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (row["add_time"].ToString() != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}