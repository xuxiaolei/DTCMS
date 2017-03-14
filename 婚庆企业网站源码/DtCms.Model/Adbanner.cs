using System;
namespace DtCms.Model
{
	/// <summary>
	/// 广告条实体类Adbanner
	/// </summary>
	[Serializable]
	public class Adbanner
	{
		public Adbanner()
		{}
		#region Model
		private int _id;
		private int _aid;
		private string _title;
		private DateTime _starttime;
		private DateTime _endtime;
		private string _adurl;
		private string _linkurl;
		private string _adremark;
		private int _islock;
		private DateTime _addtime;
		/// <summary>
        /// 自增ID PK
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// 广告位ID
		/// </summary>
		public int Aid
		{
			set{ _aid=value;}
			get{return _aid;}
		}
		/// <summary>
        /// 广告条名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
        /// 开始时间
		/// </summary>
		public DateTime StartTime
		{
			set{ _starttime=value;}
			get{return _starttime;}
		}
		/// <summary>
        /// 到期时间
		/// </summary>
		public DateTime EndTime
		{
			set{ _endtime=value;}
			get{return _endtime;}
		}
		/// <summary>
        /// 广告地址
		/// </summary>
		public string AdUrl
		{
			set{ _adurl=value;}
			get{return _adurl;}
		}
		/// <summary>
        /// 链接地址
		/// </summary>
		public string LinkUrl
		{
			set{ _linkurl=value;}
			get{return _linkurl;}
		}
		/// <summary>
        /// 备注说明
		/// </summary>
		public string AdRemark
		{
			set{ _adremark=value;}
			get{return _adremark;}
		}
		/// <summary>
        /// 状态，0正常，1暂停
		/// </summary>
		public int IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		/// <summary>
        /// 发布时间
		/// </summary>
		public DateTime AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}