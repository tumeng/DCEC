
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_ems1200" Codebehind="ems1200.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName ="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ASSET_CLASS_CODE" 
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating" 
    onrowvalidating="ASPxGridView1_RowValidating"
    onhtmleditformcreated="ASPxGridView1_HtmlEditFormCreated">

    <SettingsEditing PopupEditFormWidth="530"/>
                    
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
            <NewButton Visible="True" Text="新增"></NewButton>
            <EditButton Visible="True" Text="修改"></EditButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="分类代码" VisibleIndex="2" FieldName="ASSET_CLASS_CODE" Width="120px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="分类名称" VisibleIndex="3" FieldName="ASSET_CLASS_NAME" Width="200px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="4" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

    <Templates >
        <EditForm>
            <table>
                <tr><td style="height:10px" colspan="7"></td></tr>                                
                <tr>
                    <td style="width:8px; height:30px"></td>
                    <td style="width:70px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="类别代码"></dx:ASPxLabel>
                    </td>
                    <td style="width:190px">
                        <dx:ASPxTextBox ID="txtClassCode" runat="server" Width="160px"  Text='<%# Bind("ASSET_CLASS_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过10！" ValidationExpression="^.{0,10}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width:1px"></td>
                    <td style="width:70px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="类别名称"></dx:ASPxLabel>
                    </td>
                    <td style="width:190px">
                        <dx:ASPxTextBox ID="txtClassName" runat="server" Width="160px" Text='<%# Bind("ASSET_CLASS_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width:1px"></td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align:right;">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"  runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"  runat="server"></dx:ASPxGridViewTemplateReplacement>
                    </td>
                    <td></td> 
                </tr>
                                
                <tr>
                    <td style="height:30px" colspan="7"></td>                                    
                </tr>                                
            </table>
        </EditForm>
    </Templates>
</dx:ASPxGridView>

</asp:Content>
