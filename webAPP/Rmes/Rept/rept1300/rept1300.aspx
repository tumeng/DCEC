<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept1300.aspx.cs" Inherits="Rmes_Rept_rept1300_rept1300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--生产日报表--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Text="一般统计" Visible="true" >
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode" runat="server" ValueType="System.String" Width="100px"
                                        TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode1"  SelectedIndex=0>
                                        <%-- <Items>
                                            <dx:ListEditItem Text="东区" Value="E" />
                                            <dx:ListEditItem Text="西区" Value="W" />
                                        </Items>--%>
                                    </dx:ASPxComboBox>
                                </td>
                                <%--  <td>
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="统计方式" Width="60px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtChose" runat="server" ValueType="System.String" Width="100px"
                    ClientInstanceName="Chose" >
                    <Items>
                        <dx:ListEditItem Text="一般统计" Value="A" />
                        <dx:ListEditItem Text="按型号统计" Value="B" />
                        <dx:ListEditItem Text="工位漏扫统计" Value="C" />
                        <dx:ListEditItem Text="漏扫数量统计" Value="D" />
                    </Items>
                     
                </dx:ASPxComboBox>
            </td>
        </tr>                
        <tr>--%>
                                <td>
                                </td>
                                <td style="text-align: left; width: 60px;">
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="日期时间:" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="100px" ClientInstanceName="DateEdit1">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" ClientInstanceName="LabDate" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="统计">
                                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                        OnClick="btnXlsExport_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="True"
                            OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="计划代码;计划SO">
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
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="按型号统计" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode2" runat="server" ValueType="System.String" Width="100px"
                                        TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode2"  SelectedIndex=0>
                                        <%-- <Items>
                                            <dx:ListEditItem Text="东区" Value="E" />
                                            <dx:ListEditItem Text="西区" Value="W" />
                                        </Items>--%>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: left; width: 60px;">
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="日期时间:" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="100px" ClientInstanceName="DateEdit1">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="--" ClientInstanceName="LabDate" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit4" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnQuery2" runat="server" AutoPostBack="False" Text="统计">
                                        <ClientSideEvents Click="function(s,e){
                        grid2.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnXlsExport2" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                        OnClick="btnXlsExport2_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="True"
                            OnCustomCallback="ASPxGridView2_CustomCallback" KeyFieldName="产品代码;完成数量">
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
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="工位漏扫统计" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode3" runat="server" ValueType="System.String" Width="100px"
                                        TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode3"  SelectedIndex=0>
                                        <%-- <Items>
                                            <dx:ListEditItem Text="东区" Value="E" />
                                            <dx:ListEditItem Text="西区" Value="W" />
                                        </Items>--%>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: left; width: 60px;">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="日期时间:" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit5" runat="server" Width="100px" ClientInstanceName="DateEdit1">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="--" ClientInstanceName="LabDate" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit6" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnQuery3" runat="server" AutoPostBack="False" Text="统计">
                                        <ClientSideEvents Click="function(s,e){
                        grid3.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnXlsExport3" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                        OnClick="btnXlsExport3_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="grid3" runat="server" AutoGenerateColumns="True"
                            OnCustomCallback="ASPxGridView2_CustomCallback" KeyFieldName="漏扫条码;工位号">
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
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="漏扫数量统计" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl3" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="生产线:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="txtPCode4" runat="server" ValueType="System.String" Width="100px"
                                        TextField="PLINE_NAME" ValueField="PLINE_CODE" DataSourceID="SqlCode4" SelectedIndex=0>
                                        <%-- <Items>
                                            <dx:ListEditItem Text="东区" Value="E" />
                                            <dx:ListEditItem Text="西区" Value="W" />
                                        </Items>--%>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: left; width: 60px;">
                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="日期时间:" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit7" runat="server" Width="100px" ClientInstanceName="DateEdit1">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="--" ClientInstanceName="LabDate" />
                                </td>
                                <td style="width: 100px">
                                    <dx:ASPxDateEdit ID="ASPxDateEdit8" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnQuery4" runat="server" AutoPostBack="False" Text="统计">
                                        <ClientSideEvents Click="function(s,e){
                        grid4.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnXlsExport4" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                        OnClick="btnXlsExport4_Click" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView4" ClientInstanceName="grid4" runat="server" AutoGenerateColumns="True"
                            OnCustomCallback="ASPxGridView2_CustomCallback" KeyFieldName="工位;漏扫数量">
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
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="ASPxGridView2">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" runat="server" GridViewID="ASPxGridView3">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter4" runat="server" GridViewID="ASPxGridView4">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="SqlCode1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode3" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode4" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    --%>
</asp:Content>
