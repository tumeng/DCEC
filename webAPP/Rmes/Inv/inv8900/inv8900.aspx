<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv8900.aspx.cs" Inherits="Rmes_Inv_inv8900_inv8900" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--报表--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script type="text/javascript">
        function initEditSeries(s, e) {
            if (Chose.GetValue() == null) {
                return;
            }
            if (Chose.GetText() == '按班次查询') {
                LabBC.SetVisible(true);
                txtBC.SetVisible(true);
                DateEdit2.SetVisible(false);
                LabDate.SetVisible(false);
            }
            else {
                LabBC.SetVisible(false);
                txtBC.SetVisible(false);
                DateEdit2.SetVisible(true);
                LabDate.SetVisible(true);
            }

        }
    </script>--%>
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode" SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
        </tr>
        <tr>
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
                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" ClientInstanceName="LabDate"
                    ClientVisible="true" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientVisible="true" Width="100px"
                    ClientInstanceName="DateEdit2">
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
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="SO"  >
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
            <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains"  >
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="期初库" FieldName="QCKC" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="入库1" FieldName="RK1" VisibleIndex="3" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="出库1" FieldName="CK1" VisibleIndex="4" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="正常入库" FieldName="RK0" VisibleIndex="5" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="返修入库" FieldName="RK2" VisibleIndex="6" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零公里入库" FieldName="RK3" VisibleIndex="7" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="改制入库" FieldName="RK4" VisibleIndex="8" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="快返中心入库" FieldName="RK5" VisibleIndex="9" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="借用入库" FieldName="RK6" VisibleIndex="10" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="质量部退还入库" FieldName="RK7" VisibleIndex="11" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="产品部退入库" FieldName="RK8" VisibleIndex="12" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="ME退还入库" FieldName="RK9" VisibleIndex="13" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="建储入库" FieldName="RK10" VisibleIndex="14" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="入库合计" FieldName="RKHJ" VisibleIndex="15" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="外销库" FieldName="WXK" VisibleIndex="16" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="十堰库" FieldName="SYK" VisibleIndex="17" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="其他" FieldName="QT" VisibleIndex="18" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="出库合计" FieldName="CKHJ" VisibleIndex="19" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="库存" FieldName="KC" VisibleIndex="20" Width="60px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="StationCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
