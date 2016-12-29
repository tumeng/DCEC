<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsMaterialRequisition.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsMaterialRequisition.mmsMaterialRequisition" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function getAllPlan() {
            listPlan.ClearItems();
            for (i = 0; i < cmbPlan.GetItemCount(); i++) {
                if (listPlan.FindItemByText(cmbPlan.GetItem(i).text) == null)
                    listPlan.AddItem(cmbPlan.GetItem(i).text);
            }
        }
        function getPlan() {
            if (cmbPlan.GetText() != "") {
                if (listPlan.FindItemByText(cmbPlan.GetText()) == null)
                    listPlan.AddItem(cmbPlan.GetText());
            }
            var str = cmbPlan.GetText();
            var strs = new Array(); //定义一数组 
            strs = str.split(";"); //字符分割
            var planCode = strs[0];
            var so = strs[1];
            var qty = strs[2];

            txtSo.SetText(so);
            txtSo.SetEnabled(false);
            txtQTY.SetText(qty);
            txtQTY.SetEnabled(false);
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
                <dx:ASPxComboBox runat="server" ID="cmbPline" ClientInstanceName="cmbPline" Width="120px" SelectedIndex="0" oninit="cmbPlineCode_Init">
                </dx:ASPxComboBox>
            </td>
            <td align="right">
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产日期：" Width="60px"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="datePlanDate" runat="server" Width="120px" 
                    DisplayFormatString="yyyy-MM-dd" oninit="datePlanDate_Init" EditFormat="Custom" 
                    EditFormatString="yyyy-MM-dd" >
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
                <dx:ASPxButton ID="BtnAll" runat="server" Text="全选" AutoPostBack="false" 
                    Width="60px">
                    <ClientSideEvents Click="function(s,e){getAllPlan();}" />
                </dx:ASPxButton>
            </td>
            <td align="right">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="SO：" Width="40px"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtSo" ClientInstanceName="txtSo" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td align="right" >
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="数量：" Width="40px"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtQTY" ClientInstanceName="txtQTY" runat="server" 
                    Width="120px" ></dx:ASPxTextBox>
            </td>
        </tr>
        </table>
        <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="保管员：" Width="50px"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtStoreman" ClientInstanceName="txtStoreman11" runat="server" Width="120px">
                <ClientSideEvents TextChanged="function(s,e){
                                txtStoreman21.SetValue(txtStoreman11.GetValue());
                            }" />
                </dx:ASPxTextBox>
            </td>
            <td align="center" >
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="到"></dx:ASPxLabel>
            </td>
            <td >
                <dx:ASPxTextBox ID="txtStoreman2" ClientInstanceName="txtStoreman21" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td align="right" style="width:60px">
                &nbsp;</td>
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
                <dx:ASPxCheckBox ID="CheckSn" ClientInstanceName="CheckSn" runat="server" Text="按台份">
                    <ClientSideEvents CheckedChanged="function(s,e){
                        if(CheckSn.GetChecked()) 
                            txtQTY.SetEnabled(true);
                        else
                            txtQTY.SetEnabled(false);
                        listPlan.ClearItems();}" />
                </dx:ASPxCheckBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="11">
                <dx:ASPxListBox ID="listPlan" ClientInstanceName="listPlan" runat="server" Width="100%">
                </dx:ASPxListBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="BtnClear" runat="server" Text="清空列表" AutoPostBack="false"  Width="100px">
                    <ClientSideEvents Click="function(s,e){listPlan.ClearItems();}" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="BtnQuery" runat="server" Text="生成领料单"  AutoPostBack="false"  
                    Width="100px">
                    <ClientSideEvents Click="function(s,e){grid.PerformCallback();}" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="BtnExport" runat="server" Text="导出到Excel" 
                    AutoPostBack="false" Width="100px" onclick="BtnExport_Click"/>
            </td>
            <td>
                <dx:ASPxButton ID="BtnDetail" runat="server" Text="领料单明细" AutoPostBack="false" Width="100px">
                <ClientSideEvents Click="function(s,e){window.open('mmsMaterialRequisitionDetail.aspx');}" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td>
            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="LJDM" SettingsPager-Mode="ShowAllRecords" 
                    Width="100%" oncustomcallback="ASPxGridView1_CustomCallback" onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" >
         <TotalSummary>
            <dx:ASPxSummaryItem FieldName="LJDM" SummaryType="Count" DisplayFormat="总数={0}"/>
            <dx:ASPxSummaryItem FieldName="LJSL1" SummaryType="Sum" DisplayFormat="总数={0}"/>
        </TotalSummary>
        <Settings ShowFooter="True" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="保管员" FieldName="LJBGY"   VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM" VisibleIndex="2" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC" VisibleIndex="3" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="工位" FieldName="LJGW" VisibleIndex="4" Visible="false" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="发料工位" FieldName="FLGW" VisibleIndex="5" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="库位" FieldName="LJDD" VisibleIndex="6" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="LJSL1" VisibleIndex="16" Width="60px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="替换零件代码" FieldName="THLJDM" VisibleIndex="17" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="原零件代码" FieldName="OLDLJDM" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="站点发料" FieldName="FLGW1" VisibleIndex="19"  Width="80px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="备注" FieldName="BZ" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="供应商" FieldName="LJGYS" VisibleIndex="19"  Width="100px"></dx:GridViewDataTextColumn>
        
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
