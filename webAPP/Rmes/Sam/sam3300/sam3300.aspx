<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Theme1" Inherits="Rmes_sam3300" Title="" Codebehind="sam3300.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnRowDeleting="ASPxGridView1_RowDeleting">
    <Columns>
        <dx:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="40px">
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>

        <dx:GridViewDataComboBoxColumn Caption="操作人" FieldName="USER_ID" VisibleIndex="1" Width="80px" />
        <dx:GridViewDataTextColumn Caption="操作类别" FieldName="WORK_TYPE" VisibleIndex="3" Width="80px" />
        <dx:GridViewDataTextColumn Caption="记录事项1" FieldName="CONTENT_LOG1" VisibleIndex="5" Width="200px" >
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="记录事项2" FieldName="CONTENT_LOG2" VisibleIndex="7" Width="200px" >
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="记录事项3" FieldName="CONTENT_LOG3" VisibleIndex="9" Width="250px" >
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="影响行数" FieldName="AFFECT_ROWS" VisibleIndex="11" Width="70px" />
        <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" VisibleIndex="13" Width="150px" />
        <dx:GridViewDataTextColumn Caption="用户IP" FieldName="USER_IP" VisibleIndex="15" Width="100px" />

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%" />
    </Columns>
</dx:ASPxGridView>

</asp:Content>
