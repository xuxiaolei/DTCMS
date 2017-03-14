using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Images
{
    public class images : BasePage
    {
        protected override void ShowPage()
        {

        }

        /// <summary>
        /// 根据ID查询图片
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回 Model</returns>
        public Model.images get_images_content(int id)
        {
            if (id > 0)
            {
                return new BLL.images().GetModel(id);
            }
            return null;
        }

        /// <summary>
        /// 根据标记名查询内容列表
        /// </summary>
        /// <param name="call_index">call_index</param>
        /// <returns>返回内容</returns>
        public DataTable call_images_content(int Top, string call_index)
        {
            BLL.images bll = new BLL.images();
            return bll.GetList(Top, "is_lock=0 and sign='" + call_index + "'", "sort_id asc,id asc").Tables[0];
        }
    }
}