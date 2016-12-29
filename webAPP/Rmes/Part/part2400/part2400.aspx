<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="part2400.aspx.cs" Inherits="Rmes_part2400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：库存待冲抵物料维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        var pline;
        function filterMCode() {
            MCode.ClearItems(); //物料代码

            pline = PcodeC.GetValue().toString();

            MCode.PerformCallback(pline);

        }
        function initEditM(s, e) {
            if (MCode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?MCODE=" + MCode.GetValue() + "&opFlag=getEditM";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var mcode = "";
            var array1 = result.split(',');
            mcode = array1[0];

            if (mcode == "") {
                alert("该零件不存在，请检查数据！");
                MCode.SetFocus();
                MName.SetValue("");
                return;
            }
            MName.SetValue(mcode);
        }
        function initEditGYS(s, e) {
            if (GCode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?GCODE=" + GCode.GetValue() + "&opFlag=getEditGYS";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var gcode = "";
            var array1 = result.split(',');
            gcode = array1[0];

            if (gcode == "") {
                alert("该供应商不存在，请检查数据！");
                GCode.SetFocus();
                GName.SetValue("");
                return;
            }
            GName.SetValue(gcode);
        }
    </script>
    <table>
        <tr>
            <td style="height: 30px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="查询条件">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="生产线">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="comPLQ"  ValueField="PLINE_CODE" TextField="SHOWTEXT"
                    runat="server" DropDownStyle="DropDownList" Width="70px" ClientInstanceName="Pcode"><%--DataSourceID="SqlPL"--%>
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="物料代码:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtMatCodeQ" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="供应商:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtGysCodeQ" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" onclick="btnQuery_Click">
                    <ClientSideEvents Click="function(s, e){
                        grid.PerformCallback();
                        }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="SureAll" runat="server" Text="全部确认" UseSubmitBehavior="False"
                    OnClick="SureAll_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExportPlan" runat="server" Text="导出" UseSubmitBehavior="False"
                    OnClick="btnXlsExportPlan_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" Settings-ShowFilterRow="false"
        Width="100%" KeyFieldName="GZDD;GYS_CODE;MATERIAL_CODE" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback" OnRowValidating="ASPxGridView1_RowValidating"
        OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsEditing PopupEditFormWidth="570px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="150px">
                <EditButton Visible="True" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="SureCD" Text="确认冲抵">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1"
                Settings-AutoFilterCondition="Contains" Width="120px" Visible="false" />
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" VisibleIndex="1"
                Settings-AutoFilterCondition="Contains" Width="120px" />
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption="数量" FieldName="MATERIAL_NUM" VisibleIndex="3"
                Settings-AutoFilterCondition="Contains" Width="100px" />
            <dx:GridViewDataTextColumn Caption="操作用户" FieldName="ADD_YH" VisibleIndex="4" Settings-AutoFilterCondition="Contains"
                Width="80px" />
            <dx:GridViewDataDateColumn Caption="添加时间" FieldName="ADD_TIME" VisibleIndex="6" Settings-AutoFilterCondition="Contains"
                Width="160px" PropertiesDateEdit-EditFormat="DateTime" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="可以冲抵" FieldName="READY_FLAG" VisibleIndex="5"
                Settings-AutoFilterCondition="Contains" Width="100px" />
            <dx:GridViewDataDateColumn Caption="最后一次冲抵时间" FieldName="LAST_HANDLE_TIME" VisibleIndex="6"
                Settings-AutoFilterCondition="Contains" Width="160px" PropertiesDateEdit-EditFormat="DateTime"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="最后一次冲抵数量" FieldName="LAST_HANDLE_NUM" VisibleIndex="7"
                Settings-AutoFilterCondition="Contains" Width="160px" />
            <dx:GridViewDataTextColumn Caption="单据号" FieldName="BILL_CODE" VisibleIndex="8" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxComboBox ID="comboPCode" ValueField="PLINE_CODE" Value='<%# Bind("GZDD") %>'
                                TextField="PLINE_CODE" runat="server" DropDownStyle="DropDownList" Width="150px"
                                ClientInstanceName="PcodeC">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                        filterMCode(s,e);
                                    }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 90px">
                        </td>
                        <td style="width: 180px">
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="物料代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxComboBox ID="txtMCode" runat="server" ClientInstanceName="MCode"
                                EnableClientSideAPI="True" Value='<%# Bind("MATERIAL_CODE") %>' DropDownStyle="DropDownList"
                                ValueType="System.String" Width="150px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                OnCallback="txtMCode_Callback">
                                <%--DataSourceID="SqlDataSource2"TextField="STATION_NAME"  ValueField="STATION_NAME" --%>
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    initEditM(s,e);
                                    }" />
                            </dx:ASPxComboBox>
                            <%--<dx:ASPxGridLookup ID="txtMCode" runat="server" ClientInstanceName="MCode" SelectionMode="Single"
                                KeyFieldName="MATERIAL_CODE" Value='<%# Bind("MATERIAL_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px" OnCallback="txtMCode_Callback">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager NumericButtonCount="5">
                                    </SettingsPager>
                                </GridViewProperties>
                                <Columns>
                                    <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                    <dx:GridViewDataColumn Caption="物料代码" FieldName="MATERIAL_CODE" />
                                </Columns>
                                <ClientSideEvents TextChanged="function(s,e){
                                        initEditM(s,e);
                                    }" />
                            </dx:ASPxGridLookup>--%>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="物料名称" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px;">
                            <dx:ASPxTextBox ID="txtMName" runat="server" ClientInstanceName="MName" Height="19px"
                                Value='<%# Bind("PT_DESC2") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px" ClientEnabled="false">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过200！" ValidationExpression="^.{0,200}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="供应商代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxComboBox ID="txtGYSCode" runat="server" ClientInstanceName="GCode"
                                EnableClientSideAPI="True" Value='<%# Bind("GYS_CODE") %>' DropDownStyle="DropDownList"
                                ValueType="System.String" Width="150px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                DataSourceID="SqlGYScode" TextField="AD_ADDR" ValueField="AD_ADDR" >
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    initEditGYS(s,e);
                                    }" />
                            </dx:ASPxComboBox>
                            <%--<dx:ASPxGridLookup ID="txtGYSCode" runat="server" ClientInstanceName="GCode" SelectionMode="Single"
                                KeyFieldName="AD_ADDR" Value='<%# Bind("GYS_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px" DataSourceID="SqlGYScode">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RequiredField ErrorText="供应商代码不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager NumericButtonCount="5">
                                    </SettingsPager>
                                </GridViewProperties>
                                <Columns>
                                    <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                    <dx:GridViewDataColumn Caption="供应商代码" FieldName="AD_ADDR" />
                                </Columns>
                                <ClientSideEvents TextChanged="function(s,e){
                                        initEditGYS(s,e);
                                    }" />
                            </dx:ASPxGridLookup>--%>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="供应商名称" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px;">
                            <dx:ASPxTextBox ID="txtGYSName" runat="server" ClientInstanceName="GName" Height="19px"
                                Value='<%# Bind("AD_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px" ClientEnabled="false">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过200！" ValidationExpression="^.{0,200}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="数量">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxTextBox ID="txtMNum" runat="server" Text='<%# Bind("MATERIAL_NUM") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="False" />
                                    <RegularExpression ErrorText="数量必须为整数！" ValidationExpression="^\+?[1-9][0-9]*$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 90px">
                        </td>
                        <td style="width: 180px">
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="height: 10px" colspan="7">
                        </td>
                    </tr>--%>
                   
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
                    <tr>
                        <td style="height: 30px" colspan="7">
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
                confirm('确认要删除这条记录吗？');
               alert(theRet);
            }
           
            if(callbackName == 'refresh') 
            {
                grid2.PerformCallback();
            }
            
        }" />
    </dx:ASPxGridView>
    <table>
        <tr>
            <td style="height: 30px">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="冲抵日志">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <%--<td>
                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="生产线">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPlineCode" DataSourceID="SqlCode" ValueField="PLINE_CODE"
                    TextField="PLINE_NAME" runat="server" DropDownStyle="DropDownList" Width="70px"
                    ClientInstanceName="Pcode">
                </dx:ASPxComboBox>
            </td>--%>
            <td>
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="物料代码:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtMatCodeLog" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="供应商:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtGysCodeLog" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="冲抵时间:" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="StartDateQ" runat="server" Width="120px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="--" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="EndDateQ" runat="server" Width="120px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="单据号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtBillCodeQ" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxButton ID="ButtonLog" runat="server" Text="查询" 
                    onclick="ButtonLog_Click">
                    <ClientSideEvents Click="function(s, e){
                        grid2.PerformCallback();
                        }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="false" Settings-ShowFilterRow="false"
        KeyFieldName="GZDD;GYS_CODE;MATERIAL_CODE" Width="100%" OnCustomCallback="ASPxGridView2_CustomCallback">
        <Columns>
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" VisibleIndex="1"
                Settings-AutoFilterCondition="Contains" Width="120px" />
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption="当次冲抵数量" FieldName="CUR_HANDLE_NUM" VisibleIndex="3"
                Settings-AutoFilterCondition="Contains" Width="160px" />
            <dx:GridViewDataDateColumn Caption="当次冲抵时间" FieldName="CUR_HANDLE_TIME" VisibleIndex="6"
                Settings-AutoFilterCondition="Contains" Width="160px" PropertiesDateEdit-EditFormat="DateTime"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="单据号" FieldName="BILL_CODE" VisibleIndex="8" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption="操作说明" FieldName="CZSM" VisibleIndex="10" Settings-AutoFilterCondition="Contains"
                Width="80px" Visible="false" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="SqlGYScode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="SqlMcode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="SqlPL" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
