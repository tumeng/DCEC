<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="sam1201.aspx.cs" Inherits="Rmes_Sam_sam1200_sam1201" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;USER_CODE" OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowValidating="ASPxGridView1_RowValidating" >
    <SettingsEditing PopupEditFormWidth="600" PopupEditFormHeight="150" />
        
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" Caption="操作">
            <NewButton Visible="True">
            </NewButton>
            <DeleteButton Visible="True">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="USER_ID" VisibleIndex="3" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" VisibleIndex="2" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户代码" FieldName="USER_CODE" VisibleIndex="4" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="姓名" FieldName="USER_NAME" VisibleIndex="5" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
        
    <ClientSideEvents EndCallback="function(s, e) {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCompanyName;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
            if(callbackName == 'Ori') 
            {
                alert('重置密码成功！');
            }
        }" BeginCallback="function(s, e) {
	        grid.cpCallbackName = '';
        }" />
            
    <Templates>
        <EditForm>
            <center>
                <table width="95%">
                    <br />
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="width: 12%; text-align: left;">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="用户代码" AssociatedControlID="txtUCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 39%">
                            <dx:ASPxTextBox ID="txtUCode" EnableClientSideAPI="True" runat="server" Width="100%"
                                Text='<%# Bind("USER_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="用户代码有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="用户代码字节长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="用户代码不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8%; text-align: left;">
                            <dx:ASPxLabel ID="Label2" runat="server" Text="姓名" AssociatedControlID="txtUName">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 39%">
                                                    <dx:ASPxTextBox ID="txtUName" runat="server" Width="100%" Text='<%# Bind("USER_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="姓名有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="姓名字节长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="姓名不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td colspan="5" align="right">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                                <asp:HiddenField ID="HidUserId" runat="server" Value='<%# Bind("USER_ID") %>'></asp:HiddenField>
                        </td>
                    </tr>
                </table>
            </center>
        </EditForm>
    </Templates>
        
</dx:ASPxGridView>

</asp:Content>

