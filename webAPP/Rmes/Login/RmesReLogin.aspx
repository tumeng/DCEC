<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Login_RmesReLogin" Title="" Codebehind="RmesReLogin.aspx.cs" %>
 
 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Rmes系统登录</title>
    <script  type="text/javascript" language="javascript" ></script> 
    <style type="text/css">
        .style1
        {
            height: 4px;
            width: 76px;
        }
        .style2
        {
            height: 2px;
            width: 76px;
        }
        .style3
        {
            height: 3px;
            width: 76px;
        }
        .style4
        {
            height: 5px;
            width: 76px;
        }
    </style>
</head>
<body onload="document.form1.TxtEmployeeCode.focus();" style=" background-color:#e5f2ff" >
<center>
    <form id="form1" runat="server">
     <br /><br /><br />

    <table width="600px">
        <tr>
            <td style="width: 100%; height: 367px;background-image: url(<%=Page.ResolveUrl("~/Rmes/Pub/images/Re_Out.png")%>); background-position:center; text-align: center; background-repeat:no-repeat;" align="center">
                <br /><br /><br /><br /><br /><br />
                
      <table style="width:60%; height: 154px; text-align: center;" align="center">
            <tr>
                <td style="background-color:Transparent;" valign="middle" align="left" 
                    class="style1">
                    <asp:Label ID="Label1" runat="server" Text="公司" Width="54px" Font-Size="13pt" ForeColor="White" BackColor="Transparent" Font-Names="微软雅黑"></asp:Label></td>
                <td style="width: 281px; height: 4px; background-color: Transparent; text-align: left;">
                    &nbsp;
                    <asp:DropDownList ID="DropDownListPline" runat="server" DataSourceID="SqlDataSource1"
                        DataTextField="COMPANY_NAME" DataValueField="COMPANY_CODE" Width="225px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="background-color: Transparent;" valign="middle" align="left" 
                    class="style2">
                    <asp:Label ID="Label2" runat="server" Text="用户代码" Width="81px" Font-Size="13pt" 
                        ForeColor="White" BackColor="Transparent" Font-Names="微软雅黑"></asp:Label></td>
                <td style="height: 2px; width: 281px; background-color: Transparent; text-align: left;">
                    &nbsp;
                    <asp:TextBox ID="TxtEmployeeCode" runat="server" Width="185px" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style="background-color: Transparent;" valign="middle" align="left" 
                    class="style3">
                    <asp:Label ID="Label3" runat="server" Text="密码" Width="60px" Font-Size="13pt" ForeColor="White" BackColor="Transparent" Font-Names="微软雅黑"></asp:Label></td>
                <td style="width: 281px; height: 3px; background-color: Transparent; text-align: left;" >
                    &nbsp;
                    <asp:TextBox ID="TxtPassword" runat="server" Width="185px" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="background-color: Transparent" valign="middle" 
                    class="style4">&nbsp;
                    </td>
                <td style="width: 281px; height: 3px; background-color: Transparent; text-align: left;">
                    <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ButtonConfirm" OnClientClick="return GetReturnFlag()"  ToolTip="登录" runat="server" ImageUrl="~/Rmes/Pub/images/logIn.png" Width="50px" ForeColor="Transparent"  />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ButtonCancel" OnClientClick="return ButtonCancel_onclick()"  ToolTip="取消"  runat="server" ImageUrl="~/Rmes/Pub/images/exit.png" Width="50px" ForeColor="Transparent" />
                
                    &nbsp; <input id="ButtonConfirm" style="width: 60px; font-family: Arial;" type="image" value="登录"  onclick="GetReturnFlag()" src="~/Rmes/Pub/images/logIn.png"   /> &nbsp; &nbsp;&nbsp;&nbsp;
                    <input id="ButtonCancel" style="width: 60px" type="button" value="取消" onclick="return ButtonCancel_onclick()" />--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img id="ButtonConfirm" src="../Pub/Images/logIn.png" onclick="GetReturnFlag()"  width="50px"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img id="ButtonCancel" src="../Pub/Images/exit.png" onclick="return ButtonCancel_onclick()" width="50px" />
                             </td>
            </tr>
        </table>
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
                ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" SelectCommand="select * from code_company order by company_code">
            </asp:SqlDataSource>    

    </form>
    </center>
