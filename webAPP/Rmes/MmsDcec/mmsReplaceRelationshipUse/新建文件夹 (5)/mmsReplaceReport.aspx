<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsReplaceReport.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsReplaceReport" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
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
            grid.PerformCallback();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel13" Text="生产线：" Width="80px"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox runat="server" ID="cmbPline" ClientInstanceName="cmbPline" Width="120px" SelectedIndex="0" oninit="cmbPlineCode_Init">
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
    <hr />
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
                <dx:ASPxButton ID="BtnQry" runat="server" Text="查询" AutoPostBack="false" Width="100px">
                    <ClientSideEvents Click="function(s,e){checkData();}" />
                </dx:ASPxButton>
            </td>
            <td >
                                                    <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出-EXCEL" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />
            </td>
        </tr>
        </table>
    <table style="width:100%;">
        <tr>
            <td>
            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"  KeyFieldName="SO"
                    Width="100%" oncustomcallback="ASPxGridView1_CustomCallback">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="SO" FieldName="SO"   VisibleIndex="1" Width="10%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="旧零件代码" FieldName="LJDM1" VisibleIndex="2" Width="20%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="新零件代码" FieldName="LJDM2" VisibleIndex="3" Width="20%"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="4" Width="20%" />     
                    <dx:GridViewDataTextColumn Caption="日期" FieldName="LRSJ" VisibleIndex="5" Width="20%" />  
                    <dx:GridViewDataTextColumn Caption="员工" FieldName="YGMC" VisibleIndex="6" Width="10%" />     
                </Columns> 
                <Settings ShowFilterRow="false" ShowGroupPanel="false" />
            </dx:ASPxGridView>
            </td>
        </tr>
    </table>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>
    </form>
</body>
</html>
