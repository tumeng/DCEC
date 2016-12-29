<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept3600.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept3600.rept3600" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%--合格证打印查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td style="text-align: left; width: 60px;">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="日期时间:" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="150px" EditFormatString="yyyy-MM-dd HH:mm:ss">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="150px" EditFormatString="yyyy-MM-dd HH:mm:ss">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="true"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="ROWID" OnRowUpdating="ASPxGridView1_RowUpdating">
        <SettingsEditing PopupEditFormWidth="750px"   />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="80px">
                <EditButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="LSH" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO号" FieldName="SO" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="机型" FieldName="JX" VisibleIndex="3" Width="130px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="排放等级" FieldName="PFDJ" VisibleIndex="4" Width="150px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="排放报告号" FieldName="PFBGH" VisibleIndex="5" Width="150px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="序列号" FieldName="XLH" VisibleIndex="6" Width="120px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="时间" FieldName="RQ" VisibleIndex="7" Width="150px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="备注" FieldName="JYY" VisibleIndex="8" Width="150px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="9">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="流水号">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtLSH" runat="server" Width="150px" Text='<%# Bind("LSH") %>'
                                Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="SO号">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtSO" runat="server" Width="150px" Text='<%# Bind("SO") %>'
                                Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="机型">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtJX" runat="server" Width="150px" Text='<%# Bind("JX") %>'
                                Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px" colspan="9">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="排放等级">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtTCL" runat="server" Width="150px" Text='<%# Bind("PFDJ") %>'
                                Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="排放报告号">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtTCL1" runat="server" Width="150px" Text='<%# Bind("PFBGH") %>'
                                Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="序列号">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtXLH" runat="server" Width="150px" Text='<%# Bind("XLH") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="9" style="height: 10px">
                        </td>
                        <tr>
                            <td style="width: 8px; height: 30px">
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="日期">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtJX2" runat="server" Width="150px" Text='<%# Bind("RQ") %>'
                                    Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 1px">
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="备注">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtJYY" runat="server" Width="150px" Text='<%# Bind("JYY") %>'
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过100！" ValidationExpression="^.{0,100}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 1px">
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtROWID" runat="server" Width="150px" Text='<%# Bind("ROWID") %>'
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="false">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    <tr>
                        <td style="height: 30px; text-align: right;" colspan="9">
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
    </dx:ASPxGridView>
</asp:Content>
