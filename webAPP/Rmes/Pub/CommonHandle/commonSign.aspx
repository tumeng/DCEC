<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Pub_CommonHandle_commonSign" Codebehind="commonSign.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ǩ��ͨ�ý���</title>
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
                    <asp:Label ID="Label3" runat="server" Text="�û���" ForeColor="White" Font-Size="Small"></asp:Label></td>
                <td style="width: 70%; text-align: left">
                    <asp:TextBox ID="txtUserCode" runat="server" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 30%; text-align: right">
                    <asp:Label ID="Label1" runat="server" Text="���룺" ForeColor="White" Font-Size="Small"></asp:Label></td>
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
        alert("�û�����Ϊ�գ�");
        return false;
    }
    
    if(document.forms[0]['<%=txtPassOne.ClientID %>'].value=="")
    {
        alert("�������벻��Ϊ�գ�");
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
