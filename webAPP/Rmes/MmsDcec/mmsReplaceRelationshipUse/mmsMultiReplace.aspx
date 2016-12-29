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
    <script type="text/javascript">
        var lastDept = null;
        function OnGWChanged(cmbGW) {
            if (gridReplaced.GetEditor("GXMC1").InCallback())
                lastDept = cmbGW.GetValue().toString();
            else {
                gridReplaced.GetEditor("GXMC1").PerformCallback(cmbGW.GetValue().toString());
            }
        }
    </script>
    <table>
        <tr>
            <td style="width:100px">
                <dx:ASPxLabel ID="Label1" runat="server" Text="选择输入分组号："></dx:ASPxLabel>
            </td>
            <td style="width:150px">
                <dx:ASPxComboBox ID="cmbGroup" runat="server" Width="100%">
                <ClientSideEvents SelectedIndexChanged="function(s,e){gridMaterial.PerformCallback();gridReplaced.PerformCallback();}" />
                </dx:ASPxComboBox>
            </td>
            <td style="width:100px">
                <dx:ASPxButton ID="btnSave" runat="server" onclick="btnSave_Click" AutoPostBack="false" Text="保存" Width="100%">
                <ClientSideEvents Click="function(s,e){gridMaterial.PerformCallback();}" />
                </dx:ASPxButton>
            </td>
            <td style="width:100px">
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="false" Text="已替换查询" 
                    Width="100%" onclick="btnQuery_Click">
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
     <table  style="width:100%;">
    <dx:ASPxGridView ID="gridMaterial" ClientInstanceName="gridMaterial" runat="server" KeyFieldName="OLDPART;NEWPART"
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
            <ClientSideEvents EndCallback="function(s,e){
                        gridReplaced.PerformCallback();
                    }" />
    </dx:ASPxGridView>

    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="gridReplaced" KeyFieldName="ROWID" 
        runat="server" Width="100%" onrowupdating="ASPxGridView2_RowUpdating" 
        oncelleditorinitialize="ASPxGridView2_CellEditorInitialize"  
             oncustomcallback="ASPxGridView2_CustomCallback" onhtmlrowprepared="ASPxGridView2_HtmlRowPrepared"
        >
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="80px">
                    <EditButton Visible="True" Text="修改"></EditButton>
                    <ClearFilterButton Visible="True" Text="清除"></ClearFilterButton>
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="原零件" FieldName="ROWID" VisibleIndex="0" Visible="false" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="原零件" FieldName="LJDM1" VisibleIndex="0" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="OLDPART_NAME" VisibleIndex="1" Width="20%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="GWMC" Caption="工位" VisibleIndex="2" Width="8%">
                    <PropertiesComboBox TextField="location_code" ValueField="location_code" EnableSynchronization="False"></PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="替换件" FieldName="LJDM2" VisibleIndex="3" Width="10%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="NEWPART_NAME" VisibleIndex="4" Width="20%"></dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="GWMC1" Caption="工位" VisibleIndex="5" Width="8%">
                    <PropertiesComboBox TextField="location_code" ValueField="location_code" EnableSynchronization="False">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { OnGWChanged(s); }"></ClientSideEvents>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="GXMC1" Caption="工序" VisibleIndex="6" Width="8%">
                    <PropertiesComboBox TextField="process_code" ValueField="process_code" EnableSynchronization="False"></PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="7" Width="5%"></dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="false" ShowGroupPanel="false" ShowVerticalScrollBar="false" />
            <SettingsEditing Mode="Inline" />
    </dx:ASPxGridView>
    </table>
    </form>
</body>
</html>
