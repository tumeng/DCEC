<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="atpu2600.aspx.cs" Inherits="Rmes_atpu2600" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--功能概述：So对应config号维护以及NONPPIF维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true"
        ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Text="SO-CONFIG查询" Visible="true">
                <ContentCollection>
                    <dx:ContentControl  runat="server">
                        
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="SO" OnCustomCallback="ASPxGridView1_CustomCallback">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                            <dx:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="60px">
                                    
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="CONFIG" FieldName="CONFIG" VisibleIndex="2" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
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
            <dx:TabPage Text="NONPPIF维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl  runat="server">
            
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="SO" OnRowInserting="ASPxGridView2_RowInserting" OnRowDeleting="ASPxGridView2_RowDeleting"
                            OnCustomCallback="ASPxGridView2_CustomCallback">
                            <ClientSideEvents BeginCallback="function(s, e) {grid2.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid2.cpCallbackName;
            theRet = grid2.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <%--<SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>--%>
                            <Templates>
                                <EditForm>
                                    <table>
                                        <tr>
                                            <td style="height: 10px" colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SO">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="TextSO" runat="server" Width="150px" Text='<%# Bind("SO") %>'
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
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="6">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px; text-align: right;" colspan="5">
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
                                            <td style="height: 30px" colspan="6">
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
    <asp:SqlDataSource ID="SqlSO" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlConfig" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
