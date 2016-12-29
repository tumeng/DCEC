<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="atpu1B00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Atpu.atpu1B00.atpu1B00" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"  Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
    Width="100%" KeyFieldName="SO" OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowValidating="ASPxGridView1_RowValidating">
    <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
        PopupEditFormVerticalAlign="WindowCenter" />
                            
    <Columns>
        <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
            <EditButton Visible="FALSE" />
            <NewButton Visible="True" />
            <DeleteButton Visible="True" />
            <ClearFilterButton Visible="True" />
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="SO号" FieldName="SO" VisibleIndex="1" Width="200px"
            Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="托架类型" FieldName="TJLX" VisibleIndex="2"
            Settings-AutoFilterCondition="Contains" Width="200px" />
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
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SO号">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 170px">
                    <dx:ASPxComboBox ID="comboSO" runat="server" EnableClientSideAPI="True" Value='<%# Bind("SO") %>' DropDownStyle="DropDownList" 
                        DataSourceID="SqlDataSource1" ValueType="System.String" Width="150px" ClientInstanceName="comboSOC"
                        TextField="SO" ValueField="SO" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField IsRequired="True" ErrorText="SO不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                </td>
                <td style="width: 1px">
                </td>
                <td style="width: 90px">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="托架类型">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 180px">
                    <dx:ASPxComboBox ID="comboTJLX" runat="server" EnableClientSideAPI="True" Value='<%# Bind("TJLX") %>' DropDownStyle="DropDownList" 
                        DataSourceID="SqlDataSource2" ValueType="System.String" Width="150px" ClientInstanceName="comboTJLXC"
                        TextField="TJLX" ValueField="TJLX" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField IsRequired="True" ErrorText="托架类型不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                </td>
                <td style="width: 1px">
                </td>
            </tr>
            <tr>
                <td style="height: 10px" colspan="7"></td>
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
                <td style="height: 30px" colspan="7"></td>
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
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
