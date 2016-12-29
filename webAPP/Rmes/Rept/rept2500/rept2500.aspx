<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept2500.aspx.cs" Inherits="Rmes_Rept_rept2500_rept2500" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--装配BOM对比--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="计划代码一:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="Plan1" runat="server" ValueType="System.String" Width="140px"
                    SelectedIndex="0">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="计划代码二:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="Plan2" runat="server" ValueType="System.String" Width="140px"
                    SelectedIndex="0">
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode"
                    SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td style="width: 100px">
                <dx:ASPxCheckBox ID="chkProcess" Text="按工序统计" runat="server" Checked="false">
                </dx:ASPxCheckBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="对比" Width="80px">
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
   <%-- <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="说明：差异结果列为0表示两者相同，为1表示为项目一仅有，为2表示项目二仅有！">
                </dx:ASPxLabel>
            </td>
        </tr>
    </table>--%>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="True" KeyFieldName="零件"
        OnCustomCallback="ASPxGridView1_CustomCallback"   >
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
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="StationCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
