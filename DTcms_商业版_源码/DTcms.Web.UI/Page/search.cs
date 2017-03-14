using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class search : Web.UI.BasePage
    {
        protected string keyword = string.Empty; //关健字
        protected string pagelist = string.Empty; //分页列表
        protected int page;            //当前页码
        protected int totalcount;      //OUT数据总数
        protected int channel = 0;  //频道ID
        
        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            page = DTRequest.GetQueryInt("page", 1);
            keyword = Utils.SafeXXS(DTRequest.GetQueryString("keyword"));
            channel = DTRequest.GetQueryInt("channel", 0);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        protected DataTable get_search_list(int _pagesize, out int _totalcount, int _site_id)
        {
            //创建一个DataTable
            string link_url = null;
            DataTable dt = null;
            DataTable oldData = new BLL.article().GetSearch(this.channel, _pagesize, page, "(title like '%" + keyword + "%' or zhaiyao like '%" + keyword + "%')", "add_time desc,id desc", out _totalcount).Tables[0];
            //复制结构
            dt = oldData.Clone();
            dt.Columns.Add("link_url", Type.GetType("System.String"));
            if (oldData.Rows.Count > 0)
            {
                foreach (DataRow dr in oldData.Rows)
                {
                    link_url = get_url_rewrite(Utils.StrToInt(dr["channel_id"].ToString(), 0), dr["call_index"].ToString(), Utils.StrToInt(dr["id"].ToString(), 0));
                    if (!string.IsNullOrEmpty(link_url))
                    {
                        DataRow row = dt.NewRow();
                        row["id"] = dr["id"];
                        row["site_id"] = dr["site_id"];
                        row["channel_id"] = dr["channel_id"];
                        row["category_id"] = dr["category_id"];
                        row["title"] = dr["title"];
                        row["call_index"] = dr["call_index"];
                        row["remark"] = dr["remark"];
                        row["img_url"] = dr["img_url"];
                        row["link_url"] = link_url;
                        row["add_time"] = dr["add_time"];
                        row["update_time"] = dr["update_time"];
                        row["tags"] = dr["tags"];
                        dt.Rows.Add(row);
                    }
                }
            }
            return dt;
        }

        //查找匹配的URL
        private string get_url_rewrite(int channel_id, string call_index, int id)
        {
            if (channel_id == 0)
            {
                return string.Empty;
            }
            string querystring = id.ToString();
            string channel_name = new BLL.channel().GetChannelName(channel_id);
            if (string.IsNullOrEmpty(channel_name))
            {
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(call_index))
            {
                querystring = call_index;
            }
            BLL.url_rewrite bll = new BLL.url_rewrite();
            Model.url_rewrite model = bll.GetInfo(channel_name, "detail");
            if (model != null)
            {
                return linkurl(model.name, querystring);
            }
            return string.Empty;
        }

    }
}
