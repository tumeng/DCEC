<%@ Page Title="在制品物料清单" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="rept3200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept3200.rept3200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<%--在制品物料清单 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        //根据生产线初始化流水号列表
        function filterLocation() {
            if (listPline.GetValue() == null) return;

            pline = listPline.GetValue().toString();

            ref = "FIRST," + pline;
            grid.PerformCallback(ref);
            //刷新左边gridview时，也要刷新右边的，因为右边数据是从左边来的
            grid3.PerformCallback(pline);
            //还要刷新下面的gridview
            grid2.PerformCallback("CLEAR");
        }

        //gridview之间移动条目
        function ShowPopupAdd() {
            var count = grid.GetSelectedRowCount();

            if (count = 0) {
                alert("请选择要增加的流水号");
                return;
            }
            else {
                var fieldNames = "SN";
                grid.GetSelectedFieldValues(fieldNames, RecoverResult);
            }
        }
        function RecoverResult(result) {
            if (result == "") {
                alert('没有选择流水号！无法进行操作');
            }
            else {
                pline = listPline.GetValue().toString();

                ref = "ADD," + pline + "," + result;
                CallbackSubmit.PerformCallback(ref);
            }
        }
        function ShowPopupDelete() {
            var count = grid3.GetSelectedRowCount();

            if (count = 0) {
                alert("请选择要删除的流水号");
                return;
            }
            else {
                var fieldNames = "SN";
                grid3.GetSelectedFieldValues(fieldNames, DeleteResult);
            }
        }
        function DeleteResult(result) {
            if (result == "") {
                alert('没有选择流水号！无法进行操作');
            }
            else {
                pline = listPline.GetValue().toString();

                ref = "DEL," + pline + "," + result;
                CallbackSubmit.PerformCallback(ref);
            }
        }
        //根据返回值刷新左右gridview
        function submitRtr(e) {
            var result = "";
            var retStr = "";
            var array = e.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    //alert(retStr);
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
        //刷新左右两个gridview
        function initGridview() {
            //展示左边的流水号
            if (listPline.GetValue() == null) return;
            pline = listPline.GetValue().toString();
            ref = "NOTFIRST," + pline;
            grid.PerformCallback(ref);
            //展示右边增加的流水号
            if (listPline.GetValue() == null) return;
            pline = listPline.GetValue().toString();
            grid3.PerformCallback(pline);
            grid2.PerformCallback("CLEAR");
        }

        //在制品物料清单tab页数据在左右list之间移动
        //function addItems() {
        //    lbChoosen.SelectAll();
        //    NewMoveSelectedItems(lbAvailable, lbChoosen, 'A');
        //    lbChoosen.UnselectAll();
        //    lbAvailable.UnselectAll();
        //}

        //function deleteItems() {
        //    NewMoveSelectedItems(lbChoosen, lbAvailable, 'D');
        //}

        //function NewMoveSelectedItems(srcListBox, dstListBox, type1) {
        //    srcListBox.BeginUpdate();
        //    dstListBox.BeginUpdate();
        //    var items = srcListBox.GetSelectedItems();
        //    var items2 = dstListBox.GetSelectedItems();
        //    if (type1 == 'A') {
        //        for (var i = items.length - 1; i >= 0; i = i - 1) {
		//			var istrue = false;
        //            for (var j = items2.length - 1; j >= 0; j = j - 1) {
        //                //判断添加项是否重复
        //                if (items[i].text == items2[j].text) {
        //                    istrue = true;
        //                }
        //            }
        //            if (istrue == false) {
        //                dstListBox.AddItem(items[i].text, items[i].value);
        //            }
        //            //srcListBox.RemoveItem(items[i].index);
        //        }
        //    }
        //    if (type1 == 'D') {
        //        for (var i = items.length - 1; i >= 0; i = i - 1) {
        //            //                dstListBox.AddItem(items[i].text, items[i].value);
        //            srcListBox.RemoveItem(items[i].index);
        //        }
        //    }

        //    srcListBox.EndUpdate();
        //    dstListBox.EndUpdate();
        //}

        //function AddAllItems() {
        //    //先清空一下lbChoosen
        //    lbChoosen.ClearItems();
        //    MoveAllItems(lbAvailable, lbChoosen);
        //}

        //function deleteAllItems() {
        //    lbAvailable.ClearItems();
        //    lbChoosen.ClearItems();
        //    //没必要传参数，直接重新查询
        //    lbAvailable.PerformCallback();
        //}

        //function MoveAllItems(srcListBox, dstListBox) {
        //    srcListBox.BeginUpdate();
        //    var count = srcListBox.GetItemCount();
        //    for (var i = 0; i < count; i++) {
        //        var item = srcListBox.GetItem(i);
        //        dstListBox.AddItem(item.text, item.value);
        //    }
        //    srcListBox.EndUpdate();
        //    //srcListBox.ClearItems();
        //}

    </script>
    <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME" ClientInstanceName="listPline"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocation(); }" />
                    <%--<ClientSideEvents SelectedIndexChanged="function(s, e){
                        //刷新左边的list，一定要把右边的list也清空了
                        lbChoosen.ClearItems();
                        //刷新list时，下面的gridview数据也要刷新
                        grid2.PerformCallback();
                        lbAvailable.PerformCallback();
                        }"/>--%>
                </dx:ASPxComboBox>
            </td>
            <td>
                <%-- 加上AutoPostBack="false"，阻止自动提交刷新整个页面 --%>
                <dx:ASPxButton ID="ASPxButton5" runat="server" Text="查询" AutoPostBack="false" Visible="false">
                    <ClientSideEvents Click="function(s, e){
                        lbChoosen.ClearItems();
                        lbAvailable.PerformCallback();
                        }"/>
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <%--<td style="text-align: left; width: 100px">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="流水号" Width="100px">
                </dx:ASPxLabel>
            </td>--%>
            <td style="width: 280px;">
                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                    Width="280px" KeyFieldName="SN" Settings-VerticalScrollableHeight="200"
                    Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True" OnCustomCallback="ASPxGridView1_CustomCallback"
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
                        <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" Width="100%" Settings-AutoFilterCondition="Contains">
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
                        <ClientSideEvents Click="function(s, e) {
                            ShowPopupAdd(); 
                            }" />
                    </dx:ASPxButton>
                </div>
                <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                    OnCallback="ASPxCbSubmit_Callback">
                    <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                </dx:ASPxCallback>
                <div>
                    &nbsp;
                </div>
                <div>
                    <dx:ASPxButton ID="ASPxButton3" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                        AutoPostBack="False" Text="< 删除" Width="100px" Height="23px" ToolTip="Remove selected items">
                        <ClientSideEvents Click="function(s, e) { ShowPopupDelete(); }" />
                    </dx:ASPxButton>
                </div>
            </td>
            <td style="width: 280px;">
                <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="grid3" runat="server" AutoGenerateColumns="False"
                    Width="280px" KeyFieldName="SN" Settings-VerticalScrollableHeight="220"
                    SettingsPager-Mode="ShowAllRecords" Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True"
                    OnCustomCallback="ASPxGridView3_CustomCallback"
                    Settings-ShowFilterRow="false">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                            Width="60px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox2" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid3.SelectAllRowsOnPage(s.GetChecked()); }"
                                    Style="margin-bottom: 0px" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" Width="100%" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager NumericButtonCount="5">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
