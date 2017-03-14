using System;
using System.Collections.Generic;
using System.Web;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Lable
{
    public class lable : BasePage
    {
        protected override void ShowPage()
        {

        }
        /// <summary>
        /// 根据ID查询内容
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回内容</returns>
        public string get_lable_content(int id)
        {
            string text = "";
            if (id > 0)
            {
                text = new BLL.lable().GetContent(id);
            }
            return text;
        }
        /// <summary>
        /// 根据调用别名查询内容
        /// </summary>
        /// <param name="call_index">call_index</param>
        /// <returns>返回内容</returns>
        public string call_lable_content(string call_index)
        {
            if (!string.IsNullOrEmpty(call_index))
            {
                return new BLL.lable().GetContent(call_index);
            }
            return string.Empty;
        }
    }
}