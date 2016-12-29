<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsMaterialRequisitionDetail.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsMaterialRequisition.mmsMaterialRequisitionDetail" StylesheetTheme="Theme1" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" KeyFieldName="JHDM" SettingsPager-Mode="ShowAllRecords">
             <TotalSummary>
            <dx:ASPxSummaryItem FieldName="LJDM" SummaryType="Count" DisplayFormat="总数={0}"/>
        </TotalSummary>
        <Settings ShowFooter="True" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="2" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="保管员" FieldName="LJBGY"   VisibleIndex="3" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM" VisibleIndex="4" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC" VisibleIndex="5" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="工位" FieldName="LJGW" VisibleIndex="6" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="发料工位" FieldName="FLGW" VisibleIndex="7" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="库位" FieldName="LJDD" VisibleIndex="8" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="LJSL" VisibleIndex="16" Width="60px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="替换零件代码" FieldName="THLJDM" VisibleIndex="17" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="原零件代码" FieldName="OLDLJDM" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="站点发料" FieldName="FLGW1" VisibleIndex="19"  Width="80px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="备注" FieldName="BZ" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="供应商" FieldName="LJGYS" VisibleIndex="19"  Width="100px"></dx:GridViewDataTextColumn>
        
                </Columns> 
                <Settings ShowHorizontalScrollBar="true" />
    </dx:ASPxGridView>
    </form>
</body>
</html>
