<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="atpu1800.aspx.cs" Inherits="Rmes_atpu1800" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--功能概述：金未来凸出量维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true"
        ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Text="SO凸出量维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="SO" OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
                            OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
                            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn Caption="生产线" VisibleIndex="1" Width="100px" FieldName="PLINE_CODE">
                                    <PropertiesComboBox DataSourceID="SqlCode" ValueField="PLINE_CODE" TextField="PLINE_NAME"
                                        IncrementalFilteringMode="StartsWith" ValueType="System.String" DropDownStyle="DropDownList" />
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="SO号" FieldName="SO" VisibleIndex="1" Width="120px" />
                                <dx:GridViewDataTextColumn Caption="机型" FieldName="JX" VisibleIndex="2" Width="100px" />
                                <dx:GridViewDataTextColumn Caption="凸出量一" FieldName="TCL" VisibleIndex="3" Width="100px" />
                                <dx:GridViewDataTextColumn Caption="凸出量二" FieldName="TCL1" VisibleIndex="4" Width="120px" />
                                <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" VisibleIndex="5" Width="120px" />
                                <dx:GridViewDataTextColumn Caption="缸号" FieldName="GNUM" VisibleIndex="6" Width="100px" />
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
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="txtPCode" runat="server" Width="140px" Value='<%# Bind("PLINE_CODE") %>'
                                                    DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="SO号">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxComboBox ID="txtSO" runat="server" Width="150px" Value='<%# Bind("SO") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
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
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="凸出量一">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="txtTCL" runat="server" Width="140px" Text='<%# Bind("TCL") %>'
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
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="凸出量二">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtTCL1" runat="server" Width="150px" Text='<%# Bind("TCL1") %>'
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
                                            <td colspan="7" style="height: 10px">
                                            </td>
                                            <tr>
                                                <td style="width: 8px; height: 30px">
                                                </td>
                                                <td style="width: 80px">
                                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="机型">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 170px">
                                                    <dx:ASPxTextBox ID="txtJX" runat="server" Width="140px" Text='<%# Bind("JX") %>'
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
                                                <td style="width: 90px">
                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="缸号">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 180px">
                                                    <dx:ASPxTextBox ID="txtGNUM" runat="server" Width="150px" Text='<%# Bind("GNUM") %>'
                                                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                            ErrorDisplayMode="ImageWithTooltip">
                                                            <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                            <RequiredField IsRequired="True" ErrorText="不能为空！" />
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
                                                    <td style="height: 30px">
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="备注">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxTextBox ID="txtBZ" runat="server" Width="140px" Text='<%# Bind("REMARK") %>'
                                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                                ErrorDisplayMode="ImageWithTooltip">
                                                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                            </ValidationSettings>
                                                        </dx:ASPxTextBox>
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
                confirm('确认要删除这条记录吗？');
                alert(theRet);
            }
        }" />
                        </dx:ASPxGridView>
                        <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
                            ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="油封维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="JXREMARK" OnRowInserting="ASPxGridView2_RowInserting"
                            OnRowDeleting="ASPxGridView2_RowDeleting" OnRowUpdating="ASPxGridView2_RowUpdating"
                            OnRowValidating="ASPxGridView2_RowValidating" OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="False" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="机型备注" FieldName="JXREMARK" VisibleIndex="1" Width="120px" />
                                <dx:GridViewDataTextColumn Caption="判断" FieldName="PANDUAN" VisibleIndex="2" Width="100px" />
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <%--<SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>--%>
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
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="机型备注">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="txtJX" runat="server" Width="140px" Value='<%# Bind("JXREMARK") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="判断">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxComboBox ID="txtPD" runat="server" Width="140px" Value='<%# Bind("PANDUAN") %>'
                                                    DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dx:ListEditItem Text="YES" Value="YES" />
                                                        <dx:ListEditItem Text="NO" Value="NO" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
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
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
