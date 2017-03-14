using System;
using System.Text;
using System.Data;
using DtCms.Common;

namespace DtCms.ActionLabel
{
    public class Links
    {
        /// <summary>
        /// 输出友情链接
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <returns></returns>
        public static string ViewImgList(int top, string strWhere, string orderby)
        {
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    if (Convert.ToInt32(dr["IsImage"]) == 1)
                    {
                        strTxt.Append("<li>");
                        strTxt.Append("<a target=\"_blank\" href=\"" + dr["WebUrl"] + "\">");
                        strTxt.Append("<img border=\"0\" src=\"" + dr["ImgUrl"] + "\" alt=\"" + dr["Title"].ToString() + "\" />");
                        strTxt.Append("</a>");
                        strTxt.Append("</li>\n");
                    }
                    else
                    {
                        strTxt.Append("<a target=\"_blank\" href=\"" + dr["WebUrl"] + "\">");
                        strTxt.Append(dr["Title"].ToString());
                        strTxt.Append("</a>" + "&nbsp;&nbsp;");
                    }
                }
            }
            return strTxt.ToString();
        }
    }
}
