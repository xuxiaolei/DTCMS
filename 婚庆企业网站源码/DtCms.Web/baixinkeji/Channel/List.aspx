<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.Channel.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>类别管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript">
        $(function() {
            $(".msgtable tr:nth-child(odd)").addClass("tr_bg"); //隔行变色
            $(".msgtable tr").hover(
			    function() {
			        $(this).addClass("tr_hover_col");
			    },
			    function() {
			        $(this).removeClass("tr_hover_col");
			    }
		    );
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
        <div class="navigation"><span class="add"><a href="Add.aspx?kindId=<%=kindId %>">增加顶级类别</a></span><b>您当前的位置：首页 &gt; 类别管理 &gt; 类别列表</b></div>
        <div style="padding-bottom:10px;"></div>

        <asp:Repeater ID="rptClassList" runat="server" 
            onitemcommand="rptClassList_ItemCommand" 
            onitemdatabound="rptClassList_ItemDataBound">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
                    <tr>
                        <th width="7%">编号</th>
                        <th align="left"">类别名称</th>
                        <th width="90">优先级别</th>
                        <th width="150">管理操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td align="center">
                          <asp:HiddenField ID="txtClassId" runat="server" Value='<%#Eval("Id") %>' />
                          <asp:HiddenField ID="txtClassLayer" runat="server" Value='<%#Eval("ClassLayer") %>' />
                          <%# Eval("ID") %>
                        </td>
                        <td>
                          <asp:Literal ID="LitFirst" runat="server"></asp:Literal>
                          <%# Eval("Title") %>
                        </td>
                        <td align="center"><%# Eval("ClassOrder") %></td>
                        <td align="left">
                            <span><a href="Add.aspx?kindId=<%=kindId %>&classId=<%# Eval("Id") %>">添加子类</a></span>
                            <span><a href="<%# Eval("PageUrl") %>?classId=<%# Eval("Id") %>">管理</a></span>
                            <span><a href="Edit.aspx?kindId=<%=kindId %>&classId=<%# Eval("Id") %>">修改</a></span>
                            <span><asp:LinkButton ID="lbDel" CommandName="btndel" runat="server" OnClientClick="return confirm( '该操作会删除本类别和下属类别，确定要删除吗？ ');">删除</asp:LinkButton></span>
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
 
     </form>
</body>
</html>
