<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Top.ascx.cs" Inherits="DtCms.Web.Top" %>
 <script>
function topshow()
{
    if(document.getElementById("topbox").style.display=="none"||document.getElementById("topbox").style.display=="")
    {
         $("#topbox").slideDown("slow");;

    }else{
         $("#topbox").slideUp(1000);
    }
}
 </script>
<div id="topbox">
    <div id="top">
     <div class="topleft"><h1>服务项目<span>SERVICE PROJECT</span></h1></div>
     <div class="topnav">
          <ul>
               <li>婚礼主持<span>master of ceremony</span></li>
               <li>婚礼布置<span>wedding decoration</span></li>
               <li>主题婚礼<span>master of ceremony</span></li>
               <li class="creative">创意设计<span>Creative design</span></li>
               <li class="creative">摄影摄像<span>Photographic camera</span></li>
               <li>婚礼车队<span>Wedding car</span></li>
          </ul>
          <div class="clear"></div>
     </div>
     <div class="clear"></div>
     </div>
</div>
<div id="containerbody">
       <div class="servicebtnbox">
           <div class="servicetop"></div>
           <div class="openTop"><span><a href="#" onclick="topshow()"><img src="images\service_btnbg.jpg" /></a></span></div>
       </div>
       <div id="header">
            <div class="logocontainer">
                <div class="logo"><img src="images\logo.jpg" /></div>
                <div class="searchcon">
                     <div class="profession"><img src="images\profession_bg.jpg" /></div>
                     <div class="searchform">
                          <div class="searchinput"><asp:TextBox ID="titletxt" runat="server"></asp:TextBox></div>
                          <asp:Button ID="Button1" class="searchbtn" runat="server" Text="search" 
                              onclick="Button1_Click" />
                          <div class="clear"></div>
                     </div>
                     <div class="clear"></div>
                
                
                </div>
                <div class="clear"></div>
            </div>
            <div class="navcontainer">
                   <div class="nav" id="lavalampnav">
                        <ul>
                            <%=top1 %><a href="Index.aspx"><span>首页</span>HOME</a></li>                            <%=top2 %><a href="Product.aspx?top=2"><span>案例展示</span>SHOWCASE</a></li>                            <%=top3 %><a href="Price.aspx?top=3"><span>套系价格</span>PACKAGES</a></li>                            <%=top4 %><a href="Team.aspx?top=4"><span>服务团队</span>TEAM</a></li>                            <%=top5 %><a href="Company.aspx?Id=6&top=5"><span>关于我们</span>ABOUT US</a></li>                            <%=top6 %><a href="Company1.aspx?Id=3&top=6"><span>联系我们</span>CONTACT US</a></li>                            <%=top7 %><a href="Video.aspx?top=7"><span>结婚视频</span>VIDEO</a></li>
                            <div class="clear"></div>
                        </ul>
                   </div>
            </div>
       </div>