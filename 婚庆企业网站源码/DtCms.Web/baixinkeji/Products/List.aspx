<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="DtCms.Web.Admin.Products.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>产品管理</title>
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
		    link_to:"?<%=CombUrlTxt(this.classId, this.keywords, this.property) %>page=__id__"
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
    <div class="navigation"><span class="add"><a href="Add.aspx">发布业务</a></span><b>您当前的位置：首页 &gt; 业务管理 &gt; 业务列表</b></div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" align="center">请选择：</td>
        <td>
            <asp:DropDownList ID="ddlClassId" runat="server" CssClass="select" 
                AutoPostBack="True" onselectedindexchanged="ddlClassId_SelectedIndexChanged"></asp:DropDownList>&nbsp;
            <asp:DropDownList ID="ddlProperty" runat="server" CssClass="select" 
                AutoPostBack="True" onselectedindexchanged="ddlProperty_SelectedIndexChanged">
                <asp:ListItem Value="">所有属性</asp:ListItem>
                <asp:ListItem Value="isLock">不显示</asp:ListItem>
                <asp:ListItem Value="isMsg">评论</asp:ListItem>
                <asp:ListItem Value="isTop">置顶</asp:ListItem>
                <asp:ListItem Value="isRed">推荐</asp:ListItem>
                <asp:ListItem Value="isHot">热门</asp:ListItem>
                <asp:ListItem Value="isSlide">幻灯片</asp:ListItem>
            </asp:DropDownList>
            &nbsp;
            <asp:ImageButton ID="ibtnViewTxt" runat="server" 
                ImageUrl="~/baixinkeji/Images/txt_Show.gif" onclick="ibtnViewTxt_Click" 
                ToolTip="文字列表视图" />&nbsp;<asp:ImageButton ID="ibtnViewImg" runat="server" 
                ImageUrl="~/baixinkeji/Images/img_Show.gif" onclick="ibtnViewImg_Click" 
                ToolTip="图像列表视图" />
        </td>
        <td width="50" align="right">关健字：</td>
        <td width="150"><asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword"></asp:TextBox></td>
        <td width="60" align="center"><asp:Button ID="btnSelect" runat="server" Text="查询" 
                CssClass="submit" onclick="btnSelect_Click" /></td>
        </tr>
    </table>
    <div class="spClear"></div>

    <!--列表展示开始-->
    <asp:Repeater ID="rptList1" runat="server" onitemcommand="rptList_ItemCommand">
    <HeaderTemplate>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="6%">选择</th>
        <th width="6%">编号</th>
        <th align="left">业务标题</th>
        <th width="13%">所属类别</th>
        <th width="16%">发布时间</th>
        <th width="110">属性</th>
        <th width="8%">操作</th>
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" /></td>
        <td align="center"><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>'></asp:Label></td>
        <td><%# Convert.ToInt32(Eval("IsLock")) == 1 ? "<img src=\"../Images/wsh01.gif\" title=\"不显示\" />" : ""%> <a href="Edit.aspx?id=<%#Eval("Id") %>"><%#Eval("Title")%></a></td>
        <td align="center"><%# new DtCms.BLL.Channel().GetChannelTitle(Convert.ToInt32(Eval("ClassId")))%></td>
        <td><%#string.Format("{0:g}",Eval("AddTime"))%></td>
        <td>
          <asp:ImageButton ID="ibtnMsg" CommandName="ibtnMsg" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsMsg")) == 1 ? "../Images/ico-0.png" : "../Images/ico-0_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsMsg")) == 1 ? "取消评论" : "设置评论"%>' />
          <asp:ImageButton ID="ibtnTop" CommandName="ibtnTop" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsTop")) == 1 ? "../Images/ico-1.png" : "../Images/ico-1_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsTop")) == 1 ? "取消置顶" : "设置置顶"%>' />
          <asp:ImageButton ID="ibtnRed" CommandName="ibtnRed" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsRed")) == 1 ? "../Images/ico-2.png" : "../Images/ico-2_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsRed")) == 1 ? "取消推荐" : "设置推荐"%>' />
          <asp:ImageButton ID="ibtnHot" CommandName="ibtnHot" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsHot")) == 1 ? "../Images/ico-3.png" : "../Images/ico-3_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsHot")) == 1 ? "取消热门" : "设置热门"%>' />
          <asp:ImageButton ID="ibtnSlide" CommandName="ibtnSlide" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsSlide")) == 1 ? "../Images/ico-4.png" : "../Images/ico-4_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsSlide")) == 1 ? "取消幻灯片" : "设置幻灯片"%>' />
        </td>
        <td align="center"><span><a href="edit.aspx?id=<%#Eval("Id") %>">修改</a></span></td>
      </tr>
      </ItemTemplate>
      <FooterTemplate>
      </table>
    </FooterTemplate>
    </asp:Repeater>
    <!--列表展示结束-->
    
    <!--图片展示开始-->
    <asp:Repeater ID="rptList2" runat="server" onitemcommand="rptList_ItemCommand">
    <HeaderTemplate>
    <div class="pro_img_list">
      <ul>
      </HeaderTemplate>
        <ItemTemplate>
        <li>
          <div class="nTitle"><asp:CheckBox ID="cb_id" CssClass="checkall" runat="server" style="vertical-align:middle;" /><asp:Label ID="lb_id" runat="server" Text='<%#Eval("Id")%>' style="display:none;"></asp:Label><%#DtCms.Common.StringPlus.CutString(Eval("Xinghao").ToString(), 14)%></div>
          <div class="nImg"><a title="<%#Eval("Title")%>" href="Edit.aspx?id=<%#Eval("Id") %>"><img src="/Tools/Http_ImgLoad.ashx?w=120&h=120&gurl=<%#Eval("ImgUrl") %>" /></a></div>
          <div class="nBtm">
            <span class="left">
              <asp:ImageButton ID="ibtnMsg" CommandName="ibtnMsg" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsMsg")) == 1 ? "../Images/ico-0.png" : "../Images/ico-0_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsMsg")) == 1 ? "取消评论" : "设置评论"%>' />
              <asp:ImageButton ID="ibtnTop" CommandName="ibtnTop" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsTop")) == 1 ? "../Images/ico-1.png" : "../Images/ico-1_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsTop")) == 1 ? "取消置顶" : "设置置顶"%>' />
              <asp:ImageButton ID="ibtnRed" CommandName="ibtnRed" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsRed")) == 1 ? "../Images/ico-2.png" : "../Images/ico-2_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsRed")) == 1 ? "取消推荐" : "设置推荐"%>' />
              <asp:ImageButton ID="ibtnHot" CommandName="ibtnHot" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsHot")) == 1 ? "../Images/ico-3.png" : "../Images/ico-3_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsHot")) == 1 ? "取消热门" : "设置热门"%>' />
              <asp:ImageButton ID="ibtnSlide" CommandName="ibtnSlide" runat="server" ImageUrl='<%# Convert.ToInt32(Eval("IsSlide")) == 1 ? "../Images/ico-4.png" : "../Images/ico-4_.png"%>' ToolTip='<%# Convert.ToInt32(Eval("IsSlide")) == 1 ? "取消幻灯片" : "设置幻灯片"%>' />
            </span>
            <span class="right">
              <a href="Edit.aspx?id=<%#Eval("Id") %>"><img src="../Images/ico-6.png" title="修改" /></a>
            </span>
          </div>
        </li>
        </ItemTemplate>
      <FooterTemplate>
      </ul>
    </div> 
    </FooterTemplate>
    </asp:Repeater>
    <!--图片展示结束-->
    
    <div class="spClear"></div>
        <div style="line-height:30px;height:30px;">
            <div id="Pagination" class="right flickr"></div>
            <div class="left">
                <span class="btn_all" onclick="checkAll(this);">全选</span>
                <span class="btn_bg">
                  <asp:LinkButton ID="lbtnDel" runat="server" 
                    OnClientClick="return confirm( '确定要删除这些记录吗？ ');" onclick="lbtnDel_Click">删 除</asp:LinkButton>
                </span>
            </div>
	</div>
    </form>
</body>
</html>
