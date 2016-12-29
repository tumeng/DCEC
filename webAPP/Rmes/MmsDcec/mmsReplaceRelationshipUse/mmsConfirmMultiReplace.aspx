<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsConfirmMultiReplace.aspx.cs"
    Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsConfirmMultiReplace"
    StylesheetTheme="Theme1" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx1" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>确认多对多替换</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function getPlan(s, e) {
            var index = gridPlan.GetSelectedIndex();
            var so = gridPlan.GetItem(index).GetColumnText(1);
            var planCode = gridPlan.GetItem(index).GetColumnText(0);
            var str = so + ";" + planCode + ";init";

            grid.PerformCallback(str);
            cmbGroup.PerformCallback(str);
        }
        function AllConfirm() {

            var date1 = dataPlan1.GetDate().toLocaleString();

            var str = date1 + ";;all";
            grid.PerformCallback(str);
        }
        function Fff() {
            var alertContent = grid.cpAlertContent;
            if (alertContent != "")
                alert(alertContent);
        }
    </script>
    <table style="width: 100%;">
        <tr>
            <td style="width: 40%">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="计划日期：">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="DatePlan" runat="server" Width="150px" ClientInstanceName="dataPlan1"
                                DisplayFormatString="yyyy-MM-dd">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnQry" runat="server" AutoPostBack="false" Text="查询" Width="80px">
                                <ClientSideEvents Click="function(s,e){gridPlan.PerformCallback();}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnConfirmAll" runat="server" Text="确认所有计划" AutoPostBack="false"
                                Width="120px">
                                <ClientSideEvents Click="function(s,e){
                                     AllConfirm();

                                }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="分组号：">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbGroup" ClientInstanceName="cmbGroup" runat="server" Width="120px"
                                OnCallback="cmbGroup_Callback">
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDeleteGroup" runat="server" AutoPostBack="false" Text="按组号删除"
                                Width="100px">
                                <ClientSideEvents Click="function(s,e){
                                    var group = cmbGroup.GetText();
                                    if(group==''){
                                        alert('请选择分组号！');
                                        e.processOnServer=false;
                                        return;
                                    }
                                    if(!confirm('即将删除数据，继续？'))
                                    {
                                        e.processOnServer=false;
                                        return;
                                    }
                                    var index = gridPlan.GetSelectedIndex();
                                    var so = gridPlan.GetItem(index).GetColumnText(1);
                                    var planCode = gridPlan.GetItem(index).GetColumnText(0);

                                    ASPxCallback2.PerformCallback(so+';'+planCode+';'+group);
                                }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnConfirmGroup" AutoPostBack="false" runat="server" Text="按组号确认"
                                Width="100px">
                                <ClientSideEvents Click="function(s,e){
                                    var group = cmbGroup.GetText();
                                    if(group==''){
                                        alert('请选择分组号！');
                                        e.processOnServer=false;
                                        return;
                                    }
                                    var index = gridPlan.GetSelectedIndex();
                                    var so = gridPlan.GetItem(index).GetColumnText(1);
                                    var planCode = gridPlan.GetItem(index).GetColumnText(0);

                                    ASPxCallback1.PerformCallback(so+';'+planCode+';'+group);
                                }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <dx:ASPxListBox ID="gridPlan" ClientInstanceName="gridPlan" Height="500px" runat="server"
                    Width="100%" OnCallback="gridPlan_Callback">
                    <Columns>
                        <dx:ListBoxColumn Caption="计划编号" FieldName="PLAN_CODE" Width="20%" />
                        <dx:ListBoxColumn Caption="计划SO" FieldName="PLAN_SO" Width="15%"></dx:ListBoxColumn>
                        <dx:ListBoxColumn Caption="数量" FieldName="PLAN_QTY" Width="10%"></dx:ListBoxColumn>
                        <dx:ListBoxColumn Caption="用户名称" FieldName="CUSTOMER_NAME" Width="40%"></dx:ListBoxColumn>
                        <dx:ListBoxColumn Caption="上线数量" FieldName="ONLINE_QTY" Width="15%"></dx:ListBoxColumn>
                    </Columns>
                    <ClientSideEvents SelectedIndexChanged="function(s,e){getPlan(s,e);}" />
                </dx:ASPxListBox>
            </td>
            <td valign="top">
                <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="LJDM1"
                    Width="100%" OnCustomCallback="grid_CustomCallback" OnRowDeleting="grid_RowDeleting"  SettingsPager-Mode="ShowAllRecords">
                    <SettingsBehavior AllowSort="false" />
                    <Settings ShowFilterRow="false" />
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="60px" Visible="false">
                            <DeleteButton Visible="true">
                            </DeleteButton>
                            <ClearFilterButton Visible="true">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM1" VisibleIndex="0" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="2" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="替换零件" FieldName="LJDM2" VisibleIndex="3" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC1" VisibleIndex="4" Width="60px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工序" FieldName="GXMC1" VisibleIndex="8" Width="60px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="9" Width="60px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="员工" FieldName="YGMC" VisibleIndex="13" Width="60px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="日期" FieldName="LRSJ" VisibleIndex="14" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="分组" FieldName="THGROUP" VisibleIndex="15" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="是否确认" FieldName="ISTRUE" VisibleIndex="16" Width="60px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="plan_code" Visible="false">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowGroupPanel="false" ShowVerticalScrollBar="false" ShowHorizontalScrollBar="true" />
                    <ClientSideEvents BeginCallback="function(s,e){grid.cpAlertContent='';}" EndCallback="function(s,e){Fff();}" />
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <dx1:ASPxCallback ID="ASPxCallback1" ClientInstanceName="ASPxCallback1" runat="server"
                    OnCallback="ASPxCallback1_Callback">
                     <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                ASPxCallback1.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                     
                                    callbackName =  ASPxCallback1.cpCallbackName;
                                    theRet = ASPxCallback1.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    
                                    getPlan(s,e);
                                      
                                   
                                }" />
                </dx1:ASPxCallback>
                <dx1:ASPxCallback ID="ASPxCallback2" ClientInstanceName="ASPxCallback2" runat="server"
                    OnCallback="ASPxCallback2_Callback">
                    <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                ASPxCallback2.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                     
                                    callbackName =  ASPxCallback2.cpCallbackName;
                                    theRet = ASPxCallback2.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    
                                    getPlan(s,e);
                                      
                                   
                                }" />
                </dx1:ASPxCallback>
                <dx1:ASPxCallback ID="ASPxCallback3" ClientInstanceName="ASPxCallback3" runat="server"
                    OnCallback="ASPxCallback3_Callback">
                    <ClientSideEvents BeginCallback="function(s,e){ASPxCallback3.cpAlertContent='';}"
                        EndCallback="function(s,e){
                     <%--alert('');
                                    alertContent=ASPxCallback3.cpAlertContent;

                                    
                                        alert(alertContent);--%>
                                }" />
                </dx1:ASPxCallback>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
