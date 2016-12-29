<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv1100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv1100.inv1100" %>

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
    <asp:SqlDataSource ID="SqlRklx" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
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

            if (confirm("是否删除重复入库纪录？")) {
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
        function initGridview() {
            grid.PerformCallback();
        }

    </script>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel6" ClientInstanceName="ListBoxPanel6"
        OnCallback="ASPxCallbackPanel6_Callback">
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
                                }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent6" runat="server">
                <table>
                    <tr style="background-color: White">
                        <td style="width: 460px">
                            <fieldset style="width: 460px; text-align: center; height: 140px">
                                <legend><span style="font-size: 10pt; width: auto">
                                    <asp:Label ID="Label23" runat="server" Text="批次号选择" Font-Bold="True"></asp:Label></span></legend>
                                <table width="95%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="生产线" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="comboPline" ClientInstanceName="cPlineC" runat="server" Width="110px" SelectedIndex="0"
                                                Height="25px" DropDownStyle="DropDownList" ValueField="PLINE_CODE" TextField="PLINE_NAME">
                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { filterAline(); }" />--%>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td >
                                            <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" />
                                            <input id="File2" type="file" accept=".txt" size="20" style="font-size: medium; height: 25px; width:220px"
                                                alt="请选择文本文件" runat="server" />
                                            <dx:ASPxButton ID="ButRead" Text="读取文本" Width="220px" AutoPostBack="false" runat="server"
                                                OnClick="ButRead_Click1">
                                                <ClientSideEvents Click="function(s, e) { chekPline(); }" />
                                            </dx:ASPxButton>
                                            <%--<dx:ASPxButton ID="ButBatchCheck" Text="批次校验" Width="220px" AutoPostBack="false"
                                                runat="server" OnClick="ButBatchCheck_Click">
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="ButInputSure" Text="入库确认" Width="220px" AutoPostBack="false" runat="server"
                                                OnClick="ButInputSure_Click">
                                            </dx:ASPxButton>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="批次号" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="comboBatch" ClientInstanceName="comboBatchC" runat="server"
                                                Width="110px" Height="25px" DropDownStyle="DropDownList" ValueField="BATCHID"
                                                TextField="BATCHID" OnSelectedIndexChanged="comboBatch_SelectedIndexChanged">
                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { filterAline(); }" />--%>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td >
                                            
                                            <dx:ASPxButton ID="ButBatchCheck" Text="批次校验" Width="220px" AutoPostBack="false"
                                                runat="server" OnClick="ButBatchCheck_Click">
                                            </dx:ASPxButton>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="是否基础机" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="comboJCJ" ClientInstanceName="comboJCJC" runat="server" Width="110px"
                                                Height="25px" DropDownStyle="DropDownList">
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                        <dx:ASPxButton ID="ButInputSure" Text="入库确认" Width="220px" AutoPostBack="false" runat="server"
                                                OnClick="ButInputSure_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 400px; text-align: center; height: 140px;">
                                <legend><span style="font-size: 10pt; width: auto">
                                    <asp:Label ID="Label1" runat="server" Text="批处理" Font-Bold="True"></asp:Label></span></legend>
                                <table width="95%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="入库类型" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="comboRKLX" ClientInstanceName="comboRKLXC" runat="server" Width="150px"
                                                Height="25px" DropDownStyle="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="comboRKLX_SelectedIndexChanged">
                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { filterAline(); }" />--%>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td rowspan="2">
                                            <dx:ASPxButton ID="Cmd_Del1" Text="删除重复扫描" Width="120px" ClientInstanceName="butSubmit"
                                                AutoPostBack="false" runat="server" OnClick="Cmd_Del1_Click">
                                                <ClientSideEvents Click="function(s, e) { filterLsh3(); }" />
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="Cmd_Del2" Text="删除重复入库" Width="120px" ClientInstanceName="butSubmit2"
                                                AutoPostBack="false" runat="server" OnClick="Cmd_Del2_Click">
                                                <ClientSideEvents Click="function(s, e) { filterLsh2(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="操作员工" Width="80px"></asp:Label>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="comboUser" ClientInstanceName="comboUserC" runat="server" Width="150px"
                                                Height="25px" DropDownStyle="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="comboUser_SelectedIndexChanged">
                                            </dx:ASPxComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input runat="server" id="File1" type="file" accept="application/msexcel" size="20"
                                                            style="font-size: medium; height: 25px;" alt="请选择Excel文件"> &nbsp;</input>
                                                        </input> &nbsp;</input>&nbsp;</input></input> &nbsp;
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="ASPxButton_Import" Text="导入数据" Width="120px" AutoPostBack="false"
                                                            runat="server" OnClick="ASPxButton_Import_Click">
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
                            <fieldset style="width: 100px; text-align: center; height: 140px;">
                                <legend><span style="font-size: 10pt; width: auto">
                                    <asp:Label ID="Label8" runat="server" Text="标识" Font-Bold="True"></asp:Label></span></legend>
                                <table width="95%">
                                    <tr>
                                        <td style="background-color: Red; text-align: left">
                                            <asp:Label ID="Label9" runat="server" Text="重复扫描" Width="80px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: Yellow; text-align: left">
                                            <asp:Label ID="Label10" runat="server" Text="重复入库" Width="80px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color: Blue; text-align: left">
                                            <asp:Label ID="Label4" runat="server" Text="不一致记录" Width="80px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td style="width: 30%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table>
                                <tr>
                                    <td style="width: 80%">
                                        <fieldset style="text-align: left;">
                                            <legend><span style="font-size: 10pt; width: auto">
                                                <asp:Label ID="Label5" runat="server" Text="清单" Font-Bold="True"></asp:Label></span></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <dx:ASPxGridView ID="fpSpDetail" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                                                            KeyFieldName="GHTM;RKDATE" Settings-ShowHorizontalScrollBar="true" Settings-ShowGroupPanel="false"
                                                            Width="100%" OnHtmlRowPrepared="fpSpDetail_HtmlRowPrepared" OnBeforeColumnSortingGrouping="fpSpDetail_BeforeColumnSortingGrouping"
                                                            OnCustomCallback="fpSpDetail_CustomCallback" OnCustomDataCallback="fpSpDetail_CustomDataCallback"
                                                            OnRowUpdating="fpSpDetail_RowUpdating">
                                                            <SettingsEditing Mode="Inline" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                                                                    VisibleIndex="0" Width="40px">
                                                                    <HeaderTemplate>
                                                                        <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                                                                            Style="margin-bottom: 0px" />
                                                                    </HeaderTemplate>
                                                                    <SelectButton Text="选择">
                                                                    </SelectButton>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewCommandColumn Width="60px">
                                                                    <EditButton Visible="true" />
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewCommandColumn Caption="清理" Width="60px" ButtonType="Link">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="Delete" Text="删除">
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="GHTM" FieldName="GHTM" VisibleIndex="1" Width="80px"
                                                                    SortIndex="1" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="工作日期" FieldName="GZRQ" Width="120px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="员工名称" FieldName="YGMC" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <%--<dx:GridViewDataTextColumn Caption="入库类型" FieldName="RKLX" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>--%>
                                                                <dx:GridViewDataComboBoxColumn Caption="入库类型" FieldName="RKLX">
                                                                    <PropertiesComboBox DataSourceID="SqlRklx" ValueField="LXMC" TextField="LXMC" IncrementalFilteringMode="StartsWith"
                                                                        ValueType="System.Int32" DropDownStyle="DropDownList" />
                                                                </dx:GridViewDataComboBoxColumn>
                                                                <dx:GridViewDataTextColumn Caption="从" FieldName="SOURCEPLACE" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="到" FieldName="DESTINATION" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="批次号" FieldName="BATCHID" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="车号" FieldName="CH" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="入库日期" FieldName="RKDATE" Width="120px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Width="100px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" Width="130px" ReadOnly="true">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <Settings ShowHorizontalScrollBar="True"></Settings>
                                                            <ClientSideEvents CustomButtonClick="function (s,e){
                                                                    changeSeq(s,e);
                                                                }" />
                                                        </dx:ASPxGridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                    <td style="width: 80%">
                                        <fieldset style="text-align: left;">
                                            <legend><span style="font-size: 10pt; width: auto">
                                                <asp:Label ID="Label6" runat="server" Text="出库记录" Font-Bold="True"></asp:Label></span></legend>
                                            <table width="95%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <dx:ASPxGridView ID="fpSpDetail2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                                                            Settings-ShowGroupPanel="false" KeyFieldName="RMES_ID" OnHtmlRowPrepared="fpSpDetail2_HtmlRowPrepared">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="RMES_ID" FieldName="RMES_ID" VisibleIndex="1"
                                                                    Width="80px" SortIndex="1" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="2" Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="SN" FieldName="GHTM" VisibleIndex="3" Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="工作日期" FieldName="Gzrq" VisibleIndex="3" Width="100px">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
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
                    <tr>
                        <td colspan="4">
                            <table>
                                <tr>
                                    <td style="width: 80%">
                                        <fieldset style="text-align: left;">
                                            <legend><span style="font-size: 10pt; width: auto">
                                                <asp:Label ID="Label7" runat="server" Text="统计" Font-Bold="True"></asp:Label></span></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td style="text-align:left">
                                                        <dx:ASPxGridView ID="fpSpCal" ClientInstanceName="grid21" runat="server" AutoGenerateColumns="False"
                                                            Settings-ShowGroupPanel="false" KeyFieldName="SO" Settings-ShowHorizontalScrollBar="true"
                                                            Width="100%">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="200px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" Width="200px">
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
                                    <td style="width: 80%">
                                        <fieldset style="text-align: left;">
                                            <legend><span style="font-size: 10pt; width: auto">
                                                <asp:Label ID="Label11" runat="server" Text="统计" Font-Bold="True"></asp:Label></span></legend>
                                            <table width="95%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <dx:ASPxGridView ID="fpSpCal2" ClientInstanceName="grid22" runat="server" AutoGenerateColumns="False"
                                                            Settings-ShowGroupPanel="false" KeyFieldName="SO">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="200px">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" Width="200px">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <Settings ShowFooter="True" />
                                                            <TotalSummary>
                                                                <dx:ASPxSummaryItem FieldName="S1" SummaryType="Sum" DisplayFormat="总数={0}" />
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
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>
</asp:Content>
