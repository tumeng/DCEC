<%@ Page Language="C#"  EnableEventValidation="false" AutoEventWireup="true" Inherits="Rmes_Pub_CommonQuery_CommonDisplay" StylesheetTheme="Theme1" Codebehind="CommonDisplay.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html  xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RMES-数据浏览</title>
</head>

<body>
    <form id="form1" runat="server">
      
          &nbsp;
          <br />
          <table width="98%">
              <tr>
                  <td style="width: 960px">
                  <div style="overflow-x: scroll; width:900px" id="dvBody">
                    <asp:GridView ID="GridView1" runat="server" Width="2000px" PageSize="30"  OnRowDataBound="GridView1_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
                    </asp:GridView>
                  </div>
                  </td>
              </tr>
              <tr>
                  <td style="width: 960px" align="right">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="导出" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
              </tr>
          </table>
      
    </form>
</body>

</html>
