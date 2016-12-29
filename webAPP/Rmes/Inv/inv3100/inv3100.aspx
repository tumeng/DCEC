<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes_inv3100" Codebehind="inv3100.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID">
    
    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="40px" FixedStyle="Left">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1" Width="150px"/>

        <dx:GridViewDataComboBoxColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="2" Width="150px"/>

        <dx:GridViewDataTextColumn Caption="项目代号" FieldName="ITEM_CODE" VisibleIndex="4" Width="150px">
            <Settings AutoFilterCondition="Contains" />  <%-- 支持模糊查询--%>
        </dx:GridViewDataTextColumn>
        <%--<dx:GridViewDataTextColumn Caption="项目名称" FieldName="ITEM_NAME" VisibleIndex="5" Width="230px">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>--%>

        <dx:GridViewDataTextColumn Caption="批次号" FieldName="ITEM_BATCH" VisibleIndex="6" Width="100px" />

        <dx:GridViewDataTextColumn Caption="库存数量" FieldName="ITEM_QTY" VisibleIndex="7" Width="150px" CellStyle-HorizontalAlign="Right"/>

        <dx:GridViewDataTextColumn Caption="供应商" FieldName="VENDOR_CODE" VisibleIndex="8" Width="150px" />


        <dx:GridViewBandColumn Caption=" " VisibleIndex="98"></dx:GridViewBandColumn>
    </Columns>

</dx:ASPxGridView>

</asp:Content>
