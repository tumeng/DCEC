<%@ Page Title="改制BOM对比" Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.master"
    CodeBehind="rept3300.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept3300.rept3300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%--改制BOM对比 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (sn.GetValue() == null) {
                alert("请输入流水号！");
                return;
            }
            if (sn.GetText().length != 8) {
                alert("流水号长度非法！");
                return;
            }
            //?后放返回的参数，可以放多个
            var webFileUrl = "?SN=" + sn.GetValue() + " &PLINE_CODE=" + plineCode.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            if (result == "0")
            {
                alert("流水号不存在！");
                //不写return的话还会继续运行，将0填入list框
                return;
            }
            if (result == "1") {
                alert("流水号已录入！");
                return;
            }
            else
            {
                lsh.ClearItems();
                lsh.AddItem(result);
                return;
            }
        }

        function queryDetail(s, e) {
            if (lsh.GetValue == null || lsh.GetValue == "") {
                //??无效为什么
                alert("请输入流水号！");
                return;
            }
            else {
                grid.PerformCallback();
                return;
            }
        }

    </script>

    <table>
        <%--<tr>
            <td style="height:30px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="改制流水号"></dx:ASPxLabel>
            </td>
        </tr>--%>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="plineCode">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="地点:" Visible="false"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="120px" Text="项目一" Enabled="false" Visible="false"></dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="流水号:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSN" runat="server" Width="120px" ValueField="SN" ValueType="System.String" ClientInstanceName="sn">
                    <ClientSideEvents TextChanged ="function(s,e){
                        initEditSeries(s, e) ;
                    }" />
                </dx:ASPxTextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <dx:ASPxListBox ID="listLsh" runat="server" ClientInstanceName="lsh" Width="400"
                    Height="230px" ValueField="SN" TextField="SN" ValueType="System.String" ViewStateMode="Inherit" 
                     >
                </dx:ASPxListBox>
            </td>
            <td>
                <dx:ASPxButton ID="btnQryDetail" runat="server" Text="查询明细" Width="100px" OnClick="btnQryDetail_Click">
                    <ClientSideEvents Click="function(s, e){
                        queryDetail(s, e);
                        }"/>
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnExport" runat="server" Text="导出" Width="100px" OnClick="btnExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            <td>
                白色表示新BOM没有变化，绿色表示新BOM新增，红色表示新BOM没有
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="GHTM"
        OnHtmlRowPrepared="ASPxGridView1_HtmlRowPrepared">
        <Columns>
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件号" FieldName="ABOM_COMP" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件名称" FieldName="ABOM_DESC" VisibleIndex="3" Width="120px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="ABOM_QTY" VisibleIndex="4" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工序" FieldName="ABOM_OP" VisibleIndex="5" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位" FieldName="ABOM_WKCTR" VisibleIndex="6" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划号" FieldName="ABOM_JHDM" VisibleIndex="7" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="ABOM_SO" VisibleIndex="7" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="地点" FieldName="GZDD" VisibleIndex="7" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="ABOM_GYS" VisibleIndex="7" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="类型" FieldName="COMP_FLAG" VisibleIndex="8" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="保管员" FieldName="BGY" VisibleIndex="8" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
