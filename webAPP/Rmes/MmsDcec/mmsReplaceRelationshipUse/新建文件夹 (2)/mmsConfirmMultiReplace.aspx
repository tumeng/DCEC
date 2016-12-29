<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsConfirmMultiReplace.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsConfirmMultiReplace" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

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
            var str = so + ";" + planCode;

            grid.PerformCallback(str);
            cmbGroup.PerformCallback(str);
        }
        function AllConfirm() { 
                                            <%--var index = gridPlan.GetSelectedIndex();--%>
                                    var date1=dataPlan1.GetDate().toLocaleString();
<%--                                    var so = gridPlan.GetItem(index).GetColumnText(1);
                                    var planCode = gridPlan.GetItem(index).GetColumnText(0);--%>
                                    ASPxCallback3.PerformCallback(date1);
                                    <%--ASPxCallback3.PerformCallback(date1+';'+so+';'+planCode);--%>
        }
    </script>
    <table style="width:100%;">
        <tr>
            <td style="width:40%">

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
        </tr>
        </table>

            </td>
            <td>
                <table>
                    <tr>
                        <td><%--onclick="btnConfirm_Click"--%>
                            <dx:ASPxButton ID="btnConfirmAll" runat="server" Text="全部确认" AutoPostBack="false" Width="80px" >
                                 <ClientSideEvents Click="function(s,e){
                                     AllConfirm();

                                }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="分组号：">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbGroup" ClientInstanceName="cmbGroup" runat="server" Width="120px" 
                                oncallback="cmbGroup_Callback">
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnDeleteGroup" runat="server" AutoPostBack="false" Text="按组号删除" Width="100px">
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
                                Width="100px" >
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
                            <dx:ASPxListBox ID="gridPlan" ClientInstanceName="gridPlan" Height="500px"
                    runat="server" Width="100%"
                                oncallback="gridPlan_Callback" >
                            <Columns>
                                <dx:listboxcolumn Caption="计划编号" FieldName="PLAN_CODE" Width="20%" />
                                <dx:listboxcolumn Caption="计划SO" FieldName="PLAN_SO" Width="15%">
                                </dx:listboxcolumn>
                                <dx:listboxcolumn Caption="数量" FieldName="PLAN_QTY" Width="10%">
                                </dx:listboxcolumn>
                                <dx:listboxcolumn Caption="用户名称" FieldName="CUSTOMER_NAME" Width="40%" >
                                </dx:listboxcolumn>
                                <dx:listboxcolumn Caption="上线数量" FieldName="ONLINE_QTY" Width="15%">
                                </dx:listboxcolumn>                                
                            </Columns>
                            <ClientSideEvents SelectedIndexChanged="function(s,e){getPlan(s,e);}" />
                            </dx:ASPxListBox>
                        </td>
            <td>
                <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="LJDM1"
                    Width="100%" oncustomcallback="grid_CustomCallback"  
                    onrowdeleting="grid_RowDeleting">

                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="60px" Visible="false">
                            <DeleteButton Visible="true"></DeleteButton>
                            <ClearFilterButton Visible="true"></ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM1" VisibleIndex="0" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="2" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="替换零件" FieldName="LJDM2" VisibleIndex="3" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC1" VisibleIndex="4" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工序" FieldName="GXMC1" VisibleIndex="8" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="9" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="员工" FieldName="YGMC" VisibleIndex="13" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="日期" FieldName="LRSJ" VisibleIndex="14" Width="120px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="分组" FieldName="THGROUP" VisibleIndex="15" Width="100px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="是否确认" FieldName="ISTRUE" VisibleIndex="16" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="plan_code" Visible="false"></dx:GridViewDataTextColumn>
                    </Columns>
                <Settings ShowGroupPanel="false" ShowVerticalScrollBar="false" ShowHorizontalScrollBar="true" />
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <dx1:ASPxCallback ID="ASPxCallback1" ClientInstanceName="ASPxCallback1" runat="server" 
                    oncallback="ASPxCallback1_Callback">
                    <ClientSideEvents EndCallback="function(s,e){
                        getPlan(s,e);
                    }" />
                </dx1:ASPxCallback>
                <dx1:ASPxCallback ID="ASPxCallback2" ClientInstanceName="ASPxCallback2" runat="server" 
                    oncallback="ASPxCallback2_Callback">
                    <ClientSideEvents EndCallback="function(s,e){
                        getPlan(s,e);
                    }" />
                </dx1:ASPxCallback>
                <dx1:ASPxCallback ID="ASPxCallback3" ClientInstanceName="ASPxCallback3" runat="server" 
                    oncallback="ASPxCallback3_Callback">

                </dx1:ASPxCallback>
            </td>
            <td></td>
        </tr>
    </table>

    </form>
</body>
</html>
