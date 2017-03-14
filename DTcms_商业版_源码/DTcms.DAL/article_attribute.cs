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
    /// 扩展参数
    /// </summary>
    public partial class article_attribute
    {
        private string databaseprefix; //数据库表名前缀
        public article_attribute(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_attribute> GetList(int article_id)
        {
            List<Model.article_attribute> modelList = new List<Model.article_attribute>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select article_id,title,value ");
            strSql.Append(" FROM " + databaseprefix + "article_attribute ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.article_attribute model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.article_attribute();
                    if (dt.Rows[n]["article_id"].ToString() != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["title"].ToString() != "")
                    {
                        model.title = dt.Rows[n]["title"].ToString();
                    }
                    if (dt.Rows[n]["value"].ToString() != "")
                    {
                        model.value = dt.Rows[n]["value"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #region 扩展
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_attribute> GetListTable(DataTable dt)
        {
            List<Model.article_attribute> modelList = new List<Model.article_attribute>();

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.article_attribute model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.article_attribute();

                    if (dt.Rows[n]["article_id"].ToString() != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["title"].ToString() != "")
                    {
                        model.title = dt.Rows[n]["title"].ToString();
                    }
                    if (dt.Rows[n]["value"].ToString() != "")
                    {
                        model.value = dt.Rows[n]["value"].ToString();
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion
    }
}
