<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Exp1500.aspx.cs" Inherits="Rmes.WebApp.Rmes.Exp.Exp1500" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v11.1.Web, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>
    <tr>

    <td style="width: 60px;">
            <asp:Label ID="Label2" runat="server" Text="起始时间"></asp:Label>
    </td>
    <td>
        <dx:ASPxDateEdit ID="cdate1" runat="server" Width="160px" >
            
        </dx:ASPxDateEdit>
    </td>

    <td style="width: 60px;">
            <asp:Label ID="Label3" runat="server" Text="终止时间"></asp:Label>
    </td>
    <td>
        <dx:ASPxDateEdit ID="cdate2" runat="server" Width="160px">
            
        </dx:ASPxDateEdit>

    </td>

   

    <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查   询" UseSubmitBehavior="False"
                    OnClick="ASPxButton1_Click"/>
    </td>
    
    <td>
        <%--<dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />--%>
    </td>
    </tr>
</table>

<%--<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>

    <Columns>
        <dx:GridViewDataTextColumn Caption="" FieldName="RMES_ID" VisibleIndex="0" Width="150px" Visible="false"/>
        <dx:GridViewDataComboBoxColumn Caption="订单号" FieldName="AUFNR" VisibleIndex="1" 
            Width="130px">
<PropertiesComboBox ValueType="System.String"></PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="WORKSHOP" VisibleIndex="8" 
            Width="80px">
                   
<PropertiesComboBox ValueType="System.String"></PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
                   
        <dx:GridViewDataTextColumn Caption="工序步骤" FieldName="VORNR" VisibleIndex="8" 
            Width="80px"/>
        <dx:GridViewDataTextColumn Caption="物料编码" FieldName="SUBMAT" VisibleIndex="2" 
            Width="130px"/>
        <dx:GridViewDataTextColumn Caption="物料" FieldName="ITEM_CODE" VisibleIndex="3" 
            Width="180px"/>

        <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" VisibleIndex="3" 
            Width="180px"/>

        <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="4" 
            Width="90px" CellStyle-HorizontalAlign="Center">
            <CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="目的线边库" FieldName="T_LINESIDESTORE" VisibleIndex="6" 
            Width="80px"/>
        <dx:GridViewDataComboBoxColumn Caption="线边库名称" FieldName="STORE_NAME" VisibleIndex="7" 
            Width="180px"/>
        <dx:GridViewDataTextColumn Caption="源线边库" FieldName="S_LINESIDESTORE" VisibleIndex="5" 
            Width="100px"/>
        <dx:GridViewDataTextColumn Caption="消耗批次" FieldName="CHARG" VisibleIndex="9" 
            Width="100px" CellStyle-HorizontalAlign="Center">
<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" VisibleIndex="10" 
            Width="120px"/>
        <dx:GridViewDataTextColumn Caption="批号（任务号）" FieldName="BATCH" VisibleIndex="2" 
            Width="150px" CellStyle-HorizontalAlign="Center">  
<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="读取状态" FieldName="PRIND" 
            VisibleIndex="11" Width="100px" CellStyle-HorizontalAlign="Center">
        
<PropertiesComboBox ValueType="System.String"></PropertiesComboBox>

<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataComboBoxColumn>
        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

</dx:ASPxGridView>--%>


    <br />
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>


    <br />
    <dx:ReportToolbar ID="ReportToolbar1" runat="server" 
        ReportViewerID="ReportViewer1" ShowDefaultButtons="False">
        <Items>
            <dx:ReportToolbarButton ItemKind="Search" />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton ItemKind="PrintReport" />
            <dx:ReportToolbarButton ItemKind="PrintPage" />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
            <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
            <dx:ReportToolbarLabel ItemKind="PageLabel" />
            <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
            </dx:ReportToolbarComboBox>
            <dx:ReportToolbarLabel ItemKind="OfLabel" />
            <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
            <dx:ReportToolbarButton ItemKind="NextPage" />
            <dx:ReportToolbarButton ItemKind="LastPage" />
            <dx:ReportToolbarSeparator />
            <dx:ReportToolbarButton ItemKind="SaveToDisk" />
            <dx:ReportToolbarButton ItemKind="SaveToWindow" />
            <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                <Elements>
                    <dx:ListElement Value="pdf" />
                    <dx:ListElement Value="xls" />
                    <dx:ListElement Value="xlsx" />
                    <dx:ListElement Value="rtf" />
                    <dx:ListElement Value="mht" />
                    <dx:ListElement Value="html" />
                    <dx:ListElement Value="txt" />
                    <dx:ListElement Value="csv" />
                    <dx:ListElement Value="png" />
                </Elements>
            </dx:ReportToolbarComboBox>
        </Items>
        <Styles>
            <LabelStyle>
            <Margins MarginLeft="3px" MarginRight="3px" />
            </LabelStyle>
        </Styles>
    </dx:ReportToolbar>
    <dx:ReportViewer ID="ReportViewer1" runat="server" 
        
        ReportName="Rmes.WebApp.Rmes.Exp.Exp1500.XtraReport4">
    </dx:ReportViewer>
    <br />


</asp:Content>