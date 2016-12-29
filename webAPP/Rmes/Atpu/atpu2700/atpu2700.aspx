<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Theme="Theme1" CodeBehind="atpu2700.aspx.cs" Inherits="Rmes_atpu2700" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--功能概述：VEPS手动更新--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <tr>
        <td>
            <table>
                <td style="width: 10px">
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="查询条件" />
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td style="width: 10px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="发动机型号:" />
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ComboSo" ClientInstanceName="listSO" runat="server" Width="140px"
                            TextField="SO" ValueField="SO">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) { filter(); }" />
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="系列:" />
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ComboCSLX" ClientInstanceName="listBoxCSLX" runat="server" Width="140px"
                            TextField="CSLXDM" ValueField="CSLXDM" OnCallback="ComboCSLX_Callback">
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="编号:" />
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ComboCS" ClientInstanceName="listBoxCS" runat="server" Width="140px"
                            TextField="CSDM" ValueField="CSDM" OnCallback="ComboCS_Callback">
                            <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { filter(); }" />--%>
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="SO" OnCustomCallback="ASPxGridView1_CustomCallback">
        <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="80px">
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="编号" FieldName="CSDM" VisibleIndex="1" Width="120px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="名称" FieldName="CSMC" VisibleIndex="2" Width="180px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="参数值" FieldName="CSZ" VisibleIndex="3" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="初始值" FieldName="CSMRZ" VisibleIndex="4" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO号:" />
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSoQry" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnUpdate" runat="server" Text="手动更新" UseSubmitBehavior="False"
                    OnClick="btnUpdate_Click" Height="21px" Width="93px" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCSLX" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCS" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <script type="text/javascript">
        var SO;
        if (!String.prototype.trim) {
            String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
        }

        function filter() {
            SO = listSO.GetValue();

            listBoxCSLX.PerformCallback(SO);
            listBoxCS.PerformCallback(SO);
        }
        //        function filterCS() {
        //            stationCodeL = listBoxCS.GetValue().toString();
        //        }



        //        function checkSubmit() {
        //            if (listBoxLocation.GetSelectedItems().length == 0 || listBoxStation.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1 || listLocationPro.GetSelectedIndex() == -1) {
        //                alert("请选择站点、工位及工位属性再提交！");
        //                return false;
        //            }
        //        }

    </script>
</asp:Content>