<%--        <tr>
            <td style="width: 5px" rowspan="5">
            </td>
            <td rowspan="5">
                <dx:ASPxListBox ID="ASPxListBoxUnused" runat="server"  ClientInstanceName="lbAvailable" SelectionMode="CheckColumn" Width="200px" 
                    Height="200px" ValueField="SN" ValueType="System.String" ViewStateMode="Inherit"  TextField="SN"
                    OnCallback="ASPxListBoxUnused_Callback" >
                    <Columns>
                        <dx:ListBoxColumn FieldName="SN" Caption="流水号" Width="100%" />
                    </Columns>
                </dx:ASPxListBox>
            </td>
            <td style="width: 5px" rowspan="5">
            </td>
            <td height="200px" rowspan="5">
                <div>
                    <dx:ASPxButton ID="ASPxButton6" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                        AutoPostBack="False" Text="增加 >" Width="130px" Height="23px" ClientEnabled="true"
                        ToolTip="Add selected items">
                        <ClientSideEvents Click="function(s, e) { addItems(); }" />
                    </dx:ASPxButton>
                </div>
                <div class="TopPadding">
                    <dx:ASPxButton ID="ASPxButton7" runat="server" ClientInstanceName="btnMoveAllItemsToRight"
                        AutoPostBack="False" Text="增加全部 >>" Width="130px" Height="23px" ToolTip="Add all items">
                        <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                    </dx:ASPxButton>
                </div>
                <div style="height: 32px">
                </div>
                <div>
                    <dx:ASPxButton ID="ASPxButton8" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                        AutoPostBack="False" Text="< 删除" Width="130px" Height="23px" ClientEnabled="true"
                        ToolTip="Remove selected items">
                        <ClientSideEvents Click="function(s, e) { deleteItems(); }" />
                    </dx:ASPxButton>
                </div>
                <div class="TopPadding">
                    <dx:ASPxButton ID="ASPxButton9" runat="server" ClientInstanceName="btnMoveAllItemsToLeft"
                        AutoPostBack="False" Text="<< 删除全部" Width="130px" Height="23px" ClientEnabled="true"
                        ToolTip="Remove all items">
                        <ClientSideEvents Click="function(s, e) { deleteAllItems(); }" />
                    </dx:ASPxButton>
                </div>
            </td>
            <td style="width: 5px" rowspan="5">
            </td>
            <td height="200px" rowspan="5">
                <dx:ASPxListBox ID="ASPxListBoxUsed" runat="server" ClientInstanceName="lbChoosen" Width="200px" 
                    Height="200px" SelectionMode="CheckColumn">
                    <Columns>
                        <dx:ListBoxColumn FieldName="SN" Caption="流水号" Width="100%" />
                    </Columns>
                </dx:ASPxListBox>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="queryBill" runat="server" Text="查询物料清单" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){
                                    grid2.PerformCallback();
                                    }"/>
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExportBomList" runat="server" Text="导出物料清单" UseSubmitBehavior="False" OnClick="btnXlsExport_Bom_List">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="queryBill" runat="server" Text="查询物料清单" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){
                                    grid2.PerformCallback('QUERY');
                                    }"/>
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExportBomList" runat="server" Text="导出物料清单" UseSubmitBehavior="False" OnClick="btnXlsExport_Bom_List">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False" KeyFieldName="ITEM_CODE"
        OnCustomCallback="ASPxGridView2_CustomCallback" >
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px" Visible="false">
                <%--<EditButton Visible="True" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />--%>
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="零件号" FieldName="ITEM_CODE" VisibleIndex="1"
                Width="120px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="SUM" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
       <%-- <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />--%>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView2">
    </dx:ASPxGridViewExporter>
     <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlCode1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
