<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsConfirmOneReplace.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsConfirmOneReplace" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>确认一对一替换</title>
</head>
<body>
    <form id="form1" runat="server">

    <table style="width:100%;">
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="日期从：">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="DateFrom" runat="server" DisplayFormatString="yyyy-MM-dd">
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="计划号：">
                </dx:ASPxLabel>
                </td>
            <td>
                <dx:ASPxTextBox ID="txtPlanCode" runat="server" Width="170px">
                </dx:ASPxTextBox>
                </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="零件号：">
                </dx:ASPxLabel>
                </td>
            <td>
                <dx:ASPxTextBox ID="txtOldPart" runat="server" Width="170px">
                </dx:ASPxTextBox>
                </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="工位：">
                </dx:ASPxLabel>
                </td>
            <td>
                <dx:ASPxTextBox ID="txtLocationCode" runat="server" Width="185px">
                </dx:ASPxTextBox>
                </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="到：">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="DateTo" runat="server" DisplayFormatString="yyyy-MM-dd">
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="SO号：">
                </dx:ASPxLabel>
                </td>
            <td>
                <dx:ASPxTextBox ID="txtSO" runat="server" Width="170px">
                </dx:ASPxTextBox>
                </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="替换号：">
                </dx:ASPxLabel>
                </td>
            <td>
                <dx:ASPxTextBox ID="txtNewPart" runat="server" Width="170px">
                </dx:ASPxTextBox>
                </td>
            <td>
                &nbsp;</td>
            <td>
                <table cellpadding="0" cellspacing="0" >
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="false" Width="80px" Text="查询">
                            <ClientSideEvents Click="function(s,e){grid.PerformCallback('');}" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width:5px"></td>
                        <td>
                            <dx:ASPxButton ID="btnExport" runat="server" Width="100px" Text="导出到Excel" 
                                onclick="btnExport_Click">
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    </table>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <dx:ASPxGridView ID="grid" runat="server" Width="100%" KeyFieldName="ROWID" SettingsPager-Mode="ShowAllRecords"
        oncustomcallback="grid_CustomCallback">
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                        ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="计划号" FieldName="ROWID" VisibleIndex="0" Visible="false" Width="100px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划号" FieldName="JHDM" VisibleIndex="0" Width="100px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO号" FieldName="SO" VisibleIndex="2" Width="80px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件号" FieldName="LJDM1" VisibleIndex="3" Width="80px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="替换零件号" FieldName="LJDM2" VisibleIndex="4" Width="100px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位名称" FieldName="GWMC" VisibleIndex="5" Width="100px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="录入员工" FieldName="YGMC" VisibleIndex="6" Width="100px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="录入时间" FieldName="LRSJ" VisibleIndex="7" Width="150px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="是否确认" FieldName="ISTRUE" VisibleIndex="8" Width="80px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="现场提示" FieldName="BZ" VisibleIndex="14"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="替换数量" FieldName="THSL" VisibleIndex="15" Width="80px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="确认人" FieldName="QRYGMC" VisibleIndex="16" Width="80px"></dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="false" />
    </dx:ASPxGridView>

    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="btnDelete" runat="server" AutoPostBack="false" 
                    Width="80px" Text="删除" onclick="btnDelete_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnConfirm" runat="server" AutoPostBack="false" 
                    Width="80px" Text="确认" onclick="btnConfirm_Click" >
                </dx:ASPxButton>
            </td>
            <td></td>
        </tr>
        </table>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
        GridViewID="grid">
    </dx:ASPxGridViewExporter>

    </form>
    </body>
</html>
