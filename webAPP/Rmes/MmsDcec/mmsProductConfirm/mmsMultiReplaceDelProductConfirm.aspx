﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsMultiReplaceDelProductConfirm.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsProductConfirm.mmsMultiReplaceDelProductConfirm" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生产删除确认（多对多）</title>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        function OnGetGridRowValues(values) {
            var planCode = values[0];
            var locationCode = values[1];
            grid.PerformCallback(planCode + "," + locationCode);
        }    
    </script>
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
                                var plineCode = cmbPline.GetValue();
                                if (plineCode == null) {
                                    alert('请选择生产线！');
                                    return false;
                                }
                                gridMultiPlace.PerformCallback();
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
                <dx:ASPxGridView ID="gridMultiPlace" ClientInstanceName="gridMultiPlace" KeyFieldName="JHDM"
                    runat="server" Width="100%" oncustomcallback="gridMultiPlace_CustomCallback" 
                    oncustombuttoninitialize="gridMultiPlace_CustomButtonInitialize" 
                    oncustombuttoncallback="gridMultiPlace_CustomButtonCallback" >
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="50px">
                             <CustomButtons >
                                <dx:GridViewCommandColumnCustomButton ID="Confirm" Text="确认" Visibility="Invisible"></dx:GridViewCommandColumnCustomButton>
                             </CustomButtons>
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="1" Width="100px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="2" Width="100px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="组号" FieldName="THEGROUP" VisibleIndex="3" Width="100px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="提交时间" FieldName="TJSJ" VisibleIndex="3" Width="120px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="确认时间" FieldName="QRSJ" VisibleIndex="4" Width="120px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="提交用户" FieldName="TJYH"  VisibleIndex="5" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="确认用户" FieldName="QRYH" VisibleIndex="6" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="12" Width="60px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="确认" FieldName="FLAGNAME" VisibleIndex="13" Width="80px"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FLAG" Visible="false"></dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100" ></SettingsPager>
                    <Settings ShowHorizontalScrollBar="true" ShowVerticalScrollBar="false" ShowGroupPanel="false" />
                    <ClientSideEvents RowClick="function(s,e){
                        var index=e.visibleIndex;
                        gridMultiPlace.GetRowValues(index, 'JHDM;THEGROUP', OnGetGridRowValues); 
                    }" />
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
                <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"  KeyFieldName="SN"
                    Width="100%" oncustomcallback="grid_CustomCallback">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="条码" FieldName="SN" VisibleIndex="1" Width="20%"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" VisibleIndex="2" Width="20%"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="机型" FieldName="PRODUCT_MODEL" VisibleIndex="3" Width="20%"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="所在站点" FieldName="ZDMC" VisibleIndex="4" Width="40%"></dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100" ></SettingsPager>
                    <Settings ShowHorizontalScrollBar="true" ShowVerticalScrollBar="false" ShowGroupPanel="false" />
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