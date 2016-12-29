﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ssd1030.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd1030" %>
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

        function initListBom() {
            var strSeries = listSeries.GetValue().toString();
            listBomIndex.PerformCallback(strSeries);
        }

        //        var butSubmit = document.getElementById("ButSubmit");  //提交按钮，通过提交按钮激活与否控制作业过程
        function OnMoreInfoClick(element, key) {
            callbackPanel.SetContentHtml("");
            popup11.ShowAtElement(element);
            keyValue = key;
        }
        function popup_Shown(s, e) {
            callbackPanel.PerformCallback(keyValue);
        }
        function OnMoreInfoClick1(element, key) {
            callbackPanel.SetContentHtml("");
            popup12.ShowAtElement(element);
            keyValue = key;
        }
        function popup_Shown1(s, e) {
            callbackPanel1.PerformCallback(keyValue);
        }
        function checkSubmit() {
            var ref = "";

            butSubmit.SetEnabled(false);

            if (txtPlanCode.GetValue() == null) {
                alert("请输入计划代码！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (txtSeq.GetValue() == null) {
                alert("请输入执行序号！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (isNaN(txtSeq.GetValue())) {
                alert("执行序号必须为数字！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (ComoBoxSO.GetValue() == null) {
                alert("请输入SO！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (txtSeries.GetValue() == null) {
                alert("请输入产品系列！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (txtQty.GetValue() == null) {
                alert("请输入计划数量！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (isNaN(txtQty.GetValue())) {
                alert("计划数量必须为数字！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (dateBegin.GetDate() == null) {
                alert("请选择开始日期！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (dateEnd.GetDate() == null) {
                alert("请选择结束日期！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (dateEnd.GetDate() < dateBegin.GetDate()) {
                alert("结束日期不能小于开始日期！");
                butSubmit.SetEnabled(true);
                return;
            }

            if (listPline.GetValue() == null) {
                alert("请选择生产线！");
                butSubmit.SetEnabled(true);
                return;
            }

            if (listProcess.GetValue() == null) {
                alert("请选择工艺路线！");
                butSubmit.SetEnabled(true);
                return;
            }

            if (txtCustomerName.GetValue() == null) {
                alert("请输入客户名称！");
                butSubmit.SetEnabled(true);
                return;
            }

            CallbackSubmit.PerformCallback(ref);
        }
        function checkYlh() {
            var ref = "";

            if (YLH.GetValue() == null) {
                alert("请输入预留号！");
                return;
            }
            if (YLHsl.GetValue() == null) {
                alert("请输入预留号数量！");
                return;
            }
            var ylh1 = YLH.GetValue();
            var ylhsl1 = YLHsl.GetValue();

            if (ylh1.length != 8) {
                alert('预留号位数不合法！');
                return;
            }
            if (isNaN(YLHsl.GetValue())) {
                alert("预留号数量必须为数字！");
                return;
            }

            var yqty = labelqty.GetValue();
            var zqty = Panel1_Qty.GetValue();
            if (ylhsl1 > (zqty - yqty)) {
                alert('分配数量超过计划总数！');
                return;
            }

            ListBoxPanel.PerformCallback('YLH' + '|' + ylh1 + '|' + ylhsl1);
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
                    initGridview();
                    return;
                case "Fail":
                    alert(retStr);
                    butSubmit.SetEnabled(true);
                    return;
            }
        }
        function ClosePop(e) {
            initGridview();
        }

        function initGridview() {
            grid.PerformCallback();
        }
        function initPSO() {
            var pline = plineedit1.GetValue();
            SoCombo11.PerformCallback(pline);
        }
        function initSeries(s, e) {
            //            Series.PerformCallback(ComoBoxSO.GetValue());
            var webFileUrl = "?SO=" + s.GetValue() + "&opFlag=getEditSeries";

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
                alert("机型不存在，请检查数据！");
                chkTflag.SetEnabled(true);
                chkTflag.SetValue(false);
                ComoBoxSO.SetFocus();
                txtRemark.SetValue(""); //GCICHEAD
                txtSeries.SetValue("");
                return;
            }
            txtRemark.SetValue(retStr1);
            txtSeries.SetValue(result1);
        }
        function initSeries1(s, e) {
            if (chkTflag.GetValue() != false) {
                txtSeries.SetValue("GCICHEAD"); //GCICHEAD
            }
            else {
                txtSeries.SetValue(""); //GCICHEAD
            }
        }
        function initEditSeries(s, e) {
            var webFileUrl = "?SO=" + s.GetValue() + "&opFlag=getEditSeries";

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
                alert("机型不存在，请检查数据！");
                ComoBoxSO.SetFocus();
                txtRemark1.SetValue("");
                EditSeries.SetValue("");
                return;
            }
            txtRemark1.SetValue(retStr1);
            EditSeries.SetValue(result1);
        }

        function changeSeq(s, e) {
            index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);
            //        if (buttonID == "Up") {
            //            grid.GetValuesOnCustomCallback("up", GetDataCallback);
            //        }
            //        else if (buttonID == "Down") {
            //            grid.GetSelectedFieldValues("Down", GetDataCallback);
            //        }
        }

        function ShowPopup1() {
            var count = grid.GetSelectedRowCount();
            if (count > 1) {
                alert("分配流水号只允许选择单条计划");
                return;
            }
            if (count < 1) {
                alert("请选择一条计划");
                return;
            }
            else {
                var filedNames = "PLINE_CODE;PLAN_SO;PRODUCT_MODEL;PLAN_CODE;PLAN_QTY;REMARK1";
                grid.GetSelectedFieldValues(filedNames, GetValues);
            }
        }
        function ShowPopup2() {
            var count = grid.GetSelectedRowCount();
            if (count = 0) {
                alert("请选择计划");
                return;
            }
            else {
                autosn.SetEnabled(false);
                //                ref = 'LS,AA';
                var fieldNames = "RMES_ID";
                grid.GetSelectedFieldValues(fieldNames, RecoverResult);
                //                CallbackSubmit.PerformCallback(ref);
            }
        }
        function RecoverResult(result) {
            if (result == "") {
                autosn.SetEnabled(true);
                alert('没有选择任何计划！无法进行操作');
            }
            else {
                ref = "LS," + result;
                CallbackSubmit.PerformCallback(ref);
            }
        }

        function GetValues(values) {

            Panel1_Pline.SetEnabled(true);
            Panel1_Pline.SetValue(values[0][0]);
            Panel1_Pline.SetEnabled(false);
            Panel1_SO.SetValue(values[0][1]);
            Panel1_Series.SetValue(values[0][2]);
            Panel1_PlanCode.SetValue(values[0][3]);
            Panel1_Qty.SetValue(values[0][4]);
            Panel1_Remark.SetValue(values[0][5]);
            Panel1_SO.SetEnabled(false);
            Panel1_Series.SetEnabled(false);
            Panel1_PlanCode.SetEnabled(false);
            Panel1_Qty.SetEnabled(false);
            Panel1_Remark.SetEnabled(false);
            labelqty.SetValue('0');
            popup1.Show();
            ListBoxPanel.PerformCallback('js' + '|' + values[0][3]);

            //            ListBoxUsed.PerformCallback();
            //            ListBoxUnused.PerformCallback();
        }

        function GetDataCallback(s) {
            var result = "";
            var retStr = "";
            if (s == null) {
                grid.PerformCallback();
                return;
            }
            var array = s.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    alert(retStr);
                    initGridview();
                    return;
                case "Fail":
                    alert(retStr);
                    return;
            }
            grid.PerformCallback();
        }

        String.prototype.endWith = function (endStr) {
            var d = this.length - endStr.length;
            return (d >= 0 && this.lastIndexOf(endStr) == d)
        }

        function Import() {
            var File11 = document.getElementById("ctl00$ContentPlaceHolder1$ASPxCallbackPanel6$File1");
            var ref = File11.value;
            ListBoxPanel6.PerformCallback(ref);
        }
        function Work(workflag) {
            if (workflag == 'add') {
                var items = ListBoxUnused.GetSelectedItems();
                if (items.length == 0) {
                    alert('请选择一个流水号！');
                    return;
                };
                var ids = "";
                var _ids = "";
                var yqty = labelqty.GetValue();
                var zqty = Panel1_Qty.GetValue();
                if (items.length > (zqty - yqty)) {
                    alert('分配数量超过计划总数！');
                    return;
                }
                for (var i = 0; i < items.length; i++) {
                    ids = ids + items[i].value + ",";
                }
                if (ids.endWith(','))
                    _ids = ids.substring(0, ids.length - 1);
                ListBoxPanel.PerformCallback(workflag + '|' + _ids);
            }
            if (workflag == 'del') {
                var items = ListBoxUsed.GetSelectedItems();
                if (items.length == 0) {
                    alert('请选择一个流水号！');
                    return;
                };
                var ids = "";
                var _ids = "";
                for (var i = 0; i < items.length; i++) {
                    ids = ids + items[i].value + ",";
                }
                if (ids.endWith(','))
                    _ids = ids.substring(0, ids.length - 1);
                ListBoxPanel.PerformCallback(workflag + '|' + _ids);
            }
        }

    </script>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel6" ClientInstanceName="ListBoxPanel6"
        OnCallback="ASPxCallbackPanel6_Callback" >
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
                <table style="background-color: #99bbbb; width: 100%">
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">
                            <asp:Label ID="Label1" runat="server" Text="打开ExceL文件"></asp:Label>
                        </td>
                        <td style="width: 200px;">
                            <%--<dx:ASPxUploadControl ValidationSettings-AllowedFileExtensions="'.xls','.doc','.jpg'" ID="File2" runat="server" UploadButton-Text="导入" BrowseButton-Text="浏览"></dx:ASPxUploadControl>--%>
                            <input id="File1" type="file" accept="application/msexcel" size="20" style="font-size: medium; 
                                height: 25px;" alt="请选择Excel文件" runat="server" />
                        </td><%--OnClick="ASPxButton_Import_Click"  OnCallback="ASPxCallbackPanel6_Callback"  --%>
                        <td style="width: 100px">
                            <dx:ASPxButton ID="ASPxButton_Import" runat="server" AutoPostBack="true" Text="导入"
                                Width="100px"  OnClick="ASPxButton_Import_Click"  >
<%--                                <ClientSideEvents Click="function (s,e){
                                                                        Import();
                                                                        
                                                                    }" />--%>
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出-EXCEL" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />
                        </td>
                        <td style="text-align: right;">
                            <a href="../../File/分装计划模板.xls">计划模板</a>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>

    <table style="background-color: #99bbbb; width: 100%;">
        <tr>
            <td style="width: 5px; height: 25px;">
            </td>
            <td style="text-align: left; width: 70px;">
                <label style="font-size: small">
                    开始日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" EditFormatString="yyyy-MM-dd"
                    Width="120px">
                    <ClientSideEvents DateChanged="function(s,e){
                        dateBeginPanel.PerformCallback();
                        dateEndPanel.PerformCallback();
                    }" />
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 5px">
            </td>
            <td style="text-align: left; width: 70px">
                <label style="font-size: small">
                    结束日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" EditFormatString="yyyy-MM-dd"
                    Width="120px">
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 100px;">
                <dx:ASPxButton ID="ButSubmit" Text="查询计划" Width="90px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td style="width: 100px;">
                <dx:ASPxButton ID="ASPxButton1" Text="生成计划" Width="90px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        popup.Show();
                        txtSeries.SetEnabled(false);
                        chkTflag.SetEnabled(false);
                        ComoBoxSO.PerformCallback();
                    }" />
                </dx:ASPxButton>
            </td>
            <td style="width: 120px;">

            </td>
            <td style="width: 120px;">

            </td>
            <td colspan="3" style="width: auto">
            </td>
        </tr>
    </table>
                <dx:ASPxPopupControl ID="popup11" ClientInstanceName="popup11" runat="server" AllowDragging="true"
                PopupHorizontalAlign="OutsideRight" HeaderText="备注">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <dx:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server"
                            Width="320px" Height="120px"  RenderMode="Table" 
                            OnCallback="callbackPanel_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent11" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxMemo ID="litText" runat="server" Text="" Width="100%" Border-BorderStyle="None" ReadOnly="true" Height="120px">
                                                </dx:ASPxMemo>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ClientSideEvents Shown="popup_Shown" />
            </dx:ASPxPopupControl>
                                                <dx:ASPxPopupControl ID="popup12" ClientInstanceName="popup12" runat="server" AllowDragging="true"
                PopupHorizontalAlign="OutsideRight" HeaderText="流水号">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel7" ClientInstanceName="callbackPanel1" runat="server"
                            Width="320px" Height="120px"  RenderMode="Table" 
                            OnCallback="callbackPanel1_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent7" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxMemo ID="ASPxMemo1" runat="server" Text="" Width="100%" Border-BorderStyle="None" ReadOnly="true" Height="120px">
                                                </dx:ASPxMemo>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ClientSideEvents Shown="popup_Shown1" />
            </dx:ASPxPopupControl>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID" SettingsPager-Mode="ShowAllRecords"
        AutoGenerateColumns="False"  
        OnRowValidating="ASPxGridView1_RowValidating" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
        OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize"
        OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize" 
        onhtmlrowcreated="ASPxGridView1_HtmlRowCreated" 
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" 
        onstartrowediting="ASPxGridView1_StartRowEditing">
        <Settings ShowHorizontalScrollBar="true" />
        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ExportMode="All" />
<%--        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>--%>
        <SettingsEditing PopupEditFormWidth="600px" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                Width="60px">
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                        Style="margin-bottom: 0px" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="调序" Width="80px" ButtonType="Image">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Up">
                        <Image Url="../../Pub/Images/Up.png" Width="15px" ToolTip="计划上调" />
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Down">
                        <Image Url="../../Pub/Images/Down.png" Width="15px" ToolTip="计划下调">
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="操作" Width="90px" ButtonType="Button">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Confirm" Text="确认计划">
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Cancel" Text="取消确认">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="修改" Width="80px">
                <EditButton Visible="True" Text="修改">
                </EditButton>
                <DeleteButton Visible="True" Text="删除">
                </DeleteButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
            <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" Width="140px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AutoFilterCondition="Contains" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="执行序" FieldName="PLAN_SEQ" Width="60px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" Width="60px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="产品系列" FieldName="PRODUCT_SERIES" Width="100px" Visible="false"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="机型" FieldName="PRODUCT_MODEL" Width="80px"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划数" FieldName="PLAN_QTY" Width="60px" CellStyle-HorizontalAlign="Right"
                Settings-AllowHeaderFilter="True">
                                                                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick1(this, '<%# Container.KeyValue %>')">
                        <%#Eval("PLAN_QTY").ToString()%></a>
                </DataItemTemplate>
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="上线数" FieldName="ONLINE_QTY" Width="70px" CellStyle-HorizontalAlign="Right"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="下线数" FieldName="OFFLINE_QTY" Width="70px" CellStyle-HorizontalAlign="Right"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" Width="100px" Settings-AllowHeaderFilter="True"  Visible="false">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" Width="100px" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="开始日期" FieldName="BEGIN_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
           <dx:GridViewDataDateColumn Caption="结束日期" FieldName="END_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="入库日期" FieldName="ACCOUNT_DATE" Width="75px" CellStyle-Wrap="False"  Visible="false"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="制定时间" FieldName="CREATE_TIME" Width="150px" CellStyle-Wrap="False"  PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
           <dx:GridViewDataTextColumn Caption="客户" FieldName="CUSTOMER_NAME" Width="80px"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划员" FieldName="CREATE_USERNAME" Width="80px"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="编制时间" FieldName="CREATE_TIME" Width="130px" CellStyle-Wrap="False"  Visible="false"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工艺地点" FieldName="ROUNTING_SITE" Width="70px"
                CellStyle-Wrap="False" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <%--            <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" Width="200px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn Caption="备注" Width="100px">
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                        <%#ConvertFormat(Eval("REMARK").ToString())%></a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="柳汽标识" FieldName="LQ_FLAG" Width="200px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="" FieldName="" Width="50px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="流水号标识" FieldName="SN_FLAG" Width="200px" CellStyle-Wrap="False"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="确认标识" FieldName="CONFIRM_FLAG" Width="200px"
                CellStyle-Wrap="False" Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="BOM标识" FieldName="BOM_FLAG" Width="200px" CellStyle-Wrap="False"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="发料标识" FieldName="ITEM_FLAG" Width="200px" CellStyle-Wrap="False"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="REMARK1" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CONFIRM_FLAG" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RUN_FLAG" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ROUNTING_CODE" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " Width="80%" />
        </Columns>

         <Settings ShowFooter="True" />
         <TotalSummary>
         <dx:ASPxSummaryItem FieldName="PLAN_CODE" SummaryType="Count" DisplayFormat="计划数={0}"/>
            <dx:ASPxSummaryItem FieldName="PLAN_QTY" SummaryType="Sum" DisplayFormat="总数={0}"/>
            <dx:ASPxSummaryItem FieldName="ONLINE_QTY" SummaryType="Sum" DisplayFormat="上线数={0}"/>
            <dx:ASPxSummaryItem FieldName="OFFLINE_QTY" SummaryType="Sum" DisplayFormat="下线数={0}"/>
        </TotalSummary>
        <Templates>
            <EditForm>
                <table>
                    <tr style="height: 10px">
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                                                <td>
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="计划序">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtPlanSeq" runat="server" Width="160px" Text='<%# Bind("PLAN_SEQ") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="执行序号有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^[0-9]{1,100}$" />
                                    <RequiredField IsRequired="True" ErrorText="执行序号不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 5px">
                        </td>

                                                <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="计划代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                           <dx:ASPxTextBox ID="txtRmesid" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>' Visible="false"
                                Enabled="false" />
                            <dx:ASPxTextBox ID="txtPlanCode" runat="server" Width="160px" Text='<%# Bind("PLAN_CODE") %>'
                                Enabled="false" />
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                                        <dx:ASPxComboBox ID="SoCombo" runat="server" EnableClientSideAPI="True" 
                                            Value='<%# Bind("PLAN_SO") %>' ValueType="System.String" DataSourceID="SqlDataSource3"
                                            Width="150px" TextField="ZCDM" ValueField="ZCDM" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {initEditSeries(s,e); }" />
                                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                                <RequiredField IsRequired="True" ErrorText="SO不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="机型">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="productSeries" ClientInstanceName="EditSeries" runat="server"
                                Width="160px" Text='<%# Bind("PRODUCT_MODEL") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="产品系列有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="产品系列不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="计划开始日期" />
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="ASPxDateBegin" runat="server" Width="100%" Value='<%# Bind("BEGIN_DATE") %>'
                                EditFormatString="yyyy-MM-dd" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="日期有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="日期不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="计划结束日期" />
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxDateEdit ID="ASPxDateEnd" runat="server" Width="100%" Value='<%# Bind("END_DATE") %>'
                                 EditFormatString="yyyy-MM-dd" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="日期有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="日期不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="计划数量">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxplanqty" runat="server" Width="160px" Text='<%# Bind("PLAN_QTY") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="计划数量有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^[0-9]{1,100}$" />
                                    <RequiredField IsRequired="True" ErrorText="计划数量不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="客户名称">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="ASPxCustomer" runat="server" Width="160px" Text='<%# Bind("CUSTOMER_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                               <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="客户名称有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="客户名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="生产线"  AssociatedControlID="PlineCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="PlineCombo" runat="server"  EnableClientSideAPI="True" Enabled="false"  Value='<%# Bind("PLINE_CODE") %>' ClientInstanceName="plineedit1"
                                DataSourceID="SqlDataSource1" ValueType="System.String" Width="150px" 
                                TextField="pline_name" ValueField="PLINE_CODE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {OnPlineChanged(s); }"
                                   />
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="工艺地点"  AssociatedControlID="RountingsiteCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                          <dx:ASPxComboBox ID="RountingsiteCombo" runat="server" EnableClientSideAPI="True" Enabled="false" Value='<%# Bind("ROUNTING_SITE") %>'
                                DataSourceID="SqlDataSource1" ValueType="System.String" Width="150px" 
                                TextField="pline_name" ValueField="PLINE_CODE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {OnPlineChanged(s); }" />
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="工艺地点不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="跨线模式"  AssociatedControlID="ASPxComboBoxAcross1">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="ASPxComboBoxAcross1" runat="server"  EnableClientSideAPI="True" Enabled="false"  Value='<%# Bind("ROUNTING_CODE") %>'
                                DataSourceID="SqlDataSource2" ValueType="System.String" Width="150px" 
                                TextField="ALINE_NAME" ValueField="ALINE_CODE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <%--<ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="跨线模式不能为空！" />
                                </ValidationSettings>--%>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 180px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 50px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="详细备注信息">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="4">
                            <dx:ASPxMemo ID="txtRemark" runat="server" Width="100%" Height="50px" Text='<%# Bind("REMARK1") %>' ClientInstanceName="txtRemark1"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="备注有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                </ValidationSettings>
                            </dx:ASPxMemo>
                            <td>
                            </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td colspan="4">
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="说明：修改后计划数量不能大于原计划数量！" ForeColor="Red">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td colspan="7">
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
        <SettingsBehavior ColumnResizeMode="Control" />
        <ClientSideEvents CustomButtonClick="function (s,e){
        changeSeq(s,e);
    }" />
    </dx:ASPxGridView>
    <table><tr>
    <td><dx:ASPxLabel runat="server" ID="ASPxLabel18" runat="server" ForeColor="LightPink" Text="预留号计划 |"></dx:ASPxLabel>&nbsp&nbsp</td>
    <td><dx:ASPxLabel runat="server" ID="ASPxLabel1" runat="server" ForeColor="Green" Text="计划员确认 |"></dx:ASPxLabel>&nbsp&nbsp</td>
    <td><dx:ASPxLabel runat="server" ID="ASPxLabel41" runat="server" ForeColor="Yellow" Text="流水号生成 |"></dx:ASPxLabel>&nbsp&nbsp</td>
    <td><dx:ASPxLabel runat="server" ID="ASPxLabel61" runat="server" ForeColor="LightSkyBlue" Text="BOM转换 |"></dx:ASPxLabel>&nbsp&nbsp</td>
    <td><dx:ASPxLabel runat="server" ID="ASPxLabel81" runat="server" ForeColor="Red" Text="库房确认 |"></dx:ASPxLabel>&nbsp&nbsp</td>
