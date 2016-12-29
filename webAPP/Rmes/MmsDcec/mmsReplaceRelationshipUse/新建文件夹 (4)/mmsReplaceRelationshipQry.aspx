<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsReplaceRelationshipQry.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsReplaceRelationshipQry" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看全部替换关系</title>
</head>
<body>
    <form id="form1" runat="server">

    <table>
        <tr style="width:100px;">
            <td>
                <dx:ASPxButton ID="CmdOne" runat="server" Text="一对一替换" Width="100%" 
                    onclick="CmdOne_Click">
                </dx:ASPxButton>
            </td>
            <td style="width:100px;">
                <dx:ASPxButton ID="CmdMuti" runat="server" Text="多对多替换" Width="100%" 
                    onclick="CmdMuti_Click">
                </dx:ASPxButton>
            </td>
            <td style="width:120px;">
                <dx:ASPxButton ID="cmdOneExpire" runat="server" Text="到期一对一替换" Width="100%" 
                    onclick="cmdOneExpire_Click">
                </dx:ASPxButton>
            </td>
            <td style="width:120px;">
                <dx:ASPxButton ID="cmdMutiExpire" runat="server" Text="到期多对多替换" Width="100%" 
                    onclick="cmdMutiExpire_Click">
                </dx:ASPxButton>
            </td>
            <td style="width:80px;">
                <dx:ASPxButton ID="CmdExport" runat="server" Text="导出" Width="100%" 
                    onclick="CmdExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" KeyFieldName="SO">
    <Columns>
        <dx:GridViewDataTextColumn Caption="机型" FieldName="SO" VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="原零件" FieldName="OLDPART" VisibleIndex="2" Width="80px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="替换件" FieldName="NEWPART" VisibleIndex="3" Width="80px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="PE文件" FieldName="PEFILE"  VisibleIndex="5" Width="120px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="制定人" FieldName="CREATEUSER" VisibleIndex="6" Width="80px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="开始时间" FieldName="USETIME" VisibleIndex="7" Width="120px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="结束时间" FieldName="ENDTIME" VisibleIndex="8" Width="120px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="制定时间" FieldName="CREATETIME" VisibleIndex="10" Width="120px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="类型" FieldName="SETTYPE" VisibleIndex="12" Width="120px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="组号" FieldName="THGROUP" VisibleIndex="13" Width="120px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="14" Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="SITE" Visible="false"></dx:GridViewDataTextColumn>
    </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
        GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>

    </form>
</body>
</html>