</body>
</html>
<script  type="text/javascript" language="javascript"  src="<%=Page.ResolveUrl("~/Rmes/Pub/Script/RmesGlobal.js")%>"></script> 

<script type="text/javascript" language="javascript">
 function checkEmployeeCode(e){
   if (checkEnterToTab(e)==true) {
       if(document.forms[0]['<%=TxtEmployeeCode.ClientID %>'].value==""){
      
        alert("用户代码不能为空！");
        document.forms[0]['<%=TxtEmployeeCode.ClientID %>'].focus();        
        return;
      }
      else
      {
        document.forms[0]['<%=TxtPassword.ClientID %>'].focus();     
      }
   }

 
 
 }
 
 
   function checkPassword(e){
   if (checkEnterToTab(e)==true) {
       if(document.forms[0]['<%=TxtPassword.ClientID %>'].value==""){
      
        alert("密码不能为空！");
        document.forms[0]['<%=TxtPassword.ClientID %>'].focus();        
        return;
      }
      else
      {
//        document.forms[0]['ButtonConfirm'].focus();     
        GetReturnFlag();
      }
   }

 
 
 }
 
function ButtonCancel_onclick() {
 
       document.forms[0]['<%=TxtEmployeeCode.ClientID %>'].value="";  
       document.forms[0]['<%=TxtPassword.ClientID %>'].value="";  
 
       document.forms[0]['<%=TxtEmployeeCode.ClientID %>'].focus();       
}


//实现页面不刷新回掉功能
function GetReturnFlag(){

       if(document.forms[0]['<%=TxtEmployeeCode.ClientID %>'].value==""){
      
        alert("用户代码不能为空！");
        document.forms[0]['<%=TxtEmployeeCode.ClientID %>'].focus();        
        return;
      }
       if(document.forms[0]['<%=TxtPassword.ClientID %>'].value==""){
      
        alert("密码不能为空！");
        document.forms[0]['<%=TxtPassword.ClientID %>'].focus();        
        return;
      }
    
//    //这里用分隔符有点儿不合适了：）
    var theSplitStr="@rmes@";
    
    //分隔符从配置文件取 20071219
    
    //测试JS读取XML
//    var theUrl="<%=theServerPath %>";
// 
//    var theSplitStr=ReadFromXmlJs(theUrl,"SeparatorStr");
    
    var theIndex=document.forms[0]['<%=DropDownListPline.ClientID%>'].selectedIndex;
    var thePlineName=document.forms[0]['<%=DropDownListPline.ClientID%>'].options[theIndex].text;
     
    //增加生产线名称  20071219    
    UseCallback(document.forms[0]['<%=DropDownListPline.ClientID%>'].value+theSplitStr+document.forms[0]['<%=TxtEmployeeCode.ClientID %>'].value+theSplitStr+document.forms[0]['<%=TxtPassword.ClientID %>'].value+theSplitStr+thePlineName,"");

}

function GetReturnFlagFromServer(theReturnStr,context){
   
   if(theReturnStr=="0"){
   
       OpenIndex()
    
    }
    else
    {
     switch(theReturnStr){
       case "1":
         alert("用户不存在！");    
         break;
       case "2":
         alert("密码错误！");    
         break;           
       case "3":
         alert("用户无效！");    
         break;     
       case "4":
         alert("限制IP，不允许登录！");    
         break;    
       case "5":
         alert("当前用户超过最大登录数！");    
         break;
       case "6":
         alert("系统登录用户超过最大限制！");    
         break;
       case "7":
         alert("三次登录失败，该账户已经被锁定！");    
         break;
       case "9":
         alert("当前用户和所登录生产线不匹配！");    
         break;
       default:
         alert("系统错误！");    
         break;    
     }

   }
}
function OpenIndex(){
//    location.href = '<%=Page.ResolveUrl("~/Rmes/Login/blank.aspx")%>';
    parent.location.href = '<%=Page.ResolveUrl("~/Rmes/Login/RmesIndex.aspx?progCode=rmesIndex&progName=系统登录")%>';    

}

</script>
