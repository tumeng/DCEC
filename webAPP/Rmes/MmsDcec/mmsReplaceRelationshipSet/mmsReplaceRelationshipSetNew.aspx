<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsReplaceRelationshipSetNew.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipSet.mmsReplaceRelationshipSetNew" StylesheetTheme="Theme1" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>替换关系设定-新增</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function OnProductlineChanged(cmbPline) {
//            cmbPart2.PerformCallback(cmbPline.GetValue().toString());
        }
        function OnNoQAD(checkNoQAD) {
//            cmbPartNew.PerformCallback(CheckNoQAD.GetChecked());
        }
        function OnNoQAD2(checkNoQAD2) {
//            cmbPartNew2.PerformCallback(CheckNoQAD2.GetChecked());
        }
        function OnPart2Changed(s, e) {
            lstPtFrom.AddItem(cmbPart2.GetText().toString().toUpperCase());
            e.processOnServer = false;
        }
        function OnPartNew2Changed(s, e) {
            if (txtSl.GetText() == "") {
                alert("请填写零件数量!");
                return;
            }
            lstPtTo.AddItem(cmbPartNew2.GetText().toString().toUpperCase() + ";" + txtSl.GetText());
        }
        function DeleteConfig(e, flag) {
            ListConfig.ClearItems();
            e.processOnServer = false;
        }
        function AddConfig(e, flag) {
            if (flag == "single") {
                if (cmbSer.GetValue() == null) {
                    e.processOnServer = false;
                    return;
                }
                //不重复添加
                if(ListConfig.FindItemByText(cmbSer.GetValue().toString())==null)
                    ListConfig.AddItem(cmbSer.GetValue().toString());
                e.processOnServer = false;
            }
            if (flag == "multi") {
                if (ListConfig.GetItemCount() > 0) {
                    if (confirm("是否覆盖已经选择的SO?"))
                        ListConfig.PerformCallback("all");
                } else
                    ListConfig.PerformCallback("all");
                e.processOnServer = false;
            }
        }
        function AddSO(e, flag) {
            if (txtSo.GetText() != "")
                ListConfig.AddItem(txtSo.GetText());
            e.processOnServer = false;
        }
        function Confirm(s, e) {
            if (cmbPline.GetValue() == null) {
                alert("请选择生产线!");
                e.processOnServer = false;
                return;
            }
            if (ListConfig.GetItemCount() == 0) {
                alert("请选择SO!");
                e.processOnServer = false;
                return;
            }
            if (cmbPart.GetValue() == null) {
                alert("请选择原零件!");
                e.processOnServer = false;
                return;
            }
            if (cmbPartNew.GetValue() == null) {
                alert("请选择替换件!");
                e.processOnServer = false;
                return;
            }
            if (txtPe.GetValue() == null) {
                alert("请填写PE文件!");
                e.processOnServer = false;
                return;
            }
        }
        function Confirm2(s, e) {

            if (cmbPline.GetValue() == null) {
                alert("请选择生产线!");
                e.processOnServer = false;
                return;
            }
            if (ListConfig.GetItemCount() == 0) {
                alert("请选择SO!");
                e.processOnServer = false;
                return;
            }
            if (lstPtFrom.GetItemCount() == 0) {
                alert("请选择原零件!");
                e.processOnServer = false;
                return;
            }
            if (lstPtTo.GetItemCount() == 0) {
                alert("请选择替换件!");
                e.processOnServer = false;
                return;
            }
            if (txtPe2.GetValue() == null) {
                alert("请填写PE文件!");
                e.processOnServer = false;
                return;
            }
            //开始时间<结束时间
            if (DateFrom2.GetDate() > DateTo2.GetDate()) {
                alert("生效日期不能大于结束日期!");
                e.processOnServer = false;
                return;
            }
        }
    </script>
    <table style="width:100%;">
        <tr>
            <td style="width:40%">
                <table style="width:100%;">
                    <tr>
                        <td style="width:15%">
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Visible="false" Text="生产线:">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width:40%">
                            <dx:ASPxComboBox ID="cmbPline" runat="server" ClientInstanceName="cmbPline"  ClientVisible="false" SelectedIndex="0"
                                ValueType="System.String" Width="100%" oninit="cmbPline_Init">
                                <%--<ClientSideEvents ValueChanged="function(s,e){OnProductlineChanged(s);}" />--%>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width:20%">
                        </td>
                        <td align="right" style="width:25%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="系列:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtXl" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Config:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbSer" runat="server" ClientInstanceName="cmbSer" 
                                oninit="cmbSer_Init" ValueType="System.String" Width="100%" TabIndex="42">
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <dx:ASPxButton ID="cmdAddConfig" runat="server" Text="添加Config" Width="100%" 
                                TabIndex="40">
                                <ClientSideEvents Click="function(s,e){AddConfig(e,'single');}" />
                            </dx:ASPxButton>
                        </td>
                        <td align="right">
                            <dx:ASPxButton ID="cmdAllConfig" runat="server" Text="添加所有Config" Width="100%" 
                                TabIndex="2">
                            <ClientSideEvents Click="function(s,e){AddConfig(e,'multi');}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="SO:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtSo" runat="server" ClientInstanceName="txtSo" 
                                Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <dx:ASPxButton ID="cmdAddSo" runat="server" Text="添加SO" Width="100%" 
                                TabIndex="41">
                                <ClientSideEvents Click="function(s,e){AddSO(e,'so');}" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <dx:ASPxListBox ID="ListConfig" runat="server" ClientInstanceName="ListConfig" 
                                Height="300px" oncallback="ListConfig_Callback" Width="100%">
                            </dx:ASPxListBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:60%;vertical-align:top">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                    Width="100%">
                    <TabPages>
                        <dx:TabPage Text="一对一替换">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="width:5%">
                                            </td>
                                            <td style="width:10%">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="BOM零件:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width:25%">
                                            <dx:ASPxTextBox ID="cmbPart1" runat="server" ClientInstanceName="cmbPart" 
                                                    Width="100%">
                                             </dx:ASPxTextBox>
   <%--                                             <dx:ASPxComboBox ID="cmbPart" ClientInstanceName="cmbPart" runat="server" ValueType="System.String" 
                                                    Width="100%">
                                                </dx:ASPxComboBox>--%>
                                            </td>
                                            <td style="width:5%">
                                            </td>
                                            <td style="width:10%">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="替换零件:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width:25%">
                                            <dx:ASPxTextBox ID="cmbPartNew1" runat="server" ClientInstanceName="cmbPartNew" 
                                                    Width="100%">
                                             </dx:ASPxTextBox>
                                                <%--<dx:ASPxComboBox ID="cmbPartNew" runat="server" ClientInstanceName="cmbPartNew" 
                                                    OnCallback="cmbPartNew_Callback" ValueType="System.String" Width="100%">
                                                </dx:ASPxComboBox>--%>
                                            </td>
                                            <td>
                                                <dx:ASPxCheckBox ID="CheckNoQAD" runat="server" CheckState="Unchecked"  ClientVisible="false"
                                                    ClientInstanceName="CheckNoQAD" Text="非QAD零件">
                                                    <ClientSideEvents CheckedChanged="function(s,e){OnNoQAD(e);}" />
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td style="width:5%">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="依据文件:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtPe" runat="server" Width="100%" ClientInstanceName="txtPe">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生效日期:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="DateFrom" runat="server" OnInit="DateFrom_Init" 
                                                    Width="100%">
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="结束日期:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="DateTo" runat="server" OnInit="DateTo_Init" Width="100%">
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="6">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="6">
                                        <hr />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <dx:ASPxButton ID="BtnConfirm" runat="server" OnClick="BtnConfirm_Click" 
                                                    Text="确定" Width="100px">
                                                    <ClientSideEvents Click="function(s,e){return Confirm(s,e);}" />
                                                </dx:ASPxButton>
                                            </td>
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
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="多对多替换">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="零件数量:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtSl" runat="server" ClientInstanceName="txtSl" 
                                                    Width="100%">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width:5%">
                                                &nbsp;</td>
                                            <td style="width:10%">
                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="BOM零件:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width:25%">
                                                  <dx:ASPxTextBox ID="cmbPart21" runat="server" ClientInstanceName="cmbPart2" 
                                                    Width="100%">
                                                    <ClientSideEvents KeyDown="function(s,e){if (e.htmlEvent.keyCode == 13){cmbPart2.Focus();OnPart2Changed(s,e);} }"/>
                                                </dx:ASPxTextBox>
                                                <%--<dx:ASPxComboBox ID="cmbPart2" runat="server" ClientInstanceName="cmbPart2" 
                                                    OnCallback="cmbPart2_Callback" ValueType="System.String" Width="100%">
                                                    <ClientSideEvents ValueChanged="function(s,e){OnPart2Changed(s,e);}" />
                                                </dx:ASPxComboBox>--%>
                                            </td>
                                            <td style="width:5%">
                                                &nbsp;</td>
                                            <td style="width:10%">
                                                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="替换零件:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width:25%">
                                              <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" ClientInstanceName="cmbPartNew2" 
                                                    Width="100%">
                                                    <ClientSideEvents KeyDown="function(s,e){if (e.htmlEvent.keyCode == 13){cmbPartNew2.Focus();OnPartNew2Changed(s,e);} }"/>
                                                </dx:ASPxTextBox>
