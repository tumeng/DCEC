<%@ Page Title="JIT特殊物料查询" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="part2000.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part2000.part2000" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>

<%--JIT特殊物料查询 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="height: 34px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="查询条件">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboGzdd" runat="server" DataSourceID="sqlGzdd" ValueField="PLINE_CODE" TextField="PLINE_NAME"
                    ValueType="System.String" Width="120px">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="单据号:">
                </dx:ASPxLabel>
            </td>
            <%--<td>
                <dx:ASPxComboBox ID="ComboBillCode" runat="server" DataSourceID="sqlBillCode" TextField="BILL_CODE"
                    ValueType="System.String" Width="140px">
                </dx:ASPxComboBox>
            </td>--%>
            <td>
                <dx:ASPxGridLookup ID="ComboBillCode" runat="server" DataSourceID="sqlBillCode" Width="170px" AutoGenerateColumns="False"
                    ClientInstanceName="gridLookup" KeyFieldName="BILL_CODE" TextFormatString="{0}" SelectionMode="Single" >
                    <GridViewProperties>
                        <SettingsPager NumericButtonCount="5"></SettingsPager>
                    </GridViewProperties>
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                        <dx:GridViewDataColumn FieldName="BILL_CODE" Caption="单据号" />
                    </Columns>
                </dx:ASPxGridLookup>
            </td>
            <td>
                <dx:ASPxButton ID="queryBill" Text="查询" Width="90px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" OnClick="btnXlsExport_Click"
                    Width="140px" UseSubmitBehavior="false">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>

    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid"
        runat="server" KeyFieldName="ROWID">
        <SettingsEditing PopupEditFormWidth="550" PopupEditFormHeight="200" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="单据号" FieldName="BILL_CODE" VisibleIndex="1" Width="120px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划号" FieldName="PLAN_CODE" VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件号" FieldName="MATERIAL_CODE" VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件名称" FieldName="MATERIAL_NAME" VisibleIndex="5" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="MATERIAL_NUM" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="7" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="保管员" FieldName="BGY_CODE" VisibleIndex="8" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="日程单" FieldName="PO_NBR" VisibleIndex="9" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划员" FieldName="PART_CHARGER" VisibleIndex="10" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>

    <asp:SqlDataSource ID="sqlBillCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlGzdd" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
</asp:Content>
