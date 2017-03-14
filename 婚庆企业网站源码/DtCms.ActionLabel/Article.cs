using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DtCms.Common;

namespace DtCms.ActionLabel
{
    public class Article
    {
        /// <summary>
        /// 文章调用类(文字)
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="txtNum">显示字数</param>
        /// <param name="isTime">显示时间</param>
        /// <param name="chrico">显示符号</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <returns></returns>
        public static string ViewTxtList(int top, int txtNum, int isTime, string chrico, string strWhere, string orderby)
        {
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
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
                    strTxt.Append("<a target=\"_blank\" title=\"" + dr["Title"].ToString() + "\" href=\"News_View.aspx?id=" + dr["Id"] + "\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "</a>");
                    strTxt.Append("</li>");
                }
            }
            else
            {
                strTxt.Append("<li>暂无数据显示...</li>");
            }
            return strTxt.ToString();
        }



        /// <summary>
        /// 文章调用类(文字)行业动态
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="txtNum">显示字数</param>
        /// <param name="isTime">显示时间</param>
        /// <param name="chrico">显示符号</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <returns></returns>
        public static string ViewTxtList_line(int top, int txtNum, int isTime, string chrico, string strWhere, string orderby)
        {
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("<tr>");


                    strTxt.Append("<td height=\"30\" align=\"left\" class=\"xuxian-heng\">▪ <a target=\"_blank\" title=\"" + dr["Title"].ToString() + "\" href=\"News_View.aspx?id=" + dr["Id"] + "\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "…</a></td>");
                    if (isTime == 1)
                    {
                        strTxt.Append("<td align=\"right\" class=\"xuxian-heng\" >[" + string.Format("{0:yyyy-MM-dd}", dr["AddTime"]) + "]</td>");
                    }
                    strTxt.Append(chrico);
                    strTxt.Append("</tr>");
                }
            }
            else
            {
                strTxt.Append("</td><tr>暂无数据显示...</tr>");
            }
            return strTxt.ToString();
        }

        /// <summary>
        /// 文章调用类(文字)公司动态
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="txtNum">显示字数</param>
        /// <param name="isTime">显示时间</param>
        /// <param name="chrico">显示符号</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderby">排序条件</param>
        /// <returns></returns>
        public static string ViewTxtList_lines(int top, int txtNum, int isTime, string chrico, string strWhere, string orderby)
        {
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            StringBuilder strTxt = new StringBuilder();
            DataSet ds = bll.GetList(top, strWhere, orderby);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    strTxt.Append("<tr>");
                    if (isTime == 1)
                    {
                        strTxt.Append("<span>[" + string.Format("{yyyy-0:MM-dd}", dr["AddTime"]) + "]</span>");
                    }
                    strTxt.Append(chrico);
                    strTxt.Append("<td width=\"85%\" height=\"28\" align=\"left\" class=\"xuxian-heng\">▪ <a target=\"_blank\" title=\"" + dr["Title"].ToString() + "\" href=\"News_View.aspx?id=" + dr["Id"] + "\">" + StringPlus.CutString(dr["Title"].ToString(), txtNum) + "</a></td>");
                    strTxt.Append("<td width=\"15%\" align=\"center\" class=\"xuxian-heng\">[" + string.Format("{0:d}", dr["AddTime"]) + "]</td>");
                    strTxt.Append("</tr>");
                }
            }
            else
            {
                strTxt.Append("<tr>暂无数据显示...</tr>");
            }
            return strTxt.ToString();
        }
    }
}