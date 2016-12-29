<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="part1600.aspx.cs" Inherits="Rmes_part1600" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：发料工位维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
        function initEditSeries(s, e) {
            if (RLocation.GetValue() == null || Pcode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?RLOCATION=" + RLocation.GetValue() + " &Pcode=" + Pcode.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var str1 = "";
            var str2 = "";
            var array1 = result.split(',');
            str1 = array1[0];
            str2 = array1[1];
            if (str2 == "") {

                RLocation.SetFocus();
                ILocation.SetValue();
                ILocation.SetEnabled(true);

            }
            else 
            {
                ILocation.SetEnabled(false);
                ILocation.SetValue(str2);
            }
         
           
            if (str1 != "") {
                var items = str1.split('@');               
                RGzdd.AddItem();
                for (var i = items.length - 1; i >= 0; i = i - 1) {
                    RGzdd.AddItem(items[i], items[i]);

                }
            }
         
        }

    </script>
    <table>
        <tr>
            <td>
            </td>
            <td style="width: 90px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="查询条件">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td style="width: 10px">
            </td>
            <td style="width: 90px">
                
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_CODE"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="120px">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="计算工位:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtRLoction" runat="server" DataSourceID="SqlRLocation" TextField="LOCATION_CODE"
                    ValueField="LOCATION_CODE" ValueType="System.String" Width="120px">
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" Width="140px" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="REAL_LOCATION" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowDeleting="ASPxGridView1_RowDeleting" 
        OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
        OnCustomCallback="ASPxGridView1_CustomCallback">
        <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                <EditButton Visible="false" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="发料地点" FieldName="ISSUE_GZDD" VisibleIndex="3"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计算地点" FieldName="REAL_GZDD" VisibleIndex="1"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计算工位" FieldName="REAL_LOCATION" VisibleIndex="2"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="发料工位" FieldName="ISSUE_LOCATION" VisibleIndex="4" Width="120px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn VisibleIndex="19" Width="80%" />
        </Columns>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="6">
                        请先选择生产线和计算工位
                        </td>
                    </tr>
                    <tr></tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线" >
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxComboBox ID="txtPCode" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                 Width="120px" ClientInstanceName="Pcode">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxComboBox>
                        </td>
                         <td style="width: 1px">
                            &nbsp;
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="计算工位" >
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 200px">
                            <dx:ASPxComboBox ID="txtRLocation" runat="server" DropDownStyle="DropDownList" Value='<%# Bind("REAL_LOCATION") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200px" ClientInstanceName="RLocation">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                 <ClientSideEvents SelectedIndexChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="计算地点">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxComboBox ID="txtRGzdd" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("REAL_GZDD") %>' Width="120px"  ClientInstanceName="RGzdd">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 1px;">
                            &nbsp;
                        </td>
                        <td style="width: 90px;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="发料工位" >
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 200px;">
                            <dx:ASPxComboBox ID="txtILocation" runat="server" Height="19px" Value='<%# Bind("ISSUE_LOCATION") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200px"  ClientInstanceName="ILocation">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                    
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 90px;">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="发料地点">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 120px;">
                            <dx:ASPxComboBox ID="txtIGzdd" runat="server" Value='<%# Bind("ISSUE_GZDD") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="120px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                   
                                </ValidationSettings>
                            </dx:ASPxComboBox>
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
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="SqlRLocation" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
