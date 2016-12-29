<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Login_RmesDefaultPage"  StylesheetTheme="Theme1"   MasterPageFile="~/MasterPage.master" Codebehind="RmesDefaultPage.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxDocking" tagprefix="dx" %>
<%@ Register Src="~/Rmes/Pub/Ascx/CalendarWidget.ascx" TagName="DateTime" TagPrefix="widget" %>
<%@ Register Src="~/Rmes/Pub/Ascx/DateTimeWidget.ascx" TagName="Weather" TagPrefix="widget" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraCharts.v11.1.Web, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>
<%@ Register assembly="DevExpress.XtraCharts.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts" tagprefix="cc1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
    // <![CDATA[
        function ShowWidgetPanel(widgetPanelUID) {
            var panel = dockManager.GetPanelByUID(widgetPanelUID);
            panel.Show();
        }
        function SetWidgetButtonVisible(widgetName, visible) {
            var button = ASPxClientControl.GetControlCollection().GetByName('widgetButton_' + widgetName);
            //button.GetMainElement().className = visible ? '' : 'disabled';
        }
     // ]]> 
    </script>
    <dx:ASPxDockManager runat="server" ID="ASPxDockManager" ClientInstanceName="dockManager">
        <ClientSideEvents
            PanelShown="function(s, e) { SetWidgetButtonVisible(e.panel.panelUID, false) }"
            PanelCloseUp="function(s, e) { SetWidgetButtonVisible(e.panel.panelUID, true) }"/>
    </dx:ASPxDockManager>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" 
        SelectCommand="">
    </asp:SqlDataSource>
    <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" 
        SelectCommand=""></asp:SqlDataSource>--%>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>
    <dx:ASPxDockPanel runat="server" ID="DateTimePanel" PanelUID="DateTime" HeaderText="时间"
        OwnerZoneUID="LeftZone" ClientInstanceName="dateTimePanel" Height="200px" >
        <ContentCollection>
            <dx:PopupControlContentControl>
                <widget:Weather runat="server" ID="DateTimeWidget" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxDockPanel>
    <dx:ASPxDockPanel runat="server" ID="CalendarPanel" PanelUID="Calendar" 
        HeaderText="日期" OwnerZoneUID="LeftZone" ClientInstanceName="calendarPanel" Height="200px">
        <ContentCollection>
            <dx:PopupControlContentControl>
                <widget:DateTime ID="CalendarWidget" runat="server" />
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxDockPanel>
    <dx:ASPxDockPanel runat="server" ID="DuckPanel" PanelUID="Duck" Left="500" ShowOnPageLoad="false" 
        HeaderText="休息一下" ClientInstanceName="duckPanel" Height="450px" Width="300px">
        <ContentCollection>
        <dx:PopupControlContentControl>
            <object type="application/x-shockwave-flash"  data="../Pub/flash/3556.swf" width="300px" height="450px"  ><param name="movie" value="../Pub/flash/3556.swf"  /><param name="WMODE" value="transparent"></object>
        </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxDockPanel>    
