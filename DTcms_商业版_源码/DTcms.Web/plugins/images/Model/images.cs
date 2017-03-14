using System;

namespace DTcms.Web.Plugin.Images.Model
{
    /// <summary>
    /// 模型层
    /// <summary>
    [Serializable]
    public class images
    {
        public images() { }

        private int _is_lock = 0;
        private int _id = 0;
        private int _sort_id = 0;
        private DateTime _add_time;
        private string _sign;
        private string _title;
        private string _img_url;
        private string _link_url;
        private string _back_color;
        private string _content;

        #region 方法
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// ID号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 标记
        /// </summary>
        public string sign
        {
            set { _sign = value; }
            get { return _sign; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string link_url
        {
            set { _link_url = value; }
            get { return _link_url; }
        }
        /// <summary>
        /// 背景颜色
        /// </summary>
        public string back_color
        {
            set { _back_color = value; }
            get { return _back_color; }
        }
        /// <summary>
        /// 描述内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        #endregion
    }
}