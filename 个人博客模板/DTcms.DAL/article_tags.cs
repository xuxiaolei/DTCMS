using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Collections;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:Tag标签
    /// </summary>
    public partial class article_tags
    {
        private string databaseprefix; //数据库表名前缀
        public article_tags(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法========================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "article_tags order by id desc";
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
            strSql.Append("select count(1) from " + databaseprefix + "article_tags");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_tags");
            strSql.Append(" where title=@title");
            OleDbParameter[] parameters = {
					new OleDbParameter("@title", OleDbType.VarChar,100)};
            parameters[0].Value = title;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_tags model)
        {
            using (OleDbConnection conn = new OleDbConnection(DbHelperOleDb.connectionString))
            {
                conn.Open();
                using (OleDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "article_tags(");
                        strSql.Append("title,is_red,sort_id,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@title,@is_red,@sort_id,@add_time)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@title", OleDbType.VarChar,100),
					            new OleDbParameter("@is_red", OleDbType.Integer,4),
					            new OleDbParameter("@sort_id", OleDbType.Integer,4),
					            new OleDbParameter("@add_time", OleDbType.Date)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.is_red;
                        parameters[2].Value = model.sort_id;
                        parameters[3].Value = model.add_time;
                        DbHelperOleDb.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        model.id = GetMaxId(conn, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return model.id;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_tags set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperOleDb.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_tags model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_tags set ");
            strSql.Append("title=@title,");
            strSql.Append("is_red=@is_red,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@title", OleDbType.VarChar,100),
					new OleDbParameter("@is_red", OleDbType.Integer,4),
					new OleDbParameter("@sort_id", OleDbType.Integer,4),
					new OleDbParameter("@add_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.is_red;
            parameters[2].Value = model.sort_id;
            parameters[3].Value = model.add_time;
            parameters[4].Value = model.id;

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
            Hashtable sqllist = new Hashtable();
            //删除Tag标签关系表
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "article_tags_relation");
            strSql1.Append(" where tag_id=@tag_id");
            OleDbParameter[] parameters1 = {
					new OleDbParameter("@tag_id", OleDbType.Integer,4)};
            parameters1[0].Value = id;
            sqllist.Add(strSql1.ToString(), parameters1);

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_tags ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;
            sqllist.Add(strSql.ToString(), parameters);

            bool result = DbHelperOleDb.ExecuteSqlTran(sqllist);
            if (result)
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
        public Model.article_tags GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,is_red,sort_id,add_time from " + databaseprefix + "article_tags ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)
			};
            parameters[0].Value = id;

            DTcms.Model.article_tags model = new DTcms.Model.article_tags();
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
        /// 得到一个对象实体
        /// </summary>
        public Model.article_tags DataRowToModel(DataRow row)
        {
            Model.article_tags model = new Model.article_tags();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["is_red"] != null && row["is_red"].ToString() != "")
                {
                    model.is_red = int.Parse(row["is_red"].ToString());
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,is_red,sort_id,add_time,[count] from");
            strSql.Append(" (select id,title,is_red,sort_id,add_time,count(id) as [count] from");
            strSql.Append(" (select " + databaseprefix + "article_tags.* from " + databaseprefix + "article_tags left join " + databaseprefix + "article_tags_relation on " + databaseprefix + "article_tags.id=" + databaseprefix + "article_tags_relation.tag_id)");
            strSql.Append(" group by id,title,is_red,sort_id,add_time)");
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
            strSql.Append(" id,title,is_red,sort_id,add_time,[count] from");
            strSql.Append(" (select id,title,is_red,sort_id,add_time,count(id) as [count] from");
            strSql.Append(" (select " + databaseprefix + "article_tags.* from " + databaseprefix + "article_tags left join " + databaseprefix + "article_tags_relation on " + databaseprefix + "article_tags.id=" + databaseprefix + "article_tags_relation.tag_id)");
            strSql.Append(" group by id,title,is_red,sort_id,add_time)");
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
            strSql.Append("select * from");
            strSql.Append(" (select id,title,is_red,sort_id,add_time,count(id) as [count] from");
            strSql.Append(" (select " + databaseprefix + "article_tags.* from " + databaseprefix + "article_tags left join " + databaseprefix + "article_tags_relation on " + databaseprefix + "article_tags.id=" + databaseprefix + "article_tags_relation.tag_id)");
            strSql.Append(" group by id,title,is_red,sort_id,add_time)");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperOleDb.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperOleDb.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 扩展方法========================
        /// <summary>
        /// 检查更新Tags标签及关系，带事务
        /// </summary>
        public void Update(OleDbConnection conn, OleDbTransaction trans, string tags_title, int article_id)
        {
            int tagsId = 0;
            //检查该Tags标签是否已存在
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "article_tags");
            strSql.Append(" where title=@title");
            OleDbParameter[] parameters = {
					new OleDbParameter("@title", OleDbType.VarChar,100)};
            parameters[0].Value = tags_title;
            object obj1 = DbHelperOleDb.GetSingle(conn, trans, strSql.ToString(), parameters);
            if (obj1 != null)
            {
                //存在则将ID赋值
                tagsId = Convert.ToInt32(obj1);
            }
            //如果尚未创建该Tags标签则创建
            if (tagsId == 0)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("insert into " + databaseprefix + "article_tags(");
                strSql2.Append("title,is_red,sort_id,add_time)");
                strSql2.Append(" values (");
                strSql2.Append("@title,@is_red,@sort_id,@add_time)");
                OleDbParameter[] parameters2 = {
					    new OleDbParameter("@title", OleDbType.VarChar,100),
					    new OleDbParameter("@is_red", OleDbType.Integer,4),
					    new OleDbParameter("@sort_id", OleDbType.Integer,4),
					    new OleDbParameter("@add_time", OleDbType.Date)};
                parameters2[0].Value = tags_title;
                parameters2[1].Value = 0;
                parameters2[2].Value = 99;
                parameters2[3].Value = DateTime.Now;
                DbHelperOleDb.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                tagsId = GetMaxId(conn, trans); ///插入成功查询最新的ID
            }
            //匹配Tags标签与文章之间的关系
            if (tagsId > 0)
            {
                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("insert into " + databaseprefix + "article_tags_relation(");
                strSql3.Append("article_id,tag_id)");
                strSql3.Append(" values (");
                strSql3.Append("@article_id,@tag_id)");
                OleDbParameter[] parameters3 = {
					    new OleDbParameter("@article_id", OleDbType.Integer,4),
					    new OleDbParameter("@tag_id", OleDbType.Integer,4)};
                parameters3[0].Value = article_id;
                parameters3[1].Value = tagsId;
                DbHelperOleDb.GetSingle(conn, trans, strSql3.ToString(), parameters3);
            }
        }

        /// <summary>
        /// 删除文章对应的Tags标签关系
        /// </summary>
        public bool Delete(OleDbConnection conn, OleDbTransaction trans, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_tags_relation");
            strSql.Append(" where article_id=@article_id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@article_id", OleDbType.Integer,4)};
            parameters[0].Value = article_id;
            int rows = DbHelperOleDb.ExecuteSql(conn, trans, strSql.ToString(), parameters);
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
    }
}