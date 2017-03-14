using System;
namespace DtCms.Model
{
	/// <summary>
	/// 实体类Channel 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Channel
	{
		public Channel()
		{}
		#region Model
		private int _id;
		private string _title;
		private int _parentid;
		private string _classlist;
		private int _classlayer;
		private int _classorder;
        private string _pageurl;
		private int _kindid;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 类别名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 父类ID
		/// </summary>
		public int ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 类别ID列表
		/// </summary>
		public string ClassList
		{
			set{ _classlist=value;}
			get{return _classlist;}
		}
		/// <summary>
		/// 类别深度
		/// </summary>
		public int ClassLayer
		{
			set{ _classlayer=value;}
			get{return _classlayer;}
		}
		/// <summary>
		/// 类别排序
		/// </summary>
		public int ClassOrder
		{
			set{ _classorder=value;}
			get{return _classorder;}
		}
        /// <summary>
        /// 页面导航
        /// </summary>
        public string PageUrl
        {
            set { _pageurl = value; }
            get { return _pageurl; }
        }
		/// <summary>
		/// 类别种类
		/// </summary>
		public int KindId
		{
			set{ _kindid=value;}
			get{return _kindid;}
		}
		#endregion Model

	}
}

