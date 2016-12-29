<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Pub_CommonHandle_commonSign" Codebehind="commonSign.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>签字通用界面</title>
</head>
<body onload="document.form1.txtUserCode.focus();">
<center>
    <form id="form1" runat="server">
    
    <div>
    <table style="width: 300px; height:200px; background-image: url(<%=Page.ResolveUrl("~/Rmes/Pub/images/sign_bg.jpg") %>)">
     <tr>
      <td>
        <table  style="width:300px">
            <tr>
                <td style="width: 30%; text-align: right">
                    <asp:Label ID="Label3" runat="server" Text="用户：" ForeColor="White" Font-Size="Small"></asp:Label></td>
                <td style="width: 70%; text-align: left">
                    <asp:TextBox ID="txtUserCode" runat="server" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="密码：" ForeColor="White" Font-Size="Small"></asp:Label></td>
                <td style="width: 70%; text-align: left">
                    <asp:TextBox ID="txtPassOne" runat="server"  TextMode="Password"  Width="150px"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td style="width: 30%; height:40px">
                </td>
                <td style="width: 70%; text-align: left" align="center">
                    &nbsp; &nbsp;
                    <asp:ImageButton ID="btnSure" runat="server" ImageUrl="~/Rmes/Pub/images/yes.jpg" OnClick="btnSure_Click"  OnClientClick="return btnSure_ClientClick()" />
                    &nbsp; &nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Rmes/Pub/images/reset.jpg"  OnClientClick="return btnCancel_onclick()" />
                </td>
            </tr>
        </table>
      </td>
     </tr> 
    </table>
    </div>
    </form>
<script type="text/javascript" language="javascript">


function btnSure_ClientClick()
{
    if(document.forms[0]['<%=txtUserCode.ClientID %>'].value=="")
    {
        alert("用户不能为空！");
        return false;
    }
    
    if(document.forms[0]['<%=txtPassOne.ClientID %>'].value=="")
    {
        alert("密码输入不能为空！");
        return false;
    }
}

function btnCancel_onclick() 
{
       document.forms[0]['<%=txtUserCode.ClientID %>'].value="";  
       document.forms[0]['<%=txtPassOne.ClientID %>'].value="";  
       
       document.forms[0]['<%=txtUserCode.ClientID %>'].focus();       
}
</script>

</center>

</body>
</html>
