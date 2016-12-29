<%@ Page Title="在制品查询" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="rept3100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept3100.rept3100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%--在制品查询 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function AddSelectedItems() {
            MoveSelectedItems(lbAvailable, lbChoosen);
            UpdateButtonState();
        }
        function AddAllItems() {
            //先清空一下List1
            lbChoosen.ClearItems();
            MoveAllItems(lbAvailable, lbChoosen);
            UpdateButtonState();
        }
        function RemoveSelectedItems() {
            MoveSelectedItems(lbChoosen, lbAvailable);
            UpdateButtonState();
        }
        function RemoveAllItems() {
            MoveAllItems(lbChoosen, lbAvailable);
            UpdateButtonState();
        }
        function MoveSelectedItems(srcListBox, dstListBox) {
            srcListBox.BeginUpdate();
            dstListBox.BeginUpdate();
            var items = srcListBox.GetSelectedItems();
            for (var i = items.length - 1; i >= 0; i = i - 1) {
                dstListBox.AddItem(items[i].text, items[i].value);
                srcListBox.RemoveItem(items[i].index);
            }
            srcListBox.EndUpdate();
            dstListBox.EndUpdate();
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
        function UpdateButtonState() {
            btnMoveAllItemsToRight.SetEnabled(true);
            btnMoveAllItemsToLeft.SetEnabled(true);
            btnMoveSelectedItemsToRight.SetEnabled(true);
            btnMoveSelectedItemsToLeft.SetEnabled(true);
        }

        function addItems(s, e) {
            lbChoosen.SelectAll();
            NewMoveSelectedItems(lbAvailable, lbChoosen, 'A');
            lbChoosen.UnselectAll();
            //清空一下lbAvailable的已选
            lbAvailable.UnselectAll();

            //var leftSelectedItems = lbAvailable.GetSelectedItems();
            //if (leftSelectedItems.count == 0 || leftSelectedItems == "" || leftSelectedItems == null) {
            //    alert("请选择要增加的内容！");
            //    return;
            //}
            //lbAvailable.PerformCallback("增加");
        }

        function deleteItems(s, e) {

            NewMoveSelectedItems(lbChoosen, lbAvailable, 'D');

            //var rightSelectedItems = lbChoosen.GetSelectedItems();
            //if (rightSelectedItems.count == 0 || rightSelectedItems == "" || rightSelectedItems == null) {
            //    alert("请选择要删除的内容！");
            //    //不写return后面的会继续执行
            //    return;
            //}
            //lbChoosen.PerformCallback();
            ////有关吗
            //return;
            //因为取不到items，所以换方法
            ////写在这里就取不到选中的item了
            ////lbChoosen.PerformCallback();
            ////取不到items
            //var leftItems = lbAvailable.items;
            //var rightItems = lbChoosen.items;
            //var items = "",items1 = "",items2 = "",items3 = "";
            //items1 = leftItems[0].text;
            //for (var i = 1; i < leftItems.count; i++) {
            //    items += "&" + leftItems[i].text;
            //}
            //items2 = rightItems[0].text;
            //for (var i = 1; i < rightItems.count; i++) {
            //    items += "&" + rightItems[i].text;
            //}
            //items3 = rightSelectedItems[0].text;
            //for (var i = 1; i < rightSelectedItems.count; i++) {
            //    items += "&" + rightSelectedItems[i].text;
            //}
            //items = items1 + "@" + items2 + "@" + items3;
            ////这样写才能访问后台的callback事件，里面可以加参数，不知道能加几个？
            //lbChoosen.PerformCallback(items);
        }

        function addAllItems(s, e) {
            lbAvailable.ClearItems();
            lbChoosen.ClearItems();
            //没必要传参数，直接重新查询
            lbChoosen.PerformCallback();
        }

        function deleteAllItems(s, e) {
            lbAvailable.ClearItems();
            lbChoosen.ClearItems();
            //没必要传参数，直接重新查询
            lbAvailable.PerformCallback("查询计划");
        }

        function NewMoveSelectedItems(srcListBox, dstListBox, type1) {
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

        //数据在list之间移动
        //function AddSelectedItems() {

        //    MoveSelectedItems(ListBoxUnused, ListBoxUsed, 'A');

        //}
        //function RemoveSelectedItems() {

        //    MoveSelectedItems(ListBoxUsed, ListBoxUnused, 'D');

        //}
        //function MoveSelectedItems(srcListBox, dstListBox, type1) {
        //    srcListBox.BeginUpdate();
        //    dstListBox.BeginUpdate();
        //    var items = srcListBox.GetSelectedItems();
        //    for (var i = items.length - 1; i >= 0; i = i - 1) {
        //        dstListBox.AddItem(items[i].text, items[i].value);
        //        srcListBox.RemoveItem(items[i].index);
        //    }

        //    srcListBox.EndUpdate();
        //    dstListBox.EndUpdate();
        //}
    </script>

    <table>
        <tr>
            <td style="height:30px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="查询条件"></dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="地点:"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="txtPCode" Width="100px" runat="server" DataSourceID="sqlGzdd" ValueField="PLINE_CODE" TextField="PLINE_NAME">
                            <ClientSideEvents SelectedIndexChanged=" function(s,e){
                            cmbzd.PerformCallback();
                            }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="日期:"></dx:ASPxLabel>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxDateEdit ID="DTPicker1" runat="server" EditFormatString="yyyy-MM-dd"
                                Width="120px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="到"></dx:ASPxLabel>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxDateEdit ID="DTPicker2" runat="server" EditFormatString="yyyy-MM-dd"
                                Width="120px">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO:"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtSO" runat="server" Width="120px"></dx:ASPxTextBox>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查询计划" Width="100px" AutoPostBack="false" >
                                <ClientSideEvents Click="function(s, e){
                                    lbChoosen.ClearItems();
                                    lbAvailable.PerformCallback('查询计划');
                                    }
                                " />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <%--<td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="计划号:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="cmbJhdm" runat="server" DataSourceID="sqlJhdm" TextField="PLAN_CODE"></dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxButton ID="Command2" runat="server" Text="ALL">
                    <ClientSideEvents Click="function(s, e){
                        grid.PerformCallback();
                        }"/>
                </dx:ASPxButton>
            </td>--%>
            <td>
                <table>
                    <tr>
                        <td colspan="2" rowspan="4">
                            <dx:ASPxListBox ID="listJhdm" runat="server" ClientInstanceName="lbAvailable" SelectionMode="CheckColumn" Width="280px" 
                                Height="230px" ValueField="PLAN_CODE" ValueType="System.String" ViewStateMode="Inherit"  TextField="PLAN_CODE"
                                OnCallback="listJhdm_Callback">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />
                            </dx:ASPxListBox>
                            <%--<dx:ASPxListBox ID="listJhdm" runat="server" ClientInstanceName="ListBoxUnused"
                                SelectionMode="CheckColumn" 
                                Width="210px" Height="200px" ValueField="PLAN_CODE" TextField="PLAN_CODE" ValueType="System.String">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="PLAN_CODE" Caption="计划号" Width="100%" />
                                </Columns>
                            </dx:ASPxListBox>--%>
                        </td>
                        <td style="width: 5px" rowspan="4">
                        </td>
                        <td height="200px" rowspan="4">
                            <div>
                                <dx:ASPxButton ID="ASPxButton2" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                                    AutoPostBack="False" Text="增加 >" Width="130px" Height="23px" ClientEnabled="true"
                                    ToolTip="Add selected items">
                                    <%--<ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />--%>
                                    <ClientSideEvents Click="function(s, e){
                                        addItems(s, e);
                                        }"/>
                                </dx:ASPxButton>
                            </div>
                            <div class="TopPadding">
                                <dx:ASPxButton ID="ASPxButton3" runat="server" ClientInstanceName="btnMoveAllItemsToRight"
                                    AutoPostBack="False" Text="增加全部 >>" Width="130px" Height="23px" ToolTip="Add all items">
                                    <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                                    <%--<ClientSideEvents Click="function(s, e) { addAllItems(s, e); }" />--%>
                                </dx:ASPxButton>
                            </div>
                            <div style="height: 32px">
                            </div>
                            <div>
                                <dx:ASPxButton ID="ASPxButton4" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                                    AutoPostBack="False" Text="< 删除" Width="130px" Height="23px" ClientEnabled="true"
                                    ToolTip="Remove selected items">
                                    <%--<ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />--%>
                                    <ClientSideEvents Click="function(s, e){
                                        deleteItems(s, e);
                                        }"/>
                                </dx:ASPxButton>
                            </div>
                            <div class="TopPadding">
                                <dx:ASPxButton ID="ASPxButton5" runat="server" ClientInstanceName="btnMoveAllItemsToLeft"
                                    AutoPostBack="False" Text="<< 删除全部" Width="130px" Height="23px" ClientEnabled="true"
                                    ToolTip="Remove all items">
                                    <%--<ClientSideEvents Click="function(s, e) { RemoveAllItems(); }" />--%>
                                    <ClientSideEvents Click="function(s, e) { deleteAllItems(s, e); }" />
                                </dx:ASPxButton>
                            </div>
                        </td>
                        <%--<td height="200px" rowspan="4">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton3" runat="server" Text=">>" Border-BorderStyle="None" AutoPostBack="False"
                                            ToolTip="Add selected items" ClientInstanceName="btnMoveSelectedItemsToRight">
                                            <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                                            <Border BorderStyle="None"></Border>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton4" runat="server" Text="<<" Border-BorderStyle="None" AutoPostBack="false"
                                            ClientInstanceName="btnMoveSelectedItemsToLeft" ToolTip="Remove selected items">
                                            <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                                            <Border BorderStyle="None"></Border>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                        <%--<td style="width: 5px" rowspan="4">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="已选">
                            </dx:ASPxLabel>
                        </td>--%>
                        <td style="width: 5px" rowspan="4">
                        </td>
                        <td colspan="4" height="200px" rowspan="4">
                            <dx:ASPxListBox ID="List1" runat="server" ClientInstanceName="lbChoosen" Width="280px" OnCallback="List1_Callback"
                                Height="230px" SelectionMode="CheckColumn" ValueField="PLAN_CODE" TextField="PLAN_CODE">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }">
                                </ClientSideEvents>
                            </dx:ASPxListBox>
                            <%--<dx:ASPxListBox ID="List1" runat="server" ClientInstanceName="lbChoosen" Width="280px"
                                Height="230px" SelectionMode="CheckColumn">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }">
                                </ClientSideEvents>
                            </dx:ASPxListBox>--%>
                            <%--<dx:ASPxListBox ID="List1" runat="server" ClientInstanceName="ListBoxUsed"
                                SelectionMode="CheckColumn" Width="210px" Height="200px" ValueField="PLAN_CODE" TextField="PLAN_CODE" ValueType="System.String">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="PLAN_CODE" Caption="计划号" Width="100%" />
                                </Columns>
                            </dx:ASPxListBox>--%>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="站点:"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbzd" runat="server" 
                                TextField="STATION_CODE" Width="120px" ClientInstanceName="cmbzd" 
                                oncallback="cmbzd_Callback" >
                            
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            <%--？？清除按钮 --%>
                            <dx:ASPxButton ID="queryBill" runat="server" Text="在制品查询" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e){
                                    grid.PerformCallback();
                                    }"/>
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="数量:" Visible="false"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtSL" runat="server" Width="120px" Visible="false"></dx:ASPxTextBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="流水号:" Visible="false"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtGhtm" runat="server" Width="120px" Visible="false"></dx:ASPxTextBox>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="站点类别:" Visible="false"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbqy" runat="server" DataSourceID="sqlqy" TextField="STATION_AREA" Visible="false"></dx:ASPxComboBox>
                        </td>   
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="站点分类:" Visible="false"></dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbfl" runat="server" DataSourceID="sqlfl" TextField="STATION_TYPE" Visible="false"></dx:ASPxComboBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox runat="server" ID="Check1" Text="按台份" Checked="false" Visible="false"></dx:ASPxCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ROWID" ClientInstanceName="grid" OnCustomCallback="ASPxGridView1_CustomCallback" >
        <Columns>
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="RQSJ" VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="机型" FieldName="GGXHMC" VisibleIndex="5" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="站点名称" FieldName="ZDMC" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="班次名称" FieldName="BCMC" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="班组名称" FieldName="BZMC" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="员工名称" FieldName="YGMC" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sqlGzdd" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlJhdm" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    &nbsp;&nbsp;
    <asp:SqlDataSource ID="sqlzd" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlqy" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlfl" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>

</asp:Content>
