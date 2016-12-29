<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ssd2200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd2200.ssd2200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function checkCqty(s, e) {
            var plancode1 = "";
            var planqty1 = "",planChangeqty1="";
            if (txtPlanCode.GetValue() == null) {
                alert("请输入计划代码！");
                return;
            }
            if (txtPlanQty.GetValue() == null) {
                alert("请输入计划代码！");
                return;
            }
            if (isNaN(txtChangeQty.GetValue())) {
                txtChangeQty.SetValue("");
                alert("计划变更数量必须为数字！");
                return;
            }
            planqty1 = txtPlanQty.GetValue();
            planChangeqty1 = txtChangeQty.GetValue();
            if (Number(planChangeqty1) > Number(planqty1)) {
                txtChangeQty.SetValue("");
                txtChangeQty.SetFocus();
                alert('变更数量超过计划总数！');
                return;
            }
            if (planChangeqty1 == "0") {
                txtChangeQty.SetValue("");
                txtChangeQty.SetFocus();
                alert('变更数量不能为0！');
                return;
            }
            plancode1 = txtPlanCode.GetValue("");
            ref = 'Check' + '@' + plancode1;
//            CallbackSubmit.PerformCallback(ref);
        }
        function initgrid(s,e) {
            var plancode1 = "";
            if (txtPlanCode.GetValue() == null) {
                alert("请输入计划代码！");
                return;
            }
            plancode1 = txtPlanCode.GetValue("");
//            grid.PerformCallback(plancode1);
            grid2.PerformCallback(plancode1);
        }
        function initSeries(s, e) {
            var webFileUrl = "?PLAN=" + s.GetValue() + "&opFlag=getPlan";

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
                alert("计划信息不存在，请检查数据！");
                txtPlanCode.SetFocus();
                txtPlanSo.SetValue("");
                txtPlanQty.SetValue("");
                return;
            }
            txtPlanSo.SetValue(result1);
            txtPlanQty.SetValue(retStr1);
