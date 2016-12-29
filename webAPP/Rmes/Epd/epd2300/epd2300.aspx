<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd2300.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd2300.epd2300" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID" 
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowInserting="ASPxGridView1_RowInserting"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsEditing PopupEditFormHeight="200px" PopupEditFormWidth="400px" />
                        
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" Caption=" ">
            <DeleteButton Visible="True">
            </DeleteButton>
            <NewButton Visible="true">
            </NewButton>
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="DEPT_CODE"  Caption="部门代码" VisibleIndex="1" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="DEPT_NAME" Caption="部门名称" VisibleIndex="2" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Caption="产线代码" VisibleIndex="3" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="PLINE_NAME" Caption="产线名称" VisibleIndex="4" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <Templates>
        <EditForm>
            <table width="400px">
                <tr style="height: 30px">
                    <dx:ASPxTextBox ID="txtID" runat="server" Visible="false" Text='<%#Bind("RMES_ID") %>'></dx:ASPxTextBox>
                    <td style="width: 30px; text-align: center;">
                    </td>
                    <td style="width: 80px; text-align: left;">
                    </td>
                    <td style="width: 250px; text-align: left;">
                    </td>
                    <td style="width: 30px">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="部门">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxGridLookup ID="gridLookupDEPT" runat="server" ClientInstanceName="gridLookupDEPT"
                            DataSourceID="SqlRole" KeyFieldName="DEPT_CODE" MultiTextSeparator="," SelectionMode="Multiple"
                            TextFormatString="{1}" Width="250px">
                            <Columns>
                                <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                <dx:GridViewDataColumn Caption="部门代码" FieldName="DEPT_CODE" />
                                <dx:GridViewDataColumn Caption="部门名称" FieldName="DEPT_NAME" />
                            </Columns>
                        </dx:ASPxGridLookup>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="text-align: center;">
                    </td>
                    <td style="text-align: left;">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="产线选择">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxGridLookup ID="gridLookupPline" runat="server" ClientInstanceName="gridLookupPline"
                            DataSourceID="SqlUser" KeyFieldName="PLINE_CODE" MultiTextSeparator="," SelectionMode="Multiple"
                            TextFormatString="{1}" Width="250px">
                            <Columns>
                                <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                <dx:GridViewDataColumn Caption="产线代码" FieldName="PLINE_CODE" />
                                <dx:GridViewDataColumn Caption="产线名称" FieldName="PLINE_NAME" />
                            </Columns>
                        </dx:ASPxGridLookup>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td align="center">
                    </td>
                    <td align="right" colspan="2">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                    </td>
                    <td align="center">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td align="center">
                    </td>
                    <td align="center" colspan="2">
                    </td>
                    <td align="center">
                    </td>
                </tr>
            </table>
        </EditForm>
    </Templates>                    
</dx:ASPxGridView>
    
<asp:SqlDataSource ID="SqlUser" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlRole" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    
</asp:Content>
