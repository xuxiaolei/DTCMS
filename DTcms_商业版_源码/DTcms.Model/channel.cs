using System;
using System.Collections.Generic;

namespace DTcms.Model
{
    /// <summary>
    /// 系统频道表
    /// </summary>
    [Serializable]
    public partial class channel
    {
        public channel()
        { }
        #region Model
        private int _id;
        private int _site_id;
        private string _name = "";
        private string _title = "";
        private int _is_albums = 0;
        private int _is_attach = 0;
        private int _is_spec = 0;
        private int _is_attribute = 0;
        private int _is_comment = 0;
        private int _is_type = 0;
        private int _sort_id = 99;
        private string _seo_title = string.Empty;
        private string _seo_keywords = string.Empty;
        private string _seo_description = string.Empty;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 站点ID
        /// </summary>
        public int site_id
        {
            set { _site_id = value; }
            get { return _site_id; }
        }
        /// <summary>
        /// 频道名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 频道标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 是否开启相册功能
        /// </summary>
        public int is_albums
        {
            set { _is_albums = value; }
            get { return _is_albums; }
        }
        /// <summary>
        /// 是否开启附件功能
        /// </summary>
        public int is_attach
        {
            set { _is_attach = value; }
            get { return _is_attach; }
        }
        /// <summary>
        /// 是否开启规格
        /// </summary>
        public int is_spec
        {
            set { _is_spec = value; }
            get { return _is_spec; }
        }
        /// <summary>
        /// 是否开启自定义参数
        /// </summary>
        public int is_attribute
        {
            set { _is_attribute = value; }
            get { return _is_attribute; }
        }
        /// <summary>
        /// 是否开启评论
        /// </summary>
        public int is_comment
        {
            set { _is_comment = value; }
            get { return _is_comment; }
        }
        /// <summary>
        /// 显示方式 0图文、1列表
        /// </summary>
        public int is_type
        {
            set { _is_type = value; }
            get { return _is_type; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }

        private List<channel_field> _channel_fields;
        /// <summary>
        /// 扩展字段 
        /// </summary>
        public List<channel_field> channel_fields
        {
            set { _channel_fields = value; }
            get { return _channel_fields; }
        }
        /// <summary>
        /// SEO 标题
        /// </summary>
        public string seo_title
        {
            set { _seo_title = value; }
            get { return _seo_title; }
        }
        /// <summary>
        /// SEO 关键词
        /// </summary>
        public string seo_keywords
        {
            set { _seo_keywords = value; }
            get { return _seo_keywords; }
        }
        /// <summary>
        /// SEO 描述
        /// </summary>
        public string seo_description
        {
            set { _seo_description = value; }
            get { return _seo_description; }
        }
        #endregion Model

    }
}