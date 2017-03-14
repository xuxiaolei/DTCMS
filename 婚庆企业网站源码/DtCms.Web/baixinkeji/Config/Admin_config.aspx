<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_config.aspx.cs" Inherits="DtCms.Web.Admin.Config.admin_config"  ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>系统参数设置</title>
<link rel="stylesheet" href="../images/style.css" type="text/css" />
<script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
<script type="text/javascript" src="../../js/messages_cn.js"></script>
<script type="text/javascript" src="../js/cursorfocus.js"></script>
<script type="text/javascript"> 
$(function() {
	//表单验证JS
	$("#form1").validate({
		//出错时添加的标签
		errorElement: "span",
		success:function(label){
			//正确时的样式
			label.text(" ").addClass("success");
		}
	});
});
</script>
</head>
<body style="padding:10px;">
<form id="form1" runat="server">
<div class="navigation"><b>您当前的位置：首页 &gt; 系统管理 &gt; 系统参数设置</b></div>
<div style="padding-bottom:10px;"></div>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
<tbody>
  <tr>
    <th colspan="2" align="left">系统基本设置（注意：如果你不是专业人员请勿改动，只有开放文件的读写权限才能修改）</th>
  </tr>
  <tr>
    <td width="25%" align="right">网站标题：</td>
    <td width="75%">
        <asp:TextBox ID="txtWebName" runat="server" CssClass="input required" size="48" 
            maxlength="50" HintTitle="系统的名称" HintInfo="给你的系统起个有意义的名字哦，长度不能超过50个字符。"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="right">网站域名：</td>
    <td>
	<asp:TextBox ID="txtWebUrl" runat="server" CssClass="input required" size="48" 
            maxlength="100" HintTitle="网站的域名" HintInfo="请以http://为开头填写，长度不能超过100个字符。"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="right">虚拟目录：</td>
    <td>
	<asp:TextBox ID="txtWebPath" runat="server" CssClass="input required" size="25" 
            maxlength="20" HintTitle="网站的虚拟目录" 
            HintInfo="请填写网站的虚拟目录名称，如果放在根目录下，输入“/”；如：http://abc.com/web，输入“web”。"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="right">日志目录：</td>
    <td>
	<asp:TextBox ID="txtWebLogPath" runat="server" CssClass="input required" size="25" 
            maxlength="20" HintTitle="网站的日志目录" 
            HintInfo="请输入目录存在的物理路径，如“：D:\LogFile\”，且该目录必须开放对网站用户可写权限，否则产生错误。为了系统的安全，请不要将该目录存放在网站下面。"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="right">办公电话：</td>
    <td>
	<asp:TextBox ID="txtWebTel" runat="server" CssClass="input required" size="25" 
            maxlength="50" HintTitle="办公电话号码" HintInfo="格式如：0757-22228888。"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="right">传真号码：</td>
    <td><asp:TextBox ID="txtWebFax" runat="server" CssClass="input" size="25" 
            maxlength="50" HintTitle="传真号码" HintInfo="格式如：0757-22228888。"></asp:TextBox></td>
  </tr>
    <tr>
    <td align="right">手机号码：</td>
    <td><asp:TextBox ID="txtTell" runat="server" CssClass="input" size="25" 
            maxlength="50" HintTitle="手机号码" HintInfo="格式如：15170499579。"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">管理员信箱：<br /></td>
    <td><asp:TextBox ID="txtWebEmail" runat="server" CssClass="input" size="25" 
            maxlength="50" HintTitle="网站管理员信箱" HintInfo="方便客户需要咨询时发送邮件。"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">网站备案号：</td>
    <td><asp:TextBox ID="txtWebCrod" runat="server" CssClass="input" size="25" 
            maxlength="50" HintTitle="网站备案号码" HintInfo="信息产业部申请的合法TCP/IP备案号。"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">联系地址：</td>
    <td><asp:TextBox ID="txtAddress" runat="server" CssClass="input" size="48" 
            maxlength="100" HintTitle="联系地址" HintInfo="格式如：南昌市高新区火炬大道948号506室"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">系统版权信息：<br />
      （支持HTML）</td>
    <td>
      <asp:TextBox ID="txtWebCopyright" runat="server" TextMode="MultiLine" 
            CssClass="textarea" style="width:300px;height:80px;" HintTitle="网站版权信息" 
            HintInfo="该信息将会显示在网站的底部，支持HTML标志填写，所以请认真填写。"></asp:TextBox>
    </td>
  </tr>
