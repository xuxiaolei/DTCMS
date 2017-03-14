using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据库访问层
    /// </summary>
    public partial class sms_log
    {
        private string databaseprefix;
        public sms_log(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from [" + databaseprefix + "sms_log]");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 查询24小时内发送总数
        /// </summary>
        /// <param name="mobile">手机</param>
        /// <returns></returns>
        public int GetSmsCount(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from [" + databaseprefix + "sms_log]");
            strSql.Append(" where DateDiff(hh,add_time,getDate())<=24 and mobile=@mobile");
            SqlParameter[] parameters = {
                new SqlParameter("@mobile", SqlDbType.NVarChar,30)
            };
            parameters[0].Value = mobile;
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.sms_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [" + databaseprefix + "sms_log](");
            strSql.Append("mobile,content,add_time");
            strSql.Append(") values(");
            strSql.Append("@mobile,@content,@add_time)");
            SqlParameter[] parameters = {
                new SqlParameter("@mobile", SqlDbType.NVarChar,20),
                new SqlParameter("@content", SqlDbType.NVarChar,255),
				new SqlParameter("@add_time", SqlDbType.DateTime)
            };
            parameters[0].Value = model.mobile;
            parameters[1].Value = model.content;
            parameters[2].Value = model.add_time;
            object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (null != obj)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from [" + databaseprefix + "sms_log] where id=@id");
            SqlParameter[] parameters = {
            		new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
