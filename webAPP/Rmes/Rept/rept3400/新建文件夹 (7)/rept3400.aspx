<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept3400.aspx.cs" Inherits="Rmes_Rept_rept3400_rept3400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--附装乱序领料单查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function AddItems() {
            if (Iteam.GetValue() == null) {
                alert("请填写要增加的零件号！");
                return;
            }
        }
        function DeleteItems() {
            if (Iteam.GetValue() == null) {
                alert("请填写要删除的零件号！");
                return;
            }
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


        function submitRtr(e) {
            var result = "";
            var retStr = "";
            var array = e.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    alert(retStr);

                    return;
                case "Fail":
                    alert(retStr);

                    return;

            }
        }
       
    </script>
    <table>
        <tr>
            <td style="text-align: left; width: 60px;">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="零件名" />
            </td>
            <td>
                <dx:ASPxTextBox ID="txtIteam" ClientInstanceName="Iteam" runat="server" Width="100px"
                    Height="25px">
                </dx:ASPxTextBox>
            </td>
            <td style="width: 100px;">
                <dx:ASPxButton ID="ASPxButton4" Text="增加" Width="100px" AutoPostBack="false" runat="server"
                    OnClick="btnAdd_Click">
                    <ClientSideEvents Click="function(s, e) {  AddItems();  }" />
                </dx:ASPxButton>
            </td>
            <td style="width: 100px;">
                <dx:ASPxButton ID="ASPxButton1" Text="删除" Width="100px" AutoPostBack="false" runat="server"
                    OnClick="btnDelete_Click">
                    <ClientSideEvents Click="function(s, e) {  DeleteItems();  }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 5px" rowspan="5">
            </td>
            <td>
                <dx:ASPxLabel ID="L1" runat="server" Text="未选">
                </dx:ASPxLabel>
            </td>
            <td style="width: 5px" rowspan="5">
            </td>
            <td colspan="2" rowspan="5">
                <dx:ASPxListBox ID="ASPxListBoxUnused" runat="server" ClientInstanceName="ListBoxUnused"
                    SelectionMode="CheckColumn" Width="210px" Height="200px" ValueField="PART" ValueType="System.String">
                    <Columns>
                        <dx:ListBoxColumn FieldName="PART" Caption="零件名称" Width="100%" />
                    </Columns>
                </dx:ASPxListBox>
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
            <td style="width: 5px" rowspan="5">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="已选">
                </dx:ASPxLabel>
            </td>
            <td style="width: 5px" rowspan="5">
            </td>
            <td colspan="4" height="200px" rowspan="5">
                <dx:ASPxListBox ID="ASPxListBoxUsed" runat="server" ClientInstanceName="ListBoxUsed"
                    SelectionMode="CheckColumn" Width="210px" Height="200px" ValueField="PART" ValueType="System.String"
                    OnCallback="ASPxListBoxUsed_Callback">
                    <Columns>
                        <dx:ListBoxColumn FieldName="PART" Caption="零件名称" Width="100%" />
                    </Columns>
                </dx:ASPxListBox>
            </td>
            <td style="width: 130px" rowspan="5">
            </td>
            <td>
                <fieldset style="text-align: left;">
                    <legend><span style="font-size: 10pt; width: auto">
                        <asp:Label ID="Label6" runat="server" Text="数量统计" Font-Bold="True"></asp:Label></span></legend>
                    <table width="95%">
                        <tr>
                            <td style="text-align: right">
                                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" Width="100%" runat="server"
                                    OnCustomCallback="ASPxGridView2_CustomCallback" KeyFieldName="COMP">
                                    <Columns>
                                        <%--<dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="0" Width="100px">
                                                    </dx:GridViewDataTextColumn>--%>
                                        <dx:GridViewDataTextColumn Caption="零件名" FieldName="UDESC" VisibleIndex="1" Width="120px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="零件号" FieldName="COMP" VisibleIndex="2" Width="120px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="3" Width="60px">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="70%">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowGroupPanel="false" ShowFilterRow="false" VerticalScrollableHeight="150"
                                        ShowVerticalScrollBar="true" ShowHorizontalScrollBar="false" />
                                    <%--  <SettingsPager PageSize="10">
                                                </SettingsPager>--%>
                                    <SettingsBehavior AllowSort="False" />
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <%-- <td colspan="4" height="200px" rowspan="5">--%>
    <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="80px" ClientInstanceName="PCode"
                    SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="流水号:" Visible="false">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSN" runat="server" Width="100px" Visible="false">
                </dx:ASPxTextBox>
            </td>
            <td style="text-align: left; width: 60px;">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="日期时间:" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="200px" ClientInstanceName="DateEdit1"
                    EditFormatString="yyyy-MM-dd HH:mm:ss">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" ClientInstanceName="LabDate"
                    ClientVisible="true" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientVisible="true" Width="200px"
                    ClientInstanceName="DateEdit2" EditFormatString="yyyy-MM-dd HH:mm:ss">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 80px;">
                <dx:ASPxButton ID="ASPxButton2" Text="查询" Width="80px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s, e) {   grid.PerformCallback(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="true" ClientInstanceName="grid"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="计划" SettingsPager-Mode="ShowAllRecords">
        <Settings ShowFilterRow="false" />
        <SettingsBehavior AllowSort="false" />
        <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                     grid2.PerformCallback();
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
        <Settings ShowFooter="True" />
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="日期" SummaryType="Count" DisplayFormat="数量={0}" />
        </TotalSummary>
    </dx:ASPxGridView>
    <%-- <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>--%>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
</asp:Content>
