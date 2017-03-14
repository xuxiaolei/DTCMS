using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DtCms.Common;

namespace DtCms.Web.UI
{
    public class BasePage : System.Web.UI.Page
    {
        protected internal DtCms.Model.WebSet webset;
        public BasePage()
        {
            webset = new DtCms.BLL.WebSet().loadConfig(Server.MapPath(ConfigurationManager.AppSettings["Configpath"].ToString()));
        }

        /// <summary>
        /// 添加页面Meta信息
        /// </summary>
        /// <param name="Seokeywords">关键词</param>
        /// <param name="Seodescription">说明</param>
        /// <param name="Seohead">其它增加项</param>
        public string AddMetaInfo(string Seokeywords, string Seodescription, string Seohead)
        {
            StringBuilder strTxt = new StringBuilder();
            if (Seokeywords != "")
            {
                strTxt.Append("<meta name=\"keywords\" content=\"" + StringPlus.DropHTML(Seokeywords, 250).Replace("\"", " ") + "\" />\r\n");
            }
            if (Seodescription != "")
            {
                strTxt.Append("<meta name=\"description\" content=\"" + StringPlus.DropHTML(Seodescription, 250).Replace("\"", " ") + "\" />\r\n");
            }
            strTxt.Append("<link rel=\"icon\" href=\"favicon.ico\" mce_href=\"favicon.ico\" type=\"image/x-icon\"> \r\n");
            strTxt.Append("<link rel=\"shortcut icon\" href=\"favicon.ico\" mce_href=\"favicon.ico\" type=\"image/x-icon\"> \r\n");
            //strTxt.Append("<link rel=\"icon\" href=\"animated_favicon.gif\" type=\"image/gif\">\r\n");
            strTxt.Append(Seohead);
            return strTxt.ToString();
        }

        /// <summary>
        /// 可以自动关闭的提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">转向地址,back表示后退,空表示不转向</param>
        protected void jsAutoMsg(string msgtitle, string url)
        {
            StringBuilder strbox = new StringBuilder();
            strbox.Append("$(function(){ jsAutoMsg(\"" + msgtitle + "\", \"" + url + "\"); });");
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "AutoMsg", strbox.ToString(), true);
        }

        /// <summary>
        /// 遮罩提示窗口
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="msgtitle">提示标题</param>
        /// <param name="msgbox">提示内容</param>
        /// <param name="url">转向地址,back表示后退,空表示不转向</param>
        /// <param name="msgcss">提示样式,成功：Success,错误：Error,警告：Warning</param>
        protected void jsLayMsg(int w, int h, string msgtitle, string msgbox, string url, string msgcss)
        {
            StringBuilder strbox = new StringBuilder();
            strbox.Append("$(function(){ jsLayMsg(" + w + ", " + h + ", {title:\"" + msgtitle + "\",msbox:\"" + msgbox + "\",mscss:\"" + msgcss + "\",url:\"" + url + "\"}); });");
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "LayMsg", strbox.ToString(), true);
        }
    }
}
