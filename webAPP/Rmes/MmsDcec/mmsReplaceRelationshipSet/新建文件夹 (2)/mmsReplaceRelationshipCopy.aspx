<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsReplaceRelationshipCopy.aspx.cs"
    Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipSet.mmsReplaceRelationshipCopy"
    StylesheetTheme="Theme1" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>替换关系复制</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function initSO() {
//            cmbFromSo.PerformCallback(cmbPline.GetValue().toString());
        }
        function checkData(s, e) {
            if (grid.GetSelectedRowCount() == 0) {
                alert("请选择要复制的相关内容!");
                e.processOnServer = false;
                return;
            }
            if (ListToSo.GetItemCount() == 0) {
                alert("请填写新SO/CONFIG!");
                e.processOnServer = false;
            }
        }
        function checkSO(s, e) {
            if (cmbFromSo.GetText() == '') { alert('请先输入SO/机型!'); return; }

            var webFileUrl = "?SO=" + cmbFromSo.GetText() + " &opFlag=checkSO";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            if (result == "Fail") {
                alert("该SO/机型不存在，请重新输入！");

                return;
            }
        }
        function checkSO2(s, e) {
            if (cmbToSO.GetText() == '') { alert('请先输入SO/机型!'); return; }

            var webFileUrl = "?SO=" + cmbToSO.GetText() + " &opFlag=checkSO2";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            if (result == "Fail") {
                alert("该SO/机型不存在，请重新输入！");
                return;
            }
            ListToSo.AddItem(cmbToSO.GetText()); 
        }
    </script>
    <table style="width: 480px;">
        <tr>
            <td style="width: 80px">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="生产线：">
                </dx:ASPxLabel>
            </td>
            <td style="width: 140px">
                <dx:ASPxComboBox ID="cmbPline" ClientInstanceName="cmbPline" runat="server" Width="140px"
                    OnInit="cmbPline_Init">
                    <ClientSideEvents SelectedIndexChanged="function(s,e){initSO();}" />
                </dx:ASPxComboBox>
            </td>
            <td style="width: 100px">
            </td>
            <td style="width: 100px">
            </td>
        </tr>
        <tr>
            <td style="width: 80px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="SO/CONFIG：">
                </dx:ASPxLabel>
            </td>
            <td style="width: 200px">
                <%-- <dx:ASPxComboBox ID="cmbFromSo" ClientInstanceName="cmbFromSo" runat="server" Width="100%" 
                    oncallback="cmbFromSo_Callback">
                </dx:ASPxComboBox>--%>
                <dx:ASPxTextBox ID="txtFromSo" runat="server" Width="140px" ClientInstanceName="cmbFromSo">
                    <ClientSideEvents TextChanged="function(s,e){checkSO(s, e);
                        
                    }" />
                </dx:ASPxTextBox>
            </td>
            <td style="width: 100px">
                <dx:ASPxButton ID="cmdOne" runat="server" Width="100%" AutoPostBack="false" Text="一对一替换">
                    <ClientSideEvents Click="function(s,e){
                        if(cmbPline.GetText()=='') {alert('请选择生产线!');return;} 
                        if(cmbFromSo.GetText()=='') {alert('请先输入SO/机型!');return;}
                        ListToSo.ClearItems();
                        grid.PerformCallback('one');
                    }" />
                </dx:ASPxButton>
            </td>
            <td style="width: 100px">
                <dx:ASPxButton ID="cmdMuti" runat="server" Width="100%" Text="多对多替换" AutoPostBack="false">
                    <ClientSideEvents Click="
                        function(s,e){if(cmbPline.GetText()=='') {alert('请选择生产线!');return;}
                        if(cmbFromSo.GetText()=='') {alert('请先输入SO/机型!');return;}
                        ListToSo.ClearItems();
                        grid.PerformCallback('multi');
                    }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right">
                <dx:ASPxButton ID="CmdCopy" runat="server" Text="复制" Width="100px" OnClick="CmdCopy_Click">
                    <ClientSideEvents Click="function(s,e){checkData(s,e);}" />
                </dx:ASPxButton>
            </td>
            <td align="left" colspan="3">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="新SO/CONFIG：">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <%-- <dx:ASPxComboBox ID="cmbToSO" ClientInstanceName="cmbToSO" runat="server" Width="150px"
                                OnCallback="cmbFromSo_Callback" OnInit="cmbToSO_Init">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){ListToSo.AddItem(cmbToSO.GetValue());}" />
                            </dx:ASPxComboBox>--%>
                            <dx:ASPxTextBox ID="txtToSO" runat="server"  Width="150px" ClientInstanceName="cmbToSO">
                                <ClientSideEvents TextChanged="function(s,e){ checkSO2(s, e);  }" />
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top" style="width: 80%">
                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="100%"
                    AutoGenerateColumns="False" KeyFieldName="SO" OnCustomCallback="ASPxGridView1_CustomCallback">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="35px">
                            <HeaderTemplate>
                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                                    ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }" />
                            </HeaderTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="CONFIG或SO" FieldName="SO" VisibleIndex="1" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="原零件" FieldName="OLDPART" VisibleIndex="2" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="替换件" FieldName="NEWPART" VisibleIndex="3" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="4" Width="60px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="PE代用文件" FieldName="PEFILE" VisibleIndex="5" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="用户" FieldName="CREATEUSER" VisibleIndex="6" Width="80px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="开始时间" FieldName="USETIME" VisibleIndex="7" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="结束时间" FieldName="ENDTIME" VisibleIndex="8" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="分组" FieldName="THGROUP" VisibleIndex="9" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="维护时间" FieldName="CREATETIME" VisibleIndex="10"
                            Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="系列" FieldName="XL" VisibleIndex="11" Width="60px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SITE" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SETTYPE" Visible="false">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowHorizontalScrollBar="True" ShowVerticalScrollBar="false" />
                </dx:ASPxGridView>
            </td>
            <td valign="top" colspan="3" style="width: 20%">
                <dx:ASPxListBox ID="ListToSo" ClientInstanceName="ListToSo" runat="server" Height="400px"
                    Width="100%">
                </dx:ASPxListBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
