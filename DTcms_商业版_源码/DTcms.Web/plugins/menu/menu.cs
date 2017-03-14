using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Menu
{
    public class menu : BasePage
    {
        protected override void ShowPage()
        {

        }

        /// <summary>
        /// 返回菜单列表(迭代)
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="parent_id">父类ID</param>
        /// <returns></returns>
        public DataTable call_menu_content(int top, int parent_id)
        {
            BLL.menu bll = new BLL.menu();
            string strWhere = "is_lock=0";
            if (parent_id > 0)
            {
                strWhere += " and class_list like '%," + parent_id + ",%'";
            }
            return bll.GetList(top, strWhere, "sort_id asc,id asc", parent_id);
        }
        /// <summary>
        /// 返回菜单列表
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="parent_id">父类ID</param>
        /// <returns></returns>
        public DataTable get_menu_content(int top, int parent_id)
        {
            BLL.menu bll = new BLL.menu();
            string strWhere = "is_lock=0";
            if (parent_id > 0)
            {
                strWhere += " and parent_id = " + parent_id;
            }
            return bll.GetList(top, strWhere, "sort_id asc,id asc", parent_id);
        }
    }
}