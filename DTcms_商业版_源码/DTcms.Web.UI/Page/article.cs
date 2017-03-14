using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class article : Web.UI.BasePage
    {
        protected int page;         //当前页码
        protected int totalcount;   //OUT数据总数
        protected string pagelist;  //分页页码
        protected Model.channel model = new Model.channel();

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            page = DTRequest.GetQueryInt("page", 1);
        }

        /// <summary>
        /// 设置当前页频道
        /// </summary>
        /// <param name="channel_id"></param>
        protected void set_channel_model(string channel_name)
        {
            model = get_channel_model(channel_name);
            //判断SEO标题
            if (string.IsNullOrEmpty(model.seo_title) || "" == model.seo_title)
            {
                model.seo_title = model.title;
            }
        }
    }
}
