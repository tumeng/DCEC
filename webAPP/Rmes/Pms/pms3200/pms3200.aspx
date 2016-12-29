<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="pms3200.aspx.cs" Inherits="Rmes_pms3200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" Width="100%" KeyFieldName="RMES_ID"
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

        <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" VisibleIndex="3" Width="130px" SortIndex="3" SortOrder="Ascending"/>
        <dx:GridViewDataTextColumn Caption="SN" FieldName="SN" VisibleIndex="4" Width="150px" SortIndex="4" SortOrder="Ascending"/>

        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="5" Width="180px"/>
        <dx:GridViewDataComboBoxColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="7" Width="150px" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="9" Width="120px" SortIndex="6"/>
        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="PROCESS_NAME" VisibleIndex="11" Width="200px"/>

        <dx:GridViewDataTextColumn Caption="完成标志" FieldName="COMPLETE_FLAG" VisibleIndex="13" Width="60px">
            <DataItemTemplate>
                <%# GetCellText(Container)%>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="工作日期" FieldName="WORK_DATE" VisibleIndex="15" Width="135px" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="开始时间" FieldName="START_TIME" VisibleIndex="17" Width="135px" />
        <dx:GridViewDataTextColumn Caption="完成时间" FieldName="COMPLETE_TIME" VisibleIndex="19" Width="135px" SortIndex="5"  SortOrder="Descending"/>

        <dx:GridViewDataTextColumn Caption="工艺路线" FieldName="ROUTING_CODE" VisibleIndex="21" Width="120px"  Visible="false"/>
        <dx:GridViewDataTextColumn Caption="工艺名称" FieldName="ROUTING_NAME" VisibleIndex="23" Width="200px" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="上级路线" FieldName="PARENT_ROUTING_CODE" VisibleIndex="25" Width="70px" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="序号" FieldName="PROCESS_SEQ" VisibleIndex="27" Width="50px" Visible="false"/>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
    </dx:ASPxGridView>

</asp:Content>
