<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="studentMangage.aspx.cs" Inherits="ASP.NET_Work.studentMangage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>学生管理</title>
    <link rel="stylesheet" href="css/style.css" />
    <script  type="text/javascript" src="js/jquery-3.4.0.min.js"></script>
    <script type="text/javascript" src="js/fun.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="姓名："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Text_XM" runat="server"></asp:TextBox>
                        <input id="Button_Validate" type="button" value="验证" onclick="button_ValidXs()" />
                    </td>
                    <td>
                        <asp:Button ID="Button_AddXs" runat="server" Text="录入" OnClick="Button_AddXs_Click" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="性别："></asp:Label> 
                    </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList_XB" runat="server" Width="216px" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="True">男</asp:ListItem>
                            <asp:ListItem Value="False">女</asp:ListItem>
                        </asp:RadioButtonList> 
                    </td>
                    <td>
                        <asp:Button ID="Button_DelXs" runat="server" Text="删除" OnClick="Button_DelXs_Click" />
                    </td>
                </tr>
                <tr valign="top">
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="出生日期："></asp:Label> 
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_CSSJ" runat="server">2019-04-12</asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Button_UptXs" runat="server" Text="更新" OnClick="Button_UptXs_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="照片："></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload_Path" runat="server" Width="273px" />
                    </td>
                </tr>
                <tr valign="bottom">
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="已修改课程数："></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_KCS" runat="server" Width="30px" style="text-align: center" Enabled="false">0</asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Image ID="Image_ZP" runat="server" Height="120px" Width="90px" BorderStyle="Solid" BorderWidth="1px" />
                    </td>
                    <td>
                        <asp:Button ID="Button_QueXs" runat="server" Text="查询" OnClick="Button_QueXs_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Labe_MSG" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
