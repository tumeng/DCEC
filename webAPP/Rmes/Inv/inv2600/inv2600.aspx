<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inv2600.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv2600.inv2600" %>

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

        <dx:GridViewDataComboBoxColumn Caption="计划编号" FieldName="PLAN_CODE" VisibleIndex="1" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="2" Width="150px"/>           
        <dx:GridViewDataComboBoxColumn Caption="工作中心" FieldName="WORKUNIT_CODE" VisibleIndex="3" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工序代码" FieldName="PROCESS_CODE" VisibleIndex="4" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="PROCESS_NAME" VisibleIndex="5" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工艺代码" FieldName="ROUTING_CODE" VisibleIndex="6" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工艺代码" FieldName="ROUTING_NAME" VisibleIndex="7" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataComboBoxColumn Caption="状态标识" FieldName="COMPLETE_FLAG" VisibleIndex="8" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="操作员" FieldName="USER_ID" VisibleIndex="9" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataComboBoxColumn Caption="质量状态" FieldName="QUALITY_STATUS" VisibleIndex="10" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工序开始时间" FieldName="START_TIME" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工序开始时间" FieldName="COMPLETE_FLAG" VisibleIndex="12" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataComboBoxColumn Caption="工作类型" FieldName="WORKTYPE_FLAG" VisibleIndex="13" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataComboBoxColumn Caption="工作中心" FieldName="WORKUNIT_CODE" VisibleIndex="14" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataComboBoxColumn Caption="车间" FieldName="WORKSHOP_CODE" VisibleIndex="15" Width="80px" CellStyle-HorizontalAlign="Center"/>           
        <dx:GridViewDataComboBoxColumn Caption="完成数量" FieldName="COMPLETE_QTY" VisibleIndex="16" Width="80px" CellStyle-HorizontalAlign="Center"/>

        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>

</asp:Content>
