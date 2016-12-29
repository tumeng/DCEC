<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept1800.aspx.cs" Inherits="Rmes_Rept_rept1800_rept1800" StylesheetTheme="Theme1" %>

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
<%--装机清单预览--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function openPic(s, e) {
            if (Pcode.GetValue() == null || Sname.GetValue() == null) {
                alert("请先填写要预览图片的站点名称和生产线！")
                return;
            }
            var webFileUrl = "?PCODE=" + Pcode.GetValue() + "&SNAME=" + Sname.GetValue() + "&FLAG=" + flag.GetValue() + "  &opFlag=openPic";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var array1 = result.split('$');
            var Pics = "";
            for (i = 0; i < array1.length - 1; i++) {
                var itema = array1[i];
                Pics += itema + "|";
            }
            Pics = "N|" + Pics + Pcode.GetValue();
            window.open('/Rmes/Rept/rept1800/rept1800_pic.aspx?Pic=' + Pics);
        }

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
                Pcode.SetValue(array1[1]);
                flag.SetValue(array1[2]);
                SO.SetValue(array1[3]);
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
                <dx:ASPxComboBox ID="txtPCode" runat="server" ValueType="System.String" Width="100px"
                    SelectedIndex="0" TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode"
                    ClientInstanceName="Pcode">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSO" runat="server" ValueField="plan_so" ValueType="System.String"
                    Width="100px" ClientInstanceName="SO">
                    <ClientSideEvents TextChanged="function(s,e){
                         PlanPanel.PerformCallback();
                        
                    }" />
                </dx:ASPxTextBox>
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
                                DropDownStyle="DropDownList" ValueType="System.String" Width="120px" DataSourceID="SqlPCode"
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
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="站点名称:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSName" runat="server" ValueType="System.String" Width="100px"
                    ClientInstanceName="Sname">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="客户:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtVendor" runat="server" ValueType="System.String" Width="100px"
                    ClientInstanceName="Vendor">
                </dx:ASPxTextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="80px">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback(); grid2.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="Preview" runat="server" AutoPostBack="False" Text="装机图片预览" Width="120px">
                    <ClientSideEvents Click="function(s,e){
                         openPic(s, e);
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtFlag" runat="server" ClientVisible="false" ValueType="System.String"
                    Width="100px" ClientInstanceName="flag">
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="True"
                    OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="GWMC;COMP;QTY;UDESC;GXMC;MACHINENAME"
                    Width="700px" OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared" OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="1" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="零件号" FieldName="COMP" VisibleIndex="2" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="QTY" VisibleIndex="4" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="名称" FieldName="UDESC" VisibleIndex="3" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工序" FieldName="GXMC" VisibleIndex="5" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYSMC" VisibleIndex="6" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="MACHINENAME" FieldName="MACHINENAME" VisibleIndex="7"
                            Width="100px" Settings-AutoFilterCondition="Contains" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="零件级别" FieldName="ITEM_CLASS" VisibleIndex="8"
                            Width="100px" Settings-AutoFilterCondition="Contains" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="零件类型" FieldName="ITEM_TYPE" VisibleIndex="9"
                            Width="100px" Settings-AutoFilterCondition="Contains" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
            <td>
                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="false"
                    SettingsPager-Mode="ShowAllRecords" KeyFieldName="CZTS;GWDM;ZDDM" Width="500px"
                    OnHtmlRowPrepared="ASPxGridView2_HtmlRowPrepared">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="操作提示" FieldName="CZTS" VisibleIndex="1" Width="350px"
                            Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <%--<dx:GridViewCommandColumn Caption="预览"  Width="60px" ButtonType="Button" VisibleIndex="3"   >
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="Preview" Text="预览" >
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>--%>
                        <dx:GridViewDataTextColumn Caption="颜色" FieldName="NOTE_COLOR" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="字体" FieldName="NOTE_FONT" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWDM" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="站点" FieldName="ZDDM" Visible="false" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <%-- <dx:GridViewDataTextColumn Caption="文件路径" FieldName="WJPATH" Visible="false" Settings-AutoFilterCondition="Contains" >
                         
                        </dx:GridViewDataTextColumn>--%>
                    </Columns>
                    <Settings ShowFooter="True" />
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="CZTS" SummaryType="Count" DisplayFormat="数量={0}" />
                    </TotalSummary>
                    <%--  <ClientSideEvents   CustomButtonClick="function(s, e){
            ButtonClick(s, e);
            }" />--%>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
