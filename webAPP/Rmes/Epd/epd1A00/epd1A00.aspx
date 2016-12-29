<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd1A00.aspx.cs" Inherits="Rmes_epd1A00" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="ALINE_CODE"
    OnRowValidating="ASPxGridView1_RowValidating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    
    OnRowUpdating="ASPxGridView1_RowUpdating" 
    OnRowInserting="ASPxGridView1_RowInserting">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="650px" />
        
    <Columns>
        <dx:GridViewCommandColumn Caption="  " VisibleIndex="0" Width="100px" Visible="True">
            <EditButton Visible="true"></EditButton>
            <NewButton Visible="true"></NewButton>
            <DeleteButton Visible="true"></DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="公司代码" FieldName="COMPANY_CODE" Visible="False">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="跨线模式代码" FieldName="ALINE_CODE" VisibleIndex="1" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="跨线模式名称" FieldName="ALINE_NAME" VisibleIndex="2" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" VisibleIndex="3" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线名称 " FieldName="PLINE_NAME" VisibleIndex="4" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
        
    <Templates>
        <EditForm>
            <table>
                <tr>
                    <td style="height: 10px" colspan="7">
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td style="width: 70px; text-align: left">
                        <dx:ASPxLabel ID="Label3" runat="server" Text="生产线" AssociatedControlID="ASPxComboBox1">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px;" align="left">
                        <dx:ASPxComboBox ID="ASPxComboBox1" EnableClientSideAPI="True" runat="server" DataSourceID="SqlDataSource1"
                            ValueType="System.String" Value='<%# Bind("PLINE_ID") %>' Width="150px" TextField="PLINE_NAME"
                            ValueField="RMES_ID" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td style="width: 8px; height: 30px">
                    </td>
                    <td style="width: 90px; text-align: left">
                        <dx:ASPxLabel ID="Label1" runat="server" Text="跨线模式代码" AssociatedControlID="txtAlineCode">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px;" align="left">
                        <dx:ASPxTextBox ID="txtAlineCode" EnableClientSideAPI="True" runat="server" Width="150px"
                            Text='<%# Bind("ALINE_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="跨线模式代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField IsRequired="True" ErrorText="跨线模式代码不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 90px; text-align: left">
                        <dx:ASPxLabel ID="Label2" runat="server" Text="跨线模式名称" AssociatedControlID="txtAlineName">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px;" align="left">
                        <dx:ASPxTextBox ID="txtAlineName" EnableClientSideAPI="True" runat="server" Width="150px"
                            Text='<%# Bind("ALINE_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="跨线模式名称字节长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField IsRequired="True" ErrorText="跨线模式名称不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
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
        </EditForm>
    </Templates>
</dx:ASPxGridView>
    
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
    ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
