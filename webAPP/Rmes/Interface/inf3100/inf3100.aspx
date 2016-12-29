<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inf3100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Interface.inf3100.inf3100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>
    <tr>
    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="SAP订单号"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxTextBox runat="server" ID="txtOrderCode" Width="180px"></dx:ASPxTextBox>    
    </td>
    
    <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查   询" UseSubmitBehavior="False"
                    OnClick="ASPxButton1_Click"/>
    </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="AUFNR">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0">
            <ClearFilterButton Text="清除" Visible="true" ></ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="订单号" FieldName="AUFNR" VisibleIndex="1" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="WERKS" VisibleIndex="2" Width="160px"/>
        <dx:GridViewDataTextColumn Caption="产品编码" FieldName="MATNR" VisibleIndex="3" Width="150px"/>           
        <dx:GridViewDataTextColumn Caption="工序步骤" FieldName="VORNR" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="LTXA1" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="物料编码" FieldName="SUBMAT" VisibleIndex="5" Width="250px"/>
        <dx:GridViewDataTextColumn Caption="物料描述" FieldName="MATKT" VisibleIndex="5" Width="250px"/>
        <dx:GridViewDataTextColumn Caption="需求数量" FieldName="MENGE" VisibleIndex="6" Width="60px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="线边库" FieldName="LGORT" VisibleIndex="7" Width="250px"/>
        <dx:GridViewDataTextColumn Caption="来源库" FieldName="LGORT_F" VisibleIndex="7" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="批号（任务号）" FieldName="BATCH" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>  
        <dx:GridViewDataComboBoxColumn Caption="导入状态" FieldName="PRIND" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataDateColumn Caption="组件需求日期" FieldName="BDTER" VisibleIndex="12" ></dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>

</asp:Content>

