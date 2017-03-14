using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class error : System.Web.UI.Page
    {
        protected internal Model.siteconfig config = new BLL.siteconfig().loadConfig();
        protected string msg = string.Empty;

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        public error()
        {
            msg = Utils.ToHtml(DTRequest.GetQueryString("msg"));
            if (string.IsNullOrWhiteSpace(msg))
            {
                msg = "出错了，您要浏览的页面不存在或已删除！";
            }
        }

        //页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            Response.StatusCode = 404;
            Response.Status = "404 Moved Permanently";
        }
    }
}
