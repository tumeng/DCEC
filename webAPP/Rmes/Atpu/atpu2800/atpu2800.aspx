<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Rmes_atpu2800" CodeBehind="atpu2800.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--功能概述：SO状态修改--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="SO号:" />
            </td>
            <td>
                <dx:ASPxTextBox ID="txtPlan" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查询" UseSubmitBehavior="False"
                    OnClick="btnCx1_Click" Height="21px" Width="93px" />
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        KeyFieldName="PT_PART" OnRowUpdating="ASPxGridView1_RowUpdating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsEditing PopupEditFormWidth="530px" />
        <SettingsBehavior ColumnResizeMode="Control" />
        <ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = &#39;&#39;;
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == &#39;Delete&#39;) 
            {
                confirm(&#39;确认要删除这条记录吗？&#39;);
                alert(theRet);
            }
             
        }"></ClientSideEvents>
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
                <EditButton Visible="True" Text="修改">
                </EditButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="零件号" Name="PT_PART" FieldName="PT_PART" VisibleIndex="1"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="英文描述" Name="PT_DESC1" FieldName="PT_DESC1" VisibleIndex="3"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="中文描述" Name="PT_DESC2" FieldName="PT_DESC2" VisibleIndex="4"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件组" Name="PT_GROUP" FieldName="PT_GROUP" VisibleIndex="5"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件状态" Name="PT_STATUS" FieldName="PT_STATUS"
                VisibleIndex="2" Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件类型" Name="PT_PART_TYPE" FieldName="PT_PART_TYPE"
                VisibleIndex="6" Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件条款" Name="PT_ARTICLE" FieldName="PT_ARTICLE"
                VisibleIndex="7" Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="虚零件" Name="PT_PHANTOM" FieldName="PT_PHANTOM"
                VisibleIndex="8" Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="CONFIG" Name="PT_CONFIG" FieldName="PT_CONFIG"
                VisibleIndex="9" Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件生产线" Name="PT_PROD_LINE" FieldName="PT_PROD_LINE"
                VisibleIndex="10" Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
        <SettingsEditing PopupEditFormWidth="530px"></SettingsEditing>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="零件号" Visible="True">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxTextBox ID="TextPart" runat="server" Width="140px" Text='<%# Bind("PT_PART") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="True">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 30px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="零件状态">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxComboBox ID="TextStatus" runat="server" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("PT_STATUS") %>' Width="140px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                <Items>
                                    <dx:ListEditItem Text="P" Value="P" />
                                    <dx:ListEditItem Text="S" Value="S" />
                                    <dx:ListEditItem Text="O" Value="O" />
                                </Items>
                            </dx:ASPxComboBox>
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
                
                alert(theRet);
            }
             
        }" />
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
