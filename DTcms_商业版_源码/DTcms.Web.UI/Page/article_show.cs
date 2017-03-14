using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using DTcms.Common;
using System.Data;

namespace DTcms.Web.UI.Page
{
    public partial class article_show : Web.UI.BasePage
    {
        protected int id;  //文章ID
        protected string call_index; //调用名

        protected Model.article model = new Model.article();
        protected Model.article_category categoryModel = new Model.article_category(); //分类的实体
        protected Model.channel channelModel = new Model.channel(); //频道的实体
        
        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            id = DTRequest.GetQueryInt("id");
            call_index = Utils.SafeXXS(DTRequest.GetQueryString("call_index"));

            BLL.article bll = new BLL.article();

            if (id > 0) //如果ID获取到，将使用ID
            {
                if (!bll.Exists(id))
                {
                    HttpContext.Current.Response.Redirect(linkurl("error"));
                    return;
                }
                model = bll.GetModel(id);
            }
            else if (!string.IsNullOrEmpty(call_index)) //否则检查设置的别名
            {
                if (!bll.Exists(call_index))
                {
                    HttpContext.Current.Response.Redirect(linkurl("error"));
                    return;
                }
                model = bll.GetModel(call_index);
                //赋值文章ID
                id = model.id;
            }
            else
            {
                HttpContext.Current.Response.Redirect(linkurl("error"));
                return;
            }
            //判断是否是链接
            if (!string.IsNullOrEmpty(model.link_url))
            {
                HttpContext.Current.Response.Redirect(model.link_url);
                return;
            }
            //判断SEO标题
            if (string.IsNullOrEmpty(model.seo_title) || "" == model.seo_title)
            {
                model.seo_title = model.title;
            }
            //获取频道内容
            channelModel = new BLL.channel().GetModel(model.channel_id);
            //获取类别内容
            categoryModel = new BLL.article_category().GetModel(model.category_id);
        }

        /// <summary>
        /// 验证频道列表数据
        /// </summary>
        /// <param name="channel_name"></param>
        protected void validate_channel_data(string channel_name)
        {
            if (null != channelModel && !channelModel.name.Equals(channel_name))
            {
                HttpContext.Current.Response.Redirect(linkurl("error"));
                return;
            }
        }
        
        /// <summary>
        /// 获取上一条下一条的链接
        /// </summary>
        /// <param name="urlkey">urlkey</param>
        /// <param name="type">-1代表上一条，1代表下一条</param>
        /// <param name="defaultvalue">默认文本</param>
        /// <param name="callIndex">是否使用别名，0使用ID，1使用别名</param>
        /// <returns>A链接</returns>
        protected string get_prevandnext_article(string urlkey, int type, string defaultvalue, int callIndex)
        {
            string symbol = (type == -1 ? "<" : ">");
            BLL.article bll = new BLL.article();
            string str = string.Empty;
            str = " and category_id=" + model.category_id;

            string orderby = type == -1 ? "id desc" : "id asc";
            DataSet ds = bll.GetList(1, "channel_id=" + model.channel_id + " " + str + " and status=0 and Id" + symbol + id, orderby);
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                return defaultvalue;
            }
            if (callIndex == 1 && !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["call_index"].ToString()))
            {
                return "<a href=\"" + linkurl(urlkey, ds.Tables[0].Rows[0]["call_index"].ToString()) + "\">" + ds.Tables[0].Rows[0]["title"] + "</a>";
            }
            return "<a href=\"" + linkurl(urlkey, ds.Tables[0].Rows[0]["id"].ToString()) + "\">" + ds.Tables[0].Rows[0]["title"] + "</a>";
        }
    }
}
