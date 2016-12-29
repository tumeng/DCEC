<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsReplaceRelationshipUse.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsReplaceRelationshipUse" StylesheetTheme="Theme1" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body onunload="closeWin()">
    <form id="form1" runat="server">
    <script type="text/javascript">
        var nextWin = null;
        function closeWin() {
            if (nextWin != null) {
                nextWin.closeWin();
            }
            forClose();
        }
        function forClose() {
            window.opener = null;
            window.open("", "_self");
            window.close();
        }
        function getPlan(s, e) {
            var index = gridPlan.GetSelectedIndex();
            if (index < 0)
                return;
            var so = gridPlan.GetItem(index).GetColumnText(1);
            var planCode = gridPlan.GetItem(index).GetColumnText(0);
            var str = so + ";" + planCode;
            gridOneReplace.PerformCallback(str);
            gridMaterial.PerformCallback(str + ";init");
            gridMultiReplace.PerformCallback(str);
        }
        function resetMaterial(flag) {
            var index = gridPlan.GetSelectedIndex();
            if (index < 0)
                return;
            var so = gridPlan.GetItem(index).GetColumnText(1);
            var planCode = gridPlan.GetItem(index).GetColumnText(0);
            var str = so + ";" + planCode;

            gridMaterial.PerformCallback(str + ";" + flag);
        }
        function openConfirmedOneReplaceQuery() {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }
            var plineCode = cmbPline.GetValue();

            var index = gridPlan.GetSelectedIndex();
            if (index == -1) {
                alert("请先选择计划！");
                return false;
            }

            var so = gridPlan.GetItem(index).GetColumnText(1);
            var planCode = gridPlan.GetItem(index).GetColumnText(0);;
            var plineCode = cmbPline.GetValue();

            nextWin=window.open("mmsConfimedOneReplaceQuery.aspx?plineCode=" + plineCode + "&so=" + so + "&planCode=" + planCode);
        }
        function openConfirmedMultiReplaceQuery() {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }
            var plineCode = cmbPline.GetValue();

            var index = gridPlan.GetSelectedIndex();
            if (index == -1) {
                alert("请先选择计划！");
                return false;
            }

            var so = gridPlan.GetItem(index).GetColumnText(1);
            var planCode = gridPlan.GetItem(index).GetColumnText(0);
            var plineCode = cmbPline.GetValue();

            window.open("mmsConfimedMultiReplaceQuery.aspx?plineCode=" + plineCode + "&so=" + so + "&planCode=" + planCode);

        }
        function replaceByDate() {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }

            var planDate = dtRq.GetText();
            if (!confirm("该操作将对指定日期" + planDate + "下所有计划进行一对一替换批量处理，是否继续？")) {
                return false;
            }
            return true;
        }
        function replaceByPlan() {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }

            var index = gridPlan.GetSelectedIndex();
            if(index==-1){
                alert("请先选择计划！");
                return false;
            }
            var planCode = gridPlan.GetItem(index).GetColumnText(0);
            if (!confirm("该操作将对指定计划" + planCode + "进行替换批量处理，是否继续？"))
                return false;
            return true;
        }
        function openReplaceResult() {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }
            window.open('mmsReplaceResultQry.aspx?plineCode=' + cmbPline.GetValue());
        }
        function OnGetGridMaterialRowValues(values) {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }
            var plineCode = cmbPline.GetValue();

            var index = gridPlan.GetSelectedIndex();
            var so = gridPlan.GetItem(index).GetColumnText(1);
            var planCode = gridPlan.GetItem(index).GetColumnText(0);

            var itemCode = values[0];
            var newPart = values[1];
            var itemQty = values[2];
            var locationCode = values[3];
            var str = 'mmsOneReplace.aspx?plineCode=' + plineCode + '&so=' + so + '&planCode=' + planCode
                + '&oldPart=' + itemCode + '&newPart=' + newPart + '&locationCode=' + locationCode + '&itemQry=' + itemQty;
            window.open(str, '1', 'toolbar=no,location=no,scrollbars=no,menubar=no,status=yes,resizable=yes,depended=yes,width=800,height=550,left=100,top=50');
