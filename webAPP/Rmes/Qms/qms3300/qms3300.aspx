<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes.WebApp.Rmes.Qms.qms3300.qms3300" Codebehind="qms3300.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
        ClientInstanceName="grid" KeyFieldName="RMES_ID"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowValidating="ASPxGridView1_RowValidating"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" Width="1080px">
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

        <dx:GridViewDataTextColumn Caption="缺陷代码" FieldName="FAULT_CODE" 
            VisibleIndex="1" />
        <dx:GridViewDataTextColumn Caption="缺陷名称" FieldName="FAULT_NAME" 
            VisibleIndex="2" />

        <dx:GridViewDataTextColumn Caption="缺陷备注" FieldName="FAULT_DESC" 
            VisibleIndex="3" />
        <dx:GridViewDataTextColumn Caption="缺陷级别" FieldName="FAULT_CLASS" 
            VisibleIndex="4" />
        
        <dx:GridViewDataTextColumn Caption="缺陷类别" VisibleIndex="6" 
            FieldName="FAULT_TYPE">
        </dx:GridViewDataTextColumn>
    </Columns>

<SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>

<SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600px"></SettingsEditing>

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
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="缺陷代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtDeptCode" runat="server" Width="160px" Text='<%# Bind("FAULT_CODE")%>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="缺陷代码不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="缺陷名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtDeptName" runat="server" Width="160px" Text='<%# Bind("FAULT_NAME")%>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="缺陷名称不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="缺陷备注"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtParentCode" runat="server" Width="160px" Text='<%# Bind("FAULT_DESC")%>'>
                        
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="缺陷等级"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="160px" Text='<%# Bind("FAULT_CLASS")%>'>
                        
                        </dx:ASPxTextBox>
                    </td>
                    </tr>
                    <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="缺陷类别"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="160px" Text='<%# Bind("FAULT_TYPE")%>'>
                        
                        </dx:ASPxTextBox>
                    </td>
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
