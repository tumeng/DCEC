<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_sam2000"  StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" Codebehind="sam2000.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxButton ID="ASPxButton1" runat="server" Text="新  增" AutoPostBack="false" Width="100px">
    <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
</dx:ASPxButton>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="USER_ID;PROGRAM_CODE;PLINE_CODE"
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
       
    
        

        <dx:GridViewDataTextColumn Caption="用户代码" FieldName="USER_ID" VisibleIndex="1" Width="120px" Settings-AutoFilterCondition="Contains"/>

        <dx:GridViewDataTextColumn Caption="用户名称" FieldName="USER_NAME" VisibleIndex="2" Width="120px" Settings-AutoFilterCondition="Contains"/>
        <dx:GridViewDataTextColumn Caption="菜单权限" FieldName="PROGRAM_NAME" VisibleIndex="3" Width="220px" Settings-AutoFilterCondition="Contains"/>
        <dx:GridViewDataTextColumn Caption="菜单代码" FieldName="PROGRAM_CODE" Visible="false" />

        <dx:GridViewDataTextColumn Caption="生产线权限" FieldName="PLINE_CODE" VisibleIndex="4" Width="120px" Settings-AutoFilterCondition="Contains"/>
       
        
        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>

<script type="text/javascript">

    function OpenAddWindow() {
        window.open('sam2001.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=800,height=800,top=150,left=250');
    }

</script>

</asp:Content>

