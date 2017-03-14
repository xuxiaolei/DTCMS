using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Net;
using System.Configuration;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public class UpLoad
    {
        private Model.siteconfig siteConfig;
        public UpLoad()
        {
            siteConfig = new BLL.siteconfig().loadConfig();
        }

        /// <summary>
        /// 裁剪图片并保存
        /// </summary>
        public bool cropSaveAs(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
        {
            string fileExt = Utils.GetFileExt(fileName); //文件扩展名，不含“.”
            if (!IsImage(fileExt))
            {
                return false;
            }
            string newFileDir = Utils.GetMapPath(newFileName.Substring(0, newFileName.LastIndexOf(@"/") + 1));
            //检查是否有该路径，没有则创建
            if (!Directory.Exists(newFileDir))
            {
                Directory.CreateDirectory(newFileDir);
            }
            try
            {
                string fileFullPath = Utils.GetMapPath(fileName);
                string toFileFullPath = Utils.GetMapPath(newFileName);
                return Thumbnail.MakeThumbnailImage(fileFullPath, toFileFullPath, 180, 180, cropWidth, cropHeight, X, Y);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 文件上传方法
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="uptype">上传类型</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <returns></returns>
        public Model.upLoad fileSaveAs(HttpPostedFile postedFile, int uptype, bool isThumbnail, bool isWater)
        {
            return fileSaveAs(postedFile, uptype, isThumbnail, isWater, siteConfig.thumbnailwidth, siteConfig.thumbnailheight);
        }
        public Model.upLoad fileSaveAs(HttpPostedFile postedFile, int uptype, bool isThumbnail, bool isWater, int width, int height)
        {
            int maxSize;
            string[] allowExtensions;
            if (uptype > 1)
            {
                maxSize = siteConfig.videosize;
                allowExtensions = siteConfig.videoextension.Split(',');
            }
            else if (uptype > 0)
            {
                maxSize = siteConfig.attachsize;
                allowExtensions = siteConfig.fileextension.Split(',');
            }
            else
            {
                maxSize = siteConfig.imgsize;
                allowExtensions = siteConfig.imgextension.Split(',');
            }
            return fileSaveAs(postedFile, allowExtensions, maxSize, isThumbnail, isWater, width, height);
        }

        /// <summary>
        /// 文件上传方法
        /// </summary>
        /// <param name="postedFile">文件流</param>
        /// <param name="allowExtensions">文件类型</param>
        /// <param name="maxSize">允许文件最大尺寸</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <returns></returns>
        public Model.upLoad fileSaveAs(HttpPostedFile postedFile, string[] allowExtensions, int maxSize, bool isThumbnail, bool isWater, int width, int height)
        {
            Model.upLoad model = new Model.upLoad();
            try
            {
                model.ext = Utils.GetFileExt(postedFile.FileName); //文件扩展名，包含“.”
                model.size = postedFile.ContentLength; //获得文件大小，以字节为单位
                model.name = Path.GetFileName(postedFile.FileName);
                string newFileName = Utils.GetRamCode() + model.ext; //随机生成新的文件名
                string newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名
                string upLoadPath = GetUpLoadPath(); //上传目录相对路径
                string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径

                model.path = upLoadPath + newFileName; //上传后的路径
                model.thumb = upLoadPath + newThumbnailFileName; //上传后的缩略图路径

                //检查文件扩展名是否合法
                if (CheckFileExt(model.ext, allowExtensions))
                {
                    //检查文件大小是否合法
                    if (CheckFileSize(model.size, maxSize))
                    {
                        //检查上传的物理路径是否存在，不存在则创建
                        if (!Directory.Exists(fullUpLoadPath))
                        {
                            Directory.CreateDirectory(fullUpLoadPath);
                        }
                        //保存文件
                        postedFile.SaveAs(fullUpLoadPath + newFileName);
                        //如果是图片，检查图片是否超出最大尺寸，是则裁剪
                        if (IsImage(model.ext) && (this.siteConfig.imgmaxheight > 0 || this.siteConfig.imgmaxwidth > 0))
                        {
                            Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newFileName, this.siteConfig.imgmaxwidth, this.siteConfig.imgmaxheight);
                        }
                        //如果是图片，检查是否需要生成缩略图，是则生成
                        if (isThumbnail && IsImage(model.ext) && this.siteConfig.thumbnailwidth > 0 && this.siteConfig.thumbnailheight > 0)
                        {
                            Thumbnail.MakeThumbnailImage(fullUpLoadPath + newFileName, fullUpLoadPath + newThumbnailFileName, width, height, this.siteConfig.thumbnailmode);
                        }
                        else
                        {
                            model.thumb = "";
                        }
                        //如果是图片，检查是否需要打水印
                        if (isWater && IsWaterMark(model.ext))
                        {
                            switch (this.siteConfig.watermarktype)
                            {
                                case 1:
                                    WaterMark.AddImageSignText(model.path, model.path,
                                        this.siteConfig.watermarktext, this.siteConfig.watermarkposition,
                                        this.siteConfig.watermarkimgquality, this.siteConfig.watermarkfont, this.siteConfig.watermarkfontsize);
                                    break;
                                case 2:
                                    WaterMark.AddImageSignPic(model.path, model.path,
                                        this.siteConfig.watermarkpic, this.siteConfig.watermarkposition,
                                        this.siteConfig.watermarkimgquality, this.siteConfig.watermarktransparency);
                                    break;
                            }
                        }
                        model.status = 1;
                        model.msg = "上传文件成功！";
                    }
                    else
                    {
                        model.status = 0;
                        model.msg = "文件超过限制的大小！";
                    }
                }
                else
                {
                    model.status = 0;
                    model.msg = "不允许上传" + model.ext + "类型的文件！";
                }
            }
            catch (Exception ex)
            {
                model.status = 0;
                model.msg = ex.Message;
                LogHelper.WriteLog(ex.ToString());
            }
            return model;
        }

        #region 保存远程文件到本地
        /// <summary>
        /// 保存远程文件到本地
        /// </summary>
        /// <param name="fileUri">URI地址</param>
        /// <returns>上传后的路径</returns>
        public string remoteSaveAs(string fileUri)
        {
            WebClient client = new WebClient();
            string fileExt = string.Empty; //文件扩展名，不含“.”
            if (fileUri.LastIndexOf(".") == -1)
            {
                fileExt = ".gif";
            }
            else
            {
                fileExt = Utils.GetFileExt(fileUri);
            }
            string newFileName = Utils.GetRamCode() + fileExt; //随机生成新的文件名
            string upLoadPath = GetUpLoadPath(); //上传目录相对路径
            string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //上传目录的物理路径
            string newFilePath = upLoadPath + newFileName; //上传后的路径
            //检查上传的物理路径是否存在，不存在则创建
            if (!Directory.Exists(fullUpLoadPath))
            {
                Directory.CreateDirectory(fullUpLoadPath);
            }

            try
            {
                client.DownloadFile(fileUri, fullUpLoadPath + newFileName);
                //如果是图片，检查是否需要打水印
                if (IsWaterMark(fileExt))
                {
                    switch (this.siteConfig.watermarktype)
                    {
                        case 1:
                            WaterMark.AddImageSignText(newFilePath, newFilePath,
                                this.siteConfig.watermarktext, this.siteConfig.watermarkposition,
                                this.siteConfig.watermarkimgquality, this.siteConfig.watermarkfont, this.siteConfig.watermarkfontsize);
                            break;
                        case 2:
                            WaterMark.AddImageSignPic(newFilePath, newFilePath,
                                this.siteConfig.watermarkpic, this.siteConfig.watermarkposition,
                                this.siteConfig.watermarkimgquality, this.siteConfig.watermarktransparency);
                            break;
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            client.Dispose();
            return newFilePath;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        public string GetUpLoadPath()
        {
            string pathFormat = siteConfig.webpath + siteConfig.filepath + (siteConfig.filesave > 1 ? "/{yyyy}{mm}/{dd}/" : "/{yyyy}{mm}{dd}/");
            pathFormat = pathFormat.Replace("{yyyy}", DateTime.Now.Year.ToString());
            pathFormat = pathFormat.Replace("{mm}", DateTime.Now.Month.ToString("D2"));
            pathFormat = pathFormat.Replace("{dd}", DateTime.Now.Day.ToString("D2"));
            return pathFormat;
        }

        /// <summary>
        /// 是否需要打水印
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsWaterMark(string _fileExt)
        {
            //判断是否开启水印
            if (this.siteConfig.watermarktype > 0)
            {
                //判断是否可以打水印的图片类型
                ArrayList al = new ArrayList();
                al.Add(".bmp");
                al.Add(".jpeg");
                al.Add(".jpg");
                al.Add(".png");
                if (al.Contains(_fileExt.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsImage(string _fileExt)
        {
            ArrayList al = new ArrayList();
            al.Add(".bmp");
            al.Add(".jpeg");
            al.Add(".jpg");
            al.Add(".gif");
            al.Add(".png");
            if (al.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        /// <param name="fileExtension">扩展名</param>
        /// <param name="allowExtensions">允许上传的扩展名</param>
        /// <returns></returns>
        private bool CheckFileExt(string fileExtension, string[] allowExtensions)
        {
            fileExtension = fileExtension.ToLower();
            //检查危险文件
            string[] excExt = { ".vbs", ".asp", ".aspx", ".ashx", ".asa", ".asmx", ".asax", ".php", ".jsp", ".htm", ".html" };
            if (excExt.Select(x => x.ToLower()).Contains(fileExtension))
            {
                return false;
            }
            //检查合法文件
            string[] allowExt = (this.siteConfig.fileextension + "," + this.siteConfig.videoextension + "," + this.siteConfig.imgextension).Split(',');
            if (allowExtensions.Select(x => x.ToLower()).Contains(fileExtension))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查文件大小
        /// </summary>
        /// <param name="size">文件大小(B)</param>
        /// <param name="sizeLimit">允许上传大小</param>
        /// <returns></returns>
        private bool CheckFileSize(int size, int sizeLimit)
        {
            return size < sizeLimit;
        }
        #endregion
    }
}
