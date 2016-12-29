<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part1400.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part1400.part1400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--按计划单独要料-三方要料--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (Pcode.GetValue() == null || PlanCode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?PCode=" + Pcode.GetValue() + " &PlanCode=" + PlanCode.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var result1 = "";
            var retStr1 = "";
            var array1 = result.split(',');
            retStr1 = array1[1];
            result1 = array1[0];
            if (result1 == "") {
                alert("计划SO不存在，请检查数据！");
                Pcode.SetFocus();
                listPSo.SetValue("");
                return;
            }
            listNum.SetValue(retStr1);
            listPSo.SetValue(result1);
        }
    </script>
    <table>
        <tr>
            <td style="height: 34px">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="查询条件">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="计划号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboPlanCode" runat="server" DataSourceID="sqlPlanCode" TextField="PLAN_CODE"
                    ValueField="PLAN_CODE" ValueType="System.String" Width="140px">
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="上线时间:" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="120px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="--" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="120px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" AutoPostBack="False">
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
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="PLAN_CODE"
        AutoGenerateColumns="False" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize">
        <SettingsEditing PopupEditFormWidth="550" PopupEditFormHeight="200" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                <EditButton Visible="True"   />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
             <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划号" FieldName="PLAN_CODE" VisibleIndex="1" Width="120px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO号" FieldName="PLAN_SO" VisibleIndex="2" Width="120px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="PLAN_NUM" VisibleIndex="3" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="上线工位" FieldName="ONLINE_LOCATION" VisibleIndex="4"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="上线时间" FieldName="ONLINE_TIME" VisibleIndex="5"
                Width="200px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="添加时间" FieldName="ADD_TIME" VisibleIndex="6" Width="200px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="是否已计算" FieldName="FLAG" VisibleIndex="7" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计算时间" FieldName="CALCULATE_TIME" VisibleIndex="8"
                Width="200px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="6">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 200px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线" width="200px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtPCode" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("GZDD") %>' Width="160px" ClientInstanceName="Pcode">
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
                        <td style="width: 200px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="计划号"  width="200px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxComboBox ID="txtPlanCode" runat="server" ClientInstanceName="PlanCode" DropDownStyle="DropDownList"
                                Text='<%# Bind("PLAN_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px">
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
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 200px;">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="数量"  width="200px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px;">
                            <dx:ASPxTextBox ID="txtPNum" ClientInstanceName="listNum" runat="server" Text='<%# Bind("PLAN_NUM") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="160px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                    <RegularExpression ErrorText="计划数量必须为整数！" ValidationExpression="^\+?[1-9][0-9]*$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px;">
                            &nbsp;
                        </td>
                        <td style="width: 200px;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SO："  width="200px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px;">
                            <dx:ASPxTextBox ID="txtPSo" runat="server" ClientInstanceName="listPSo" Height="19px"
                                Text='<%# Bind("PLAN_SO") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="上线时间:"  width="200px">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="DateOnlinetime" runat="server" Value='<%# Bind("ONLINE_TIME") %>'  Width="160px"
                                EditFormatString="yyyy-MM-dd HH:mm:ss" EditFormat="DateTime">
                                <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                </CalendarProperties>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                        </td>
                        <td style="width: 200px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="上线工位"  width="200px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxComboBox ID="txtOnLocation" runat="server" DropDownStyle="DropDownList" Text='<%# Bind("ONLINE_LOCATION") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="140px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 30px; text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" 
                                ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" 
                                ReplacementType="EditFormCancelButton" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sqlPlanCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
</asp:Content>
