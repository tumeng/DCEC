<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="epd3300.aspx.cs" Inherits="Rmes_epd3300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        KeyFieldName="WORKSHOP_CODE" OnRowInserting="ASPxGridView1_RowInserting1" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowValidating="ASPxGridView1_RowValidating"
        OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">--%>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="WORKSHOP_CODE"
    OnRowDeleting="ASPxGridView1_RowDeleting"  OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowInserting="ASPxGridView1_RowInserting1"  OnRowValidating="ASPxGridView1_RowValidating"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">

        <%--<SettingsBehavior ColumnResizeMode="Control" />--%>
        <SettingsEditing PopupEditFormWidth="550px" PopupEditFormHeight="200px" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="150px" FixedStyle="Left">
                <NewButton Visible="True" Text="新增">
                </NewButton>
                <EditButton Visible="True" Text="修改">
                </EditButton>
                <DeleteButton Visible="True" Text="删除">
                </DeleteButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1"
                Width="210px">
                <PropertiesComboBox ValueType="System.String">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="车间" FieldName="WORKSHOP_CODE" VisibleIndex="2"
                Width="150px">
                <PropertiesComboBox ValueType="System.String">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataColumn Caption="备注" FieldName="TEMP01" VisibleIndex="3" Width="150px">
            </dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <%--<SettingsEditing PopupEditFormHeight="150px" PopupEditFormWidth="550px" />--%>
        <Templates>
            <EditForm>
                <table>
                    <tr style="height: 10px">
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 200px;">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="生产线" Width="200px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxComboBox ID="comboPlineCode" runat="server" Width="160px" Value='<%# Bind("PLINE_CODE") %>' DropDownStyle="DropDownList"
                                OnDataBinding="PlineCode_DataBinding" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="车间" Width="100px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxComboBox ID="comboWorkshopCode" ClientInstanceName="planBatch" runat="server" DropDownStyle="DropDownList"
                                Width="160px" Value='<%# Bind("WORKSHOP_CODE") %>' OnDataBinding="WorkshopCode_DataBinding"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="车间不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                    <tr style="height: 50px">
                        <td style="width: 8px">
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="备注信息" Width="100px">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="4">
                            <dx:ASPxMemo ID="txtRemark" runat="server" Width="100%" Height="50px" Text='<%# Bind("TEMP01") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="备注有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="备注字节长度不能超过500！" ValidationExpression="^.{0,500}$" />
                                </ValidationSettings>
                            </dx:ASPxMemo>
                            <td>
                            </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="text-align: right;" colspan="6">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td colspan="7">
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
       <%-- <ClientSideEvents BeginCallback="function(s, e) 
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
        }" />--%>
    </dx:ASPxGridView>
</asp:Content>
