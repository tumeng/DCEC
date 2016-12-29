<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="atpu2500.aspx.cs" Inherits="Rmes_atpu2500" Theme="Theme1" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：随机带走件包装维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true"
        ActiveTabIndex="1">
        <TabPages>
            <dx:TabPage Text="包装单元" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                       <table>  
                       <tr>
                            <td>
                               
                                    <td style="width: 10px">
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="查询条件" />
                                    </td>
                                
                            </td>
                        </tr>
                       
                              
                                    <tr>
                                        <td style="width: 10px">
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO:" />
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="txtSoQry" runat="server" Width="120px">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" UseSubmitBehavior="False" OnClick="btnQuery_Click" />
                                        </td>
                            
                            <td align="center">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                    OnClick="btnXlsExport_Click" />
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        </table> 
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="SO" OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
                            OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
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
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="单元序号" FieldName="DYXH" VisibleIndex="2" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="包装单元" FieldName="BZDY" VisibleIndex="3" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="附件包" FieldName="FJB" VisibleIndex="4" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
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
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SO">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="TextSO" runat="server" Width="140px" Text='<%# Bind("SO") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="单元序号">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxComboBox ID="ComboDYXH" runat="server" Width="140px" Value='<%# Bind("DYXH") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dx:ListEditItem Text="1" Value="1" />
                                                        <dx:ListEditItem Text="2" Value="2" />
                                                        <dx:ListEditItem Text="3" Value="3" />
                                                        <dx:ListEditItem Text="4" Value="4" />
                                                        <dx:ListEditItem Text="5" Value="5" />
                                                        <dx:ListEditItem Text="6" Value="6" />
                                                        <dx:ListEditItem Text="7" Value="7" />
                                                        <dx:ListEditItem Text="8" Value="8" />
                                                        <dx:ListEditItem Text="9" Value="9" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="附件包">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="ComboFJB" runat="server" Width="140px" Value='<%# Bind("FJB") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
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
                                            <td style="height: 30px" colspan="7">
                                            </td>
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
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                        </dx:ASPxGridViewExporter>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="物料清单" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="LJDM" OnRowInserting="ASPxGridView2_RowInserting"
                            OnRowDeleting="ASPxGridView2_RowDeleting" OnRowValidating="ASPxGridView2_RowValidating"
                            OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="False" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="零件类别" FieldName="LJLB" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件号" FieldName="LJDM" VisibleIndex="2" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC" VisibleIndex="3" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="简称" FieldName="LJJC" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="规格型号" FieldName="LJGG" VisibleIndex="2" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="单位" FieldName="LJDW" VisibleIndex="3" Width="80px"
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
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="零件类别">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="ComboLJLB" runat="server" Width="150px" Value='<%# Bind("LJLB") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="零件号">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="TextLJDM" runat="server" Width="150px" Text='<%# Bind("LJDM") %>'
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
                                        </tr>
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px">
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="零件名称">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="TextLJMC" runat="server" Width="150px" Text='<%# Bind("LJMC") %>'
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
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="简称">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="TextLJJC" runat="server" Width="150px" Text='<%# Bind("LJJC") %>'
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
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="规格型号">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="TextLJGG" runat="server" Width="150px" Text='<%# Bind("LJGG") %>'
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
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="单位">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="TextLJDW" runat="server" Width="150px" Text='<%# Bind("LJDW") %>'
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
                                            <td style="height: 30px" colspan="7">
                                            </td>
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
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
</asp:Content>
