<%@ Page Title="MES需求发出查询" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="part2200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part2200.part2200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%--MES需求发出查询 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="height:30px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="查询条件"></dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="生产线:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="ComboGzdd" runat="server" DataSourceID="sqlGzdd" ValueField="PLINE_CODE" TextField="PLINE_NAME" Width="120px">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="物料号:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="TextPartNbr" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="供应商:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="TextPoVend" runat="server" Width="120px"></dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="起始日期:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="DateStart" runat="server" Width="120px"></dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="截止日期:"></dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxDateEdit ID="DateEnd" runat="server" Width="120px"></dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxButton ID="queryBill" runat="server" Text="查询" AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e){
                        grid.PerformCallback();
                        }"/>
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ROWID" ClientInstanceName="grid" OnCustomCallback="ASPxGridView1_CustomCallback">
                       <Settings ShowFooter="True" />
                         <TotalSummary>
            <dx:ASPxSummaryItem FieldName="RCT_AMOUNT" SummaryType="Sum" DisplayFormat="总数={0}"/>
        </TotalSummary>
        <Columns>
            <dx:GridViewDataTextColumn Caption="物料号" FieldName="PART_NBR" VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="PO_VEND" VisibleIndex="3" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="PT_DESC2" VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料数量" FieldName="RCT_AMOUNT" VisibleIndex="5" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="日期" FieldName="IMP_DATETIME" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
<%--            <dx:GridViewDataTextColumn Caption="QAD地点" FieldName="PO_NBER" VisibleIndex="6" Width="100px" Settings-AutoFilterCondition="Contains"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>--%>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sqlGzdd" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" 
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>">
    </asp:SqlDataSource>

</asp:Content>
