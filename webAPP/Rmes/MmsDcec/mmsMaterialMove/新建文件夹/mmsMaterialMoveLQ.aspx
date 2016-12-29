<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsMaterialMoveLQ.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsMaterialMove.mmsMaterialMoveLQ" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function getPlan() {
            if (cmbPlan.GetText() != "")
                listPlan.AddItem(cmbPlan.GetText());
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel13" Text="生产线：" Width="50px"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox runat="server" ID="cmbPline" ClientInstanceName="cmbPline" Width="120px" oninit="cmbPlineCode_Init" SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td align="right">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="计划日期：从" Width="80px"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="datePlanDate" runat="server" Width="120px" 
                    DisplayFormatString="yyyy-MM-dd" oninit="datePlanDate_Init" EditFormat="Custom" 
                    EditFormatString="yyyy-MM-dd" >
                </dx:ASPxDateEdit>   
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="到" Width="20px"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="datePlanDateTo" runat="server" Width="120px" 
                    DisplayFormatString="yyyy-MM-dd"  EditFormat="Custom" 
                    EditFormatString="yyyy-MM-dd" oninit="datePlanDateTo_Init" >
                </dx:ASPxDateEdit>   
            </td>
            <td>
                <dx:ASPxButton ID="BtnQry" runat="server" Text="查询" AutoPostBack="false" Width="100px">
                    <ClientSideEvents Click="function(s,e){cmbPlan.PerformCallback();}" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <hr />
    <table>
        <tr>
            <td>
                <dx:ASPxLabel runat="server" ID="ASPxLabel11" Text="计划号：" Width="50px"></dx:ASPxLabel>
            </td>
            <td >
            <dx:ASPxComboBox runat="server" ID="cmbPlan" ClientInstanceName="cmbPlan" 
                    Width="260px" oncallback="cmbPlan_Callback">
                <ClientSideEvents SelectedIndexChanged="function(s,e){getPlan();}" />
            </dx:ASPxComboBox>
            </td>
            <td align="right">
                &nbsp;</td>
            <td >
                &nbsp;</td>
            <td align="right" >
                &nbsp;</td>
            <td >
                &nbsp;</td>
        </tr>
        </table>

                <dx:ASPxListBox ID="listPlan" ClientInstanceName="listPlan" runat="server" Width="100%">
                </dx:ASPxListBox>
    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="BtnClear" runat="server" Text="清空列表" AutoPostBack="false"  Width="100px">
                    <ClientSideEvents Click="function(s,e){listPlan.ClearItems();}" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="BtnQuery" runat="server" Text="生成移仓单"  AutoPostBack="false"  
                    Width="100px">
                    <ClientSideEvents Click="function(s,e){grid.PerformCallback();}" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="BtnExport" runat="server" Text="导出到Excel" 
                    AutoPostBack="false" Width="100px" onclick="BtnExport_Click"/>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td>
            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" SettingsPager-Mode="ShowAllRecords" KeyFieldName="ABOM_COMP"
                    Width="100%" oncustomcallback="ASPxGridView1_CustomCallback">
                                        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="ABOM_COMP" SummaryType="Count" DisplayFormat="总数={0}"/>
        </TotalSummary>
        <Settings ShowFooter="True" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="零件" FieldName="ABOM_COMP"   VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="工位" FieldName="ABOM_WKCTR" VisibleIndex="2" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="库位" FieldName="ABOM_KW" VisibleIndex="3" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="QTY1" VisibleIndex="4" Width="100px" />        
                </Columns> 
                <Settings ShowFilterRow="false" ShowGroupPanel="false" />
            </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
        GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    
    </form>
</body>
</html>
