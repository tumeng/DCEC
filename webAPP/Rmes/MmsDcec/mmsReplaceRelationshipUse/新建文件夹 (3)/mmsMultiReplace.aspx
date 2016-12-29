<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsMultiReplace.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsMultiReplace" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>多对多替换</title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td style="width:100px">
                <dx:ASPxLabel ID="Label1" runat="server" Text="选择输入分组号："></dx:ASPxLabel>
            </td>
            <td style="width:150px">
                <dx:ASPxComboBox ID="cmbGroup" runat="server" Width="100%">
                <ClientSideEvents SelectedIndexChanged="function(s,e){gridMaterial.PerformCallback();}" />
                </dx:ASPxComboBox>
            </td>
            <td style="width:100px">
                <dx:ASPxButton ID="btnSave" runat="server" onclick="btnSave_Click" Text="保存" Width="100%">
                <ClientSideEvents Click="function(s,e){gridMaterial.PerformCallback();}" />
                </dx:ASPxButton>
            </td>
            <td style="width:100px">
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="false" Text="已替换查询" Width="100%">
                <ClientSideEvents Click="function(s,e){gridReplaced.PerformCallback();}" />
                </dx:ASPxButton>
            </td>
        </tr>
        </table>

    <dx:ASPxGridView ID="gridMaterial" ClientInstanceName="gridMaterial" runat="server"
        oncustomcallback="ASPxGridView1_CustomCallback" Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn Caption="原零件" FieldName="OLDPART" VisibleIndex="0" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="OLDPART_NAME" VisibleIndex="1" Width="30%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="2" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="替换件" FieldName="NEWPART" VisibleIndex="3" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="NEWPART_NAME" VisibleIndex="4" Width="30%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="5" Width="10%"></dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowVerticalScrollBar="false" />
    </dx:ASPxGridView>
    <br />
    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="gridReplaced" runat="server" Width="100%" 
        oncustomcallback="ASPxGridView2_CustomCallback">
            <Columns>
                <dx:GridViewDataTextColumn Caption="原零件" FieldName="LJDM1" VisibleIndex="0" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="OLDPART_NAME" VisibleIndex="1" Width="30%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC1" VisibleIndex="2" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="替换件" FieldName="LJDM2" VisibleIndex="3" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="NEWPART_NAME" VisibleIndex="4" Width="30%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="5" Width="10%"></dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowVerticalScrollBar="false" />
    </dx:ASPxGridView>

    </form>
</body>
</html>
