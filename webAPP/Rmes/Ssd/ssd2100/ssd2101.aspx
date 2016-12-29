<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="ssd2101.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd2100.ssd2101" %>

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

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function filterLocation() {
            //初始化计划列表
            if (listPline.GetValue() == null) {
                alert("请选择生产线！");
                return;
            }
            if (BeginDate.GetDate() == null) {
                alert("请选择日期！");
                return;
            }
            var pline = listPline.GetValue().toString();
            var begindate1 = BeginDate.GetDate().toLocaleString();
            listBoxLSH1.ClearItems();
            listBoxLSH1.PerformCallback(pline + "," + begindate1);
            grid.PerformCallback();
        }
        function filterLsh2() {
            //添加全天计划
            if (listPline.GetValue() == null) {
                alert("请选择生产线！");
                return;
            }
            if (BeginDate.GetDate() == null) {
                alert("请选择日期！");
                return;
            }
            var pline = listPline.GetValue().toString();
            var begindate1 = BeginDate.GetDate().toLocaleString();
            listBoxLSH2.ClearItems();
            listBoxLSH2.PerformCallback("ALL," + pline + "," + begindate1);
        }
        function filterLshW() {
            //添加未转换BOM计划
            if (listPline.GetValue() == null) {
                alert("请选择生产线！");
                return;
            }
            if (BeginDate.GetDate() == null) {
                alert("请选择日期！");
                return;
            }
            var pline = listPline.GetValue().toString();
            var begindate1 = BeginDate.GetDate().toLocaleString();
            listBoxLSH2.ClearItems();
            listBoxLSH2.PerformCallback("ALLW," + pline + "," + begindate1);
        }
        function filterLsh21() {
            //添加单个计划
            var ref = "";
            if (txtPlanCode.GetValue() == null) {
                alert("请输入计划代码！");
                return;
            }
            listBoxLSH2.SelectAll();
            var items = listBoxLSH2.GetSelectedItems();
            var ids = "";
            var _ids = "";
            for (var i = 0; i < items.length; i++) {
                ids = ids + items[i].text + "|";
            }
            _ids = ids;
            var plan = txtPlanCode.GetValue().toUpperCase();
            ListBoxPanel.PerformCallback("ADD," + plan + "," + _ids);
            listBoxLSH2.UnselectAll();
            //            ref = 'ADD' + '@' + plan;
            //            CallbackSubmit.PerformCallback(ref);
        }
        function filterLsh3() {
            //提交
            var ref = "";
            butSubmit.SetEnabled(false);
            listBoxLSH2.SelectAll();
            var items = listBoxLSH2.GetSelectedItems();
            if (items.length == 0) {
                alert("请选择待转BOM计划！");
                butSubmit.SetEnabled(true);
                return;
            }
            var ids = "";
            var _ids = "";
            for (var i = 0; i < items.length; i++) {
                ids = ids + items[i].text + "|";
            }
            _ids = ids;
            //            if(ids.endWith('|'))
            //                _ids = ids.substring(0, ids.length - 1);
            ref = 'Commit' + '@' + _ids;
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
                    listBoxLSH2.ClearItems();
                    grid.PerformCallback();
                    return;
                case "Fail":
                    alert(retStr);
                    butSubmit.SetEnabled(true);
                    return;
            }
        }
        function AddSelectedItems() {
            listBoxLSH2.SelectAll();
            MoveSelectedItems(listBoxLSH1, listBoxLSH2, 'A');
            listBoxLSH2.UnselectAll();
            //            UpdateButtonState();
        }
        function RemoveSelectedItems() {
            MoveSelectedItems(listBoxLSH2, listBoxLSH1, 'D');
            //            UpdateButtonState();
        }
        function MoveSelectedItems(srcListBox, dstListBox, type1) {
            srcListBox.BeginUpdate();
            dstListBox.BeginUpdate();
            var items = srcListBox.GetSelectedItems();
            var items2 = dstListBox.GetSelectedItems();
            var istrue = false;
            if (type1 == 'A') {
                for (var i = items.length - 1; i >= 0; i = i - 1) {
                    for (var j = items2.length - 1; j >= 0; j = j - 1) {
                        //判断添加项是否重复
                        if (items[i].text == items2[j].text) {
                            istrue = true;
                        }
                    }
                    if (istrue == false) {
                        dstListBox.AddItem(items[i].text, items[i].value);
                    }
                    //srcListBox.RemoveItem(items[i].index);
                }
            }
            if (type1 == 'D') {
                for (var i = items.length - 1; i >= 0; i = i - 1) {
                    //                dstListBox.AddItem(items[i].text, items[i].value);
                    srcListBox.RemoveItem(items[i].index);
                }
            }

            srcListBox.EndUpdate();
            dstListBox.EndUpdate();
        }
    </script>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel5" ClientInstanceName="ListBoxPanel"
        OnCallback="ASPxCallbackPanel5_Callback">
        <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                ListBoxPanel.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName = ListBoxPanel.cpCallbackName;
                                    theRet = ListBoxPanel.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   listBoxLSH1.PerformCallback();
                                }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent5" runat="server">
                <table style="background-color: #99bbbb; width: 100%;">
                    <tr>
                        <td style="width: 5px; height: 25px;">
                        </td>
                        <td style="width: 60px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxComboBox ID="ASPxComboBoxPline" ClientInstanceName="listPline" runat="server"
                                Width="100px" AutoPostBack="false">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    filterLocation();
                                }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="text-align: left; width: 60px;">
                            <label style="font-size: small">
                                开始日期</label>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" EditFormatString="yyyy-MM-dd"
                                ClientInstanceName="BeginDate" Width="150px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxButton ID="ButSubmit" Text="查询计划" Width="120px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) { filterLocation(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxButton ID="ASPxButton1" Text="添加全天计划" Width="150px" AutoPostBack="false"
                                runat="server">
                                <ClientSideEvents Click="function(s, e) { filterLsh2(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px; height: 25px;">
                        </td>
                        <td style="width: 80px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="手动添加计划">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="2" style="width: 150px;">
                            <dx:ASPxTextBox ID="ASPxTextPlanCode" ClientInstanceName="txtPlanCode" runat="server"
                                Width="150px" />
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxButton ID="ASPxButton3" Text="手工新增计划" Width="150px" AutoPostBack="false"
                                runat="server">
                                <ClientSideEvents Click="function(s, e) { filterLsh21(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton5" Text="未转换BOM" Width="120px" AutoPostBack="false"
                                runat="server">
                                <ClientSideEvents Click="function(s, e) { filterLshW(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td align="center">
                            <%--<dx:ASPxButton ID="ASPxButton2" Text="开始转换" Width="90px" AutoPostBack="false" runat="server" >
                    <ClientSideEvents Click="function(s, e) { filterLocation(); }" />
                </dx:ASPxButton>--%>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                </table>
                <table style="background-color: #99bbbb; width: 100%;">
                    <tr style="height: 230px">
                        <td style="width: 5px">
                        </td>
                        <td style="width: 80px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="未转换计划">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 350px">
                            <dx:ASPxListBox ID="ASPxListBoxLocation" runat="server" ClientInstanceName="listBoxLSH1"
                                SelectionMode="CheckColumn" Width="350px" Height="230px" ValueField="RMES_ID"
                                ValueType="System.String" OnCallback="listBoxLSH1_Callback" ViewStateMode="Inherit">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="RMES_ID" Caption="id" Width="0%" Visible="false" />
                                    <dx:ListBoxColumn FieldName="PLAN_CODE" Caption="计划代码" Width="40%" />
                                    <dx:ListBoxColumn FieldName="PLAN_SO" Caption="SO" Width="35%" />
                                    <dx:ListBoxColumn FieldName="PLAN_QTY" Caption="计划数量" Width="20%" />
                                    <dx:ListBoxColumn FieldName="PLINE_CODE" Caption="生产线" Width="20%" />
                                </Columns>
                            </dx:ASPxListBox>
                        </td>
                        <td style="width: 20px;" height="200px" rowspan="5">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxBT1" runat="server" Text=">>" Border-BorderStyle="None" AutoPostBack="False"
                                            ToolTip="Add selected items" ClientInstanceName="btnMoveSelectedItemsToRight">
                                            <%--<ClientSideEvents Click="function(s,e) { initListBoxUsed(s,e); }" />--%>
                                            <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                                            <Border BorderStyle="None"></Border>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxBT2" runat="server" Text="<<" Border-BorderStyle="None" AutoPostBack="false"
                                            ClientInstanceName="btnMoveSelectedItemsToLeft" ToolTip="Remove selected items">
                                            <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                                            <Border BorderStyle="None"></Border>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 80px; text-align: right;">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="已选择计划">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 350px">
                            <dx:ASPxListBox ID="ASPxListBox1" runat="server" ClientInstanceName="listBoxLSH2"
                                SelectionMode="CheckColumn" Width="350px" Height="230px" ValueField="ID1"
                                ValueType="System.String" ViewStateMode="Inherit" OnCallback="ASPxListBox1_Callback">
                                <Columns>
                                    <dx:ListBoxColumn Caption="计划信息" FieldName="ID1" Width="90%" />
                                    <%--                        <dx:ListBoxColumn FieldName="RMES_ID" Caption="计划信息" Width="0%"  Visible="false"/>
                        <dx:ListBoxColumn FieldName="PLAN_CODE" Caption="计划代码" Width="40%" />
                        <dx:ListBoxColumn FieldName="PLAN_SO" Caption="SO" Width="35%" />
                        <dx:ListBoxColumn FieldName="PLAN_QTY" Caption="计划数量" Width="20%" />--%>
                                </Columns>
                            </dx:ASPxListBox>
                        </td>
                        <td style="width: 50px; text-align: left;">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton6" Text="开始转换" Width="90px" AutoPostBack="false" runat="server" ClientInstanceName="butSubmit">
                                            <ClientSideEvents Click="function(s, e) { filterLsh3(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="清空" Border-BorderStyle="None"
                                            AutoPostBack="false" ClientInstanceName="btnMoveSelectedItemsToLeft" ToolTip="清空">
                                            <ClientSideEvents Click="function(s, e) { listBoxLSH2.ClearItems(); }" />
                                            <Border BorderStyle="None"></Border>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                </table>
                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID"
                    AutoGenerateColumns="False" OnCustomCallback="ASPxGridView1_CustomCallback">
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ExportMode="All" />
                    <Settings ShowFooter="True" />
                    <%--         <TotalSummary>
            <dx:ASPxSummaryItem FieldName="PLAN_QTY" SummaryType="Sum" DisplayFormat="总数={0}"/>
            <dx:ASPxSummaryItem FieldName="ONLINE_QTY" SummaryType="Sum" DisplayFormat="上线数={0}"/>
            <dx:ASPxSummaryItem FieldName="OFFLINE_QTY" SummaryType="Sum" DisplayFormat="下线数={0}"/>
        </TotalSummary>--%>
                    <Columns>
                        <dx:GridViewCommandColumn Name="CommondColumn" VisibleIndex="0" Width="40px" Caption=" ">
                            <ClearFilterButton Visible="true">
                            </ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
                        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" Width="100px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划SO" FieldName="JHSO" Width="100px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Width="60px" CellStyle-HorizontalAlign="Center"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="提交用户" FieldName="YHMC" Width="100px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="提交时间" FieldName="TJSJ" Width="150px" CellStyle-Wrap="False"
                            PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" Settings-AllowHeaderFilter="True">
<PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>

                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="计算开始时间" FieldName="KSSJ" Width="150px" CellStyle-Wrap="False"
                            PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" Settings-AllowHeaderFilter="True">
<PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>

                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="计算结束时间" FieldName="JSSJ" Width="150px" CellStyle-Wrap="False"
                            PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" Settings-AllowHeaderFilter="True">
<PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss"></PropertiesDateEdit>

                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="转换标识" FieldName="IFZH" Width="80px" Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="转换状态" FieldName="ZHBZ" Width="150px" Settings-AllowHeaderFilter="True">
                             <Settings AutoFilterCondition="Contains" />
                             <Settings AllowHeaderFilter="True"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="分装计划" FieldName="IFZC" Width="70px" CellStyle-Wrap="False"
                            Settings-AllowHeaderFilter="True">
                            <Settings AutoFilterCondition="Contains" />
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " Width="80%" />
                    </Columns>
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <SettingsEditing PopupEditFormWidth="600px" />
                    <Settings ShowHorizontalScrollBar="true" />
                    <SettingsDetail ShowDetailRow="true"/>
                    <Templates>
            <DetailRow>
                 <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExportPlanBOM" runat="server" Text="导出转换失败物料清单-EXCEL" UseSubmitBehavior="False" OnClick="btnXlsExport_Click2" />
                        </td>
                    </tr>
                </table>
            <table  width="100%">
                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                    <TabPages>
                        <dx:TabPage Text="转换失败物料清单" Visible="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" Width="100%">
                                    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" Width="1450px" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
                                        Settings-ShowGroupPanel="false" Settings-ShowHeaderFilterButton="false" Settings-ShowVerticalScrollBar="false"
                                        OnBeforePerformDataSelect="ASPxGridView2_DataSelect" Visible="true">
                                        <Settings ShowHorizontalScrollBar="true" />
                                        <Columns>
                                            <dx:GridViewCommandColumn Caption="清除" Width="40px">
                                                <ClearFilterButton Visible="True" />
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="RMES_ID" Width="130px" Visible="false" />
                                            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" Width="130px" />
                                            <dx:GridViewDataTextColumn Caption="计划SO" FieldName="PLAN_SO" Width="130px"  Visible="false" />
                                            <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" Width="100px" />
                                            <dx:GridViewDataTextColumn Caption="站点代码" FieldName="STATION_CODE" Width="100px" />
                                            <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="工序代码" FieldName="PROCESS_CODE" Width="60px" />

                                            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" Width="60px" />
                                            <dx:GridViewDataTextColumn Caption="物料数量" FieldName="ITEM_QTY" Width="60px" />
                                            <dx:GridViewDataTextColumn Caption="物料重要级别代码" FieldName="ITEM_CLASS" Width="100px" />
                                            <dx:GridViewDataTextColumn Caption="物料类型" FieldName="ITEM_TYPE" Width="100px" />

                                            <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="VENDOR_CODE" Width="100px" />
                                            <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="用户ID" FieldName="CREATE_USERID" Width="60px"  />
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
                </table>
            </DetailRow>
        </Templates>
                </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="ASPxGridView2">
    </dx:ASPxGridViewExporter>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>

</asp:Content>

