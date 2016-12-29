<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Theme1" Inherits="Rmes_Sam_sam2200_sam2200"
    MasterPageFile="~/MasterPage.master" CodeBehind="sam2200.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--角色权限定义--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;PROGRAM_CODE;ROLE_CODE;PLINE_CODE"
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
            <dx:GridViewDataTextColumn FieldName="ROLE_CODE" Caption="角色代码" VisibleIndex="1" Settings-AutoFilterCondition="Contains" 
                Width="80px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ROLE_NAME" Caption="角色名称" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="120px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PROGRAM_CODE" Caption="程序代码" VisibleIndex="3" Settings-AutoFilterCondition="Contains"
                Width="120px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PROGRAM_NAME" Caption="程序名称" VisibleIndex="4" Settings-AutoFilterCondition="Contains"
                Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PLINE_NAME" Caption="生产线" VisibleIndex="5" Settings-AutoFilterCondition="Contains"
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
                        <td style="width: 80px; text-align: left;">
                        </td>
                        <td style="width: 250px; text-align: left;">
                        </td>
                        <td style="width: 30px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="角色选择">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxGridLookup ID="GridLookupRole" runat="server"  ClientInstanceName="gridLookupRole"
                                DataSourceID="SqlRole" KeyFieldName="ROLE_CODE" MultiTextSeparator="," SelectionMode="Multiple"
                                TextFormatString="{1}" Width="250px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                    <dx:GridViewDataColumn Caption="角色代码" FieldName="ROLE_CODE" />
                                    <dx:GridViewDataColumn Caption="角色名称" FieldName="ROLE_NAME" />
                                </Columns>
                                 <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="角色信息有误，请重新输入！"
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RequiredField ErrorText="角色不能为空！" IsRequired="True" />
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
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="菜单选择">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxGridLookup ID="GridLookupProgram" runat="server" ClientInstanceName="gridLookupProgram" 
                                DataSourceID="SqlProgram" KeyFieldName="PROGRAM_CODE" MultiTextSeparator="," IncrementalFilteringMode="Contains"
                                SelectionMode="Multiple" TextFormatString="{1}" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="250px">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                    <dx:GridViewDataColumn Caption="程序代码" FieldName="PROGRAM_CODE" Settings-AutoFilterCondition="Contains"  Width="100px"></dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="程序名称" FieldName="PROGRAM_NAME"  Settings-AutoFilterCondition="Contains" Width="100px"/>
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
                                    <dx:GridViewDataColumn Caption="菜单代码" FieldName="PLINE_CODE" />
                                    <dx:GridViewDataColumn Caption="菜单名称" FieldName="PLINE_NAME" />
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
                        <td align="center">
                        </td>
                        <td align="right" colspan="2">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                        </td>
                        <td align="center">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td align="center">
                        </td>
                        <td align="center" colspan="2">
                        </td>
                        <td align="center">
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlProgram" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlRole" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPline" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
