using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 扩展属性数据访问类:article_attribute_value
    /// </summary>
    public partial class article_attribute_value
    {
        private string databaseprefix; //数据库表名前缀
        public article_attribute_value(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_attribute_value");
            strSql.Append(" where article_id=@article_id");
            SqlParameter[] parameters = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters[0].Value = article_id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得指定分类下所有文章扩展内容
        /// </summary>
        /// <param name="category_id">分类ID</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        public DataSet GetList(int category_id, string field)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select " + field + " from " + databaseprefix + "article_attribute_value where article_id in (select id from " + databaseprefix + "article where category_id=" + category_id + ")");
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
