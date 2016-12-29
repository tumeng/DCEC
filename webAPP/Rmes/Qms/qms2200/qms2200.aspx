<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="qms2200.aspx.cs" Inherits="Rmes_qms2200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="RMES_ID"
    Settings-ShowHeaderFilterButton="false" >
    <Settings ShowFilterRow="True" />

    <Columns>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="计划号" FieldName="PLAN_CODE" VisibleIndex="1" Width="140px"/>
        <dx:GridViewDataTextColumn Caption="SN" FieldName="SN" VisibleIndex="3" Width="160px"/>
        

        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="5" Width="180px"/>
        <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_NAME" VisibleIndex="7" Width="120px"/>
        <dx:GridViewDataTextColumn Caption="站点" FieldName="STATION_NAME" VisibleIndex="9" Width="120px"/>

        <dx:GridViewDataTextColumn Caption="检测项代码" FieldName="DETECT_DATA_CODE" VisibleIndex="11" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="检验项名称" FieldName="DETECT_DATA_NAME" VisibleIndex="13" Width="120px"/>

        <dx:GridViewDataTextColumn Caption="最小值" FieldName="DETECT_DATA_DOWN" VisibleIndex="15" Width="60px"/>
        <dx:GridViewDataTextColumn Caption="最大值" FieldName="DETECT_DATA_UP" VisibleIndex="17" Width="60px"/>
        <dx:GridViewDataTextColumn Caption="实际值" FieldName="DETECT_DATA_VALUE" VisibleIndex="19" Width="80px" CellStyle-Wrap="False"/>
        
        <dx:GridViewDataTextColumn Caption="检验时间" FieldName="WORK_TIME" VisibleIndex="21" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="作业员" FieldName="USER_NAME" VisibleIndex="23" Width="80px"/>

        <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" VisibleIndex="25" Width="80px"/>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"/>

    </Columns>
</dx:ASPxGridView>

</asp:Content>
