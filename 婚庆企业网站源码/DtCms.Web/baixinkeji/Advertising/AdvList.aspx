<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvList.aspx.cs" Inherits="DtCms.Web.Admin.Advertising.AdvList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>广告位管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.pagination.js"></script>
    <script type="text/javascript" src="../js/function.js"></script>
    <script type="text/javascript">
        $(function() {
            //分页参数设置
            $("#Pagination").pagination(<%=pcount %>, {
            callback: pageselectCallback,
            prev_text: "« 上一页",
            next_text: "下一页 »",
            items_per_page:<%=pagesize %>,
		    num_display_entries:3,
		    current_page:<%=page %>,
		    num_edge_entries:2,
		    link_to:"?<%=this.CombKeywords(this.keywords, this.property) %>page=__id__"
           });
        });
        
        function pageselectCallback(page_id, jq) {
                //alert(page_id); 回调函数，进一步使用请参阅说明文档
        }
        
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
    <div class="navigation"><span class="add"><a href="AdvAdd.aspx">增加广告位</a></span><b>您当前的位置：管理中心 &gt; 系统管理 &gt; 广告位列表</b></div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" align="center">筛选：</td>
        <td  width="100">
           <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select" 
                AutoPostBack="True" onselectedindexchanged="ddlProperty_SelectedIndexChanged">
                <asp:ListItem Value="">所有类别</asp:ListItem>
                <asp:ListItem Value="1">文字</asp:ListItem>
                <asp:ListItem Value="2">图片</asp:ListItem>
                <asp:ListItem Value="3">幻灯片</asp:ListItem>
                <asp:ListItem Value="4">动画</asp:ListItem>
                <asp:ListItem Value="5">视频</asp:ListItem>
                <asp:ListItem Value="6">代码</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td></td>
        <td width="50" align="right">关健字：</td>
        <td width="150"><asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox></td>
        <td width="60" align="center"><asp:Button ID="btnSelect" runat="server" Text="查询" 
                onclick="btnSelect_Click" CssClass="submit" /></td>
        </tr>
    </table>
    <div class="spClear"></div>
    <asp:Repeater ID="rptList" runat="server" >
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="25%">广告位名称</th>
        <th width="8%">类型</th>
        <th width="8%">数量</th>
        <th width="15%">价格</th>
        <th width="10%">尺寸</th>
        <th width="10%">链接目标</th>
        <th width="18%">管理操作</th>
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /><asp:Label ID="lb_id" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label></td>
        <td><a title="管理该广告位下的广告列表" href="BarList.aspx?Pid=<%#Eval("ID") %>"><%#Eval("Title")%></a></td>
        <td align="center">
        <%#GetTypeName(Eval("AdType").ToString())%>
        </td>
        <td align="center"><%#Eval("AdNum").ToString()%></td>
        <td align="center"><%#Convert.ToDouble(Eval("AdPrice")).ToString("#.##")%> 元/月</td>
        <td align="center"><%#Eval("AdWidth").ToString()%>×<%#Eval("AdHeight").ToString()%></td>
        <td align="center"><%#Eval("AdTarget")%></td>
        <td align="center"><span><a href="BarList.aspx?Pid=<%#Eval("ID") %>">内容管理</a>&nbsp;<a href="AdvView.aspx?id=<%#Eval("ID") %>">调用</a>&nbsp;<a href="AdvEdit.aspx?id=<%#Eval("ID") %>">编辑</a></span></td>
      </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
      </FooterTemplate>
      </asp:Repeater>

    <div class="spClear"></div>
        <div style="line-height:30px;height:30px;">
            <div id="Pagination" class="right flickr"></div>
            <div class="left">
                <span class="btn_all" onclick="checkAll(this);">全选</span>
                <span class="btn_bg">
                    <asp:LinkButton ID="lbtnDel" runat="server" 
                    OnClientClick="return confirm( '此操作将会删除这些广告位及以下的所有广告，确定要删除吗？ ');" 
                    onclick="lbtnDel_Click">删 除</asp:LinkButton>
                </span>
            </div>
	</div>
</form>
</body>
</html>
