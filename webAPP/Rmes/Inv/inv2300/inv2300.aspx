<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv2300.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv2300.inv2300" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="btnNewBill" runat="server" Text="手动建立要料单" AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) { popup.Show(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                |
            </td>
            <td>
                <dx:ASPxButton ID="btnReset" runat="server" Text="重置料单状态" AutoPostBack="true" OnClick="btnReset_Click">

                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="TMBH" Width="100%">

    <SettingsBehavior ColumnResizeMode="Control"/>
        <Columns>
            <dx:GridViewCommandColumn Width="40">
                <ClearFilterButton Text="清除" Visible="true">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataComboBoxColumn Caption="合同号" FieldName="LLGCH" Width="80px" />
            <dx:GridViewDataComboBoxColumn Caption="工作号" FieldName="LLGZH" Width="80px" />
            <dx:GridViewDataComboBoxColumn Caption="组件代号" FieldName="LLZJDH" Width="100px" />
            <dx:GridViewDataTextColumn Caption="项目代号" FieldName="LLXMDH" Width="150px" />
            <dx:GridViewDataTextColumn Caption="项目名称" FieldName="LLXMMC" Width="200px" CellStyle-Wrap="false" />
            <dx:GridViewDataTextColumn Caption="领料" FieldName="LLSL" Width="50px" />
            <dx:GridViewDataTextColumn Caption="实收" FieldName="YSSL" Width="50px" />
            <dx:GridViewDataTextColumn Caption="需求日期" FieldName="LLRQ" Width="75px" CellStyle-Wrap="false" />
            <dx:GridViewDataTextColumn Caption="单据日期" FieldName="LLCJRQ" Width="135px" />
            <dx:GridViewDataComboBoxColumn Caption="领料组" FieldName="LLZPXZ" Width="100px" />
            <dx:GridViewDataComboBoxColumn Caption="组长" FieldName="LLXZZZ" Width="100px" />
            <dx:GridViewDataComboBoxColumn Caption="状态" FieldName="LLBS" Width="70px" />
            <dx:GridViewDataTextColumn Caption="合并计划" FieldName="PLAN_CODE" Width="300px" />
            
        </Columns>
    </dx:ASPxGridView>

    <dx:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server"
        Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
        Width="300" Height="220" HeaderText="在MES中手动添加要料单，必须输入正确的临改单号" AllowDragging="True"
        EnableViewState="False">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:Panel ID="panel1" runat="server">
                    <table>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="**临改单号**" Font-Bold="true" ForeColor="Red"
                                    Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="LLPGDH" Width="280">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="追溯修改源头，十分重要" ForeColor="Gray"
                                    Width="280">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择工程" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox runat="server" ClientInstanceName="cbgch" ID="LLGCH" Width="280">
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择工作单" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox runat="server" ID="LLGZH" ClientInstanceName="cbgzh" Width="280">
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="领用项目代码" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="LLXMDH" Width="280">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="领用项目类型" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="LLXMLX" Width="280">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="系统根据此号自动检索物料，请勿输错" ForeColor="Gray">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="领用项目名称" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="LLXMMC" Width="280">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="领用数量" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox runat="server" ID="LLSL" Width="280" MaskSettings-Mask="<1..1..99999999>">
                                    <MaskSettings Mask="&lt;1..1..99999999&gt;"></MaskSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择领用小组" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox runat="server" ID="LLZPXZ" Width="280">
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="领用日期" Width="100">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxDateEdit runat="server" ID="LYRQ" Width="280">
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <hr />
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <dx:ASPxButton runat="server" ID="btnCancel" Text="取消">
                                                <ClientSideEvents Click="function(s,e) { popup.Hide(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    <script type="text/javascript">
        var initWorkCode = function (proejctcode) {
            cbgzh.ClearItems();
            cbgzh.PerformCallback(proejctcode);
        };
</script>
</asp:Content>
