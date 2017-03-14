using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Web.Plugin.Images.DAL
{
    /// <summary>
    /// 数据库访问层
    /// </summary>
    public partial class images
    {
        private string databaseprefix; //数据库表名前缀
        public images(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
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
            strSql.Append("select count(1) from " + databaseprefix + "images where id=@id");
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
            strSql.Append("select count(1) from " + databaseprefix + "images where title=@title");
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
            strSql.Append("select top 1 title from " + databaseprefix + "images where id=@id");
            SqlParameter[] parameters = {
            	new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            string result = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters)); ;
            if (!string.IsNullOrEmpty(result))
            {
                return result;
            }
            return string.Empty;
        }
        /// <summary>
        /// 返回数据总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H " + databaseprefix + "images");
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
        public int Add(Model.images model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "images(");
            strSql.Append("is_lock,sort_id,add_time,sign,title,img_url,link_url,back_color,content");
            strSql.Append(") values(");
            strSql.Append("@is_lock,@sort_id,@add_time,@sign,@title,@img_url,@link_url,@back_color,@content)");
            SqlParameter[] parameters = {
                new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
                new SqlParameter("@sort_id", SqlDbType.Int,4),
                new SqlParameter("@add_time", SqlDbType.DateTime),
                new SqlParameter("@sign", SqlDbType.NVarChar,255),
                new SqlParameter("@title", SqlDbType.NVarChar,255),
                new SqlParameter("@img_url", SqlDbType.NVarChar,255),
                new SqlParameter("@link_url", SqlDbType.NVarChar,255),
				new SqlParameter("@back_color", SqlDbType.NVarChar,255),
                new SqlParameter("@content", SqlDbType.NText)
            };
            parameters[0].Value = model.is_lock;
            parameters[1].Value = model.sort_id;
            parameters[2].Value = model.add_time;
            parameters[3].Value = model.sign;
            parameters[4].Value = model.title;
            parameters[5].Value = model.img_url;
            parameters[6].Value = model.link_url;
            parameters[7].Value = model.back_color;
            parameters[8].Value = model.content;
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

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.images model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "images set ");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("sign=@sign,");
            strSql.Append("title=@title,");
            strSql.Append("img_url=@img_url,");
            strSql.Append("link_url=@link_url,");
            strSql.Append("back_color=@back_color,");
            strSql.Append("content=@content");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
                new SqlParameter("@sort_id", SqlDbType.Int,4),
                new SqlParameter("@add_time", SqlDbType.DateTime),
                new SqlParameter("@sign", SqlDbType.NVarChar,255),
                new SqlParameter("@title", SqlDbType.NVarChar,255),
                new SqlParameter("@img_url", SqlDbType.NVarChar,255),
                new SqlParameter("@link_url", SqlDbType.NVarChar,255),
				new SqlParameter("@back_color", SqlDbType.NVarChar,255),
                new SqlParameter("@content", SqlDbType.NText),
                new SqlParameter("@id", SqlDbType.Int,4)
             };
            parameters[0].Value = model.is_lock;
            parameters[1].Value = model.sort_id;
            parameters[2].Value = model.add_time;
            parameters[3].Value = model.sign;
            parameters[4].Value = model.title;
            parameters[5].Value = model.img_url;
            parameters[6].Value = model.link_url;
            parameters[7].Value = model.back_color;
            parameters[8].Value = model.content;
            parameters[9].Value = model.id;
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

        #region 更新一列数据
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "images set " + strValue);
            strSql.Append(" where id=" + id);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            //删除前先删除LOGO文件
            string img_url = GetImages(id);
            if (img_url != "")
            {
                Utils.DeleteFile("~" + img_url);
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "images where id=@id");
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
        /// <summary>
        /// 返回图片
        /// </summary>
        public string GetImages(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 img_url from " + databaseprefix + "images");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            string Images = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (string.IsNullOrEmpty(Images))
            {
                return "";
            }
            return Images;
        }
        #endregion

        #region 返回一个实体
        /// <summary>
        /// 返回一个实体
        /// </summary>
        public Model.images GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select is_lock,id,sort_id,add_time,sign,title,img_url,link_url,back_color,back_color,content from " + databaseprefix + "images where id=@id");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Model.images model = new Model.images();
                if (ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                model.sign = ds.Tables[0].Rows[0]["sign"].ToString();
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                model.link_url = ds.Tables[0].Rows[0]["link_url"].ToString();
                model.back_color = ds.Tables[0].Rows[0]["back_color"].ToString();
                model.content = ds.Tables[0].Rows[0]["content"].ToString();
                return model;
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
            strSql.Append("is_lock,id,sort_id,add_time,sign,title,img_url,link_url,back_color,content from " + databaseprefix + "images");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
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
            strSql.Append("select * FROM " + databaseprefix + "images");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 扩展
        /// <summary>
        /// 获取标记列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetSign()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct sign from " + databaseprefix + "images");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
    }
}