<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_mms1300" Codebehind="mms1300.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table width="100%">
    <tr>
        <td>
            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False" OnClick="btnXlsExport_Click" />
        </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName ="grid" KeyFieldName="VENDOR_CODE" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating" 
    onrowvalidating="ASPxGridView1_RowValidating"
    onhtmleditformcreated="ASPxGridView1_HtmlEditFormCreated">

    <SettingsEditing PopupEditFormWidth="580"/>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
            <NewButton Visible="True" Text="新增"></NewButton>
            <EditButton Visible="True" Text="修改"></EditButton>
            <DeleteButton Visible="True" Text="删除"></DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="往来单位代码" Name="VENDOR_CODE" VisibleIndex="2" FieldName="VENDOR_CODE" Width="100px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="类别" Name="TYPE" VisibleIndex="3" FieldName="TYPE" Width="70px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="往来单位名称" Name="VENDOR_NAME" VisibleIndex="4" FieldName="VENDOR_NAME" Width="400px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="5" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

    <Templates >
        <EditForm>
            <table>
                <tr><td style="height:30px" colspan="7"></td></tr>
                <tr>
                    <td style="width:8px; height:30px"></td>
                    <td style="width:90px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="往来单位代码"></dx:ASPxLabel>
                    </td>
                    <td style="width:190px">
                        <dx:ASPxTextBox ID="txtVendorCode" runat="server" Width="160px"  Text='<%# Bind("VENDOR_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width:1px"></td>
                    <td style="width:70px"><dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="类别"></dx:ASPxLabel></td>
                    <td style="width:190px">
                        <dx:ASPxComboBox ID="ASPxComType"  EnableClientSideAPI="True"  runat="server" ValueType="System.String" Width="100%" 
                            text='<%# Bind("TYPE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <Items>
                                <dx:ListEditItem Text="供应商" Value="A" />
                                <dx:ListEditItem Text="客　户" Value="B" />
                            </Items>

                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="类别不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width:10px"></td>
                </tr>

                <tr>
                    <td></td>
                    <td><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="往来单位名称"></dx:ASPxLabel></td>
                    <td colspan="4" >
                        <dx:ASPxTextBox ID="txtVendorName" runat="server" Text='<%# Bind("VENDOR_NAME") %>' width="435px"
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                SetFocusOnError="True" ValidateOnLeave="True">
                                <RegularExpression ErrorText="长度不能超过60！" ValidationExpression="^.{0,60}$" />
                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td ></td>
                </tr>

                <tr>
                    <td colspan="6" style="height:30px;text-align:right;">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"  runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"  runat="server"></dx:ASPxGridViewTemplateReplacement>
                    </td>
                    <td></td> 
                </tr>
                                
                <tr><td style="height:30px" colspan="7"></td></tr>
                                
            </table>
        </EditForm>
    </Templates>
    
    <ClientSideEvents 
        BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }"
                        
        EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" 
    /> 
</dx:ASPxGridView>
 
<dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>

</asp:Content>
