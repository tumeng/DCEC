<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Rmes_atpu1900" CodeBehind="atpu1900.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--功能概述：工艺属性基础数据维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID" 
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
       
        <dx:GridViewDataTextColumn Caption="公司代码" Name="COMPANY_CODE" FieldName="COMPANY_CODE" Visible="false" VisibleIndex="2" Width="80px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工艺路线" Name="ROUNTING_REMARK" FieldName="ROUNTING_REMARK" VisibleIndex="3" Width="80px" Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线代码" Name="PLINE_CODE" FieldName="PLINE_CODE" VisibleIndex="4" Width="80px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn> 
       
        <dx:GridViewDataTextColumn Caption="缸数" Name="GS" FieldName="GS" VisibleIndex="5" Width="60px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="系列" Name="XL" FieldName="XL" VisibleIndex="6" Width="60px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="排量" Name="PL" FieldName="PL" VisibleIndex="8" Width="60px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="燃料" Name="RL" FieldName="RL" VisibleIndex="9" Width="60px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="是否电控" Name="ISDK" FieldName="ISDK" VisibleIndex="10" Width="60px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="是否EGR" Name="ISEGR" FieldName="ISEGR" VisibleIndex="11" Width="60px" Settings-AutoFilterCondition="Contains">
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
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="生产线">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxComboBox ID="txtPCode" runat="server" DropDownStyle="DropDownList" 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Value='<%# Bind("PLINE_CODE") %>' Width="140px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 1px">
                        &nbsp;</td>
                   
                      <td style="width: 90px; text-align: left;">
                          <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="工艺路线" Visible="false">
                          </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="comboxROUNT" runat="server" style="margin-bottom: 0px" 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Value='<%# Bind("ROUNTING_REMARK") %>' Width="140px" Visible="false">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>  
                    </td>
                    <td style="width: 1px">
                    </td>
                </tr>

                <tr>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 90px; text-align: left;">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="缸数">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                       <dx:ASPxTextBox ID="txtGS" runat="server" Text='<%# Bind("GS") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="140px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 30px">
                    </td>
                    <td style="width: 90px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="系列">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtXL" runat="server" Text='<%# Bind("XL") %>' 
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                            Width="150px">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
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
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="排量">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxTextBox ID="txtPL" runat="server" Text='<%# Bind("PL") %>' 
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
                    <td style="width: 90px">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="燃料">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtRL" runat="server" Text='<%# Bind("RL") %>' 
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
                    <td style="height: 30px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="是否电控" Width="120px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 140px">
                        <dx:ASPxComboBox ID="comboxISDK" runat="server" Width="140px" Value='<%# Bind("ISDK") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                            <Items>
                                <dx:ListEditItem Text="是" Value="Y" />
                                <dx:ListEditItem Text="否" Value="N" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 1px">
                    </td>
                    <td style="width: 120px">
                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="是否EGR">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="comboxISEGR" runat="server" Width="150px" Value='<%# Bind("ISEGR") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                            <Items>
                                <dx:ListEditItem Text="是" Value="Y" />
                                <dx:ListEditItem Text="否" Value="N" />
                            </Items>
                        </dx:ASPxComboBox>
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
                        &nbsp;</td>
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

