<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_epd3200" Title=""   Codebehind="epd3200.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;PLINE_CODE;EVENT_CODE" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnHeaderFilterFillItems="ASPxGridView1_HeaderFilterFillItems">
    <SettingsEditing Mode="Inline" />
    <SettingsBehavior ColumnResizeMode="Control"/>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="80" Caption="操作">
            <EditButton Visible="True">
            </EditButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" VisibleIndex="1" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataColumn Caption="生产线" FieldName="PLINE_NAME" ReadOnly="True" VisibleIndex="3" Width="80px">
            <EditItemTemplate>
                <asp:Label ID="labPLCode" runat="server" Text='<%# Bind("PLINE_CODE") %>' Visible="false" />
                <asp:Label ID="labPLname" runat="server" Text='<%# Bind("PLINE_NAME") %>' />
            </EditItemTemplate>
        </dx:GridViewDataColumn>
        <dx:GridViewDataTextColumn Caption="事件代码" FieldName="EVENT_CODE" VisibleIndex="4" Width="100px">
            <EditItemTemplate>
                <asp:Label ID="labECode" runat="server" Text='<%# Bind("EVENT_CODE") %>' />
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="事件名称" FieldName="EVENT_NAME" VisibleIndex="5" Width="200px">
            <EditItemTemplate>
                <asp:Label ID="labEName" runat="server" Text='<%# Bind("EVENT_NAME") %>' />
            </EditItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataCheckColumn Caption="是否控制" FieldName="EVENT_FLAG" Name="EVENT_FLAG" VisibleIndex="6" Width="80px" CellStyle-HorizontalAlign="Center" EditCellStyle-HorizontalAlign="Center">
            <PropertiesCheckEdit DisplayTextChecked="Yes" DisplayTextUnchecked="No" />
        </dx:GridViewDataCheckColumn>
        <dx:GridViewBandColumn Caption=" " VisibleIndex="98"></dx:GridViewBandColumn>
    </Columns>
</dx:ASPxGridView>

</asp:Content>