</tbody>
</table>
<div class="spClear"></div>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
<tbody>
  <tr>
    <th colspan="2" align="left"><strong>系统参数设置</strong></th>
  </tr>
    <tr>
    <td width="25%" align="right">产品分页数量：</td>
    <td width="75%">
        <asp:TextBox ID="txtWebProSize" runat="server" 
            CssClass="input required digits" HintTitle="产品每页显示数量" HintInfo="纯数字，网站前台产品每页显示的数量。" 
            maxlength="9" size="3"></asp:TextBox>
&nbsp;条</td>
  </tr>
  <tr>
    <td align="right">新闻分页数量：</td>
    <td>
        <asp:TextBox ID="txtWebNewsSize" runat="server" 
            CssClass="input required digits" HintTitle="新闻每页显示数量" HintInfo="纯数字，网站前台新闻每页显示的数量。" 
            maxlength="9" size="3"></asp:TextBox>
&nbsp;条</td>
  </tr>
  <tr>
    <td align="right">网站关健字：</td>
    <td>
      <asp:TextBox ID="txtWebKeywords" runat="server" CssClass="input" style="width:300px;" maxlength="250" HintTitle="网站关健字" HintInfo="搜索引擎可根据网站设置的关健字，以“,”号分隔开。"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="right">网站描述：</td>
    <td>
      <asp:TextBox ID="txtWebDescription" runat="server" CssClass="textarea" style="width:300px;height:45px;" maxlength="250" HintTitle="网站描述" HintInfo="搜索引擎可根据网站设置的描述信息，字符小于等于250位字符。" 
            TextMode="MultiLine"></asp:TextBox>
        &nbsp;</td>
  </tr>
  <tr>
    <td align="right">管理日志：</td>
    <td>
        <asp:RadioButtonList ID="rblWebLogStatus" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">开启</asp:ListItem>
            <asp:ListItem Value="0">关闭</asp:ListItem>
        </asp:RadioButtonList>
        </td>
  </tr>
    <tr>
    <td align="right">脏话过滤：</td>
    <td>
      <asp:TextBox ID="txtWebKillKeywords" runat="server" CssClass="textarea" 
            style="width:300px;height:45px;" maxlength="255" HintTitle="脏话过滤" HintInfo="以“|”号分开，如：我操|我日|妈比，设置后对网站内容进行过滤。" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
  </tr>
</tbody>
</table>
<div class="spClear"></div>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
<tbody>
  <tr>
    <th colspan="2" align="left"><strong>留言审核设置</strong></th>
  </tr>
    <tr>
    <td width="25%" align="right">留言审核：</td>
    <td width="75%">
        <asp:RadioButtonList ID="messlist" runat="server" RepeatDirection="Horizontal" 
            RepeatLayout="Flow">
            <asp:ListItem Value="0">开通</asp:ListItem>
            <asp:ListItem Value="1">取消</asp:ListItem>
        </asp:RadioButtonList>
                </td>
  </tr>
</tbody>
</table>
<div class="spClear"></div>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
<tbody>
  <tr>
    <th colspan="2" align="left"><strong>附件参数设置</strong></th>
  </tr>
  <tr>
    <td width="25%" align="right">文件上传目录：</td>
    <td width="75%">
        <asp:TextBox ID="txtWebFilePath" runat="server" CssClass="input required" size="50" 
            maxlength="255" HintTitle="文件上传目录" HintInfo="上传图片或附件的目录，该目录将会自动创建在网站根目录下，如：“UploadFiles”。"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td align="right">允许文件上传类型：</td>
    <td>
        <asp:TextBox ID="txtWebFileType" runat="server" CssClass="input required" size="50" maxlength="255" HintTitle="允许上传文件扩展名" HintInfo="上传图片或附件时用于检测，以英文的|号分隔开，如：“jpg|gif|rar”。"></asp:TextBox>
                </td>
  </tr>
  <tr>
    <td align="right">允许文件上传大小：</td>
    <td>
        <asp:TextBox ID="txtWebFileSize" runat="server" CssClass="input required digits" maxlength="9" size="5" HintTitle="允许上传文件大小" HintInfo="整数，如果超过设置的大小将不给予上传。"></asp:TextBox>
