<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part2B00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part2B00.part2B00" %>

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
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%--按物料单独要料-kufang要料--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        //根据生产线初始化要料地点20161115
        var pline;
        function filterPlace() {
            Qsite.ClearItems(); //要料地点

            pline = Pcode.GetValue().toString();

            Qsite.PerformCallback(pline);
            //            LCode.PerformCallback(pline);
        }
        function initEditSeries(s, e) {
            if (Pcode.GetValue() == null) {
                return;
            }
            var aaa = Pcode.GetValue();
            var webFileUrl = "?Pcode=" + Pcode.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;


            var pcode = "";
            var array1 = result.split(',');

            pcode = array1[1];

            if (pcode == "") {
                alert("生产线请选择E或W");
                Pcode.SetFocus();
                Qsite.SetValue("");
                return;
            }

            Qsite.SetValue(pcode);

        }
        function initEditSeries2(s, e) {
            if (Pcode2.GetValue() == null) {
                return;
            }
            var webFileUrl = "?Pcode=" + Pcode2.GetValue() + " &opFlag=getEditSeries2";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;


            var pcode = "";
            var array1 = result.split(',');

            pcode = array1[1];

            if (pcode == "") {

                // Pcode.SetFocus();
                Qsite2.SetValue("");
                return;
            }

            Qsite2.SetValue(pcode);
        }
        function MATERIAL(s, e) {
            if (MCode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?MCODE=" + MCode.GetValue() + " &opFlag=getMATERIAL";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            if (result == "no") {
                alert("没有该零件!");
                MCode.SetFocus();
                MName.SetValue("");
                return;
            }

            MName.SetValue(result);
        }
        function GYS(s, e) {
            if (GCode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?GCODE=" + GCode.GetValue() + " &opFlag=getGYS";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            if (result == "no") {
                alert("没有该供应商!");
                GCode.SetFocus();
                GName.SetValue("");
                return;
            }

            GName.SetValue(result);
        }
    </script>
    <table>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="生产线">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPlineCode" DataSourceID="SqlCode" ValueField="pline_code"
                    TextField="pline_name" runat="server" DropDownStyle="DropDownList" Width="70px"
                    ClientInstanceName="Pcode">
                    <ClientSideEvents SelectedIndexChanged="function(s,e){
                    filterPlace(s,e);
                }" />
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="要料地点:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="TextQadSite" runat="server" DropDownStyle="DropDownList" Width="80px"
                    ClientInstanceName="Qsite" OnCallback="TextQadSite_Callback">
                   
                </dx:ASPxComboBox>
            </td>
            <td>
                
                    <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="供应商:">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="TextGCode" runat="server" ValueField="GYS_CODE" ValueType="System.String"
                        Width="100px">
                    </dx:ASPxTextBox>
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="物料代码:">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="TextMCode" runat="server" ValueField="MATERIAL_CODE" ValueType="System.String"
                        Width="100px">
                    </dx:ASPxTextBox>
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
                <td style="width: 20px;">
                </td>
                <td>
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" AutoPostBack="False" Width="80px">
                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="导出-EXCEL" UseSubmitBehavior="False"
                        OnClick="btnXlsExport_Click" />
                </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
           
        </tr>
    </table>
    
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="MATERIAL_CODE"
        AutoGenerateColumns="False" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
        OnInitNewRow="ASPxGridView1_InitNewRow">
        <SettingsEditing PopupEditFormWidth="900" PopupEditFormHeight="170" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                <EditButton Visible="True" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" VisibleIndex="2"
                Width="100px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="MATERIAL_NAME" VisibleIndex="2"
                Width="160px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="3" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="MATERIAL_NUM" VisibleIndex="4"
                Width="60px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料地点" FieldName="QADSITE" VisibleIndex="6" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="上线时间" FieldName="ONLINE_TIME" VisibleIndex="7"
                Width="140px" Settings-AutoFilterCondition="Contains">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="上线工位" FieldName="ONLINE_LOCATION" VisibleIndex="7"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="是否已计算" FieldName="FLAG" VisibleIndex="8" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="添加时间" FieldName="ADD_TIME" VisibleIndex="9" Width="140px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计算时间" FieldName="CALCULATE_TIME" VisibleIndex="11"
                Width="140px" Settings-AutoFilterCondition="Contains">
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
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtPCode" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("GZDD") %>' Width="120px" ClientInstanceName="Pcode2" Height="18px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                    initEditSeries2(s,e);
                }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 1px">
                            &nbsp;
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="物料代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtMCode" runat="server" ClientInstanceName="MCode" Width="160px"
                                Text='<%# Bind("MATERIAL_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){
                    MATERIAL(s,e);
                }" />
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 29px">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="物料名称">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtMName" runat="server" ClientInstanceName="MName" Width="140px"
                                Text='<%# Bind("MATERIAL_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="供应商代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtGCode" runat="server" Width="120px" Text='<%# Bind("GYS_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" ClientInstanceName="GCode">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){
                    GYS(s,e);
                }" />
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 90px;">
                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="供应商名称">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px;">
                            <dx:ASPxTextBox ID="txtGName" runat="server" ClientInstanceName="GName" Text='<%# Bind("GYS_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="160px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 29px;">
                            &nbsp;
                        </td>
                        <td style="width: 140px;">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="工位">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px;">
                            <dx:ASPxComboBox ID="txtLocation" runat="server" ClientInstanceName="LCode" DropDownStyle="DropDownList"
                                Value='<%# Bind("ONLINE_LOCATION") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px" DataSourceID="sqlLocation" TextField="LOCATION_CODE" ValueField="LOCATION_CODE"
                                OnInit="txtLocation_Init">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="物料地点:">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtQSite" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("QADSITE") %>' Width="120px" ClientInstanceName="Qsite2">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                            <td>
                            </td>
                            <td style="width: 90px">
                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="数量">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 90px">
                                <dx:ASPxTextBox ID="txtMNum" runat="server" ClientInstanceName="listNum" Text='<%# Bind("MATERIAL_NUM") %>'
                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="160px">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                        ValidateOnLeave="True">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                        <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                        <RegularExpression ErrorText="计划数量必须为整数！" ValidationExpression="^\+?[1-9][0-9]*$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 29px">
                            </td>
                            <td style="width: 90px">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="上线时间">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 90px">
                                <dx:ASPxDateEdit ID="txtOnTime" runat="server" EditFormat="DateTime" EditFormatString="yyyy-MM-dd HH:mm:ss"
                                    Value='<%# Bind("ONLINE_TIME") %>' Width="140px">
                                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                    </CalendarProperties>
                                </dx:ASPxDateEdit>
                            </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="width: 780px">
                        </td>
                        <td style="height: 30px; text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlLocation" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
</asp:Content>
