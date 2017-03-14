using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;
using System.IO;
using DtCms.Web.UI;

namespace DtCms.Web.Tools
{
    /// <summary>
    /// AJAX多文件上传页
    /// </summary>
    public class MultipleUpload : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //检查是否登录后上传操作
            if (context.Session["AdminNo"] == null)
            {
                context.Response.Write("{msg: 0, msbox: \"请登录后再进行上传文件！\"}");
                return;
            }
            HttpPostedFile _upfile = context.Request.Files["FileUpload"];
            string _delfile = context.Request.Params["txtImgUrl"];
            if (_upfile == null)
            {
                context.Response.Write("{msg: 0, msbox: \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upFiles = new UpLoad();
            string msg = upFiles.fileSaveAs(_upfile, 1);
            //删除已存在的旧文件
            if (!string.IsNullOrEmpty(_delfile))
            {
                string _filename = HttpContext.Current.Server.MapPath(_delfile);
                if (File.Exists(_filename))
                {
                    File.Delete(_filename);
                }
            }
            //返回成功信息
            context.Response.Write(msg);
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
