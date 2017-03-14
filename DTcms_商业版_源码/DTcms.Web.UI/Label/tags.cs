using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 文章Tags标签列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_tags(int top, string strwhere)
        {
            return new BLL.article_tags().GetList(top, strwhere, "count desc").Tables[0];
        }

        /// <summary>
        /// 文章Tags标签列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_tags(int top, string strwhere, string filedOrder)
        {
            return new BLL.article_tags().GetList(top, strwhere, filedOrder).Tables[0];
        }

        /// <summary>
        /// 获取文章Tags标签列表
        /// </summary>
        /// <param name="tags">标签列表</param>
        /// <returns></returns>
        protected DataTable get_article_tags(string tags)
        {
            //创建一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("id", Type.GetType("System.Int32"));
            dt.Columns.Add("title", Type.GetType("System.String"));
            dt.Columns.Add("call_index", Type.GetType("System.String"));

            if (string.IsNullOrEmpty(tags) || "" == tags)
            {
                return dt;
            }
            tags = tags.Replace("，",",");
            string[] arr = tags.Split(',');
            //遍历
            BLL.article_tags bll = new BLL.article_tags();
            foreach (string t in arr)
            {
                Model.article_tags model = bll.GetTitleModel(t);
                if (null != model)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = model.id;
                    dr["title"] = model.title;
                    dr["call_index"] = model.call_index;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
