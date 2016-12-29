<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd3A00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd3A00.epd3A00" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<dx:ASPxButton ID="ASPxButton1" runat="server" Text="新  增" AutoPostBack="false" Width="100px">
    <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
</dx:ASPxButton>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnRowDeleting="ASPxGridView1_RowDeleting"  >
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

        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1" Width="120px" Settings-AutoFilterCondition="Contains" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINECODE1" VisibleIndex="1" Width="120px" Settings-AutoFilterCondition="Contains"/>

        <dx:GridViewDataTextColumn Caption="分装站点代码" FieldName="SUBSTATION_CODE" VisibleIndex="3" Width="110px" Settings-AutoFilterCondition="Contains" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="分装站点名称" FieldName="SUBSTATION_NAME" VisibleIndex="5" Width="150px" Settings-AutoFilterCondition="Contains"/>

        <dx:GridViewDataTextColumn Caption="触发站点代码" FieldName="CFSTATION_CODE" VisibleIndex="7" Width="110px" Settings-AutoFilterCondition="Contains" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="触发站点名称" FieldName="CFSTATION_NAME" VisibleIndex="9" Width="150px" Settings-AutoFilterCondition="Contains"/>

        <dx:GridViewDataTextColumn Caption="装配站点代码" FieldName="ZPSTATION_CODE" VisibleIndex="11" Width="110px" Settings-AutoFilterCondition="Contains" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="装配站点名称" FieldName="ZPSTATION_NAME" VisibleIndex="12" Width="150px" Settings-AutoFilterCondition="Contains"/>

        <dx:GridViewDataTextColumn Caption="分装总成号" FieldName="SUB_ZC" VisibleIndex="13" Width="110px" Settings-AutoFilterCondition="Contains"/>
        <dx:GridViewDataTextColumn Caption="校验方式" FieldName="CHECK_FLAG_NAME" VisibleIndex="14" Width="90px" Settings-AutoFilterCondition="Contains"/>
        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>

<script type="text/javascript">

    function OpenAddWindow() {
        window.open('epd3A01.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=800,height=800,top=150,left=250');
    }

</script>
</asp:Content>
