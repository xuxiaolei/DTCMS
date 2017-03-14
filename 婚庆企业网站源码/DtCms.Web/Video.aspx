<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Video.aspx.cs" Inherits="DtCms.Web.Video" %>
<%@ Register src="Top.ascx" tagname="top" tagprefix="DtCmsControl" %>
<%@ Register src="Footer.ascx" tagname="footer" tagprefix="DtCmsControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>结婚视频</title>
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link type="text/css" href="css\justlove.css" rel="stylesheet" />
    <script type="text/javascript" src="Js\swfobject.js"></script>
    <script src="Js/jquery-1.3.2.min.js" type="text/javascript"></script>
</head>

<body>
<form id="Form1" runat="server">

<!--Head.Start-->
<DtCmsControl:top ID="Header" runat="server" />
<!--Head.End-->
        <div id="content">
             <div class="showcase">
                   <div class="showtitle">
                       <img src="images\contact_videobg.jpg" />
                   </div>
                   <div class="contactbox fr showcontact">
                     <div class="telphone"></div>
                     <ul> 
                         <li class="msn"><span>MSN: </span></li>
                         <li class="qq"><span>QQ:<a href="#"></a>&nbsp;&nbsp;<a href="#"></a></span></li>
                     </ul>
                </div>
                <div class="clear"></div>
             </div>
             <div class="showcontent">
             <div class="borderdot"></div>
                <div style="text-align:center;font-size:13px;font-weight:bold;"><%=title%></div>
                <div style="height:15px;"></div>
                <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" width="800" height="350">
                <param name="allowScriptAccess" value="sameDomain">
                <param name="movie" value="baixinkeji/Article/<%=path %>">
                <param name="quality" value="high">
                <param name="bgcolor" value="#ffffff">
                <embed src="baixinkeji/Article/<%=path %>" quality="high" bgcolor="#ffffff" width="800" height="350" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
                </object>
                <div>
                   <ul class="showlist"  id="video">                       <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>                            <li><a href="Video.aspx?top=7&id=<%#Eval("ID")%>"><%#Eval("Title") %></a></li>                        </ItemTemplate>                        </asp:Repeater>
                   </ul>
                </div>
             </div>
       </div>

<!--footer Start-->
<DtCmsControl:footer ID="Footer" runat="server" />
<!--footer End-->
</div>
</form>
</body>
</html>
