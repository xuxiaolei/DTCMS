using System;
using System.Xml;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.Plugin.Service.tools
{
    public class ajax : IHttpHandler
    {
        DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig();
        private int id = 0;

        public void ProcessRequest(HttpContext context)
        {
            Model.install config = new BLL.install().loadConfig("../config/install.config");
            if(config.status==0){
                context.Response.Write("{ \"info\":\"在线客服功能未开启！\", \"status\":0 }");
                return;
            }
            this.id = DTRequest.GetQueryInt("id");
            string strWhere = "is_lock=0";
            if (this.id > 0)
            {
                strWhere += " and id=" + this.id;
            }

            DataSet ds = new BLL.online_service_group().GetList(0, "is_lock=0", "sort_id asc,id desc");

            bool status = false;
            StringBuilder outHtml = new StringBuilder();
            outHtml.Append("{");
            outHtml.Append("\"status\":" + config.status + ",");
            outHtml.Append("\"content\":\"" + config.content + "\",");
            outHtml.Append("\"groups\":[");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (status)
                {
                    outHtml.Append(",");
                }
                else
                {
                    status = true;
                }
                bool status2 = false;
                outHtml.Append("{");
                outHtml.Append("\"title\":\"" + dr["title"].ToString() + "\",");
                outHtml.Append("\"mode\":\"" + dr["default_view"].ToString() + "\",");
                outHtml.Append("\"list\":[");

                DataSet ds1 = new BLL.online_service().GetList(0, "is_lock=0 and group_id=" + dr["id"].ToString(), "sort_id asc,add_time desc");
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    if (status2)
                    {
                        outHtml.Append(",");
                    }
                    else
                    {
                        status2 = true;
                    }
                    outHtml.Append("{");
                    outHtml.Append("\"name\":\"" + dr1["title"].ToString() + "\",");
                    outHtml.Append("\"url\":\"" + dr1["link_url"].ToString() + "\",");
                    outHtml.Append("\"img\":\"" + dr1["img_url"].ToString() + "\"");
                    outHtml.Append("}");
                }
                outHtml.Append("]");
                outHtml.Append("}");
            }
            outHtml.Append("]");
            outHtml.Append("}");
            context.Response.Write(outHtml.ToString());
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