//            var str = 'mmsOneReplace.aspx?plineCode=' + plineCode + '&so=' + so + '&planCode=' + planCode
//                + '&oldPart=' + itemCode + '&newPart=' + newPart + '&locationCode=' + locationCode + '&itemQry=' + itemQty;
//            window.open(str,'','toolbar=no,location=no,scrollbars=no,menubar=no,status=yes,resizable=yes,width=800,height=550,left=100,top=50');
        }
        function OnGetGridMaterialRowValuesMulti(values) {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }
            var plineCode = cmbPline.GetValue();

            var index = gridPlan.GetSelectedIndex();
            var so = gridPlan.GetItem(index).GetColumnText(1);
            var planCode = gridPlan.GetItem(index).GetColumnText(0);

            var itemCode = values[0];
            var newPart = values[1];
            var itemQty = values[2];
            var locationCode = values[3];
            var str = 'mmsMultiReplace.aspx?plineCode=' + plineCode + '&so=' + so + '&planCode=' + planCode
                + '&oldPart=' + itemCode + '&newPart=' + newPart + '&locationCode=' + locationCode + '&itemQry=' + itemQty;
            window.open(str, '2', 'toolbar=no,location=no,scrollbars=no,menubar=no,status=yes,resizable=yes,width=800,height=550,left=100,top=50');
//            window.open('mmsMultiReplace.aspx?plineCode=' + plineCode + '&so=' + so + '&planCode=' + planCode
//                + '&oldPart=' + itemCode + '&newPart=' + newPart + '&locationCode=' + locationCode + '&itemQry=' + itemQty);
        }
        function openConfirmOneReplace() {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }
            var plineCode = cmbPline.GetValue();
            window.open("mmsConfirmOneReplace.aspx?plineCode=" + plineCode);
        }
        function openConfirmMultiReplace() {
            var plineCode = cmbPline.GetValue();
            if (plineCode == null) {
                alert("请选择生产线！");
                return false;
            }
            var plineCode = cmbPline.GetValue();
            window.open("mmsConfirmMultiReplace.aspx?plineCode=" + plineCode);
        }

//        function changeSeq(s, e) {
//            index = e.visibleIndex;
//            var buttonID = e.buttonID;
//            if (buttonID == "OneReplace")
//                gridMaterial.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);
//        }
//        function GetDataCallback(s) {
//            var result = "";
//            var retStr = "";
//            if (s == null) {
//                gridMaterial.PerformCallback();
//                return;
//            }
//            var array = s.split(',');
//            retStr = array[1];
//            result = array[0];

