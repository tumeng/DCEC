<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Sam_sam1300_sam1300" Codebehind="sam1300.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ROLE_CODE" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowValidating="ASPxGridView1_RowValidating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsEditing PopupEditFormWidth="330" PopupEditFormHeight="350" />                    
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
            <NewButton Visible="True" Text="新增">
            </NewButton>
            <EditButton Visible="True" Text="修改">
            </EditButton>
            <DeleteButton Visible="True" Text="删除">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="角色代码" Name="ROLE_CODE" FieldName="ROLE_CODE" VisibleIndex="2" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="角色名称" Name="ROLE_NAME" FieldName="ROLE_NAME" VisibleIndex="3" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="角色描述" Name="ROLE_DESC" FieldName="ROLE_DESC" VisibleIndex="3" Width="700px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <Templates>
        <EditForm>
            <table style="width:300px">
                <tr>
                    <td style="height: 30px" colspan="7">
                    </td>
                </tr>
                <tr>
                    <td style="width: 8px; height: 30px">
                    </td>
                    <td style="width: 70px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="角色代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 190px">
                        <dx:ASPxTextBox ID="txtUserCode" runat="server" Width="160px" Text='<%# Bind("ROLE_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <%--<RequiredField IsRequired="True" ErrorText="不能为空！" />--%>
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                   </tr>
                   <tr>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 70px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="角色名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 190px">
                        <dx:ASPxTextBox ID="txtUserName" runat="server" Width="160px" Text='<%# Bind("ROLE_NAME") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 70px">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="角色描述">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 190px">
                        <%--<dx:ASPxTextBox ID="txtUserDesc" runat="server" Width="160px" Text='<%# Bind("ROLE_DESC") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                        </dx:ASPxTextBox>--%>
                        <dx:ASPxMemo ID="txtUserDesc" runat="server" Text='<%# Bind("ROLE_DESC") %>' Width="160px" Border-BorderStyle="None" 
                          ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Height="120px">
                        </dx:ASPxMemo>
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: right;">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                        &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px" colspan="7">
                    </td>
                </tr>
            </table>
        </EditForm>
    </Templates>
                    
    <ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
</dx:ASPxGridView>


</asp:Content>
