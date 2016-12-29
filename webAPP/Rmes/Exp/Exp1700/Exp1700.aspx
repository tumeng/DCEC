<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Exp1700.aspx.cs" Inherits="Rmes.WebApp.Rmes.Exp.Exp1700.Exp1700" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraReports.v11.1.Web, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
    <tr>
        <td width="80px"><dx:ASPxLabel ID="l_DEPT" runat ="server" Text="部门："></dx:ASPxLabel></td>
        <td width="5px"></td>
        <td width="160px"><dx:ASPxComboBox ID="com_DEPT" runat="server" Width="160px" 
                AutoPostBack="True" onselectedindexchanged="com_DEPT_SelectedIndexChanged"></dx:ASPxComboBox></td>
        <td width="5px"></td>
        <td width="80px"><dx:ASPxLabel ID="ASPxLabel2" runat ="server" Text="生产单元："></dx:ASPxLabel></td>
        <td width="5px"></td>
        <td width="160px"><dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="160px"></dx:ASPxComboBox></td>
        <td width="5px"></td>
        
            <td width="120px"><dx:ASPxButton ID="ASPxButton1" runat="server" Text="查询" onclick="ASPxButton1_Click"></dx:ASPxButton></td>
            <td>
        <%--<dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />--%>
    </td>
    </tr>
    </table> 
    <div>
        <%--<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
            Width="983px">
            <Columns>
                <dx:GridViewDataTextColumn Caption="物料编码" FieldName="ITEM_CODE" 
                    VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" 
                    VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料数量" FieldName="ITEM_QTY" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn Caption="操作时间" FieldName="WORK_TIME" 
                    VisibleIndex="3">
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataComboBoxColumn Caption="操作员" FieldName="CREATE_USER" 
                    VisibleIndex="4">
                    <PropertiesComboBox ValueType="System.String">
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="订单号" FieldName="ORDER_CODE" 
                    VisibleIndex="5">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>--%>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
        <dx:ReportViewer ID="ReportViewer1" runat="server" 
            
            ReportName="Rmes.WebApp.Rmes.Exp.Exp1700.Report_Exp1700">
        </dx:ReportViewer>
    </div>
    <dx:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" 
        ReportViewerID="ReportViewer1">
        <Items>
            <dx:ReportToolbarButton ItemKind="Search" />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton ItemKind="PrintReport" />
            <dx:ReportToolbarButton ItemKind="PrintPage" />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
            <dx:ReportToolbarLabel ItemKind="PageLabel" />
            <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
            </dx:ReportToolbarComboBox>
            <dx:ReportToolbarLabel ItemKind="OfLabel" />
            <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
            <dx:ReportToolbarButton ItemKind="NextPage" />
            <dx:ReportToolbarButton ItemKind="LastPage" />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton ItemKind="SaveToDisk" />
            <dx:ReportToolbarButton ItemKind="SaveToWindow" />
            <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                <Elements>
                    <dx:ListElement Value="pdf" />
                    <dx:ListElement Value="xls" />
                    <dx:ListElement Value="xlsx" />
                    <dx:ListElement Value="rtf" />
                    <dx:ListElement Value="mht" />
                    <dx:ListElement Value="html" />
                    <dx:ListElement Value="txt" />
                    <dx:ListElement Value="csv" />
                    <dx:ListElement Value="png" />
                </Elements>
            </dx:ReportToolbarComboBox>
        </Items>
        <Styles>
            <LabelStyle>
            <Margins MarginLeft="3px" MarginRight="3px" />
            </LabelStyle>
        </Styles>
    </dx:ReportToolbar>
    </form>
</body>
</html>
