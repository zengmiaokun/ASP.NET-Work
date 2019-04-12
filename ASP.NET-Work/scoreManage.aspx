<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scoreManage.aspx.cs" Inherits="ASP.NET_Work.scoreManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>成绩管理</title>
</head>
<body bgcolor="D9DFAA">
    <form id="form1" runat="server">
        <div>
            <table>
	<tr>
		<td>
            <asp:Label ID="Label1" runat="server" Text="课程名:"></asp:Label>
            <asp:TextBox ID="TextBox_KCM" runat="server" Width="120px"></asp:TextBox>
            &nbsp;
        </td>
		<td>
            <asp:Label ID="Label2" runat="server" Text="姓名："></asp:Label>
            <asp:DropDownList ID="DropDownList_XM" runat="server" Width="80px" DataSourceID="SqlDataSource1" DataTextField="XM" DataValueField="XM" OnSelectedIndexChanged="DropDownList_XM_SelectedIndexChanged" OnTextChanged="DropDownList_XM_TextChanged">
            </asp:DropDownList>
		    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PXSCJConnectionString %>" SelectCommand="SELECT [XM] FROM [XS]"></asp:SqlDataSource>
		    <asp:Button ID="Button_Dn" runat="server" Text="↓" OnClick="Button_Dn_Click" />
            <asp:Button ID="Button_Up" runat="server" Text="↑" OnClick="Button_Up_Click" />
            &nbsp;		    
		</td>
		<td>
            <asp:Label ID="Label3" runat="server" Text="成绩："></asp:Label>
		    <asp:TextBox ID="TextBox_CJ" runat="server" Width="50px" style="text-align: center">0</asp:TextBox>
		</td>
	</tr>
	<tr valign="top">
		<td rowspan="3" colspan="2">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="382px" BorderStyle="Solid" BorderWidth="2px">
                <Columns>
                    <asp:BoundField DataField="XM" HeaderText="姓名" SortExpression="XM">
                    <HeaderStyle BackColor="#999999" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="KCM" HeaderText="课程名" SortExpression="KCM">
                    <HeaderStyle BackColor="#999999" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CJ" HeaderText="成绩" SortExpression="CJ">
                    <HeaderStyle BackColor="#999999" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </td>
		<td align="right">
            <asp:Button ID="Button_AddCj" runat="server" Text="录入" Width="100px" OnClick="Button_AddCj_Click" />
        </td>
	</tr>
	<tr>
		<td align="right">
            <asp:Button ID="Button_DelCj" runat="server" Text="删除" Width="100px" OnClick="Button_DelCj_Click" />
        </td>
	</tr>
	<tr>
		<td align="right">
            <asp:Button ID="Button_QueCj" runat="server" Text="查询" Width="100px" OnClick="Button_QueCj_Click" />
        </td>
	</tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label_MSG" runat="server"></asp:Label>
        </td>
    </tr>
</table>
        </div>
    </form>
</body>
</html>
