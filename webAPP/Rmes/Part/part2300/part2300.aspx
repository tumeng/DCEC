<%@ Page Title="线边库存查询" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part2300.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part2300.part2300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--线边库存查询 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td style="height: 30px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="查询条件">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboGzdd" runat="server" ValueField="PLINE_CODE" TextField="PLINE_NAME"
                    Width="120px" ClientInstanceName="plineC">
                    <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocationBGY(); }" />
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="工位:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbLocation" ClientInstanceName="cmbLocationC" runat="server"
                    Width="120px" OnCallback="cmbLocation_Callback">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="保管员:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbBgy" ClientInstanceName="cmbBgyC" runat="server" Width="120px"
                    OnCallback="cmbBgy_Callback">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="零件号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtMatCode" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxCheckBox runat="server" Text="忽略供应商" ID="cbGys">
                </dx:ASPxCheckBox>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" Text="查询">
                    <ClientSideEvents Click="function(s, e){
                        grid.PerformCallback();
                        }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExportPlan" runat="server" Text="导出" UseSubmitBehavior="False"
                    OnClick="btnXlsExportPlan_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ROWID" ClientInstanceName="grid">
        <Columns>
            <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" VisibleIndex="2"
                Width="100px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件号" FieldName="MATERIAL_CODE" VisibleIndex="3"
                Width="100px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件名称" FieldName="PT_DESC2" VisibleIndex="4" Width="100px"
                Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="保管员" FieldName="IN_QADC01" VisibleIndex="5" Width="100px"
                Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="库存" FieldName="LINESIDE_NUM" VisibleIndex="6"
                Width="100px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="GYS_CODE" VisibleIndex="7"
                Width="100px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商名称" FieldName="VD_SORT" VisibleIndex="8" Width="100px"
                Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="sqlGzdd" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlLocation" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlBgy" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <script type="text/javascript">
        var pline;
        var locationCode;
        var bgyCode;

        if (!String.prototype.trim) {
            String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
        }
        //根据生产线初始化工位，保管员
        function filterLocationBGY() {
            cmbBgyC.ClearItems(); //保管员
            cmbLocationC.ClearItems(); //工位

            pline = plineC.GetValue().toString();

            cmbBgyC.PerformCallback(pline);
            cmbLocationC.PerformCallback(pline);

        }
    </script>
</asp:Content>
