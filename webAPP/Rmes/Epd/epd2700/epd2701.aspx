<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd2701.aspx.cs" Inherits="Rmes_epd2701" %>

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
    <div>
        <table width="500px">
            <tr style="height: 20px">
                <td style="width: 20px" colspan="4" align="center">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择工位和对应工序，确定提交" Font-Size="Medium">
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
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStation(); }" />
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
                    <dx:ASPxComboBox ID="ASPxListBoxLocation" ClientInstanceName="listBoxLocation" runat="server" Width="280px" Height="25px"
                        OnCallback="listBoxLocation_Callback" >
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择工序">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxListBox ID="ASPxListBoxProcess" runat="server"  ClientInstanceName="listBoxProcess" SelectionMode="CheckColumn" Width="280px" Height="230px"
                        ValueField="PROCESS_CODE" ValueType="System.String" OnCallback="listBoxProcess_Callback">
                        <Columns>
                            <dx:ListBoxColumn FieldName="PROCESS_CODE" Caption="工位代码" Width="30%" />
                            <dx:ListBoxColumn FieldName="PROCESS_NAME" Caption="工位名称" Width="60%" />
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
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>

</body>

<script type="text/javascript">

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterStation() {
        var pline = listPline.GetValue().toString();

        listBoxLocation.PerformCallback(pline);
        listBoxProcess.PerformCallback(pline);
    }

    function checkSubmit() {
        if (listBoxProcess.GetSelectedItems().length == 0 || listBoxLocation.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1) {
            alert("请选择站点及工位再提交！");
            return false;
        }
    }

</script>

</html>