//            grid.PerformCallback();
//            grid2.PerformCallback();
        }
        function filterLsh3() {
            //提交
            var ref = "";
            var plancode1 = "";
            var planqty1 = "", planChangeqty1 = "";
            butSubmit.SetEnabled(false);
            if (txtPlanCode.GetValue() == null) {
                alert("请输入计划代码！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (txtPlanSo.GetValue() == null) {
                alert("计划SO为空！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (txtPlanQty.GetValue() == null) {
                alert("计划数量为空！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (isNaN(txtChangeQty.GetValue())) {
                alert("计划变更数量必须为数字！");
                txtChangeQty.SetValue("");
                butSubmit.SetEnabled(true);
                return;
            }
            if (txtChangeQty.GetValue() == null) {
                alert("请输入计划变更数量！");
                butSubmit.SetEnabled(true);
                return;
            }
            planqty1 = txtPlanQty.GetValue();
            planChangeqty1 = txtChangeQty.GetValue();
            if (Number(planChangeqty1) > Number(planqty1)) {
                alert('变更数量超过计划总数！');
                txtChangeQty.SetValue("");
                butSubmit.SetEnabled(true);
                return;
            }

            plancode1 = txtPlanCode.GetValue("");
            planso = txtPlanSo.GetValue();

            if (confirm("该操作将对计划" + plancode1 + "退掉" + planChangeqty1 + "台份的物料，是否继续？")) {
            }
            else {
                alert("变更操作已取消！");
                butSubmit.SetEnabled(true);
                return;
            }

            ref = 'Commit' + '@' + plancode1 + ',' + planso + ',' + planqty1 + ',' + planChangeqty1;
            CallbackSubmit.PerformCallback(ref);
        }
        function submitRtr(e) {
            var result = "";
            var retStr = "";
            var array = e.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    alert(retStr);
                    butSubmit.SetEnabled(true);
                    txtPlanCode.SetValue("");
                    txtPlanSo.SetValue("");
                    txtPlanQty.SetValue("");
                    txtChangeQty.SetValue("");
//                    grid.PerformCallback();
                    grid2.PerformCallback();
                    return;
                case "Fail":
                    alert(retStr);
//                    txtPlanCode.SetValue("");
//                    txtPlanSo.SetValue("");
//                    txtPlanQty.SetValue("");
//                    txtChangeQty.SetValue("");
                    butSubmit.SetEnabled(true);
//                    grid.PerformCallback();
//                    grid2.PerformCallback();
                    return;
            }
        }

    </script>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel5" ClientInstanceName="ListBoxPanel"
        OnCallback="ASPxCallbackPanel5_Callback">
        <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                ListBoxPanel.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName = ListBoxPanel.cpCallbackName;
                                    theRet = ListBoxPanel.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   listBoxLSH1.PerformCallback();
                                }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent5" runat="server">
                <table style="background-color: #99bbbb; width: 100%;">
                <tr>
                <td colspan="7">
                    <dx:ASPxLabel runat="server" ID="ASPxLabel81" runat="server" Font-Size="Large"  ForeColor="Red" Text="该操作将对选中计划对应的三方物流要料进行调整，请谨慎使用！">
                    </dx:ASPxLabel>&nbsp&nbsp
                </td>
                </tr>
                    <tr>
                        <td style="width: 5px; height: 25px;">
                        </td>
                        <td style="width: 60px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="计划代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxTextBox ID="ASPxTextPlanCode" ClientInstanceName="txtPlanCode" runat="server"
                                Width="150px" >
                                <ClientSideEvents TextChanged="function(s,e){
                                        initSeries(s,e);
                                    }" />
                                </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: left; width: 60px;">
                            <label style="font-size: small">
                                计划SO</label>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxTextBox ID="ASPxTextPlanSo" ClientInstanceName="txtPlanSo" runat="server" ClientEnabled="false"
                                Width="150px" />
                        </td>
                        <td style="text-align: left; width: 60px;">
                               <label style="font-size: small">
                                计划数量</label>
                        </td>
                        <td style="width: 100px;">
                          <dx:ASPxTextBox ID="ASPxTextQty" ClientInstanceName="txtPlanQty" runat="server" ClientEnabled="false"
                                Width="150px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">  
                            <dx:ASPxButton ID="ButCx" Text="查询计划" Width="120px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) { initgrid(s,e); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px; height: 25px;">
                        </td>
                        <td style="width: 80px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="计划变更数量">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2" style="width: 150px;">
                            <dx:ASPxTextBox ID="ASPxTextCQty" ClientInstanceName="txtChangeQty" runat="server"
                                Width="150px" >
                                 <ClientSideEvents TextChanged="function(s,e){
                                        checkCqty(s,e);
                                    }" />
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxButton ID="ButSubmit" Text="变更提交" Width="90px" AutoPostBack="false" runat="server" ClientInstanceName="butSubmit">
                                <ClientSideEvents Click="function(s, e) { filterLsh3(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td align="center">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                <tr>
                <td>
                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" KeyFieldName="ROWID"  SettingsText-GroupPanel="三方物流计划修改历史记录"
                    AutoGenerateColumns="False" 
                    OnCustomCallback="ASPxGridView2_CustomCallback" ToolTip="三方物流计划修改历史记录" Width="600px">
                    <Settings ShowFooter="True" />
                    <Columns>
                        <dx:GridViewCommandColumn Name="CommondColumn" VisibleIndex="0" Width="40px" Caption=" ">
                            <ClearFilterButton Visible="true">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="ROWID" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" Width="100px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划SO" FieldName="PLAN_SO" Width="90px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划数量" FieldName="PLAN_NUM" Width="70px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Width="60px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="提交时间" FieldName="LOG_TIME" Width="150px" CellStyle-Wrap="False"
                            PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" Settings-AllowHeaderFilter="True">
                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="提交用户" FieldName="LOG_USER" Width="100px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " Width="80%" />
                    </Columns>
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <SettingsEditing PopupEditFormWidth="300px" />
                    <Settings ShowHorizontalScrollBar="true" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ExportMode="All" />
                </dx:ASPxGridView>
                                    <%--<SettingsPager Mode="ShowAllRecords"></SettingsPager>--%>
                    <%--<Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="0" />--%>
                <%--<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="ROWID"  SettingsText-GroupPanel="回冲池变更信息"
                    AutoGenerateColumns="False" 
                    OnCustomCallback="ASPxGridView1_CustomCallback" ToolTip="回冲池变更信息" Width="750px">

                    <Settings ShowHorizontalScrollBar="true" />
                    <Columns>
                        <dx:GridViewCommandColumn Name="CommondColumn" VisibleIndex="0" Width="20px" Caption=" ">
                            <ClearFilterButton Visible="true">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="ROWID" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="BILL_CODE" Width="120px" Settings-AllowHeaderFilter="True">
                             <Settings AutoFilterCondition="Contains" />
                             <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" Width="100px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="物料数量" FieldName="MATERIAL_NUM" Width="50px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="GYS_CODE" Width="80px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Width="60px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="QAD地点" FieldName="QADSITE" Width="70px" CellStyle-Wrap="False"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="提交用户" FieldName="LOG_USER" Width="80px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="提交时间" FieldName="LOG_TIME" Width="150px" CellStyle-Wrap="False"
                            PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" Settings-AllowHeaderFilter="True">
                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>

                        <dx:GridViewDataTextColumn Caption=" " Width="80%" />
                    </Columns>
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <SettingsEditing PopupEditFormWidth="300px" />
                    <Settings ShowFooter="True" />
                    <SettingsText GroupPanel="回冲池变更信息" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ExportMode="All" />
                </dx:ASPxGridView>--%>
                </td>
                <%--<td style="width:450px">--%>
                <%--<dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" KeyFieldName="ROWID"  SettingsText-GroupPanel="三方物流计划修改历史记录"
                    AutoGenerateColumns="False" 
                    OnCustomCallback="ASPxGridView2_CustomCallback" ToolTip="三方物流计划修改历史记录" Width="600px">
                    <Settings ShowFooter="True" />
                    <Columns>
                        <dx:GridViewCommandColumn Name="CommondColumn" VisibleIndex="0" Width="20px" Caption=" ">
                            <ClearFilterButton Visible="true">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="ROWID" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" Width="100px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划SO" FieldName="PLAN_SO" Width="90px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划数量" FieldName="PLAN_NUM" Width="50px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Width="60px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="提交时间" FieldName="LOG_TIME" Width="150px" CellStyle-Wrap="False"
                            PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" Settings-AllowHeaderFilter="True">
                            <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="提交用户" FieldName="LOG_USER" Width="100px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " Width="80%" />
                    </Columns>
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <SettingsEditing PopupEditFormWidth="300px" />
                    <Settings ShowHorizontalScrollBar="true" />
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ExportMode="All" />
                </dx:ASPxGridView>--%>
                <%--</td>--%>
                </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>
</asp:Content>
