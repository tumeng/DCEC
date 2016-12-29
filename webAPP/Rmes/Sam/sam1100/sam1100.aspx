<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Sam_sam1100_sam1100" Title="" Codebehind="sam1100.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowValidating="ASPxGridView1_RowValidating">
    <SettingsEditing PopupEditFormWidth="550" />
    <Columns>
        <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="100px">
            <EditButton Visible="True">
            </EditButton>
            <NewButton Visible="True">
            </NewButton>
            <DeleteButton Visible="True">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="公司代码" FieldName="COMPANY_CODE" VisibleIndex="1" Width="80px" SortIndex="1" SortOrder="Ascending">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="公司名称" FieldName="COMPANY_NAME" VisibleIndex="2" Width="200px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="公司简称" FieldName="COMPANY_NAME_BRIEF" VisibleIndex="3" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="英文名称" FieldName="COMPANY_NAME_EN" VisibleIndex="4" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="公司网址" FieldName="COMPANY_WEBSITE" VisibleIndex="5" Width="200px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="公司地址" FieldName="COMPANY_ADDRESS" VisibleIndex="6" Width="400px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>        
        
    <Templates>
        <EditForm>
            <center>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; height: 30px">
                        </td>
                        <td style="width: 80px; text-align: left;">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="公司代码" AssociatedControlID="txtCCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="txtCCode" runat="server" Width="150px" Text='<%# Bind("COMPANY_CODE") %>'
                                EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="公司代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="公司代码不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td style="width: 80px; text-align: left;">
                            <dx:ASPxLabel ID="Label2" runat="server" Text="公司名称" AssociatedControlID="txtCName">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="txtCName" runat="server" Width="150px" Text='<%# Bind("COMPANY_NAME") %>'
                                EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="公司名称字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="公司名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label4" runat="server" Text="公司简称" AssociatedControlID="txtCNameBrief">
                            </dx:ASPxLabel>
                        </td>
                        <td style="align="left">
                            <dx:ASPxTextBox ID="txtCNameBrief" runat="server" Width="150px" Text='<%# Bind("COMPANY_NAME_BRIEF") %>'
                                EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="公司简称字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="公司简称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label5" runat="server" Text="英文名称" AssociatedControlID="txtCNameEn">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtCNameEn" runat="server" Width="150px" Text='<%# Bind("COMPANY_NAME_EN") %>'
                                EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="英文名称字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="英文名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="公司网址" AssociatedControlID="txtCWebsite">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left">
                            <dx:ASPxTextBox ID="txtCWebsite" runat="server" Width="150px" Text='<%# Bind("COMPANY_WEBSITE") %>'
                                EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="公司网址节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="公司网址不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label6" runat="server" Text="公司地址" AssociatedControlID="txtCAddress">
                            </dx:ASPxLabel>
                        </td>
                        <td style="align="left">
                            <dx:ASPxTextBox ID="txtCAddress" runat="server" Width="150px" Text='<%# Bind("COMPANY_ADDRESS") %>'
                                EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="公司地址节长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="right">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px" colspan="7">
                        </td>
                    </tr>
                </table>
            </center>
        </EditForm>
    </Templates>
        
    <ClientSideEvents BeginCallback="function(s, e) {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCompanyName;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />        
        
</dx:ASPxGridView>

</asp:Content>
