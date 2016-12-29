<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inv2400.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv2400.inv2400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="SN">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <Settings ShowHorizontalScrollBar="true" />

    <Columns>
        <dx:GridViewDataComboBoxColumn Caption="订单号" FieldName="ORDER_CODE" VisibleIndex="1" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="计划编号" FieldName="PLAN_CODE" VisibleIndex="2" Width="160px"/>
        <dx:GridViewDataTextColumn Caption="SN号" FieldName="SN" VisibleIndex="3" Width="150px"/>           
        <dx:GridViewDataTextColumn Caption="产品编号" FieldName="PRODUCT_CODE" VisibleIndex="4" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="入库地点" FieldName="STORE_CODE" VisibleIndex="5" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="入库数量" FieldName="INSTORE_QTY" VisibleIndex="6" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataComboBoxColumn Caption="收货类型" FieldName="INSTORE_TYPE_CODE" VisibleIndex="6" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="批次" FieldName="PLAN_BATCH" VisibleIndex="7" Width="150px" CellStyle-HorizontalAlign="Center"/>                
        
       
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>

</asp:Content>
