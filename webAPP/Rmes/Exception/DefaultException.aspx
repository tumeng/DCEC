<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Exception_DefaultException"  StylesheetTheme="Theme1" Codebehind="DefaultException.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="content.css" type="text/css" rel="stylesheet" /> 
    <title>错误处理</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="color: #ff0066">
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;width:400px" rowspan="3" >
                    <img alt="" src="../Pub/Images/20071208004101231.png" /></td>
                <td style="text-align: center">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;width:30%">
        <asp:Label ID="Label1" runat="server" Text="登录超时，请重新登录." 
            Font-Names="微软雅黑" Font-Size="Large"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center" valign="top">
    <asp:Button ID="Button_Confirm" runat="server" width="100px" OnClientClick="return OpenIndex()" 
                        OnClick="Button_Confirm_Click" Text="确定" Font-Size="Small" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td colspan="2" style="text-align: center">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
function OpenIndex(){
    parent.location.href = '<%=Page.ResolveUrl("~/RmesLogin.aspx")%>';    
}

function CloseRmes(){
    parent.window.close(); 
}
</script>