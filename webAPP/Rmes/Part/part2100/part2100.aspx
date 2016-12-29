<%@ Page Title="物料清单查询" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="part2100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part2100.part2100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>

<%--物料清单查询 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function refreshGrid2(s, e) {
//            grid2.PerformCallback(s.GetFocusedRowIndex());
        }
    </script>

    <table>
         
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="表:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboTable" runat="server" DropDownStyle="DropDownList" SelectedIndex="0" Width="120px">
                    <Items>
                        <dx:ListEditItem Value="三方物流"/>
                        <%--<dx:ListEditItem Value="内装"/>--%>
                    </Items>
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="生产线:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboGzdd" runat="server" DataSourceID="sqlGzdd" ValueField="PLINE_CODE" TextField="PLINE_NAME" Width="120px" SelectedIndex=0>
                <ClientSideEvents  SelectedIndexChanged="function(s, e){
                        billCode.PerformCallback();
                        
                        }"/>
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="上线时间:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="DateStart" runat="server" Width="120px">
                    <%-- DateChanged能在ClientSideEvents里写，跟click类似，所以可以写js事件 --%>
                    <ClientSideEvents DateChanged="function(s, e){
                        billCode.PerformCallback();
                        //grid.PerformCallback();
                        //grid2.PerformCallback();
                        }"/>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="到"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="DateEnd" runat="server" Width="120px">
                    <%-- 结束日期变化时也要刷新combobillcode --%>
                    <ClientSideEvents DateChanged="function(s, e){
                        billCode.PerformCallback();
                        //grid.PerformCallback();
                        //grid2.PerformCallback();
                        }"/>
                    
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="单据号:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboBillCode" runat="server" DataSourceID="sqlBillCode" TextField="BILL_CODE" Width="180px" ClientInstanceName="billCode"
                    OnCallback="ComboBillCode_Callback" SelectedIndex=0>
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxButton ID="btnBillCode" runat="server" Text="查询单据号" Visible="false" AutoPostBack="false">
                    <ClientSideEvents click="function(s, e){
                        billCode.PerformCallback();
                        grid.PerformCallback();
                        grid2.PerformCallback();
                        }"/>
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="queryGrid" runat="server" Text="查询" AutoPostBack="false" Width="100px">
                    <ClientSideEvents Click="function(){
                        grid.PerformCallback();
                        grid2.PerformCallback();
                        }"/>
                </dx:ASPxButton>
            </td>
        </tr>
        <tr>
            
            <%--<td>
                <dx:ASPxGridLookup ID="ComboBillCode" runat="server" DataSourceID="sqlBillCode"
                    ClientInstanceName="gridLookup" KeyFieldName="BILL_CODE" TextFormatString="{0}" SelectionMode="Single" AutoGenerateColumns="False" >
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " Width="10px" />
                        <dx:GridViewDataColumn FieldName="BILL_CODE" Caption="单据号" Width="150px"/>
                    </Columns>
                </dx:ASPxGridLookup>
            </td>--%>
            <%--<td>
                <dx:ASPxTextBox ID="ComboBillCode" runat="server" Width="150px"></dx:ASPxTextBox>
            </td>--%>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="工位:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="ComboLocationCode" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <%--<td>
                <dx:ASPxComboBox ID="ComboLocationCode" runat="server" DataSourceID="sqlLocationCode" TextField="LOCATION_CODE">
                </dx:ASPxComboBox>
            </td>--%>
            <%--<td>
                <dx:ASPxGridLookup ID="ComboLocationCode" runat="server" DataSourceID="sqlLocationCode" Width="120px"
                    ClientInstanceName="gridLookup" KeyFieldName="LOCATION_CODE" SelectionMode="Single">
                    <GridViewProperties>
                        <SettingsPager NumericButtonCount="5"></SettingsPager>
                    </GridViewProperties>
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="选择" Width="10%"/>
                        <dx:GridViewDataColumn FieldName="LOCATION_CODE" Caption="工位"/>
                        </Columns>
                </dx:ASPxGridLookup>
            </td>--%>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="保管员:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="ComboBgyCode" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
             <td>
                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="物料号:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="TextMaterialCode" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="备注:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="TextBillRemark" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <%--<td>
                <dx:ASPxComboBox ID="ComboBgyCode" runat="server" DataSourceID="sqlBgyCode" TextField="BGY_CODE">
                </dx:ASPxComboBox>
            </td>--%>
            <%--<td>
                <dx:ASPxGridLookup ID="ComboBgyCode" runat="server" DataSourceID="sqlBgyCode" Width="120px"
                    ClientInstanceName="gridLookup" KeyFieldName="BGY_CODE" SelectionMode="Single" TextFormatString="{0}" AutoGenerateColumns="False">
                    <GridViewProperties>
                        <SettingsPager NumericButtonCount="5"></SettingsPager>
                    </GridViewProperties>
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption="选择" Width="10%" />
                        <dx:GridViewDataColumn FieldName="BGY_CODE" Caption="保管员" Width="30px"/>
                    </Columns>
                </dx:ASPxGridLookup>
            </td>--%>
            <td>
            </td>
             
             <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" OnClick="btnXlsExport_Click"></dx:ASPxButton>
            </td>
        </tr>
        <tr>
           
            <td></td>
            
           
        </tr>
    </table>

    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" 
        runat="server" KeyFieldName="ROWID" OnCustomCallback="ASPxGridView1_CustomCallback">
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
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="60px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="单据号" FieldName="BILL_CODE" VisibleIndex="1" Width="140px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料号" FieldName="MATERIAL_CODE" VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="PT_DESC2" VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="保管员" FieldName="BGY_CODE" VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="SUM_MATERIAL_NUM" VisibleIndex="5" Width="60px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="6" Width="80px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="GYS_CODE" VisibleIndex="7" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商名称" FieldName="VD_SORT" VisibleIndex="8" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="库房" FieldName="INV_CODE" VisibleIndex="9" Width="80px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="日程单号" FieldName="PO_NBR" VisibleIndex="10" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="大致使用时间" FieldName="REQUIRE_TIME" VisibleIndex="11" Width="150px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="备注" FieldName="BILL_REMARK" VisibleIndex="12" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents FocusedRowChanged="function(s, e){
            refreshGrid2(s, e);
            }"
        />
    </dx:ASPxGridView>

    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" SettingsPager-Mode="ShowPager" OnCustomCallback="ASPxGridView2_CustomCallback"
        runat="server" KeyFieldName="PLAN_CODE" >
        <Columns>
            <dx:GridViewDataTextColumn Caption="单据号" FieldName="SF_JIT_ID" VisibleIndex="1" Width="140px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划数量" FieldName="PLAN_QTY" VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="当前工位" FieldName="CURRENT_LOCATION" VisibleIndex="5" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="大致使用时间" FieldName="REQUIRE_TIME" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
        </Columns>
         <Settings ShowFooter="false"/>
         <TotalSummary>
            <%--Count计算个数，什么类型都可以；Sum计算和，必须数字才能计算 --%>
            <dx:ASPxSummaryItem FieldName="SF_JIT_ID" SummaryType="Count" DisplayFormat="数量={0}"/>
        </TotalSummary>
    </dx:ASPxGridView>

    <asp:SqlDataSource ID="sqlGzdd" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlBillCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlLocationCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlBgyCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>

</asp:Content>
