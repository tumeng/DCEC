<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd4200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd4200.epd4200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" AutoGenerateColumns="false"  KeyFieldName="RMES_ID">
    <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600px"/>
    <SettingsBehavior ColumnResizeMode="Control"/>
    <Columns>
        <dx:GridViewDataTextColumn Caption="" FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="" FieldName="COMPANY_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" Width="150px" />
        <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" />
        <dx:GridViewDataTextColumn Caption="英文名称" FieldName="ITEM_NAME_EN" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="物料类型" FieldName="ITEM_TYPE" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="重要级别" FieldName="ITEM_CLASS_CODE" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="最小包装量" FieldName="MIN_PACK_QTY" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="单位" FieldName="UNIT_CODE" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="规格" FieldName="ITEM_SPEC" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="型号" FieldName="ITEM_MODEL" Width="160px"/>

        <dx:GridViewDataTextColumn Caption="" VisibleIndex="99" />

    </Columns>
</dx:ASPxGridView>

</asp:Content>