//            switch (result) {
//                case "OK":
//                    alert(retStr);
//                    return;
//                case "Fail":
//                    alert(retStr);
//                    return;
//            }
//            gridMaterial.PerformCallback();
//        }

    </script>
    <table style="width:100%;">
        <tr>
            <td style="width:40%;vertical-align:top">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width:50px" >
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线:">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxComboBox ID="cmbPline" ClientInstanceName="cmbPline" runat="server" Width="100%"  SelectedIndex="0"
                                            oninit="cmbPline_Init">
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width:80px" align="right">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="计划日期:">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxDateEdit ID="dtRq" ClientInstanceName="dtRq" runat="server" Width="100%"></dx:ASPxDateEdit>
                                    </td>
                                    <td style="width:80px">
                                        <dx:ASPxButton ID="cmdQry" runat="server" Text="查询" AutoPostBack="False" Width="100%" Height="16px">
                                        <ClientSideEvents Click="function(s,e){
                                            var plineCode = cmbPline.GetValue();
                                            if (plineCode == null) {
                                                alert('请选择生产线！');
                                                return false;
                                            }
                                            gridPlan.PerformCallback();}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                </table>
                                <table>
                                <tr>
                                    <td style="width:107px">
                                        <dx:ASPxButton ID="cmdReplaceByDate" runat="server" Text="按日期替换" Width="100%" 
                                            onclick="cmdReplaceByDate_Click" >
                                            <ClientSideEvents Click="function(s,e){if(!replaceByDate()) e.processOnServer = false; }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:108px">
                                        <dx:ASPxButton ID="cmdReplaceByPlan" runat="server" Text="按计划替换" Width="100%" 
                                            onclick="cmdReplaceByPlan_Click">
                                            <ClientSideEvents Click="function(s,e){if(!replaceByPlan()) e.processOnServer = false; }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxButton ID="cmdViewReplaceResult" runat="server" AutoPostBack="False" Text="查看替换结果" Width="100%">
                                            <ClientSideEvents Click="function(s,e){openReplaceResult();}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxButton ID="cmdQryAll" runat="server"  AutoPostBack="False" Text="查看全部替换关系" Width="100%">
                                            <ClientSideEvents Click="function(s,e){window.open('mmsReplaceRelationshipQry.aspx');}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxListBox ID="gridPlan" ClientInstanceName="gridPlan" runat="server" Width="100%" 
                                oncallback="gridPlan_Callback" >
                            <Columns>
                                <dx:ListBoxColumn Caption="计划编号" FieldName="PLAN_CODE" Width="20%" />
                                <dx:ListBoxColumn Caption="计划SO" FieldName="PLAN_SO" Width="15%"></dx:ListBoxColumn>
                                <dx:ListBoxColumn Caption="数量" FieldName="PLAN_QTY" Width="10%"></dx:ListBoxColumn>
                                <dx:ListBoxColumn Caption="用户名称" FieldName="CUSTOMER_NAME" Width="40%" ></dx:ListBoxColumn>
                                <dx:ListBoxColumn Caption="上线数量" FieldName="ONLINE_QTY" Width="15%"></dx:ListBoxColumn>                                
                            </Columns>
                            <ClientSideEvents SelectedIndexChanged="function(s,e){getPlan(s,e);}" />
                            </dx:ASPxListBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                                Width="100%">
                                <TabPages>
                                    <dx:TabPage Text="一对一替换">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="gridOneReplace" ClientInstanceName="gridOneReplace" 
                                                    runat="server" Width="100%" KeyFieldName="OLDPART" AutoGenerateColumns="False" 
                                                    OnCustomCallback="gridOneReplace_CustomCallback" 
                                                    OnPageIndexChanged="gridOneReplace_PageIndexChanged" 
                                                    OnHtmlRowPrepared="gridOneReplace_HtmlRowPrepared" OnInit="gridOneReplace_Init">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="原零件" FieldName="OLDPART" VisibleIndex="0" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="零件名称" FieldName="OLDPART_NAME" VisibleIndex="2" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="替换件" FieldName="NEWPART" VisibleIndex="3" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="零件名称" FieldName="NEWPART_NAME" VisibleIndex="4" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="状态" FieldName="STATUS" VisibleIndex="4" Width=""></dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Settings ShowGroupPanel="false" ShowVerticalScrollBar="false" />
                                                    <SettingsPager PageSize="10"></SettingsPager>
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                    <dx:TabPage Text="多对多替换">
                                        <ContentCollection>
                                            <dx:ContentControl ID="ContentControl2" runat="server" SupportsDisabledAttribute="True">
                                                <dx:ASPxGridView ID="gridMulitiReplace" ClientInstanceName="gridMultiReplace" 
                                                    runat="server" KeyFieldName="OLDPART" Width="100%" 
                                                    OnCustomCallback="gridMulitiReplace_CustomCallback" 
                                                    OnPageIndexChanged="gridMulitiReplace_PageIndexChanged"  OnHtmlRowPrepared="gridMulitiReplace_HtmlRowPrepared"
                                                    OnInit="gridMulitiReplace_Init">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="原零件" FieldName="OLDPART" VisibleIndex="0" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="零件名称" FieldName="OLDPART_NAME" VisibleIndex="2" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="替换件" FieldName="NEWPART" VisibleIndex="3" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="零件名称" FieldName="NEWPART_NAME" VisibleIndex="4" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="状态" FieldName="STATUS" VisibleIndex="5" Width=""></dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="分组" FieldName="THGROUP" VisibleIndex="6" Width=""></dx:GridViewDataTextColumn>
                                                    </Columns>      
                                                    <Settings ShowGroupPanel="false" ShowVerticalScrollBar="false" />
                                                    <SettingsPager PageSize="10"></SettingsPager>                                                                                              
                                                </dx:ASPxGridView>
                                            </dx:ContentControl>
                                        </ContentCollection>
                                    </dx:TabPage>
                                </TabPages>
                            </dx:ASPxPageControl>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:60%;vertical-align:top">
                <table style="width:100%;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="width:120px;">
                                        <dx:ASPxButton ID="cmbSupplier" runat="server" Text="显示指定供应商" width="100%" 
                                            BackColor="#FFFF99" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s,e){resetMaterial('supplier');}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px;">
                                        <dx:ASPxButton ID="cmbOneReplace" runat="server" Text="显示一对一替换" width="100%" 
                                            BackColor="#999999" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s,e){resetMaterial('one');}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px;">
                                        <dx:ASPxButton ID="cmbMultiReplace" runat="server" Text="显示多对多替换" width="100%" 
                                            BackColor="#9CFC9C" AutoPostBack="False" >
                                            <ClientSideEvents Click="function(s,e){alert('注意:多重替换零件不能和其他类型替换零件重复');resetMaterial('multi');}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="cmbConfirmedOneReplace" runat="server" Text="已确认一对一查询" AutoPostBack="False" width="100%">
                                            <ClientSideEvents Click="function(s,e){openConfirmedOneReplaceQuery();}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="cmbConfirmOneReplace" runat="server" AutoPostBack="False" Text="确认一对一替换" width="100%">
                                            <ClientSideEvents Click="function(s,e){openConfirmOneReplace();}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="cmbConfirmedMultiReplace" runat="server" Text="已确认多对多查询" AutoPostBack="False" width="100%">
                                            <ClientSideEvents Click="function(s,e){openConfirmedMultiReplaceQuery();}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="cmbConfirmMultiReplace" runat="server" AutoPostBack="False" Text="确认多对多替换" width="100%">
                                            <ClientSideEvents Click="function(s,e){openConfirmMultiReplace();}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><%--OnCustomDataCallback="ASPxGridView1_CustomDataCallback"--%>
                        <td>
                            <dx:ASPxGridView ID="gridMaterial" ClientInstanceName="gridMaterial" 
                                runat="server" Width="100%" 
                                oncustomcallback="gridMaterial_CustomCallback" KeyFieldName="ITEM_CODE" 
                                onpageindexchanged="gridMaterial_PageIndexChanged" 
                                oncustombuttoninitialize="gridMaterial_CustomButtonInitialize" oninit="gridMaterial_Init" 
                                >