<%--    <td style=" color:Green"> 计划员确认</td>&nbsp&nbsp<td  style=" color:Yellow"> 流水号生成</td>&nbsp&nbsp
    <td  style=" color:Blue"> BOM转换</td>&nbsp&nbsp
    <td  style=" color:Red"> 库房确认</td>&nbsp&nbsp--%>
    </tr>
    </table>

    <dx:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
        ShowHeader="True" HeaderText="生成计划" AllowDragging="true" Width="80%">
        <ContentCollection>
            <dx:PopupControlContentControl Width="100%">
                <table align="center" style="width: 800px; height: 200px;">
                    <tr>
                        <td style="width: 5px">
                        </td>
                                                <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划序</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxSeq" ClientInstanceName="txtSeq" runat="server" Width="120px" />
                        </td>
                        
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划代码</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextPlanCode" ClientInstanceName="txtPlanCode" runat="server"
                                Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                SO</label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ComoBoxSO" runat="server" ClientInstanceName="ComoBoxSO" Width="120px" OnCallback="ComoBoxSO_Callback">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    chkTflag.SetEnabled(false);
                                    initSeries(s,e);
                                }"  />
<ClientSideEvents SelectedIndexChanged="function(s,e){
                                    chkTflag.SetEnabled(false);
                                    initSeries(s,e);
                                }"></ClientSideEvents>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                机型</label>
                        </td>
                        <td>
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" ClientInstanceName="Series"
                                OnCallback="ASPxCallbackPanel1_Callback">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent2" runat="server">
                                        <dx:ASPxTextBox ID="ASPxTextSeries" ClientInstanceName="txtSeries" runat="server" ClientEnabled="false"
                                            Width="120px" />
                                        <dx:ASPxCheckBox ID="TransFlag" runat="server" ClientInstanceName="chkTflag" ClientEnabled="false" Text="老虎缸盖">
                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            initSeries1(s,e);
                                        }" />
