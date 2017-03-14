using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class article_list : Web.UI.BasePage
    {
        protected int page;         //当前页码
        protected int category_id;  //类别ID
        protected int totalcount;   //OUT数据总数
        protected string pagelist;  //分页页码
        protected string call_index;  //调用别名
        protected string page_param;

        protected Model.article_category model = new Model.article_category(); //分类的实体
        protected Model.channel channelModel = new Model.channel(); //分类的实体

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            page = DTRequest.GetQueryInt("page", 1);
            category_id = DTRequest.GetQueryInt("category_id");
            call_index = Utils.SafeXXS(DTRequest.GetQueryString("call_index"));

            BLL.article_category bll = new BLL.article_category();
            model.title = "所有类别";
            if (category_id > 0) //如果ID获取到，将使用ID
            {
                if (!bll.Exists(category_id))
                {
                    HttpContext.Current.Response.Redirect(linkurl("error"));
                    return;
                }
                model = bll.GetModel(category_id);
            }
            else if (!string.IsNullOrEmpty(call_index)) //否则检查设置的别名
            {
                if (!bll.Exists(call_index))
                {
                    HttpContext.Current.Response.Redirect(linkurl("error"));
                    return;
                }
                model = bll.GetModel(call_index);
                //赋值类别ID
                category_id = model.id;
            }
            //else
            //{
            //    HttpContext.Current.Response.Redirect(linkurl("error"));
            //    return;
            //}
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
            //分页参数
            if (model.call_index != "")
            {
                page_param = model.call_index;
            }
            else
            {
                page_param = model.id.ToString();
            }
            //获取频道内容
            channelModel = new BLL.channel().GetModel(model.channel_id);
        }

        /// <summary>
        /// 验证频道列表数据
        /// </summary>
        /// <param name="channel_name"></param>
        protected void validate_channel_data(string channel_name)
        {
            if (category_id > 0 && null != channelModel && !channelModel.name.Equals(channel_name))
            {
                HttpContext.Current.Response.Redirect(linkurl("error"));
                return;
            }
        }
    }
}
