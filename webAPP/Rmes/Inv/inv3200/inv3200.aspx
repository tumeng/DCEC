<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inv3200.aspx.cs" Inherits="Rmes_inv3200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function initListZJDH() {
        var project = Project.GetValue().toString();
        ZJDH.PerformCallback(project);
    }
</script>

<table>
    <tr>
        <td style="width: 5px">
        </td>
        <td style="width: 50px">
            <dx:ASPxLabel ID="LGCH" runat="server" Text="工程号">
            </dx:ASPxLabel>
        </td>
        <td style="width: 150px">
            <dx:ASPxComboBox ID="ASPxComboBoxProject" ClientInstanceName="Project" runat="server"
                Width="143px">
                <ClientSideEvents SelectedIndexChanged="function(s, e) { initListZJDH(); }" />
            </dx:ASPxComboBox>
        </td>
        <td style="width: 5px">
        </td>
        <td style="width: 50px">
            <dx:ASPxLabel ID="LZJDH" runat="server" Text="组件代号">
            </dx:ASPxLabel>
        </td>
        <td style="width: 150px">
            <dx:ASPxComboBox ID="ASPxComboBoxZJDH" ClientInstanceName="ZJDH" runat="server" Width="143px"
                OnCallback="ASPxComboBoxZJDH_Callback">
            </dx:ASPxComboBox>
        </td>
        <td style="width: 5px">
        </td>
        <td style="width: 50px">
            <dx:ASPxLabel ID="LXZ" runat="server" Text="领料小组">
            </dx:ASPxLabel>
        </td>
        <td style="width: 150px">
            <dx:ASPxComboBox ID="ASPxComboBoxTeam" ClientInstanceName="LLXZ" runat="server" Width="143px">
            </dx:ASPxComboBox>
        </td>
        <td style="width: 5px">
        </td>
        <td>
            <dx:ASPxButton ID="bt_check" runat="server" AutoPostBack="true" Text="查 询" OnClick="bt_check_Click">
            </dx:ASPxButton>
        </td>
        <td>
        </td>
        <td>
            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                OnClick="btnXlsExport_Click" />
        </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ITEM_CODE">
    <Settings ShowHorizontalScrollBar="true" ShowFilterRow="true" />
    <Columns>
        <dx:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="40px">
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataComboBoxColumn Caption="工程号" FieldName="PROJECT_CODE" Width="100px" />
        <dx:GridViewDataComboBoxColumn Caption="组件代号" FieldName="ASSEMBLY_CODE" Width="100px" />
        <dx:GridViewDataComboBoxColumn Caption="班组" FieldName="TEAM_CODE" Width="150px" />
        <dx:GridViewDataTextColumn Caption="项目代号" FieldName="ITEM_CODE" Width="150px">
            <Settings AutoFilterCondition="Contains" />
            <%-- 支持模糊查询--%>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="项目名称" FieldName="ITEM_NAME" Width="220px">
            <Settings AutoFilterCondition="Contains" />
            <%-- 支持模糊查询--%>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="计划数量" FieldName="PLAN_QTY" Width="60px" CellStyle-HorizontalAlign="Center" />
        <dx:GridViewDataTextColumn Caption="已发数量" FieldName="SEND_QTY" Width="60px" CellStyle-HorizontalAlign="Center" />
        <dx:GridViewDataTextColumn Caption="已收数量" FieldName="RECEIVE_QTY" Width="60px" CellStyle-HorizontalAlign="Center" />
        <dx:GridViewDataTextColumn Caption="统计" FieldName="CALCULATE_QTY" Width="60px" CellStyle-HorizontalAlign="Center" />
    </Columns>

    <ClientSideEvents CustomButtonClick="function(s,e){ getUnit(s,e); }" />

</dx:ASPxGridView>

<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>

</asp:Content>
