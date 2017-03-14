<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sys_config.aspx.cs" Inherits="DTcms.Web.admin.settings.sys_config" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>系统参数设置</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var dt = {};
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //Tabs
        $("#smstabs").TabBox({bd:".bd .box"});
    });
    //
    function SendAnswer(s, d, p) {
        if ($(s).find("input").eq(0).prop("checked") == true) {
            $(d).html("正则表达式");
            $(p).find(".Validform_checktip").html("*默认为空，不查询");
        } else if ($(s).find("input").eq(1).prop("checked") == true) {
            $(d).html("名称");
            $(p).find(".Validform_checktip").html("*Json节点名称");
        } else if ($(s).find("input").eq(2).prop("checked") == true) {
            $(d).html("节点名称");
            $(p).find(".Validform_checktip").html("*XPath节点名称");
        }
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>系统参数设置</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">系统基本信息</a></li>
        <li><a href="javascript:;">功能权限设置</a></li>
        <li><a href="javascript:;">短信平台设置</a></li>
        <li><a href="javascript:;">邮件发送设置</a></li>
        <li><a href="javascript:;">文件上传设置</a></li>
        <li><a href="javascript:;">其它扩展设置</a></li>
      </ul>
    </div>
  </div>
</div>

<!--主站基本信息-->
<div class="tab-content">
  <dl>
    <dt>主站名称</dt>
    <dd>
      <asp:TextBox ID="webname" runat="server" CssClass="input normal" datatype="*2-255" sucmsg=" " />
      <span class="Validform_checktip">*任意字符，控制在255个字符内</span>
    </dd>
  </dl>
  <dl>
    <dt>主站域名</dt>
    <dd>
      <asp:TextBox ID="weburl" runat="server" CssClass="input normal" datatype="url" sucmsg=" " />
      <span class="Validform_checktip">*以“http://”开头，不能绑定到频道分类</span>
    </dd>
  </dl>
  <dl>
    <dt>公司名称</dt>
    <dd>
      <asp:TextBox ID="webcompany" runat="server" CssClass="input normal" />
    </dd>
  </dl>
  <dl>
    <dt>通讯地址</dt>
    <dd>
      <asp:TextBox ID="webaddress" runat="server" CssClass="input normal" />
    </dd>
  </dl>
  <dl>
    <dt>客服电话</dt>
    <dd>
      <asp:TextBox ID="webtel" runat="server" CssClass="input normal" />
      <span class="Validform_checktip">*非必填，区号+电话号码</span>
    </dd>
  </dl>
  <dl>
    <dt>传真号码</dt>
    <dd>
      <asp:TextBox ID="webfax" runat="server" CssClass="input normal" />
      <span class="Validform_checktip">*非必填，区号+传真号码</span>
    </dd>
  </dl>
  <dl>
    <dt>管理员邮箱</dt>
    <dd>
      <asp:TextBox ID="webmail" runat="server" CssClass="input normal" />
    </dd>
  </dl>
  <dl>
    <dt>主站备案号</dt>
    <dd>
      <asp:TextBox ID="webcrod" runat="server" CssClass="input normal" />
    </dd>
  </dl>
</div>
<!--/网站基本信息-->


<!--功能权限设置-->
<div class="tab-content" style="display:none">
  <dl>
    <dt>网站安装目录</dt>
    <dd>
      <asp:TextBox ID="webpath" runat="server" CssClass="input txt" datatype="*1-100" sucmsg=" " />
      <span class="Validform_checktip">*根目录输入“/”，其它输入“/目录名/”</span>
    </dd>
  </dl>
  <dl>
    <dt>后台管理目录</dt>
    <dd>
      <asp:TextBox ID="webmanagepath" runat="server" CssClass="input txt" datatype="*1-100" sucmsg=" " />
      <span class="Validform_checktip">*默认admin，其它请输入目录名，否则无法进入后台</span>
    </dd>
  </dl>
  <dl>
    <dt>URL重写开关</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="staticstatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
        <asp:ListItem Value="1">伪URL重写</asp:ListItem>
        <asp:ListItem Value="2">生成静态</asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <span class="Validform_checktip">*URL配置规则，点击这里查看说明</span>
    </dd>
  </dl>
  <dl>
    <dt>静态URL后缀</dt>
    <dd>
      <asp:TextBox ID="staticextension" runat="server" CssClass="input small" datatype="*1-100" sucmsg=" " />
      <span class="Validform_checktip">*扩展名，不包括“.”，访问或生成时将会替换此值，如：aspx、html</span>
    </dd>
  </dl>
  <dl>
    <dt>开启会员功能</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="memberstatus" runat="server" />
      </div>
      <span class="Validform_checktip">*关闭后关联会员的内容将失效</span>
    </dd>
  </dl>
  <dl>
    <dt>开启评论审核</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="commentstatus" runat="server" />
      </div>
      <span class="Validform_checktip">*开启后评论将会审核才显示</span>
    </dd>
  </dl>
  <dl>
    <dt>开启管理日志</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="logstatus" runat="server" />
      </div>
      <span class="Validform_checktip">*开启后将会记录管理员在后台的操作日志</span>
    </dd>
  </dl>
  <dl>
    <dt>是否开启网站</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="webstatus" runat="server" />
      </div>
      <span class="Validform_checktip">*关闭后网站前台将不能访问</span>
    </dd>
  </dl>
  <dl>
    <dt>网站关闭原因</dt>
    <dd>
      <asp:TextBox ID="webclosereason" runat="server" CssClass="input" TextMode="MultiLine" />
      <span class="Validform_checktip">支持HTML</span>
    </dd>
  </dl>
  <dl>
    <dt>网站统计代码</dt>
    <dd>
      <asp:TextBox ID="webcountcode" runat="server" CssClass="input" TextMode="MultiLine" />
      <span class="Validform_checktip">支持HTML</span>
    </dd>
  </dl>
</div>
<!--/功能权限设置-->

<!--手机短信设置-->
<div class="tab-content" style="display:none">
  <div class="usertab" id="smstabs">
    <div class="hd">
        <ul>
            <li class="on">发送配置</li>
            <li>查询配置</li>
            <li>错误代码</li>
        </ul>
    </div>
    <div class="bd">
        <div class="box">
          <dl>
            <dt><asp:Label ID="smsTip" runat="server" Text="Label"></asp:Label></dt>
            <dd>
              <div ID="labSmsCount" runat="server" class="smscount"></div>
            </dd>
          </dl>
          <dl>
            <dt>短信API地址</dt>
            <dd>
              <asp:TextBox ID="smsapiurl" runat="server" CssClass="input normal" />
              <span class="Validform_checktip">*以“http://”开头</span>
            </dd>
          </dl>
          <dl>
            <dt>提交方式</dt>
            <dd>
              <div class="rule-multi-radio">
                <asp:RadioButtonList ID="smssubmit" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="post" Selected="True">POST</asp:ListItem>
                <asp:ListItem Value="get">GET</asp:ListItem>
                </asp:RadioButtonList>
              </div>
              <span class="Validform_checktip">*默认为POST</span>
            </dd>
          </dl>
          <dl>
            <dt>平台登录账户</dt>
            <dd>
              <asp:TextBox ID="smsusername" runat="server" CssClass="input txt" />
              <span class="Validform_checktip">*短信平台注册的用户名,{uid}</span>
            </dd>
          </dl>
          <dl>
            <dt>平台登录密码</dt>
            <dd>
              <asp:TextBox ID="smspassword" runat="server" CssClass="input txt" TextMode="Password" />
              <span class="Validform_checktip">*短信平台注册的密码,{pass}</span>
            </dd>
          </dl>
          <dl>
            <dt>参数</dt>
            <dd style=" line-height:18px; padding-bottom:10px;">
                {uid} 用户账号、{pass} 密码、{md5pass} MD5加密密码、{mobile} 号码、{content} 内容
            </dd>
            <dd>
              <asp:TextBox ID="smssendpara" runat="server" CssClass="input" TextMode="MultiLine" />
            </dd>
          </dl>
          <dl>
            <dt>是否加入签名</dt>
            <dd>
               <div class="rule-multi-radio SignBox">
                <asp:RadioButtonList ID="smssign" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
                <asp:ListItem Value="1">签名在前</asp:ListItem>
                <asp:ListItem Value="2">签名在后</asp:ListItem>
                </asp:RadioButtonList>
              </div>
              <span class="Validform_checktip">*如：短信+【公司名称】</span>
            </dd>
          </dl>
          <dl id="SignBox">
            <dt>签名</dt>
            <dd>
              <asp:TextBox ID="smssigntxt" runat="server" CssClass="input txt" />
              <span class="Validform_checktip"></span>
            </dd>
          </dl>
          <script type="text/javascript">
              SignAnswer();
              $(function () {
                  $(".SignBox").find("a").on("click", function () { SignAnswer() });
              })
              function SignAnswer() {
                  if ($("#smssign input").eq(0).prop("checked") == true) {
                      $("#SignBox").hide();
                  } else {
                      $("#SignBox").show();
                  }
              }
          </script>
          <dl>
            <dt>响应结果类型</dt>
            <dd>
              <div class="rule-multi-radio SendBox">
                <asp:RadioButtonList ID="smssendanswer" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="0" Selected="True">文本</asp:ListItem>
                <asp:ListItem Value="1">Json</asp:ListItem>
                <asp:ListItem Value="2">XML</asp:ListItem>
                </asp:RadioButtonList>
              </div>
              <span class="Validform_checktip"></span>
            </dd>
          </dl>
          <dl id="SendPannel">
            <dt id="SendDiv">正则查询</dt>
            <dd>
              <asp:TextBox ID="smssendcode" runat="server" CssClass="input txt" />
              <span class="Validform_checktip">默认为空，不查询</span>
            </dd>
          </dl>
          <script type="text/javascript">
              SendAnswer("#smssendanswer", "#SendDiv", "#SendPannel");
              $(function () {
                  $(".SendBox").find("a").on("click", function () {
                      SendAnswer("#smssendanswer", "#SendDiv", "#SendPannel");
                  });
              })
          </script>
          <dl>
            <dt>成功标识</dt>
            <dd>
              <asp:TextBox ID="smssendlable" runat="server" CssClass="input txt" />
              <span class="Validform_checktip">*如：0或succeed</span>
            </dd>
          </dl>
          <dl>
            <dt>批量间隔符</dt>
            <dd>
              <asp:TextBox ID="smsmark" runat="server" CssClass="input small" />
              <span class="Validform_checktip">*默认为逗号</span>
            </dd>
          </dl>
          <dl>
            <dt>24小时允许发送次数</dt>
            <dd>
              <asp:TextBox ID="smssendcount" runat="server" CssClass="input small" />
              <span class="Validform_checktip">*默认为10，24小时同一手机号最大允许发送次数，0代表不限制</span>
            </dd>
          </dl>
        </div>
        <div class="box hide">
          <dl>
            <dt>查询API地址</dt>
            <dd>
              <asp:TextBox ID="smsqueryapiurl" runat="server" CssClass="input normal" />
              <span class="Validform_checktip">*以“http://”开头</span>
            </dd>
          </dl>
            <dl>
            <dt>参数</dt>
            <dd style=" line-height:18px; padding-bottom:10px;">
                {uid} 用户账号、{pass} 密码、{md5pass} MD5加密密码
            </dd>
            <dd>
              <asp:TextBox ID="smsquerypara" runat="server" CssClass="input" TextMode="MultiLine" />
            </dd>
          </dl>
          <dl>
            <dt>响应结果类型</dt>
            <dd>
              <div class="rule-multi-radio QueryBox">
                <asp:RadioButtonList ID="smsqueryanswer" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="0" Selected="True">文本</asp:ListItem>
                <asp:ListItem Value="1">Json</asp:ListItem>
                <asp:ListItem Value="2">XML</asp:ListItem>
                </asp:RadioButtonList>
              </div>
              <span class="Validform_checktip">*纯文本</span>
            </dd>
          </dl>
          <dl id="QueryPannel">
            <dt id="QueryDiv">正则查询</dt>
            <dd>
              <asp:TextBox ID="smsquerycode" runat="server" CssClass="input txt" />
              <span class="Validform_checktip">为空则不查询</span>
            </dd>
          </dl>
            <script type="text/javascript">
                SendAnswer("#smsqueryanswer", "#QueryDiv", "#QueryPannel");
                $(function () {
                    $(".QueryBox").find("a").on("click", function () {
                        SendAnswer("#smsqueryanswer", "#QueryDiv", "#QueryPannel");
                    });
                })
            </script>
          <dl>
            <dt>结果显示格式</dt>
            <dd>
              <asp:TextBox ID="smsqueryformat" runat="server" CssClass="input txt" />
              <span class="Validform_checktip">*如：短信剩余数量{0}条</span>
            </dd>
          </dl>
        </div>
        <div class="box hide">
            <dl>
            <dt>错误代码</dt>
            <dd>
              <asp:TextBox ID="smserrorcode" runat="server" CssClass="input txt maxheight" 
                    TextMode="MultiLine" />
              <span class="Validform_checktip">*填写说明：代码|代码说明，以回车换行为一行。</span>
            </dd>
          </dl>
        </div>
    </div>
  </div>
</div>
<!--/手机短信设置-->

<!--邮件发送设置-->
<div class="tab-content" style="display:none">
  <dl>
    <dt>SMTP服务器</dt>
    <dd>
      <asp:TextBox ID="emailsmtp" runat="server" CssClass="input normal" datatype="*0-100" sucmsg=" " />
      <span class="Validform_checktip">*发送邮件的SMTP服务器地址</span>
    </dd>
  </dl>
  <dl>
    <dt>SSL加密连接</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="emailssl" runat="server" />
      </div>
      <span class="Validform_checktip">*是否启用SSL加密连接</span>
    </dd>
  </dl>
  <dl>
    <dt>SMTP端口</dt>
    <dd>
      <asp:TextBox ID="emailport" runat="server" CssClass="input small" datatype="n" sucmsg=" " />
      <span class="Validform_checktip">*SMTP服务器的端口，大部分服务商都支持25端口</span>
    </dd>
  </dl>
  <dl>
    <dt>发件人地址</dt>
    <dd>
      <asp:TextBox ID="emailfrom" runat="server" CssClass="input normal" datatype="e" sucmsg=" " />
      <span class="Validform_checktip">*发件人的邮箱地址</span>
    </dd>
  </dl>
  <dl>
    <dt>邮箱账号</dt>
    <dd>
      <asp:TextBox ID="emailusername" runat="server" CssClass="input normal" datatype="*0-100" sucmsg=" " />
      <span class="Validform_checktip">*</span>
    </dd>
  </dl>
  <dl>
    <dt>邮箱密码</dt>
    <dd>
      <asp:TextBox ID="emailpassword" runat="server" CssClass="input normal" datatype="*0-100" sucmsg=" " TextMode="Password" />
      <span class="Validform_checktip">*</span>
    </dd>
  </dl>
  <dl>
    <dt>发件人昵称</dt>
    <dd>
      <asp:TextBox ID="emailnickname" runat="server" CssClass="input normal" datatype="*0-50" sucmsg=" " />
      <span class="Validform_checktip">*对方收到邮件时显示的昵称</span>
    </dd>
  </dl>
</div>
<!--/邮件发送设置-->

<!--上传配置-->
<div class="tab-content" style="display:none">
  <dl>
    <dt>文件上传目录</dt>
    <dd>
      <asp:TextBox ID="filepath" runat="server" CssClass="input txt" datatype="*2-100" sucmsg=" " />
      <span class="Validform_checktip">*文件保存的目录名，自动创建根目录下</span>
    </dd>
  </dl>
  <dl>
    <dt>文件保存方式</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="filesave" runat="server" datatype="*" sucmsg=" ">
          <asp:ListItem Value="1">按年月日每天一个目录</asp:ListItem>
          <asp:ListItem Value="2" Selected="True">按年月/日/存入不同目录</asp:ListItem>
        </asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>编辑器图片</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="fileremote" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0" Selected="True">关闭下载</asp:ListItem>
        <asp:ListItem Value="1">自动下载</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
   <dl>
    <dt>图片上传类型</dt>
    <dd>
      <asp:TextBox ID="imgextension" runat="server" CssClass="input normal" datatype="*1-500" sucmsg=" " />
      <span class="Validform_checktip">*以英文的逗号分隔开，如：“.jpg,.png”</span>
    </dd>
  </dl>
  <dl>
    <dt>文件上传类型</dt>
    <dd>
      <asp:TextBox ID="fileextension" runat="server" CssClass="input normal" datatype="*1-500" sucmsg=" " />
      <span class="Validform_checktip">*以英文的逗号分隔开，如：“zip,rar”</span>
    </dd>
  </dl>
  <dl>
    <dt>视频上传类型</dt>
    <dd>
      <asp:TextBox ID="videoextension" runat="server" CssClass="input normal" datatype="*1-500" sucmsg=" " />
      <span class="Validform_checktip">*以英文的逗号分隔开，如：“mp4,flv”</span>
    </dd>
  </dl>
  <dl>
    <dt>图片上传大小</dt>
    <dd>
      <asp:TextBox ID="imgsize" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> B
      <span class="Validform_checktip">*超过设定的图片大小不予上传，0不限制</span>
    </dd>
  </dl>
  <dl>
    <dt>附件上传大小</dt>
    <dd>
      <asp:TextBox ID="attachsize" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> B
      <span class="Validform_checktip">*超过设定的文件大小不予上传，0不限制</span>
    </dd>
  </dl>
  <dl>
    <dt>视频上传大小</dt>
    <dd>
      <asp:TextBox ID="videosize" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> B
      <span class="Validform_checktip">*超过设定的文件大小不予上传，0不限制</span>
    </dd>
  </dl>
  <dl>
    <dt>图片最大尺寸</dt>
    <dd>
      <asp:TextBox ID="imgmaxheight" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> ×
      <asp:TextBox ID="imgmaxwidth" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> px
      <span class="Validform_checktip">*左边高度，右边宽度，超出自动裁剪，0为不受限制</span>
    </dd>
  </dl>
  <dl>
    <dt>缩略图生成尺寸</dt>
    <dd>
      <asp:TextBox ID="thumbnailheight" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> ×
      <asp:TextBox ID="thumbnailwidth" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> px
      <span class="Validform_checktip">*左边高度，右边宽度，0为不生成缩略图</span>
    </dd>
  </dl>
  <dl>
    <dt>缩略图生成方式</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="thumbnailmode" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="Cut" Selected="True">裁剪</asp:ListItem>
        <asp:ListItem Value="HWB">指定高宽缩放（补白）</asp:ListItem>
        <asp:ListItem Value="W">指定宽，高按比例</asp:ListItem>
        <asp:ListItem Value="H">指定高，宽按比例</asp:ListItem>
        <asp:ListItem Value="HW">指定高宽缩放</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>图片水印类型</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="watermarktype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0">关闭水印</asp:ListItem>
        <asp:ListItem Value="1">文字水印</asp:ListItem>
        <asp:ListItem Value="2">图片水印</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>图片水印位置</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="watermarkposition" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1">左上</asp:ListItem>
        <asp:ListItem Value="2">中上</asp:ListItem>
        <asp:ListItem Value="3">右上</asp:ListItem>
        <asp:ListItem Value="4">左中</asp:ListItem>
        <asp:ListItem Value="5">居中</asp:ListItem>
        <asp:ListItem Value="6">右中</asp:ListItem>
        <asp:ListItem Value="7">左下</asp:ListItem>
        <asp:ListItem Value="8">中下</asp:ListItem>
        <asp:ListItem Value="9">右下</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>图片生成质量</dt>
    <dd>
      <asp:TextBox ID="watermarkimgquality" runat="server" CssClass="input small" datatype="n" sucmsg=" " />
      <span class="Validform_checktip">*只适用于加水印的jpeg格式图片.取值范围 0-100, 0质量最低, 100质量最高, 默认80</span>
    </dd>
  </dl>
  <dl>
    <dt>图片水印文件</dt>
    <dd>
      <asp:TextBox ID="watermarkpic" runat="server" CssClass="input txt" />
      <asp:FileUpload ID="imgUpload" runat="server" CssClass="upimg" />
    </dd>
    <dd id="ImgDiv" class="imgdiv" runat="server" visible="false">
        <asp:Image runat="server" ID="ImgUrl" />
    </dd>
  </dl>
  <dl>
    <dt>水印透明度</dt>
    <dd>
      <asp:TextBox ID="watermarktransparency" runat="server" CssClass="input small" datatype="n" sucmsg=" " />
      <span class="Validform_checktip">*取值范围1--10 (10为不透明)</span>
    </dd>
  </dl>
  <dl>
    <dt>水印文字</dt>
    <dd>
      <asp:TextBox ID="watermarktext" runat="server" CssClass="input txt" datatype="s2-100" sucmsg=" " />
      <span class="Validform_checktip">*文字水印的内容</span>
    </dd>
  </dl>
  <dl>
    <dt>文字字体</dt>
    <dd>
      <div class="rule-single-select up">
        <asp:DropDownList id="watermarkfont" runat="server">
          	<asp:ListItem Value="Arial">Arial</asp:ListItem>
	        <asp:ListItem Value="Arial Black">Arial Black</asp:ListItem>
	        <asp:ListItem Value="Batang">Batang</asp:ListItem>
	        <asp:ListItem Value="BatangChe">BatangChe</asp:ListItem>
	        <asp:ListItem Value="Comic Sans MS">Comic Sans MS</asp:ListItem>
	        <asp:ListItem Value="Courier New">Courier New</asp:ListItem>
	        <asp:ListItem Value="Dotum">Dotum</asp:ListItem>
	        <asp:ListItem Value="DotumChe">DotumChe</asp:ListItem>
	        <asp:ListItem Value="Estrangelo Edessa">Estrangelo Edessa</asp:ListItem>
	        <asp:ListItem Value="Franklin Gothic Medium">Franklin Gothic Medium</asp:ListItem>
	        <asp:ListItem Value="Gautami">Gautami</asp:ListItem>
	        <asp:ListItem Value="Georgia">Georgia</asp:ListItem>
	        <asp:ListItem Value="Gulim">Gulim</asp:ListItem>
	        <asp:ListItem Value="GulimChe">GulimChe</asp:ListItem>
	        <asp:ListItem Value="Gungsuh">Gungsuh</asp:ListItem>
	        <asp:ListItem Value="GungsuhChe">GungsuhChe</asp:ListItem>
	        <asp:ListItem Value="Impact">Impact</asp:ListItem>
	        <asp:ListItem Value="Latha">Latha</asp:ListItem>
	        <asp:ListItem Value="Lucida Console">Lucida Console</asp:ListItem>
	        <asp:ListItem Value="Lucida Sans Unicode">Lucida Sans Unicode</asp:ListItem>
	        <asp:ListItem Value="Mangal">Mangal</asp:ListItem>
	        <asp:ListItem Value="Marlett">Marlett</asp:ListItem>
	        <asp:ListItem Value="Microsoft Sans Serif">Microsoft Sans Serif</asp:ListItem>
	        <asp:ListItem Value="MingLiU">MingLiU</asp:ListItem>
	        <asp:ListItem Value="MS Gothic">MS Gothic</asp:ListItem>
	        <asp:ListItem Value="MS Mincho">MS Mincho</asp:ListItem>
	        <asp:ListItem Value="MS PGothic">MS PGothic</asp:ListItem>
	        <asp:ListItem Value="MS PMincho">MS PMincho</asp:ListItem>
	        <asp:ListItem Value="MS UI Gothic">MS UI Gothic</asp:ListItem>
	        <asp:ListItem Value="MV Boli">MV Boli</asp:ListItem>
	        <asp:ListItem Value="Palatino Linotype">Palatino Linotype</asp:ListItem>
	        <asp:ListItem Value="PMingLiU">PMingLiU</asp:ListItem>
	        <asp:ListItem Value="Raavi">Raavi</asp:ListItem>
	        <asp:ListItem Value="Shruti">Shruti</asp:ListItem>
	        <asp:ListItem Value="Sylfaen">Sylfaen</asp:ListItem>
	        <asp:ListItem Value="Symbol">Symbol</asp:ListItem>
	        <asp:ListItem Value="Tahoma" Selected="True">Tahoma</asp:ListItem>
	        <asp:ListItem Value="Times New Roman">Times New Roman</asp:ListItem>
	        <asp:ListItem Value="Trebuchet MS">Trebuchet MS</asp:ListItem>
	        <asp:ListItem Value="Tunga">Tunga</asp:ListItem>
	        <asp:ListItem Value="Verdana">Verdana</asp:ListItem>
	        <asp:ListItem Value="Webdings">Webdings</asp:ListItem>
	        <asp:ListItem Value="Wingdings">Wingdings</asp:ListItem>
	        <asp:ListItem Value="仿宋_GB2312">仿宋_GB2312</asp:ListItem>
	        <asp:ListItem Value="宋体">宋体</asp:ListItem>
	        <asp:ListItem Value="新宋体">新宋体</asp:ListItem>
	        <asp:ListItem Value="楷体_GB2312">楷体_GB2312</asp:ListItem>
	        <asp:ListItem Value="黑体">黑体</asp:ListItem>
        </asp:DropDownList>
      </div>
      <asp:TextBox ID="watermarkfontsize" runat="server" CssClass="input small" datatype="n" sucmsg=" " /> px
      <span class="Validform_checktip">*文字水印的字体和大小 </span>
    </dd>
  </dl>
</div>
<!--/上传配置-->
<!--其它扩展设置-->
<div class="tab-content" style="display:none">
    <dl>
        <dt>格式化代码：</dt>
        <dd>
          <div class="rule-single-checkbox">
              <asp:CheckBox ID="chbFomatPage" runat="server" />
          </div>
        </dd>
    </dl>
    <dl>
        <dt>开启页面缓存：</dt>
        <dd>
            <div class="rule-single-checkbox CacheBox">
              <asp:CheckBox ID="chbPageCache" runat="server" />
            </div>
        </dd>
    </dl>
    <div id="divCache">
        <dl>
            <dt>缓存时间</dt>
            <dd>
              <asp:TextBox ID="txtCacheTime" runat="server" CssClass="input" datatype="n" sucmsg=" " />
              <span class="Validform_checktip">*单位（分钟）默认30</span>
            </dd>
        </dl>
        <dl>
            <dt>缓存后缀</dt>
            <dd>
              <asp:TextBox ID="txtCacheFix" runat="server" CssClass="input" datatype="*" sucmsg=" " />
              <span class="Validform_checktip">**扩展名，不包括“.”，默认为cache</span>
            </dd>
        </dl>
    </div>
    <script type="text/javascript">
        CacheSwitch();
        $(function () {
            $(".CacheBox").find("a").on("click", function () { CacheSwitch() });
        })
        function CacheSwitch() {
            if ($('#chbPageCache').is(':checked')) {
                $("#divCache").show();
            } else {
                $("#divCache").hide();
            }
        }
    </script>
    <dl>
        <dt>插件删除操作：</dt>
        <dd>
            <div class="rule-multi-radio">
                <asp:RadioButtonList ID="rblDelTable" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="0">删除数据库</asp:ListItem>
                <asp:ListItem Value="1">保留数据库</asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </dd>
    </dl>
    <dl>
        <dt>文章作者</dt>
        <dd>
          <asp:TextBox ID="txtAuthor" runat="server" CssClass="input normal" datatype="*" sucmsg=" " />
          <span class="Validform_checktip"></span>
        </dd>
    </dl>
    <dl>
        <dt>文章来源</dt>
        <dd>
          <asp:TextBox ID="txtSource" runat="server" CssClass="input normal" datatype="*" sucmsg=" " />
          <span class="Validform_checktip"></span>
        </dd>
    </dl>
</div>
<!--/其它扩展设置-->

<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
