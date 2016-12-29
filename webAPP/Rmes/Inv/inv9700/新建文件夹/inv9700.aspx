<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Inv_inv9700_inv9700"
    StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" CodeBehind="inv9700.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--入出库查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function changeSeq(s, e) {
            var buttonID = e.buttonID;
            if (buttonID == "Delete") {
                if (PCode.GetValue() == null) {
                    alert("请先选择生产线！");
                    return;
                }
                if (confirm("确认删除该记录？")) {
                }
                else { return; }
                var pcode = PCode.GetValue();
                grid.GetValuesOnCustomCallback(buttonID + '|' + "" + '|' + pcode + '|' + "", GetDataCallback);
            }
            else {
                if (Chose.GetValue() == null && PCode.GetValue() == null) {
                    alert("请先选择生产线和要删除的批次！");
                    return;
                }
                else {
                    var batch = Batch.GetText();
                    var batch1 = Batch.GetValue();
                    var pcode = PCode.GetValue();
                    var chose = Chose.GetValue();
                    if (confirm("确认删除该'" + batch + "'批次的记录？")) {
                    }
                    else { return; }
                    grid.GetValuesOnCustomCallback(buttonID + '|' + batch1 + '|' + pcode + '|' + chose, GetDataCallback);
                }
            }



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
                    grid.PerformCallback();
                    return;
                case "Fail":
                    alert(retStr);
                    grid.PerformCallback();
                    return;
            }

        }

        function initEditSeries(s, e) {
            if (Chose.GetValue() == null) {
                return;
            }
            if (Chose.GetText() == '出库') {
                LabFX.SetVisible(true);
                txtFx.SetVisible(true);
            }
            else {
                LabFX.SetVisible(false);
                txtFx.SetVisible(false);
            }

        }
        function initBatch(s, e) {
            if (DateEdit1.GetValue() == null || DateEdit1.GetValue() == null || Chose.GetValue() == null || PCode.GetValue() == null) {
                alert("请先选择生产线和入/出库选项！");
                return;
            }
            var webFileUrl = "?DATEEDIT1=" + DateEdit1.GetText() + " &DATEEDIT2=" + DateEdit2.GetText() + " "
                           + "&CHOSE=" + Chose.GetValue() + "&PCODE=" + PCode.GetValue() + "&opFlag=getBatch";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var str1 = "";
            var str2 = "";
            var str3 = "";
            var array1 = result.split(',');
            str1 = array1[0];
            str2 = array1[1];
            str3 = array1[2];
            if (str1 == "Overtime") {
                alert("选择日期范围不能超过30天，请重新选择！");
                Batch.SetValue("");
            }
            else if (str1 == "ok") {
                var items = str2.split('@');
                var items2 = str3.split('@');
                Batch.ClearItems();
                txtFx.ClearItems();
                Batch.AddItem('全部批次', '%');
                for (var i = items.length - 1; i >= 0; i = i - 1) {
                    Batch.AddItem(items[i], items[i]);
                }

                for (var j = items2.length - 1; j >= 0; j = j - 1) {
                    txtFx.AddItem(items2[j], items2[j]);

                }
            }




        }

    </script>
    <table style="background-color: #99bbbb; width: 100%;">
        <tr>
            <td style="width: 5px; height: 25px;">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="生产线选择:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="120px" ClientInstanceName="PCode"
                    SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td style="width: 25px">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="SO:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="textSO" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="入/出库选择" >
                </dx:ASPxLabel>
            </td>
            <td style="width: 63px">
                <dx:ASPxComboBox ID="txtChose" runat="server" ValueType="System.String" Width="80px"
                    ClientInstanceName="Chose">
                    <Items>
                        <dx:ListEditItem Text="入库" Value="RK" />
                        <dx:ListEditItem Text="出库" Value="CK" />
                    </Items>
                    <ClientSideEvents SelectedIndexChanged="function(s,e){
                      initBatch(s, e); initEditSeries(s, e);
                }" />
                </dx:ASPxComboBox>
            </td>
            <td style="width: 40px">
                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="批次:">
                </dx:ASPxLabel>
            </td>
            <td style="width: 99px">
                <dx:ASPxComboBox ID="txtPc" runat="server" ValueType="System.String" Width="140px"
                    ClientInstanceName="Batch">
                    <%--   <ClientSideEvents   Init="function(s, e) {initBatch(s, e);}" />--%>
                </dx:ASPxComboBox>
            </td>
            <td style="width: 60px">
                <dx:ASPxLabel ID="LabFX" runat="server" Text="出库方向:" ClientVisible="false" ClientInstanceName="LabFX">
                </dx:ASPxLabel>
            </td>
            <td style="width: 99px">
                <dx:ASPxComboBox ID="txtFx" runat="server" ValueType="System.String" Width="100px"
                    ClientVisible="false" ClientInstanceName="txtFx">
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td style="width: 30%">
            </td>
        </tr>
    </table>
    <table style="background-color: #99bbbb; width: 100%;">
        <tr>
            <td style="width: 5px; height: 25px;">
            </td>
            <td style="text-align: left; width: 70px;">
                <label style="font-size: small">
                    开始日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="120px" ClientInstanceName="DateEdit1">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 25px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="——" />
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="120px" ClientInstanceName="DateEdit2">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 44px">
                <dx:ASPxButton ID="ButSubmit" Text="查询" Width="80px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback(); grid2.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td  style="width:20px">
            </td>
            <td>
                <dx:ASPxButton ID="Batdelete" runat="server" Text="批删除" Width="80px" AutoPostBack="false"
                    ClientInstanceName="Batdelete">
                    <ClientSideEvents Click="function (s,e) { changeSeq(s,e);}" />
                </dx:ASPxButton>
            </td>
            <td style="width:20px">
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
            </td>
            <td  style="width:20px">
            </td>
            <td>
                <dx:ASPxButton ID="btnTxt" runat="server" Text="生成文本-TXT" UseSubmitBehavior="False"  AutoPostBack="false" 
                    ><%-- OnClick="btnTxt_Click"--%>
                    <ClientSideEvents Click="function(s,e){grid.PerformCallback('TXT|txt');}" />
                    </dx:ASPxButton>
            </td>
             
            <td>
