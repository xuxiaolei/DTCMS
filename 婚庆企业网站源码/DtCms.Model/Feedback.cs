using System;
namespace DtCms.Model
{
	/// <summary>
	/// 在线留言实体类Feedback
	/// </summary>
	[Serializable]
	public class Feedback
	{
		public Feedback()
		{}
		#region Model
		private int _id;
		private string _username;
		private string _usertel;
		private string _userqq;
		private string _title;
		private string _content;
        private int _islock = 0;
        private DateTime _addtime = DateTime.Now;
        private string _recontent = "";
        private DateTime _retime = DateTime.Now;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 昵称或姓名
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
		/// 联系QQ
		/// </summary>
		public string UserQQ
		{
			set{ _userqq=value;}
			get{return _userqq;}
		}
		/// <summary>
		/// 留言标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 留言内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 是否不显示
		/// </summary>
		public int IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
		/// 留言时间
		/// </summary>
		public DateTime AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 回复内容
		/// </summary>
		public string ReContent
		{
			set{ _recontent=value;}
			get{return _recontent;}
		}
		/// <summary>
		/// 回复时间
		/// </summary>
		public DateTime ReTime
		{
			set{ _retime=value;}
			get{return _retime;}
		}
		#endregion Model

	}
}

