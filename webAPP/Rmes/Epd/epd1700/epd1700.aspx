<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_epd1700" Title=""  Codebehind="epd1700.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
   <%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table>
        <tr>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
            </td>
        </tr>
    </table>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;LOCATION_CODE"
    OnRowUpdating="ASPxGridView1_RowUpdating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowInserting="ASPxGridView1_RowInserting"
    OnRowValidating="ASPxGridView1_RowValidating">
    <SettingsEditing PopupEditFormWidth="550px" />
    <SettingsBehavior ColumnResizeMode="Control"/>    
    <Columns>
        
        <dx:GridViewDataTextColumn Caption="公司代码" FieldName="COMPANY_CODE" Visible="False" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" VisibleIndex="1" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线名称" FieldName="PLINE_NAME" VisibleIndex="2" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工位名称" FieldName="LOCATION_NAME" VisibleIndex="4" Width="150px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工位顺序" FieldName="LOCATION_SEQ" VisibleIndex="5" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工位工时" FieldName="LOCATION_MANHOUR" VisibleIndex="6" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewCommandColumn Caption="  " VisibleIndex="0" Width="60px" Visible="True">
            <EditButton Visible="false">
            </EditButton>
            <NewButton Visible="false">
            </NewButton>
            <DeleteButton Visible="false">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
        
    <Templates>
        <EditForm>
            <center>
                <table>
                    <tr>
                        <td style="height: 30px" colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td style="width: 70px; text-align: left">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="生产线名称" AssociatedControlID="ASPxComboBox1">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px;" align="left">
                            <dx:ASPxComboBox ID="ASPxComboBox1" EnableClientSideAPI="True" runat="server" OnLoad="ASPxComboBox1_Load"
                                DataSourceID="SqlDataSource1" ValueType="System.String" Width="150px" Value='<%# Bind("PLINE_CODE") %>'
                                OnInit="ASPxComboBox1_Init" TextField="pline_name" ValueField="pline_code" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                        <td style="width: 70px; text-align: left">
                            <dx:ASPxLabel ID="Label4" runat="server" Text="工位代码" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="txtLocationCode" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("LOCATION_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="工位代码不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 70px; text-align: left">
                            <dx:ASPxLabel ID="Label5" runat="server" Text="工位名称" AssociatedControlID="txtLocationName">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtLocationName" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("LOCATION_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位名称字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="工位名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 70px; text-align: left">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="工位顺序" AssociatedControlID="txtLocationOrder">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtLocationOrder" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("LOCATION_SEQ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位顺序必须输入正整数！" ValidationExpression="^[0-9]{1,100}$" />
                                    <RequiredField IsRequired="True" ErrorText="工位顺序不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 70px; text-align: left">
                            <dx:ASPxLabel ID="Label2" runat="server" Text="工位工时" AssociatedControlID="txtLocationManhour">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtLocationManhour" EnableClientSideAPI="True" runat="server"
                                Width="150px" Text='<%# Bind("LOCATION_MANHOUR") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工时必须输入数字！" ValidationExpression="^-?(0|\d+)(\.\d+)?$" />
                                    <RequiredField IsRequired="True" ErrorText="工时不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 50px" colspan="7">
                        </td>
                    </tr>
                </table>
            </center>
        </EditForm>
    </Templates>
</dx:ASPxGridView>
    
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
    ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
  <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                        </dx:ASPxGridViewExporter>  
</asp:Content>
