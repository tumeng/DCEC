<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_epd2000"  Theme="Theme1"  Codebehind="epd2000.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSiteMapControl" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;TEAM_CODE" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowValidating="ASPxGridView1_RowValidating">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="610px" PopupEditFormHeight="180px" />
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" Caption="操作">
            <EditButton Visible="True"/>
            <NewButton Visible="True"/>
            <DeleteButton Visible="True"/>
            <ClearFilterButton Visible="True"/>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="LEADER_CODE" Visible="false" />

        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1" Width="200px"/>

        <dx:GridViewDataTextColumn Caption="班组代码" FieldName="TEAM_CODE" VisibleIndex="3" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="班组名称" FieldName="TEAM_NAME" VisibleIndex="5" Width="250px"/>
        
        <dx:GridViewDataTextColumn Caption="班组长" FieldName="USER_NAME" VisibleIndex="7" Width="200px"/>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"/>
    </Columns>
        
    <Templates>
        <EditForm>
            <center>
                <table width="600px">
                    <tr>
                        <td colspan="7" style=" height:20px"></td>
                    </tr>
                    <tr style="height:30px">
                        <td style="width: 10px"></td>
                        <td style="width: 80px; text-align: left;">
                            <asp:Label ID="Label1" runat="server" Text="班组代码"></asp:Label>
                        </td>
                        <td style="width: 200px;text-align: left;">
                            <dx:ASPxTextBox ID="txtPCode" runat="server" Width="180px" Text='<%# Bind("TEAM_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="班组代码有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="班组代码字节长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="班组代码不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 10px"></td>
                        <td style="width: 80px; text-align: left;">
                            <asp:Label ID="Label2" runat="server" Text="班组名称"></asp:Label>
                        </td>
                        <td style="width: 200px;text-align: left;">
                            <dx:ASPxTextBox ID="txtPName" runat="server" Width="180px" Text='<%# Bind("TEAM_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="班组名称有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="班组名称字节长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="班组名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 10px"></td>
                    </tr>
                    <tr style="height:30px">
                        <td style="width: 10px"></td>
                        <td style="width: 80px; text-align: left; ">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 200px;text-align: left;">
                            <dx:ASPxComboBox ID="dropPlineCode" runat="server" Width="180px"  DropDownStyle="DropDownList" Value='<%# Bind("PLINE_CODE") %>'>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px"> </td>
                        <td style="width: 80px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="班组长">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 200px; text-align: left;">
                            <dx:ASPxComboBox ID="dropTeamLeader" runat="server" Width="180px"  DropDownStyle="DropDownList" Value='<%# Bind("LEADER_CODE") %>'>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px"> </td>
                    </tr>
                    <tr style="height:30px">
                        <td></td>
                        <td colspan="5" align="right">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="7" style=" height:30px"></td>
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

