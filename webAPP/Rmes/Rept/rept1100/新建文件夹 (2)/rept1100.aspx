<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" Theme="Theme1"
    AutoEventWireup="true" CodeBehind="rept1100.aspx.cs" Inherits="Rmes_Rept_rept1100_rept1100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：实际装机清单查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
       function initListZD() {
            cmbScode.PerformCallback();
        }
    </script>
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="80px" ClientInstanceName="PCode" SelectedIndex="0">
                     <ClientSideEvents SelectedIndexChanged="function(s,e){
                           cmbScode.PerformCallback(PCode.GetValue());
                        }" />
                </dx:ASPxComboBox>
            </td>
               <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="站点:">
                
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtSCode" runat="server" DataSourceID="StationCode" TextField="STATION_NAME"
                    ValueField="STATION_CODE" ValueType="System.String" Width="120px"  
                    ClientInstanceName="cmbScode" OnCallback="cmbScode_Callback"  >
                    
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="起始流水号">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtLSH1" runat="server" Width="100px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="截止流水号" Width="60px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtLSH2" runat="server" Width="100px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="查询">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback(); grid2.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
        </tr>
    </table>
     <table>
     <tr>
     <td>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="SN" OnCustomCallback="ASPxGridView1_CustomCallback">
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
       
            <columns>
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="1" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件代码" FieldName="ITEM_CODE" VisibleIndex="3" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件名称" FieldName="ITEM_NAME" VisibleIndex="4" Width="130px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="5" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="6" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="ITEM_VENDOR" VisibleIndex="7" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="日期" FieldName="CREATE_TIME" VisibleIndex="8" Width="140px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn VisibleIndex="19" Width="10%" />
        </columns>
    </dx:ASPxGridView>
    </td>
    <td> 
    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="ITEM_CODE" OnCustomCallback="ASPxGridView2_CustomCallback">
            <columns>
            <dx:GridViewDataTextColumn Caption="零件代码" FieldName="ITEM_CODE" VisibleIndex="1" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件名称" FieldName="ITEM_NAME" VisibleIndex="2" Width="130px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="QTY" VisibleIndex="3" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
             
        </columns>
    </dx:ASPxGridView>
    </td>
    </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
        <asp:SqlDataSource ID="StationCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
