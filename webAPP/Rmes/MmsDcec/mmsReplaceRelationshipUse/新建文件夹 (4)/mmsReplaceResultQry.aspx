<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsReplaceResultQry.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsReplaceResultQry" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看替换结果</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function query() {
            grid.PerformCallback(DatePlan.GetText());
        }
    </script>
    <table>
        <tr>
            <td style="width:60px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="计划日期：">
                </dx:ASPxLabel>
            </td>
            <td style="width:120px">
                <dx:ASPxDateEdit ID="DatePlan" runat="server" Width="100%" DisplayFormatString="yyyy-MM-dd" oninit="DatePlan_Init" >
                <ClientSideEvents DateChanged="function(s,e){cmbPlan.PerformCallback(DatePlan.GetText());}" />
                </dx:ASPxDateEdit>
            </td>
            <td style="width:60px" align="right">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="计划：">
                </dx:ASPxLabel>
            </td>
            <td style="width:120px">
                <dx:ASPxComboBox ID="cmbPlan" ClientInstanceName="cmbPlan" runat="server" Width="100%" 
                    oncallback="cmbPlan_Callback">
                </dx:ASPxComboBox>
            </td>
            <td style="width:100px">
                <dx:ASPxButton ID="btnQry" runat="server" AutoPostBack="false" Text="查询" Width="100%">
                <ClientSideEvents Click="function(s,e){query();}" />
                </dx:ASPxButton>
            </td>
            <td style="width:100px">
                &nbsp;</td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" 
        Width="100%" oncustomcallback="ASPxGridView1_CustomCallback" 
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared">
    <Columns>
        <dx:GridViewDataTextColumn Caption="计划名称" FieldName="JHDM" VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="2" Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="原零件" FieldName="LJDM1" VisibleIndex="3" Width="80px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="替换件" FieldName="LJDM2"  VisibleIndex="5" Width="80px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="6" Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="提交时间" FieldName="LRSJ" VisibleIndex="7" Width="140px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="信息说明" FieldName="BZ" VisibleIndex="8" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="提交用户" FieldName="YGMC" VisibleIndex="10" Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="确认用户" FieldName="QRYGMC" VisibleIndex="12" Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="确认时间" FieldName="QRSJ" VisibleIndex="13" Width="140px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="确认" FieldName="ISTRUE" VisibleIndex="14" Width="60px"></dx:GridViewDataTextColumn>
    </Columns>
    </dx:ASPxGridView>

    </form>
</body>
</html>
