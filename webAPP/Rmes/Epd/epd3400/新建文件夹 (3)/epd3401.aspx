<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd34011.aspx.cs" Inherits="Rmes_epd3401" %>

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
    // <![CDATA[
    
    // ]]> 
    </script>
    <form id="form1" runat="server">
    <div style="float: left">
        <table width="1100px">
            <tr style="height: 20px">
                <td colspan="2">
                </td>
                <td colspan="3" align="left">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择站点和对应工位，确定提交" Font-Size="Medium"
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
                <td style="width: 280px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline" runat="server"
                        Width="280px" Height="25px" DropDownStyle="DropDownList" ValueField="RMES_ID"
                        TextField="PLINE_CODE">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 120px">
                </td>
                <td style="width: 280px">
                </td>
                <td style="width: 250px">
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
                        Width="280px" Height="25px" DropDownStyle="DropDownList" OnCallback="comboStationCode_Callback">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStationL(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="工位属性">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="locationPro" ClientInstanceName="listLocationPro" DropDownStyle="DropDownList"
                        runat="server" Width="280px" Height="25px" ValueField="INTERNAL_CODE" TextField="INTERNAL_NAME"
                        OnCallback="locationPro_Callback">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="查看工位">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboItemPro" ClientInstanceName="listItemPro" runat="server"
                        DropDownStyle="DropDownList" Width="280px" Height="25px" OnCallback="itemPro_Callback"
                        ValueField="INTERNAL_CODE" TextField="INTERNAL_NAME">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocation1(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 230px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择工位">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                        Width="280px" KeyFieldName="RMES_ID" Settings-VerticalScrollableHeight="300"
                        Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True" OnCustomCallback="ASPxGridView1_CustomCallback1"
                        SettingsPager-Mode="ShowAllRecords">
                        <Columns>
                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                Width="60px">
                                <HeaderTemplate>
                                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                                        Style="margin-bottom: 0px" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" Width="30%" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工位名称" FieldName="LOCATION_NAME" Width="80%" Settings-AutoFilterCondition="Contains">
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
                            <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" Width="30%" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="工位名称" FieldName="LOCATION_NAME" Width="80%" Settings-AutoFilterCondition="Contains">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsPager NumericButtonCount="5">
                        </SettingsPager>
                    </dx:ASPxGridView>
                </td>
                <td>
                    <fieldset style="width: 230px; text-align: center; height: 350px">
                        <legend><span style="font-size: 10pt; width: auto">
                            <asp:Label ID="Label23" runat="server" Text="未分配站点工位" Font-Bold="True"></asp:Label></span></legend>
                        <table width="95%">
                            <tr>
                                <td>
                                    <dx:ASPxListBox ID="ASPxListBox1" runat="server" ClientInstanceName="ASPxListBox1C"
                                        Width="220px" Height="330px" ValueField="RMES_ID" ValueType="System.String" OnCallback="ASPxListBox1_Callback"
                                        ViewStateMode="Inherit" TextField="LOCATION_NAME">
                                    </dx:ASPxListBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr style="height: 50px">
                <td>
                </td>
                <td>
                </td>
                <td colspan="4">
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
            <tr style="height: 230px">
                <td>
                </td>
                <td valign="middle" align="center" style="padding: 10px; width: 10%">
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
    var locationPro;
    var itemPro;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }
    //根据生产线初始化站点
    function filterStation() {
        listBoxStation.ClearItems(); //站点
        listLocationPro.ClearItems(); //工位属性
        listItemPro.ClearItems(); //是否查看工位

        pline = listPline.GetValue().toString();

        listBoxStation.PerformCallback(pline);
        listLocationPro.PerformCallback();
        ASPxListBox1C.PerformCallback(pline);
    }
    //初始化工位属性
    function filterStationL() {
        listLocationPro.ClearItems();
        listItemPro.ClearItems();

        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();

        listLocationPro.PerformCallback();
    }
    //选择工位属性后初始化工位列表和已选择工位列表
    function filterLocation() {
        listItemPro.ClearItems();


        if (listLocationPro.GetValue() == null) return;

        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();
        locationPro = listLocationPro.GetValue().toString();

        listItemPro.PerformCallback(locationPro);
        if (locationPro == "A") {
            grid.PerformCallback(pline + "," + locationPro + "," + stationCodeL);
            grid2.PerformCallback(pline + "," + locationPro + "," + stationCodeL + ",CHUFA,AAA");
        }


    }
    //工位属性为显示工位时，初始化工位列表和已选择工位列表
    function filterLocation1() {
        if (listItemPro.GetValue() == null) return;

        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();
        locationPro = listLocationPro.GetValue().toString();
        itemPro = listItemPro.GetValue().toString();

        grid.PerformCallback(pline + "," + locationPro + "," + stationCodeL);
        grid2.PerformCallback(pline + "," + locationPro + "," + stationCodeL + ",CHUFA," + itemPro);

    }
    //点确定时前台事件
    function checkSubmit() {
        //        if (lbChoosen.GetItemCount() == 0 || listBoxStation.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1 || listLocationPro.GetSelectedIndex() == -1) {
        //            alert("请选择站点、工位及工位属性再提交！");
        //            return false;
        //        }

    }

    function ShowPopupAdd() {
        var count = grid.GetSelectedRowCount();

        if (count = 0) {
            alert("请选择要增加的工位");
            return;
        }
        else {
            var fieldNames = "RMES_ID";
            grid.GetSelectedFieldValues(fieldNames, RecoverResult);
        }
    }
    function RecoverResult(result) {
        if (result == "") {
            alert('没有选择工位！无法进行操作');
        }
        else {
            pline = listPline.GetValue().toString();
            stationCodeL = listBoxStation.GetValue().toString();
            locationPro = listLocationPro.GetValue().toString();
            if (locationPro == "B") {
                itemPro = listItemPro.GetValue().toString();
            }
            else {
                itemPro = "";
            }

            ref = "ADD," + pline + "," + stationCodeL + "," + locationPro + "," + itemPro + "," + result;
            CallbackSubmit.PerformCallback(ref);
        }
    }
    function ShowPopupDelete() {
        var count = grid2.GetSelectedRowCount();

        if (count = 0) {
            alert("请选择要删除的工位");
            return;
        }
        else {
            var fieldNames = "RMES_ID";
            grid2.GetSelectedFieldValues(fieldNames, DeleteResult);
        }
    }
    function DeleteResult(result) {
        if (result == "") {
            alert('没有选择工位！无法进行操作');
        }
        else {
            pline = listPline.GetValue().toString();
            stationCodeL = listBoxStation.GetValue().toString();
            locationPro = listLocationPro.GetValue().toString();
            if (locationPro == "B") {
                itemPro = listItemPro.GetValue().toString();
            }
            else {
                itemPro = "";
            }

            ref = "DELETE," + pline + "," + stationCodeL + "," + locationPro + "," + itemPro + "," + result;
            CallbackSubmit.PerformCallback(ref);
        }
    }
    function THSure() {
        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();
        locationPro = listLocationPro.GetValue().toString();
        if (locationPro == "B") {
            itemPro = listItemPro.GetValue().toString();
        }
        else {
            itemPro = "";
        }

        ref = "SURE," + pline + "," + stationCodeL + "," + locationPro + "," + itemPro + ",";
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
                window.opener.location.href = '../epd3400/epd3400.aspx';
                return;
        }
    }
    function initGridview() {
        pline = listPline.GetValue().toString();
        stationCodeL = listBoxStation.GetValue().toString();
        locationPro = listLocationPro.GetValue().toString();
        if (locationPro == "B") {
            itemPro = listItemPro.GetValue().toString();
        }
        else {
            itemPro = "";
        }

        grid.PerformCallback(pline + "," + locationPro + "," + stationCodeL);
        if (listItemPro.GetValue() == null) {
            grid2.PerformCallback(pline + "," + locationPro + "," + stationCodeL + ",INIT,BBB");
        }
        else {
            grid2.PerformCallback(pline + "," + locationPro + "," + stationCodeL + ",INIT," + itemPro);
        }
    }

</script>
</html>
