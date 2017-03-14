using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using DtCms.DBUtility;//请先添加引用
namespace DtCms.DAL
{
    /// <summary>
    /// 数据访问类Advertising。
    /// </summary>
    public class Advertising
    {
        public Advertising()
        { }
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Advertising");
            strSql.Append(" where Id=@Id ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
            parameters[0].Value = Id;

            return DbHelperOleDb.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回长查询数据总数 （分页用到）
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from Advertising ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperOleDb.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(DtCms.Model.Advertising model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Advertising(");
            strSql.Append("Title,AdType,AdRemark,AdNum,AdPrice,AdWidth,AdHeight,AdTarget)");
            strSql.Append(" values (");
            strSql.Append("@Title,@AdType,@AdRemark,@AdNum,@AdPrice,@AdWidth,@AdHeight,@AdTarget)");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@AdType", OleDbType.Integer,4),
					new OleDbParameter("@AdRemark", OleDbType.VarWChar,0),
					new OleDbParameter("@AdNum", OleDbType.Integer,4),
					new OleDbParameter("@AdPrice", OleDbType.Decimal,9),
					new OleDbParameter("@AdWidth", OleDbType.Integer,4),
					new OleDbParameter("@AdHeight", OleDbType.Integer,4),
					new OleDbParameter("@AdTarget", OleDbType.VarWChar,50)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.AdType;
            parameters[2].Value = model.AdRemark;
            parameters[3].Value = model.AdNum;
            parameters[4].Value = model.AdPrice;
            parameters[5].Value = model.AdWidth;
            parameters[6].Value = model.AdHeight;
            parameters[7].Value = model.AdTarget;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(DtCms.Model.Advertising model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Advertising set ");
            strSql.Append("Title=@Title,");
            strSql.Append("AdType=@AdType,");
            strSql.Append("AdRemark=@AdRemark,");
            strSql.Append("AdNum=@AdNum,");
            strSql.Append("AdPrice=@AdPrice,");
            strSql.Append("AdWidth=@AdWidth,");
            strSql.Append("AdHeight=@AdHeight,");
            strSql.Append("AdTarget=@AdTarget");
            strSql.Append(" where Id=@Id ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Title", OleDbType.VarWChar,100),
					new OleDbParameter("@AdType", OleDbType.Integer,4),
					new OleDbParameter("@AdRemark", OleDbType.VarWChar,0),
					new OleDbParameter("@AdNum", OleDbType.Integer,4),
					new OleDbParameter("@AdPrice", OleDbType.Decimal,9),
					new OleDbParameter("@AdWidth", OleDbType.Integer,4),
					new OleDbParameter("@AdHeight", OleDbType.Integer,4),
					new OleDbParameter("@AdTarget", OleDbType.VarWChar,50),
                    new OleDbParameter("@Id", OleDbType.Integer,4)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.AdType;
            parameters[2].Value = model.AdRemark;
            parameters[3].Value = model.AdNum;
            parameters[4].Value = model.AdPrice;
            parameters[5].Value = model.AdWidth;
            parameters[6].Value = model.AdHeight;
            parameters[7].Value = model.AdTarget;
            parameters[8].Value = model.Id;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Advertising ");
            strSql.Append(" where Id=@Id ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
            parameters[0].Value = Id;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
            //删除该广告位下所有的广告
            DelChildAll(Id);
        }

        /// <summary>
        /// 删除该广告位下所有的广告
        /// </summary>
        /// <param name="Pid"></param>
        private void DelChildAll(int Pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Adbanner ");
            strSql.Append(" where Aid=@Pid ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Pid", OleDbType.Integer,4)};
            parameters[0].Value = Pid;

            DbHelperOleDb.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DtCms.Model.Advertising GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,AdType,AdRemark,AdNum,AdPrice,AdWidth,AdHeight,AdTarget from Advertising ");
            strSql.Append(" where Id=@Id ");
            OleDbParameter[] parameters = {
					new OleDbParameter("@Id", OleDbType.Integer,4)};
            parameters[0].Value = Id;

            DtCms.Model.Advertising model = new DtCms.Model.Advertising();
            DataSet ds = DbHelperOleDb.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                if (ds.Tables[0].Rows[0]["AdType"].ToString() != "")
                {
                    model.AdType = int.Parse(ds.Tables[0].Rows[0]["AdType"].ToString());
                }
                model.AdRemark = ds.Tables[0].Rows[0]["AdRemark"].ToString();
                if (ds.Tables[0].Rows[0]["AdNum"].ToString() != "")
                {
                    model.AdNum = int.Parse(ds.Tables[0].Rows[0]["AdNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdPrice"].ToString() != "")
                {
                    model.AdPrice = int.Parse(ds.Tables[0].Rows[0]["AdPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdWidth"].ToString() != "")
                {
                    model.AdWidth = int.Parse(ds.Tables[0].Rows[0]["AdWidth"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdHeight"].ToString() != "")
                {
                    model.AdHeight = int.Parse(ds.Tables[0].Rows[0]["AdHeight"].ToString());
                }
                model.AdTarget = ds.Tables[0].Rows[0]["AdTarget"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Title,AdType,AdRemark,AdNum,AdPrice,AdWidth,AdHeight,AdTarget ");
            strSql.Append(" FROM Advertising ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOleDb.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,Title,AdType,AdRemark,AdNum,AdPrice,AdWidth,AdHeight,AdTarget ");
            strSql.Append(" FROM Advertising ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperOleDb.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}