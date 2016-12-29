<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" StylesheetTheme="Theme1"
    AutoEventWireup="true" CodeBehind="part1200.aspx.cs" Inherits="Rmes_part1200" %>

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
<%--功能概述：JIT计算参数定义--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <table>
        <tr>
            <td>
                <td style="width: 10px">
                </td>
                <td>
                </td>
        </tr>
        <tr>
            <td style="width: 10px">
            </td>
            
            <td>
            </td>
            
           
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
            </td>
            <td>
            </td>
             <td>
                <dx:ASPxButton ID="btnCheck" runat="server" Text="检查参数是否全部定义" UseSubmitBehavior="False"
                    OnClick="btnCheck_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="PARAMETER_CODE" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsEditing PopupEditFormWidth="660px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="80px">
               
                <EditButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="参数标识" FieldName="PARAMETER_CODE" VisibleIndex="1" Width="180px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="参数值" FieldName="PARAMETER_VALUE" VisibleIndex="2" Width="130px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="参数描述" FieldName="PARAMETER_DESC" VisibleIndex="3" Width="280px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="单位" FieldName="TEMP01" VisibleIndex="4" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="备注" FieldName="TEMP02" VisibleIndex="5" Width="140px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="6" Width="60px"
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
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="参数标识">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width:30px"></td>
                        <td style="width: 170px">
                            <dx:ASPxTextBox ID="TextPmCode" runat="server" Width="140px" Text='<%# Bind("PARAMETER_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                               
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="参数值">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="TextPmValue" runat="server" Width="140px" Text='<%# Bind("PARAMETER_VALUE") %>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="必须输入正整数！" 
                                        ValidationExpression="^\+?[1-9][0-9]*$" />
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                 
                                
                            </dx:ASPxTextBox>
                            <dx:ASPxDateEdit ID="DatePmValue" runat="server" Width="160px" Text='<%# Bind("PARAMETER_VALUE") %>' Visible="false"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                Height="20px"  EditFormatString="yyyy-MM-dd HH:mm:ss" EditFormat="DateTime"> 
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过200！" ValidationExpression="^.{0,200}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="width: 1px">
                        </td>
                          
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="TextGzdd" runat="server" Width="140px" Text='<%# Bind("GZDD") %>'  Visible="false"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    </table>
                    <table>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="参数描述">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 200px">
                            <dx:ASPxTextBox ID="TextPmDesc" runat="server" Width="455px" Value='<%# Bind("PARAMETER_DESC") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                Height="20px">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过200！" ValidationExpression="^.{0,200}$" />
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
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="备注">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            
                            <dx:ASPxTextBox ID="TextPmTemp02" runat="server" Height="20px" 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                Value='<%# Bind("TEMP02") %>' Width="455px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过200！" ValidationExpression="^.{0,200}$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                            
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px; text-align: right;" colspan="4">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="height: 30px" colspan="4">
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
