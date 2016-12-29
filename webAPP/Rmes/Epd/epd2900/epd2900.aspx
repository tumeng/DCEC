<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_epd2900"  Codebehind="epd2900.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="100%">
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线：">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="CombPlCodeQ" runat="server" AutoPostBack="true" DropDownStyle="DropDownList"  
                            Width="150" OnSelectedIndexChanged="CombPlCodeQ_SelectedIndexChanged">
                        </dx:ASPxComboBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;PLINE_CODE;USER_ID" OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowValidating="ASPxGridView1_RowValidating">

    <SettingsEditing PopupEditFormHeight="150px" PopupEditFormWidth="350px" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" Caption="操作">
            <DeleteButton Visible="True">
            </DeleteButton>
            <NewButton Visible="true">
            </NewButton>
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="USER_ID" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="TEAM_CODE" Caption="班组代码" VisibleIndex="1" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="TEAM_NAME" Caption="班组名称" VisibleIndex="2" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="USER_CODE" Caption="用户代码" VisibleIndex="3" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="USER_NAME" Caption="姓名" VisibleIndex="4" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <%--<ClientSideEvents EndCallback="function(s, e) {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCompanyName;
            if(callbackName == 'Check') 
            {
                alert(theRet+'已经被分配过班组！');
            }
                            
        }" BeginCallback="function(s, e) {
	        grid.cpCallbackName = '';
        }" />   --%>
                        
    <Templates>
        <EditForm>
            <table width="90%">
                <br />
                <tr>
                    <td style="width: 15%; text-align: center;">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="班组">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 85%">
                        <%--<dx:ASPxComboBox ID="CombTeam" runat="server" Width="250">
                        </dx:ASPxComboBox>--%>
                        <dx:ASPxGridLookup ID="GridLookupTeam" runat="server" ClientInstanceName="GridLookupTeam"
                            KeyFieldName="TEAM_CODE" MultiTextSeparator="," SelectionMode="Single" TextFormatString="{1}"
                            DataSourceID="SqlTeam" Width="250px">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                <dx:GridViewDataColumn Caption="班组代码" FieldName="TEAM_CODE" />
                                <dx:GridViewDataColumn Caption="班组名称" FieldName="TEAM_NAME" />
                            </Columns>
                        </dx:ASPxGridLookup>
                    </td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="用户">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxGridLookup ID="GridLookupUser" runat="server" ClientInstanceName="gridLookupUser"
                            KeyFieldName="USER_ID" MultiTextSeparator="," SelectionMode="Multiple" TextFormatString="{2}"
                            DataSourceID="SqlUser" Width="250px">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                <dx:GridViewDataColumn FieldName="USER_ID" Visible="false" />
                                <dx:GridViewDataColumn Caption="用户代码" FieldName="USER_CODE" />
                                <dx:GridViewDataColumn Caption="姓名" FieldName="USER_NAME" />
                            </Columns>
                        </dx:ASPxGridLookup>
                    </td>
                </tr>
                <tr style="height: 20px">
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                    </td>
                </tr>
            </table>
        </EditForm>
    </Templates>
</dx:ASPxGridView>
    
    <asp:SqlDataSource ID="SqlPLine" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlUser" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlTeam" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    
</asp:Content>

