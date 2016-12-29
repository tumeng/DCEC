<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Theme1" Inherits="Rmes_sam2300"
    MasterPageFile="~/MasterPage.master" CodeBehind="sam2300.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--   人员权限定义--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true"
        ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Text="人员权限定义" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;USER_ID;PROGRAM_CODE;PLINE_CODE"
                            OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting"
                            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormHeight="220px" PopupEditFormWidth="400px" />
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" Caption=" ">
                                    <DeleteButton Visible="True">
                                    </DeleteButton>
                                    <NewButton Visible="true">
                                    </NewButton>
                                    <ClearFilterButton Visible="true">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="USER_ID" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="USER_CODE" Caption="用户代码" VisibleIndex="1"
                                    Width="80px" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="USER_NAME" Caption="用户名称" VisibleIndex="2"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PROGRAM_CODE" Caption="程序代码" VisibleIndex="3"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PROGRAM_NAME" Caption="程序名称" VisibleIndex="4"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PLINE_NAME" Caption="生产线" VisibleIndex="5"
                                    Width="80px">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
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
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="员工选择">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxGridLookup ID="GridLookupUser" runat="server" ClientInstanceName="gridLookupUser"
                                                    KeyFieldName="USER_ID" MultiTextSeparator="," SelectionMode="Multiple" TextFormatString="{2}"
                                                    DataSourceID="SqlUser" Width="250px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                                        <dx:GridViewDataColumn FieldName="USER_ID" Visible="false" />
                                                        <dx:GridViewDataColumn Caption="用户代码" FieldName="USER_CODE" />
                                                        <dx:GridViewDataColumn Caption="姓名" FieldName="USER_NAME" />
                                                    </Columns>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="员工信息有误，请重新输入！"
                                                        SetFocusOnError="True" ValidateOnLeave="True">
                                                        <RequiredField ErrorText="员工不能为空！" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxGridLookup>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px">
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="菜单选择">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxGridLookup ID="GridLookupProgram" runat="server" ClientInstanceName="gridLookupProgram"
                                                    KeyFieldName="PROGRAM_CODE" MultiTextSeparator="," SelectionMode="Multiple" TextFormatString="{1}"
                                                    DataSourceID="SqlProgram" Width="250px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                                        <dx:GridViewDataColumn Caption="程序代码" FieldName="PROGRAM_CODE" Settings-AutoFilterCondition="Contains" />
                                                        <dx:GridViewDataColumn Caption="程序名称" FieldName="PROGRAM_NAME" Settings-AutoFilterCondition="Contains"  />
                                                    </Columns>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="菜单信息有误，请重新输入！"
                                                        SetFocusOnError="True" ValidateOnLeave="True">
                                                        <RequiredField ErrorText="菜单不能为空！" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxGridLookup>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px">
                                            <td style="text-align: center;">
                                            </td>
                                            <td style="text-align: left;">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="生产线选择">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxGridLookup ID="GridLookupPline" runat="server" ClientInstanceName="gridLookupPline"
                                                    DataSourceID="SqlPline" KeyFieldName="PLINE_CODE" MultiTextSeparator="," SelectionMode="Multiple"
                                                    TextFormatString="{1}" Width="250px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                                        <dx:GridViewDataColumn Caption="生产线代码" FieldName="PLINE_CODE" />
                                                        <dx:GridViewDataColumn Caption="生产线名称" FieldName="PLINE_NAME" />
                                                    </Columns>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="生产线信息有误，请重新输入！"
                                                        SetFocusOnError="True" ValidateOnLeave="True">
                                                        <RequiredField ErrorText="生产线不能为空！" IsRequired="True" />
                                                    </ValidationSettings>
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
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="人员权限查询" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="USER_ID">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="60px">
                                    <%--<EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />--%>
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <%-- <dx:GridViewDataComboBoxColumn Caption="用户" VisibleIndex="1" Width="100px" FieldName="USER_NAME" Settings-AutoFilterCondition="Contains">
                                        <PropertiesComboBox DataSourceID="SqlUser" ValueField="USER_ID" TextField="USER_NAME"
                                            IncrementalFilteringMode="StartsWith" ValueType="System.String" DropDownStyle="DropDownList" />
                                    </dx:GridViewDataComboBoxColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="USER_ID" Visible="false">
                                </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn FieldName="USER_CODE" Caption="用户账户" VisibleIndex="2" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="USER_NAME" Caption="用户名称" VisibleIndex="2" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PROGRAM_CODE" Caption="程序代码" VisibleIndex="3"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PROGRAM_NAME" Caption="程序名称" VisibleIndex="4"
                                    Width="180px" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Caption="生产线代码" VisibleIndex="4"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <asp:SqlDataSource ID="SqlProgram" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlUser" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPline" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
