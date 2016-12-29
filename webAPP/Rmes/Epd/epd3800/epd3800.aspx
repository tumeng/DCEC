<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_epd3800"  StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" Codebehind="epd3800.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxButton ID="ASPxButton1" runat="server" Text="新  增" AutoPostBack="false" Width="100px">
    <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
</dx:ASPxButton>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
        onprocesscolumnautofilter="ASPxGridView1_ProcessColumnAutoFilter" >
    <SettingsBehavior ColumnResizeMode="Control" />
    
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" Caption=" ">
            <DeleteButton Visible="True">
            </DeleteButton>
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false" />

        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1" Width="200px" Settings-AutoFilterCondition="Contains"/>

        <dx:GridViewDataTextColumn Caption="站点代码" FieldName="STATION_CODE1" VisibleIndex="3" Width="120px" Settings-AutoFilterCondition="Contains"/>
        <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="5" Width="220px" Settings-AutoFilterCondition="Contains"/>

        <dx:GridViewDataTextColumn Caption="员工代码" FieldName="USER_CODE" VisibleIndex="7" Width="120px" Settings-AutoFilterCondition="Contains"/>
        <dx:GridViewDataTextColumn Caption="员工姓名" FieldName="USER_NAME" VisibleIndex="9" Width="220px" Settings-AutoFilterCondition="Contains"/>

        
        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>

<script type="text/javascript">

    function OpenAddWindow() {
        window.open('epd3801.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=800,height=800,top=150,left=250');
    }

</script>

</asp:Content>

