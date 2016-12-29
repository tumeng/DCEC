<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="pms3100.aspx.cs" Inherits="Rmes_pms3100" %>

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
        <%--<dx:GridViewDataComboBoxColumn Caption="站点" FieldName="STATION_CODE" VisibleIndex="9" Width="120px"/>
        <dx:GridViewDataTextColumn Caption="站点IP" FieldName="STATION_IP" VisibleIndex="11" Width="120px"/>--%>

        <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="13" Width="60px" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="项目代码" FieldName="ITEM_CODE" VisibleIndex="15" Width="150px"  SortIndex="6"/>
        <dx:GridViewDataTextColumn Caption="项目名称" FieldName="ITEM_NAME" VisibleIndex="17" Width="230px"/>
        <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="19" Width="60px"/>
        <dx:GridViewDataTextColumn Caption="实际用量" FieldName="COMPLETE_QTY" VisibleIndex="20" Width="60px"/>
        <dx:GridViewDataTextColumn Caption="批次" FieldName="ITEM_BATCH" VisibleIndex="21" Width="50px"/>
        <dx:GridViewDataTextColumn Caption="供应商" FieldName="VENDOR_CODE" VisibleIndex="23" Width="100px"/>
        
        <dx:GridViewDataTextColumn Caption="操作人员" FieldName="USER_ID" VisibleIndex="25" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="操作时间" FieldName="WORK_TIME" VisibleIndex="27" Width="135px" SortIndex="5" SortOrder="Descending"/>
        <%--<dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" VisibleIndex="29" Width="120px"/>--%>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
    </dx:ASPxGridView>

</asp:Content>
