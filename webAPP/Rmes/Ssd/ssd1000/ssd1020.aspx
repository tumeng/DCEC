<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ssd1020.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd1020" %>

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
            var plancode11=txtPlanCode.GetValue();
            if (plancode11.length <8) {
                alert("计划代码格式不规范！");
                butSubmit.SetEnabled(true);
                return;
            }
//            if (txtSeq.GetValue() == null) {
//                alert("请输入执行序号！");
//                butSubmit.SetEnabled(true);
//                return;
//            }
//            if (isNaN(txtSeq.GetValue())) {
//                alert("执行序号必须为数字！");
//                butSubmit.SetEnabled(true);
//                return;
//            }
            if (txtSO.GetValue() == null) {
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
                alert("请选工艺路线！");
                butSubmit.SetEnabled(true);
                return;
            }

            if (txtCustomerName.GetValue() == null) {
                alert("请输入客户名称！");
                butSubmit.SetEnabled(true);
                return;
            }
            if (cmbPtype.GetValue() == null) {
                alert("请选择计划类型！");
                butSubmit.SetEnabled(true);
                return;
            } 
            //返修限定数量一台
            if (cmbPtype.GetValue() == "D") {
                if (txtQty.GetValue() != "1") {
                    alert("返修计划数量限定一台！");
                    butSubmit.SetEnabled(true);
                    return;
                }
            }
            //改制必须转BOM
            if (cmbPtype.GetValue() == "C") {
                if (chkTflag.GetValue() != true) {
                    alert("改制计划必须转BOM！");
                    butSubmit.SetEnabled(true);
                    return;
                }
            }

            //            var beginDate = dateBegin.GetDate().toLocaleString();
            //            var pline = listPline.GetValue().toString();
            //            var series = listSeries.GetValue().toString();
            //            var bom = listBomIndex.GetValue().toString();
            //            var tcm = listTcmIndex.GetValue().toString();
            //            var project = listProject.GetValue().toString();
            //            var qty = txtQty.GetValue().toString();
            //            var shift = listShift.GetValue().toString();
            //            var seq = txtSeq.GetValue().toString();
            //            var flag = chkFlag.GetValue().toString();

            //            var remark = "";
            //            if (txtRemark.GetValue() != null) {
            //                remark = txtRemark.GetValue().toString();
            //            }

            //            var item = "";
            //            if (txtItem.GetValue() != null) {
            //                item = txtItem.GetValue().toString();
            //            }

            //            ref = beginDate + "," + pline + "," + series + "," + bom + "," + project + "," + qty + "," + shift + "," + seq + "," + remark + "," + item + "," + tcm + "," + flag;
            CallbackSubmit.PerformCallback(ref);
        }
        function checkYlh() {
            var ref = "";

            if (YLH.GetValue() == null) {
                alert("请输入预留号！");
                return;
            }
            var ylh1 = YLH.GetValue();

            if (ylh1.length != 8) {
                alert('预留号位数不合法！');
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

        function initSeries(s, e) {
            //            Series.PerformCallback(txtSO.GetValue());
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
                txtSO.SetFocus();
                txtRemark.SetValue("");
                txtSeries.SetValue("");
                return;
            }
            txtRemark.SetValue(retStr1);
            txtSeries.SetValue(result1);
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
                txtSO.SetFocus();
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
                var filedNames = "PLINE_CODE;PLAN_SO;PRODUCT_MODEL;PLAN_CODE;PLAN_QTY;REMARK1;PLAN_TYPE1;IS_BOM";
                grid.GetSelectedFieldValues(filedNames, GetValues);
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
            Panel1_PlanType.SetValue(values[0][6]);
            Panel1_BOM.SetValue(values[0][7]);
            Panel1_SO.SetEnabled(false);
            Panel1_Series.SetEnabled(false);
            Panel1_PlanCode.SetEnabled(false);
            Panel1_Qty.SetEnabled(false);
            Panel1_Remark.SetEnabled(false);
            Panel1_PlanType.SetEnabled(false);
            Panel1_BOM.SetEnabled(false);
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
//            ListBoxPanel6.PerformCallback(ref);
        }
        function Import1() {
            var File11 = document.getElementById('ctl00_ContentPlaceHolder1_ASPxPopupControl2_ASPxCallbackPanel5_File2'); 
            var ref = File11.value;
            ListBoxPanel.PerformCallback('import|' + ref);
        }
        function Work(workflag) {
            //分配改制返修流水号
            if (workflag == 'add') {
                var plantype1 = Panel1_PlanType.GetValue();
                var plancode1 = Panel1_PlanCode.GetValue();
                var isbom1 = Panel1_BOM.GetValue();
                var so1 = Panel1_SO.GetValue();
                var items = YLH.GetValue();
                if (items == null) {
                    alert('请输入发动机流水号！');
                    return;
                };
                var ids = "";
                var _ids = "";
                var yqty = labelqty.GetValue();
                var zqty = Panel1_Qty.GetValue();
                if (1 > (zqty - yqty)) {
                    alert('分配数量超过计划总数！');
                    return;
                }
                var ref = items;
                ListBoxPanel.PerformCallback(workflag + '|' + ref);
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
                    ids = ids + items[i].text + ",";
                }
                if (ids.endWith(','))
                    _ids = ids.substring(0, ids.length - 1);
                ListBoxPanel.PerformCallback(workflag + '|' + _ids);
            }
        }
        function changebom(s, e) {
            if (cmbPtype.GetValue() == "C") {
                chkTflag.SetValue(true);
                chkTflag.SetEnabled(false);
            }
            else {
                chkTflag.SetValue(false);
                chkTflag.SetEnabled(true);
            }
        }
        function changebom1(s, e) {
            if (EditType.GetValue() == "C") {
                EditBom.SetValue(true);
                EditBom.SetEnabled(false);
            }
            else {
                EditBom.SetValue(false);
                EditBom.SetEnabled(true);
            }
        }
        function initshow(s, e) {
            if (cmbPtype.GetValue() == "C") {
                chkTflag.SetValue(true);
                chkTflag.SetEnabled(false);
            }
            else {
                chkTflag.SetValue(false);
                chkTflag.SetEnabled(true);
            }
            popup.Show(); 
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
                            <asp:Label ID="Label1" runat="server" Text="打开ExceL文件导入计划"></asp:Label>
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
                            <a href="../../File/改制计划模板.xls">计划模板</a>
                        </td>
                        <td style="text-align: right;">
                            <a href="../../File/改制计划流水号模板.xls">计划流水号模板</a>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                </table>


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
                      initshow(s,e);

                    }" />
                </dx:ASPxButton>
            </td>
            <td style="width: 120px;">
                <dx:ASPxButton ID="ASPxButton2" Text="手动生成流水号" Width="150px" AutoPostBack="false"
                    runat="server">
                    <ClientSideEvents Click="function(s,e){
                       ShowPopup1();
                    }" />
                </dx:ASPxButton>
            </td>
            <td style="width: 100px">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="批量导入计划流水号:" Width="100px">
                </dx:ASPxLabel>
            </td>
            <td style="width: 300px">
                <input id="File2" type="file" accept=".txt" size="20" style="font-size: medium; height: 25px;"
                    alt="请选择EXCEL文件" runat="server" />
            </td>
            <td style="width: 200px">
                <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="true" Text="导入"  OnClick="ASPxButton3_Import_Click" Width="100px">
                </dx:ASPxButton>
            </td>
            <td colspan="3" style="width: auto">
            </td>
        </tr>
    </table>
                </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
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
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared">
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
            <dx:GridViewDataTextColumn Caption="机型" FieldName="PRODUCT_MODEL" Width="100px"
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
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" Width="80px" Settings-AllowHeaderFilter="True">
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
                                    <dx:GridViewDataTextColumn Caption="计划类型" FieldName="PLAN_TYPE1" Width="80px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AutoFilterCondition="Contains" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="柳汽标识" FieldName="LQ_FLAG" Width="200px" CellStyle-Wrap="False" Visible="false"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="是否转BOM" FieldName="IS_BOM" Width="100px" CellStyle-Wrap="False"
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

             <dx:GridViewDataTextColumn Caption="计划类型" FieldName="PLAN_TYPE" Width="80px" CellStyle-HorizontalAlign="Center" Visible="false"
                Settings-AllowHeaderFilter="True">
                <Settings AutoFilterCondition="Contains" />
                <CellStyle HorizontalAlign="Center">
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
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="执行序号">
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
                            <dx:ASPxTextBox ID="txtPlanSO" ClientInstanceName="EditSO" runat="server" Width="160px"
                                Text='<%# Bind("PLAN_SO") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="产品系列有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="SO不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxTextBox>
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
                            <dx:ASPxComboBox ID="PlineCombo" runat="server"  EnableClientSideAPI="True" Enabled="false"  Value='<%# Bind("PLINE_CODE") %>' DropDownStyle="DropDownList"
                                DataSourceID="SqlDataSource1" ValueType="System.String" Width="150px" 
                                TextField="pline_name" ValueField="PLINE_CODE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {OnPlineChanged(s); }" />
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
                            <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="计划类型"  AssociatedControlID="ASPxComboBox1">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server"  ClientInstanceName="EditType" EnableClientSideAPI="True" Value='<%# Bind("PLAN_TYPE") %>'
                                DataSourceID="SqlDataSource3" ValueType="System.String" Width="150px"  ClientEnabled="false"
                                TextField="PLAN_NAME" ValueField="PLAN_TYPE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="计划类型不能为空！" />
                                </ValidationSettings>
                                               <ClientSideEvents  SelectedIndexChanged="function(s,e){
                                                    changebom1(s,e);
                                                }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="是否转BOM"   AssociatedControlID="ASPxComboBox2">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                          <dx:ASPxCheckBox ID="ASPxComboBox2" runat="server" ClientInstanceName="EditBom" Checked='<%#Eval("IS_BOM").ToString()=="Y"%>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                        </td>
                    </tr>
