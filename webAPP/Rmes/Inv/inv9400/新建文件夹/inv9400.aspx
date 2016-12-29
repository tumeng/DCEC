<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv9400.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv9400.inv9400" %>

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
    <%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%--入库回冲确认--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (Lsh.GetValue() == null) {
                return;
            }
            var webFileUrl = "?LSH=" + Lsh.GetValue() + "&opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var planso = "";
            var plancode = "";
            var stationcode = "";
            var array1 = result.split(',');
            planso = array1[0];
            plancode = array1[1];
            stationcode = array1[2];

            PlanSo.SetValue(planso);
            PlanCode.SetValue(plancode);
            Zdmc.SetValue(stationcode);

        }
        function initEditSeries2(s) {
            if (Lsh.GetValue() == null) {
                return;
            }
            var webFileUrl = "?LSH=" + Lsh.GetValue() + "&opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            var planso = "";
            var plancode = "";
            var stationcode = "";
            var array1 = result.split(',');
            planso = array1[0];
            plancode = array1[1];
            stationcode = array1[2];

            PlanSo.SetValue(planso);
            PlanCode.SetValue(plancode);
            Zdmc.SetValue(stationcode);

        }

        function changeSeq() {
          
            btnConfirm.SetEnabled(false);
           
           
            if (confirm(" 是否全部确认？"))
             {
            }
            else {
                alert("确认操作已取消！");
                btnConfirm.SetEnabled(true);
                return;
            }
            ref = 'Commit' + '@' + 'false';
            CallbackSubmit.PerformCallback(ref);

        }

        function submitRtr(e) {
            var result = "";
            var retStr = "";
            var array = e.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {

                case "Fail":
                    alert(retStr);
                    grid.PerformCallback();
                    return;
                case "OK":
                    alert(retStr);
                    grid.PerformCallback();
                    return;
            }
        }

        function ButtonClick(s, e) {

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
                    grid.PerformCallback();
                    return;
                case "Fail":
                    alert(retStr);
                    grid.PerformCallback();
                    return;
            }

        }  
    </script>
      
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="height: 40px">
                             <span style="color :Blue ;font-size: 12pt";>
                            流水号加工单筛选</span>
                        </td>
                        <td style="width: 28px">
                        </td>
                        <td style="width: 40px">
                            <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="生产线" Width="40px" Height="17px" >
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="txtPlineCode" DataSourceID="SqlCode" ValueField="pline_code"
                                TextField="pline_name" runat="server" Width="80px" SelectedIndex="0">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="流水号:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="TextSN" runat="server"  Width="100px"  ClientInstanceName="Lsh" >
                            <ClientSideEvents  TextChanged ="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="计划号:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="TextPlanCode" runat="server"   Width="100px" ClientInstanceName="PlanCode">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="SO:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="TexPlanSo" runat="server" Width="100px" ClientInstanceName="PlanSo">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="所在站点:" >
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="TextZdmc" runat="server" Width="100px" ClientInstanceName="Zdmc">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <dx:ASPxButton ID="BtnSubmit" runat="server" AutoPostBack="False"  
                                Text="查询" >
                                 <ClientSideEvents Click="function(s,e){
                    
                     grid.PerformCallback('BtnSubmit');
                        
                    }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
         <td style="height: 40px; width:80px">
                              
                        </td>
            <td>
                <table>
                    <tr>
                        <td style="height: 34px">
                             <span style="color :Blue ;font-size: 12pt" >   
                            按日期筛选</span>
                        </td>
                        <td>
                        </td>
                    </tr>
                    </table>
                    <table>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="下线入库日期:" Width="120px" />
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="100px">
                                <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                                </CalendarProperties>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="--" />
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="100px">
                                <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                                </CalendarProperties>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxButton ID="Query" runat="server" AutoPostBack="False"  
                                Text="查询" Height="16px"  ClientInstanceName="btn" >
                                 <ClientSideEvents Click="function(s,e){
                     
                     grid.PerformCallback('Query');
                        
                    }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="计划号（可选）:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="TextPlanCode2" runat="server" ReadOnly="True" Width="100px">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        
                        <td>
                            <dx:ASPxButton ID="BtnConfirm"  AutoPostBack="false" runat="server"
                                Text="全部确认" ClientInstanceName="btnConfirm" >
                                
                                <ClientSideEvents Click="function (s,e){ changeSeq(); }" />
                            </dx:ASPxButton>
                            
                        </td>
                          <td>
                              &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td>

                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="GHTM"
                    AutoGenerateColumns="False"  OnCustomCallback="ASPxGridView1_CustomCallback" OnCustomDataCallback="ASPxGridView1_CustomDataCallback" 
                     OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize" onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" >
                    <SettingsEditing PopupEditFormWidth="550" PopupEditFormHeight="200" />
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
                        
                        <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" VisibleIndex="1" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="加工单" FieldName="JHDM" VisibleIndex="1"
                            Width="130px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Visible="false"
                              Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="2" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="下线时间" FieldName="GZRQ" VisibleIndex="3"
                            Width="150px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                      <%--  <dx:GridViewCommandColumn Caption="清单"  Width="80px" ButtonType="Button" VisibleIndex="4"   >
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="Bill" Text="清单" >
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>--%>
                        
                        <dx:GridViewCommandColumn Caption="状态1"  ButtonType="Button"  VisibleIndex="5"
                            Width="100px" >
                            <CustomButtons>
                                <dx:GridViewCommandColumnCustomButton ID="Offset1" Text="未回冲"  >
                                </dx:GridViewCommandColumnCustomButton>
                                 <dx:GridViewCommandColumnCustomButton ID="Offset2" Text="已冲" >
                                </dx:GridViewCommandColumnCustomButton>
                                 <dx:GridViewCommandColumnCustomButton ID="Offset3" Text="等待回冲" >
                                </dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <%--<dx:GridViewCommandColumn Caption=" "  Width="80px" ButtonType="Button" VisibleIndex="6">
                        <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Part" Text="物料">
                    </dx:GridViewCommandColumnCustomButton>  
                </CustomButtons>             
            </dx:GridViewCommandColumn>--%>
                        <dx:GridViewDataTextColumn Caption="状态2" FieldName="STATE2" VisibleIndex="6"
                            Width="80px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn Caption="状态3" FieldName="STATE3" VisibleIndex="7"
                            Width="80px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                        </dx:GridViewDataTextColumn>
                    </Columns>
   
                   <ClientSideEvents  CustomButtonClick="function(s, e){
            ButtonClick(s, e);
           

            }" />

                </dx:ASPxGridView>
            </td>
            <%--<td style="width: 450px" >

                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" KeyFieldName="ABOM_JHDM"
                    Width="600px"    OnCustomCallback="ASPxGridView2_CustomCallback"  >
                    <Columns>
                    
                        <dx:GridViewDataTextColumn Caption="零件" FieldName="ABOM_COMP" VisibleIndex="1" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="ABOM_QTY" VisibleIndex="1"
                            Width="60px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工序" FieldName="ABOM_OP" VisibleIndex="2" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="ABOM_WKCTR" VisibleIndex="3"
                            Width="80px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划号" FieldName="ABOM_JHDM" VisibleIndex="4"
                            Width="100px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="ABOM_SO" VisibleIndex="5" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="日期" FieldName="ABOM_DATE" VisibleIndex="6" Width="100px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>--%>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>
</asp:Content>
