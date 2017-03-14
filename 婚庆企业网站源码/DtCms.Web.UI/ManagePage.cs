using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;

namespace DtCms.Web.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        protected internal DtCms.Model.WebSet webset;
        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            webset = new DtCms.BLL.WebSet().loadConfig(Server.MapPath(ConfigurationManager.AppSettings["Configpath"].ToString()));
        }

        void ManagePage_Load(object sender, EventArgs e)
        {
            if (Session["AdminNo"] == null || Session["AdminName"] == null || Session["AdminLevel"] == null || Session["AdminType"] == null)
            {
                Response.Write("<script>parent.location.href='/baixinkeji/login.aspx'</script>");
                Response.End();
            }
        }


        protected void chkLoginLevel(string pagestr)
        {
            string msbox = "";
            int utype = int.Parse(Session["AdminType"].ToString());
            string ulevel = Session["AdminLevel"].ToString();
            if (utype > 1)
            {
                pagestr += ",";
                if (ulevel.IndexOf(pagestr) == -1)
                {
                    msbox += "<script type=\"text/javascript\">\n";
                    msbox += "parent.jsmsg(350,230,\"警告提示\",\"<b>没有管理权限</b>您没有权限管理该功能，请勿非法进入。\",\"back\")\n";
                    msbox += "</script>\n";
                    //ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsMsg", msbox);
                    Response.Write(msbox);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 遮罩提示窗口
        /// </summary>
        /// <param name="w">宽度</param>
        /// <param name="h">高度</param>
        /// <param name="msgtitle">窗口标题</param>
        /// <param name="msgbox">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptMsg(int w, int h, string msgtitle, string msgbox, string url, string msgcss)
        {
            string msbox = "";
            msbox += "<script type=\"text/javascript\">\n";
            msbox += "parent.jsmsg(" + w + "," + h + ",\"" + msgtitle + "\",\"" + msgbox + "\",\"" + url + "\",\"" + msgcss + "\")\n";
            msbox += "</script>\n";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsMsg", msbox);
        }

        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptPrint(string msgtitle, string url, string msgcss)
        {
            string msbox = "";
            msbox += "<script type=\"text/javascript\">\n";
            msbox += "parent.jsprint(\"" + msgtitle + "\",\"" + url + "\",\"" + msgcss + "\")\n";
            msbox += "</script>\n";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox);
        }
    }
}
