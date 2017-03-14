using System;
namespace DtCms.Model
{
	/// <summary>
	/// 实体类Article 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Article
	{
		public Article()
		{}
		#region Model
		private int _id;
		private string _title;
		private string _author;
		private string _form;
		private string _keyword;
		private string _zhaiyao;
		private int _classid;
		private string _imgurl;
		private string _daodu;
		private string _content;
        private int _click = 0;
        private int _ismsg = 0;
        private int _istop = 0;
        private int _isred = 0;
        private int _ishot = 0;
        private int _isslide = 0;
        private int _islock = 0;
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
		/// 文章标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 作者
		/// </summary>
		public string Author
		{
			set{ _author=value;}
			get{return _author;}
		}
		/// <summary>
		/// 来源
		/// </summary>
		public string Form
		{
			set{ _form=value;}
			get{return _form;}
		}
		/// <summary>
		/// 关健字
		/// </summary>
		public string Keyword
		{
			set{ _keyword=value;}
			get{return _keyword;}
		}
		/// <summary>
		/// 摘要
		/// </summary>
		public string Zhaiyao
		{
			set{ _zhaiyao=value;}
			get{return _zhaiyao;}
		}
		/// <summary>
		/// 所属类别
		/// </summary>
		public int ClassId
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 文章图片
		/// </summary>
		public string ImgUrl
		{
			set{ _imgurl=value;}
			get{return _imgurl;}
		}
		/// <summary>
		/// 文章导读
		/// </summary>
		public string Daodu
		{
			set{ _daodu=value;}
			get{return _daodu;}
		}
		/// <summary>
		/// 详细介绍
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 浏览次数
		/// </summary>
		public int Click
		{
			set{ _click=value;}
			get{return _click;}
		}
		/// <summary>
		/// 是否允许评论
		/// </summary>
		public int IsMsg
		{
			set{ _ismsg=value;}
			get{return _ismsg;}
		}
		/// <summary>
		/// 是否置顶
		/// </summary>
		public int IsTop
		{
			set{ _istop=value;}
			get{return _istop;}
		}
		/// <summary>
		/// 是否推荐
		/// </summary>
		public int IsRed
		{
			set{ _isred=value;}
			get{return _isred;}
		}
		/// <summary>
		/// 是否热门
		/// </summary>
		public int IsHot
		{
			set{ _ishot=value;}
			get{return _ishot;}
		}
		/// <summary>
		/// 是否幻灯片
		/// </summary>
		public int IsSlide
		{
			set{ _isslide=value;}
			get{return _isslide;}
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

