<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="part1300.aspx.cs" Inherits="Rmes_part1300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：工位工时维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
       
        
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="120px" ClientInstanceName="Pcode"
                     DropDownStyle="DropDownList" >
                    <ClientSideEvents SelectedIndexChanged="function(s,e){  Location.PerformCallback(Pcode.GetValue()); grid.PerformCallback();   }" />
                </dx:ASPxComboBox>
            </td>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" Width="140px" />
            </td>
        </tr>
         
    </table>
    <table>
    <tr>
    <td>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"    AutoGenerateColumns="False"
        Width="900px" KeyFieldName="LOCATION_CODE" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowValidating="ASPxGridView1_RowValidating" 
            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" oncustomcallback="ASPxGridView1_CustomCallback"
          >
        <SettingsEditing PopupEditFormWidth="400px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                <EditButton Visible="True" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="工位工时" FieldName="LOCATION_TIME" VisibleIndex="3"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="顺序号" FieldName="LOCATION_SEQ" VisibleIndex="1"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" VisibleIndex="2"
                Width="120px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="4"
                Width="120px" Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="GZDD" Visible="false" Width="120px"
                Settings-AutoFilterCondition="Contains">
<Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn VisibleIndex="19" Width="80%" />
        </Columns>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        
                        <td style="width: 140px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="未维护工位代码" Width="140px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 200px">
                            <dx:ASPxComboBox ID="txtLCode" runat="server" DropDownStyle="DropDownList" Text='<%# Bind("LOCATION_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 120px;">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="工位顺序">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 200px;">
                            <dx:ASPxTextBox ID="txtLSeq" runat="server" Text='<%# Bind("LOCATION_SEQ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="200px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^\+?[1-9][0-9]*$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                         
                        </tr>
                        <tr>
                         <td style="width: 8px;">
                        </td>
                        <td style="width: 120px;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="工位工时（分钟）" Width="120px">
                            </dx:ASPxLabel>
                        </td>
                        
                        <td style="width: 200px;">
                            <dx:ASPxTextBox ID="txtLTime" runat="server" Height="19px" Text='<%# Bind("LOCATION_TIME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^\+?[1-9][0-9]*$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px; text-align: right;" colspan="4">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
        <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
    </dx:ASPxGridView>
    </td>
    <td>
    <dx:ASPxListBox ID="ASPxListBoxLocation" runat="server" DataSourceID="SqlLocation"
                Width="200px" Height="450px" ValueField="LOCATION_CODE"  ClientInstanceName="Location"
            ValueType="System.String" oncallback="ASPxListBoxLocation_Callback">
                <Columns>
                    <dx:ListBoxColumn FieldName="LOCATION_CODE" Caption="已维护的多余工位" Width="100%" />
                </Columns>
            </dx:ASPxListBox>
    </td>
    </tr>
    </table>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <%-- <asp:SqlDataSource ID="GridView1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="SqlLocation" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
