<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mms1404.aspx.cs" Inherits="Rmes_mms1404" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

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
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择合同号，选择要替换的原物料代码，确定提交" Font-Size="Medium" Width="500px">
                    </dx:ASPxLabel>
                </td>
                
            </tr>
            <tr style="height: 20px">
                <td colspan="4" align="center">
                    <asp:Label ID="lblMessage" runat="server" Width="500px" ForeColor="#FF0033" Text="开始..." />
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px">
                </td>
                <td style="width: 140px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择合同号">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 300px">
                    <dx:ASPxComboBox ID="comboProject" ClientInstanceName="listProject"  runat="server" Width="280px" Height="25px"
                        ValueField="PROJECT_CODE" TextField="SHOWNAME" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { initListITEM(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 30px">
                </td>
            </tr>


            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="原物料代码">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="ComboItemFrom" ClientInstanceName="listItemFrom"  runat="server" Width="280px" Height="25px"
                        ValueField="ITEM_CODE_FROM" TextField="SHOWNAME"  OnCallback="ASPxComboBoxItemFrom_Callback">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { setValue(); }" />
                    </dx:ASPxComboBox>
                    <%--<asp:TextBox ID="TextOldBOM" runat="server" Width="280px" Height="25px" onchange="onTextOldBOMChange(this)"/>--%>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="物料名称">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <asp:TextBox ID="TextOldName" runat="server" Width="280px" Height="25px"/>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="替换物料代码">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <asp:TextBox ID="TextNewBOM" runat="server" Width="280px" Height="25px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="物料名称">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <asp:TextBox ID="TextNewName" runat="server" Width="280px" Height="25px"/>
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
                    <asp:Button ID="butConfirm" runat="server" OnClientClick="return checkSubmit();" onclick="butConfirm_Click" Text="替换物料" Width="100px" Height="30px" />
                    &nbsp; &nbsp; 
                    <asp:Button ID="ButRestore" runat="server" OnClientClick="return checkSubmit();" onclick="butRestore_Click" Text="还原物料" Width="100px" Height="30px" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>

    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit" OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>

    </form>

</body>

<script type="text/javascript">

    var oldCode = document.getElementById("TextOldBOM");
    var oldName = document.getElementById("TextOldName");
    var newCode = document.getElementById("TextNewBOM");
    var newName = document.getElementById("TextNewName");

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function initListITEM() {
        var project = listProject.GetValue().toString();
        listItemFrom.PerformCallback(project);
    }

    function setValue() {
        var code = listItemFrom.GetValue().toString();

        ref = "checkValue," + code;
        CallbackSubmit.PerformCallback(ref);
    }


    function checkSubmit() {
        if (listProject.GetSelectedIndex() == -1 || listItemFrom.GetSelectedIndex() == -1 || oldName.value == "" || newCode.value == "" || newName.value == "") {
            alert("请选择合同号，输入原物料代码、替换代码，再提交！");
            return false;
        }
    }

    function submitRtr(e) {
        var result = "";
        var retStr = "";
        var array = e.split(',');
        retStr = array[1];
        result = array[0];

        switch (result) {
            case "checkOK":
                oldName.value = array[1];
                newCode.value = array[2];
                newName.value = array[3];
                newCode.focus();
                break;
        }
    }

</script>

</html>
