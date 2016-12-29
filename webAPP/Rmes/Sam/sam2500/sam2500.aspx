<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Sam_sam2500_sam2500"  StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" Codebehind="sam2500.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>

<%--用户生产线权限定义--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowInserting="ASPxGridView1_RowInserting"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsEditing PopupEditFormHeight="200px" PopupEditFormWidth="400px" />
                    
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" Caption=" ">
            <DeleteButton Visible="True">
            </DeleteButton>
            <NewButton Visible="true">
            </NewButton>
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="USER_ID" Visible="false">
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false">
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="USER_CODE" VisibleIndex="1" Caption="用户代码" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="USER_NAME" VisibleIndex="2" Caption="用户姓名" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="PLINE_NAME" VisibleIndex="3" Caption="生产线" Width="220px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

    <Templates>
        <EditForm>
            <table width="100%">
                <tr style="height: 30px">
                </tr>
                <tr>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 90px; text-align: left;">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="用户选择">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 250px">
                        <dx:ASPxGridLookup ID="GridLookupUser" runat="server" ClientInstanceName="gridLookupUser"
                            KeyFieldName="USER_ID" MultiTextSeparator="," SelectionMode="Multiple" TextFormatString="{2}"
                            DataSourceID="SqlUser" Width="250px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                <dx:GridViewDataColumn FieldName="USER_ID" Visible="false" />
                                <dx:GridViewDataColumn Caption="用户代码" FieldName="USER_CODE" />
                                <dx:GridViewDataColumn Caption="姓名" FieldName="USER_NAME" />
                            </Columns>
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="用户信息有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="用户不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxGridLookup>
                    </td>
                    <td style="width: 30px">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="width: 90px; text-align: left;">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线选择">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxGridLookup ID="GridLookupRole" runat="server" ClientInstanceName="gridLookupRole"
                            KeyFieldName="RMES_ID" MultiTextSeparator="," SelectionMode="Multiple" TextFormatString="{1}"
                            DataSourceID="SqlRole" Width="250px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                <dx:GridViewDataColumn FieldName="PLINE_CODE" Visible="false" />
                                <dx:GridViewDataColumn Caption="生产线" FieldName="PLINE_CODE" />
                                <dx:GridViewDataColumn Caption="生产线名称" FieldName="PLINE_NAME" />
                            </Columns>
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="生产线信息有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxGridLookup>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height: 20px">
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center" colspan="2" style="text-align: right;">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
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
