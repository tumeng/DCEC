<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Inv_inv9800_inv9800"
    StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" CodeBehind="inv9800.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%--库存查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        


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

        
        function initBatch(s, e) {
            if (DateEdit1.GetValue() == null || DateEdit2.GetValue() == null || PCode.GetValue() == null) {
                alert("请先选择生产线和时间选项！");
                return;
            }
            var webFileUrl = "?DATEEDIT1=" + DateEdit1.GetText() + " &DATEEDIT2=" + DateEdit2.GetText() + " "
                           + "&PCODE=" + PCode.GetValue() + "&opFlag=getBatch";
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
            if (str1 == "Overtime") 
            {
            alert("选择日期范围不能超过31天，请重新选择！");
             Batch.SetValue("");
            }
            else if(str1 == "ok")
            {
                var items = str2.split('@');
                var items2 = str3.split('@');
                Batch.ClearItems();
                
                Batch.AddItem('全部批次', '%');
            for (var i = items.length - 1; i >= 0; i = i - 1) {
                Batch.AddItem(items[i], items[i]);
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
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode" SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
             <td style="text-align: left; width: 70px;">
                <label style="font-size: small">
                    开始日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="150px" ClientInstanceName="DateEdit1" EditFormatString="yyyy-MM-dd HH:mm:ss">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" >
                    </CalendarProperties>
                    <ClientSideEvents  ValueChanged="function(s,e){
                      initBatch(s, e); 
                }" />   
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 25px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="--" />
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="150px" ClientInstanceName="DateEdit2" EditFormatString="yyyy-MM-dd HH:mm:ss">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天"   >
                    </CalendarProperties>
                 <ClientSideEvents  ValueChanged="function(s,e){
                      initBatch(s, e); 
                }" />   
                </dx:ASPxDateEdit>
            </td>
           
            <td style="width: 25px">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="SO:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="textSO" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
           
            <td style="width: 60px">
                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="入库批次:">
                </dx:ASPxLabel>
            </td>
            <td style="width: 99px">
                <dx:ASPxComboBox ID="txtPc" runat="server" ValueType="System.String" Width="160px" ClientInstanceName="Batch">
            
                </dx:ASPxComboBox>
            </td>
            <td style="width: 44px">
                <dx:ASPxButton ID="ButSubmit" Text="查询" Width="80px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback(); grid2.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
           <td style="width: 20%">
                &nbsp;</td>
        </tr>
       <%-- <tr>
            <td style="width: 5px; height: 25px;">
            </td>
           
              <td>
                <dx:ASPxButton ID="Batdelete" runat="server" Text="批删除" Width="80px"  AutoPostBack="false" ClientInstanceName="Batdelete">
                    <ClientSideEvents    Click="function (s,e) { changeSeq(s,e);}" />
                </dx:ASPxButton>
            </td> 
            
        </tr>--%>
    </table>
    <table>
    <tr>
    <td style="width: 1000px"> 
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="GHTM;SO;GZRQ;RKDATE"
          AutoGenerateColumns="False" OnCustomCallback="ASPxGridView1_CustomCallback"
         Width="1000px" >
        <Settings ShowHorizontalScrollBar="false" />
         <SettingsBehavior AllowFocusedRow="True" />
        <Columns>
        
            

            <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" Visible="false" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="入/出" FieldName="RC" Width="60px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="80px" Settings-AutoFilterCondition="Contains">
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
            <dx:GridViewDataTextColumn Caption="从" FieldName="SOURCEPLACE" Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="到" FieldName="DESTINATION" Width="80px" Settings-AutoFilterCondition="Contains">
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
             <dx:GridViewDataTextColumn Caption="库位" FieldName="KW" Width="75px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            
        </Columns>
        <Settings ShowFooter="True" />
        <SettingsBehavior ColumnResizeMode="Control" />
         
    </dx:ASPxGridView>
    </td>
    <td style="width: 100px">
                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" KeyFieldName="SO"
                    Width="16px"  OnCustomCallback="ASPxGridView2_CustomCallback" 
                    style="margin-left: 0px" >
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="0" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="1"
                            Width="60px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                      
                       
                    </Columns>
                    <Settings ShowFooter="True" />
                  <TotalSummary>
            <dx:ASPxSummaryItem FieldName="SL" SummaryType="Sum" DisplayFormat="合计={0}"/>
            
        </TotalSummary>
        <SettingsBehavior ColumnResizeMode="Control" />  
                </dx:ASPxGridView>
            </td>
    </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>

</asp:Content>
