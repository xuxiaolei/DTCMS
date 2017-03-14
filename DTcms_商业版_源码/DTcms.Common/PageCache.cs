using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DTcms.Common
{
    public class PageCache
    {
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string filePath)
        {
            return HttpContext.Current.Server.MapPath(filePath);
        }
        /// <summary>
        /// 判断缓存文件是否过期
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static bool CacheFileTime(string filePath, int minute)
        {
            DateTime dt = File.GetLastWriteTime(Utils.GetMapPath(filePath));
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalMinutes <= minute)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 缓存路径格式化
        /// </summary>
        /// <param name="cachePath"></param>
        /// <returns></returns>
        public static string CachePathFormart(string cachePath)
        {
            StringBuilder sb = new StringBuilder(cachePath);
            sb.Replace("{", "\" + ");
            sb.Replace("}", " + \"");
            return sb.ToString();
        }
        /// <summary>
        /// 读取缓存文件文件
        /// </summary>
        /// <param name="temp">文件路径</param>
        /// <returns></returns>
        public static string ReadHtmlFile(string filePath)
        {
            //2016-09-24 所在页面在存储1分钟,减少磁盘访问
            string content = CacheHelper.Get<string>(filePath);
            if (null == content)
            {
                using (FileStream fs = new FileStream(GetMapPath(filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        content = sr.ReadToEnd(); // 读取文件
                        sr.Close();
                    }
                    fs.Close();
                }
                CacheHelper.Insert(filePath, content, 1);
            }
            return content;
        }
        /// <summary>
        /// 写入缓存文件文件
        /// </summary>
        /// <param name="pageCache">是否开启缓存</param>
        /// <param name="fomatCode">是否格式化代码</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="_content">内容</param>
        /// <param name="format">格式化内容</param>
        public static void WriteHtmlFile(int pageCache, int fomatCode, string filePath, string _content, int minute)
        {
            string content = _content;
            //是否格式化代码
            if (fomatCode == 1)
            {
                content = Regex.Replace(content, " {2,}", " ");
                content = Regex.Replace(content, @"\t+", "");
                content = Regex.Replace(content, @"[\r\n\s?\r\n]{2,}", "\r\n");
            }
            try
            {
                //判断目录是否存在
                Utils.DirectoryExists(filePath.Substring(0, filePath.LastIndexOf("/") + 1));
                //写入缓存
                using (var fs = File.Open(Utils.GetMapPath(filePath), FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    fs.SetLength(0); // 清空原有文件的内容
                    var streamWriter = new StreamWriter(fs, Encoding.UTF8);
                    streamWriter.Write(content);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (IOException ex)
            {
                LogHelper.WriteLog("缓存文件写操作", ex);
            }
        }
    }
}
