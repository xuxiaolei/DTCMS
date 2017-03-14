using System;
namespace DtCms.Model
{
	/// <summary>
	/// 友情链接实体类Links
	/// </summary>
	[Serializable]
	public class Links
	{
		public Links()
		{}
		#region Model
		private int _id;
		private string _title;
		private string _username;
		private string _usertel;
		private string _usermail;
		private string _weburl;
		private string _imgurl;
        private int _isimage = 0;
        private int _isred = 0;
        private int _islock = 0;
        private int _sortid = 0;
        private DateTime _addtime = DateTime.Now;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 网站标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 联系人姓名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string UserTel
		{
			set{ _usertel=value;}
			get{return _usertel;}
		}
		/// <summary>
		/// 联系邮箱
		/// </summary>
		public string UserMail
		{
			set{ _usermail=value;}
			get{return _usermail;}
		}
		/// <summary>
		/// 网址
		/// </summary>
		public string WebUrl
		{
			set{ _weburl=value;}
			get{return _weburl;}
		}
		/// <summary>
		/// 图片地址
		/// </summary>
		public string ImgUrl
		{
			set{ _imgurl=value;}
			get{return _imgurl;}
		}
		/// <summary>
		/// 是否图片
		/// </summary>
		public int IsImage
		{
			set{ _isimage=value;}
			get{return _isimage;}
		}
        /// <summary>
        /// 推荐到首页
        /// </summary>
        public int IsRed
        {
            set { _isred = value; }
            get { return _isred; }
        }
		/// <summary>
		/// 是否显示
		/// </summary>
		public int IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
		/// 排序号，越小越向前
		/// </summary>
		public int SortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}

