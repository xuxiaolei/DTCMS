using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.tools
{
    /// <summary>
    /// 文件上传处理页
    /// </summary>
    public class upload_ajax : IHttpHandler, IRequiresSessionState
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //系统配置信息

        public void ProcessRequest(HttpContext context)
        {
            //检查管理员是否登录
            if (!new ManagePage().IsAdminLogin())
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"尚未登录或已超时，请登录后操作！\"}");
                return;
            }
            //取得处事类型
            string action = DTRequest.GetQueryString("action");
            switch (action)
            {
                case "config": //配置文件
                    ConfigHandler(context);
                    break;
                case "uploadimage": //图片上传
                    UploadHandler(context, "image");
                    break;
                case "uploadscrawl": //涂鸦
                    UploadHandler(context, "scrawl");
                    break;
                case "uploadvideo": //上传视频
                    UploadHandler(context, "video");
                    break;
                case "uploadfile": //上传附件
                    UploadHandler(context, "file");
                    break;
                case "listimage": //图片列表
                    ListFileManager(context, "imageManagerListPath", 0);
                    break;
                case "listfile": //文件列表
                    ListFileManager(context, "fileManagerListPath", 1);
                    break;
                default: //管理员上传
                    Upload(context);
                    break;
            }
        }

        #region 上传文件处理
        private void Upload(HttpContext context)
        {
            bool iswater = false; //默认不打水印
            bool isthumbnail = false; //默认不生成缩略图

            int upType = DTRequest.GetInt("type", 0);  //上传类型  0图片、1文件、2视频
            int width = DTRequest.GetInt("width", siteConfig.thumbnailwidth);
            int height = DTRequest.GetInt("height", siteConfig.thumbnailheight);

            HttpPostedFile upFile = context.Request.Files["Filedata"];
            if (DTRequest.GetQueryString("IsWater") == "1")
            {
                iswater = true;
            }
            if (DTRequest.GetQueryString("IsThumbnail") == "1")
            {
                isthumbnail = true;
            }
            if (null == upFile)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
                return;
            }
            //开始上传
            Model.upLoad model = new UpLoad().fileSaveAs(upFile, upType, isthumbnail, iswater, width, height);
            if (model.status > 0)
            {
                JsonHelper.WriteJson(context, new
                {
                    status = 1,
                    msg = model.msg,
                    name = model.name,
                    path = model.path,
                    thumb = model.thumb,
                    size = model.size,
                    ext = model.ext
                });
            }
            else
            {
                JsonHelper.WriteJson(context, new { status = 0, msg = model.msg });
            }
        }
        #endregion

        #region 配置文件
        private void ConfigHandler(HttpContext context)
        {
            string savePath = siteConfig.webpath + siteConfig.filepath + (siteConfig.filesave > 1 ? "/{yyyy}{mm}/{dd}/" : "/{yyyy}{mm}{dd}/");
            JsonHelper.WriteJson(context,
                new
                {
                    imageActionName = "uploadimage",  //上传图片配置项
                    imageFieldName = "upfile",
                    imageMaxSize = siteConfig.imgsize,
                    imageAllowFiles = siteConfig.imgextension.Split(','),
                    imageInsertAlign = "none",
                    imageUrlPrefix = "",
                    imagePathFormat = savePath,
                    scrawlActionName = "uploadscrawl",  //涂鸦图片上传配置项
                    scrawlFieldName = "upfile",
                    scrawlPathFormat = savePath,
                    scrawlMaxSize = siteConfig.imgsize,
                    scrawlUrlPrefix = "",
                    scrawlInsertAlign = "none",
                    snapscreenActionName = "uploadimage", //截图工具上传
                    snapscreenPathFormat = savePath,
                    snapscreenUrlPrefix = "",
                    snapscreenInsertAlign = "none",
                    catcherLocalDomain = new string[] { "127.0.0.1", "localhost", "img.baidu.com" },  //抓取远程图片配置
                    catcherActionName = "catchimage",
                    catcherFieldName = "source",
                    catcherPathFormat = savePath,
                    catcherUrlPrefix = "",
                    catcherMaxSize = siteConfig.imgsize,
                    catcherAllowFiles = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" },
                    videoActionName = "uploadvideo",  //上传视频配置
                    videoFieldName = "upfile",
                    videoPathFormat = savePath,
                    videoUrlPrefix = "",
                    videoMaxSize = siteConfig.videosize,
                    videoAllowFiles = siteConfig.videoextension.Split(','),
                    fileActionName = "uploadfile",  //上传文件配置
                    fileFieldName = "upfile",
                    filePathFormat = savePath,
                    fileUrlPrefix = "",
                    fileMaxSize = siteConfig.attachsize,
                    fileAllowFiles = siteConfig.fileextension.Split(','),
                    imageManagerActionName = "listimage",   //列出指定目录下的图片
                    imageManagerListPath = "/",
                    imageManagerListSize = 20,
                    imageManagerUrlPrefix = "",
                    imageManagerInsertAlign = "none",
                    imageManagerAllowFiles = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" },
                    fileManagerActionName = "listfile",  //列出指定目录下的文件
                    fileManagerListPath = "/",
                    fileManagerUrlPrefix = "",
                    fileManagerListSize = 20,
                    fileManagerAllowFiles = Utils.MergerArray(siteConfig.fileextension.Split(','), siteConfig.videoextension.Split(','))
                });
        }
        #endregion

        #region 文件上传
        private void UploadHandler(HttpContext context, string name)
        {
            string fieldName = "upfile";
            string[] allowExtensions = null;
            int sizeLimit = 0;
            switch (name)
            {
                case "image":
                    sizeLimit = siteConfig.imgsize;
                    allowExtensions = siteConfig.imgextension.Split(',');
                    break;
                case "scrawl":
                    sizeLimit = 2048000;
                    allowExtensions = new string[] { ".png" };
                    break;
                case "video":
                    sizeLimit = siteConfig.videosize;
                    allowExtensions = siteConfig.videoextension.Split(',');
                    break;
                case "file":
                    sizeLimit = siteConfig.attachsize;
                    allowExtensions = siteConfig.fileextension.Split(',');
                    break;
                default:
                    JsonHelper.WriteJson(context, new
                    {
                        status = "参数传输错误！"
                    });
                    return;
            }
            if (sizeLimit <= 0 || allowExtensions.Length == 0)
            {
                JsonHelper.WriteJson(context, new
                {
                    status = "参数传输错误！"
                });
                return;
            }
            if (name == "scrawl")
            {
                string uploadFileName = Utils.GetRamCode() + ".png";
                byte[] uploadFileBytes = Convert.FromBase64String(context.Request[fieldName]);

                string Url = new UpLoad().GetUpLoadPath() + uploadFileName;
                string localPath = Utils.GetMapPath(Url);
                string ErrorMessage = string.Empty;
                DTEnums.ResultState State = DTEnums.ResultState.Success;
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                    }
                    File.WriteAllBytes(localPath, uploadFileBytes);
                    State = DTEnums.ResultState.Success;
                }
                catch (Exception e)
                {
                    State = DTEnums.ResultState.FileAccessError;
                    ErrorMessage = e.Message;
                }
                JsonHelper.WriteJson(context, new
                {
                    state = Utils.GetStateString(State),
                    url = Url,
                    title = uploadFileName,
                    original = uploadFileName,
                    error = ErrorMessage
                });
            }
            else
            {
                context.Response.ContentType = "text/plain";
                HttpPostedFile _upfile = context.Request.Files[fieldName];
                Model.upLoad model = new UpLoad().fileSaveAs(_upfile, allowExtensions, sizeLimit, false, false, 0, 0);
                if (model.status > 0)
                {
                    JsonHelper.WriteJson(context, new
                    {
                        state = "SUCCESS",
                        url = model.path,
                        title = model.name,
                        original = model.name,
                        error = ""
                    });
                }
                else
                {
                    JsonHelper.WriteJson(context, new
                    {
                        status = model.msg
                    });
                }
            }
        }
        #endregion

        #region 文件列表
        /// <summary>
        /// 读取文件列表
        /// </summary>
        /// <param name="listPath"></param>
        /// <param name="allow">类型 0图片，1文件</param>
        private void ListFileManager(HttpContext context, string listPath, int allow)
        {
            int Start = DTRequest.GetQueryIntValue("start", 0);
            int Size = DTRequest.GetQueryIntValue("size", 20);
            int Total = 0;
            DTEnums.ResultState State = DTEnums.ResultState.Success;
            String PathToList = siteConfig.webpath + siteConfig.filepath;
            String[] FileList = null;
            String[] SearchExtensions;
            if (allow > 0)
            {
                //文件
                SearchExtensions = Utils.MergerArray(siteConfig.fileextension.Split(','), siteConfig.videoextension.Split(','));
            }
            else
            {
                //图片
                SearchExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };
            }
            var buildingList = new List<String>();
            try
            {
                var localPath = Utils.GetMapPath(PathToList);
                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                Total = buildingList.Count;
                FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                State = DTEnums.ResultState.AuthorizError;
            }
            catch (DirectoryNotFoundException)
            {
                State = DTEnums.ResultState.PathNotFound;
            }
            catch (IOException)
            {
                State = DTEnums.ResultState.IOError;
            }
            finally
            {
                JsonHelper.WriteJson(context, new
                {
                    state = Utils.GetStateString(State),
                    list = FileList == null ? null : FileList.Select(x => new { url = x }),
                    start = Start,
                    size = Size,
                    total = Total
                });
            }
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}