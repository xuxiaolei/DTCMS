using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Advert
{
    public class advert : BasePage
    {
        protected override void ShowPage()
        {

        }

        /// <summary>
        /// 广告列表
        /// </summary>
        /// <param name="group_id">广告位ID</param>
        /// <returns></returns>
        public DataTable call_advert_list(int group_id)
        {
            BLL.advert bll = new BLL.advert();
            Model.advert model = bll.GetModel(group_id);
            if (model == null)
            {
                return null;
            }
            string strWhere = "is_lock=0 and datediff(d,start_time,getdate())>=0 and datediff(d,end_time,getdate())<=0 and aid=" + group_id;
            DataSet ds = new BLL.advert_banner().GetList(model.view_num, strWhere, "sort_id asc,id desc");
            if (ds.Tables[0].Rows.Count < 1)
            {
                return null;
            }
            return ds.Tables[0];
        }
    }
}
