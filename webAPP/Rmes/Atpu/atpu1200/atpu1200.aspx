<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="atpu1200.aspx.cs" Inherits="Rmes_atpu1200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--功能概述：流水号号段定义--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Text="流水号号段维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="RMES_ID" OnRowInserting="ASPxGridView1_RowInserting"
                            OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
                            OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
                            OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared" OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="最小值" FieldName="INITIAL_VALUE" VisibleIndex="2"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="当前值" FieldName="CURRENT_VALUE" VisibleIndex="3"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="最大值" FieldName="MAX_VALUE" VisibleIndex="4" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="增减标识" FieldName="INTERNAL_NAME" VisibleIndex="5"
                                    Width="80px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="是否正在使用" FieldName="ENABLE_FLAG1" VisibleIndex="5"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="是否正在使用" FieldName="ENABLE_FLAG" VisibleIndex="5"
                                    Width="100px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="提醒值" FieldName="WARNING_VALUE" VisibleIndex="5"
                                    Width="80px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="用户名" FieldName="VENDER_CODE" VisibleIndex="5"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
                                <EditForm>
                                    <table>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <dx:ASPxTextBox ID="txtID" runat="server" Width="150px" Text='<%# Bind("RMES_ID") %>'
                                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="false">
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtCvalue" runat="server" Width="150px" Text='<%# Bind("CURRENT_VALUE") %>'
                                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="false">
                                            </dx:ASPxTextBox>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="txtPCode" runat="server" Width="140px" Value='<%# Bind("PLINE_CODE") %>'
                                                    DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip" >
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="用户名">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtSName" runat="server" Width="150px" Text='<%# Bind("VENDER_CODE") %>'
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
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="最小值">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="txtSinitial" runat="server" Width="140px" Text='<%# Bind("INITIAL_VALUE") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                        <RegularExpression ErrorText="必须输入数字！" ValidationExpression="^[0-9]*$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="最大值">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtSmax" runat="server" Width="150px" Text='<%# Bind("MAX_VALUE") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                        <RegularExpression ErrorText="必须输入数字！" ValidationExpression="^[0-9]*$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="height: 10px">
                                            </td>
                                            <tr>
                                                <td style="width: 8px; height: 30px">
                                                </td>
                                                <td style="width: 80px">
                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="提醒值">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 170px">
                                                    <dx:ASPxTextBox ID="txtSwarning" runat="server" Width="140px" Text='<%# Bind("WARNING_VALUE") %>'
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                            ErrorDisplayMode="ImageWithTooltip">
                                                            <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                            <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                            <RegularExpression ErrorText="必须输入数字！" ValidationExpression="^[0-9]*$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="width: 1px">
                                                </td>
                                                <td style="width: 90px">
                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="增减标识">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 180px">
                                                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="150px" Value='<%# Bind("INCREASE_FLAG") %>'
                                                        DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                            ErrorDisplayMode="ImageWithTooltip">
                                                            <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                            <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                        </ValidationSettings>
                                                        <Items>
                                                            <dx:ListEditItem Text="增" Value="A" />
                                                            <dx:ListEditItem Text="减" Value="D" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td style="width: 1px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="7" style="height: 10px">
                                                </td>
                                                <tr>
                                                    <td style="height: 30px">
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="正在使用">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="140px" Value='<%# Bind("ENABLE_FLAG") %>'
                                                            DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                                ErrorDisplayMode="ImageWithTooltip">
                                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                            </ValidationSettings>
                                                            <Items>
                                                                <dx:ListEditItem Text="是" Value="Y" />
                                                                <dx:ListEditItem Text="否" Value="N" />
                                                            </Items>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px; text-align: right;" colspan="6">
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
                            <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="预留号维护" Visible="false">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="RMES_ID" OnRowInserting="ASPxGridView2_RowInserting"
                            OnRowDeleting="ASPxGridView2_RowDeleting" OnRowUpdating="ASPxGridView2_RowUpdating"
                            OnRowValidating="ASPxGridView2_RowValidating" OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated">
                            <%--<SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>--%>
                            <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="预留号" FieldName="SN" VisibleIndex="2" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="使用标识" FieldName="INTERNAL_NAME" VisibleIndex="3"
                                    Width="80px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Templates>
                                <EditForm>
                                    <table>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <dx:ASPxTextBox ID="txtID2" runat="server" Width="150px" Text='<%# Bind("RMES_ID") %>'
                                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="false">
                                            </dx:ASPxTextBox>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="txtPCode" runat="server" Width="140px" Value='<%# Bind("PLINE_CODE") %>'
                                                    DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="预留号">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtSN" runat="server" Width="150px" Text='<%# Bind("SN") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                        <RegularExpression ErrorText="必须输入数字！" ValidationExpression="^[0-9]*$" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px">
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="使用标识">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Width="140px" Value='<%# Bind("SN_FLAG") %>'
                                                    DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dx:ListEditItem Text="正常使用" Value="A" />
                                                        <dx:ListEditItem Text="限制使用" Value="B" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px; text-align: right;" colspan="6">
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
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
