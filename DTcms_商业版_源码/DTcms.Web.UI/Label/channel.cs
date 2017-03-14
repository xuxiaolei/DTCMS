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
        /// 根据ID取得频道模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>String</returns>
        protected Model.channel get_channel_model(int channel_id)
        {
            Model.channel model = new Model.channel();
            if (channel_id > 0)
            {
                model = new BLL.channel().GetViewModel(channel_id);
                //判断SEO标题
                if (string.IsNullOrEmpty(model.seo_title) || "" == model.seo_title)
                {
                    model.seo_title = model.title;
                }
            }
            return model;
        }
        /// <summary>
        /// 根据别名取得频道模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>String</returns>
        protected Model.channel get_channel_model(string call_index)
        {
            Model.channel model = new Model.channel();
            if (!string.IsNullOrEmpty(call_index))
            {
                model = new BLL.channel().GetViewModel(call_index);
                //判断SEO标题
                if (string.IsNullOrEmpty(model.seo_title) || "" == model.seo_title)
                {
                    model.seo_title = model.title;
                }
            }
            return model;
        }
    }
}
