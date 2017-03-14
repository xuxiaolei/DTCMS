<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvView.aspx.cs" Inherits="DtCms.Web.Admin.Advertising.AdvView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>调用广告</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript">
        $(function() {
            $("#btnCopy").bind("click", function() {
                window.clipboardData.setData("Text", $("#txtCopyUrl").val());
                alert("已将代码复制至剪切板，请将其贴粘到指定位置即可。");
            });
        });
    </script>
</head>
<body style="padding:10px;">
<form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="AdvList.aspx">返回列表</a></span><b>您当前的位置：管理中心 &gt; 系统管理 &gt; 调用广告</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th colspan="2" align="left">调用广告示例</th>
      </tr>
      <tr>
        <td width="15%" align="right">广告名称：</td>
        <td width="85%"><%=model.Title%></td>
      </tr>
      <tr>
        <td align="right">备注说明：</td>
        <td><%=model.AdRemark%></td>
      </tr>
      <tr>
        <td align="right">广告类型：</td>
        <td><%=GetTypeName(model.AdType.ToString())%></td>
      </tr>
      <tr>
        <td align="right">尺寸大小：</td>
        <td><%=model.AdWidth%>×<%=model.AdHeight%>px</td>
      </tr>
      <tr>
        <td align="right">链接目标：</td>
        <td><%=model.AdTarget%></td>
      </tr>
      <tr>
        <td align="right" valign="top">调用说明：</td>
        <td>
          <div style="color:#060;">
          1、暂停、过期的广告不会在网站上显示；<br />
          2、请确保该站点下的广告所需的/images/Player.swf（FLV插放器插件）、/images/Player.swf（幻灯片插件）的存在；<br />
          3、除广告类型为幻灯片、视频、代码外，如该广告位下存在多条广告时，均以&ltul&gt;&ltli&gt;...&lt/li&gt;&lt/ul&gt;包括进行输出；<br />
          4、广告以JS形式输出，可使用CSS进行控制其样式，前提是您熟悉HTML、DIV、CSS的知识；<br />
          5、了解上述，请将复制下列的代码粘贴于对应的广告位中。
          </div>
        </td>
      </tr>
      <tr>
        <td align="right">复制代码：</td>
        <td>
            <textarea id="txtCopyUrl" class="textarea" style="width:350px;height:45px;vertical-align:middle;"><script type="text/javascript" src="/Tools/Advert_js.ashx?id=<%=model.Id%>"></script></textarea>&nbsp; <input id="btnCopy" type="button" value="复制代码" class="submit" style="vertical-align:middle;" /></td>
      </tr>
    </table>
</form>
</body>
</html>
