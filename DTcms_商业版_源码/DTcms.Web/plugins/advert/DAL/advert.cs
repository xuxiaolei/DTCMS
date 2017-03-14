using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Web.Plugin.Advert.DAL
{
    /// <summary>
    /// 数据访问类:advert
    /// </summary>
    public partial class advert
    {
        private string column;
        private string databaseprefix;
        public advert(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
            this.column = "id,title,type,price,remark,view_num,view_width,view_height,add_time";
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
            strSql.Append("select count(1) from [" + databaseprefix + "advert] where id=@id");
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
            strSql.Append("select count(1) from [" + databaseprefix + "advert] where title=@title");
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
            strSql.Append("select title from [" + databaseprefix + "advert] where id=@id");
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
            strSql.Append("select count(*) as H [" + databaseprefix + "advert]");
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
        public int Add(Model.advert model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [" + databaseprefix + "advert](");
            strSql.Append("title,type,price,remark,view_num,view_width,view_height,add_time");
            strSql.Append(") values(");
            strSql.Append("@title,@type,@price,@remark,@view_num,@view_width,@view_height,@add_time)");
            SqlParameter[] parameters = {
                new SqlParameter("@title", SqlDbType.NVarChar,100),
                new SqlParameter("@type", SqlDbType.TinyInt,1),
                new SqlParameter("@price", SqlDbType.Decimal,13),
                new SqlParameter("@remark", SqlDbType.NVarChar,255),
                new SqlParameter("@view_num", SqlDbType.Int,6),
                new SqlParameter("@view_width", SqlDbType.Int,6),
                new SqlParameter("@view_height", SqlDbType.Int,6),
				new SqlParameter("@add_time", SqlDbType.DateTime)
            };
            parameters[0].Value = model.title;
            parameters[1].Value = model.type;
            parameters[2].Value = model.price;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.view_num;
            parameters[5].Value = model.view_width;
            parameters[6].Value = model.view_height;
            parameters[7].Value = model.add_time;
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
            strSql.Append("update [" + databaseprefix + "advert] set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.advert model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [" + databaseprefix + "advert] set ");
            strSql.Append("title=@title,");
            strSql.Append("type=@type,");
            strSql.Append("price=@price,");
            strSql.Append("remark=@remark,");
            strSql.Append("view_num=@view_num,");
            strSql.Append("view_width=@view_width,");
            strSql.Append("view_height=@view_height,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@title", SqlDbType.NVarChar,100),
                new SqlParameter("@type", SqlDbType.TinyInt,1),
                new SqlParameter("@price", SqlDbType.Decimal,13),
                new SqlParameter("@remark", SqlDbType.NVarChar,255),
                new SqlParameter("@view_num", SqlDbType.Int,6),
                new SqlParameter("@view_width", SqlDbType.Int,6),
                new SqlParameter("@view_height", SqlDbType.Int,6),
				new SqlParameter("@add_time", SqlDbType.DateTime),
                new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = model.title;
            parameters[1].Value = model.type;
            parameters[2].Value = model.price;
            parameters[3].Value = model.remark;
            parameters[4].Value = model.view_num;
            parameters[5].Value = model.view_width;
            parameters[6].Value = model.view_height;
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
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [" + databaseprefix + "advert] where id=@id");
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
        public Model.advert GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + this.column + " from [" + databaseprefix + "advert] where id=@id");
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
            strSql.Append(this.column + " from [" + databaseprefix + "advert]");
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
            strSql.Append("select * from [" + databaseprefix + "advert]");
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
        private Model.advert DataRowToModel(DataRow row)
        {
            Model.advert model = new Model.advert();
            if (row != null)
            {
                if (row["id"].ToString() != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["title"].ToString() != "")
                {
                    model.title = row["title"].ToString();
                }
                if (row["type"].ToString() != null && row["type"].ToString() != "")
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (row["price"].ToString() != null && row["price"].ToString() != "")
                {
                    model.price = decimal.Parse(row["price"].ToString());
                }
                if (row["remark"].ToString() != "")
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["view_num"].ToString() != null && row["view_num"].ToString() != "")
                {
                    model.view_num = int.Parse(row["view_num"].ToString());
                }
                if (row["view_width"].ToString() != null && row["view_width"].ToString() != "")
                {
                    model.view_width = int.Parse(row["view_width"].ToString());
                }
                if (row["view_height"].ToString() != null && row["view_height"].ToString() != "")
                {
                    model.view_height = int.Parse(row["view_height"].ToString());
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