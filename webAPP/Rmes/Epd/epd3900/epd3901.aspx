<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd3901.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd3900.epd3901" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float: left">
        <table width="1100px">
            <tr style="height: 20px">
                <td style="width: 20px">
                </td>
                <td colspan="5" align="left">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线跨线模式维护" Font-Size="Medium"
                        Width="400px">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px; width: 20px">
                </td>
                <td style="width: 100px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Width="100%" Text="选择跨线模式">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 250px">
                    <dx:ASPxComboBox ID="comboAline" ClientInstanceName="comboAlineClient" runat="server"
                        Width="200px" Height="25px" DropDownStyle="DropDownList" ValueField="ALINE_CODE"
                        TextField="ALINE_NAME">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterAline(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 420px">
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Width="100%" Text="原生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxComboBox ID="comboPlineCodeOld" ClientInstanceName="listPlineOldC" runat="server"
                        Width="200px" Height="25px" DropDownStyle="DropDownList" ValueField="PLINE_ID_OLD"
                        TextField="PLINE_NAME_OLD" OnCallback="comboPlineCodeOld_Callback">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterProcess(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Width="100%" Text="目标生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxComboBox ID="comboPlineCodeNew" ClientInstanceName="listPlineNewC" runat="server"
                        Width="200px" Height="25px" DropDownStyle="DropDownList" ValueField="RMES_ID"
                        TextField="PLINE_NAME">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table width="1100px">
                        <tr>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 480px">
                                <fieldset style="width: 98%; text-align: center">
                                    <legend><span style="font-size: 10pt; width: auto">
                                        <asp:Label ID="Label23" runat="server" Text="选择生产线工序" Font-Bold="True"></asp:Label></span></legend>
                                    <table width="95%">
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" KeyFieldName="PROCESS_ID" SettingsPager-PageSize="15" Settings-VerticalScrollableHeight="350"
                                                    Settings-ShowVerticalScrollBar="True" OnCustomCallback="ASPxGridView1_CustomCallback1"
                                                    OnPageIndexChanged="ASPxGridView1_PageIndexChanged">
                                                    <Columns>
                                                        <%--<dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Width="30px">
                                                            <SelectButton Text="选择">
                                                            </SelectButton>
                                                        </dx:GridViewCommandColumn>--%>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                                            Width="60px">
                                                            <HeaderTemplate>
                                                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                                                                    Style="margin-bottom: 0px" />
                                                            </HeaderTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" Width="130px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" Width="130px">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager NumericButtonCount="5">
                                                    </SettingsPager>
                                                    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="350"></Settings>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 250px">
                                <fieldset style="width: 98%; text-align: center">
                                    <legend><span style="font-size: 10pt; width: auto">
                                        <asp:Label ID="Label1" runat="server" Text="选择替换工位" Font-Bold="True"></asp:Label></span></legend>
                                    <table width="95%">
                                        <tr>
                                            <td>
                                                <dx:ASPxListBox ID="listLocationTH" runat="server" ClientInstanceName="listLocationTHC"
                                                    SelectionMode="Single" Width="230px" Height="400px" ValueField="RMES_ID" TextField="LOCATION_CODE"
                                                    ValueType="System.String" ViewStateMode="Inherit" OnCallback="listLocationTH_Callback">
                                                </dx:ASPxListBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="width: 20px">
                                <table>
                                    <tr>
                                        <td style="text-align: center">
                                            <dx:ASPxButton ID="BtnAdd" Text="替换" Width="80px" AutoPostBack="false" ClientInstanceName="BtnAddC"
                                                runat="server">
                                                <ClientSideEvents Click="function(s,e){
                                                               ShowPopup2();     
                                                            }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center">
                                            <dx:ASPxButton ID="BtnDelete" Text="删除" Width="80px" AutoPostBack="false" ClientInstanceName="BtnDeleteC"
                                                runat="server">
                                                <ClientSideEvents Click="function(s,e){
                                                               ShowPopup1();     
                                                            }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                                <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                                    OnCallback="ASPxCbSubmit_Callback">
                                    <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                                </dx:ASPxCallback>
                            </td>
                            <td style="width: 330px">
                                <fieldset style="width: 98%; text-align: left">
                                    <legend><span style="font-size: 10pt; width: auto">
                                        <asp:Label ID="Label2" runat="server" Text="工序工位替换关系列表" Font-Bold="True"></asp:Label></span></legend>
                                    <table width="95%">
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="PROCESS_ID" SettingsPager-PageSize="15" Settings-VerticalScrollableHeight="350"
                                                    Settings-ShowVerticalScrollBar="True" OnCustomCallback="ASPxGridView2_CustomCallback1"
                                                    OnPageIndexChanged="ASPxGridView2_PageIndexChanged">
                                                    <Columns>
                                                        <%--<dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Width="30px">
                                                        </dx:GridViewCommandColumn>--%>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                                            Width="60px">
                                                            <HeaderTemplate>
                                                                <dx:ASPxCheckBox ID="SelectAllCheckBox2" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid2.SelectAllRowsOnPage(s.GetChecked()); }"
                                                                    Style="margin-bottom: 0px" />
                                                            </HeaderTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" Width="130px">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" Width="130px">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager NumericButtonCount="5">
                                                    </SettingsPager>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="text-align: right;">
                                <dx:ASPxButton ID="BtnSure" Text="保存替换" Width="80px" AutoPostBack="false" ClientInstanceName="BtnSureC"
                                    runat="server">
                                    <ClientSideEvents Click="function(s,e){
                             THSure();     
                            }" />
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>

                <td style="text-align: left;" colspan="4">
                    <dx:ASPxButton ID="BtnCloseWindow" Text="关闭窗口" Width="80px" 
                        AutoPostBack="false" ClientInstanceName="BtnSureC"
                        runat="server" onclick="BtnCloseWindow_Click" Visible="False">
                    </dx:ASPxButton>
                    <%--<asp:Button ID="ButtonCloseWindow" runat="server" OnClick="butCloseWindow_Click"
                        Text="关闭窗口" Width="80px" Height="25px" />--%>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    var plineOld;
    var plineNew;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterLocation() {
        plineNew = listPlineNewC.GetValue().toString();
        listLocationTHC.PerformCallback(plineNew);
        grid2.PerformCallback();
    }
    function filterAline() {
        var alineCode = comboAlineClient.GetValue().toString();

        listPlineOldC.PerformCallback(alineCode);
        grid.PerformCallback(alineCode);
        grid2.PerformCallback();
    }
    function ShowPopup2() {
        var count = grid.GetSelectedRowCount();
        if (count = 0 || listLocationTHC.GetSelectedItems().length == 0) {
            alert("请选择要替换的工序,工位");
            return;
        }
        else {
            var fieldNames = "PROCESS_ID";
            grid.GetSelectedFieldValues(fieldNames, RecoverResult);
        }
    }
    function RecoverResult(result) {
        if (result == "") {
            alert('没有选择替换的工序！无法进行操作');
        }
        else {
            ref = "TIHUAN," + listLocationTHC.GetSelectedItems()[0].value + "," + result;
            CallbackSubmit.PerformCallback(ref);
        }
    }
    function ShowPopup1() {
        var count = grid2.GetSelectedRowCount();
        if (count = 0) {
            alert("请选择要删除的工序");
            return;
        }
        else {
            var fieldNames = "PROCESS_ID";
            grid2.GetSelectedFieldValues(fieldNames, DeleteResult);
        }
    }
    function DeleteResult(result) {
        if (result == "") {
            alert('没有选择删除的工序！无法进行操作');
        }
        else {
            ref = "SHANCHU," + "AA," + result;
            CallbackSubmit.PerformCallback(ref);
        }
    }
    function THSure() {
        ref = "SURE";
        CallbackSubmit.PerformCallback(ref);
    }

    function submitRtr(e) {
        var result = "";
        var retStr = "";
        var array = e.split(',');
        retStr = array[1];
        result = array[0];

        switch (result) {
            case "OK":
                alert(retStr);
                initGridview();
                return;
            case "OK1":
                initGridview();
                return;
            case "Fail":
                alert(retStr);
                return;
            case "Sure":
                alert(retStr);
                initGridview();
                window.opener.location.href = '../epd3900/epd3900.aspx';
                return;
        }
    }
    function initGridview() {
        filterAline();
        grid2.PerformCallback();
    }

</script>
</html>