<%--                                        <ClientSideEvents CustomButtonClick="function (s,e){
        changeSeq(s,e);
    }" />--%>
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="60px" Visible="false">
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="OneReplace" Text="替换"></dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="MultiReplace" Text="替换"></dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <ClearFilterButton Visible="true"></ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="零件号" FieldName="ITEM_CODE" VisibleIndex="0" Width="80px"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="名称" FieldName="ITEM_NAME" VisibleIndex="2" Width="150px"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="3" Width="50px"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="4" Width="60px"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="8" Width="60px"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="类型" FieldName="ITEM_TYPE" VisibleIndex="9" Width="60px"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="状态" FieldName="COMMENT1" VisibleIndex="13" Width="100px"></dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="" FieldName="NEWPART" VisibleIndex="14" Width=""></dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFilterRow="false" />
                                <SettingsPager PageSize="20"></SettingsPager>
                                <ClientSideEvents CustomButtonClick="function(s,e){
                                    var index=e.visibleIndex;
                                    if(e.buttonID=='OneReplace')
                                        gridMaterial.GetRowValues(index, 'ITEM_CODE;NEWPART;ITEM_QTY;LOCATION_CODE', OnGetGridMaterialRowValues); 
                                    if(e.buttonID=='MultiReplace')
                                        gridMaterial.GetRowValues(index, 'ITEM_CODE;NEWPART;ITEM_QTY;LOCATION_CODE', OnGetGridMaterialRowValuesMulti); 

                                    
                                        
                                }" />
                            </dx:ASPxGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
