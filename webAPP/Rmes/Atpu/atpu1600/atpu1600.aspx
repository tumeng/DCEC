<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes_atpu1600" Codebehind="atpu1600.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%--功能概述：排放参数维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="XH" 
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
            <EditButton Visible="True" Text="修改">
            </EditButton>
            <DeleteButton Visible="True" Text="删除">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
       
        <dx:GridViewDataTextColumn Caption="序号" Name="XH" FieldName="XH" VisibleIndex="2" Width="80px"  Visible="false"  Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="SO号" Name="SO" FieldName="SO" VisibleIndex="3" Width="120px" Settings-AutoFilterCondition="Contains" >
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="排放参数" Name="PF" FieldName="PF" VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn> 
       
        <dx:GridViewDataTextColumn Caption="报告号" Name="BZ" FieldName="BZ" VisibleIndex="5" Width="150px" Settings-AutoFilterCondition="Contains" />

        <dx:GridViewDataTextColumn Caption="生产线" Name="DD" FieldName="DD" VisibleIndex="6" Width="100px" Visible="false" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
       
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <Templates>
        <EditForm>
            <table>
            <tr>
                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="当前记录号" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtXH" runat="server" Width="140px" Text='<%# Bind("XH") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="False">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 90px">
                        
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px">
                    </td>
                   <td style="width:90px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                    </td>
                    <td style="width:170px">
                         <dx:ASPxComboBox ID="txtPCode" runat="server" Width="140px" Value='<%# Bind("DD") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" DropDownStyle="DropDownList">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="SO号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtSO" runat="server" Text='<%# Bind("SO") %>' 
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
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="排放参数">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtPF" runat="server" Text='<%# Bind("PF") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="140px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="报告号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtBZ" runat="server" Text='<%# Bind("BZ") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="False" />
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
                confirm('确认要删除这条记录吗？');
                alert(theRet);
            }
             
        }" />
       
</dx:ASPxGridView>
<%--<asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>--%>
</asp:Content>

