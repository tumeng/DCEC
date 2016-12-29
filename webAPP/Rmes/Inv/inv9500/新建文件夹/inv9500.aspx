﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv9500.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv9500.inv9500" %>

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
<%--临时措施替换--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (PlanCode.GetValue() == null) {
                alert("计划号不能为空！");
                return;
            }
            if (Item.GetValue() == null) {
                alert("零件号不能为空！");
                return;
            }
            if (Location.GetValue() == null) {
                alert("工位不能为空！");
                return;
            }
            if (Process.GetValue() == null) {
                alert("工序不能为空！");
                return;
            }
            var webFileUrl = "?PLANCODE=" + PlanCode.GetValue() + " &ITEM=" + Item.GetValue() + " "
            + "&LOCATION=" + Location.GetValue() + " &PROCESS=" + Process.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var array = result.split(',');
            var a = array[0];
            var b = array[1]; 
            if (a == "OK") {
                
                var item = Item.GetValue().toString();
                var location = Location.GetValue().toString();
                
                var str = item + "," + location;
                listBoxPART1.AddItem(str, str);
            }
            else {
                alert(b);

            }


        }
        function initEditSeries2(s, e) {
            if (PlanCode.GetValue() == null) {
                alert("计划号不能为空！");
                return;
            }
            if (Item.GetValue() == null) {
                alert("零件号不能为空！");
                return;
            }
            if (Location.GetValue() == null) {
                alert("工位不能为空！");
                return;
            }
            if (Process.GetValue() == null) {
                alert("工序不能为空！");
                return;
            }
            if (SL.GetValue() == null) {
                alert("数量不能为空！");
                return;
            }
            var te = /^[1-9][0-9]*$/;  
               if (!te.test(SL.GetValue()))
             {
                 alert("数量应为大于零的整数！");
                 return;
            }  
            var webFileUrl = "?PLANCODE=" + PlanCode.GetValue() + " &ITEM=" + Item.GetValue() + " "
            + "&LOCATION=" + Location.GetValue() + " &PROCESS=" + Process.GetValue() + " &opFlag=getEditSeries2";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var array = result.split(',');
            var a = array[0];
            var b = array[1];
            if (a == "OK") {
            
                var plancode = PlanCode.GetValue().toString();
                var item = Item.GetValue().toString();
                var location = Location.GetValue().toString();
                var process = Process.GetValue().toString();
 
                var sl = SL.GetValue().toString();
                var str = item + "," + location + "," + process + "," + sl;
               
                listBoxPART2.AddItem(str, str);
            }
            else {
                alert(b);

            }


        }
//        function filterLocation() {

//            if (PlanCode.GetValue() == null) {
//                alert("计划号不能为空！");
//                return;
//            }
//            if (Item.GetValue() == null) {
//                alert("零件号不能为空！");
//                return;
//            }
//            if (Location.GetValue() == null) {
//                alert("工位不能为空！");
//                return;
//            }
//            if (Process.GetValue() == null) {
//                alert("工序不能为空！");
//                return;
//            }

//            var plancode = PlanCode.GetValue().toString();
//            var item = Item.GetValue().toString();
//            var location = Location.GetValue().toString();
//            var process = Process.GetValue().toString();

//            var str = item + "," + location;
//            listBoxPART1.PerformCallback(plancode + "," + item + "," + location + "," + process);
//            listBoxPART1.AddItem(str, str);


//        }
//        function filterLocation2() {
//            // 
//            if (PlanCode.GetValue() == null) {
//                alert("计划号不能为空！");
//                return;
//            }
//            if (Item.GetValue() == null) {
//                alert("零件号不能为空！");
//                return;
//            }
//            if (Location.GetValue() == null) {
//                alert("工位不能为空！");
//                return;
//            }
//            if (Process.GetValue() == null) {
//                alert("工序不能为空！");
//                return;
//            }
//            if (SL.GetValue() == null) {
//                alert("数量不能为空！");
//                return;
//            }
//            var plancode = PlanCode.GetValue().toString();
//            var item = Item.GetValue().toString();
//            var location = Location.GetValue().toString();
//            var process = Process.GetValue().toString();

//            var sl = SL.GetValue().toString();
//            var str = item + "," + location + "," + process + "," + sl;
//            listBoxPART2.PerformCallback(plancode + "," + item + "," + location + "," + process + "," + sl);

//            listBoxPART2.AddItem(str, str);

