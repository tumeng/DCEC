<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="ssd1010.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd1010" %>

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
            ListBoxUsed.SelectAll();
            var items = ListBoxUsed.GetSelectedItems();
            if (items.length == 0) {
                alert('请选择一个流水号！');
                butSubmit.SetEnabled(true);
                return;
            };
            var ids = "";
            var _ids = "";
            for (var i = 0; i < items.length; i++) {
                ids = ids + items[i].value + ",";
            }
            if (ids.endWith(','))
                _ids = ids.substring(0, ids.length - 1);
            var ids1 = "";
            var _ids1 = "";
            for (var i = 0; i < items.length; i++) {
                ids1 = ids1 + items[i].text + ",";
            }
            if (ids1.endWith(','))
                _ids1 = ids1.substring(0, ids1.length - 1);
            ref = 'Commit' + '|' + _ids + '|' + _ids1;
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
                    ListBoxUsed.ClearItems();
                    labelqty.SetValue('0');
                    initGridview();
                    return;
                case "Fail":
                    alert(retStr);
                    //                    ListBoxUsed.ClearItems();
                    //                    labelqty.SetValue('0');
                    ListBoxUsed.UnselectAll();
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

        //        function initSeries(s, e) {
        //            //            Series.PerformCallback(txtSO.GetValue());
        //            var webFileUrl = "?SO=" + s.GetValue() + "&opFlag=getEditSeries";

        //            var result = "";
        //            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
        //            xmlHttp.open("Post", webFileUrl, false);
        //            xmlHttp.send("");

        //            result = xmlHttp.responseText;
        //            var result1 = "";
        //            var retStr1 = "";
        //            var array1 = result.split(',');
        //            retStr1 = array1[1];
        //            result1 = array1[0];
        //            if (result1 == "") {
        //                alert("机型不存在，请检查数据！");
        //                txtSO.SetFocus();
        //                txtRemark.SetValue("");
        //                txtSeries.SetValue("");
        //                return;
        //            }
        //            txtRemark.SetValue(retStr1);
        //            txtSeries.SetValue(result1);
        //        }

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
            var index1 = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index1, GetDataCallback);
            //        if (buttonID == "Up") {
            //            grid.GetValuesOnCustomCallback("up", GetDataCallback);
            //        }
            //        else if (buttonID == "Down") {
            //            grid.GetSelectedFieldValues("Down", GetDataCallback);
            //        }
        }

        function ShowPopup1() {
            labelqty.SetValue('0');
            popup.Show();
            ListBoxPanel.PerformCallback('js' + '|' + '');
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
        function initListBoxUsed(s, e) {


        }
        function Work(workflag) {
            if (workflag == 'add') {
                var count1 = grid12.GetSelectedRowCount();
                if (count1 < 1) {
                    alert('请选择流水号！');
                    return;
                };
                grid12.PerformCallback(workflag + '|' + '1' + '|' + '2');
                //                var filedNames = "RMES_ID;GHTM";
                //                grid12.GetSelectedFieldValues(filedNames, GetValues);

                //                var ids = "";
                //                var _ids = "";
                //                for (var i = 0; i < items.length; i++) {
                //                    ids = ids + items[i].value + ",";
                //                }
                //                if (ids.endWith(','))
                //                    _ids = ids.substring(0, ids.length - 1);

                //                var ids1 = "";
                //                var _ids1 = "";
                //                for (var i = 0; i < items.length; i++) {
                //                    ids1 = ids1 + items[i].text + ",";
                //                }
                //                if (ids1.endWith(','))
                //                    _ids1 = ids1.substring(0, ids1.length - 1);
                //                ListBoxPanel.PerformCallback(workflag + '|' + _ids + '|' + _ids1);
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

                var ids1 = "";
                var _ids1 = "";
                for (var i = 0; i < items.length; i++) {
                    ids1 = ids1 + items[i].text + ",";
                }
                if (ids1.endWith(','))
                    _ids1 = ids1.substring(0, ids1.length - 1);
                ListBoxPanel.PerformCallback(workflag + '|' + _ids + '|' + _ids1);
            }

        }
        function GetValues(values) {
            ListBoxPanel.PerformCallback('add' + '|' + values[0][3]);
        }

        //        function AddSelectedItems() {
        //            MoveSelectedItems(ListBoxUnused, ListBoxUsed,'A');
        ////            UpdateButtonState();
        //        }
        //        function RemoveSelectedItems() {
        //            MoveSelectedItems(ListBoxUsed, ListBoxUnused,'D');
        ////            UpdateButtonState();
        //        }
        //        function MoveSelectedItems(srcListBox, dstListBox,type1) {
        //            srcListBox.BeginUpdate();
        //            dstListBox.BeginUpdate();
        //            var items = srcListBox.GetSelectedItems();
        //            for (var i = items.length - 1; i >= 0; i = i - 1) {
        //                dstListBox.AddItem(items[i].text, items[i].value);
        //                srcListBox.RemoveItem(items[i].index);
        //            }
        //            if (type1 == 'A') {
        //                labelqty.SetValue(dstListBox.GetItemCount());
        //            }
        //            else if (type1 == 'D') {
        //                labelqty.SetValue(srcListBox.GetItemCount());
        //            } else {
        //                labelqty.SetValue('0');
        //            }
        //            
        //            srcListBox.EndUpdate();
        //            dstListBox.EndUpdate();
        //        }

    </script>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel6" ClientInstanceName="ListBoxPanel6">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent6" runat="server">
                <table style="background-color: #99bbbb; width: 100%">
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出-EXCEL" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />
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
                    <%--<ClientSideEvents Click="function(s,e){
                        popup.Show();
                        
                    }" />--%>
                    <ClientSideEvents Click="function(s,e){
                       ShowPopup1();
                    }" />
                </dx:ASPxButton>
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
                    Width="320px" Height="120px" RenderMode="Table" OnCallback="callbackPanel_Callback">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent11" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxMemo ID="litText" runat="server" Text="" Width="100%" Border-BorderStyle="None"
                                            ReadOnly="true" Height="120px">
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
                <dx:ASPxCallbackPanel ID="ASPxCallbackPanel7" ClientInstanceName="callbackPanel1"
                    runat="server" Width="320px" Height="120px" RenderMode="Table" OnCallback="callbackPanel1_Callback">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent7" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxMemo ID="ASPxMemo1" runat="server" Text="" Width="100%" Border-BorderStyle="None"
                                            ReadOnly="true" Height="120px">
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
        AutoGenerateColumns="False" OnRowValidating="ASPxGridView1_RowValidating" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
        OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize"
        OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize" OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared">
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
            <dx:GridViewDataTextColumn Caption="产品系列" FieldName="PRODUCT_SERIES" Width="100px"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="机型" FieldName="PRODUCT_MODEL" Width="80px" Settings-AllowHeaderFilter="True">
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
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" Width="100px" Settings-AllowHeaderFilter="True"
                Visible="false">
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
            <dx:GridViewDataDateColumn Caption="制定时间" FieldName="CREATE_TIME" Width="150px" CellStyle-Wrap="False"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="客户" FieldName="CUSTOMER_NAME" Width="80px" Settings-AllowHeaderFilter="True">
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
                Visible="false" Settings-AllowHeaderFilter="True">
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
            <dx:ASPxSummaryItem FieldName="PLAN_QTY" SummaryType="Sum" DisplayFormat="总数={0}" />
            <dx:ASPxSummaryItem FieldName="ONLINE_QTY" SummaryType="Sum" DisplayFormat="上线数={0}" />
            <dx:ASPxSummaryItem FieldName="OFFLINE_QTY" SummaryType="Sum" DisplayFormat="下线数={0}" />
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
                            <dx:ASPxTextBox ID="txtRmesid" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>'
                                Visible="false" Enabled="false" />
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
                                Enabled="false" Text='<%# Bind("PLAN_SO") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                                Enabled="false" Width="160px" Text='<%# Bind("PRODUCT_MODEL") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                                Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="生产线" AssociatedControlID="PlineCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="PlineCombo" runat="server" EnableClientSideAPI="True" Enabled="false"
                                Value='<%# Bind("PLINE_CODE") %>' DataSourceID="SqlDataSource1" ValueType="System.String"
                                Width="150px" TextField="pline_name" ValueField="PLINE_CODE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {OnPlineChanged(s); }" />
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="工艺地点" AssociatedControlID="RountingsiteCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxComboBox ID="RountingsiteCombo" runat="server" EnableClientSideAPI="True"
                                Enabled="false" Value='<%# Bind("ROUNTING_SITE") %>' DataSourceID="SqlDataSource1"
                                ValueType="System.String" Width="150px" TextField="pline_name" ValueField="PLINE_CODE"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="跨线模式" AssociatedControlID="ASPxComboBoxAcross1">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="ASPxComboBoxAcross1" runat="server" EnableClientSideAPI="True"
                                Enabled="false" Value='<%# Bind("ROUNTING_CODE") %>' DataSourceID="SqlDataSource2"
                                ValueType="System.String" Width="150px" TextField="ALINE_NAME" ValueField="ALINE_CODE"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                            <dx:ASPxMemo ID="txtRemark" runat="server" Width="100%" Height="50px" Text='<%# Bind("REMARK1") %>'
                                ClientInstanceName="txtRemark1" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
    <table>
        <tr>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel18" runat="server" ForeColor="LightPink"
                    Text="预留号计划 |">
                </dx:ASPxLabel>
                &nbsp&nbsp
            </td>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel1" runat="server" ForeColor="Green" Text="计划员确认 |">
                </dx:ASPxLabel>
                &nbsp&nbsp
            </td>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel41" runat="server" ForeColor="Yellow" Text="流水号生成 |">
                </dx:ASPxLabel>
                &nbsp&nbsp
            </td>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel61" runat="server" ForeColor="LightSkyBlue" Text="BOM转换 |">
                </dx:ASPxLabel>
                &nbsp&nbsp
            </td>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel81" runat="server" ForeColor="Red" Text="库房确认 |">
                </dx:ASPxLabel>
                &nbsp&nbsp
            </td>
            <%--    <td style=" color:Green"> 计划员确认</td>&nbsp&nbsp<td  style=" color:Yellow"> 流水号生成</td>&nbsp&nbsp
    <td  style=" color:Blue"> BOM转换</td>&nbsp&nbsp
    <td  style=" color:Red"> 库房确认</td>&nbsp&nbsp--%>
        </tr>
    </table>
    <dx:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server"
        MinHeight="400px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        CloseAction="CloseButton" ShowHeader="True" HeaderText="柳汽流水号分配" AllowDragging="true"
        Width="80%" MaxHeight="400px">
        <ClientSideEvents CloseUp="function(s, e) { ClosePop(e.result); }" />
        <ClientSideEvents CloseUp="function(s, e) { ClosePop(e.result); }"></ClientSideEvents>
        <ContentCollection>
            <dx:PopupControlContentControl Width="100%">
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
                                }" />
                    <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                ListBoxPanel.cpCallbackName = &#39;&#39;;
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName = ListBoxPanel.cpCallbackName;
                                    theRet = ListBoxPanel.cpCallbackRet;
                                    if(callbackName == &#39;Fail&#39;) 
                                    {
                                        alert(theRet);
                                    }
                                }"></ClientSideEvents>
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent5" runat="server">
                            <table align="center" style="width: 800px; height: 300px;">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 5px" rowspan="5">
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="L1" runat="server" Text="未选">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 5px" rowspan="5">
                                                </td>
                                                <td colspan="2" rowspan="5" height="200px">
                                                    <%--<dx:ASPxListBox ID="ASPxListBoxUnused" runat="server" ClientInstanceName="ListBoxUnused"
                                                        SelectionMode="CheckColumn" Width="210px" Height="200px" ValueField="RMES_ID"
                                                        ValueType="System.String" >
                                                        
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="GHTM" Caption="流水号" Width="100%" />
                                                        </Columns>
                                                    </dx:ASPxListBox>--%>
                                                    <dx:ASPxGridView ID="ASPxGridView12" ClientInstanceName="grid12" runat="server" KeyFieldName="RMES_ID"
                                                        Width="200px" AutoGenerateColumns="False" Settings-VerticalScrollableHeight="300"
                                                        Settings-ShowVerticalScrollBar="true" SettingsPager-PageSize="10" 
                                                        OnCustomCallback="ASPxGridView12_CustomCallback">
                                                        <SettingsPager NumericButtonCount="5" PageSize="15">
                                                        </SettingsPager>
                                                        <Settings ShowHorizontalScrollBar="true" />
                                                        <ClientSideEvents EndCallback="function(s,e){ListBoxPanel.PerformCallback(&#39;init&#39; + &#39;|&#39; + &#39;&#39;);}">
                                                        </ClientSideEvents>
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                                                Width="60px">
                                                                <HeaderTemplate>
                                                                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid12.SelectAllRowsOnPage(s.GetChecked()); }"
                                                                        Style="margin-bottom: 0px" />
                                                                </HeaderTemplate>
                                                                <SelectButton Text="选择">
                                                                </SelectButton>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
                                                            <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" Width="140px" CellStyle-HorizontalAlign="Center"
                                                                Settings-AllowHeaderFilter="True">
                                                                <Settings AutoFilterCondition="Contains" />
                                                                <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains"></Settings>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <Settings ShowVerticalScrollBar="True" ShowHorizontalScrollBar="True" VerticalScrollableHeight="300">
                                                        </Settings>
                                                        <ClientSideEvents EndCallback="function(s,e){ListBoxPanel.PerformCallback('init' + '|' + '');}" />
                                                    </dx:ASPxGridView>
                                                </td>
                                                <td style="width: 5px" rowspan="5">
                                                </td>
                                                <td height="200px" rowspan="5">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="ASPxBT1" runat="server" Text=">>" Border-BorderStyle="None" AutoPostBack="False"
                                                                    ToolTip="Add selected items" ClientInstanceName="btnMoveSelectedItemsToRight">
                                                                    <%--<ClientSideEvents Click="function(s,e) { initListBoxUsed(s,e); }" />--%>
                                                                    <ClientSideEvents Click="function(s, e) { Work('add'); }" />
                                                                    <ClientSideEvents Click="function(s, e) { Work(&#39;add&#39;); }"></ClientSideEvents>
                                                                    <Border BorderStyle="None"></Border>
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxButton ID="ASPxBT2" runat="server" Text="<<" Border-BorderStyle="None" AutoPostBack="false"
                                                                    ClientInstanceName="btnMoveSelectedItemsToLeft" ToolTip="Remove selected items">
                                                                    <ClientSideEvents Click="function(s, e) { Work('del'); }" />
                                                                    <ClientSideEvents Click="function(s, e) { Work(&#39;del&#39;); }"></ClientSideEvents>
                                                                    <Border BorderStyle="None"></Border>
                                                                </dx:ASPxButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 5px" rowspan="5">
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="已选">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 5px" rowspan="5">
                                                </td>
                                                <td colspan="4" height="300px" rowspan="5">
                                                    <dx:ASPxListBox ID="ASPxListBoxUsed" runat="server" ClientInstanceName="ListBoxUsed"
                                                        SelectionMode="CheckColumn" Width="210px" Height="400px" ValueField="RMES_ID"
                                                        ValueType="System.String" OnCallback="ASPxListBoxUsed_Callback">
                                                        <Columns>
                                                            <dx:ListBoxColumn FieldName="GHTM" Caption="流水号" Width="100%" />
                                                        </Columns>
                                                    </dx:ASPxListBox>
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel181" runat="server" Text="已选择数量:" Width="80px">
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
                                                <td style="width: 5px" rowspan="5">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 5px">
                                                </td>
                                                <td style="text-align: left; width: 150px">
                                                    <label style="font-size: small; width: 150px">
                                                        开始日期</label>
                                                </td>
                                                <td>
                                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" runat="server" ClientInstanceName="dateBeginPanel"
                                                        OnCallback="ASPxCallbackPanel2_Callback">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxDateEdit ID="ASPxBeginDate" runat="server" ClientInstanceName="dateBegin"
                                                                    EditFormat="Custom" EditFormatString="yyyy-MM-dd" Width="120px">
                                                                </dx:ASPxDateEdit>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </td>
                                                <td style="width: 5px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5px">
                                                </td>
                                                <td style="text-align: left; width: 150px">
                                                    <label style="font-size: small; width: 150px">
                                                        下线日期</label>
                                                </td>
                                                <td style="width: 120px">
                                                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel3" runat="server" ClientInstanceName="dateEndPanel"
                                                        OnCallback="ASPxCallbackPanel3_Callback">
                                                        <PanelCollection>
                                                            <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                                                <dx:ASPxDateEdit ID="ASPxEndDate" runat="server" ClientInstanceName="dateEnd" EditFormat="Custom"
                                                                    EditFormatString="yyyy-MM-dd" Width="120px">
                                                                </dx:ASPxDateEdit>
                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </td>
                                                <td style="width: auto">
                                                </td>
                                            </tr>
                                            <tr style="height: 30px">
                                                <td>
                                                </td>
                                                <td align="center" colspan="2">
                                                    <dx:ASPxButton ID="UpdateButton" runat="server" AutoPostBack="False" ClientInstanceName="butSubmit"
                                                        ReplacementType="EditFormUpdateButton" Text="提交" Width="80px">
                                                        <ClientSideEvents Click="function(s,e) { checkSubmit(); }" />
                                                        <ClientSideEvents Click="function(s,e) { checkSubmit(); }"></ClientSideEvents>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="center" colspan="2">
                                                    <dx:ASPxButton ID="CancelButton" runat="server" AutoPostBack="False" ReplacementType="EditFormCancelButton"
                                                        Text="取消" Width="80px">
                                                        <ClientSideEvents Click="function(s,e) { popup.Hide(); }" />
                                                        <ClientSideEvents Click="function(s,e) { popup.Hide(); }"></ClientSideEvents>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
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
</asp:Content>
