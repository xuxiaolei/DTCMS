using System;
using System.Text;
using System.Data;
using DtCms.Common;

namespace DtCms.ActionLabel
{
    public class Products
    {
        /// <summary>
        /// 产品图片列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="txtNum">显示字符,注意一个汉字等于二个字节</param>
        /// <param name="imgW">预览图宽度</param>
        /// <param name="imgH">预览图高度</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <returns></returns>
        public static string ViewImgList(int top, int txtNum, int imgW, int imgH, string strWhere, string orderby)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("<li>");
                    strTxt.Append("<a target=\"_blank\" href=\"Product_View.aspx?id=" + dr["ID"] + "\">");
                    strTxt.Append("<img src=\"/Tools/Http_ImgLoad.ashx?w=" + imgW + "&h=" + imgH + "&gurl=" + dr["ImgUrl"] + "\" alt=\"" + dr["Title"].ToString() + "\" />");
                    strTxt.Append("</a>");
                    strTxt.Append("<span><a target=\"_blank\" title=\"" + dr["Title"].ToString() + "\" href=\"Product_View.aspx?id=" + dr["Id"] + "\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "</a></span>");
                    strTxt.Append("</li>\n");
                }
            }
            else
            {
                strTxt.Append("<div>暂无数据显示...</div>");
            }
            return strTxt.ToString();
        }

        /// <summary>
        /// 首页文字产品
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="txtNum">标题字数</param>
        /// <param name="isTime">是否显示时间</param>
        /// <param name="chrico">标题前符号</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <returns></returns>
        public static string ViewTxtList(int top, int txtNum, int isTime, string chrico, string strWhere, string orderby)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("<li>");
                    if (isTime == 1)
                    {
                        strTxt.Append("<span>[" + string.Format("{0:MM-dd}", dr["AddTime"]) + "]</span>");
                    }
                    strTxt.Append(chrico);
                    if (i < 3)
                    {
                        strTxt.Append("<em class=\"hot\">" + (i + 1) + "</em>");
                    }
                    else
                    {
                        strTxt.Append("<em>" + (i + 1) + "</em>");
                    }
                    strTxt.Append("<a target=\"_blank\" title=\"" + dr["Title"].ToString() + "\" href=\"Product_View.aspx?id=" + dr["Id"] + "\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "</a>");
                    strTxt.Append("</li>\n");
                }
            }
            else
            {
                strTxt.Append("<div>暂无数据显示...</div>");
            }
            return strTxt.ToString();
        }

        //发布
        public static string ViewTxtList2(int top, int txtNum, int isTime, string chrico, string strWhere, string orderby)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("<li>");
                    if (isTime == 1)
                    {
                        strTxt.Append("<span>[" + string.Format("{0:MM-dd}", dr["AddTime"]) + "]</span>");
                    }
                    strTxt.Append(chrico);
                    strTxt.Append("<a target=\"_blank\" title=\"" + dr["Title"].ToString() + "\" href=\"Product_View.aspx?id=" + dr["Id"] + "\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "</a>");
                    strTxt.Append("</li>");
                }
            }
            else
            {
                strTxt.Append("<div>暂无数据显示...</div>");
            }
            return strTxt.ToString();
        }

        //产品工程案例
        public static string ViewTxtList_line(int top, int txtNum, int isTime, string chrico, string strWhere, string orderby)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("<tr>");
                    if (isTime == 1)
                    {
                        strTxt.Append("<span>[" + string.Format("{0:MM-dd}", dr["AddTime"]) + "]</span>");
                    }
                    strTxt.Append(chrico);
                    strTxt.Append("<td height=\"25\" align=\"left\">▪ <a target=\"_blank\" title=\"" + dr["Title"].ToString() + "\" href=\"Product_View.aspx?id=" + dr["Id"] + "\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "</a></td>");
                    strTxt.Append("</tr>");
                }
            }
            else
            {
                strTxt.Append("<div>暂无数据显示...</div>");
            }
            return strTxt.ToString();
        }

        //产品首页图片
        public static string ViewTxtList_Image(int top, int txtNum, int isTime, string chrico, string strWhere, string orderby)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            //如果记录存在
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("<td style=\"padding-right:35px;\"><table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr>\n");
                    if (isTime == 1)
                    {
                        strTxt.Append("<span>[" + string.Format("{0:MM-dd}", dr["AddTime"]) + "]</span>");
                    }
                    strTxt.Append(chrico);
                    strTxt.Append("<td align=\"center\" class=\"product\"><A href=\"Product_View.aspx?id=" + dr["Id"] + "\" target=\"_blank\"><img src=\"/Tools/Http_ImgLoad.ashx?w=105&h=120&gurl=" + dr["ImgUrl"] + "\"  alt=" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "\" border=\"0\" /></A></td>");
                    strTxt.Append("</tr><tr>");
                    strTxt.Append("<td align=\"center\" height=\"30\" style=\"color:#666666;\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "</td>");
                    strTxt.Append("</tr></table></td>");
                }




            }
            else
            {
                strTxt.Append("<div>暂无数据显示...</div>");
            }
            return strTxt.ToString();
        }

    }
}

          
   