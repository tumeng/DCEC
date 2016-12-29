<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Rmes_atpu1700" CodeBehind="atpu1700.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--功能概述：排放参数维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        KeyFieldName="SO" OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
        OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsEditing PopupEditFormWidth="530px" />
        <SettingsBehavior ColumnResizeMode="Control" />
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
            <dx:GridViewDataTextColumn Caption="SO号" Name="SO" FieldName="SO" VisibleIndex="3"
                Width="120px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="颜色" Name="COLOR" FieldName="COLOR" VisibleIndex="4"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="程序号" Name="PROC" FieldName="PROC" VisibleIndex="5"
                Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="生产线" Name="SITE" FieldName="SITE" VisibleIndex="6"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <EditForm>
                <table>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 90px; text-align: left;">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="txtPCode" runat="server" Width="140px" Value='<%# Bind("SITE") %>'
                            DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="SO号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtSO" runat="server" Text='<%# Bind("SO") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="颜色">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtColor" runat="server" Text='<%# Bind("COLOR") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                            <%--<dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="140px" Value='<%# Bind("COLOR") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                               
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                            <Items>
                                <dx:ListEditItem Text="1" Value="1"/>
                                <dx:ListEditItem Text="2" Value="2" />
                                <dx:ListEditItem Text="3" Value="3" />
                                <dx:ListEditItem Text="4" Value="4" />
                            </Items>
                        </dx:ASPxComboBox>--%>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="程序号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="textPro" runat="server" Text='<%# Bind("PROC") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="150px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 30px; text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="height: 30px">
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
                confirm('确认要删除这条记录吗？');
                alert(theRet);
            }
             
        }" />
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
