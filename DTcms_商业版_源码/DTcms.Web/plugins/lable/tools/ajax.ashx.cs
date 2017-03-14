using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;

namespace DTcms.Web.Plugin.Lable.tools
{
    /// <summary>
    /// 管理后台AJAX处理页
    /// </summary>
    public class ajax : IHttpHandler, IRequiresSessionState
    {
        DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig();

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {
                case "validate": //验证名称
                    validate(context);
                    break;
            }
        }
        //验证名称
        private void validate(HttpContext context)
        {
            string lable_name = DTRequest.GetString("param");
            string old_lable_name = DTRequest.GetString("old_name");
            if (string.IsNullOrEmpty(lable_name))
            {
                JsonHelper.WriteJson(context, new
                {
                    status = "n",
                    msg = "名称不可为空！"
                });
                return;
            }
            if (lable_name.ToLower() == old_lable_name.ToLower())
            {
                JsonHelper.WriteJson(context, new
                {
                    status = "y",
                    msg = "该名称可使用！"
                });
                return;
            }
            BLL.lable bll = new BLL.lable();
            if (bll.Exists(lable_name))
            {
                JsonHelper.WriteJson(context, new
                {
                    status = "y",
                    msg = "该名称已被占用，请更换！"
                });
                return;
            }
            JsonHelper.WriteJson(context, new
            {
                status = "y",
                msg = "该名称可使用！"
            });
            return;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}