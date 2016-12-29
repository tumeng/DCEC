<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept2800.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept2800.rept2800" %>

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
<%--拆解零件查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="发动机流水号">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSN" runat="server" ValueType="System.String" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="零件号">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtItem" runat="server" ValueType="System.String" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="供应商">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtVendor" runat="server" ValueType="System.String" Width="120px">
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="批次/流水号">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtPC" runat="server" ValueType="System.String" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td style="text-align: left;">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="拆解时间:" />
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="120px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="——" />
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="120px" ClientInstanceName="DateEdit2">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
                <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" Width="140px" />
            </td>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="true"
        OnCustomCallback="ASPxGridView1_CustomCallback">
        <Columns>
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" VisibleIndex="1" Width="80px"
                Settings-AutoFilterCondition="Contains" Visible="false">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件" FieldName="LJDM" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYSDM" VisibleIndex="3" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="批次" FieldName="LJLSH" VisibleIndex="4" Width="120px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="LJSL" VisibleIndex="5" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="拆解时间" FieldName="RQSJ" VisibleIndex="6" Width="130px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="站点" FieldName="ZDMC" VisibleIndex="7" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="班次" FieldName="BCMC" VisibleIndex="8" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="班组" FieldName="BZMC" VisibleIndex="9" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="员工" FieldName="YGMC" VisibleIndex="10" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
     
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
</asp:Content>
