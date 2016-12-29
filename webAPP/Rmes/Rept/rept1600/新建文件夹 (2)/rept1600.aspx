<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept1600.aspx.cs" Inherits="Rmes_Rept_rept1600_rept1600" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%--装配零件批次查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="发动机流水号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSN" runat="server" ValueField="SN" ValueType="System.String"
                    Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="零件号:" Width="45px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtItem" runat="server" ValueField="ITEM_CODE" ValueType="System.String"
                    Width="120px">
                </dx:ASPxTextBox>
            </td>
        <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="供应商:" Width="45px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtGYS" runat="server" ValueField="VENDOR_CODE" ValueType="System.String"
                    Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="批次/流水号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtPC" runat="server" ValueField="ITEM_BATCH" ValueType="System.String"
                    Width="120px">
                </dx:ASPxTextBox>
            </td>     
          
        </tr>
        <tr>
            <td>
            </td>
             <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="站点:">
                
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtSCode" runat="server" DataSourceID="StationCode" TextField="STATION_NAME"
                    ValueField="STATION_CODE" ValueType="System.String" Width="120px"  >
                    
                </dx:ASPxComboBox>
            </td>
              <td style="text-align: left; width: 80px;">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="日期时间:" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="120px" >
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" ClientInstanceName="LabDate" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="120px"  >
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
           
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" OnClick="btnXlsExport_Click" Width="125px">
                </dx:ASPxButton>
            </td>
        </tr>
       
    </table>
 <%--   <table>
     <tr>
        <td></td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="不输入流水号的时候默认查询该段时间内未下线的装配零件信息">
                </dx:ASPxLabel>
            </td>
        </tr>
    </table>--%>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="SN">
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
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件号" FieldName="ITEM_CODE" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件供应商" FieldName="ITEM_VENDOR" VisibleIndex="3"
                Width="150px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件批次号/流水号" FieldName="ITEM_SN" VisibleIndex="4" Width="150px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="CREATE_TIME" VisibleIndex="5"
                Width="140px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="站点" FieldName="STATION_NAME" VisibleIndex="6"
                Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="员工" FieldName="USER_NAME" VisibleIndex="9" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="StationCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
</asp:Content>
