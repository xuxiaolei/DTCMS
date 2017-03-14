using System;
namespace DtCms.Model
{
	/// <summary>
	/// 产品实体类Products
	/// </summary>
	[Serializable]
	public class Products
	{
		public Products()
		{}
		#region Model
		private int _id;
		private string _title;
		private int _classid;
		private string _guige;
		private string _xinghao;
		private decimal _price;
		private string _imgurl;
		private string _content;
		private int _click;
		private int _ismsg;
		private int _istop;
		private int _isred;
		private int _ishot;
		private int _isslide;
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
		/// 产品名称
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 所属分类
		/// </summary>
		public int ClassId
		{
			set{ _classid=value;}
			get{return _classid;}
		}
		/// <summary>
		/// 产品规格
		/// </summary>
		public string Guige
		{
			set{ _guige=value;}
			get{return _guige;}
		}
		/// <summary>
		/// 产品型号
		/// </summary>
		public string Xinghao
		{
			set{ _xinghao=value;}
			get{return _xinghao;}
		}
		/// <summary>
		/// 产品价格
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 产品图片
		/// </summary>
		public string ImgUrl
		{
			set{ _imgurl=value;}
			get{return _imgurl;}
		}
		/// <summary>
		/// 产品详细介绍
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