<%--    <dx:ASPxDockPanel runat="server" ID="ProductPie" PanelUID="ProductPie" HeaderText="生产完成统计"
        OwnerZoneUID="RightZone" ClientInstanceName="dateTimePanel" Height="400px"  >
        <ContentCollection>
            <dx:PopupControlContentControl>
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="SqlDataSource2" KeyFieldName="PLAN_CODE" Width="600px">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="PLAN_CODE" VisibleIndex="0" Caption="计划号" Width="150px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PLAN_ONLINE_QUANTITY" VisibleIndex="1" Caption="数量" Width="70px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PLAN_OFFLINE_QUANTITY" VisibleIndex="2" Caption="下线数量"  Width="70px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataProgressBarColumn FieldName="PLAN_COMPLETE" VisibleIndex="3" Caption="进度" >
                            <PropertiesProgressBar Width="" Height="15px" Minimum="1" Maximum = "100" ></PropertiesProgressBar>
                        </dx:GridViewDataProgressBarColumn>
                    </Columns>
                    <Settings ShowFilterRow="False" ShowGroupPanel="False"  ShowHeaderFilterButton="False" VerticalScrollableHeight="150"  ></Settings>
                    <SettingsPager Mode="ShowAllRecords"  ></SettingsPager>
                </dx:ASPxGridView>
                <br />
                <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" 
                    DataSourceID="SqlDataSource1" Height="200px" Width="600px" >
                    <diagramserializable>
                        <cc1:XYDiagram>
                            <axisx visibleinpanesserializable="-1">
                                <range sidemarginsenabled="True" />
                            </axisx>
                            <axisy visibleinpanesserializable="-1">
                                <range sidemarginsenabled="True" />
                            </axisy>
                        </cc1:XYDiagram>
                    </diagramserializable>
                    
                    <fillstyle>
                        <optionsserializable>
                            <cc1:SolidFillOptions />
                        </optionsserializable>
                    </fillstyle>
                    <seriesserializable>
                        <cc1:Series ArgumentDataMember="DATE_MONTH" LegendText="实际产量" Name="Series 1" ValueDataMembersSerializable="MONTH_COMPLETE">
                            <viewserializable>
                                <cc1:SideBySideBarSeriesView BarWidth="0.3">
                                    <border visible="False" />
                                </cc1:SideBySideBarSeriesView>
                            </viewserializable>
                            <labelserializable>
                                <cc1:SideBySideBarSeriesLabel BackColor="Transparent" LineVisible="True" Position="TopInside">
                                    <border visible="False" />
                                    <fillstyle>
                                        <optionsserializable>
                                            <cc1:SolidFillOptions />
                                        </optionsserializable>
                                    </fillstyle>
                                </cc1:SideBySideBarSeriesLabel>
                            </labelserializable>
                            <pointoptionsserializable>
                                <cc1:PointOptions>
                                </cc1:PointOptions>
                            </pointoptionsserializable>
                            <legendpointoptionsserializable>
                                <cc1:PointOptions>
                                </cc1:PointOptions>
                            </legendpointoptionsserializable>
                        </cc1:Series>
                        <cc1:Series ArgumentDataMember="DATE_MONTH" LegendText="目标产量" Name="Series 2" 
                            ValueDataMembersSerializable="MONTH_TARGET">
                            <viewserializable>
                                <cc1:LineSeriesView>
                                </cc1:LineSeriesView>
                            </viewserializable>
                            <labelserializable>
                                <cc1:PointSeriesLabel LineVisible="True">
                                    <fillstyle>
                                        <optionsserializable>
                                            <cc1:SolidFillOptions />
                                        </optionsserializable>
                                    </fillstyle>
                                </cc1:PointSeriesLabel>
                            </labelserializable>
                            <pointoptionsserializable>
                                <cc1:PointOptions>
                                </cc1:PointOptions>
                            </pointoptionsserializable>
                            <legendpointoptionsserializable>
                                <cc1:PointOptions>
                                </cc1:PointOptions>
                            </legendpointoptionsserializable>
                        </cc1:Series>
                    </seriesserializable>
                    <seriestemplate>
                        <viewserializable>
                            <cc1:SideBySideBarSeriesView></cc1:SideBySideBarSeriesView>
                        </viewserializable>
                        <labelserializable>
                            <cc1:SideBySideBarSeriesLabel LineVisible="True">
                                <fillstyle>
                                    <optionsserializable>
                                        <cc1:SolidFillOptions />
                                    </optionsserializable>
                                </fillstyle>
                            </cc1:SideBySideBarSeriesLabel>
                        </labelserializable>
                        <pointoptionsserializable>
                            <cc1:PointOptions></cc1:PointOptions>
                        </pointoptionsserializable>
                        <legendpointoptionsserializable>
                            <cc1:PointOptions></cc1:PointOptions>
                        </legendpointoptionsserializable>
                    </seriestemplate>
                    <titles>
                        <cc1:ChartTitle Text="年度月生产统计"/>
                    </titles>
                </dxchartsui:WebChartControl>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxDockPanel>    
--%>    <div>
        <dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl='~/Rmes/Pub/Images/FirstPage/DateTime.png' Cursor="pointer"
            ClientInstanceName='widgetButton_DateTime' ToolTip='时间' >
            <ClientSideEvents Click="function(s, e) {
                ShowWidgetPanel('DateTime');
            }" />
        </dx:ASPxImage>
        <dx:ASPxImage ID="ASPxImage2" runat="server" ImageUrl='~/Rmes/Pub/Images/FirstPage/Calendar.png' Cursor="pointer"
            ClientInstanceName='widgetButton_Calendar' ToolTip='日历' >
            <ClientSideEvents Click="function(s, e) {
                ShowWidgetPanel('Calendar');
            }" />
        </dx:ASPxImage>
<%--        <dx:ASPxImage ID="ASPxImage3" runat="server" ImageUrl='~/Rmes/Pub/Images/FirstPage/ProductPie.png' Cursor="pointer"
            ClientInstanceName='widgetButton_ProductPie' ToolTip='生产完成统计' >
            <ClientSideEvents Click="function(s, e) {
                ShowWidgetPanel('ProductPie');
            }" />
        </dx:ASPxImage>--%>
        <dx:ASPxImage ID="ASPxImage4" runat="server" ImageUrl='~/Rmes/Pub/Images/FirstPage/Duck.png' Cursor="pointer"
            ClientInstanceName='widgetButton_Duck' ToolTip='休息一下' >
            <ClientSideEvents Click="function(s, e) {
                ShowWidgetPanel('Duck');
            }" />
        </dx:ASPxImage>                     
    </div>
    
    
    
    <table>
    <tr>
    <td>
        <dx:ASPxDockZone runat="server" ID="ASPxDockZone1" ZoneUID="LeftZone"   Width="300px" PanelSpacing="5">
        </dx:ASPxDockZone>
    </td>
    <td>
        <dx:ASPxDockZone runat="server" ID="ASPxDockZone2" ZoneUID="RightZone"   Width="650px" PanelSpacing="2"></dx:ASPxDockZone>    
    </td>
    <td>
        <dx:ASPxDockZone runat="server" ID="ASPxDockZone3" ZoneUID="RightEstZone"   Width="200px" PanelSpacing="2"></dx:ASPxDockZone>    
    </td>    
    </tr>
    </table>



</asp:Content>