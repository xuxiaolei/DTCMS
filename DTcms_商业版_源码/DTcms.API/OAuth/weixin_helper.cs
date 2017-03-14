﻿using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.API.OAuth
{
    public class weixin_helper
    {
        public weixin_helper()
        { }

        /// <summary>
        /// 取得临时的Access Token
        /// </summary>
        /// <param name="code">临时Authorization Code，官方提示2小时过期</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(string code)
        {
            //获得配置信息
            oauth_config config = oauth_helper.get_config("weixin");
            string send_url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + config.oauth_app_id + "&secret=" + config.oauth_app_key + "&code=" + code + "&grant_type=authorization_code";
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("errcode"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                return dic;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户个人信息
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <param name="open_id">普通用户的标识，对当前开发者帐号唯一</param>
        /// <returns>JsonData</returns>
        public static Dictionary<string, object> get_info(string access_token, string open_id)
        {
            string send_url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + open_id;
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("errcode"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

    }
}
