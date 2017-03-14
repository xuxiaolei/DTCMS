using System;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.Plugin.Advert
{
    public class ajax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int id = DTRequest.GetQueryInt("id");
            //获得广告位的ID
            if (id < 1)
            {
                context.Response.Write("{\"status\": 0, \"msg\":\"参数错误！\"}");
                return;
            }
            //检查广告位是否存在
            BLL.advert abll = new BLL.advert();
            if (!abll.Exists(id))
            {
                context.Response.Write("{\"status\": 0, \"msg\":\"该广告位不存在！\"}");
                return;
            }
            //取得该广告位详细信息
            Model.advert model = abll.GetModel(id);
            //输出该广告位下的广告条,不显示未开始、过期、暂停广告
            BLL.advert_banner bbll = new BLL.advert_banner();
            DataSet ds = bbll.GetList(0, "is_lock=0 and datediff(d,start_time,getdate())>=0 and datediff(d,end_time,getdate())<=0 and aid=" + id, "sort_id asc,id desc");
            if (ds.Tables[0].Rows.Count < 1)
            {
                context.Response.Write("{\"status\": 0, \"msg\":\"该广告位下暂无广告内容或者已过期！\"}");
                return;
            }
            //新增，取得站点配置信息
            DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig();
            //输出Json
            JsonHelper.WriteJson(context, new
            {
                title = model.title,
                type = model.type.ToString(),
                number = model.view_num.ToString(),
                width = model.view_width.ToString(),
                height = model.view_height.ToString(),
                list = ds.Tables[0]
            });
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
