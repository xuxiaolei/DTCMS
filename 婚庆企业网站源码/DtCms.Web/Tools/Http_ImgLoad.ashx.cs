using System;
using System.Web;
using DtCms.Common;
using System.IO;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// 显示缩略图
    /// </summary>
    public class Http_ImgLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int strW;
            int strH;
            if (int.TryParse(context.Request.Params["w"], out strW) && int.TryParse(context.Request.Params["h"], out strH))
            {
                context.Response.Clear();
                string gurl = context.Request.Params["gurl"];
                if (!string.IsNullOrEmpty(gurl))
                {
                    if (File.Exists(context.Server.MapPath(gurl)))
                    {
                        LoadImage.GenThumbnail(context.Request.Params["gurl"], strW, strH);
                    }
                    else
                    {
                        LoadImage.GenThumbnail("/images/nopic.gif", strW, strH);
                    }
                }
                else
                {
                    LoadImage.GenThumbnail("/images/nopic.gif", strW, strH);
                }
            }
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
