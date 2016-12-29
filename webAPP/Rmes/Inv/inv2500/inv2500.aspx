<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inv2500.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv2500.inv2500" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>
    <tr>
    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="SAP订单号"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxComboBox runat="server" ID="orderCode">
            <Items>
                <dx:ListEditItem Text="全部" Value="All" Selected="true"/>
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel2" Text="MES计划号"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxComboBox runat="server" ID="planCode">
            <Items>
                <dx:ListEditItem Text="全部" Value="All" Selected="true"/>
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="生产线"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxComboBox runat="server" ID="plineCode">
            <Items>
                <dx:ListEditItem Text="全部" Value="All" Selected="true"/>
            </Items>
        </dx:ASPxComboBox>
    </td>
    
    <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查   询" UseSubmitBehavior="False"
                    OnClick="ASPxButton1_Click"/>
    </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>

    <Columns>
        <dx:GridViewDataTextColumn Caption="" FieldName="RMES_ID" VisibleIndex="0" Width="150px" Visible="false"/>
        <dx:GridViewDataComboBoxColumn Caption="订单号" FieldName="ORDER_CODE" VisibleIndex="1" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="计划编号" FieldName="PLAN_CODE" VisibleIndex="2" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="3" Width="150px"/>           
        <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" VisibleIndex="5" Width="250px"/>
        <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="6" Width="60px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="SN" FieldName="SN" VisibleIndex="7" Width="250px"/>
        <dx:GridViewDataTextColumn Caption="车间" FieldName="WORKSHOP_CODE" VisibleIndex="7" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>
        
        <dx:GridViewDataTextColumn Caption="工艺" FieldName="PROCESS_CODE" VisibleIndex="9" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataDateColumn Caption="消耗时刻" FieldName="WORK_TIME" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>
         <%--<dx:GridViewDataTextColumn Caption="单位" FieldName="ITEM_UNIT" VisibleIndex="12" Width="150px" CellStyle-HorizontalAlign="Center"/>  --%>         
        <dx:GridViewDataTextColumn Caption="零件重要级别" FieldName="ITEM_CLASS_CODE" VisibleIndex="13" Width="60px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="供应商" FieldName="VENDOR_CODE" VisibleIndex="14" Width="60px"/>
                    
        

        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>

</asp:Content>
