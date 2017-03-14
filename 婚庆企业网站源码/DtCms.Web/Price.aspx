<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Price.aspx.cs" Inherits="DtCms.Web.Price" %>
<%@ Register src="Top.ascx" tagname="top" tagprefix="DtCmsControl" %>
<%@ Register src="Footer.ascx" tagname="footer" tagprefix="DtCmsControl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>套系价格</title>
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
                       <img src="images\packages_bg.jpg" />
                       <ul class="packageslist">
                           <asp:Repeater ID="Repeater2" runat="server">
                           <ItemTemplate>
                           <li><a href="#title<%#Eval("ID")%>"><%#Eval("Title")%></a></li>
                           </ItemTemplate>
                           </asp:Repeater>
                       <div class="clear"></div>
                       </ul>
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
                 <asp:Repeater ID="Repeater1" runat="server">
                   <ItemTemplate>
                         <div class="caseeg">                               <div class="casetitle">                                    <p class="fl casetitlelink"><a href="#" name="title<%#Eval("ID")%>"><%#Eval("Title")%></a></p>                                    <a href="#" class="fr">婚礼服务项目优惠套餐</a>                                    <div class="clear"></div>                               </div>                               <img src="<%#Eval("ImgUrl")%>" />                               <p class="packtxt"><%#Eval("Content")%></p>                               <div class="borderdot"></div>                        </div>
                   </ItemTemplate>
                 </asp:Repeater>
             </div>
       </div>

<!--footer Start-->
<DtCmsControl:footer ID="Footer" runat="server" />
<!--footer End-->
</div>
</form>
</body>
</html>
