<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Exp2000.aspx.cs" Inherits="Rmes.WebApp.Rmes.Exp.Exp2000.Exp2000" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v11.1.Web, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table >
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
        <td width="80px"><dx:ASPxLabel ID="ASPxLabel1" runat ="server" Text="日期："></dx:ASPxLabel></td>
        <td width="5px"></td>
        <td width="190px">
            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="180px">
            </dx:ASPxDateEdit>
        </td>
            <td width="120px"><dx:ASPxButton ID="submit" runat="server" Text="查询" onclick="submit_Click"></dx:ASPxButton></td>
    </tr>

    <tr>
    <td colspan="13" align="center">
    <dx:ReportToolbar ID="ReportToolbar1" runat="server" 
        ReportViewerID="ReportViewer1" ShowDefaultButtons="False" 
            style="margin-left: 0px" Width="100%">
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
                <elements>
                    <dx:ListElement Value="xlsx" />
                    <dx:ListElement Value="pdf" />
                    <dx:ListElement Value="xls" />
                    
                    <dx:ListElement Value="rtf" />
                    <dx:ListElement Value="mht" />
                    <dx:ListElement Value="html" />
                    <dx:ListElement Value="txt" />
                    <dx:ListElement Value="csv" />
                    <dx:ListElement Value="png" />
                </elements>
            </dx:ReportToolbarComboBox>
        </Items>
        <styles>
            <LabelStyle><Margins MarginLeft='3px' MarginRight='3px' /></LabelStyle>
        </styles>
    </dx:ReportToolbar>
    </td>
    </tr>
    

    <tr>
    <td colspan="13" align="center">
    <dx:ReportViewer ID="ReportViewer1" runat="server"        
        ReportName="Rmes.WebApp.Rmes.Exp.Exp2000.Report_Exp2000_1" 
            style="margin-right: 0px">
    </dx:ReportViewer>
    </td>
</tr>
</table>
</asp:Content>
