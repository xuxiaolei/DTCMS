using System;

namespace DTcms.Web.Plugin.Lable.Model
{
    /// <summary>
    /// 模型层
    /// <summary>
    [Serializable]
    public class lable
    {
        public lable() { }

        private int _id = 0;
        private string _call_index = string.Empty;
        private string _title = string.Empty;
        private int _type = 0;
        private int _sort_id = 0;
        private string _content = string.Empty;
        private string _user_name = string.Empty;
        private int _is_lock = 0;
        private DateTime _add_time = DateTime.Now;

        #region 方法
        /// <summary>
        /// ID号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 调用名称
        /// </summary>
        public string call_index
        {
            set { _call_index = value; }
            get { return _call_index; }
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
        /// 类型
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
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
        /// 内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        #endregion
    }
}