<%--                <label style="font-size: x-small">
                    文件生成到C:\TRANLIST\Storage中</label>--%>
            </td>
            <td style="width:25%">
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 1000px">
                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="GHTM;SO;GZRQ;RKDATE"
                    AutoGenerateColumns="False" OnCustomCallback="ASPxGridView1_CustomCallback" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
                    Width="1000px">
                    <Settings ShowHorizontalScrollBar="true" />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <%--     <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                Width="60px">
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                        Style="margin-bottom: 0px" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>--%>
                        <%--<dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="60px" ButtonType="Button">
                
                <DeleteButton Visible="True" Text="删除" >
                </DeleteButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>--%>
                        <dx:GridViewCommandColumn Caption="操作" Width="80px" ButtonType="Button">
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="Delete" Text="删除">
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Visible="false" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="入/出" FieldName="RC" Width="60px" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="100px" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" Width="100px" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="入出库时间" FieldName="GZRQ" Width="120px" CellStyle-Wrap="False"
                            Settings-AllowHeaderFilter="True">
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="操作员" FieldName="YGMC" Width="80px" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="入出库类型" FieldName="RKLX" Width="80px" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="批次" FieldName="BATCHID" Width="120px" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="车号" FieldName="CH" Width="75px" Settings-AutoFilterCondition="Contains">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn Caption="扫描时间" FieldName="RKDATE" Width="120px" CellStyle-Wrap="False"
                            Settings-AllowHeaderFilter="True">
                            <Settings AllowHeaderFilter="True"></Settings>
                            <CellStyle Wrap="False">
                            </CellStyle>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption=" " Width="80%" />
                    </Columns>
                    <Settings ShowFooter="True" />
                    <SettingsBehavior ColumnResizeMode="Control" />
                    <ClientSideEvents CustomButtonClick="function (s,e){
        changeSeq(s,e);
    }" />
                    <ClientSideEvents EndCallback="function(s, e) {
                    
                    callbackValue=grid.cpCallbackValue;
                if(callbackValue!='.txt')
                    window.open('http://192.168.112.144/tranlist/Storage/'+callbackValue);
                }" BeginCallback="function(s, e) {
	                grid.callbackValue = '';
                }" />  
                </dx:ASPxGridView>
            </td>
            <td style="width: 100px">
                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" KeyFieldName="SO"
                    Width="16px" OnCustomCallback="ASPxGridView2_CustomCallback">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="0" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="1" Width="60px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " Width="60%">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Settings ShowFooter="True" />
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="SL" SummaryType="Sum" DisplayFormat="合计={0}" />
                    </TotalSummary>
                    <SettingsBehavior ColumnResizeMode="Control" />
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
</asp:Content>
