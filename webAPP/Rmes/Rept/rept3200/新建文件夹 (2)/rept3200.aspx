<%@ Page Title="在制品物料清单" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="rept3200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept3200.rept3200" %>

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

<%--在制品物料清单 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        //调序所需
        function GetDataCallback(s) {
            //alert("ok");
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

        //调序前获取按钮及其他所需信息
        function changeSeq(s, e) {
            //alert("OK");
            index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);
        }

        //站点序列tab页在左右list之间移动
        function addStationItems() {
            ListBoxUsed.SelectAll();
            NewMoveSelectedItems(ListBoxUnused, ListBoxUsed, 'A');
            ListBoxUsed.UnselectAll();
            ListBoxUnused.UnselectAll();
        }

        function deleteStationItems() {
            NewMoveSelectedItems(ListBoxUsed, ListBoxUnused, 'D');
        }

        //在制品物料清单tab页数据在左右list之间移动
        function addItems() {
            lbChoosen.SelectAll();
            NewMoveSelectedItems(lbAvailable, lbChoosen, 'A');
            lbChoosen.UnselectAll();
            lbAvailable.UnselectAll();
        }

        function deleteItems() {
            NewMoveSelectedItems(lbChoosen, lbAvailable, 'D');
        }

        function NewMoveSelectedItems(srcListBox, dstListBox, type1) {
            srcListBox.BeginUpdate();
            dstListBox.BeginUpdate();
            var items = srcListBox.GetSelectedItems();
            var items2 = dstListBox.GetSelectedItems();
            if (type1 == 'A') {
                for (var i = items.length - 1; i >= 0; i = i - 1) {
					var istrue = false;
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

        function AddAllItems() {
            //先清空一下lbChoosen
            lbChoosen.ClearItems();
            MoveAllItems(lbAvailable, lbChoosen);
        }

        function deleteAllItems() {
            lbAvailable.ClearItems();
            lbChoosen.ClearItems();
            //没必要传参数，直接重新查询
            lbAvailable.PerformCallback();
        }

        function MoveAllItems(srcListBox, dstListBox) {
            srcListBox.BeginUpdate();
            var count = srcListBox.GetItemCount();
            for (var i = 0; i < count; i++) {
                var item = srcListBox.GetItem(i);
                dstListBox.AddItem(item.text, item.value);
            }
            srcListBox.EndUpdate();
            //srcListBox.ClearItems();
        }

    </script>
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="1">
        <TabPages>
            <dx:TabPage Text="站点序列" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode1" TextField="PLINE_NAME"
                                        ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode">
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查询" Width="50px" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){
                                            ListBoxUsed.ClearItems();
                                            ListBoxUnused.PerformCallback();
                                            grid.PerformCallback();
                                            }"/>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td colspan="2" rowspan="5">
                                    <dx:ASPxListBox ID="listZD" runat="server" ClientInstanceName="ListBoxUnused" SelectionMode="CheckColumn" 
                                        Width="210px" Height="200px" ValueField="STATION_CODE" ValueType="System.String"
                                        OnCallback="listZD_Callback">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="STATION_CODE" Caption="站点" Width="100%" />
                                        </Columns>
                                    </dx:ASPxListBox>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td height="200px" rowspan="5">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text=">>" Border-BorderStyle="None" AutoPostBack="False"
                                                    ToolTip="Add selected items" ClientInstanceName="btnMoveSelectedItemsToRight">
                                                    <ClientSideEvents Click="function(s, e) { addStationItems(); }" />
                                                    <Border BorderStyle="None"></Border>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ASPxButton4" runat="server" Text="<<" Border-BorderStyle="None" AutoPostBack="false"
                                                    ClientInstanceName="btnMoveSelectedItemsToLeft" ToolTip="Remove selected items">
                                                    <ClientSideEvents Click="function(s, e) { deleteStationItems(); }" />
                                                    <Border BorderStyle="None"></Border>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="已选">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td colspan="4" height="200px" rowspan="5">
                                    <dx:ASPxListBox ID="ListInsert" runat="server" ClientInstanceName="ListBoxUsed"
                                        SelectionMode="CheckColumn" Width="210px" Height="200px" ValueField="STATION_CODE" ValueType="System.String">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="STATION_CODE" Caption="站点" Width="100%" />
                                        </Columns>
                                    </dx:ASPxListBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="插入" AutoPostBack="false" OnClick="btnAdd_Click" >
                                        <%--<ClientSideEvents Click="function(s, e){
                                            grid.PerformCallback('插入');
                                            ListBoxUsed.ClearItems();
                                            }"/>--%>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="ROWID"
                            OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
                            OnRowDeleting="ASPxGridView1_RowDeleting" >
                            <Columns>
                                <dx:GridViewCommandColumn Caption="调序" VisibleIndex="0" Width="80px" ButtonType="Image">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="Up">
                                            <Image Url="../../Pub/Images/Up.png" Width="15px" ToolTip="向前调整" />
                                        </dx:GridViewCommandColumnCustomButton>
                                        <dx:GridViewCommandColumnCustomButton ID="Down">
                                            <Image Url="../../Pub/Images/Down.png" Width="15px" ToolTip="向后调整">
                                            </Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="1" Width="120px">
                                    <DeleteButton Visible="True" Text="删除">
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="ROWID" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="顺序" FieldName="SX" VisibleIndex="2"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="站点代码" FieldName="ZDDM" VisibleIndex="3"
                                    Width="150px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <ClientSideEvents CustomButtonClick="function (s,e){
                                changeSeq(s,e);
                                }" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="在制品物料清单" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode1" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                                        ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode">
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <%-- 加上AutoPostBack="false"，阻止自动提交刷新整个页面 --%>
                                    <dx:ASPxButton ID="ASPxButton5" runat="server" Text="查询" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e){
                                            lbAvailable.PerformCallback();
                                            }"/>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <%--<td style="width: 5px" rowspan="5">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="L1" runat="server" Text="未选">
                                    </dx:ASPxLabel>
                                </td>--%>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td rowspan="5">
                                    <dx:ASPxListBox ID="ASPxListBoxUnused" runat="server"  ClientInstanceName="lbAvailable" SelectionMode="CheckColumn" Width="280px" 
                                        Height="230px" ValueField="GHTM" ValueType="System.String" ViewStateMode="Inherit"  TextField="GHTM"
                                        OnCallback="ASPxListBoxUnused_Callback" >
                                        <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />--%>
                                    </dx:ASPxListBox>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td height="200px" rowspan="5">
                                    <div>
                                        <dx:ASPxButton ID="ASPxButton6" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                                            AutoPostBack="False" Text="增加 >" Width="130px" Height="23px" ClientEnabled="true"
                                            ToolTip="Add selected items">
                                            <ClientSideEvents Click="function(s, e) { addItems(); }" />
                                        </dx:ASPxButton>
                                    </div>
                                    <div class="TopPadding">
                                        <dx:ASPxButton ID="ASPxButton7" runat="server" ClientInstanceName="btnMoveAllItemsToRight"
                                            AutoPostBack="False" Text="增加全部 >>" Width="130px" Height="23px" ToolTip="Add all items">
                                            <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                                        </dx:ASPxButton>
                                    </div>
                                    <div style="height: 32px">
                                    </div>
                                    <div>
                                        <dx:ASPxButton ID="ASPxButton8" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                                            AutoPostBack="False" Text="< 删除" Width="130px" Height="23px" ClientEnabled="true"
                                            ToolTip="Remove selected items">
                                            <ClientSideEvents Click="function(s, e) { deleteItems(); }" />
                                        </dx:ASPxButton>
                                    </div>
                                    <div class="TopPadding">
                                        <dx:ASPxButton ID="ASPxButton9" runat="server" ClientInstanceName="btnMoveAllItemsToLeft"
                                            AutoPostBack="False" Text="<< 删除全部" Width="130px" Height="23px" ClientEnabled="true"
                                            ToolTip="Remove all items">
                                            <ClientSideEvents Click="function(s, e) { deleteAllItems(); }" />
                                        </dx:ASPxButton>
                                    </div>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td height="200px" rowspan="5">
                                    <dx:ASPxListBox ID="ASPxListBoxUsed" runat="server" ClientInstanceName="lbChoosen" Width="280px" 
                                        Height="230px" SelectionMode="CheckColumn">
                                        <%--<ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }">
                                        </ClientSideEvents>--%>
                                    </dx:ASPxListBox>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="queryBill" runat="server" Text="查询物料清单" AutoPostBack="false">
                                                    <ClientSideEvents Click="function(s, e){
                                                        grid2.PerformCallback();
                                                        }"/>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnXlsExportBomList" runat="server" Text="导出物料清单" UseSubmitBehavior="False" OnClick="btnXlsExport_Bom_List">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                            OnCustomCallback="ASPxGridView2_CustomCallback" >
                            <%--<SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>--%>
                            <%--<ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
                                {
                                    callbackName = grid.cpCallbackName;
                                    theRet = grid.cpCallbackRet;
                                    if(callbackName == 'Delete') 
                                    {
                                        alert(theRet);
                                    }
                                }" />--%>
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px" Visible="false">
                                    <EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="零件号" FieldName="ABOM_COMP" VisibleIndex="1"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数量" FieldName="SUM" VisibleIndex="2" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                        </dx:ASPxGridViewExporter>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
     <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlCode1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
