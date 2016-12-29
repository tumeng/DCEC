<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes_atpu2400" Codebehind="atpu2400.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%--功能概述：EMARK维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="FDJJX" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowValidating="ASPxGridView1_RowValidating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    >
    <SettingsEditing PopupEditFormWidth="530px" />
    <SettingsBehavior ColumnResizeMode="Control"/>                
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
            <NewButton Visible="True" Text="新增">
            </NewButton>
            <EditButton Visible="false" Text="修改">
            </EditButton>
            <DeleteButton Visible="True" Text="删除">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
       
        <dx:GridViewDataTextColumn Caption="机型" Name="FDJJX" FieldName="FDJJX" VisibleIndex="2" Width="120px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="SC组" Name="SC" FieldName="SC" VisibleIndex="3" Width="120px" Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="R24" Name="RZZS1" FieldName="RZZS1" VisibleIndex="4" Width="120px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn> 
       
        <dx:GridViewDataTextColumn Caption="R49/R96" Name="RZZS2" FieldName="RZZS2" 
            VisibleIndex="5" Width="120px" Settings-AutoFilterCondition="Contains" >

<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="R85/R120" Name="RZZS3" FieldName="RZZS3" VisibleIndex="6" Width="120px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="认证国" Name="RZG" FieldName="RZG" VisibleIndex="7" Width="80px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="是否默认值" Name="SFMRZ" FieldName="SFMRZ" VisibleIndex="8" Width="80px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
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
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="机型" Visible="True">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtJX" runat="server" Width="140px" Text='<%# Bind("FDJJX") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="True">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="认证国">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtRZG" runat="server" Text='<%# Bind("RZG") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 90px; text-align: left;">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="SC组">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtSC" runat="server" Width="140px" Text='<%# Bind("SC") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="True">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="R24">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtS1" runat="server" Text='<%# Bind("RZZS1") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <%--<RequiredField ErrorText="不能为空！" IsRequired="True" />--%>
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="R49/R96">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtS2" runat="server" Text='<%# Bind("RZZS2") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="140px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <%--<RequiredField ErrorText="不能为空！" IsRequired="True" />--%>
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="R85/R120">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtS3" runat="server" Text='<%# Bind("RZZS3") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <%--<RequiredField ErrorText="不能为空！" IsRequired="False" />--%>
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>
                <tr>

                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="是否默认值">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxComboBox ID="txtSFMRZ" runat="server" 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Value='<%# Bind("SFMRZ") %>' Width="140px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                            <Items>
                                <dx:ListEditItem Text="是" Value="是" />
                                <dx:ListEditItem Text="否" Value="否" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 120px">
                    </td>
                    <td style="width: 1px">
                    </td>
                    <tr>
                        <td colspan="6" style="height: 30px; text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" 
                                ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" 
                                ReplacementType="EditFormCancelButton" />
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
<asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>

