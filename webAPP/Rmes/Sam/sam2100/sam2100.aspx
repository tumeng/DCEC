<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Sam_sam2100_sam2100" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" Codebehind="sam2100.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;USER_ID;ROLE_CODE"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowInserting="ASPxGridView1_RowInserting"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsEditing PopupEditFormHeight="200px" PopupEditFormWidth="400px" />
                    
    <Columns>
        <dx:GridViewCommandColumn Name="CommondColumn" VisibleIndex="0" Width="80px" Caption=" ">
            <DeleteButton Visible="True">
            </DeleteButton>
            <NewButton Visible="true">
            </NewButton>
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="USER_CODE" Caption="用户代码" VisibleIndex="1" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="USER_NAME" Caption="用户姓名" VisibleIndex="2" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="ROLE_CODE" Caption="角色代码" VisibleIndex="3" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="ROLE_NAME" Caption="角色名称" VisibleIndex="4" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <Templates>
        <EditForm>
            <table width="400px">
                <tr style="height: 30px">
                    <td style="width: 30px; text-align: center;">
                    </td>
                    <td style="width: 80px; text-align: center;">
                    </td>
                    <td style="width: 250px">
                    </td>
                    <td style="width: 30px">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="用户选择">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxGridLookup ID="GridLookupUser" runat="server" ClientInstanceName="gridLookupUser"
                            DataSourceID="SqlUser" KeyFieldName="USER_ID" MultiTextSeparator="," SelectionMode="Multiple"
                            TextFormatString="{2}" Width="250px">
                            <Columns>
                                <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                <dx:GridViewDataColumn FieldName="USER_ID" Visible="false" />
                                <dx:GridViewDataColumn Caption="用户代码" FieldName="USER_CODE" />
                                <dx:GridViewDataColumn Caption="姓名" FieldName="USER_NAME" />
                            </Columns>
                        </dx:ASPxGridLookup>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="权限选择">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxGridLookup ID="GridLookupRole" runat="server" ClientInstanceName="gridLookupRole"
                            KeyFieldName="ROLE_CODE" MultiTextSeparator="," SelectionMode="Multiple" TextFormatString="{1}"
                            DataSourceID="SqlRole" Width="250px">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                <dx:GridViewDataColumn Caption="角色代码" FieldName="ROLE_CODE" />
                                <dx:GridViewDataColumn Caption="名称" FieldName="ROLE_NAME" />
                            </Columns>
                        </dx:ASPxGridLookup>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                    </td>
                    <td colspan="2" align="right">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                    </td>
                    <td colspan="2">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </EditForm>
    </Templates>                    
</dx:ASPxGridView>

<asp:SqlDataSource ID="SqlUser" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlRole" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>

</asp:Content>