<%--                    <tr style="height: 30px">
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
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="跨线模式不能为空！" />
                                </ValidationSettings>
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
                    </tr>--%>
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
        <ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
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
                            <dx:ASPxTextBox ID="ASPxTextBoxSO" ClientInstanceName="txtSO" runat="server" Width="120px">
                                <ClientSideEvents TextChanged="function(s,e){
                    initSeries(s,e);
                }" />
                            </dx:ASPxTextBox>
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
                                        <dx:ASPxTextBox ID="ASPxTextSeries" ClientInstanceName="txtSeries" runat="server"
                                            Width="120px" />
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
<%--                            <label style="font-size: small ">
                                计划序</label>--%>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxSeq" ClientInstanceName="txtSeq" runat="server" Width="120px" ClientVisible="false" />
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
                                }" />
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
                                        <dx:ASPxComboBox ID="ASPxComboBoxProcess" ClientInstanceName="listProcess" runat="server" DropDownStyle="DropDownList"
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
                                计划类型</label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ASPxComboBoxPlanType" ClientInstanceName="cmbPtype" runat="server"
                                Width="120px" AutoPostBack="false">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    changebom(s,e);
                                }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                需要转BOM</label>
                        </td>
                        <td>
                            
                            <dx:ASPxCheckBox ID="TransFlag" runat="server" Text="" ClientInstanceName="chkTflag"></dx:ASPxCheckBox>
                            
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
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td colspan="5" align="center">
                            <dx:ASPxButton ID="CancelButton" ReplacementType="EditFormCancelButton" Text="取消"
                                AutoPostBack="false" runat="server" Width="80px">
                                <ClientSideEvents Click="function(s,e) { popup.Hide(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                    OnCallback="ASPxCbSubmit_Callback">
                    <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                </dx:ASPxCallback>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="ASPxPopupControl2" ClientInstanceName="popup1" runat="server"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
        ShowHeader="True" HeaderText="流水号分配" AllowDragging="true" Width="80%">
        <ClientSideEvents CloseUp="function(s, e) { ClosePop(e.result); }" />
        <ContentCollection>
            <dx:PopupControlContentControl Width="100%">
                <table align="center" style="width: 800px;">
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                生产线</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="Panel1_Pline" runat="server" ClientEnabled="false"
                                 Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                SO</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox2" ClientInstanceName="Panel1_SO" runat="server" Width="120px" ClientEnabled="false"
                                 />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                机型</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox3" ClientInstanceName="Panel1_Series" runat="server" ClientEnabled="false"
                                Width="120px" >
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划号</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox5" ClientInstanceName="Panel1_PlanCode" runat="server" ClientEnabled="false"
                                Width="120px"  />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                数量</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox6" ClientInstanceName="Panel1_Qty" runat="server" ClientEnabled="false"
                                Width="120px"  />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left;">
                            <label style="font-size: small">
                                备注</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox4" ClientInstanceName="Panel1_Remark" runat="server" ClientEnabled="false"
                                Width="120px" />
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                                        <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划类型</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox7" ClientInstanceName="Panel1_PlanType" runat="server" ClientEnabled="false"
                                Width="120px"  />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                是否转BOM</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox8" ClientInstanceName="Panel1_BOM" runat="server" ClientEnabled="false"
                                Width="120px"  />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left;">

                        </td>
                        <td>

                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                </table>
                <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel5" ClientInstanceName="ListBoxPanel"
                    OnCallback="ASPxCallbackPanel5_Callback">
                <ClientSideEvents       
                                 BeginCallback="function(s, e) 
                                {
	                                ListBoxPanel.cpCallbackName = '';
                                }"  
                                EndCallback="function(s, e) 
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
                                   
                                }" />
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent5" runat="server">
                            <table align="center" style="width: 800px; height: 200px;" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 5px;">
                                                </td>
                                                <td>
                                                    <%--<table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="未选" Width="30px">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 180px">
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="输入流水号" Width="80px">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td>
                                                    <dx:ASPxTextBox ID="YLH" runat="server" Width="120px" ClientInstanceName="YLH">
                                                        <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="请输入一个有效的8位数字！"
                                                            ErrorDisplayMode="ImageWithTooltip">
                                                            <RegularExpression ErrorText="请输入一个有效的8位数字！" ValidationExpression="^\d{8}$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td height="200px">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="ASPxBT1" runat="server" Text=">>" Border-BorderStyle="None" AutoPostBack="false"
                                                                    ToolTip="分配" Width="30px">
                                                                    <ClientSideEvents Click="function (s,e){
                                                                        Work('add');
                                                                        
                                                                    }" />
                                                                    <Border BorderStyle="None"></Border>
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="ASPxBT2" runat="server" Text="<<" Border-BorderStyle="None" AutoPostBack="false"
                                                                    ToolTip="删除">
                                                                    <ClientSideEvents Click="function (s,e){
                                               Work('del');
                                            }" />
                                                                    <Border BorderStyle="None"></Border>
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="已选" Width="30px">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 180px">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                                <td height="200px">
                                                    <dx:ASPxListBox ID="ASPxListBoxUsed" runat="server" ClientInstanceName="ListBoxUsed"
                                                        SelectionMode="CheckColumn" Width="210px" Height="200px" ValueField="RMES_ID"
                                                        ValueType="System.String" OnCallback="ASPxListBoxUsed_Callback">
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="SN" Caption="流水号" Width="100%" />
                                                        </Columns>
                                                    </dx:ASPxListBox>
                                                </td>
                                                <td style="width: 10px">
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel181" runat="server" Text="已分配数量:" Width="80px">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 180px;">
                                                                <dx:ASPxLabel ID="lblYqty" runat="server" ClientInstanceName="labelqty" ForeColor="Red">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 100px">
                                    </td>
                                </tr>
                            </table>
                            <table align="center" style="width: 800px;">
                                <tr>
<%--                                    <td style="width: 100px">
                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="批量导入流水号:" Width="100px">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 300px">
                                        <input id="File2" type="file" accept=".txt" size="20" style="font-size: medium; height: 25px;"
                                            alt="请选择文本文件" runat="server" />
                                    </td>
                                    <td style="width: 200px">
                                        <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="false" Text="导入" Width="100px" 
                                            >
                                            <ClientSideEvents Click="function (s,e){  
                                             Import1();
                                                                    }" />
                                        </dx:ASPxButton>
                                    </td>--%>
                                    <td>

                                    </td>
                                                                        <td>

                                    </td>
                                                                        <td>

                                    </td>
                                                                        <td>

                                    </td>
<%--                                    <td style="width: 5px">
                                    </td>
                                    <td>

                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>

                                    </td>
                                    <td style="width: 100px">
                                    </td>--%>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
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

