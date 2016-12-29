<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Theme1"
    CodeBehind="rept1500.aspx.cs" Inherits="Rmes_rept1500" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%--功能概述：重要零件装配查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (CHOSE.GetValue() == null) {
                return;
            }
            var webFileUrl = "?CHOSE=" + CHOSE.GetValue() + "&opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            DetectPanel.PerformCallback();

        }
       
        
                function AddSelectedItems() {
                    ListBoxUsed.SelectAll();
                    MoveSelectedItems(ListBoxUnused, ListBoxUsed, 'A');
                    ListBoxUsed.UnselectAll();
                    ListBoxUnused.UnselectAll();
                }
                function RemoveSelectedItems() {

                    MoveSelectedItems(ListBoxUsed, ListBoxUnused, 'D');

                }
        function MoveSelectedItems(srcListBox, dstListBox, type1) {
                        srcListBox.BeginUpdate();
                        dstListBox.BeginUpdate();
//            var items = srcListBox.GetSelectedItems();
//            for (var i = items.length - 1; i >= 0; i = i - 1) {
//                dstListBox.AddItem(items[i].text, items[i].value);
//                srcListBox.RemoveItem(items[i].index);
            //            }
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
    </script>
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="3">
        <TabPages>
            <%--<dx:TabPage Text="零件种类" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
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
                                        TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode" SelectedIndex="0">
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="检测名称" Width="60px">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtIKind" runat="server" ValueType="System.String" Width="100px">
                                        <Items>
                                            <dx:ListEditItem Text="缸体号" Value="A" />
                                            <dx:ListEditItem Text="空压机号" Value="B" />
                                            <dx:ListEditItem Text="曲轴号" Value="C" />
                                            <dx:ListEditItem Text="燃油泵号" Value="D" />
                                            <dx:ListEditItem Text="凸轮轴号" Value="E" />
                                            <dx:ListEditItem Text="增压器号" Value="F" />
                                            <dx:ListEditItem Text="缸盖号" Value="G" />
                                            <dx:ListEditItem Text="起动机号" Value="H" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="检测结果">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtICode" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询">
                                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }"></ClientSideEvents>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" OnCustomCallback="ASPxGridView1_CustomCallback"   KeyFieldName="SN">
                            <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                
                                    callbackName =  grid.cpCallbackName;
                                    theRet = grid.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
                            <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid.cpCallbackName = &#39;&#39;;
                                }" EndCallback="function(s, e) 
                                {
                                
                                    callbackName =  grid.cpCallbackName;
                                    theRet = grid.cpCallbackRet;
                                    if(callbackName == &#39;Fail&#39;) 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == &#39;OK&#39;) 
                                    {
                                         
                                    }
                                   
                                }"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" VisibleIndex="2" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="测量数据" FieldName="DETECT_VALUE" VisibleIndex="3"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>--%>
            <%--<dx:TabPage Text="零件号" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode1" runat="server" ValueType="System.String" Width="100px"
                                        TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode1" SelectedIndex="0">
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="text-align: left;">
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="时间:" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="100px">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="零件编号">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                <dx:ASPxTextBox ID="txtPartCode" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                     
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="查询">
                                        <ClientSideEvents Click="function(s,e){  grid2.PerformCallback();   }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                            Width="100%" OnCustomCallback="ASPxGridView2_CustomCallback" KeyFieldName="SN">
                            <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid2.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                
                                    callbackName =  grid2.cpCallbackName;
                                    theRet = grid2.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" VisibleIndex="2"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="3"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="5"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="时间" FieldName="CREATE_TIME" VisibleIndex="6"
                                    Width="140px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="员工" FieldName="USER_NAME" VisibleIndex="8"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="地点" FieldName="PLINE_CODE" VisibleIndex="9" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" VisibleIndex="4"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="供应商" FieldName="VENDOR_CODE" VisibleIndex="11"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件流水号" FieldName="ITEM_SN" VisibleIndex="12"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>--%>
            <dx:TabPage Text="测量项目" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                <dx:ASPxTextBox ID="txtChose" runat="server" ValueType="System.String" Width="220px"
                                    ClientInstanceName="CHOSE">
                                    <ClientSideEvents  TextChanged="function(s, e) { initEditSeries(s, e); }" />
                                </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="L1" runat="server" Text="未选">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td>
                                     
                                    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel4" ClientInstanceName="DetectPanel"
                                        OnCallback="ASPxCallbackPanel4_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent4" runat="server">
                                                <dx:ASPxListBox ID="ASPxListBoxUnused" runat="server" ClientInstanceName="ListBoxUnused"
                                                    SelectionMode="CheckColumn" Width="220px" Height="240px" ValueField="DETECT_NAME"
                                                    ValueType="System.String">
                                                    <Columns>
                                                        <dx:ListBoxColumn FieldName="DETECT_NAME" Caption="检测数据名称" Width="100%" />
                                                    </Columns>
                                                </dx:ASPxListBox>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td height="200px" rowspan="5">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ASPxBT1" runat="server" Text=">>" Border-BorderStyle="None" AutoPostBack="False"
                                                    ToolTip="Add selected items" ClientInstanceName="btnMoveSelectedItemsToRight">
                                                    <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                                                    <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }"></ClientSideEvents>
                                                    <Border BorderStyle="None"></Border>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="ASPxBT2" runat="server" Text="<<" Border-BorderStyle="None" AutoPostBack="false"
                                                    ClientInstanceName="btnMoveSelectedItemsToLeft" ToolTip="Remove selected items">
                                                    <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                                                    <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }"></ClientSideEvents>
                                                    <Border BorderStyle="None"></Border>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="已选">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 5px" rowspan="5">
                                </td>
                                <td>
                                     
                                    <dx:ASPxListBox ID="ASPxListBoxUsed" runat="server" ClientInstanceName="ListBoxUsed"
                                        SelectionMode="CheckColumn" Width="220px" Height="240px" ValueField="DETECT_NAME"
                                        ValueType="System.String">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="DETECT_NAME" Caption="检测数据名称" Width="100%" />
                                        </Columns>
                                    </dx:ASPxListBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode2" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="PCODE" TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode2"
                                        SelectedIndex="0">
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="站点:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtStation" runat="server" ValueType="System.String" Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="text-align: left;">
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="时间:" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="130px">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="--" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit4" runat="server" Width="130px">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="查询" Width="100px">
                                        <ClientSideEvents Click="function(s,e){ grid3.PerformCallback();  }" />
                                        <ClientSideEvents Click="function(s,e){ grid3.PerformCallback();  }"></ClientSideEvents>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="grid3" runat="server" AutoGenerateColumns="True"
                            Width="100%" OnCustomCallback="ASPxGridView3_CustomCallback" KeyFieldName="SO;流水号">
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <%--  <dx:TabPage Text="站点" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl4" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td style="text-align: left;">
                                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="时间:" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit5" runat="server" Width="100px">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="--" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit6" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="站点名称：">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtStation" runat="server" Width="100px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="查询">
                                        <ClientSideEvents Click="function(s,e){grid4.PerformCallback(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView4" ClientInstanceName="grid4" runat="server" AutoGenerateColumns="True"
                            Width="100%" OnCustomCallback="ASPxGridView4_CustomCallback" KeyFieldName="SO;流水号">
                            <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid4.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                
                                    callbackName =  grid4.cpCallbackName;
                                    theRet = grid4.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>--%>
        </TabPages>
    </dx:ASPxPageControl>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlICode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <%--  <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
    </dx:ASPxCallback>--%>
</asp:Content>
