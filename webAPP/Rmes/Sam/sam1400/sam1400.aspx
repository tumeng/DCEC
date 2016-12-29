<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Sam_sam1400_sam1400"    Codebehind="sam1400.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSiteMapControl" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ClientInstanceName="grid" ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;PROGRAM_CODE" OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowValidating="ASPxGridView1_RowValidating"
    OnBeforeColumnSortingGrouping="ASPxGridView1_BeforeColumnSortingGrouping">

    <SettingsEditing PopupEditFormWidth="600" PopupEditFormHeight="200" />
        
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" Caption="操作">
            <EditButton Visible="True">
            </EditButton>
            <NewButton Visible="True">
            </NewButton>
            <DeleteButton Visible="True">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" VisibleIndex="1" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="程序代码" FieldName="PROGRAM_CODE" VisibleIndex="2" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="程序名称" FieldName="PROGRAM_NAME" VisibleIndex="3" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="英文名称" FieldName="PROGRAM_NAME_EN" VisibleIndex="4" Width="200px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="链接地址" FieldName="PROGRAM_VALUE" VisibleIndex="5" Width="250px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="是否授权" FieldName="RIGHT_FLAG1" VisibleIndex="6" Width="80px" CellStyle-HorizontalAlign="Center">
<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="是否授权" FieldName="RIGHT_FLAG" VisibleIndex="6" Visible="false" Width="80px" CellStyle-HorizontalAlign="Center">
<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
        
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
            
    <Templates>
        <EditForm>
            <center>
                <table width="95%">
                    <caption>
                        <br />
                        <tr style="height: 30px">
                            <td style="width: 10%; text-align: right;">
                                <dx:ASPxLabel ID="Label1" runat="server" AssociatedControlID="txtPCode" 
                                    Text="程序代码" Width="50px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 32%">
                                <dx:ASPxTextBox ID="txtPCode" runat="server" Text='<%# Bind("PROGRAM_CODE") %>' 
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                    Width="100%">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                        ErrorText="程序代码有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                        <RegularExpression ErrorText="程序代码字节长度不能超过50！" 
                                            ValidationExpression="^.{0,15}$" />
                                        <%--<RequiredField ErrorText="程序代码不能为空！" IsRequired="True" />--%>
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <dx:ASPxLabel ID="Label2" runat="server" AssociatedControlID="txtPName" 
                                    Text="程序名称" Width="50px">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 48%">
                                <dx:ASPxTextBox ID="txtPName" runat="server" Text='<%# Bind("PROGRAM_NAME") %>' 
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                    Width="100%">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                        ErrorText="程序名称有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                        <RegularExpression ErrorText="程序名称字节长度不能超过50！" 
                                            ValidationExpression="^.{0,15}$" />
                                        <RequiredField ErrorText="程序名称不能为空！" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td style="text-align: right">
                                <dx:ASPxLabel ID="Label4" runat="server" AssociatedControlID="txtPNameEn" 
                                    Text="英文缩写">
                                </dx:ASPxLabel>
                            </td>
                            <td colspan="3">
                                <dx:ASPxTextBox ID="txtPNameEn" runat="server" 
                                    Text='<%# Bind("PROGRAM_NAME_EN") %>' 
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                    Width="100%">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                        ErrorText="英文缩写有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                        <RegularExpression ErrorText="英文缩写字节长度不能超过50！" 
                                            ValidationExpression="^.{0,50}$" />
                                        <RequiredField ErrorText="英文缩写不能为空！" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td style="text-align: right">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" AssociatedControlID="txtPValue" 
                                    Text="链接地址">
                                </dx:ASPxLabel>
                            </td>
                            <td colspan="3">
                                <dx:ASPxTextBox ID="txtPValue" runat="server" 
                                    Text='<%# Bind("PROGRAM_VALUE") %>' 
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                    Width="100%">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                        ErrorText="链接地址有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                        <RegularExpression ErrorText="链接地址字节长度不能超过500！" 
                                            ValidationExpression="^.{0,500}$" />
                                        <RequiredField ErrorText="链接地址不能为空！" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td style="text-align: right">
                                <dx:ASPxLabel ID="Label3" runat="server" Text="需要授权">
                                </dx:ASPxLabel>
                            </td>
                            <td colspan="3" style="text-align: left">
                                <dx:ASPxCheckBox ID="chRFlag" runat="server" Checked="true" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                </dx:ASPxCheckBox>
                            </td><%--Checked='<%#Eval("RIGHT_FLAG").ToString()=="Y"%>' --%>
                        </tr>
                        <tr>
                            <td align="right" colspan="4">
                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" 
                                    ReplacementType="EditFormUpdateButton" />
                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" 
                                    ReplacementType="EditFormCancelButton" />
                            </td>
                        </tr>
                    </caption>
                </table>
            </center>
        </EditForm>
    </Templates>        
</dx:ASPxGridView>

</asp:Content>