<%--                                                <dx:ASPxComboBox ID="cmbPartNew2" runat="server" 
                                                    ClientInstanceName="cmbPartNew2" Height="20px" 
                                                    OnCallback="cmbPartNew2_Callback" ValueType="System.String" Width="100%">
                                                    <ClientSideEvents ValueChanged="function(s,e){OnPartNew2Changed(s,e);}" />
                                                </dx:ASPxComboBox>--%>
                                            </td>
                                            <td>
                                                <dx:ASPxCheckBox ID="CheckNoQAD2" runat="server" CheckState="Unchecked"  ClientVisible="false"
                                                    ClientInstanceName="CheckNoQAD2" Text="非QAD零件">
                                                    <ClientSideEvents CheckedChanged="function(s,e){OnNoQAD2(e);}" />
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td style="width:5%">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width:5%">
                                            </td>
                                            <td style="width:10%">
                                            </td>
                                            <td style="width:25%">
                                                <dx:ASPxListBox ID="lstPtFrom" runat="server" ClientInstanceName="lstPtFrom" 
                                                    Height="200px" Width="100%">
                                                </dx:ASPxListBox>
                                            </td>
                                            <td style="width:5%">
                                            </td>
                                            <td style="width:10%">
                                            </td>
                                            <td style="width:25%">
                                                <dx:ASPxListBox ID="lstPtTo" runat="server" ClientInstanceName="lstPtTo" 
                                                    Height="200px" Width="100%">
                                                </dx:ASPxListBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width:5%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="生效日期:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="DateFrom2" ClientInstanceName="DateFrom2"  runat="server" OnInit="DateFrom2_Init" 
                                                    Width="100%">
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="结束日期:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="DateTo2" ClientInstanceName="DateTo2" runat="server" OnInit="DateTo2_Init" Width="100%">
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="依据文件:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtPe2" runat="server" ClientInstanceName="txtPe2" Width="100%">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="6">
                                        <hr />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <dx:ASPxButton ID="BtnConfirm2" runat="server" OnClick="BtnConfirm2_Click" 
                                                    Text="确定" Width="100px">
                                                    <ClientSideEvents Click="function(s,e){return Confirm2(s,e);}" />
                                                </dx:ASPxButton>
                                            </td>
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
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </td>
        </tr>
        <tr>
            <td style="width:40%">
                <dx:ASPxButton ID="cmdRemoveAll" runat="server" Text="移除所有" Width="120px">
                        <ClientSideEvents Click="function(s,e){DeleteConfig(e);}" />
                    </dx:ASPxButton>
            </td>
            <td style="width:60%;vertical-align:top">
            </td>
        </tr>
    </table>


    </form>
</body>
</html>
