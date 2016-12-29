<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv1200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv1200.inv1200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="detailDS" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <script type="text/javascript">

        function changeSeq(s, e) {
            index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);

        }


        function GetDataCallback(s) {
            var result = "";
            var retStr = "";
            if (s == null) {
                grid.PerformCallback();
                return;
            }
            var array = s.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    alert(retStr);
                    initGridview();
                    return;
                case "Fail":
                    alert(retStr);
                    return;
            }
            grid.PerformCallback();
        }
        
        function filterLsh3() {
            //提交
            var ref = "";
            butSubmit.SetEnabled(false);

            if (confirm("是否删除重复扫描纪录？")) {
            }
            else {
                alert("操作已取消！");
                butSubmit.SetEnabled(true);
                return;
            }

            ref = 'DeleteScan';
            CallbackSubmit.PerformCallback(ref);
        }
        function filterLsh2() {
            //提交
            var ref = "";
            butSubmit2.SetEnabled(false);

            if (confirm("是否删除重复出库纪录？")) {
            }
            else {
                alert("操作已取消！");
                butSubmit2.SetEnabled(true);
                return;
            }

            ref = 'DeleteCFRK';
            CallbackSubmit.PerformCallback(ref);
        }
        function submitRtr(e) {
            var result = "";
            var retStr = "";
            var array = e.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    alert(retStr);
                    butSubmit.SetEnabled(true);
                    butSubmit2.SetEnabled(true);
                    grid.PerformCallback();

                    return;
                case "Fail":
                    alert(retStr);
                    butSubmit.SetEnabled(true);
                    butSubmit2.SetEnabled(true);
                    grid.PerformCallback();

                    return;
            }
        }
        function chekPline() {
            if (cPlineC.GetSelectedIndex() == -1) {
                alert("请选择生产线！");
                return false;
            }

        }
        function chekSJ() {
            if (cmbSjPcC.GetSelectedIndex() == -1) {
                alert("请选择时间批次！");
                return false;
            }

        }
        function initGridview() {
            grid.PerformCallback();
            grid21.PerformCallback();
        }

    </script>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel6" ClientInstanceName="ListBoxPanel6">
        <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                ListBoxPanel6.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName = ListBoxPanel6.cpCallbackName;
                                    theRet = ListBoxPanel6.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    grid.PerformCallback();
                                    grid21..PerformCallback();
                                }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent6" runat="server">
                <table>
                    <tr style="background-color: White">
                        <td style="width: 500px">
                            <fieldset style="width: 500px; text-align: center; height: 120px">
                                <legend><span style="font-size: 10pt; width: auto">
                                    <asp:Label ID="Label23" runat="server" Text="操作" Font-Bold="True"></asp:Label></span></legend>
                                <table width="95%">
                                    <tr>
                                        <td style="text-align:left">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label13" runat="server" Text="生产线"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="comboPline" runat="server" ClientInstanceName="cPlineC" Height="25px" SelectedIndex="0"
                                                            TextField="PLINE_NAME" ValueField="PLINE_CODE" ValueType="System.String" Width="280px">
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="CmdPrint" Text="打印收发名单" Width="150px" AutoPostBack="false" runat="server"  
                                                OnClick="CmdPrint_Click">
                                                <%--<ClientSideEvents Click="function(s, e) { chekPline(); }" />--%>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input id="File2" type="file" accept=".txt" size="20" style="font-size: medium; height: 25px;
                                                            width: 220px" alt="请选择文本文件" runat="server" />
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ButRead" Text="读取文本" Width="100px" AutoPostBack="false" runat="server"
                                                             OnClick="cmdRefresh_Click">
                                                            <ClientSideEvents Click="function(s, e) { chekPline(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="CmdPrint2" Text="打印流水号清单" Width="150px" AutoPostBack="false" runat="server" 
                                                OnClick="CmdPrint2_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left">
                                            <dx:ASPxComboBox ID="CmbCh" ClientInstanceName="CmbChC" runat="server" Width="320px"  AutoPostBack="true"
                                                Height="25px" DropDownStyle="DropDownList" OnSelectedIndexChanged="CmbCh_SelectedIndexChanged">
                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { filterAline(); }" />--%>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="cmdCfm" Text="出库确认" Width="150px" AutoPostBack="false" runat="server" 
                                                OnClick="cmdCfm_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 500px; text-align: center; height: 120px;">
                                <legend><span style="font-size: 10pt; width: auto">
                                    <asp:Label ID="Label1" runat="server" Text="批处理" Font-Bold="True"></asp:Label></span></legend>
                                <table width="95%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="车号" Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxTextBox ID="TxtCh" runat="server" Width="150px" OnTextChanged="TxtCh_TextChanged">
                                            </dx:ASPxTextBox>
                                            <%--<dx:ASPxButton ID="BtnHidden" Text="H" Width="140px" ClientInstanceName="butSubmitHidden"
                                                AutoPostBack="false" runat="server" OnClick="BtnHidden_Click"  Visible="false" >
                                                
                                            </dx:ASPxButton>--%>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Text="类型" Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="CmbRklx" ClientInstanceName="CmbRklxC" runat="server" Width="150px"
                                                Height="25px" DropDownStyle="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="CmbRklx_SelectedIndexChanged">
                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { filterAline(); }" />--%>
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="方向" Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="CmbFx" ClientInstanceName="CmbFxC" runat="server" Width="150px"
                                                Height="25px" DropDownStyle="DropDownList" AutoPostBack="true" 
                                                OnSelectedIndexChanged="CmbFx_SelectedIndexChanged">
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="员工" Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="CmbYgmc" ClientInstanceName="CmbYgmcC" runat="server" Width="150px"
                                                Height="25px" DropDownStyle="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="CmbYgmc_SelectedIndexChanged">
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <table>
                                                <tr>
                                                    <td style="width: 170px; text-align: right">
                                                        <dx:ASPxButton ID="Cmd_Del1" Text="删除重复扫描" Width="140px" ClientInstanceName="butSubmit"
                                                            AutoPostBack="false" runat="server" >
                                                            <ClientSideEvents Click="function(s, e) { filterLsh3(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="Cmd_Del2" Text="删除重复出库" Width="140px" ClientInstanceName="butSubmit2"
                                                            AutoPostBack="false" runat="server" >
                                                            <ClientSideEvents Click="function(s, e) { filterLsh2(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="cmdQryFj" Text="查看随机附件" Width="160px" AutoPostBack="false" runat="server"
                                                            OnClick="cmdQryFj_Click"  >
                                                            <ClientSideEvents Click="function(s, e) { chekPline(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 180px; text-align: left; height: 120px;">
                                <legend><span style="font-size: 10pt; width: auto">
                                    <asp:Label ID="Label8" runat="server" Text="标识" Font-Bold="True"></asp:Label></span></legend>
                                <table width="95%">
                                    <tr>
                                        <td style="background-color: Red; text-align: left">
                                            <asp:Label ID="Label9" runat="server" Text="扫描重复" Width="170px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: Yellow; text-align: left">
                                            <asp:Label ID="Label10" runat="server" Text="重复出库" Width="170px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: LightGreen; text-align: left">
                                            <asp:Label ID="Label4" runat="server" Text="库存中没有记录" Width="170px"></asp:Label>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td style="background-color: Gray; text-align=: left">
                                            <asp:Label ID="Label7" runat="server" Text="右键可查询单台流程" Width="170px"></asp:Label>
                                        </td>
                                    </tr>--%>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 10%">
                        </td>
                    </tr>
                    <div runat="server" id="QITA">
                        <tr>
                            <td colspan="4">
                                <fieldset style="text-align: left; width: 1300px; height: 450px">
                                    <legend><span style="font-size: 10pt; width: auto">
                                        <asp:Label ID="Label11" runat="server" Text="清单" Font-Bold="True"></asp:Label></span></legend>
                                    <table width="95%">
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="fpSpDetail" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                                                    KeyFieldName="GHTM" Settings-ShowHorizontalScrollBar="false" Settings-ShowGroupPanel="false"
                                                    Settings-VerticalScrollableHeight="365" 
                                                    Settings-ShowVerticalScrollBar="true" Settings-ShowFilterRow="false"
                                                    Width="100%" OnHtmlRowPrepared="fpSpDetail_HtmlRowPrepared" 
                                                    OnPageIndexChanged="fpSpDetail_PageIndexChanged" 
                                                    OnCustomDataCallback="fpSpDetail_CustomDataCallback" 
                                                    OnCustomCallback="fpSpDetail_CustomCallback" 
                                                    OnRowUpdating="fpSpDetail_RowUpdating">
                                                    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" />
                                                    <SettingsEditing Mode="Inline" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                                            Width="40px">
                                                            <HeaderTemplate>
                                                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                                                                    Style="margin-bottom: 0px" />
                                                            </HeaderTemplate>
                                                            <SelectButton Text="选择">
                                                            </SelectButton>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewCommandColumn Width="60px" VisibleIndex="1">
                                                                    <EditButton Visible="true" />
                                                                </dx:GridViewCommandColumn>
                                                        <dx:GridViewCommandColumn Caption="清理" Width="50px" ButtonType="Link" VisibleIndex="1">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="Delete" Text="删除">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="80px" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" Width="80px" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" Width="110px" VisibleIndex="4" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="入出库日期" FieldName="GZRQ" Width="120px" VisibleIndex="5" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Width="50px" VisibleIndex="6" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="员工名称" FieldName="YGMC" Width="60px" VisibleIndex="7" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="人/出" FieldName="RC" Width="40px" VisibleIndex="8" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="入出库类型" FieldName="RKLX" Width="90px" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="从" FieldName="SOURCEPLACE" Width="90px" VisibleIndex="10" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="到" FieldName="DESTINATION" Width="90px" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="批次号" FieldName="BATCHID" Width="80px" VisibleIndex="12" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="车号" FieldName="CH" Width="70px" VisibleIndex="13" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="RKDATE" Width="120px" VisibleIndex="14" ReadOnly="true">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <DetailRow>
                                                            <dx:ASPxGridView ID="fpSpDetail_detail" ClientInstanceName="grid2" runat="server"
                                                                AutoGenerateColumns="False" Settings-ShowGroupPanel="false" KeyFieldName="GHTM" Settings-ShowFilterRow="false"
                                                                SettingsPager-Mode="ShowAllRecords" Settings-ShowHeaderFilterButton="false" Settings-ShowVerticalScrollBar="false"
                                                                Width="100%" OnBeforePerformDataSelect="fpSpDetail_detail_BeforePerformDataSelect">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="入出库时间" FieldName="GZRQ" Width="120px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="操作员工" FieldName="YGMC" Width="70px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="入/出" FieldName="RC" Width="50px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="入出库类型" FieldName="RKLX" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="从" FieldName="SOURCEPLACE" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="到" FieldName="DESTINATION" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="批次" FieldName="BATCHID" Width="100px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="车号" FieldName="CH" Width="70px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="RKDATE" Width="120px">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                            </dx:ASPxGridView>
                                                        </DetailRow>
                                                    </Templates>
                                                    <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="365"></Settings>
                                                    <ClientSideEvents CustomButtonClick="function (s,e){
                                                                    changeSeq(s,e);
                                                                }" />
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td style="width: 1000px">
                                            <fieldset style="text-align: left;">
                                                <legend><span style="font-size: 10pt; width: auto">
                                                    <asp:Label ID="Label14" runat="server" Text="统计" Font-Bold="True"></asp:Label></span></legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <dx:ASPxGridView ID="fpSpCal" ClientInstanceName="grid21" runat="server" AutoGenerateColumns="False"
                                                                Settings-ShowGroupPanel="false" KeyFieldName="SO" 
                                                                Width="100%" Settings-ShowFilterRow="false" 
                                                                OnCustomCallback="fpSpCal_CustomCallback">
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="批次" FieldName="BATCHID" Width="150px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="车号" FieldName="CH" Width="150px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="方向" FieldName="DESTINATION" Width="200px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="200px">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" >
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <Settings ShowFooter="True" />
                                                                <TotalSummary>
                                                                    <dx:ASPxSummaryItem FieldName="SL" SummaryType="Sum" DisplayFormat="总数={0}" />
                                                                </TotalSummary>
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </div>
                    <div id="SJFJQD" runat="server">
                        <tr>
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td style="width: 1000px">
                                            <fieldset style="text-align: left;">
                                                <legend><span style="font-size: 10pt; width: auto">
                                                    <asp:Label ID="Label15" runat="server" Text="随机附件清单" Font-Bold="True"></asp:Label></span></legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="text-align: right; width: 100px">
                                                            <asp:Label ID="Label12" runat="server" Text="时间批次"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 160px">
                                                            <dx:ASPxComboBox ID="cmbSjPc" ClientInstanceName="cmbSjPcC" runat="server" Width="150px"
                                                                Height="25px" DropDownStyle="DropDownList">
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 190px">
                                                            <dx:ASPxButton ID="cmdSelSj" Text="查看所选时间随机附件" AutoPostBack="false" runat="server"
                                                                Width="180px" OnClick="cmdSelSj_Click">
                                                                <ClientSideEvents Click="function(s, e) { chekSJ(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="width: 160px">
                                                            <%--<dx:ASPxButton ID="cmdPrtFj" Text="打印随机附件" AutoPostBack="false" runat="server" Width="150px">
                                                            </dx:ASPxButton>--%>
                                                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1" >
                                                            </dx:ASPxGridViewExporter>
                                                            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="打印随机附件" 
                                                                OnClick="btnXlsExport_Click" />
                                                        </td>
                                                        <td style="width: 110px">
                                                            <dx:ASPxButton ID="cmdShut" Text="退出" AutoPostBack="false" runat="server" Width="100px"
                                                                OnClick="cmdShut_Click">
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="text-align: left; width: 550px">
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 40px">
                                                        <td colspan="6" style="text-align: center">
                                                            <asp:Label ID="Label16" runat="server" Text="发动机随机附件明细" Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 15%">
                                                                        <asp:Label ID="Label24" runat="server" Text="打印时间"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 85%">
                                                                        <dx:ASPxTextBox ID="txtThisSj" runat="server" Width="80%">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label17" runat="server" Text="S0"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtTheSo" runat="server" Width="80%">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label19" runat="server" Text="流水号数量"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txttheLshSl" runat="server" Width="80%">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label20" runat="server" Text="流水号明细"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxMemo ID="txttheLshStr" runat="server" ClientInstanceName="cltRemark" Height="50px"
                                                                            Width="80%">
                                                                        </dx:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label21" runat="server" Text="包装单元数量"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txttheBzSl" runat="server" Width="80%">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px">
                                                                    <td colspan="2" style="vertical-align:bottom"  >
                                                                        <asp:Label ID="Label22" runat="server" Text="包装单元明细" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid21" runat="server" AutoGenerateColumns="False"
                                                                            Settings-ShowGroupPanel="false" KeyFieldName="DYXH" SettingsPager-Mode="ShowAllRecords" 
                                                                            Settings-ShowFilterRow="false"
                                                                            Width="100%" OnPageIndexChanged="ASPxGridView1_PageIndexChanged">
                                                                            <Columns>
                                                                                <dx:GridViewDataTextColumn Caption="序号" FieldName="DYXH" Width="200px" CellStyle-HorizontalAlign="Left">
                                                                                    <CellStyle HorizontalAlign="Left">
                                                                                    </CellStyle>
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="包装单元" FieldName="BZDY" Width="300px">
                                                                                </dx:GridViewDataTextColumn>
                                                                                <dx:GridViewDataTextColumn Caption="附件包" FieldName="FJB" Width="300px">
                                                                                </dx:GridViewDataTextColumn>
                                                                            </Columns>
                                                                            <SettingsPager Mode="ShowAllRecords">
                                                                            </SettingsPager>
                                                                        </dx:ASPxGridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </div>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>
</asp:Content>
