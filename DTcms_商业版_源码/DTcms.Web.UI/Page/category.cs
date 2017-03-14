using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class category : Web.UI.BasePage
    {
        protected int category_id; //类别ID
        protected string call_index;  //调用别名

        protected Model.article_category model = new Model.article_category(); //分类的实体
        protected Model.channel channelModel = new Model.channel(); //分类的实体

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
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
            //判断SEO标题
            if (string.IsNullOrEmpty(model.seo_title) || "" == model.seo_title)
            {
                model.seo_title = model.title;
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
