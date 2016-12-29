<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept3500.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept3500.rept3500" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%--发动机履历--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries2(s, e) {
            if (PlanCode.GetValue() == null) {
                alert("请选择计划号！")
                return;
            }

            var webFileUrl = "?PLANCODE=" + PlanCode.GetValue() + " &opFlag=getEditSeries2";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var array1 = result.split('@');

            if (result == "") {
                alert("该计划号不存在！")
            }
            else {
                Vendor.SetValue(array1[0]);

                SO.SetValue(array1[1]);
                Pcode.SetValue(array1[2]);
            }
        }
    </script>
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" SelectedIndex="1" ClientInstanceName="Pcode">
                     <ClientSideEvents TextChanged="function(s,e){
                         PlanPanel.PerformCallback();
                        
                    }" />
                </dx:ASPxComboBox>
                
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="计划号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel4" ClientInstanceName="PlanPanel"
                    OnCallback="ASPxCallbackPanel4_Callback">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent4" runat="server">
                            <dx:ASPxComboBox ID="txtPlanCode" runat="server" TextField="PLAN_CODE" ValueField="PLAN_CODE"
                                DropDownStyle="DropDownList" ValueType="System.String" Width="140px" DataSourceID="SqlPlanCode"
                                ClientInstanceName="PlanCode">
                                <ClientSideEvents Init="function(s,e){
                         PlanPanel.PerformCallback();
                        
                    }" TextChanged="function(s,e){
                        initEditSeries2(s, e);
                        
                    }" />
                            </dx:ASPxComboBox>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="计划SO:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSO" runat="server" ValueField="plan_so" ValueType="System.String"
                    Width="120px" ClientInstanceName="SO">
                    <ClientSideEvents TextChanged="function(s,e){
                         PlanPanel.PerformCallback();
                        
                    }" />
                    </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="客户:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtVendor" runat="server" ValueType="System.String" Width="130px"
                    ClientInstanceName="Vendor">
                </dx:ASPxTextBox>
            </td>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="80px">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="true"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="GZDD;PART;GXDM">
        <Columns>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="80px"
                Settings-AutoFilterCondition="Contains" Visible="false">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO组件" FieldName="PART" VisibleIndex="2" Width="150px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="装机提示" FieldName="CZTS" VisibleIndex="3" Width="400px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工序代码" FieldName="GXDM" VisibleIndex="4" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" VisibleIndex="5"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPlanCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
