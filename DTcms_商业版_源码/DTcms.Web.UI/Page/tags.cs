using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using DTcms.Common;


namespace DTcms.Web.UI.Page
{
    public partial class tags : Web.UI.BasePage
    {
        protected int id;  //文章ID
        protected string call_index; //调用名

        protected string channel = string.Empty;//频道名称
        protected Model.article_tags model = new Model.article_tags();

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            id = DTRequest.GetQueryInt("id");
            call_index = Utils.SafeXXS(DTRequest.GetQueryString("call_index"));
            channel = Utils.SafeXXS(DTRequest.GetQueryString("channel"));

            //验证tags标签是否存在
            BLL.article_tags bll = new BLL.article_tags();
            
            if (id > 0) //如果ID获取到，将使用ID
            {
                if (!bll.Exists(id))
                {
                    HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，您要浏览的页面不存在或已删除！")));
                    return;
                }
                model = bll.GetModel(id);
            }
            else if (!string.IsNullOrEmpty(call_index)) //否则检查设置的别名
            {
                if (!bll.Exists(call_index))
                {
                    HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，您要浏览的页面不存在或已删除！")));
                    return;
                }
                model = bll.GetModel(call_index);
                id = model.id;
            }
            else
            {
                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，您要浏览的页面不存在或已删除！")));
                return;
            }

            //判断SEO标题
            if (string.IsNullOrEmpty(model.seo_title) || "" == model.seo_title)
            {
                model.seo_title = model.title;
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        protected DataTable get_search_list(int tags_id)
        {
            //创建一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("id", Type.GetType("System.Int32"));
            dt.Columns.Add("title", Type.GetType("System.String"));
            dt.Columns.Add("zhaiyao", Type.GetType("System.String"));
            dt.Columns.Add("channel_id", Type.GetType("System.String"));
            dt.Columns.Add("link_url", Type.GetType("System.String"));
            dt.Columns.Add("add_time", Type.GetType("System.String"));
            dt.Columns.Add("img_url", Type.GetType("System.String"));
            dt.Columns.Add("tags", Type.GetType("System.String"));
            dt.Columns.Add("click", Type.GetType("System.Int32"));

            //创建一个DataSet,判断是使用Tags还是关健字查询
            DataSet ds = new BLL.article().GetSearch(channel, tags_id, 20, "status=0", "add_time desc,id desc");

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr1 = ds.Tables[0].Rows[i];
                    string link_url = get_url_rewrite(Utils.StrToInt(dr1["channel_id"].ToString(), 0), dr1["call_index"].ToString(), Utils.StrToInt(dr1["id"].ToString(), 0));
                    if (!string.IsNullOrEmpty(link_url))
                    {
                        DataRow dr = dt.NewRow();
                        dr["id"] = dr1["id"]; //自增ID
                        dr["title"] = dr1["title"]; //标题
                        dr["zhaiyao"] = dr1["zhaiyao"]; //摘要
                        dr["link_url"] = link_url; //链接地址
                        dr["add_time"] = dr1["add_time"]; //发布时间
                        dr["channel_id"] = dr1["channel_id"]; //频道ID
                        dr["img_url"] = dr1["img_url"]; //发布时间
                        dr["tags"] = dr1["tags"]; //分类
                        dr["click"] = dr1["click"]; //点击量
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        //查找匹配的URL
        private string get_url_rewrite(int channel_id, string call_index, int id)
        {
            if (channel_id == 0)
            {
                return string.Empty;
            }
            string querystring = id.ToString();
            string channel_name = new BLL.channel().GetChannelName(channel_id);
            if (string.IsNullOrEmpty(channel_name))
            {
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(call_index))
            {
                querystring = call_index;
            }
            BLL.url_rewrite bll = new BLL.url_rewrite();
            Model.url_rewrite model = bll.GetInfo(channel_name, "detail");
            if (model != null)
            {
                return linkurl(model.name, querystring);
            }
            return string.Empty;
        }
    }
}
