using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {

        #region 列表标签======================================
        /// <summary>
        /// 随机文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_random_list(string channel_name, int category_id, int top, string strwhere)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetRandomList(channel_name, category_id, top, strwhere).Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int top, string strwhere)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetList(channel_name, top, strwhere, "sort_id asc,add_time desc").Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int top, string strwhere)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetList(channel_name, category_id, top, strwhere, "sort_id asc,add_time desc").Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int top, string strwhere, string orderby)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetList(channel_name, category_id, top, strwhere, orderby).Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 文章分页列表(自定义页面大小)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int page_size, int page_index, string strwhere, string orderby, out int totalcount)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetList(channel_name, category_id, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
            }
            else
            {
                totalcount = 0;
            }
            return dt;
        }

        /// <summary>
        /// 文章分页列表(自动页面大小)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_params">传输参数</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int page_size, int page_index, string strwhere, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetList(channel_name, category_id, page_size, page_index, strwhere, "sort_id asc,add_time desc", out totalcount).Tables[0];
                pagelist = Utils.OutPageList(page_size, page_index, totalcount, linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dt;
        }
        /// <summary>
        /// 文章分页列表(自动页面大小)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_call_index">调用名</param>
        /// <param name="_id">类别ID</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list_pag(string channel_name, int category_id, int page_size, int page_index, string strwhere, out int totalcount, out string pagelist, string _key, string _call_index, int _id)
        {
            if (!string.IsNullOrEmpty(_call_index))
            {
                return get_article_list(channel_name, category_id, page_size, page_index, strwhere, out totalcount, out pagelist, _key, _call_index, "__id__");
            }
            else
            {
                return get_article_list(channel_name, category_id, page_size, page_index, strwhere, out totalcount, out pagelist, _key, _id, "__id__");
            }
        }

        /// <summary>
        /// 文章分页列表(可排序)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_params">传输参数</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int page_size, int page_index, string strwhere, string orderby, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetList(channel_name, category_id, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
                pagelist = Utils.OutPageList(page_size, page_index, totalcount, linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dt;
        }

        /// <summary>
        /// 根据频道及规格获得分页列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="spec_ids">规格列表</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, Dictionary<string, string> spec_ids, int page_size, int page_index, string strwhere, string orderby, out int totalcount)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().GetList(channel_name, category_id, spec_ids, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
            }
            else
            {
                totalcount = 0;
            }
            return dt;
        }
        #endregion

        #region 内容标签======================================
        /// <summary>
        /// 根据ID取得内容图片列表
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>String</returns>
        protected DataTable get_article_albums(int article_id)
        {
            if (article_id > 0)
            {
                return new BLL.article_albums().GetImagesList(article_id);
            }
            return null;
        }

        /// <summary>
        /// 根据ID取得模型
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>String</returns>
        protected Model.article get_article_model(int id)
        {
            if (id > 0)
            {
                BLL.article bll = new BLL.article();
                if (bll.Exists(id))
                {
                    return bll.GetModel(id);
                }
            }
            return null;
        }

        /// <summary>
        /// 根据调用标识取得内容
        /// </summary>
        /// <param name="call_index">调用别名</param>
        /// <returns>String</returns>
        protected string get_article_content(string call_index)
        {
            if (string.IsNullOrEmpty(call_index))
                return string.Empty;
            BLL.article bll = new BLL.article();
            if (bll.Exists(call_index))
            {
                return bll.GetModel(call_index).content;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取对应的图片路径
        /// </summary>
        /// <param name="article_id">信息ID</param>
        /// <returns>String</returns>
        protected string get_article_img_url(int article_id)
        {
            Model.article model = new BLL.article().GetModel(article_id);
            if (model != null)
            {
                return model.img_url;
            }
            return "";
        }

        /// <summary>
        /// 获取扩展字段的值
        /// </summary>
        /// <param name="article_id">内容ID</param>
        /// <param name="field_name">扩展字段名</param>
        /// <returns>String</returns>
        protected string get_article_field(int article_id, string field_name)
        {
            Model.article model = new BLL.article().GetModel(article_id);
            if (model != null && model.fields.ContainsKey(field_name))
            {
                return model.fields[field_name];
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取扩展字段的值
        /// </summary>
        /// <param name="call_index">调用别名</param>
        /// <param name="field_name">扩展字段名</param>
        /// <returns>String</returns>
        protected string get_article_field(string call_index, string field_name)
        {
            if (string.IsNullOrEmpty(call_index))
                return string.Empty;
            BLL.article bll = new BLL.article();
            if (!bll.Exists(call_index))
            {
                return string.Empty;
            }
            Model.article model = bll.GetModel(call_index);
            if (model != null && model.fields.ContainsKey(field_name))
            {
                return model.fields[field_name];
            }
            return string.Empty;
        }
        #endregion

        #region 扩展方法===========================================
        /// <summary>
        /// 根据分类和字段名称查询列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="regular">正则表达式{os:字段:分类ID}</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_params">传输参数</param>
        /// <returns></returns>
        protected DataTable get_field_article_list(string channel_name, string regular, int page_size, int page_index, string _strwhere, string orderby, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            pagelist = "";
            totalcount = 0;
            if (regular != "")
            {
                MatchCollection List = RegexHelper.toMatches(@"(\w+)|(\d+)", regular);
                if (List.Count > 2 && List[0].Groups[1].ToString() == "os")
                {
                    string field = List[1].Groups[1].ToString();
                    int category_id = Utils.StrToInt(List[2].Groups[1].ToString(), 0);
                    if (field != "" && category_id > 0)
                    {
                        return get_field_article_list(channel_name, field, category_id, page_size, page_index, _strwhere, orderby, out totalcount, out pagelist, _key, _params);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 根据分类和字段名称查询列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="field">字段名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_params">传输参数</param>
        /// <returns></returns>
        protected DataTable get_field_article_list(string channel_name, string field, int category_id, int page_size, int page_index, string _strwhere, string orderby, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            pagelist = "";
            totalcount = 0;
            DataTable dt = new DataTable();
            DataSet ds = new BLL.article_attribute_value().GetList(category_id, field);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string strWhere = null;
                List<int> idList = new List<int>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string[] arr = dr[field].ToString().Split(',');
                    foreach (string s in arr)
                    {
                        if (!idList.Contains(int.Parse(s)))
                        {
                            idList.Add(int.Parse(s));
                        }
                    }
                }
                if (idList != null && idList.Count > 0)
                {
                    strWhere = string.Format("id in ({0})", string.Join(",",idList));
                    if (_strwhere != "")
                    {
                        strWhere += string.Format(" and {0}", _strwhere);
                    }
                    dt = new BLL.article().GetList(channel_name, 0, page_size, page_index, strWhere, orderby, out totalcount).Tables[0];
                    pagelist = Utils.OutPageList(page_size, page_index, totalcount, linkurl(_key, _params), 8);
                }
            }
            return dt;
        }
        #endregion

    }
}
