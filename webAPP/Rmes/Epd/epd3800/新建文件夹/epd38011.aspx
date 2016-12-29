<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd38011.aspx.cs" Inherits="Rmes_epd3801" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float: left">
        <table width="450px">
            <tr style="height: 20px">
                <td style="width: 20px" colspan="4" align="center">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择工位和员工，确定提交" Font-Size="Medium" Width="400px">
                    </dx:ASPxLabel>
                </td>
                
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px">
                </td>
                <td style="width: 60px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 300px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline"  runat="server" Width="280px" Height="25px"
                        ValueField="RMES_ID" TextField="PLINE_NAME" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 30px">
                </td>
            </tr>


            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择工位">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboLocationCode" ClientInstanceName="comboBoxLocation" runat="server" Width="280px" Height="25px"
                        OnCallback="comboLocationCode_Callback" >
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            
            <tr style="height:230px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择员工">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxListBox ID="listBoxUser" runat="server"  
                        ClientInstanceName="listBoxuserClient" SelectionMode="CheckColumn" 
                        Width="280px" Height="230px"
                        ValueField="USER_ID" ValueType="System.String"   ViewStateMode="Inherit" 
                        oncallback="listBoxUser_Callback" >
                        <Columns>
                            <dx:ListBoxColumn FieldName="USER_ID" Caption="员工代码" Width="40%" />
                            <dx:ListBoxColumn FieldName="USER_NAME" Caption="员工姓名" Width="50%" />
                        </Columns>
                    </dx:ASPxListBox>
                </td>
                <td>
                </td>
            </tr>   
              
            <tr style="height: 50px">
                <td>
                </td>
                <td>
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="butConfirm" runat="server" OnClientClick="return checkSubmit();" onclick="butConfirm_Click" Text="确定" Width="100px" Height="30px" />
                    &nbsp; 
                    <asp:Button ID="ButtonCloseWindow" runat="server" onclick="butCloseWindow_Click" Text="关闭窗口" Width="100px" Height="30px" />

                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>

</body>

<script type="text/javascript">
    var pline ;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterLocation() {
        pline = listPline.GetValue().toString();

        comboBoxLocation.PerformCallback(pline);
        listBoxuserClient.PerformCallback(pline);
    }

    function checkSubmit() {
        if (listBoxuserClient.GetSelectedItems().length == 0 || comboBoxLocation.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1) {
            alert("请选择工位、员工再提交！");
            return false;
        }
    }

</script>

</html>
