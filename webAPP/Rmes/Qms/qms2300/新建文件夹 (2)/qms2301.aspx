<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qms2301.aspx.cs" Inherits="Rmes.WebApp.Rmes.Qms.qms2300.qms2301" %>

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
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">

        function changeSeq(s, e) {
            index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);

        }
        function GetDataCallback(s) {
            var result = "";
            var retStr = "";
            pline = listPline.GetValue().toString();
            stationCodeL = listBoxStation.GetValue().toString();
            seriesC = comboPSeriesC.GetText().toString();

            if (s == null) {
                //            grid.PerformCallback(pline + "," + stationCodeL + "," + seriesC);
                filterSeries()
                //            grid.PerformCallback();
                return;
            }
            var array = s.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    alert(retStr);
                    initGridview();
                    return;
                case "Fail":
                    alert(retStr);
                    return;
            }
            //        grid.PerformCallback(pline + "," + stationCodeL + "," + seriesC);
            filterSeries()
        }

        String.prototype.endWith = function (endStr) {
            var d = this.length - endStr.length;
            return (d >= 0 && this.lastIndexOf(endStr) == d)
        }
        // ]]> 
    </script>
    <form id="form1" runat="server">
    <div style="float: left">
        <table width="1350px">
            <tr style="height: 20px">
                <td style="width: 20px" colspan="4" align="center">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择生产线、站点、系列和检测数据，确定提交" Font-Size="Medium"
                        Width="400px">
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
                <td style="width: 200px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline" runat="server"
                        Width="260px" Height="25px" ValueField="RMES_ID" TextField="PLINE_NAME">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 720px">
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
                    <dx:ASPxComboBox ID="comboStationCode" ClientInstanceName="listBoxStation" runat="server"
                        Width="260px" Height="25px" OnCallback="comboStationCode_Callback">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStationL(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="选择工艺路线">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboPSeries" ClientInstanceName="comboPSeriesC" runat="server"
                        Width="260px" Height="25px" OnCallback="comboPSeries_Callback">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterSeries(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 320px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择检测数据">
                    </dx:ASPxLabel>
                </td>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td valign="top" style="width: 280px">
                                <div class="BottomPadding">
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="待选检测数据:" />
                                </div>
                                <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="grid3" runat="server" AutoGenerateColumns="False"
                                    Width="280px" KeyFieldName="DETECT_CODE" Settings-VerticalScrollableHeight="300"
                                    Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True" OnCustomCallback="ASPxGridView3_CustomCallback1"
                                    SettingsPager-Mode="ShowAllRecords">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                            Width="40px">
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid3.SelectAllRowsOnPage(s.GetChecked()); }"
                                                    Style="margin-bottom: 0px" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="检测数据代码" FieldName="DETECT_CODE" Width="35%" Settings-AutoFilterCondition="Contains">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="检测数据名称" FieldName="DETECT_NAME" Width="60%" Settings-AutoFilterCondition="Contains">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsPager NumericButtonCount="5">
                                    </SettingsPager>
                                </dx:ASPxGridView>
                            </td>
                            <td valign="middle" align="center" style="padding: 10px; width: 100px">
                                <div>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                                        AutoPostBack="False" Text="增加 >" Width="100px" Height="23px" ToolTip="Add selected items">
                                        <ClientSideEvents Click="function(s, e) { ShowPopupAdd(); }" />
                                    </dx:ASPxButton>
                                </div>
                                <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="CallbackSubmit1"
                                    OnCallback="ASPxCbSubmit1_Callback">
                                    <ClientSideEvents CallbackComplete="function(s, e) { submitRtr1(e.result); }" />
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
                            <td valign="top" style="width: 300px">
                                <div class="BottomPadding">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="已选检测数据:" />
                                </div>
                                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                                    Width="280px" KeyFieldName="DETECT_CODE" Settings-VerticalScrollableHeight="320"
                                    SettingsPager-Mode="ShowAllRecords" Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True"
                                    OnCustomCallback="ASPxGridView2_CustomCallback1" Settings-ShowFilterRow="false">
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                            Width="40px">
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="SelectAllCheckBox2" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid2.SelectAllRowsOnPage(s.GetChecked()); }"
                                                    Style="margin-bottom: 0px" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="检测数据代码" FieldName="DETECT_CODE" Width="35%" Settings-AutoFilterCondition="Contains">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="检测数据名称" FieldName="DETECT_NAME" Width="60%" Settings-AutoFilterCondition="Contains">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsPager NumericButtonCount="5">
                                    </SettingsPager>
                                </dx:ASPxGridView>
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td>
                                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID"
                                    Width="100%" Settings-VerticalScrollableHeight="320" AutoGenerateColumns="False"
                                    SettingsPager-Mode="ShowAllRecords" Settings-ShowVerticalScrollBar="true" Settings-ShowHeaderFilterButton="False"
                                    OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnCustomCallback="ASPxGridView1_CustomCallback"
                                    Settings-ShowGroupPanel="false" Settings-ShowFilterRow="false">
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="调序" Width="80px" ButtonType="Image" CellStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="Up">
                                                    <Image Url="../../Pub/Images/Up.png" Width="15px" ToolTip="上调" />
                                                </dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="Down">
                                                    <Image Url="../../Pub/Images/Down.png" Width="15px" ToolTip="下调">
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="RMES_ID" FieldName="RMES_ID" Width="100px" Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="检测数据代码" FieldName="DETECT_CODE" Width="100px"
                                            CellStyle-HorizontalAlign="left">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="检测数据名称" FieldName="DETECT_NAME" Width="200px"
                                            CellStyle-HorizontalAlign="left">
                                            <Settings AutoFilterCondition="Contains"></Settings>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="顺序" FieldName="DETECT_SEQ" Width="60px" CellStyle-HorizontalAlign="left">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="PLINE_CODE" FieldName="PLINE_CODE" Width="100px"
                                            Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="STATION_CODE" FieldName="STATION_CODE" Width="100px"
                                            Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="PRODUCT_SERIES" FieldName="PRODUCT_SERIES" Width="100px"
                                            Visible="false">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior ColumnResizeMode="Control" />
                                    <ClientSideEvents CustomButtonClick="function (s,e){
                                            changeSeq(s,e);
                                        }" />
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 50px">
                <td>
                </td>
                <td>
                </td>
                <td style="text-align: left;">
                    <table>
                        <tr>
                            <td style="width: 120px">
                                <dx:ASPxButton ID="ASPxButton5" ClientInstanceName="ASPxButton1" runat="server" AutoPostBack="false"
                                    Text="确定" Width="100px">
                                    <ClientSideEvents Click="function(s,e){ THSure(); }" />
                                </dx:ASPxButton>
                            </td>
                            <td>
                                <dx:ASPxButton ID="ASPxButton6" ClientInstanceName="ASPxButton11" runat="server"
                                    AutoPostBack="false" Visible="false" Text="关闭窗口" Width="100px" OnClick="ASPxButton6_Click">
                                </dx:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    var pline;
    var stationCodeL;
    var seriesC;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterStation() {
        pline = listPline.GetValue().toString();

        listBoxStation.PerformCallback(pline);
    }
    function filterStationL() {
        pline = listPline.GetValue().toString();

        comboPSeriesC.PerformCallback(pline);
    }

    function filterSeries() {
        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();
        seriesC = comboPSeriesC.GetText().toString();

        grid3.PerformCallback(pline + "," + stationCodeL + "," + seriesC);
        grid2.PerformCallback(pline + "," + stationCodeL + ",CHUFA," + seriesC);

        grid.PerformCallback(pline + "," + stationCodeL + "," + seriesC);

    }
    function ShowPopupAdd() {
        var count = grid3.GetSelectedRowCount();

        if (count = 0) {
            alert("请选择要增加的检测数据");
            return;
        }
        else {
            var fieldNames = "DETECT_CODE";
            grid3.GetSelectedFieldValues(fieldNames, RecoverResult);
        }
    }
    function RecoverResult(result) {
        if (result == "") {
            alert('没有选择检测数据！无法进行操作');
        }
        else {
            pline = listPline.GetValue().toString();
            stationCodeL = listBoxStation.GetValue().toString();
            seriesC = comboPSeriesC.GetText().toString();

            ref = "ADD," + pline + "," + stationCodeL + "," + seriesC + "," + result;
            CallbackSubmit1.PerformCallback(ref);
        }
    }
    function ShowPopupDelete() {
        var count = grid2.GetSelectedRowCount();

        if (count = 0) {
            alert("请选择要删除的检测数据");
            return;
        }
        else {
            var fieldNames = "DETECT_CODE";
            grid2.GetSelectedFieldValues(fieldNames, DeleteResult);
        }
    }
    function DeleteResult(result) {
        if (result == "") {
            alert('没有选择检测数据！无法进行操作');
        }
        else {
            pline = listPline.GetValue().toString();
            stationCodeL = listBoxStation.GetValue().toString();
            seriesC = comboPSeriesC.GetText().toString();

            ref = "DELETE," + pline + "," + stationCodeL + "," + seriesC + "," + result;
            CallbackSubmit1.PerformCallback(ref);
        }
    }
    function THSure() {
        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();
        seriesC = comboPSeriesC.GetText().toString();

        ref = "SURE," + pline + "," + stationCodeL + "," + seriesC;
        CallbackSubmit1.PerformCallback(ref);
    }
    function submitRtr1(e) {
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
                window.opener.location.href = '../qms2300/qms2300.aspx';
                return;
        }
    }
    function initGridview() {
        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();
        seriesC = comboPSeriesC.GetText().toString();

        grid3.PerformCallback(pline + "," + stationCodeL + "," + seriesC);
        grid2.PerformCallback(pline + "," + stationCodeL + ",INIT," + seriesC);
        grid.PerformCallback(pline + "," + stationCodeL + "," + seriesC);

    }
    

</script>
</html>
