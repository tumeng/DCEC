<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qms2301bak.aspx.cs" Inherits="Rmes.WebApp.Rmes.Qms.qms2300.qms2301bak" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
    // <![CDATA[

        function changeSeq(s, e) {
            index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);

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
        function checkAdd() {

            
//            UpdateButtonState();
//            if (listPlineOldC.GetSelectedIndex() == -1 || listPlineNewC.GetSelectedIndex() == -1 || comboAlineClient.GetSelectedIndex() == -1 || listProcessC.GetSelectedItems().length == 0 || listLocationTHC.GetSelectedItems().length == 0) {
//                alert("请选择跨线模式、原生产线、目标生产线、工序以及替换工位再提交！");
//                return false;
//            }
        }
        function checkDel() {
            var count = grid.GetSelectedRowCount();
            if (count = 0) {
                alert("请选择要删除的检测数据");
                return;
            }
//            else {
//                var filedNames = "DETECT_CODE";
//                grid.GetSelectedFieldValues(filedNames, RecoverResult);
//            }
        }
        function RecoverResult(result) {
//            if (result == "") {
////                autosn.SetEnabled(true);
//                alert('没有选择任何检测数据！无法进行操作');
//            }
//            else {
//                ref = "DEL," + result;
//                CallbackSubmit.PerformCallback(ref);
//            }
        }
        function AddSelectedItems() {
//            MoveSelectedItems(lbAvailable, lbChoosen);
//            UpdateButtonState();
        }
        function AddAllItems() {
//            MoveAllItems(lbAvailable, lbChoosen);
//            UpdateButtonState();
        }
        function RemoveSelectedItems() {
//            MoveSelectedItems(lbChoosen, lbAvailable);
//            UpdateButtonState();
        }
        function RemoveAllItems() {
//            MoveAllItems(lbChoosen, lbAvailable);
//            UpdateButtonState();
        }
        function MoveSelectedItems(srcListBox, dstListBox) {
//            srcListBox.BeginUpdate();
//            dstListBox.BeginUpdate();
//            var items = srcListBox.GetSelectedItems();
//            for (var i = items.length - 1; i >= 0; i = i - 1) {
//                dstListBox.AddItem(items[i].text, items[i].value);
//                srcListBox.RemoveItem(items[i].index);
//            }
//            srcListBox.EndUpdate();
//            dstListBox.EndUpdate();
        }
        function MoveAllItems(srcListBox, dstListBox) {
//            srcListBox.BeginUpdate();
//            var count = srcListBox.GetItemCount();
//            for (var i = 0; i < count; i++) {
//                var item = srcListBox.GetItem(i);
//                dstListBox.AddItem(item.text, item.value);
//            }
//            srcListBox.EndUpdate();
//            srcListBox.ClearItems();
        }
        function UpdateButtonState() {
//            btnMoveAllItemsToRight.SetEnabled(lbAvailable.GetItemCount() > 0);
//            btnMoveAllItemsToLeft.SetEnabled(lbChoosen.GetItemCount() > 0);
//            btnMoveSelectedItemsToRight.SetEnabled(lbAvailable.GetSelectedItems().length > 0);
//            btnMoveSelectedItemsToLeft.SetEnabled(lbChoosen.GetSelectedItems().length > 0);
        }
    // ]]> 
    </script>
    <form id="form1" runat="server">
    <div style="float: left">
        <table width="1050px">
            <tr style="height: 20px">
                <td style="width: 20px" colspan="4" align="center">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择生产线、站点、系列和检测数据，确定提交" Font-Size="Medium"
                        Width="400px">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px">
                </td>
                <td style="width: 60px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 300px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline" runat="server"
                        Width="280px" Height="25px" ValueField="RMES_ID" TextField="PLINE_NAME">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 620px">
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择站点">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboStationCode" ClientInstanceName="listBoxStation" runat="server"
                        Width="280px" Height="25px" OnCallback="comboStationCode_Callback">
                        <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { filterStationL(); }" />--%>
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="选择产品系列">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboPSeries" ClientInstanceName="comboPSeriesC" runat="server"
                        Width="280px" Height="25px" OnCallback="comboPSeries_Callback">
                        <%-- <ClientSideEvents SelectedIndexChanged="function(s, e) { filterDetect(); }" />--%>
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 230px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择检测数据">
                    </dx:ASPxLabel>
                </td>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td valign="top" style="width: 35%">
                                <div class="BottomPadding">
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="待选检测数据:" />
                                </div>
                                <dx:ASPxListBox ID="ASPxListBox1" runat="server" ClientInstanceName="lbAvailable"
                                    ValueField="RMES_ID" ValueType="System.String" Width="100%" Height="240px" SelectionMode="CheckColumn"
                                    OnCallback="lbAvailable_Callback"  ViewStateMode="Inherit">
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="DETECT_CODE" Caption="检测数据代码" Width="30%" />
                                        <dx:ListBoxColumn FieldName="DETECT_NAME" Caption="检测数据名称" Width="60%" />
                                    </Columns>
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />
                                </dx:ASPxListBox>
                            </td>
                            <td valign="middle" align="center" style="padding: 10px; width: 10%">
                                <div>
                                    <asp:Button ID="ButtonAdd" runat="server" OnClientClick="return checkAdd();" OnClick="ButtonAdd_Click"
                                        Text="增加>>" Width="80px" Height="23px" />
                                    <%--<dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                                        AutoPostBack="False" Text="增加 >" Width="130px" Height="23px" ClientEnabled="False"
                                        ToolTip="Add selected items">
                                        <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                                    </dx:ASPxButton>--%>
                                </div>
                                <div class="TopPadding">
                                    <%--<dx:ASPxButton ID="ASPxButton2" runat="server" ClientInstanceName="btnMoveAllItemsToRight"
                                        AutoPostBack="False" Text="增加全部 >>" Width="130px" Height="23px" ToolTip="Add all items">
                                        <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                                    </dx:ASPxButton>--%>
                                </div>
                                <div style="height: 32px">
                                </div>
                                <div>
                                    <asp:Button ID="ButtonDelete" runat="server" OnClientClick="return checkDel();" OnClick="ButtonDelete_Click"
                                        Text="<<删除" Width="80px" Height="23px" />
                                    <%--<dx:ASPxButton ID="ASPxButton3" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                                        AutoPostBack="False" Text="< 删除" Width="130px" Height="23px" ClientEnabled="False"
                                        ToolTip="Remove selected items">
                                        <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                                    </dx:ASPxButton>--%>
                                </div>
                                <div class="TopPadding">
                                    <%--<dx:ASPxButton ID="ASPxButton4" runat="server" ClientInstanceName="btnMoveAllItemsToLeft"
                                        AutoPostBack="False" Text="<< 删除全部" Width="130px" Height="23px" ClientEnabled="False"
                                        ToolTip="Remove all items">
                                        <ClientSideEvents Click="function(s, e) { RemoveAllItems(); }" />
                                    </dx:ASPxButton>--%>
                                </div>
                            </td>
                            <td valign="top" style="width: 35%">
                                <div class="BottomPadding">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="已选检测数据:" />
                                </div>
                                <%--<dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                                    OnCallback="ASPxCbSubmit_Callback">
                                    <clientsideevents callbackcomplete="function(s, e) { submitRtr(e.result); }" />
                                </dx:ASPxCallback>--%>
                                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="DETECT_CODE" Width="100%"  Settings-VerticalScrollableHeight="220"
                                    AutoGenerateColumns="False" SettingsPager-Mode="ShowAllRecords" Settings-ShowVerticalScrollBar="true" Settings-ShowHeaderFilterButton="False" ><%--OnRowValidating="ASPxGridView1_RowValidating" OnCustomCallback="ASPxGridView1_CustomCallback"
                                    OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize"
                                    OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize" OnHtmlRowCreated="ASPxGridView1_HtmlRowCreated"
                                    OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared">--%>
                                   
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                            Width="60px">
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                                                    Style="margin-bottom: 0px" CellStyle-HorizontalAlign="Center" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewCommandColumn Caption="调序" Width="80px" ButtonType="Image" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <CustomButtons>
                                                <dx:GridViewCommandColumnCustomButton ID="Up">
                                                    <Image Url="../../Pub/Images/Up.png" Width="15px" ToolTip="上调" />
                                                </dx:GridViewCommandColumnCustomButton>
                                                <dx:GridViewCommandColumnCustomButton ID="Down">
                                                    <Image Url="../../Pub/Images/Down.png" Width="15px" ToolTip="下调">
                                                    </Image>
                                                </dx:GridViewCommandColumnCustomButton>
                                            </CustomButtons>
                                        </dx:GridViewCommandColumn>
                                       
                                        <dx:GridViewDataTextColumn Caption="检测数据代码" FieldName="DETECT_CODE" Width="40%" CellStyle-HorizontalAlign="left">
                                            <Settings AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="检测数据名称" FieldName="DETECT_NAME" Width="60%" CellStyle-HorizontalAlign="left">
                                            <Settings AutoFilterCondition="Contains"></Settings>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior ColumnResizeMode="Control" />
                                    <ClientSideEvents CustomButtonClick="function (s,e){
                                            changeSeq(s,e);
                                        }" />
                                </dx:ASPxGridView>
                                <%--<dx:ASPxListBox ID="listChosedDetect" runat="server" ClientInstanceName="lbChoosen"
                                    Width="100%" Height="240px" SelectionMode="CheckColumn">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }">
                                    </ClientSideEvents>
                                </dx:ASPxListBox>--%>
                            </td>
                            <%--<td>
                                <dx:ASPxButton ID="ASPxButton5" runat="server" ClientInstanceName="btnMoveUp" AutoPostBack="False"
                                    Text="上移" Width="60px" Height="23px" ClientEnabled="False" Style="margin-bottom: 0px"
                                    OnCommand="ASPxButton5_Command">
                                    <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="ASPxButton6" runat="server" ClientInstanceName="btnMoveDown" AutoPostBack="False"
                                    Text="下移" Width="60px" Height="23px" ClientEnabled="False" Style="margin-bottom: 0px">
                                    <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                                </dx:ASPxButton>
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 50px">
                <td>
                </td>
                <td>
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="butConfirm" runat="server" OnClientClick="return checkSubmit();"
                        OnClick="butConfirm_Click" Text="确定" Width="100px" Height="30px" />
                    &nbsp;
                    <asp:Button ID="ButtonCloseWindow" runat="server" OnClick="butCloseWindow_Click"
                        Text="关闭窗口" Width="100px" Height="30px" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    var pline;
    var stationCodeL;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterStation() {
        pline = listPline.GetValue().toString();

        listBoxStation.PerformCallback(pline);
        comboPSeriesC.PerformCallback(pline);
        //        listDetectC.PerformCallback(pline);
        lbAvailable.PerformCallback(pline);
    }
    function checkSubmit() {
//        if (lbChoosen.GetItemCount() == 0 || listBoxStation.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1) {
//            alert("请选择站点、生产线、产品系列和检测数据再提交！");
//            return false;
//        }
    }

</script>
</html>
