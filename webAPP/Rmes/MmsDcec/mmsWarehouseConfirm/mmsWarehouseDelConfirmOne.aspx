<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsWarehouseDelConfirmOne.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsWarehouseConfirm.mmsWarehouseDelConfirmOne" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>库房删除确认（一对一）</title>
</head>
<body>
    <form id="form1" runat="server">

    <table style="width:100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线：">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="cmbPline" runat="server" Width="100px" SelectedIndex="0"
                                oninit="cmbPline_Init">
                            </dx:ASPxComboBox>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="日期：">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dateFrom" runat="server" Width="100px" 
                                DisplayFormatString="yyyy-MM-dd">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="到">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="dateTo" runat="server" Width="100px" 
                                DisplayFormatString="yyyy-MM-dd">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" AutoPostBack="false" Width="80px">
                            <ClientSideEvents Click="function(s,e){
                                gridOnePlace.PerformCallback();
                            }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr><td><hr /></td></tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="待确认物料：">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="gridOnePlace" ClientInstanceName="gridOnePlace" 
                    runat="server" Width="100%" oncustomcallback="gridOnePlace_CustomCallback">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="60px">
                            <DeleteButton Visible="true"></DeleteButton>
                            <ClearFilterButton Visible="true"></ClearFilterButton>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="2" Width="100px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM1" VisibleIndex="3" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC1" VisibleIndex="4" Width="120px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="替换零件" FieldName="LJDM2"  VisibleIndex="5" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="替换零件名称" FieldName="LJMC2" VisibleIndex="6" Width="120px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="7" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="提交时间" FieldName="TJSJ" VisibleIndex="8" Width="120px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="确认时间" FieldName="QRSJ" VisibleIndex="9" Width="120px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="提交员工" FieldName="TJYH" VisibleIndex="10" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="确认员工" FieldName="QRYH" VisibleIndex="11" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="地点" FieldName="GZDD" VisibleIndex="12" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="确认" FieldName="FLAG" VisibleIndex="13" Width="60px" Visible="false"></dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100" ></SettingsPager>
                    <Settings ShowHorizontalScrollBar="true" ShowGroupPanel="false" />
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="发动机记录：">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" Width="100%">
                </dx:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>

    </form>
</body>
</html>
