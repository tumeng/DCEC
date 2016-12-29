<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="atpu2100.aspx.cs" Inherits="Rmes_atpu2100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--功能概述：经济版标定维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="FR" OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
        OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
        OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                <EditButton Visible="True" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <%-- <dx:GridViewDataComboBoxColumn Caption="生产线" VisibleIndex="1" Width="100px" FieldName="PLINE_CODE">
                                        <PropertiesComboBox DataSourceID="SqlCode" ValueField="PLINE_CODE" TextField="PLINE_NAME"
                                            IncrementalFilteringMode="StartsWith" ValueType="System.String" DropDownStyle="DropDownList" />
                                    </dx:GridViewDataComboBoxColumn>--%>
            <dx:GridViewDataTextColumn Caption="FR" FieldName="FR" VisibleIndex="1" Settings-AutoFilterCondition="Contains"
                Width="120px" />
            <dx:GridViewDataTextColumn Caption="SC" FieldName="SC" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataTextColumn Caption="DO" FieldName="DO" VisibleIndex="3" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataTextColumn Caption="是否判断出口" FieldName="SFPDCK" VisibleIndex="4" Settings-AutoFilterCondition="Contains"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="用户名称" FieldName="YHMC" VisibleIndex="5" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataDateColumn Caption="修改日期" FieldName="RQSJ" VisibleIndex="6" Settings-AutoFilterCondition="Contains"
                Width="160px" PropertiesDateEdit-EditFormat="DateTime" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="试装台数" FieldName="SZTS" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="已装台数" FieldName="YZTS" VisibleIndex="8" Settings-AutoFilterCondition="Contains"
                Width="80px" />
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
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="FR">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtFR" runat="server" Width="140px" Value='<%# Bind("FR") %>'
                                DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="SC">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxComboBox ID="txtSC" runat="server" Width="150px" Value='<%# Bind("SC") %>'
                                DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                        <td style="height: 10px" colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="DO">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtDO" runat="server" Width="140px" Value='<%# Bind("DO") %>'
                                DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="是否判断出口">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxComboBox ID="txtSFPDCK" runat="server" Width="150px" Value='<%# Bind("SFPDCK") %>'
                                DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <Items>
                                    <dx:ListEditItem Text="是" Value="是" />
                                    <dx:ListEditItem Text="否" Value="否" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="height: 10px">
                        </td>
                        <tr>
                            <td style="width: 8px; height: 30px">
                            </td>
                            <td style="width: 80px">
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="试装台数">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 170px">
                                <dx:ASPxTextBox ID="txtSZTS" runat="server" Width="140px" Text='<%# Bind("SZTS") %>'
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="已装台数" Visible="false">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 180px">
                                <dx:ASPxTextBox ID="txtYZTS" runat="server" Width="150px" Text='<%# Bind("YZTS") %>'
                                    Visible="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                        <%--<RequiredField IsRequired="True" ErrorText="不能为空！" />--%>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 1px">
                            </td>
                        </tr>
                    <tr>
                        <td colspan="7" style="height: 10px">
                        </td>
                        <tr>
                            <td style="height: 30px">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
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
                confirm('确认要删除这条记录吗？');
               alert(theRet);
            }
           
            if(callbackName == 'noDelete') 
            {
                alert('已装台数大于0，不可删除！');
            }
            if(callbackName == 'refresh') 
            {
                grid2.PerformCallback();
            }
            
        }" />
    </dx:ASPxGridView>
    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="false"
          KeyFieldName="FR;SC;RQSJ" Width="100%" 
        oncustomcallback="ASPxGridView2_CustomCallback">
        <Columns>
            <dx:GridViewDataTextColumn Caption="FR" FieldName="FR" VisibleIndex="1" Settings-AutoFilterCondition="Contains"
                Width="120px" />
            <dx:GridViewDataTextColumn Caption="SC" FieldName="SC" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataTextColumn Caption="DO" FieldName="DO" VisibleIndex="3" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataTextColumn Caption="是否判断出口" FieldName="SFPDCK" VisibleIndex="4" Settings-AutoFilterCondition="Contains"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="操作用户" FieldName="YHMC" VisibleIndex="11" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataTextColumn Caption="试装台数" FieldName="SZTS" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="已装台数" FieldName="YZTS" VisibleIndex="8" Settings-AutoFilterCondition="Contains"
                Width="80px" />
            <dx:GridViewDataDateColumn Caption="修改日期" FieldName="RQSJ" VisibleIndex="9" Settings-AutoFilterCondition="Contains"
                Width="160px" PropertiesDateEdit-EditFormat="DateTime" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="操作说明" FieldName="CZSM" VisibleIndex="10" Settings-AutoFilterCondition="Contains"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFooter="True" />
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="FR" SummaryType="Count" DisplayFormat="数量={0}" />
        </TotalSummary>
    </dx:ASPxGridView>
</asp:Content>
