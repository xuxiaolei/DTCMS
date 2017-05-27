using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:支付方式
    /// </summary>
    public partial class payment
    {
        private string databaseprefix; //数据库表名前缀
        public payment(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(OleDbConnection conn, OleDbTransaction trans)
        {
            string strSql = "select top 1 id from " + databaseprefix + "payment order by id desc";
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
            strSql.Append("select count(1) from " + databaseprefix + "payment");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.payment model)
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
                        strSql.Append("insert into " + databaseprefix + "payment(");
                        strSql.Append("title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_mobile,is_lock,api_path)");
                        strSql.Append(" values (");
                        strSql.Append("@title,@img_url,@remark,@type,@poundage_type,@poundage_amount,@sort_id,@is_mobile,@is_lock,@api_path)");
                        OleDbParameter[] parameters = {
					            new OleDbParameter("@title", OleDbType.VarChar,100),
					            new OleDbParameter("@img_url", OleDbType.VarChar,255),
					            new OleDbParameter("@remark", OleDbType.VarChar,500),
					            new OleDbParameter("@type", OleDbType.Integer,4),
					            new OleDbParameter("@poundage_type", OleDbType.Integer,4),
					            new OleDbParameter("@poundage_amount", OleDbType.Decimal,5),
					            new OleDbParameter("@sort_id", OleDbType.Integer,4),
					            new OleDbParameter("@is_mobile", OleDbType.Integer,4),
					            new OleDbParameter("@is_lock", OleDbType.Integer,4),
					            new OleDbParameter("@api_path", OleDbType.VarChar,100)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.img_url;
                        parameters[2].Value = model.remark;
                        parameters[3].Value = model.type;
                        parameters[4].Value = model.poundage_type;
                        parameters[5].Value = model.poundage_amount;
                        parameters[6].Value = model.sort_id;
                        parameters[7].Value = model.is_mobile;
                        parameters[8].Value = model.is_lock;
                        parameters[9].Value = model.api_path;
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
        public bool Update(Model.payment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "payment set ");
            strSql.Append("title=@title,");
            strSql.Append("img_url=@img_url,");
            strSql.Append("[remark]=@remark,");
            strSql.Append("type=@type,");
            strSql.Append("poundage_type=@poundage_type,");
            strSql.Append("poundage_amount=@poundage_amount,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("is_mobile=@is_mobile,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("api_path=@api_path");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@title", OleDbType.VarChar,100),
					new OleDbParameter("@img_url", OleDbType.VarChar,255),
					new OleDbParameter("@remark", OleDbType.VarChar,500),
					new OleDbParameter("@type", OleDbType.Integer,4),
					new OleDbParameter("@poundage_type", OleDbType.Integer,4),
					new OleDbParameter("@poundage_amount", OleDbType.Decimal,5),
					new OleDbParameter("@sort_id", OleDbType.Integer,4),
					new OleDbParameter("@is_mobile", OleDbType.Integer,4),
					new OleDbParameter("@is_lock", OleDbType.Integer,4),
					new OleDbParameter("@api_path", OleDbType.VarChar,100),
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.img_url;
            parameters[2].Value = model.remark;
            parameters[3].Value = model.type;
            parameters[4].Value = model.poundage_type;
            parameters[5].Value = model.poundage_amount;
            parameters[6].Value = model.sort_id;
            parameters[7].Value = model.is_mobile;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.api_path;
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
            strSql.Append("delete from " + databaseprefix + "payment ");
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
        public Model.payment GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_mobile,is_lock,api_path");
            strSql.Append(" from " + databaseprefix + "payment");
            strSql.Append(" where id=@id");
            OleDbParameter[] parameters = {
					new OleDbParameter("@id", OleDbType.Integer,4)};
            parameters[0].Value = id;

            Model.payment model = new Model.payment();
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_mobile,is_lock,api_path ");
            strSql.Append(" FROM " + databaseprefix + "payment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort_id asc,id desc");
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
            strSql.Append(" id,title,img_url,[remark],type,poundage_type,poundage_amount,sort_id,is_mobile,is_lock,api_path ");
            strSql.Append(" FROM " + databaseprefix + "payment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperOleDb.Query(strSql.ToString());
        }
        #endregion

        #region 扩展方法============================
        /// <summary>
        /// 返回标题名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "payment");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperOleDb.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "-";
            }
            return title;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "payment set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperOleDb.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.payment DataRowToModel(DataRow row)
        {
            Model.payment model = new Model.payment();
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
                if (row["img_url"] != null)
                {
                    model.img_url = row["img_url"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["type"] != null && row["type"].ToString() != "")
                {
                    model.type = int.Parse(row["type"].ToString());
                }
                if (row["poundage_type"] != null && row["poundage_type"].ToString() != "")
                {
                    model.poundage_type = int.Parse(row["poundage_type"].ToString());
                }
                if (row["poundage_amount"] != null && row["poundage_amount"].ToString() != "")
                {
                    model.poundage_amount = decimal.Parse(row["poundage_amount"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["is_mobile"] != null && row["is_mobile"].ToString() != "")
                {
                    model.is_mobile = int.Parse(row["is_mobile"].ToString());
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (row["api_path"] != null)
                {
                    model.api_path = row["api_path"].ToString();
                }
            }
            return model;
        }
        #endregion
    }
}