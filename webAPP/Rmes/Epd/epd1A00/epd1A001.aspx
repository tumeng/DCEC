<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd1A001.aspx.cs" Inherits="epd1A001" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ALINE_CODE" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowValidating="ASPxGridView1_RowValidating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsEditing PopupEditFormWidth="660px"/>
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

        <dx:GridViewDataTextColumn Caption="跨线模式代码" Name="ALINE_CODE" FieldName="ALINE_CODE" VisibleIndex="1" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="跨线模式名称" Name="ALINE_NAME" FieldName="ALINE_NAME" VisibleIndex="2" Width="180px">
        </dx:GridViewDataTextColumn>
        
        <dx:GridViewDataTextColumn Caption="生产线代码" Name="PLINE_CODE" FieldName="PLINE_CODE" VisibleIndex="4" Width="210px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线名称" Name="PLINE_NAME" FieldName="PLINE_NAME" VisibleIndex="5" Width="150px">
        </dx:GridViewDataTextColumn>
        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <Templates>
        <EditForm>
            <table>
            <tr>
                    <td style="height: 30px" colspan="7">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px; height: 30px">
                    </td>
                    <td style="width: 110px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="跨线模式代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtAlineCode" runat="server" Width="150px" Text='<%# Bind("ALINE_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td style="width: 110px">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="跨线模式名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtAlineName" runat="server" Width="150px" Text='<%# Bind("ALINE_NAME") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 10px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                    
                        <dx:ASPxComboBox ID="dropPlineCode" runat="server" Width="150px" Value='<%# Bind("PLINE_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="6" style="text-align: right;">
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
                    
    <ClientSideEvents BeginCallback="function(s, e) 
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
        }" />
</dx:ASPxGridView>
</asp:Content>
