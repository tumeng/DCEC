<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
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
            if (Item1.GetValue() == null) {
                alert("原零件号不能为空！");
                return;
            }
            if (Location1.GetValue() == null) {
                alert("原零件工位不能为空！");
                return;
            }
            if (Process1.GetValue() == null) {
                alert("原零件工序不能为空！");
                return;
            }
            if (YSL.GetValue() == null) {
                alert("原零件数量不能为空！");
                return;
            }
            var te = /^[1-9][0-9]*$/;
            if (!te.test(YSL.GetValue())) {
                alert("原零件数量应为大于零的整数！");
                return;
            }
            var webFileUrl = "?PLANCODE=" + PlanCode.GetValue() + " &ITEM=" + Item1.GetValue() + " "
            + "&LOCATION=" + Location1.GetValue() + " &PROCESS=" + Process1.GetValue() + " &YSL=" + YSL.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var array = result.split(',');
            var a = array[0];
            var b = array[1];
            if (a == "OK") {

                var item = Item1.GetValue().toString();
                var location = Location1.GetValue().toString();
                var process = Process1.GetValue().toString();
                var sl = YSL.GetValue().toString();
                var str = item + "," + location + "," + process + "," + sl;

                var cnt = listBoxPART1.GetItemCount();
                for (var i = 0; i < cnt; i++) {
                    var lstr = listBoxPART1.GetItem(i);
                    if (lstr.text == str) {
                        return;
                    }
                }

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
            if (!te.test(SL.GetValue())) {
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
                var cnt = listBoxPART2.GetItemCount();

                for (var i = 0; i < cnt; i++) {
                    var lstr = listBoxPART2.GetItem(i);
                    if (lstr.text == str) {
                        return;
                    }
                }
                listBoxPART2.AddItem(str, str);
            }
            else {
                alert(b);

            }


        }


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
            grid2.PerformCallback();
            Item1.PerformCallback();
            listBoxPART1.ClearItems();
            listBoxPART2.ClearItems();
            if (Item1.GetValue() == null) {
                var str = Item1.GetValue() + "," + '###';
                Location1.PerformCallback();
                Process1.PerformCallback(str);
            }
            else if (Item1.GetValue() != null && Location1.GetValue() == null) {
                var str = Item1.GetValue() + "," + '###';
                Location1.PerformCallback(Item1.GetValue());
                Process1.PerformCallback(str);
            }
            else if (Item1.GetValue() != null && Location1.GetValue() != null) {
                var str = Item1.GetValue() + "," + Location1.GetValue();
                Location1.PerformCallback(Item1.GetValue());
                Process1.PerformCallback(str);
            }

        }
        function filterQuery2() {
            //
            if (PlanCode.GetValue() == null) {
                alert("计划号不能为空！");
                return;
            }
            if (DH.GetValue() == null) {
                alert("单号不能为空！");
                return;
            }
            listBoxPART1.PerformCallback();
            listBoxPART2.PerformCallback();


        }
        function plan() {
            //
            if (PlanCode1.GetValue() == null) {

                return;
            }

            PlanCode.SetValue(PlanCode1.GetValue());


        }
        function filter() {


            if (Item1.GetValue() == null) {
                return;
            }
             Location1.PerformCallback(Item1.GetValue());
//            else if (Location1.GetValue() == null) {
//                var str = Item1.GetValue() + "," + '###';
//                Location1.PerformCallback(Item1.GetValue());
//                Process1.PerformCallback(str);
//            }
//            else {
//                var str = Item1.GetValue() + "," + Location1.GetValue();
//                Location1.PerformCallback(Item1.GetValue());
//                Process1.PerformCallback(str);
//            }
        }
        function filter2() {


            if (Item1.GetValue() == null) {
                return;
            }
            else if (Location1.GetValue() == null) {
                var str = Item1.GetValue() + "," + '###';
                
                Process1.PerformCallback(str);
            }
            else {
                var str = Item1.GetValue() + "," + Location1.GetValue();
                 
                Process1.PerformCallback(str);
            }
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
                        <td style="width: 50px; height: 25px;">
                        </td>
                        <td style="width: 40px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="计划号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 120px;">
                            <dx:ASPxTextBox ID="txtPlanCode" ClientInstanceName="PlanCode" runat="server" Width="120px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 20px; text-align: left;">
                        </td>
                        <td style="width: 40px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="单号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px;">
                            <dx:ASPxTextBox ID="txtDH" ClientInstanceName="DH" runat="server" Width="140px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 60px;">
                            <dx:ASPxButton ID="ASPxButQuery" Text="查询" Width="60px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) {  filterQuery2();  }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 80px;">
                            <dx:ASPxButton ID="ASPxButton1" Text="删除单号" Width="100px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) { filterDelet(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                </table>
                <table style="background-color: #99bbbb; width: 100%;">
                    <tr>
                        <td>
                        </td>
                        <td style="width: 85px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="零件号(原零件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxComboBox ID="ComboItem" ClientInstanceName="Item1" runat="server" ValueType="System.String"
                                Width="100px" SelectedIndex="0" DataSourceID="sqlItem" ValueField="COMP" TextField="COMP"
                                OnCallback="ComboItem_Callback" DropDownStyle="DropDownList">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { filter() }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 85px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="工位(原零件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxComboBox ID="ComboLocation" ClientInstanceName="Location1" ValueType="System.String"
                                runat="server" SelectedIndex="0" Width="100px" DataSourceID="sqlItem2" ValueField="GWMC"
                                TextField="GWMC" OnCallback="ComboLocation_Callback" DropDownStyle="DropDownList">
                                <ClientSideEvents EndCallback="function(s, e) { filter2() }" SelectedIndexChanged="function(s, e) { filter2() }"   />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 85px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="工序(原零件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxComboBox ID="ComboProcess" ClientInstanceName="Process1" ValueType="System.String"
                                runat="server" Width="100px" SelectedIndex="0" DataSourceID="sqlItem3" ValueField="GXMC"
                                TextField="GXMC" OnCallback="ComboProcess_Callback" DropDownStyle="DropDownList">
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 70px;">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="数量(原零件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtSL" ClientInstanceName="YSL" runat="server" Width="100px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 80px;">
                            <dx:ASPxButton ID="ButOPart" runat="server" AutoPostBack="False" Text="原零件" Width="80px">
                                <ClientSideEvents Click="function(s, e) { initEditSeries(s, e); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 85px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="零件号(替换件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtItem" ClientInstanceName="Item" runat="server" Width="100px"
                                TextField="" ValueField="">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 85px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="工位(替换件)">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 100px;">
                            <dx:ASPxTextBox ID="txtLocation" ClientInstanceName="Location" runat="server" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 85px; text-align: left;">
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
                            <dx:ASPxTextBox ID="txtSL2" ClientInstanceName="SL" runat="server" Width="100px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 90px;">
                            <dx:ASPxButton ID="ButRPart" runat="server" AutoPostBack="False" Text="替换件" Width="80px">
                                <ClientSideEvents Click="function(s, e) { initEditSeries2(s, e) ; }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="保存" Width="60px">
                                <ClientSideEvents Click="function(s, e) { filterLsh2(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px; height: 25px;">
                        </td>
                        <td style="width: 40px">
                            &nbsp;
                        </td>
                        <td style="width: 80px;">
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <%-- <td style="width: 60px;">
                            <dx:ASPxButton ID="ASPxButton3" Text="查询" Width="80px" AutoPostBack="false" runat="server">
                                <ClientSideEvents Click="function(s, e) { filterLocation(); }" />
                            </dx:ASPxButton>
                        </td>--%>
                        <td style="width: 80px;">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table style="background-color: #99bbbb; width: 100%">
                    <tr>
                        <td style="width: 2px; height: 400px;">
                        </td>
                        <td style="width: 200px; height: 400px;">
                            <dx:ASPxListBox ID="ASPxListBoxPart1" runat="server" ClientInstanceName="listBoxPART1"
                                OnCallback="ASPxListBoxPart1_Callback" Width="200px" Height="400px" ValueField="PT_PART"
                                ValueType="System.String" ViewStateMode="Inherit">
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
                        <td style="width: 10px; height: 400px;" rowspan="5">
                        <td style="width: 200px; height: 400px;">
                            <dx:ASPxListBox ID="ASPxListBoxPart2" runat="server" ClientInstanceName="listBoxPART2"
                                OnCallback="ASPxListBoxPart2_Callback" Width="200px" Height="400px" ValueField="PT_PART"
                                ValueType="System.String" ViewStateMode="Inherit">
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
                                    <td style="width: 120px;">
                                        <dx:ASPxTextBox ID="ASPxTextPlanCode" ClientInstanceName="PlanCode1" runat="server"
                                            Width="120px">
                                            <ClientSideEvents TextChanged="function(s, e) {  plan();  }" />
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
                                        <td style="width: 120px;">
                                            <dx:ASPxTextBox ID="ASPxTextSation" runat="server" ClientInstanceName="Station2"
                                                Width="120px">
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
                                    KeyFieldName="GWMC;GXMC;COMP" OnCustomCallback="ASPxGridView1_CustomCallback">
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
                                            VisibleIndex="1" Width="80px">
                                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                            <CellStyle HorizontalAlign="Center">
                                            </CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="工序" FieldName="GXMC" ShowInCustomizationForm="True"
                                            VisibleIndex="2" Width="80px">
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
                                            VisibleIndex="4" Width="60px">
                                            <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="供应商名称" FieldName="GYSMC" ShowInCustomizationForm="True"
                                            VisibleIndex="5" Width="100px">
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
                <table style="background-color: #99bbbb; width: 100%">
                    <tr>
                        <td style="height: 400px">
                            <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="PART;GXDM;CZTS" OnRowDeleting="ASPxGridView2_RowDeleting" OnRowInserting="ASPxGridView2_RowInserting"
                                OnRowUpdating="ASPxGridView2_RowUpdating" OnRowValidating="ASPxGridView2_RowValidating"
                                OnCustomCallback="ASPxGridView2_CustomCallback" OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated">
                                <SettingsEditing PopupEditFormWidth="530px" />
                                <SettingsBehavior ColumnResizeMode="Control" />
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
                                        <NewButton Visible="True" Text="新增">
                                        </NewButton>
                                        <EditButton Visible="True" Text="修改">
                                        </EditButton>
                                        <DeleteButton Visible="True" Text="删除">
                                        </DeleteButton>
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="计划号" Name="JHDM" FieldName="JHDM" VisibleIndex="1"
                                        Width="100px" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="组件" Name="PART" FieldName="PART" VisibleIndex="2"
                                        Width="100px" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="工序代码" Name="GXDM" FieldName="GXDM" VisibleIndex="3"
                                        Width="80px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="装机提示" Name="CZTS" FieldName="CZTS" VisibleIndex="4"
                                        Width="200px" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="文件路径" Name="WJPATH" FieldName="WJPATH" VisibleIndex="5"
                                        Width="150px" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="提示种类" Name="TSTYPE" FieldName="TSTYPE" Visible="false"
                                        Width="150px" Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <EditForm>
                                        <table>
                                            <tr>
                                                <td style="height: 30px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px">
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="工序代码">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 140px">
                                                    <dx:ASPxComboBox ID="txtGxdm" runat="server" Width="140px" Value='<%# Bind("GXDM") %>'
                                                        DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                            ErrorDisplayMode="ImageWithTooltip">
                                                            <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td style="width: 1px">
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="文件路径">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 140px">
                                                    <dx:ASPxTextBox ID="txtPath" runat="server" Text='<%# Bind("WJPATH") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                                        Width="140px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                            ValidateOnLeave="True">
                                                            <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="width: 1px">
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="height: 30px">
                                                </td>
                                                <td style="width: 50px">
                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="装机提示">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 370px">
                                                    <dx:ASPxTextBox ID="txtCzts" runat="server" Text='<%# Bind("CZTS") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                                        Width="370px">
                                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                            ValidateOnLeave="True">
                                                            <RegularExpression ErrorText="长度不能超过3000！" ValidationExpression="^.{0,3000}$" />
                                                            <RequiredField ErrorText="不能为空！" IsRequired="False" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="height: 30px; text-align: right;">
                                                    <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                                                    <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 30px">
                                                </td>
                                            </tr>
                                        </table>
                                    </EditForm>
                                </Templates>
                                <%--<ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
            grid.PerformCallback();
             
        }" />--%>
                            </dx:ASPxGridView>
                        </td>
                        <td style="width: auto; height: 400px;">
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
    <asp:SqlDataSource ID="sqlItem" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlItem2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlItem3" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
