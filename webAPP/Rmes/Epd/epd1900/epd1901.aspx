<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd1901.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd1900.epd1901" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" KeyFieldName="RMES_ID"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px" Mode="PopupEditForm" />

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

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="部门代码" FieldName="DEPT_CODE" VisibleIndex="2" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="部门名称" FieldName="DEPT_NAME" VisibleIndex="3" Width="120px"/>

        <dx:GridViewDataTextColumn Caption="上级部门" FieldName="PARENT_DEPT" VisibleIndex="4" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="备注" FieldName="DEPT_REMARK" VisibleIndex="5" Width="200px"/>
        
        <dx:GridViewBandColumn Caption=" " VisibleIndex="98"></dx:GridViewBandColumn>
    </Columns>

    <Templates>
        <EditForm>
            <dx:ASPxTextBox ID="txtRmesID" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>' Visible="false"/>
            <table>
                <tr style="height: 10px">
                    <td colspan="7">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="部门代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtDeptCode" runat="server" Width="160px" Text='<%# Bind("DEPT_CODE")%>'>
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="部门代码不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="部门名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtDeptName" runat="server" Width="160px" Text='<%# Bind("DEPT_NAME")%>'>
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="部门名称不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="上级部门"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtParentCode" runat="server" Width="160px" Text='<%# Bind("PARENT_DEPT")%>'>
                        
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                    <td></td>
                </tr>

                <tr style="height: 50px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="详细备注信息">
                        </dx:ASPxLabel>
                    </td>
                    <td colspan="4" >
                        <dx:ASPxMemo ID="txtRemark" runat="server" Width="100%" Height="50px" Text='<%# Bind("DEPT_REMARK") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="备注有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="备注字节长度不能超过500！" ValidationExpression="^.{0,500}$" />
                            </ValidationSettings>
                        </dx:ASPxMemo>
                    <td></td>
                </tr>
                

                <tr style="height: 30px">
                    <td style="text-align: right;" colspan="6">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
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
                    

</dx:ASPxGridView>

</asp:Content>
