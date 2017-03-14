using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Web.Plugin.Menu.DAL
{
    /// <summary>
    /// 数据库访问层
    /// </summary>
    public partial class menu
    {
        private string column;
        private string databaseprefix; //数据库表名前缀
        public menu(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
            this.column = "id,title,link_url,parent_id,class_list,class_layer,open_mode,sort_id,css_txt,img_url,is_lock,add_time";
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
            strSql.Append("select count(1) from " + databaseprefix + "menu where id=@id");
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
            strSql.Append("select count(1) from " + databaseprefix + "menu where title=@title");
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
            strSql.Append("select top 1 title from " + databaseprefix + "menu where id=@id");
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
            strSql.Append("select count(*) as H " + databaseprefix + "menu");
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
        public int Add(Model.menu model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into [" + databaseprefix + "menu](");
                        strSql.Append("title,link_url,parent_id,class_list,class_layer,open_mode,sort_id,css_txt,img_url,is_lock,add_time");
                        strSql.Append(") values(");
                        strSql.Append("@title,@link_url,@parent_id,@class_list,@class_layer,@open_mode,@sort_id,@css_txt,@img_url,@is_lock,@add_time)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
                            new SqlParameter("@title", SqlDbType.NVarChar,255),
                            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
                            new SqlParameter("@parent_id", SqlDbType.Int,6),
                            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
                            new SqlParameter("@class_layer", SqlDbType.Int,6),
                            new SqlParameter("@open_mode", SqlDbType.NVarChar,20),
                            new SqlParameter("@sort_id", SqlDbType.Int,6),
                            new SqlParameter("@css_txt", SqlDbType.NVarChar,50),
                            new SqlParameter("@img_url", SqlDbType.NVarChar,200),
                            new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
                            new SqlParameter("@add_time", SqlDbType.DateTime)
                        };
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.link_url;
                        parameters[2].Value = model.parent_id;
                        parameters[3].Value = model.class_list;
                        parameters[4].Value = model.class_layer;
                        parameters[5].Value = model.open_mode;
                        parameters[6].Value = model.sort_id;
                        parameters[7].Value = model.css_txt;
                        parameters[8].Value = model.img_url;
                        parameters[9].Value = model.is_lock;
                        parameters[10].Value = model.add_time;

                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务
                        model.id = Convert.ToInt32(obj);

                        if (model.parent_id > 0)
                        {
                            Model.menu model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }
                        //修改节点列表和深度
                        DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "menu set class_list='" + model.class_list + "', class_layer=" + model.class_layer + " where id=" + model.id); //带事务
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            return model.id;
        }
        #endregion

        #region 修改一列数据
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "menu set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.menu model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //先判断选中的父节点是否被包含
                        if (IsContainNode(model.id, model.parent_id))
                        {
                            //查找旧数据
                            Model.menu oldModel = GetModel(model.id);
                            //查找旧父节点数据
                            string class_list = "," + model.parent_id + ",";
                            int class_layer = 1;
                            if (oldModel.parent_id > 0)
                            {
                                Model.menu oldParentModel = GetModel(conn, trans, oldModel.parent_id); //带事务
                                class_list = oldParentModel.class_list + model.parent_id + ",";
                                class_layer = oldParentModel.class_layer + 1;
                            }
                            //先提升选中的父节点
                            DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "menu set parent_id=" + oldModel.parent_id + ",class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + model.parent_id); //带事务
                            UpdateChilds(conn, trans, model.parent_id); //带事务
                        }
                        //更新子节点
                        if (model.parent_id > 0)
                        {
                            Model.menu model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update [" + databaseprefix + "menu] set ");
                        strSql.Append("title=@title,");
                        strSql.Append("link_url=@link_url,");
                        strSql.Append("parent_id=@parent_id,");
                        strSql.Append("class_list=@class_list,");
                        strSql.Append("class_layer=@class_layer,");
                        strSql.Append("open_mode=@open_mode,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("css_txt=@css_txt,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("is_lock=@is_lock,");
                        strSql.Append("add_time=@add_time");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
                            new SqlParameter("@title", SqlDbType.NVarChar,255),
                            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
                            new SqlParameter("@parent_id", SqlDbType.Int,6),
                            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
                            new SqlParameter("@class_layer", SqlDbType.Int,6),
                            new SqlParameter("@open_mode", SqlDbType.NVarChar,20),
                            new SqlParameter("@sort_id", SqlDbType.Int,6),
                            new SqlParameter("@css_txt", SqlDbType.NVarChar,50),
                            new SqlParameter("@img_url", SqlDbType.NVarChar,200),
                            new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
                            new SqlParameter("@add_time", SqlDbType.DateTime),
            			    new SqlParameter("@id", SqlDbType.Int,4)
            			};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.link_url;
                        parameters[2].Value = model.parent_id;
                        parameters[3].Value = model.class_list;
                        parameters[4].Value = model.class_layer;
                        parameters[5].Value = model.open_mode;
                        parameters[6].Value = model.sort_id;
                        parameters[7].Value = model.css_txt;
                        parameters[8].Value = model.img_url;
                        parameters[9].Value = model.is_lock;
                        parameters[10].Value = model.add_time;
                        parameters[11].Value = model.id;

                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //更新子节点
                        UpdateChilds(conn, trans, model.id);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            Model.menu model = GetModel(id);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "menu where id=@id");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                Utils.DeleteFile(model.img_url);
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
        public Model.menu GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + this.column + " from [" + databaseprefix + "menu] where id=@id");
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
        /// <summary>
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        public Model.menu GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + this.column + " from " + databaseprefix + "menu");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.menu model = new Model.menu();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
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
            strSql.Append(this.column + " from [" + databaseprefix + "menu]");
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

        #region 获得前几行数据
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder, int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString() + " ");
            }
            strSql.Append(this.column + " from " + databaseprefix + "menu");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parent_id);
            return newData;
        }
        #endregion

        #region 取得所有列表
        /// <summary>
        /// 取得所有列表
        /// </summary>
        public DataTable GetList(int Top, int parent_id, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString() + " ");
            }
            strSql.Append("id,title,link_url,parent_id,class_list,class_layer,open_mode,sort_id,css_txt,img_url,is_lock,add_time from [" + databaseprefix + "menu]");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
            }
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DataTable
            GetChilds(oldData, newData, parent_id);
            return newData;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 组合成对象实体
        /// </summary>
        private Model.menu DataRowToModel(DataRow row)
        {
            Model.menu model = new Model.menu();
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
                if (row["link_url"].ToString() != "")
                {
                    model.link_url = row["link_url"].ToString();
                }
                if (row["parent_id"].ToString() != null && row["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(row["parent_id"].ToString());
                }
                if (row["class_list"].ToString() != "")
                {
                    model.class_list = row["class_list"].ToString();
                }
                if (row["class_layer"].ToString() != null && row["class_layer"].ToString() != "")
                {
                    model.class_layer = int.Parse(row["class_layer"].ToString());
                }
                if (row["open_mode"].ToString() != "")
                {
                    model.open_mode = row["open_mode"].ToString();
                }
                if (row["sort_id"].ToString() != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["css_txt"].ToString() != "")
                {
                    model.css_txt = row["css_txt"].ToString();
                }
                if (row["img_url"].ToString() != "")
                {
                    model.img_url = row["img_url"].ToString();
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

        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["title"] = dr[i]["title"].ToString();
                row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                row["class_list"] = dr[i]["class_list"].ToString();
                row["class_layer"] = int.Parse(dr[i]["class_layer"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                row["link_url"] = dr[i]["link_url"].ToString();
                row["is_lock"] = int.Parse(dr[i]["is_lock"].ToString());
                row["css_txt"] = dr[i]["css_txt"].ToString();
                row["img_url"] = dr[i]["img_url"].ToString();
                row["open_mode"] = dr[i]["open_mode"].ToString();
                row["add_time"] = Convert.ToDateTime(dr[i]["add_time"].ToString());
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()));
            }
        }

        /// <summary>
        /// 修改子节点的ID列表及深度（自身迭代）
        /// </summary>
        /// <param name="parent_id"></param>
        private void UpdateChilds(SqlConnection conn, SqlTransaction trans, int parent_id)
        {
            //查找父节点信息
            Model.menu model = GetModel(conn, trans, parent_id);
            if (model != null)
            {
                //查找子节点
                string strSql = "select id from [" + databaseprefix + "menu] where parent_id=" + parent_id;
                DataSet ds = DbHelperSQL.Query(conn, trans, strSql); //带事务
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //修改子节点的ID列表及深度
                    int id = int.Parse(dr["id"].ToString());
                    string class_list = model.class_list + id + ",";
                    int class_layer = model.class_layer + 1;
                    DbHelperSQL.ExecuteSql(conn, trans, "update [" + databaseprefix + "menu] set class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + id); //带事务
                    //调用自身迭代
                    this.UpdateChilds(conn, trans, id); //带事务
                }
            }
        }

        /// <summary>
        /// 验证节点是否被包含
        /// </summary>
        /// <param name="id">待查询的节点</param>
        /// <param name="parent_id">父节点</param>
        /// <returns></returns>
        private bool IsContainNode(int id, int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [" + databaseprefix + "menu] where class_list like '%," + id + ",%' and id=" + parent_id);
            return DbHelperSQL.Exists(strSql.ToString());
        }
        #endregion
    }
}