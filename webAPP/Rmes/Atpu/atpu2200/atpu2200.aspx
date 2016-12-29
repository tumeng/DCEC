<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" StylesheetTheme="Theme1"  AutoEventWireup="true" Inherits="Rmes_atpu2200" Codebehind="atpu2200.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%--功能概述：客户条码打印维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="SO" 
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
       
        <dx:GridViewDataTextColumn Caption="客户名称" Name="KHMC" FieldName="KHMC" VisibleIndex="2" Width="80px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="客户订单号" Name="KHH" FieldName="KHH" VisibleIndex="3" Width="120px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="物料号" Name="WLH" FieldName="WLH" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn> 
       
        <dx:GridViewDataTextColumn Caption="供应商代码" Name="GYSDM" FieldName="GYSDM" VisibleIndex="5" Width="80px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="SO号" Name="SO" FieldName="SO" VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="版本号" Name="BBH" FieldName="BBH" VisibleIndex="7" Width="80px" Settings-AutoFilterCondition="Contains">
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

                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="客户名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtKHMC" runat="server" Width="140px" Text='<%# Bind("KHMC") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="客户订单号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtKHH" runat="server" Text='<%# Bind("KHH") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                <%--<RequiredField ErrorText="不能为空！" IsRequired="True" />--%>
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
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="SO号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtSO" runat="server" Width="140px" Text='<%# Bind("SO") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="物料号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtWLH" runat="server" Text='<%# Bind("WLH") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                <%--<RequiredField ErrorText="不能为空！" IsRequired="True" />--%>
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
                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="供应商代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtGYSDM" runat="server" Width="140px" Text='<%# Bind("GYSDM") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                <%--<RequiredField IsRequired="True" ErrorText="不能为空！" />--%>
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="版本号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtBBH" runat="server" Text='<%# Bind("BBH") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField  IsRequired="False" />
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
<asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>

