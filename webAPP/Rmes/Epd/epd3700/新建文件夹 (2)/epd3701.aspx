<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd3701.aspx.cs" Inherits="Rmes_epd3701" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
    <div style="float: left">
        <table width="850px">
            <tr style="height: 20px">
            <td colspan="2"></td>
                <td colspan="3" align="left">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择站点和前道站点，确定提交" Font-Size="Medium" Width="400px">
                    </dx:ASPxLabel>
                </td>
                
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px">
                </td>
                <td style="width: 80px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 300px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline"  runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList"
                        ValueField="RMES_ID" TextField="PLINE_NAME" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 150px">
                </td>
                <td style="width: 300px">
                </td>
            </tr>


            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择站点">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboStationCode" ClientInstanceName="comboBoxStation" runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList"
                        OnCallback="comboStationCode_Callback" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterPreStation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            
            <tr style="height:230px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择前道站点">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                        Width="280px" KeyFieldName="RMES_ID" Settings-VerticalScrollableHeight="300"
                        Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True" OnCustomCallback="ASPxGridView1_CustomCallback1"
                        SettingsPager-Mode="ShowAllRecords" >
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                Width="60px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                                        Style="margin-bottom: 0px" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="前道站点代码" FieldName="STATION_CODE" Width="30%" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="前道站点名称" FieldName="STATION_NAME" Width="80%" Settings-AutoFilterCondition="Contains">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager NumericButtonCount="5">
                        </SettingsPager>
                    </dx:ASPxGridView>
                   
                </td>
                <td valign="middle" align="center" style="padding: 10px; width: 10%">
                    <div>
                        <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                            AutoPostBack="False" Text="增加 >" Width="100px" Height="23px" ToolTip="Add selected items">
                            <ClientSideEvents Click="function(s, e) { ShowPopupAdd(); }" />
                        </dx:ASPxButton>
                    </div>
                    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                        OnCallback="ASPxCbSubmit_Callback">
                        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                    </dx:ASPxCallback>
                    <div>
                        &nbsp;</div>
                    <div>
                        <dx:ASPxButton ID="ASPxButton3" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                            AutoPostBack="False" Text="< 删除" Width="100px" Height="23px" ToolTip="Remove selected items">
                            <ClientSideEvents Click="function(s, e) { ShowPopupDelete(); }" />
                        </dx:ASPxButton>
                    </div>
                </td>
                <td>
                    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                        Width="280px" KeyFieldName="RMES_ID" Settings-VerticalScrollableHeight="320"
                        SettingsPager-Mode="ShowAllRecords" Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True"
                        OnCustomCallback="ASPxGridView2_CustomCallback1" Settings-ShowFilterRow="false">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                Width="60px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="SelectAllCheckBox2" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid2.SelectAllRowsOnPage(s.GetChecked()); }"
                                        Style="margin-bottom: 0px" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="前道站点代码" FieldName="STATION_CODE" Width="30%" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="前道站点名称" FieldName="STATION_NAME" Width="80%" Settings-AutoFilterCondition="Contains">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager NumericButtonCount="5">
                        </SettingsPager>
                    </dx:ASPxGridView>
                    
                </td>
            </tr>   
              
            <tr style="height: 50px">
                <td>
                </td>
                <td>
                </td>
                <td style="text-align: left;" colspan="3">
                    <table>
                        <tr>
                            <td style="text-align: left;">
                                <dx:ASPxButton ID="BtnSure" Text="确定" Width="100px" AutoPostBack="false" ClientInstanceName="BtnSureC"
                                    runat="server">
                                    <ClientSideEvents Click="function(s,e){
                                         THSure();     
                                        }" />
                                </dx:ASPxButton>
                            </td>
                            <td style="text-align: left;">
                                <dx:ASPxButton ID="BtnCloseWindow" Text="关闭窗口" Width="100px" AutoPostBack="false"
                                    ClientInstanceName="BtnSureC" runat="server" OnClick="BtnCloseWindow_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
        </table>
    </div>
    </form>

</body>

<script type="text/javascript">
    var pline;
    var comstation1;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterStation() {
        pline = listPline.GetValue().toString();

        comboBoxStation.PerformCallback(pline);
    }

    function filterPreStation() {
        pline = listPline.GetValue().toString();
        comstation1 = comboBoxStation.GetValue().toString();

        grid.PerformCallback(pline + "," + comstation1 );
        grid2.PerformCallback(pline + "," + comstation1  + ",CHUFA" );

    }
    function ShowPopupAdd() {
        var count = grid.GetSelectedRowCount();

        if (count = 0) {
            alert("请选择要增加的前道站点");
            return;
        }
        else {
            var fieldNames = "RMES_ID";
            grid.GetSelectedFieldValues(fieldNames, RecoverResult);
        }
    }
    function RecoverResult(result) {
        if (result == "") {
            alert('没有选择前道站点！无法进行操作');
        }
        else {
            pline = listPline.GetValue().toString();
            comstation1 = comboBoxStation.GetValue().toString();

            ref = "ADD," + pline + "," + comstation1 + ","  + result;
            CallbackSubmit.PerformCallback(ref);
        }
    }
    function ShowPopupDelete() {
        var count = grid2.GetSelectedRowCount();

        if (count = 0) {
            alert("请选择要删除的前道站点");
            return;
        }
        else {
            var fieldNames = "RMES_ID";
            grid2.GetSelectedFieldValues(fieldNames, DeleteResult);
        }
    }
    function DeleteResult(result) {
        if (result == "") {
            alert('没有选择前道站点！无法进行操作');
        }
        else {
            pline = listPline.GetValue().toString();
            comstation1 = comboBoxStation.GetValue().toString();

            ref = "DELETE," + pline + "," + comstation1 + "," + result;
            CallbackSubmit.PerformCallback(ref);
        }
    }
    function THSure() {
        pline = listPline.GetValue().toString();
        comstation1 = comboBoxStation.GetValue().toString();

        ref = "SURE," + pline + "," + comstation1 ;
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
            case "Fail":
                alert(retStr);
                initGridview();
                return;
            case "Sure":
                alert(retStr);
                initGridview();
                window.opener.location.href = '../epd3700/epd3700.aspx';
                return;
        }
    }
    function initGridview() {
        pline = listPline.GetValue().toString();
        comstation1 = comboBoxStation.GetValue().toString();

        grid.PerformCallback(pline + "," + comstation1);
        grid2.PerformCallback(pline + "," + comstation1 +",INIT");
       
    }
</script>

</html>
