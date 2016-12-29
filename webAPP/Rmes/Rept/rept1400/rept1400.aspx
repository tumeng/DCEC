<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept1400.aspx.cs" Inherits="Rmes_rept1400" %>

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
<%--功能概述：单机装配信息查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (SN.GetValue() == null) {
                return;
            }
//            if (SN.GetText().length != 8) {
//                alert("流水号长度非法！")
//                return;
//            }
            var webFileUrl = "?SN=" + SN.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var str1 = "";
            var str2 = "";
            var array1 = result.split('@');

            if (result == "") {
                alert("该流水号不存在！")
                return;
            }
            else {

                PModel.SetValue(array1[2]);
                SO.SetValue(array1[0]);
                PSeries.SetValue(array1[1]);
                PlanCode.SetValue(array1[3]);
                FR.SetValue(array1[4]);
                //                DateEdit1.SetValue(array1[5]);
                //                DateEdit2.SetValue(array1[6]);

            }



        }
        function initEditSeries2(s, e) {
            if (SN2.GetValue() == null) {
                return;
            }
//            if (SN2.GetText().length != 8) {
//                alert("流水号长度非法！")
//                return;
//            }
            var webFileUrl = "?SN=" + SN2.GetValue() + " &opFlag=getEditSeries2";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var str1 = "";
            var str2 = "";
            var array1 = result.split('@');

            if (result == "") {
                alert("该流水号不存在！")
                return;
            }
            else {


                FDJGH.SetValue(array1[0]);
                FDJXH.SetValue(array1[1]);
                CGQXH.SetValue(array1[2]);
                CGQBH.SetValue(array1[3]);
                CYGG.SetValue(array1[4]);
                JYGG.SetValue(array1[5]);
                SYRY.SetValue(array1[6]);
                SYDZ.SetValue(array1[7]);
                

            }


        }
        function initEditSeries3(s, e) {
            if (SN3.GetValue() == null) {
                return;
            }
//            if (SN3.GetText().length != 8) {
//                alert("流水号长度非法！")
//                return;
//            }
            var webFileUrl = "?SN3=" + SN3.GetValue() + " &opFlag=getEditSeries3";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var str1 = "";
            var str2 = "";
            var array1 = result.split('@');

            if (result == "") {
                alert("该流水号不存在！")
                return;
            }
        }
    </script>
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="0" OnTabClick="pageControl_TabClick">
        <TabPages>
            <dx:TabPage Text="装配及质量数据" Visible="true" Name="0">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="流水号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSN" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="SN">
                                        <ClientSideEvents TextChanged="function(s,e){  initEditSeries(s, e);       }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="SO:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSO" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="SO">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="规格型号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtGgxh" runat="server" Width="100px" ClientInstanceName="PModel">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="发动机系列:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPmodel" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="PSeries">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="计划号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPlanCode" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="PlanCode">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="FR:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtFR" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="FR">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback(); grid6.PerformCallback(); grid7.PerformCallback();  
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="text-align: left; width: 60px;">
                                    <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="生产时间:" Visible="false" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxTextBox ID="ASPxDateEdit1" runat="server" Width="100px" ClientInstanceName="DateEdit1"
                                        Visible="false">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="--" Visible="false" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxTextBox ID="ASPxDateEdit2" runat="server" Width="100px" ClientInstanceName="DateEdit2"
                                        Visible="false">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                                        Width="100%" OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="RMES_ID"
                                        SettingsPager-Mode="ShowAllRecords">
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
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="RMES_ID" FieldName="RMES_ID" VisibleIndex="1"
                                                Visible="false" Width="100px" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="1"
                                                Width="100px" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="生产时间" FieldName="START_TIME" VisibleIndex="2"
                                                Width="140px" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="员工" FieldName="USER_NAME" VisibleIndex="3" Width="90px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="班次" FieldName="SHIFT_NAME" VisibleIndex="4" Width="70px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="班组" FieldName="TEAM_NAME" VisibleIndex="5" Width="80px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowFooter="True" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="STATION_NAME" SummaryType="Count" DisplayFormat="数量={0}" />
                                        </TotalSummary>
                                    </dx:ASPxGridView>
                                </td>
                                <td>
                                    <dx:ASPxGridView ID="ASPxGridView6" ClientInstanceName="grid6" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="STATION_NAME" Width="100%" OnCustomCallback="ASPxGridView6_CustomCallback"
                                        SettingsPager-Mode="ShowAllRecords">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="1"
                                                Width="120px" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="测量数据名称" FieldName="DETECT_NAME" VisibleIndex="2"
                                                Width="200px" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="测量数据" FieldName="DETECT_VALUE" VisibleIndex="3"
                                                Width="440px" Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager Mode="ShowAllRecords">
                                        </SettingsPager>
                                        <Settings ShowFooter="True" />
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="STATION_NAME" SummaryType="Count" DisplayFormat="数量={0}" />
                                        </TotalSummary>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxGridView ID="ASPxGridView7" ClientInstanceName="grid7" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="流水号" Width="100%" OnCustomCallback="ASPxGridView7_CustomCallback">
                                        <Columns>
                                            <dx:GridViewDataTextColumn Caption="流水号" FieldName="流水号" VisibleIndex="1" Width="100px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="打印时间" FieldName="打印时间" VisibleIndex="2" Width="135px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="机型" FieldName="机型" VisibleIndex="3" Width="100px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="标准SO" FieldName="标准SO" VisibleIndex="4" Width="120px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="功率" FieldName="功率" VisibleIndex="5" Width="80px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="进阀进气" FieldName="进阀进气" VisibleIndex="6" Width="100px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="排量" FieldName="排量" VisibleIndex="7" Width="80px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="系列" FieldName="系列" VisibleIndex="8" Width="70px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="怠速" FieldName="怠速" VisibleIndex="9" Width="70px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="发火次序" FieldName="发火次序" VisibleIndex="10" Width="100px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="性能表号" FieldName="性能表号" VisibleIndex="11" Width="120px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="打印机型" FieldName="打印机型" VisibleIndex="12" Width="100px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="转速" FieldName="转速" VisibleIndex="13" Width="80px"
                                                Settings-AutoFilterCondition="Contains">
                                                <Settings AutoFilterCondition="Contains"></Settings>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <%--<dx:TabPage Text="测试台数据" Visible="true" Name="1">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="流水号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSN2" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="SN2">
                                        <ClientSideEvents TextChanged="function(s,e){  initEditSeries2(s, e);       }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="发动机缸号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtFDJGH" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="FDJGH">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="发动机型号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtFDJXH" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="FDJXH">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="测功器型号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtCGQXH" runat="server" Width="100px" ClientInstanceName="CGQXH">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="测功器编号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox5" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="CGQBH">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="柴油规格:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtCYGG" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="CYGG">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="机油规格:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtJYGG" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="JYGG">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="试验人员:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSYRY" runat="server" Width="100px" ClientInstanceName="SYRY">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="试验地址:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSYDZ" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="SYDZ">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                                        <ClientSideEvents Click="function(s,e){
                        grid2.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="XMMC" Width="100%" OnCustomCallback="ASPxGridView2_CustomCallback">
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
                                <dx:GridViewDataTextColumn Caption="项目名称" FieldName="XMMC" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据一" FieldName="XM1" VisibleIndex="2" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据二" FieldName="XM2" VisibleIndex="3" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据三" FieldName="XM3" VisibleIndex="4" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据四" FieldName="XM4" VisibleIndex="5" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据五" FieldName="XM5" VisibleIndex="6" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据六" FieldName="XM6" VisibleIndex="7" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据七" FieldName="XM7" VisibleIndex="8" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="数据八" FieldName="XM8" VisibleIndex="9" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>--%>
            <%--<dx:TabPage Text="附装上线数据" Visible="true" Name="3">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="流水号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSN3" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="SN3">
                                        <ClientSideEvents TextChanged="function(s,e){  initEditSeries3(s, e);       }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                                        <ClientSideEvents Click="function(s,e){
                        grid3.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="grid3" runat="server" AutoGenerateColumns="false"
                            KeyFieldName="ZDMC" Width="100%" OnCustomCallback="ASPxGridView3_CustomCallback">
                            <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid3.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                
                                    callbackName =  grid3.cpCallbackName;
                                    theRet = grid3.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="站点名称" FieldName="ZDMC" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="测量项目名称" FieldName="XMMC" VisibleIndex="2" Width="200px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="测量数据" FieldName="SJ1" VisibleIndex="3" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="活塞凸出量" Visible="true" Name="4">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl4" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="txtSN4" runat="server" Text="流水号:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" ValueType="System.String" Width="100px"
                                        ClientInstanceName="SN3">
                                        <ClientSideEvents TextChanged="function(s,e){  initEditSeries3(s, e);       }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton4" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                                        <ClientSideEvents Click="function(s,e){
                        grid4.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView4" ClientInstanceName="grid4" runat="server" AutoGenerateColumns="false"
                            KeyFieldName="GHTM" Width="100%" OnCustomCallback="ASPxGridView4_CustomCallback">
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
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TCL" FieldName="TCL" VisibleIndex="2" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TCL1" FieldName="TCL1" VisibleIndex="3" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TCL4" FieldName="TCL4" VisibleIndex="4" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TCL5" FieldName="TCL5" VisibleIndex="5" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TCL6" FieldName="TCL6" VisibleIndex="6" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TCL2" FieldName="TCL2" VisibleIndex="7" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TCL3" FieldName="TCL3" VisibleIndex="8" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="测量时间" FieldName="CLRQ" VisibleIndex="9" Width="150px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>--%>
            <dx:TabPage Text="扫描零件" Visible="true" Name="5">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl5" runat="server">
                        <table>
                            <tr>
                              
                                <td>
                                    <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                        OnClick="btnXlsExport_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView5" ClientInstanceName="grid5" runat="server" AutoGenerateColumns="true"
                            KeyFieldName="流水号" Width="100%"     SettingsPager-Mode="ShowAllRecords">
                            <Settings ShowFilterRow="false" />
                   
                    <SettingsBehavior AllowSort="false" />
                            <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid5.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                
                                    callbackName =  grid5.cpCallbackName;
                                    theRet = grid5.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="发动机流水号" FieldName="流水号" VisibleIndex="1" Width="90px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="站点" FieldName="站点" VisibleIndex="2" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="扫描时间" VisibleIndex="3" Width="145px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="员工" FieldName="员工" VisibleIndex="4" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="地点" FieldName="地点" VisibleIndex="5" Width="60px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件流水号/批次号" FieldName="零件流水号" VisibleIndex="6" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                   
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件号" FieldName="零件号" VisibleIndex="6" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="零件名称" VisibleIndex="7" Width="140px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件供应商" FieldName="供应商" VisibleIndex="8" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="条码" FieldName="条码" VisibleIndex="9" Width="240px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                               <%-- <dx:GridViewDataTextColumn Caption="FR" FieldName="FR" VisibleIndex="0" Width="100px"
                                      Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlICode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView5">
    </dx:ASPxGridViewExporter>
</asp:Content>
