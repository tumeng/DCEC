<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Theme="Theme1" Debug="true"  AutoEventWireup="true" Inherits="Rmes_Sam_sam2600_sam2600" Codebehind="sam2600.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="USER_ID;TAG_CODE" OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowValidating="ASPxGridView1_RowValidating">
    <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600" PopupEditFormHeight="200" />
    <Settings ShowFilterRow="true" />
        
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px" CellStyle-Wrap="False">
            <EditButton Visible="True">
            </EditButton>
            <NewButton Visible="True">
            </NewButton>
            <DeleteButton Visible="True">
            </DeleteButton>
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="用户代码" FieldName="USER_ID" VisibleIndex="1" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户姓名" FieldName="USER_NAME" VisibleIndex="2" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="TAG_CODE_FATHER" VisibleIndex="3" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="父项标签" FieldName="TAG_NAME_FATHER" VisibleIndex="6" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="TAG_CODE" VisibleIndex="4" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户标签" FieldName="TAG_NAME" VisibleIndex="5"  Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="是否有效" FieldName="TAG_FLAG" VisibleIndex="7" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
        
    <Templates>
        <EditForm>
            <center>
                <table width="95%">
                    <tr style="height: 30px">
                    </tr>
                    <tr>
                        <td style="width: 10%; text-align: right;">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="用户代码" />
                        </td>
                        <td style="width: 32%">
                            <dx:ASPxComboBox ID="drpUID" EnableClientSideAPI="true" runat="server" Value='<%# Bind("USER_ID") %>' />
                        </td>
                        <td style="width: 10%; text-align: right;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="标签代码" />
                        </td>
                        <td style="width: 48%">
                            <dx:ASPxComboBox ID="drpTagCode" EnableClientSideAPI="True" runat="server" Width="98%"
                                Value='<%# Bind("TAG_CODE") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="标签有效" />
                        </td>
                        <td style="text-align: left" colspan="3">
                            <dx:ASPxCheckBox ID="chkTagFlag" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                        </td>
                    </tr>
                </table>
            </center>
        </EditForm>
    </Templates>
        
    <ClientSideEvents EndCallback="function(s, e) {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCompanyName;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" BeginCallback="function(s, e) {
	        grid.cpCallbackName = '';
        }" />
        
</dx:ASPxGridView>

</asp:Content>
