<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="atpu1E00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Atpu.atpu1E00.atpu1E00" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function OpenAddWindow() {
            window.open('atpu1E01.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=800,height=800,top=150,left=250');
        }
        ///////////////////////////////////////////
        function ShowDel() {
            var count = grid.GetSelectedRowCount();
            if (count = 0) {
                alert("请选择事件");
                return;
            }
            else {
//                butDeleteC.SetEnabled(false);
                var fieldNames = "ROWID";
                grid.GetSelectedFieldValues(fieldNames, RecoverResult);
            }
        }
        function RecoverResult(result) {
            if (result == "") {
//                butDeleteC.SetEnabled(true);
                alert('没有选择任何事件！无法进行操作');
            }
            else {
                var ref = "DEL," + result;
                CallbackSubmit.PerformCallback(ref);
            }
        }

        //////////////////////////////////////////////
        function ShowZX() {
            var count = grid.GetSelectedRowCount();
            if (count = 0) {
                alert("请选择事件");
                return;
            }
            else {
                butZXC.SetEnabled(false);
                var fieldNames = "ROWID";
                grid.GetSelectedFieldValues(fieldNames, ZxResult);
            }
        }
        
        function ZxResult(result) {
            if (result == "") {
                butZXC.SetEnabled(true);
                alert('没有选择任何事件！无法进行操作');
            }
            else {
                var ref = "ZX," + result;
                CallbackSubmit.PerformCallback(ref);
            }
        }
        ////////////////////////////////////////////////////////////////
        function ShowGB() {
            var count = grid.GetSelectedRowCount();
            if (count = 0) {
                alert("请选择事件");
                return;
            }
            else {
                butGBC.SetEnabled(false);
                var fieldNames = "ROWID";
                grid.GetSelectedFieldValues(fieldNames, GbResult);
            }
        }

        function GbResult(result) {
            if (result == "") {
                butGBC.SetEnabled(true);
                alert('没有选择任何事件！无法进行操作');
            }
            else {
                var ref = "GB," + result;
                CallbackSubmit.PerformCallback(ref);
            }
        }
        function submitRtr(e) {
            var result = "";
            var retStr = "";
            if (e == null) return;
            var array = e.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    if (retStr != "") {
                        alert(retStr);
                    }
//                    butDeleteC.SetEnabled(true);
                    butZXC.SetEnabled(true);
                    butGBC.SetEnabled(true);
                    initGridview();
                    return;
                case "Fail":
                    alert(retStr);
//                    butDeleteC.SetEnabled(true);
                    butZXC.SetEnabled(true);
                    butGBC.SetEnabled(true);
                    return;
            }
        }
        function initGridview() {
            grid.PerformCallback();
        }
    </script>
    <table>
        <br />
        <tr>
            <%--<td style="width:90px">
                <dx:ASPxButton runat="server" ID="AddButton" Text="新增" Width="80px" Visible="false">
                    <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
                </dx:ASPxButton>
            </td>
            <td style="width:90px">
                <dx:ASPxButton ID="butDel" Text="删除" Width="80px" AutoPostBack="false" ClientInstanceName="butDeleteC" Visible="false"
                    runat="server">
                    <ClientSideEvents Click="function(s,e){
                       ShowDel();     
                    }" />
                </dx:ASPxButton>
                
            </td>--%>
            <td style="width:90px">
                <dx:ASPxButton ID="butZhiXing" Text="执行" Width="80px" AutoPostBack="false" ClientInstanceName="butZXC"
                    runat="server">
                    <ClientSideEvents Click="function(s,e){
                       ShowZX();     
                    }" />
                </dx:ASPxButton>
            </td>
            <td style="width:90px">
                <dx:ASPxButton ID="butGuanBi" Text="关闭" Width="80px" AutoPostBack="false" ClientInstanceName="butGBC"
                    runat="server">
                    <ClientSideEvents Click="function(s,e){
                       ShowGB();     
                    }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>

    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback1">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>

    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="ROWID" SettingsBehavior-AllowFocusedRow="false"
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" >
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="50px" ShowSelectCheckbox="True">
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Select/Unselect all rows on the page"
                        ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="事件代码" FieldName="SJDM" VisibleIndex="1" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataTextColumn Caption="事件名称" FieldName="SJMC" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="150px" />
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="3"
                Width="100px" Settings-AutoFilterCondition="Contains" Visible="false" />
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="4" Width="100px"
                Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="执行情况" FieldName="SJBS" VisibleIndex="5" Width="100px"
                Settings-AutoFilterCondition="Contains" Visible="false" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
