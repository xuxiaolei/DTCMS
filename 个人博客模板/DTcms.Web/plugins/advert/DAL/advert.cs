using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Web.Plugin.Advert.DAL
{
    /// <summary>
    /// 数据访问类:advert
    /// </summary>
    public partial class advert
    {
        private string databaseprefix; //数据库表名前缀
        public advert(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "advert order by id desc";
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
            strSql.Append("select count(1) from " + databaseprefix + "advert");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回广告位名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "advert");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperOleDb.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.advert model)
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
                        strSql.Append("insert into " + databaseprefix + "advert(");
                        strSql.Append("title,type,price,remark,view_num,view_width,view_height,target,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@title,@type,@price,@remark,@view_num,@view_width,@view_height,@target,@add_time)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@title", OleDbType.VarChar,100),
					            new OleDbParameter("@type", OleDbType.Integer,4),
					            new OleDbParameter("@price", OleDbType.Decimal,9),
					            new OleDbParameter("@remark", OleDbType.VarChar,255),
					            new OleDbParameter("@view_num", OleDbType.Integer,4),
					            new OleDbParameter("@view_width", OleDbType.Integer,4),
					            new OleDbParameter("@view_height", OleDbType.Integer,4),
					            new OleDbParameter("@target", OleDbType.VarChar,30),
					            new OleDbParameter("@add_time", OleDbType.Date)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.type;
                        parameters[2].Value = model.price;
                        parameters[3].Value = model.remark;
                        parameters[4].Value = model.view_num;
                        parameters[5].Value = model.view_width;
                        parameters[6].Value = model.view_height;
                        parameters[7].Value = model.target;
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
        public bool Update(Model.advert model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "advert set ");
            strSql.Append("title=@title,");
            strSql.Append("type=@type,");
            strSql.Append("price=@price,");
            strSql.Append("remark=@remark,");
            strSql.Append("view_num=@view_num,");
            strSql.Append("view_width=@view_width,");
            strSql.Append("view_height=@view_height,");
            strSql.Append("target=@target,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@title", OleDbType.VarChar,100),
					new OleDbParameter("@type", OleDbType.Integer,4),
					new OleDbParameter("@price", OleDbType.Decimal,9),
					new OleDbParameter("@remark", OleDbType.VarChar,255),
					new OleDbParameter("@view_num", OleDbType.Integer,4),
					new OleDbParameter("@view_width", OleDbType.Integer,4),
					new OleDbParameter("@view_height", OleDbType.Integer,4),
					new OleDbParameter("@target", OleDbType.VarChar,30),
					new OleDbParameter("@add_time", OleDbType.Date),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.type;
            parameters[2].Value = model.price;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.view_num;
            parameters[5].Value = model.view_width;
            parameters[6].Value = model.view_height;
            parameters[7].Value = model.target;
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
        /// 删除一条数据及其子表
        /// </summary>
        public bool Delete(int id)
        {
            Hashtable sqllist = new Hashtable();

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "advert_banner ");
            strSql2.Append(" where aid=@aid ");
            OleDbParameter[] parameters2 = {
					new OleDbParameter("@aid", OleDbType.Integer,4)};
            parameters2[0].Value = id;
            sqllist.Add(strSql2.ToString(), parameters2);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "advert ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;
            sqllist.Add(strSql.ToString(), parameters);

            return DbHelperOleDb.ExecuteSqlTran(sqllist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.advert GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,type,price,remark,view_num,view_width,view_height,target,add_time from " + databaseprefix + "advert ");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            Model.advert model = new Model.advert();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["type"] != null && ds.Tables[0].Rows[0]["type"].ToString() != "")
                {
                    model.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["price"] != null && ds.Tables[0].Rows[0]["price"].ToString() != "")
                {
                    model.price = decimal.Parse(ds.Tables[0].Rows[0]["price"].ToString());
                }
                if (ds.Tables[0].Rows[0]["remark"] != null && ds.Tables[0].Rows[0]["remark"].ToString() != "")
                {
                    model.remark = ds.Tables[0].Rows[0]["remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["view_num"] != null && ds.Tables[0].Rows[0]["view_num"].ToString() != "")
                {
                    model.view_num = int.Parse(ds.Tables[0].Rows[0]["view_num"].ToString());
                }
                if (ds.Tables[0].Rows[0]["view_width"] != null && ds.Tables[0].Rows[0]["view_width"].ToString() != "")
                {
                    model.view_width = int.Parse(ds.Tables[0].Rows[0]["view_width"].ToString());
                }
                if (ds.Tables[0].Rows[0]["view_height"] != null && ds.Tables[0].Rows[0]["view_height"].ToString() != "")
                {
                    model.view_height = int.Parse(ds.Tables[0].Rows[0]["view_height"].ToString());
                }
                model.target = ds.Tables[0].Rows[0]["target"].ToString();
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
            strSql.Append("select id,title,type,price,remark,view_num,view_width,view_height,target,add_time ");
            strSql.Append(" FROM " + databaseprefix + "advert ");
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
            strSql.Append(" id,title,type,price,remark,view_num,view_width,view_height,target,add_time ");
            strSql.Append(" FROM " + databaseprefix + "advert ");
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
            strSql.Append("select * FROM " + databaseprefix + "advert");
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