<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv8100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv8100.inv8100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--生产完成统计--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
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
    </script>
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
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode"
                    SelectedIndex="0">
                    <ClientSideEvents SelectedIndexChanged="function(s,e){
                           txtSCode1.PerformCallback(PCode.GetValue()); 
                        }" />
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="站点:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtSCode" runat="server" ClientInstanceName="txtSCode1" DataSourceID="StationCode"
                    TextField="STATION_NAME" ValueField="STATION_CODE" ValueType="System.String"
                    Width="100px" SelectedIndex="0" OnCallback="txtSCode_Callback">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="查询方式" Width="60px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtChose" runat="server" ValueType="System.String" Width="100px"
                    ClientInstanceName="Chose">
                    <Items>
                        <dx:ListEditItem Text="按班次查询" Value="BC" />
                        <dx:ListEditItem Text="按时间查询" Value="RQ" />
                    </Items>
                    <ClientSideEvents SelectedIndexChanged="function(s,e){
                    initEditSeries(s, e);
                }" />
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
                    ClientVisible="false" />
                <dx:ASPxLabel ID="LabBC" runat="server" Text="班次:" ClientVisible="true" ClientInstanceName="LabBC" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" ClientVisible="false" Width="100px"
                    ClientInstanceName="DateEdit2">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
                <dx:ASPxComboBox ID="txtBc" runat="server" ValueType="System.String" Width="100px"
                    ClientVisible="true" ClientInstanceName="txtBC">
                    <Items>
                        <dx:ListEditItem Text="全部" Value="ALL" />
                        <dx:ListEditItem Text="白班" Value="DAY" />
                        <dx:ListEditItem Text="夜班" Value="NIGHT" />
                    </Items>
                </dx:ASPxComboBox>
            </td>
            <td style="text-align: left">
                <dx:ASPxCheckBox ID="chkJX" ClientInstanceName="QAD" runat="server" Checked="false"
                    Text="是否按型号统计" Visible="false">
                </dx:ASPxCheckBox>
                <td>
                    <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();grid2.PerformCallback();
                        
                    }" />
                    </dx:ASPxButton>
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table>
        <tr style="width:1000px">
            <td >
                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="false"
                    OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="JHDM" Width="650px">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="站点名称" FieldName="站点名称" VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="2" Width="130px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="机型" FieldName="PLAN_SO" VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="计划数量" FieldName="计划数量" VisibleIndex="4" Width="80px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="完成数量" FieldName="完成数量" VisibleIndex="5" Width="80px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
            <td>
                <fieldset style="text-align: left;">
                    <legend><span style="font-size: 10pt;  ">
                        <asp:Label ID="Label5" runat="server" Text="按型号统计" Font-Bold="True"></asp:Label></span></legend>
                    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="false"
                        OnCustomCallback="ASPxGridView2_CustomCallback" KeyFieldName="PLAN_SO" Width="300px"     >
                          <Columns>
                        <dx:GridViewDataTextColumn Caption="站点名称" FieldName="站点名称" VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                         
                        <dx:GridViewDataTextColumn Caption="机型" FieldName="PLAN_SO" VisibleIndex="3" Width="120px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                         
                        <dx:GridViewDataTextColumn Caption="完成数量" FieldName="完成数量" VisibleIndex="5" Width="80px" Settings-AutoFilterCondition="Contains" >
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    </dx:ASPxGridView>
                </fieldset>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="StationCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
