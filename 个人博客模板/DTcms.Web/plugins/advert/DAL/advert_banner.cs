using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DTcms.Common;
using DTcms.DBUtility;

namespace DTcms.Web.Plugin.Advert.DAL
{
    /// <summary>
    /// 数据访问类:广告Banner
    /// </summary>
    public partial class advert_banner
    {
        private string databaseprefix; //数据库表名前缀
        public advert_banner(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "advert_banner order by id desc";
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
            strSql.Append("select count(1) from " + databaseprefix + "advert_banner");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.advert_banner model)
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
                        strSql.Append("insert into " + databaseprefix + "advert_banner(");
                        strSql.Append("aid,title,start_time,end_time,file_path,link_url,content,sort_id,is_lock,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@aid,@title,@start_time,@end_time,@file_path,@link_url,@content,@sort_id,@is_lock,@add_time)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@aid", OleDbType.Integer,4),
					            new OleDbParameter("@title", OleDbType.VarChar,100),
					            new OleDbParameter("@start_time", OleDbType.Date),
					            new OleDbParameter("@end_time", OleDbType.Date),
					            new OleDbParameter("@file_path", OleDbType.VarChar,255),
					            new OleDbParameter("@link_url", OleDbType.VarChar,255),
					            new OleDbParameter("@content", OleDbType.VarChar),
					            new OleDbParameter("@sort_id", OleDbType.Integer,4),
					            new OleDbParameter("@is_lock", OleDbType.Integer,4),
					            new OleDbParameter("@add_time", OleDbType.Date)};
                        parameters[0].Value = model.aid;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.start_time;
                        parameters[3].Value = model.end_time;
                        parameters[4].Value = model.file_path;
                        parameters[5].Value = model.link_url;
                        parameters[6].Value = model.content;
                        parameters[7].Value = model.sort_id;
                        parameters[8].Value = model.is_lock;
                        parameters[9].Value = model.add_time;
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
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "advert_banner set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperOleDb.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.advert_banner model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "advert_banner set ");
            strSql.Append("aid=@aid,");
            strSql.Append("title=@title,");
            strSql.Append("start_time=@start_time,");
            strSql.Append("end_time=@end_time,");
            strSql.Append("file_path=@file_path,");
            strSql.Append("link_url=@link_url,");
            strSql.Append("content=@content,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@aid", OleDbType.Integer,4),
					new OleDbParameter("@title", OleDbType.VarChar,100),
					new OleDbParameter("@start_time", OleDbType.Date),
					new OleDbParameter("@end_time", OleDbType.Date),
					new OleDbParameter("@file_path", OleDbType.VarChar,255),
					new OleDbParameter("@link_url", OleDbType.VarChar,255),
					new OleDbParameter("@content", OleDbType.VarChar),
					new OleDbParameter("@sort_id", OleDbType.Integer,4),
					new OleDbParameter("@is_lock", OleDbType.Integer,4),
					new OleDbParameter("@add_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.aid;
            parameters[1].Value = model.title;
            parameters[2].Value = model.start_time;
            parameters[3].Value = model.end_time;
            parameters[4].Value = model.file_path;
            parameters[5].Value = model.link_url;
            parameters[6].Value = model.content;
            parameters[7].Value = model.sort_id;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.id;

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
            strSql.Append("delete from " + databaseprefix + "advert_banner ");
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
        public Model.advert_banner GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,aid,title,start_time,end_time,file_path,link_url,content,sort_id,is_lock,add_time from " + databaseprefix + "advert_banner ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            Model.advert_banner model = new Model.advert_banner();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["aid"] != null && ds.Tables[0].Rows[0]["aid"].ToString() != "")
                {
                    model.aid = int.Parse(ds.Tables[0].Rows[0]["aid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["start_time"] != null && ds.Tables[0].Rows[0]["start_time"].ToString() != "")
                {
                    model.start_time = DateTime.Parse(ds.Tables[0].Rows[0]["start_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["end_time"] != null && ds.Tables[0].Rows[0]["end_time"].ToString() != "")
                {
                    model.end_time = DateTime.Parse(ds.Tables[0].Rows[0]["end_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["file_path"] != null && ds.Tables[0].Rows[0]["file_path"].ToString() != "")
                {
                    model.file_path = ds.Tables[0].Rows[0]["file_path"].ToString();
                }
                if (ds.Tables[0].Rows[0]["link_url"] != null && ds.Tables[0].Rows[0]["link_url"].ToString() != "")
                {
                    model.link_url = ds.Tables[0].Rows[0]["link_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["content"] != null && ds.Tables[0].Rows[0]["content"].ToString() != "")
                {
                    model.content = ds.Tables[0].Rows[0]["content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,aid,title,start_time,end_time,file_path,link_url,content,sort_id,is_lock,add_time ");
            strSql.Append(" FROM " + databaseprefix + "advert_banner ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
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
            strSql.Append(" id,aid,title,start_time,end_time,file_path,link_url,content,sort_id,is_lock,add_time ");
            strSql.Append(" FROM " + databaseprefix + "advert_banner ");
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
            strSql.Append("select * FROM " + databaseprefix + "advert_banner");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion
    }
}