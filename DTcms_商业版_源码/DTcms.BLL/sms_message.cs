using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 手机短信
    /// </summary>
    public partial class sms_message
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private DAL.sms_log log;
        public sms_message()
        {
            log = new DAL.sms_log(siteConfig.sysdatabaseprefix);
        }

        #region 发送手机短信
        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="mobiles">手机号码，以英文“,”逗号分隔开</param>
        /// <param name="content">短信内容</param>
        /// <param name="pass">短信通道1验证码通道2广告通道</param>
        /// <param name="msg">返回提示信息</param>
        /// <returns>bool</returns>
        public bool Send(string mobiles, string content, int pass, out string msg)
        {
            //检查是否设置好短信账号
            if (string.IsNullOrEmpty(siteConfig.smsapiurl) || string.IsNullOrEmpty(siteConfig.smsusername) || string.IsNullOrEmpty(siteConfig.smspassword))
            {
                msg = "短信配置参数有误，请完善后再提交！";
                return false;
            }
            //检查手机号码，如果超过2000则分批发送
            int sucCount = 0; //成功提交数量
            string errorMsg = string.Empty; //错误消息
            string[] oldMobileArr = mobiles.Split(',');
            int batch = oldMobileArr.Length / 2000 + 1; //2000条为一批，求出分多少批

            //赋值参数
            string temp = string.Empty;
            string result = string.Empty;
            string parameter = siteConfig.smssendpara;
            parameter = parameter.Replace("{uid}", siteConfig.smsusername);
            parameter = parameter.Replace("{pass}", siteConfig.smspassword);
            parameter = parameter.Replace("{md5pass}", Utils.MD5(siteConfig.smspassword));
            if (parameter.Contains("{content}"))
            {
                string _content = content;
                switch (siteConfig.smssign)
                {
                    case 1:
                        _content = siteConfig.smssigntxt + _content;
                        break;
                    case 2:
                        _content += siteConfig.smssigntxt;
                        break;
                }
                parameter = parameter.Replace("{content}", Utils.UrlEncode(_content));
            }
            //加载错误代码
            Dictionary<string, string> dic = ErorrCode();
            //循环发送
            for (int i = 0; i < batch; i++)
            {
                StringBuilder sb = new StringBuilder();
                int sendCount = 0; //发送数量
                int maxLenght = (i + 1) * 2000; //循环最大的数
                //检测号码，忽略不合格的，重新组合
                for (int j = 0; j < oldMobileArr.Length && j < maxLenght; j++)
                {
                    int arrNum = j + (i * 2000);
                    string mobile = oldMobileArr[arrNum].Trim();
                    if ("" != RegexHelper.toStr(@"^1\d{10}$", mobile))
                    {
                        if (siteConfig.smssendcount == 0 || log.GetSmsCount(mobile) < siteConfig.smssendcount)
                        {
                            sendCount++;
                            sb.Append(mobile + siteConfig.smsmark);
                        }
                    }
                }
                //发送短信
                if (sb.ToString().Length > 0)
                {
                    try
                    {
                        //号码列表
                        temp = Utils.DelLastChar(sb.ToString(), siteConfig.smsmark);
                        //写入参数
                        parameter = parameter.Replace("{mobile}", temp);
                        bool status = false;
                        if (!string.IsNullOrWhiteSpace(siteConfig.smsquerycode))
                        {
                            status = true;
                        }
                        //提交方式
                        if ("post" == siteConfig.smssubmit)
                        {
                            result = Utils.HttpPost(siteConfig.smsapiurl, parameter);
                        }
                        else
                        {
                            if (!parameter.StartsWith("?"))
                            {
                                parameter = "?" + parameter;
                            }
                            result = Utils.HttpGet(siteConfig.smsapiurl + parameter);
                        }
                        //返回结果类型
                        switch (siteConfig.smsqueryanswer)
                        {
                            case 0:
                                if (status)
                                {
                                    result = RegexHelper.toStr(siteConfig.smssendcode, result);
                                }
                                break;
                            case 1:
                                if (status)
                                {
                                    result = JsonHelper.GetString(result, siteConfig.smssendcode);
                                }
                                break;
                            case 2:
                                if (status)
                                {
                                    result = XmlHelper.GetNodesValue(result, siteConfig.smssendcode);
                                }
                                break;
                        }
                        if (result != siteConfig.smssendlable)
                        {
                            if (dic.ContainsKey(result))
                            {
                                errorMsg = string.Format("代码：{0}，{1}", result, dic[result]);
                            }
                            else
                            {
                                errorMsg = string.Format("代码：{0}，{1}", result, "错误代码未收录！");
                            }
                            continue;
                        }
                        sucCount += sendCount; //成功数量

                        //写入短信记录
                        string[] arr = temp.Replace(siteConfig.smsmark, ",").Split(',');
                        foreach (string m in arr)
                        {
                            log.Add(new Model.sms_log { mobile = m, content = content, add_time = DateTime.Now });
                        }
                    }
                    catch
                    {
                        //没有动作
                    }
                }
            }
            //返回状态
            if (sucCount > 0)
            {
                msg = "成功提交" + sucCount + "条，失败" + (oldMobileArr.Length - sucCount) + "条";
                return true;
            }
            msg = errorMsg;
            return false;
        } 
        #endregion

        #region 查询账户剩余短信数量
        /// <summary>
        /// 查询账户剩余短信数量
        /// </summary>
        public int GetAccountQuantity(out string errorMsg)
        {
            //检查是否设置好短信账号
            if (string.IsNullOrEmpty(siteConfig.smsqueryapiurl) || string.IsNullOrEmpty(siteConfig.smsusername) || string.IsNullOrEmpty(siteConfig.smspassword))
            {
                errorMsg = "查询出错：请完善账户信息";
                return 0;
            }
            try
            {
                bool status = false;
                string result, parameter = siteConfig.smsquerypara;
                parameter = parameter.Replace("{uid}", siteConfig.smsusername);
                parameter = parameter.Replace("{pass}", siteConfig.smspassword);
                parameter = parameter.Replace("{md5pass}", Utils.MD5(siteConfig.smspassword));
                //提交方式
                if ("post" == siteConfig.smssubmit)
                {
                    result = Utils.HttpPost(siteConfig.smsapiurl, parameter);
                }
                else
                {
                    if (!parameter.StartsWith("?"))
                    {
                        parameter = "?" + parameter;
                    }
                    result = Utils.HttpGet(siteConfig.smsapiurl + parameter);
                }
                if (!string.IsNullOrWhiteSpace(siteConfig.smsquerycode))
                {
                    status = true;
                }
                //返回结果类型
                switch (siteConfig.smsqueryanswer)
                {
                    case 0:
                        if (status)
                        {
                            result = RegexHelper.toStr(siteConfig.smsquerycode, result);
                        }
                        break;
                    case 1:
                        if (status)
                        {
                            result = JsonHelper.GetString(result, siteConfig.smsquerycode);
                        }
                        break;
                    case 2:
                        if (status)
                        {
                            result = XmlHelper.GetNodesValue(result, siteConfig.smsquerycode);
                        }
                        break;
                    default:
                        errorMsg = "115";
                        break;
                }
                errorMsg = string.Empty;
                return Utils.StrToInt(result, 0);
            }
            catch
            {
                errorMsg = "发生未知错误";
                return 0;
            }
        } 
        #endregion

        #region 私有方法
        /// <summary>
        /// 错误代码
        /// </summary>
        private Dictionary<string, string> ErorrCode()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] valArr = siteConfig.smserrorcode.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            for (int i = 0; i < valArr.Length; i++)
            {
                string[] valItemArr = valArr[i].Split('|');
                if (valItemArr.Length == 2)
                {
                    dic.Add(valItemArr[0], valItemArr[1]);
                }
            }
            return dic;
        }
        #endregion
    }
}