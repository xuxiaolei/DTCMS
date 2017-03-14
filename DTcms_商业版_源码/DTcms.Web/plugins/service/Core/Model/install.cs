using System;

namespace DTcms.Web.Plugin.Service.Model
{
    /// <summary>
    /// 设置参数模型
    /// </summary>
    [Serializable]
    public partial class install
    {
        public install()
        { }

        private int _status = 0;
        private string _content = "";

        /// <summary>
        /// 状态
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
    }
}
