<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inf1000.aspx.cs" Inherits="Rmes.WebApp.Rmes.Interface.inf1000.inf1000" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="AUFNR"
    OnRowDeleting="ASPxGridView1_RowDeleting">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0">
            <ClearFilterButton Text="清除" Visible="true" ></ClearFilterButton>
            <DeleteButton Text="撤销订单" Visible="true"></DeleteButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="订单号" FieldName="AUFNR" VisibleIndex="1" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="WERKS" VisibleIndex="2" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="产品编码" FieldName="MATNR" VisibleIndex="3" Width="150px"/>           
        <dx:GridViewDataTextColumn Caption="订单数量" FieldName="GAMNG" VisibleIndex="4" Width="60px"/>
        <dx:GridViewDataTextColumn Caption="管理员编码" FieldName="FEVOR" VisibleIndex="5" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="管理员名称" FieldName="FNAME" VisibleIndex="6" Width="160px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="计划员编码" FieldName="DISPO" VisibleIndex="7" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="计划员名称" FieldName="DNAME" VisibleIndex="7" Width="150px" CellStyle-HorizontalAlign="Center"/>
         
        <dx:GridViewDataTextColumn Caption="创建者" FieldName="CNAME" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataComboBoxColumn Caption="导入状态" FieldName="PRIND" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>
        
        <dx:GridViewDataTextColumn Caption="创建日期" FieldName="CDATE" VisibleIndex="12" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="开始日期" FieldName="GSTRS" VisibleIndex="12" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="完成日期" FieldName="GLTRS" VisibleIndex="12" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="开始时间" FieldName="GSUZS" VisibleIndex="12" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="完成时间" FieldName="GLUZS" VisibleIndex="12" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>

</asp:Content>