<ClientSideEvents CheckedChanged="function(s,e){
                                            initSeries1(s,e);
                                        }"></ClientSideEvents>
                                        </dx:ASPxCheckBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划数量</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxQty" ClientInstanceName="txtQty" runat="server" Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                客户名称</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxCustomerName" ClientInstanceName="txtCustomerName"
                                runat="server" Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left;">
                            <label style="font-size: small">
                                开始日期</label>
                        </td>
                        <td>
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel2" ClientInstanceName="dateBeginPanel"
                                OnCallback="ASPxCallbackPanel2_Callback">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent1" runat="server">
                                        <dx:ASPxDateEdit ID="ASPxBeginDate" ClientInstanceName="dateBegin" runat="server"
                                            EditFormatString="yyyy-MM-dd" Width="120px">
                                        </dx:ASPxDateEdit>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                完成日期</label>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel3" ClientInstanceName="dateEndPanel"
                                OnCallback="ASPxCallbackPanel3_Callback">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent3" runat="server">
                                        <dx:ASPxDateEdit ID="ASPxEndDate" ClientInstanceName="dateEnd" runat="server" EditFormatString="yyyy-MM-dd"
                                            Width="120px">
                                        </dx:ASPxDateEdit>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                产线</label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ASPxComboBoxPline" ClientInstanceName="listPline" runat="server" DropDownStyle="DropDownList"
                                Width="120px" AutoPostBack="false">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    ProcessPanel.PerformCallback();
                                    ComoBoxSO.PerformCallback();
                                }"></ClientSideEvents>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                工艺地点</label>
                        </td>
                        <td>
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel4" ClientInstanceName="ProcessPanel"
                                OnCallback="ASPxCallbackPanel4_Callback">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent4" runat="server">
                                        <dx:ASPxComboBox ID="ASPxComboBoxProcess" ClientInstanceName="listProcess" runat="server"  ClientEnabled="false" DropDownStyle="DropDownList"
                                            Width="120px">
                                        </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left;">
                            <label style="font-size: small">
                                备注信息</label>
                        </td>
                        <td colspan="4">
                            <%--<dx:ASPxTextBox ID="ASPxTextBoxRemark" ClientInstanceName="txtRemark" runat="server"
                                Width="120px" />--%>
                            <dx:ASPxMemo ID="aspxmemoRemark" runat="server" Text="" Width="100%" Border-BorderStyle="None" ClientInstanceName="txtRemark"
                                Height="100px">
                                <Border BorderStyle="Solid"></Border>
                            </dx:ASPxMemo>
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                跨线模式</label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ASPxComboBoxAcross" ClientInstanceName="listAcross" runat="server" DropDownStyle="DropDownList"
                                Width="120px" AutoPostBack="false">
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                        </td>
                        <td>
                            
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left;">
                        </td>
                        <td colspan="4">
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td colspan="5" align="center">
                            <dx:ASPxButton ID="UpdateButton" ClientInstanceName="butSubmit" ReplacementType="EditFormUpdateButton"
                                Text="提交" AutoPostBack="false" runat="server" Width="80px">
                                <ClientSideEvents Click="function(s,e) { checkSubmit(); }" />
<ClientSideEvents Click="function(s,e) { checkSubmit(); }"></ClientSideEvents>
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td colspan="5" align="center">
                            <dx:ASPxButton ID="CancelButton" ReplacementType="EditFormCancelButton" Text="取消"
                                AutoPostBack="false" runat="server" Width="80px">
                                <ClientSideEvents Click="function(s,e) { popup.Hide(); }" />
<ClientSideEvents Click="function(s,e) { popup.Hide(); }"></ClientSideEvents>
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                    OnCallback="ASPxCbSubmit_Callback">
                    <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
<ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }"></ClientSideEvents>
                </dx:ASPxCallback>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
    ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
    ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
    ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>

</asp:Content>