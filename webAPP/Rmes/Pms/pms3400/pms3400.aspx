<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="pms3400.aspx.cs" Inherits="Rmes_pms3400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" Width="100%" KeyFieldName="SN"
    Settings-ShowHeaderFilterButton="false" >

    <Settings ShowFilterRow="True" />
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="40px">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="合同号" FieldName="PROJECT_CODE" VisibleIndex="1" Width="100px" Settings-AllowHeaderFilter="True" SortIndex="1" />
        <dx:GridViewDataTextColumn Caption="组件代号" FieldName="PRODUCT_MODEL" VisibleIndex="2" Width="100px" Settings-AllowHeaderFilter="True" SortIndex="2" />

        <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" VisibleIndex="3" Width="130px" SortIndex="3"/>
        <dx:GridViewDataTextColumn Caption="SN" FieldName="SN" VisibleIndex="4" Width="150px" SortIndex="4"/>
        <dx:GridViewDataTextColumn Caption="项目代号" FieldName="PLAN_SO" VisibleIndex="4" Width="150px" SortIndex="4"/>
        <dx:GridViewDataTextColumn Caption="项目名称" FieldName="PLAN_SO_NAME" VisibleIndex="4" Width="150px" SortIndex="4" CellStyle-Wrap="False"/>

        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="5" Width="180px"/>
        <%--<dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_NAME" VisibleIndex="7" Width="150px"/>--%>
        <dx:GridViewDataTextColumn Caption="站点" FieldName="STATION_NAME" VisibleIndex="9" Width="150px" SortIndex="5"/>
        <dx:GridViewDataTextColumn Caption="班组" FieldName="TEAM_NAME" VisibleIndex="11" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="班次" FieldName="SHIFT_NAME" VisibleIndex="13" Width="50px"/>

        <dx:GridViewDataTextColumn Caption="作业人员" FieldName="USER_NAME" VisibleIndex="15" Width="80px"/>

        <dx:GridViewDataTextColumn Caption="工作日期" FieldName="WORK_DATE" VisibleIndex="17" Width="135px"/>
        <dx:GridViewDataTextColumn Caption="开始时间" FieldName="START_TIME" VisibleIndex="19" Width="135px"/>
        <dx:GridViewDataTextColumn Caption="完成时间" FieldName="COMPLETE_TIME" VisibleIndex="21" Width="135px"/>

        
        <dx:GridViewDataTextColumn Caption="完成状态" FieldName="COMPLETE_FLAG" VisibleIndex="23" Width="60px" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="质量状态" FieldName="QUALITY_STATUS" VisibleIndex="25" Width="60px"/>


        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
    </dx:ASPxGridView>

</asp:Content>