//        }
        function filterLsh2() {

            if (PlanCode.GetValue() == null) {
                alert("计划号不能为空！");
                return;
            }
            if (DH.GetValue() == null) {
                alert("单号不能为空！");
                return;
            }
            var plancode = PlanCode.GetValue().toString();

            var dh = DH.GetValue().toString();

            ref = 'Save' + "," + plancode + "," + dh;
            CallbackSubmit.PerformCallback(ref);
        }
        function filterDelet() {

            if (PlanCode.GetValue() == null) {
                alert("计划号不能为空！");
                return;
            }
            if (DH.GetValue() == null) {
                alert("单号不能为空！");
                return;
            }
            if (confirm(" 该操作将删除该计划该单号下的替换关系，是否确认？")) {
            }
            else { return; }
            var plancode = PlanCode.GetValue().toString();

            var dh = DH.GetValue().toString();
            ref = 'Delete' + "," + plancode + "," + dh;
            CallbackSubmit.PerformCallback(ref);

        }
        function filterQuery() {
            //
            if (PlanCode1.GetValue() == null) {
                alert("计划号不能为空！");
                return;
            }
            if (Station2.GetValue() == null) {
                alert("站点名称不能为空！");
                return;
            }
            grid.PerformCallback();


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
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel5">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent5" runat="server">
                <table style="background-color: #99bbbb; width: 100%;">
                    <tr>
                        <td style="width: 5px; height: 25px;">
                        </td>
                        <td style="width: 40px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="计划号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtPlanCode" ClientInstanceName="PlanCode" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 40px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="单号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtDH" ClientInstanceName="DH" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 40px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="零件号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtItem" ClientInstanceName="Item" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 40px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="工位">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtLocation" ClientInstanceName="Location" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 70px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="工序(替换件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtProcess" ClientInstanceName="Process" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 70px;">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="数量(替换件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtSL" ClientInstanceName="SL" runat="server" Width="100px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px; height: 25px;">
                        </td>
                        <td style="width: 40px">
                            &nbsp;
                        </td>
                        <td style="width: 70px;">
                            <dx:ASPxButton ID="ButOPart" runat="server" AutoPostBack="False" Text="原零件" Width="70px">
                                <ClientSideEvents Click="function(s, e) { initEditSeries(s, e); }" />
                            </dx:ASPxButton>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxButton ID="ButRPart" runat="server" AutoPostBack="False" Text="替换件" Width="70px">
                                <ClientSideEvents Click="function(s, e) { initEditSeries2(s, e) ; }" />
                            </dx:ASPxButton>
                        </td>
                        <%-- <td style="width: 60px;">
                            <dx:ASPxButton ID="ASPxButton3" Text="查询" Width="80px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) { filterLocation(); }" />
                            </dx:ASPxButton>
                        </td>--%>
                        <td style="width: 80px;">
                            <dx:ASPxButton ID="ASPxButton2" Text="保存" Width="60px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) { filterLsh2(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 80px;">
                            <dx:ASPxButton ID="ASPxButton1" Text="删除单号" Width="100px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) { filterDelet(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
                <table style="background-color: #99bbbb; width: 100%">
                    <tr>
                        <td style="width: 5px; height: 400px;">
                        </td>
                        <td style="width: 200px; height: 400px;">
                            <dx:ASPxListBox ID="ASPxListBoxPart1" runat="server" ClientInstanceName="listBoxPART1"
                                Width="200px" Height="400px" ValueField="PT_PART" ValueType="System.String"  
                                ViewStateMode="Inherit" SelectionMode="CheckColumn">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="PT_PART" Caption="原零件" Width="90%" />
                                </Columns>
                                <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                listBoxPART1.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName =  listBoxPART1.cpCallbackName;
                                    theRet = listBoxPART1.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
                            </dx:ASPxListBox>
                        </td>
                        <td style="width: 20px; height: 400px;" rowspan="5">
                        <td style="width: 200px; height: 400px;">
                            <dx:ASPxListBox ID="ASPxListBoxPart2" runat="server" ClientInstanceName="listBoxPART2"
                                Width="200px" Height="400px" ValueField="PT_PART" ValueType="System.String" ViewStateMode="Inherit"
                                SelectionMode="CheckColumn">
                                
                                <Columns>
                                    <dx:ListBoxColumn Caption="替换件" FieldName="PT_PART" Width="90%" />
                                </Columns>
                                <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                listBoxPART2.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName =  listBoxPART2.cpCallbackName;
                                    theRet = listBoxPART2.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
                            </dx:ASPxListBox>
                        </td>
                        <td style="width: 50px; text-align: left; height: 234px;">
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 20px;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 60px; height: 400px;">
                            <table>
                                <tr>
                                    <td style="width: 40px; text-align: left;">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="计划号">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px;">
                                        <dx:ASPxTextBox ID="ASPxTextPlanCode" ClientInstanceName="PlanCode1" runat="server"
                                            Width="100px">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60px; text-align: left;">
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="站点名称">
                                        </dx:ASPxLabel>
                                    </td>
                                    <tr>
                                        <td style="width: 100px;">
                                            <dx:ASPxTextBox ID="ASPxTextSation" runat="server" ClientInstanceName="Station2"
                                                Width="100px">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                <tr>
                                    <td style="height: 20px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 60px;">
                                        <dx:ASPxButton ID="ASPxButton4" Text="查询" Width="60px" AutoPostBack="false" runat="server">
                                            <ClientSideEvents Click="function(s, e) {  filterQuery();  }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <td style="height: 400px">
                                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
                                    KeyFieldName="GWMC;GXMC;COMP"  OnCustomCallback="ASPxGridView1_CustomCallback">
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
                                        <dx:GridViewCommandColumn Caption=" " Name="CommondColumn" ShowInCustomizationForm="True"
                                            VisibleIndex="0" Width="40px">
                                            <ClearFilterButton Visible="True">
                                            </ClearFilterButton>
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn Caption="工位名称" FieldName="GWMC" ShowInCustomizationForm="True"
                                            VisibleIndex="1" Width="100px">
                                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序" FieldName="GXMC" ShowInCustomizationForm="True"
                                            VisibleIndex="2" Width="100px">
                                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="零件号" FieldName="COMP" ShowInCustomizationForm="True"
                                            VisibleIndex="3" Width="100px">
                                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="数量" FieldName="QTY" ShowInCustomizationForm="True"
                                            VisibleIndex="4" Width="100px">
                                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption=" " ShowInCustomizationForm="True" VisibleIndex="5"
                                            Width="80%">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsBehavior ColumnResizeMode="Control" />
                                    <Settings ShowFooter="True" ShowHorizontalScrollBar="True" />
                                </dx:ASPxGridView>
                            </td>
                        <td style="width: auto; height: 400px;">
                        </td>
                    </tr>
                </table>
                <table>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>
</asp:Content>
