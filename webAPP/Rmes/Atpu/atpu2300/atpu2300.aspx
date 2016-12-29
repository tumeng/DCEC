<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" StylesheetTheme="Theme1"  AutoEventWireup="true" Inherits="Rmes_atpu2300" Codebehind="atpu2300.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%--功能概述：维护FR组对应的程序号--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="FR" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowValidating="ASPxGridView1_RowValidating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    >
    <SettingsEditing PopupEditFormWidth="530px" />
    <SettingsBehavior ColumnResizeMode="Control"/>                
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
            <NewButton Visible="True" Text="新增">
            </NewButton>
            
            <DeleteButton Visible="True" Text="删除">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
       
        <dx:GridViewDataTextColumn Caption="FR" Name="FR" FieldName="FR" VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="程序号" Name="CXH" FieldName="CXH" VisibleIndex="3" Width="160px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="SAT" Name="SAT" FieldName="SAT" VisibleIndex="4" Width="160px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <Templates>
        <EditForm>
            <table>
            <tr>
                    <td style="height: 30px">
                    </td>
                    <td style="width: 120px">

                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="FR">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtFR"  runat="server" Width="140px" Value='<%# Bind("FR") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" >
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="程序号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtCXH" runat="server" Text='<%# Bind("CXH") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>
                            <tr>
                    <td style="height: 30px">
                    </td>
                    <td style="width: 120px">


                    </td>
                    <td style="width: 140px">

                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SAT">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtSAT" runat="server" Text='<%# Bind("SAT") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>

               

                
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

