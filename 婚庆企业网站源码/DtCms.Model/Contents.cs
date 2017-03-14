using System;
namespace DtCms.Model
{
	/// <summary>
	/// 实体类Contents 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Contents
	{
		public Contents()
		{}
		#region Model
		private int _id;
		private string _title;
		private int _classid;
		private string _content;
        private int _sortid = 0;
		/// <summary>
		/// 自增ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 单页标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		/// 详细内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 排序号，越小越向前
		/// </summary>
		public int SortId
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		#endregion Model

	}
}

