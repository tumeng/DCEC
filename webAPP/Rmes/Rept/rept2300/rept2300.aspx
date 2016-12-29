<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept2300.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept2300.rept2300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%--改制测量日志查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
       
            <td style="width: 10px">
            </td>
            <td style="text-align: left; width: 60px;">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="日期时间:" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="100px">
                    <calendarproperties clearbuttontext="清空" todaybuttontext="今天">
                    </calendarproperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                    <calendarproperties clearbuttontext="清空" todaybuttontext="今天">
                    </calendarproperties>
                </dx:ASPxDateEdit>
            </td>
      
   
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="流水号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSN" runat="server" ValueType="System.String" Width="100px"
                    ClientInstanceName="Vendor">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                    <clientsideevents click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
      
        
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="true"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="SN">
        <Columns>
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="检测数据名称" FieldName="DETECT_NAME" VisibleIndex="2"
                Width="140px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="质检结果" FieldName="DETECT_VALUE" VisibleIndex="3"
                Width="250px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="4"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="发动机系列" FieldName="PRODUCT_SERIES" VisibleIndex="6"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="检测时间" FieldName="WORK_TIME" VisibleIndex="7"
                Width="150px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
