<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsMaterialRequisitionQry.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsMaterialRequisition.mmsMaterialRequisitionQry" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>领料单</title>
    <script type="text/javascript">
        function checkData() {
            if (cmbPline.GetValue() == "") {
                alert('请选择生产线！');
                return;
            }
            var dateFrom = datePlanDate.GetText();
            var dateTo = datePlanDateTo.GetText();

            if (CheckSn.GetChecked())
                if (dateFrom.substring(0,7) != dateTo.substring(0,7)) {
                    alert('计划开始日期和结束日期请选择相同月份！');
                    return;
                }

            grid.PerformCallback();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel13" Text="生产线：" Width="80px" ></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox runat="server" ID="cmbPline" ClientInstanceName="cmbPline" Width="120px" oninit="cmbPlineCode_Init" SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="计划日期：从" Width="80px"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxDateEdit ID="datePlanDate" ClientInstanceName="datePlanDate" runat="server" Width="120px" 
                    DisplayFormatString="yyyy-MM-dd" oninit="datePlanDate_Init" EditFormat="Custom" 
                    EditFormatString="yyyy-MM-dd" >
                </dx:ASPxDateEdit>   
            </td>
            <td align="right">
                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="到"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxDateEdit ID="datePlanDateTo" ClientInstanceName="datePlanDateTo" runat="server" Width="120px" 
                    DisplayFormatString="yyyy-MM-dd"  EditFormat="Custom" 
                    EditFormatString="yyyy-MM-dd" oninit="datePlanDateTo_Init" >
                </dx:ASPxDateEdit>   
            </td>
            <td align="right" >
                <dx:ASPxCheckBox ID="CheckSn" ClientInstanceName="CheckSn" runat="server" Text="只显示每月设定零件">
                </dx:ASPxCheckBox>
            </td>
            <td ></td>
        </tr>
        </table>
        <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="保管员：从" Width="80px"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtStoreman" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td align="center" >
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="到"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtStoreman2" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td align="right">
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="零件：" Width="40px"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtLj" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td align="right">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="工位：" Width="40px"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtGwdm" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        </table>
            <hr />
    
    <table style="width:100%;">
        <tr>
            <td>
                <dx:ASPxButton ID="BtnQry" runat="server" Text="查询" AutoPostBack="false" Width="100px">
                    <ClientSideEvents Click="function(s,e){checkData();}" />
                </dx:ASPxButton>
            </td>
            <td>
                  <dx:ASPxButton ID="BtnExport" runat="server" Text="导出到Excel" 
                    AutoPostBack="false" Width="100px" onclick="BtnExport_Click"/>
            </td>
            <td style=" width="80%""></td>
        </tr>
        <tr>
            <td colspan="3">
            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" SettingsPager-Mode="ShowAllRecords" 
                    Width="100%" oncustomcallback="ASPxGridView1_CustomCallback" KeyFieldName="ABOM_COMP">
                             <TotalSummary>
            <dx:ASPxSummaryItem FieldName="ABOM_COMP" SummaryType="Count" DisplayFormat="总数={0}"/>
            <dx:ASPxSummaryItem FieldName="ABOM_QTY" SummaryType="Sum" DisplayFormat="总数={0}"/>
        </TotalSummary>
        <Settings ShowFooter="True" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="零件" FieldName="ABOM_COMP"   VisibleIndex="1" Width="20%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件名称" FieldName="ABOM_DESC"   VisibleIndex="1" Width="20%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="工位" FieldName="ABOM_WKCTR" VisibleIndex="2" Width="20%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="库位" FieldName="ABOM_KW" VisibleIndex="3" Width="20%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="保管员" FieldName="ABOM_BGY"   VisibleIndex="1" Width="10%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="ABOM_QTY" VisibleIndex="4" Width="10%" />        
                </Columns> 
                <Settings ShowFilterRow="false" ShowGroupPanel="false" />
            </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
        GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>
    </form>
</body>
</html>
