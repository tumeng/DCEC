<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes_epd2200" Codebehind="epd2200.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowValidating="ASPxGridView1_RowValidating"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px" />

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

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="车间代码" FieldName="WORKSHOP_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="车间名称" FieldName="WORKSHOP_NAME" VisibleIndex="2" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="车间地址" FieldName="WORKSHOP_ADDRESS" VisibleIndex="3" Width="220px"/>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

    <Templates>
        <EditForm>
            <table>
                <tr style="height: 10px">
                    <td colspan="7">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="车间代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtSubCode" runat="server" Width="160px" Text='<%# Bind("WORKSHOP_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过25！" ValidationExpression="^.{0,25}$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="车间名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtSubName" runat="server" Width="160px" Text='<%# Bind("WORKSHOP_NAME") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过25！" ValidationExpression="^.{0,25}$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="车间地址"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="dropPlineCode" runat="server" Width="150px" Value='<%# Bind("WORKSHOP_ADDRESS") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                    <td></td>
                </tr>

                <dx:ASPxTextBox ID="txtRmesID" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>' Visible="false" />
                <dx:ASPxTextBox ID="txtCompanyCode" runat="server" Width="160px" Text='<%# Bind("COMPANY_CODE") %>' Visible="false" />

                <tr style="height: 30px">
                    <td style="text-align: right;" colspan="6">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                        &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td colspan="7">
                    </td>
                </tr>

            </table>
        </EditForm>
    </Templates>
                    
    <ClientSideEvents 
        BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" 
        EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" 
    />
</dx:ASPxGridView>

</asp:Content>
