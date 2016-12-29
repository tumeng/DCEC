<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes_inv2200" Codebehind="inv2200.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<table>
    <tr>
    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="生产线"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxComboBox runat="server" ID="plineCode">
            <Items>
                <dx:ListEditItem Text="全部" Value="All" Selected="true"/>
            </Items>
            <ClientSideEvents SelectedIndexChanged="function (s,e){
                storeCode.PerformCallback(s.lastSuccessValue);
                storeCode.SetSelectedIndex(0);
            }" />
        </dx:ASPxComboBox>
    </td>

    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="线边库"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxComboBox runat="server" ID="storeCode" ClientInstanceName="storeCode" OnCallback="storeCode_Callback">
            <Items>
                <dx:ListEditItem Text="全部" Value="All" Selected="true"/>
            </Items>
        </dx:ASPxComboBox>
    </td>
    
    <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查   询" UseSubmitBehavior="False"
                    OnClick="ASPxButton1_Click"/>
    </td>
    
    <td>
        <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
    </td>
    </tr>
</table>

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
        >
        <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>
    <Settings ShowHorizontalScrollBar="true"  ShowFilterRow="true"/>
    <Columns>

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="1" Width="180px"/>
        <dx:GridViewDataTextColumn Caption="物料数量" FieldName="ITEM_QTY" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="FACTORY_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="线边库" FieldName="STORE_CODE" VisibleIndex="1" Width="100px"/>
        
        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>


</dx:ASPxGridView>

<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>

</asp:Content>
