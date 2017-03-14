<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Index.aspx.cs" Inherits="DtCms.Web.Admin.admin_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=webset.WebName.ToString()%> - 后台管理</title>
    <link href="../css/custom-theme/jquery-ui-1.7.2.custom.css" rel="Stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="images/style.css">
    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-1.7.2.custom.min.js"></script>
    <script type="text/javascript" src="js/manager.js"></script>
</head>
<body>
<form id="form1" runat="server">

<table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%" style="background:#EBF5FC;">
<tbody>
  <tr>
    <td height="70" colspan="3" style="background:url(images/head_bg.gif);"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="24%" height="70"><img src="images/head_logo.png" width="233" height="50"></td>
        <td width="76%" valign="bottom">
	  <!--导航菜单,与下面的相关联,修改时注意参数-->
          <div id="tabs">
          <ul>
			<li onclick="tabs(1);"><a href="Article/List.aspx" target="sysMain"><span>视频管理</span></a></li>
			<li onclick="tabs(2);"><a href="Products/List.aspx" target="sysMain"><span>业务管理</span></a></li>
			<li onclick="tabs(3);"><a href="Contents/List.aspx" target="sysMain"><span>内容管理</span></a></li>
			<li onclick="tabs(4);"><a href="Links/List.aspx" target="sysMain"><span>友情链接</span></a></li>
			<li onclick="tabs(5);"><a href="Admin_center.aspx" target="sysMain"><span>系统管理</span></a></li>
          </ul>
          </div>

        </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td height="30" colspan="3" style="padding:0px 10px;font-size:12px;background:url(images/navsub_bg.gif) repeat-x;">
    <div style="float:right;line-height:20px;"><a href="admin_center.aspx" target="sysMain">管理中心</a> | 
        <a target="_blank" href="../">预览网站</a> | 
        <asp:LinkButton 
            ID="lbtnExit" runat="server" onclick="lbtnExit_Click">安全退出</asp:LinkButton>
        </div>
    <div style="padding-left:20px;line-height:20px;background:url(images/siteico.gif) 0px 0px no-repeat;">当前登录用户：<font color="#FF0000"><asp:Label
        ID="lblAdminName" runat="server" Text="Label"></asp:Label></font>您好，欢迎光临。</div>
    </td>
  </tr>

  <tr>
    <td align="middle" id="mainLeft" valign="top" style="background:#FFF;">
	  <div style="text-align:left;width:185px;height:100%;font-size:12px;">
        <!--导航顶部-->
        <div style="padding-left:10px;height:29px;line-height:29px;background:url(images/menu_bg.gif) no-repeat;">
          <span style="padding-left:15px;font-weight:bold;color:#039;background:url(images/menu_dot.gif) no-repeat;">功能导航</span>
        </div>
        <!--导航菜单,修改时注意顺序-->
        <div class="left_menu">
          <ul>
            <li onclick="tabs(5);"><a href="Config/Admin_config.aspx" target="sysMain">系统设置</a></li>
            <li onclick="tabs(1);"><a href="Article/List.aspx" target="sysMain">资讯管理</a></li>
            <li onclick="tabs(2);"><a href="Products/List.aspx" target="sysMain">产品管理</a></li>
            <li onclick="tabs(4);"><a href="Feedback/List.aspx" target="sysMain">留言管理</a></li>
          </ul>
        </div>
        
        <div class="left_menu">
          <ul>
            <li><a href="Article/Add.aspx" target="sysMain">发布视频</a></li>
            <li><a href="Article/List.aspx" target="sysMain">视频管理</a></li>
          </ul>
        </div>
        
        <div class="left_menu">
          <ul>
            <li><a href="Products/Add.aspx" target="sysMain">发布业务</a></li>
            <li><a href="Products/List.aspx" target="sysMain">业务管理</a></li>
          </ul>
          <ul>
            <li><a href="Channel/Add.aspx?kindId=2" target="sysMain">增加类别</a></li>
            <li><a href="Channel/List.aspx?kindId=2" target="sysMain">类别管理</a></li>
          </ul>
        </div>
        
        <div class="left_menu">
          <ul>
            <li><a href="Contents/Add.aspx" target="sysMain">增加内容</a></li>
            <li><a href="Contents/List.aspx" target="sysMain">内容管理</a></li>
          </ul>
          <ul>
            <li><a href="Channel/Add.aspx?kindId=3" target="sysMain">增加类别</a></li>
            <li><a href="Channel/List.aspx?kindId=3" target="sysMain">类别管理</a></li>
          </ul>
        </div>
        
        <div class="left_menu">
          <ul>
            <li><a href="Links/List.aspx" target="sysMain">链接管理</a></li>
          </ul>
        </div>

        <div class="left_menu">
          <ul>
            <li><a target="sysMain" href="Config/Admin_config.aspx">系统参数设置</a></li>
            <li><a target="sysMain" href="Advertising/BarList.aspx?Pid=1">首页Flash设置</a></li>
            <li><a target="sysMain" href="Manage/List.aspx">管理员管理</a></li>
          </ul>
        </div>
        
      </div>
	</td>
	<td valign="middle" style="width:8px;background:url(images/main_cen_bg.gif) repeat-x;">
      <div id="sysBar" style="cursor:pointer;"><img id="barImg" src="images/butClose.gif" alt="关闭/打开左栏" /></div>
	</td>
	<td style="width:100%" valign="top">
      <iframe frameborder="0" id="sysMain" name="sysMain" scrolling="yes" src="admin_center.aspx" style="height:100%;visibility:inherit; width:100%;z-index:1;"></iframe>
	</td>
  </tr>
  <tr>
    <td height="28" colspan="3" bgcolor="#EBF5FC" style="padding:0px 10px;font-size:10px;color:#2C89AD;background:url(images/foot_bg.gif) repeat-x;"><%=webset.WebCopyright.ToString()%></td>
  </tr>
  </tbody>
</table>

</form>
</body>
</html>
