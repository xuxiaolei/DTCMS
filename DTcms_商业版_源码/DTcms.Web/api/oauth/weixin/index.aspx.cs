using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth.weixin
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获得配置信息
            oauth_config config = oauth_helper.get_config("weixin");
            if (config == null)
            {
                Response.Write("出错了，您尚未配置微信的API信息！");
                return;
            }
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["oauth_state"] = state;
            string send_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + config.oauth_app_id + "&redirect_uri=" + Utils.UrlEncode(config.return_uri) + "&response_type=code&scope=snsapi_userinfo&state=" + state + "#wechat_redirect";
            //开始发送
            Response.Redirect(send_url);
        }
    }
}