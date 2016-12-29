<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part1500.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part1500.part1500" %>

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
<%--按物料单独要料-三方要料--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        //根据生产线初始化要料地点20161115
        var pline;
        function filterPlace() {
            Qsite.ClearItems(); //要料地点

            pline = Pcode.GetValue().toString();

            Qsite.PerformCallback(pline);

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
                    ClientInstanceName="Qsite"   OnCallback="TextQadSite_Callback"   >
                    <%--ValueField="QADSITE" ValueType="System.String"--%>
                </dx:ASPxComboBox>
            </td>
            <td>
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
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="要料类型:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="TextTName" runat="server" DataSourceID="sqlTName" ValueField="TYPE_NAME"
                    TextField="TYPE_NAME" ValueType="System.String" Width="80px" >
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
            <td style="width: 20px;">
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" AutoPostBack="False" Width="80px">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <%--<td style="width: 100px;">
                <asp:Label ID="Label1" runat="server" Text="打开ExceL文件"></asp:Label>
            </td>
            <td style="width: 200px;">
                <input id="File1" type="file" accept="application/msexcel" size="20" style="font-size: medium;
                    height: 25px;" alt="请选择Excel文件" runat="server" />
            </td>
            <td style="width: 100px">
                <dx:ASPxButton ID="ASPxButton_Import" runat="server" AutoPostBack="true" Text="导入"
                    Width="100px" OnClick="ASPxButton_Import_Click">
                </dx:ASPxButton>
            </td>--%>
            <%--<td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" Width="140px" />
            </td>--%>
        </tr>
    </table>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel6" ClientInstanceName="ListBoxPanel6"
        OnCallback="ASPxCallbackPanel6_Callback">
        <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                ListBoxPanel6.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName = ListBoxPanel6.cpCallbackName;
                                    theRet = ListBoxPanel6.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    grid.PerformCallback();
                                }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent6" runat="server">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 60px;">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="到货时间:" />
                        </td>
                        <td style="width: 120px;">
                            <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="120px">
                                <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                                </CalendarProperties>
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="width: 100px;">
                            <asp:Label ID="Label1" runat="server" Text="打开ExceL文件"></asp:Label>
                        </td>
                        <td style="width: 200px;">
                            <input id="File1" type="file" accept="application/msexcel" size="20" style="font-size: medium;
                                height: 25px;" alt="请选择Excel文件" runat="server" />
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxButton ID="ASPxButton_Import" runat="server" AutoPostBack="true" Text="导入"
                                Width="100px" OnClick="ASPxButton_Import_Click">
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="导出-EXCEL" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />
                        </td>
                        <td style="text-align: right;">
                            <a href="../../File/三方要料模板.xls">三方要料模板</a>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="MATERIAL_CODE"
        AutoGenerateColumns="False" OnCustomCallback="ASPxGridView1_CustomCallback" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
        OnInitNewRow="ASPxGridView1_InitNewRow">
        <SettingsEditing PopupEditFormWidth="900" PopupEditFormHeight="250" />
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
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="MATERIAL_NAME" VisibleIndex="2"
                Width="140px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="3" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="MATERIAL_NUM" VisibleIndex="4"
                Width="60px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="要料类型" FieldName="TYPE_NAME" VisibleIndex="5"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料地点" FieldName="QADSITE" VisibleIndex="6" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="上线时间" FieldName="ONLINE_TIME" VisibleIndex="7"
                Width="140px" Settings-AutoFilterCondition="Contains">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd"></PropertiesTextEdit>
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
            <dx:GridViewDataTextColumn Caption="添加用户" FieldName="ADD_YHDM" VisibleIndex="10"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计算时间" FieldName="CALCULATE_TIME" VisibleIndex="11"
                Width="140px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="是否冲抵" FieldName="HC_FLAG" VisibleIndex="12" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="备注" FieldName="BILL_REMARK" VisibleIndex="13"
                Width="120px" Settings-AutoFilterCondition="Contains">
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
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="是否冲抵回冲池">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px;">
                            <dx:ASPxComboBox ID="txtHcFLAG" runat="server" DropDownStyle="DropDownList" Value='<%# Bind("HC_FLAG") %>'
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
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="要料类型">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxComboBox ID="txtTName" runat="server" DropDownStyle="DropDownList" Text='<%# Bind("TYPE_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="140px"
                                >
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="是否柳汽:">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="txtLqFlag" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("LQ_FLAG") %>' Width="120px" ClientInstanceName="Pcode">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                            <td>
                            </td>
                        <td style="width: 90px">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="上线时间">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxDateEdit ID="txtOnTime" runat="server" EditFormat="DateTime" EditFormatString="yyyy-MM-dd HH:mm:ss"
                                Value='<%# Bind("ONLINE_TIME") %>' Width="160px">
                                <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                </CalendarProperties>
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="width: 29px">
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="是否按最小包装要料" Width="140px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 90px">
                            <dx:ASPxComboBox ID="txtPackFlag" runat="server" DropDownStyle="DropDownList" Value='<%# Bind("PACK_FLAG") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="140px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr style="width: 800px;">
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 78px">
                            <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="备注">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtRemark" runat="server" Text='<%# Bind("BILL_REMARK") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="438px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过500！" ValidationExpression="^.{0,500}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="150" style="height: 30px; text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td style="width: 29px">
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sqlTName" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlQadSite" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
</asp:Content>
