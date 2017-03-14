<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="DtCms.Web.Footer" %>
<div id="footercon">
    <div id="footer">
         <div class="copyrightcon">
                <div class="copyright">
                      <%=webset.WebCopyright%>
                </div>
                <div class="clear"></div>
         </div> 
         <div class="friendlink">
                  友情连接：<%=DtCms.ActionLabel.Links.ViewImgList(10, "IsRed=1 and IsImage=0 and IsLock=0", "SortId asc,AddTime desc") %>
         </div>
    </div>
</div>