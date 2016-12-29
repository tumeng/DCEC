<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes_epd1100" Codebehind="epd1100.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%--功能概述：生产线定义--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowValidating="ASPxGridView1_RowValidating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsEditing PopupEditFormWidth="530px" />
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
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="生产线代码" Name="PLINE_CODE" FieldName="PLINE_CODE" VisibleIndex="2" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线名称" Name="PLINE_NAME" FieldName="PLINE_NAME" VisibleIndex="3" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线类型" Name="PLINE_TYPE_NAME" FieldName="PLINE_TYPE_NAME" VisibleIndex="4" Width="100px">
        </dx:GridViewDataTextColumn>
       
        <dx:GridViewDataTextColumn Caption="三方物料计算" Name="THIRD_FLAG" FieldName="THIRD_FLAG" VisibleIndex="5" Width="80px" />
        <dx:GridViewDataTextColumn Caption="库房物料计算" Name="STOCK_FLAG" FieldName="STOCK_FLAG" VisibleIndex="6" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="QAD代码" Name="SAP_CODE" FieldName="SAP_CODE" VisibleIndex="7" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
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
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 170px">
                        <dx:ASPxTextBox ID="txtPlineCode" runat="server" Width="140px" Text='<%# Bind("PLINE_CODE") %>'
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
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtPlineName" runat="server" Width="150px" Text='<%# Bind("PLINE_NAME") %>'
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
                </tr>
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线类型">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="dropPlineType" runat="server" Width="140px" Value='<%# Bind("PLINE_TYPE_CODE") %>' DropDownStyle="DropDownList"
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="QAD代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtSapCode" runat="server" Width="150px" Text='<%# Bind("SAP_CODE") %>'
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
                </tr>

                <tr>
                    <td style="height: 30px">
                    </td>

                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="是否参与三方物料计算">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="140px" Value='<%# Bind("THIRD_FLAG") %>' DropDownStyle="DropDownList"
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                            <Items>
                                <dx:ListEditItem Text="是" Value="Y" />
                                <dx:ListEditItem Text="否" Value="N" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="是否参与库房物料计算">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Width="150px" Value='<%# Bind("STOCK_FLAG") %>' DropDownStyle="DropDownList"
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                               
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                            <Items>
                                <dx:ListEditItem Text="是" Value="Y"/>
                                <dx:ListEditItem Text="否" Value="N" />
                            </Items>
                        </dx:ASPxComboBox>
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