&nbsp;KB </td>
  </tr>
  <tr>
    <td align="right">是否生成产品缩略图：</td>
    <td>
        <asp:RadioButtonList ID="rblIsThumbnail" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="0"> 是</asp:ListItem>
            <asp:ListItem Value="1"> 否</asp:ListItem>
        </asp:RadioButtonList>
    </td>
  </tr>
  <tr>
    <td align="right">产品缩略图大小：</td>
    <td>
        宽：<asp:TextBox ID="txtProWidth" CssClass="input required digits" size="2" runat="server" maxlength="9"></asp:TextBox>
        &nbsp;高：<asp:TextBox ID="txtProHight" CssClass="input required digits" size="2" runat="server" maxlength="9"></asp:TextBox> &nbsp;（单位：像素）
    </td>
  </tr>
  <tr>
    <td align="right">是否开启水印：</td>
    <td>
        <asp:RadioButtonList ID="rblIsWatermark" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="0">关闭水印 </asp:ListItem>
            <asp:ListItem Value="1">文字水印 </asp:ListItem>
            <asp:ListItem Value="2">图片水印 </asp:ListItem>
        </asp:RadioButtonList>
    </td>
  </tr>
  <tr>
    <td align="right">图片水印位置：</td>
    <td>
        <asp:RadioButtonList ID="rblWatermarkStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">左上 </asp:ListItem>
            <asp:ListItem Value="2">中上 </asp:ListItem>
            <asp:ListItem Value="3">右上 </asp:ListItem>
            <asp:ListItem Value="4">左中 </asp:ListItem>
            <asp:ListItem Value="5">居中 </asp:ListItem>
            <asp:ListItem Value="6">右中 </asp:ListItem>
            <asp:ListItem Value="7">左下 </asp:ListItem>
            <asp:ListItem Value="8">中下 </asp:ListItem>
            <asp:ListItem Value="9">右下 </asp:ListItem>
        </asp:RadioButtonList>
    </td>
  </tr>
  <tr>
    <td align="right">图片生成质量：</td>
    <td><asp:TextBox ID="txtImgQuality" CssClass="input required digits" size="2" runat="server" maxlength="9" HintTitle="上传图片生成的质量" HintInfo="整数，取值范围 0-100，0质量最低，100质量最高，默认80"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">图片型水印文件：</td>
    <td><asp:TextBox ID="txtImgWaterPath" runat="server" CssClass="input required" size="50" maxlength="255" HintTitle="图片型水印文件路径" HintInfo="必须为网站根目录下的PNG图片文件，默认为：watermark.png"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">图片水印透明度：</td>
    <td><asp:TextBox ID="txtImgWaterTransparency" CssClass="input required digits" size="2" runat="server" maxlength="9" HintTitle="图片水印的透明度" HintInfo="整数，取值范围1--10 (10为不透明)"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">文字水印内容：</td>
    <td><asp:TextBox ID="txtWaterText" runat="server" CssClass="input" size="50" maxlength="250" HintTitle="文字水印内容" HintInfo="可输入中英文，字符长度250以内，如“本图片出自xxx”。"></asp:TextBox></td>
  </tr>
  <tr>
    <td align="right">文字水印设置：</td>
    <td>字体：<asp:DropDownList ID="ddlWaterFont" runat="server">
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
		<asp:ListItem Value="Tahoma" selected="selected">Tahoma</asp:ListItem>
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
        &nbsp;大小：<asp:TextBox ID="txtFontSize" CssClass="input required digits" size="2" runat="server" maxlength="9"></asp:TextBox>（单位：像素）</td>
  </tr>
  </tbody>
</table>

<div style="margin-top:10px;text-align:center;">
  <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" 
        onclick="btnSave_Click" />
  &nbsp;
  <input name="重置" type="reset" class="submit" value="重置" />
</div>
</form>
</body>
</html>
