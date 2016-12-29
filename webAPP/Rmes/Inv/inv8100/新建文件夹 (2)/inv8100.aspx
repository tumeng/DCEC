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
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode" SelectedIndex="0">
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
                <dx:ASPxComboBox ID="txtSCode" runat="server" ClientInstanceName="txtSCode1" DataSourceID="StationCode" TextField="STATION_NAME" 
                    ValueField="STATION_CODE" ValueType="System.String" Width="100px" 
                    SelectedIndex="0" oncallback="txtSCode_Callback">

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
                    Visible="true" Text="是否按型号统计">
                </dx:ASPxCheckBox>
                <td>
                    <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="60px">
                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
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
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="True"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName=" ">
         
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="StationCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
