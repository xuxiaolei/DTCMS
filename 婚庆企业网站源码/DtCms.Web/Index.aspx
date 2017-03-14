<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DtCms.Web.index"
    EnableViewState="false" %>
<%@ Register Src="Top.ascx" TagName="top" TagPrefix="DtCmsControl" %>
<%@ Register Src="Footer.ascx" TagName="footer" TagPrefix="DtCmsControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=webset.WebName %></title>
    <%=AddMetaInfo(webset.WebKeywords,webset.WebDescription,"") %>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link type="text/css" href="css\justlove.css" rel="stylesheet" />
    <script type="text/javascript" src="Js\swfobject.js"></script>
    <script src="Js/jquery-1.3.2.min.js" type="text/javascript"></script>
</head>
<body>
<form runat="server">

<!--Head.Start-->
<DtCmsControl:top ID="Header" runat="server" />
<!--Head.End-->
<div id="content">
           <div class="flash" id="index_flash">
           </div>
           <script type="text/javascript">
                 var so = new SWFObject("images\\imageshow.swf", "scherer", "970", "346", "9", "", true);
                 so.addParam("wmode", "transparent");
                 so.write("index_flash");
           </script>
           <div class="indexcontent">
                <div class="contactbox fl">
                     <div class="telphone"></div>
                     <ul> 
                         <li class="msn"><span>MSN: </span></li>
                         <li class="qq"><span>QQ:<a href="#"></a>&nbsp;&nbsp;<a href="#"></a></span></li>
                     </ul>
                </div>
                <div class="news">
                      <ul>
                       <asp:Repeater ID="videolist" runat="server">
                        <ItemTemplate>
                           <li><a href="Video.aspx?top=5?id=<%#Eval("Id")%>" target="_blank"><%#Eval("Title")%></a></li>
                        </ItemTemplate>
                        </asp:Repeater>
                      </ul>
                </div>
                <div class="aboutbox">
                     <p><%= DtCms.Common.StringPlus.CutString(model.Content.ToString(), 150)%>..</p>
                </div>
                <div class="clear"></div>
           </div>
       </div>

<!--footer Start-->
<DtCmsControl:footer ID="Footer" runat="server" />
<!--footer End-->
</div>
</form>
</body>
</html>
