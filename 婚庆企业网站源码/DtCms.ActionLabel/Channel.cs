using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DtCms.Common;

namespace DtCms.ActionLabel
{
    public class Channel
    {
        public static string ProTxtList(int pId, int kId)
        {
            DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
            StringBuilder strTxt = new StringBuilder();
            DataTable dt = bll.GetList(pId, kId);
            if (dt.Rows.Count > 0)
            {
                strTxt.Append("<li>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (Convert.ToInt32(dr["ClassLayer"]) == 1)
                    {
                        //if (i > 0)
                        //{
                         //   strTxt.Append("</dl>\n<dl>\n");
                       // }
                      //  strTxt.Append("<dt>" + dr["Title"].ToString() + "</dt>\n");
                    }
                    else
                    {
                        strTxt.Append("<a title=\"" + dr["Title"].ToString() + "\" href=\"Product.aspx?classId=" + dr["Id"].ToString() + "\" title=\"" + dr["Title"].ToString() + "\">" + StringPlus.CutString(dr["Title"].ToString(), 22) + "</a>");
                    }
                }
                strTxt.Append("</li>");
            }

            return strTxt.ToString();
        }

        public static string ViewTxtList(int pId, int kId, string txtUrl)
        {
            DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
            StringBuilder strTxt = new StringBuilder();
            DataTable dt = bll.GetList(pId, kId);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    strTxt.Append("<li><a href=\"" + txtUrl + "?classId=" + dr["Id"].ToString() + "\" title=\"" + dr["Title"].ToString() + "\">" + StringPlus.CutString(dr["Title"].ToString(), 20) + "</a></li>\n");
                }
            }

            return strTxt.ToString();
        }

        //输出栏目名称
        public static string ViewChannelTitle(int classId)
        {
            DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
            if (bll.Exists(classId))
            {
                DtCms.Model.Channel model = bll.GetModel(classId);
                return model.Title;
            }
            return "所有列表";
        }
    }
}
