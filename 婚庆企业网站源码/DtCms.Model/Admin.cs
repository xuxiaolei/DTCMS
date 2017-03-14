using System;
namespace DtCms.Model
{
	/// <summary>
	/// 实体类Admin 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Admin
	{
		public Admin()
		{}
		#region Model
		private int _id;
		private string _username;
		private string _userpwd;
		private string _readname;
		private string _useremail;
		private int _usertype;
		private string _userlevel;
		private int _islock;
		/// <summary>
		/// 自增编号
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 登录帐号
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 登录密码
		/// </summary>
		public string UserPwd
		{
			set{ _userpwd=value;}
			get{return _userpwd;}
		}
		/// <summary>
		/// 真实名称
		/// </summary>
		public string ReadName
		{
			set{ _readname=value;}
			get{return _readname;}
		}
		/// <summary>
		/// 电子邮件
		/// </summary>
		public string UserEmail
		{
			set{ _useremail=value;}
			get{return _useremail;}
		}
		/// <summary>
		/// 用户级别
		/// </summary>
		public int UserType
		{
			set{ _usertype=value;}
			get{return _usertype;}
		}
		/// <summary>
		/// 用户权限
		/// </summary>
		public string UserLevel
		{
			set{ _userlevel=value;}
			get{return _userlevel;}
		}
		/// <summary>
		/// 是否锁定
		/// </summary>
		public int IsLock
		{
			set{ _islock=value;}
			get{return _islock;}
		}
		#endregion Model

	}
}

