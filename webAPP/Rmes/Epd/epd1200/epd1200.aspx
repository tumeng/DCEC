<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="epd1200.aspx.cs" Inherits="Rmes_epd1200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：站点代码表维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="RMES_ID" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                <EditButton Visible="True" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINECODE1" VisibleIndex="1"
                Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" Visible="false"
                VisibleIndex="1" Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="站点代码" FieldName="STATION_CODE" Visible="false"
                VisibleIndex="2" Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="3"
                Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="站点类型" FieldName="STATION_TYPE_NAME" VisibleIndex="4"
                Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="站点物理区域" FieldName="AREA_NAME" VisibleIndex="5"
                Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="站点顺序" FieldName="STATION_SEQ" VisibleIndex="6"
                Width="80px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="站点备注" FieldName="STATION_REMARK" VisibleIndex="7"
                Width="220px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn VisibleIndex="19" Width="80%" />
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
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="站点名称">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxTextBox ID="txtSName" runat="server" Width="150px" Text='<%# Bind("STATION_NAME") %>'
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
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="站点代码" Visible="false">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxTextBox ID="txtSCode" Visible="false" runat="server" Width="140px" Text='<%# Bind("STATION_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            </dx:ASPxTextBox>
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
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtPCode" runat="server" Width="140px" Value='<%# Bind("PLINE_CODE") %>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" DropDownStyle="DropDownList">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="站点类型">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtSType" runat="server" Width="150px" Value='<%# Bind("STATION_TYPE_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" DropDownStyle="DropDownList">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
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
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="物理区域">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtSArea" runat="server" Width="140px" Value='<%# Bind("AREA_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" DropDownStyle="DropDownList">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="站点顺序">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxTextBox ID="txtSSeq" runat="server" Width="150px" Text='<%# Bind("STATION_SEQ") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                    <RegularExpression ErrorText="必须输入正整数且不可为0！" ValidationExpression="^\+?[1-9][0-9]*$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="站点备注">
                            </dx:ASPxLabel>
                        </td>
                        <td style="height: 30px; text-align: left;" colspan="4">
                            <dx:ASPxTextBox ID="txtSRemark" runat="server" Width="421px" Text='<%# Bind("STATION_REMARK") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Height="16px">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过1000！" ValidationExpression="^.{0,1000}$" />
                                    <RequiredField IsRequired="False" />
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
</asp:Content>
