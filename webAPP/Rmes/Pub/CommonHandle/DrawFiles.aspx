<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Pub_CommonHandle_DrawFiles" Codebehind="DrawFiles.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文件预览</title>
</head>
<body>

    <form id="form1" runat="server">
        <center>
    <div>
        <table width="100%">
            <tr>
                <td>
                </td>
                <td>
        <asp:Label ID="Label1" runat="server" Text="代号："></asp:Label>
        <asp:Label ID="Label2" runat="server"></asp:Label></td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>

        <asp:ListBox ID="ListBox1" runat="server" Width="100%" Height="101px" AutoPostBack="True"></asp:ListBox></td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: right">
                    &nbsp;<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="调阅" /></td>
                <td>
                </td>
            </tr>
        </table>
    </div>
            </center>
    </form>
</body>

</html>
