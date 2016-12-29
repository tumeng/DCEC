<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="relogin.aspx.cs" Inherits="Rmes.WebApp.Rmes.Login.relogin" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; height: 400px;">
        <div style="width: 400px; height: 300px; margin: 40px auto auto auto; border: 0px solid black">
        <form action="relogin.aspx" method="post">
            <table>
            <tr><td colspan="2">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="您无法访问该页面，可能登录超时，请重新登录。" Font-Names="微软雅黑" Font-Size="Medium" ForeColor="Red">
                </dx:ASPxLabel>
            </td></tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="用户名" Font-Names="微软雅黑" Font-Size="Larger">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtUserName" runat="server" Width="210px" Font-Names="微软雅黑"  Font-Size="Large">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="密码" Font-Names="微软雅黑" Font-Size="Larger">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPass" Password="true" runat="server" Width="210px" Font-Names="微软雅黑" Font-Size="Medium">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnOk" runat="server" Text="登录" Width="210px" 
                            Font-Names="微软雅黑" Font-Size="Larger" onclick="btnOk_Click1">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
</asp:Content>
