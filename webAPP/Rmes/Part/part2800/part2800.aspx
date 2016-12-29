<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="part2800.aspx.cs" Inherits="Rmes_part2800" %>

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
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView3">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="ASPxGridView4">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" runat="server" GridViewID="ASPxGridView5">
    </dx:ASPxGridViewExporter>
    <script type="text/javascript">
        var pline;
        var planCode;

        if (!String.prototype.trim) {
            String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
        }

        function filterPlan() {
            pline = comboPlineCodeC.GetValue().toString();

            comboPlanCodeC.PerformCallback(pline);
        }
        function filterAspxgridview1() {
            pline = comboPlineCodeC.GetValue().toString();
            planCode = comboPlanCodeC.GetValue().toString();

            grid1.PerformCallback(pline + "," + planCode);
        }
        function ShowPopupAdd() {
            var count = grid1.GetSelectedRowCount();

            if (count = 0) {
                alert("请选择要增加的SN");
                return;
            }
            else {
                var fieldNames = "SN";
                grid1.GetSelectedFieldValues(fieldNames, RecoverResult);
            }
        }
        function RecoverResult(result) {
            if (result == "") {
                alert('没有选择SN！无法进行操作');
            }
            else {
                pline = comboPlineCodeC.GetValue().toString();
                planCode = comboPlanCodeC.GetValue().toString();

                ref = "ADD," + pline + "," + planCode + "," + result;
                CallbackSubmit1.PerformCallback(ref);
            }
        }
        function ShowPopupDelete() {
            var count = grid2.GetSelectedRowCount();

            if (count = 0) {
                alert("请选择要删除的SN");
                return;
            }
            else {
                var fieldNames = "SN";
                grid2.GetSelectedFieldValues(fieldNames, DeleteResult);
            }
        }
        function DeleteResult(result) {
            if (result == "") {
                alert('没有选择SN！无法进行操作');
            }
            else {
                pline = comboPlineCodeC.GetValue().toString();
                planCode = comboPlanCodeC.GetValue().toString();

                ref = "DELETE," + pline + "," + planCode + "," + result;
                CallbackSubmit1.PerformCallback(ref);
            }
        }
        function CmdQrySure() {
            var count = grid2.GetSelectedRowCount();

            if (count = 0) {
                alert("请选择要计算的流水号");
                return;
            }
            else {
                var fieldNames = "SN";
                grid2.GetSelectedFieldValues(fieldNames, CmdQryResult);
            }
        }
        function CmdQryResult(result) {
            if (result == "") {
                alert('没有选择SN！无法进行操作');
            }
            else {
                pline = comboPlineCodeC.GetValue().toString();
                planCode = comboPlanCodeC.GetValue().toString();

                ref = "CMDQRY," + pline + "," + planCode + "," + result;
                CallbackSubmit1.PerformCallback(ref);
            }
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
            }
        }
        function initGridview() {
            pline = comboPlineCodeC.GetValue().toString();
            planCode = comboPlanCodeC.GetValue().toString();

            grid1.PerformCallback(pline + "," + planCode);
            grid2.PerformCallback(pline + "," + planCode);
        }
        function initGridview3() {
            if (comboPlineCodeC.GetValue() == null) {
                alert("请先选择生产线！");
                return;
            }

            pline = comboPlineCodeC.GetValue().toString();
            planCode = comboPlanCodeC.GetValue().toString();

            grid3.PerformCallback(pline + "," + planCode);
        }
        function initGridview4() {
            if (comboPlineCodeC.GetValue() == null) {
                alert("请先选择生产线！");
                return;
            }
            pline = comboPlineCodeC.GetValue().toString();
            planCode = comboPlanCodeC.GetValue().toString();

            grid4.PerformCallback(pline + "," + planCode);
        }
        function initGridview5() {
            if (comboPlineCodeC.GetValue() == null) {
                alert("请先选择生产线！");
                return;
            }
            pline = comboPlineCodeC.GetValue().toString();
            planCode = comboPlanCodeC.GetValue().toString();

            grid5.PerformCallback(pline + "," + planCode);
        }


        // ]]> 
    </script>
    <form id="form1" runat="server">
    <div style="float: left">
        <table style="width:100%">
            <tr>
                <td style="width:40%">
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="在线发动机清单" Width="40%">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 70px; text-align: left;">
                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择生产线">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="width: 130px">
                                            <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="comboPlineCodeC" runat="server"
                                                Width="120px" Height="25px" ValueField="PLINE_CODE" TextField="PLINE_NAME">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { filterPlan(); }" />
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="CmdQry" ClientInstanceName="CmdQryC" runat="server" AutoPostBack="false"
                                                Text="物料计算" Width="100px">
                                                <ClientSideEvents Click="function(s,e){ CmdQrySure(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td style="text-align: left; width: 60px">
                                        </td>
                                        <td style="width: 140px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 70px; text-align: left;">
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择计划">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="width: 130px">
                                            <dx:ASPxComboBox ID="comboPlanCode" ClientInstanceName="comboPlanCodeC" runat="server"
                                                Width="120px" Height="25px" OnCallback="comboPlanCode_Callback">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { filterAspxgridview1(); }" />
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="text-align: left; width: 60px">
                                        </td>
                                        <td style="width: 140px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="BottomPadding">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="待选流水号:" />
                                            </div>
                                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid1" runat="server" AutoGenerateColumns="False"
                                                Width="200px" KeyFieldName="SN" Settings-VerticalScrollableHeight="340" Settings-ShowGroupPanel="false"
                                                Settings-ShowVerticalScrollBar="True" SettingsPager-Mode="ShowAllRecords" OnCustomCallback="ASPxGridView1_CustomCallback">
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                                        Width="40px">
                                                        <HeaderTemplate>
                                                            <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid1.SelectAllRowsOnPage(s.GetChecked()); }"
                                                                Style="margin-bottom: 0px" />
                                                        </HeaderTemplate>
                                                        <SelectButton Text="选择">
                                                        </SelectButton>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" Settings-AutoFilterCondition="Contains">
                                                        <Settings AutoFilterCondition="Contains"></Settings>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager NumericButtonCount="5">
                                                </SettingsPager>
                                                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="340"></Settings>
                                            </dx:ASPxGridView>
                                        </td>
                                        <td valign="middle" align="center" style="padding: 10px; width: 90px">
                                            <div>
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                                                    AutoPostBack="False" Text="增加 >" Width="90px" Height="23px" ToolTip="Add selected items">
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
                                                    AutoPostBack="False" Text="< 删除" Width="90px" Height="23px" ToolTip="Remove selected items">
                                                    <ClientSideEvents Click="function(s, e) { ShowPopupDelete(); }" />
                                                </dx:ASPxButton>
                                            </div>
                                        </td>
                                        <td colspan="2">
                                            <div class="BottomPadding">
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="参与统计流水号:" />
                                            </div>
                                            <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                                                Width="200px" KeyFieldName="SN" Settings-VerticalScrollableHeight="340" SettingsPager-Mode="ShowAllRecords"
                                                Settings-ShowGroupPanel="false" Settings-ShowVerticalScrollBar="True" OnCustomCallback="ASPxGridView2_CustomCallback1"
                                                Settings-ShowFilterRow="false">
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                                        Width="40px">
                                                        <HeaderTemplate>
                                                            <dx:ASPxCheckBox ID="SelectAllCheckBox2" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid2.SelectAllRowsOnPage(s.GetChecked()); }"
                                                                Style="margin-bottom: 0px" />
                                                        </HeaderTemplate>
                                                        <SelectButton Text="选择">
                                                        </SelectButton>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" Settings-AutoFilterCondition="Contains">
                                                        <Settings AutoFilterCondition="Contains"></Settings>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsPager NumericButtonCount="5">
                                                </SettingsPager>
                                                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="340"></Settings>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                </td>
                <td style="width:60%">
                    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="在制品物料清单" Width="60%">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
                                    ActiveTabIndex="0">
                                    
                                    <TabPages>
                                        <dx:TabPage Text="线上物料清单" Visible="true">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl1" runat="server">
                                                    <table style="width:100%">
                                                        <tr>
                                                            <td style="width:100px">
                                                                <dx:ASPxButton ID="cmdOnline" ClientInstanceName="cmdOnlineC" runat="server" AutoPostBack="false"
                                                                    Text="线上物料清单" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) { initGridview3(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style=" text-align:left" >
                                                                <dx:ASPxButton ID="btnXlsExport1" ClientInstanceName="btnXlsExport1C" runat="server"
                                                                    AutoPostBack="false" Text="导出" Width="100px" OnClick="btnXlsExport1_Click">
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style="width:1000px"></td>
                                                        </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                        <tr>
                                                            <td >
                                                                <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="grid3" runat="server" KeyFieldName="ROWID"
                                                                    Width="100%" Settings-VerticalScrollableHeight="330" AutoGenerateColumns="False"
                                                                    Settings-ShowHeaderFilterButton="False" settingspager-NumericButtonCount="3"
                                                                    Settings-ShowGroupPanel="false" Settings-ShowFilterRow="false" OnCustomCallback="ASPxGridView3_CustomCallback">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="零件号" FieldName="MATERIAL_CODE" Width="30%">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="GYS_CODE" Width="30%" CellStyle-HorizontalAlign="left">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                            <CellStyle HorizontalAlign="Left">
                                                                            </CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="保管员" FieldName="IN_QADC01" Width="20%" CellStyle-HorizontalAlign="left">
                                                                            <Settings AutoFilterCondition="Contains"></Settings>
                                                                            <CellStyle HorizontalAlign="Left">
                                                                            </CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="线上数量" FieldName="ONLINE_NUM" Width="20%" CellStyle-HorizontalAlign="left">
                                                                            <CellStyle HorizontalAlign="Left">
                                                                            </CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <%--<dx:GridViewDataTextColumn Caption="" FieldName="" CellStyle-HorizontalAlign="left">
                                                                            
                                                                        </dx:GridViewDataTextColumn>--%>
                                                                    </Columns>
                                                                    <SettingsBehavior ColumnResizeMode="Control" />
                                                                    
                                                                    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="330" ShowHorizontalScrollBar="false"></Settings>
                                                                </dx:ASPxGridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                    <TabPages>
                                        <dx:TabPage Text="线边物料清单" Visible="true">
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl2" runat="server">
                                                    <table style="width:100%">
                                                        <tr>
                                                            <td style="width:100px">
                                                                <dx:ASPxButton ID="cmdLineside" ClientInstanceName="cmdLinesideC" runat="server"
                                                                    AutoPostBack="false" Text="线边物料清单" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) { initGridview4(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style=" text-align:left">
                                                                <dx:ASPxButton ID="btnXlsExport2" ClientInstanceName="btnXlsExport2C" runat="server"
                                                                    AutoPostBack="false" Text="导出" Width="100px" OnClick="btnXlsExport2_Click">
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style="width:1000px"></td>
                                                        </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                        <tr>
                                                            <td >
                                                                <dx:ASPxGridView ID="ASPxGridView4" ClientInstanceName="grid4" runat="server" KeyFieldName="ROWID"
                                                                    Width="100%" Settings-VerticalScrollableHeight="330" AutoGenerateColumns="False"
                                                                    Settings-ShowHeaderFilterButton="False"  settingspager-NumericButtonCount="3"
                                                                    Settings-ShowGroupPanel="false" Settings-ShowFilterRow="false" OnCustomCallback="ASPxGridView4_CustomCallback">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="零件号" FieldName="MATERIAL_CODE" Width="30%">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="GYS_CODE" Width="30%" CellStyle-HorizontalAlign="left">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                            <CellStyle HorizontalAlign="Left">
                                                                            </CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="保管员" FieldName="IN_QADC01" Width="20%" CellStyle-HorizontalAlign="left">
                                                                            <Settings AutoFilterCondition="Contains"></Settings>
                                                                            <CellStyle HorizontalAlign="Left">
                                                                            </CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="线边数量" FieldName="LINESIDE_NUM" Width="20%"  CellStyle-HorizontalAlign="left">
                                                                            <CellStyle HorizontalAlign="Left">
                                                                            </CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <%--<dx:GridViewDataTextColumn Caption="" FieldName="" CellStyle-HorizontalAlign="left">
                                                                            
                                                                        </dx:GridViewDataTextColumn>--%>
                                                                    </Columns>
                                                                    <SettingsBehavior ColumnResizeMode="Control" />
                                                                    
                                                                    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="330" ShowHorizontalScrollBar="false"></Settings>
                                                                </dx:ASPxGridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                    <TabPages>
                                        <dx:TabPage Text="线上线边对比" Visible="true" >
                                            <ContentCollection>
                                                <dx:ContentControl ID="ContentControl3" runat="server">
                                                    <table style="width:100%">
                                                        <tr>
                                                            <td style="width:100px">
                                                                <dx:ASPxButton ID="cmdCompare" ClientInstanceName="cmdCompareC" runat="server" AutoPostBack="false"
                                                                    Text="线上线边对比" Width="100px">
                                                                    <ClientSideEvents Click="function(s, e) { initGridview5(); }" />
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style=" text-align:left">
                                                                <dx:ASPxButton ID="btnXlsExport3" ClientInstanceName="btnXlsExport3C" runat="server"
                                                                    AutoPostBack="false" Text="导出" Width="100px" OnClick="btnXlsExport3_Click">
                                                                </dx:ASPxButton>
                                                            </td>
                                                            <td style="width:1000px"></td>
                                                        </tr>
                                                        </table>
                                                        <table style="width:100%">
                                                        <tr>
                                                            <td >
                                                                <dx:ASPxGridView ID="ASPxGridView5" ClientInstanceName="grid5" runat="server" KeyFieldName="ROWID"
                                                                    Width="100%" Settings-VerticalScrollableHeight="330" AutoGenerateColumns="False"
                                                                    Settings-ShowHeaderFilterButton="False" settingspager-NumericButtonCount="3"
                                                                    Settings-ShowGroupPanel="false" Settings-ShowFilterRow="false" OnCustomCallback="ASPxGridView5_CustomCallback">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn Caption="零件号" FieldName="MATERIAL_CODE" Width="25%">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="GYS_CODE" Width="20%" CellStyle-HorizontalAlign="left">
                                                                            <Settings AutoFilterCondition="Contains" />
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="保管员" FieldName="IN_QADC01" Width="15%" CellStyle-HorizontalAlign="left">
                                                                            <Settings AutoFilterCondition="Contains"></Settings>
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="线边数量" FieldName="LINESIDE_NUM" Width="20%" CellStyle-HorizontalAlign="left">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="线上数量" FieldName="ONLINE_NUM" Width="20%" CellStyle-HorizontalAlign="left">
                                                                            <CellStyle HorizontalAlign="Left">
                                                                            </CellStyle>
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                    <SettingsBehavior ColumnResizeMode="Control" />
                                                                    
                                                                    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="330"></Settings>
                                                                </dx:ASPxGridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxRoundPanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
