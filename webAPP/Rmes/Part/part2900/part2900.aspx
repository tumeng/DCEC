<%@ Page Title="线边库存维护" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part2900.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part2900.part2900" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%--线边库存维护 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td style="height: 30px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="查询条件">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboGzdd" runat="server" ValueField="PLINE_CODE" TextField="PLINE_NAME"
                    Width="120px" ClientInstanceName="plineC">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocationBGY(); }" />
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="工位:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbLocation" ClientInstanceName="cmbLocationC" runat="server"
                    Width="120px" OnCallback="cmbLocation_Callback">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="零件号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtMatCode" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="供应商:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtGysCode" runat="server" Width="120px">
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
                <dx:ASPxButton ID="btnXlsExportPlan" runat="server" Text="导出" UseSubmitBehavior="False"
                    OnClick="btnXlsExportPlan_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="cmdSet" runat="server" Text="置所有数据为零" 
                    UseSubmitBehavior="False" onclick="cmdSet_Click"
                   >
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="GZDD;LOCATION_CODE;MATERIAL_CODE;GYS_CODE" ClientInstanceName="grid"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowUpdating="ASPxGridView1_RowUpdating" 
        OnRowValidating="ASPxGridView1_RowValidating" 
        onhtmleditformcreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsEditing PopupEditFormWidth="570px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
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
            <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="2"
                Width="150px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" VisibleIndex="3"
                Width="150px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <%--<dx:GridViewDataTextColumn Caption="零件名称" FieldName="PT_DESC2" VisibleIndex="4" Width="100px"
                Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="保管员" FieldName="IN_QADC01" VisibleIndex="5" Width="100px"
                Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="LINESIDE_NUM" VisibleIndex="7"
                Width="120px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="6"
                Width="150px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="" FieldName="" VisibleIndex="9"
                Width="80%" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <%--<dx:GridViewDataTextColumn Caption="供应商名称" FieldName="VD_SORT" VisibleIndex="8" Width="100px"
                Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>--%>
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
                        <td style="width: 140px">
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
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    initEditM(s,e);
                                    }" />
                            </dx:ASPxComboBox>
                            
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
                            <dx:ASPxComboBox ID="txtGYSCodeA" runat="server" ClientInstanceName="GCode"
                                EnableClientSideAPI="True" Value='<%# Bind("GYS_CODE") %>' DropDownStyle="DropDownList"
                                ValueType="System.String" Width="150px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <%--DataSourceID="SqlGYScode" TextField="AD_ADDR" ValueField="AD_ADDR" >--%>
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    initEditGYS(s,e);
                                    }" />
                            </dx:ASPxComboBox>
                            
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
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="工位">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxComboBox ID="comboLocation" runat="server" ClientInstanceName="comboLocationC"
                                EnableClientSideAPI="True" Value='<%# Bind("LOCATION_CODE") %>' DropDownStyle="DropDownList"
                                ValueType="System.String" Width="150px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" OnCallback="comboLocation_Callback">
                                <%--DataSourceID="SqlLcode" TextField="AD_ADDR" ValueField="AD_ADDR" >--%>
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                                <%--<ClientSideEvents SelectedIndexChanged="function(s,e){
                                    initEditGYS(s,e);
                                    }" />--%>
                            </dx:ASPxComboBox>
                            
                        </td>
                        
                        <td style="width: 1px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="数量">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 170px">
                            <dx:ASPxTextBox ID="txtMNum" runat="server" Text='<%# Bind("LINESIDE_NUM") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
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
                    <tr>
                        <td style="height: 30px" colspan="7">
                        </td>
                    </tr>
                    </table>
            </center>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
    <%--<dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>--%>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="sqlGzdd" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlLocation" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlBgy" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <script type="text/javascript">
        var pline;
        var locationCode;
        var GCode;

        if (!String.prototype.trim) {
            String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
        }
        var pline;
        function filterMCode() {
            MCode.ClearItems(); //物料代码

            pline = PcodeC.GetValue().toString();

            MCode.PerformCallback(pline);
            comboLocationC.PerformCallback(pline);

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
        
        //根据生产线初始化工位
        function filterLocationBGY() {
            cmbLocationC.ClearItems(); //工位

            pline = plineC.GetValue().toString();

            cmbLocationC.PerformCallback(pline);

        }
    </script>
</asp:Content>
