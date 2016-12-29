<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Exp1400.aspx.cs" Inherits="Rmes.WebApp.Rmes.Exp.Exp1400" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v11.1.Web, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function DataBind(s, e) {


        panel.PerformCallback();
        //grid.GetValuesOnCustomCallback(s.lastSuccessValue, GetDataCallback);
    }

    

</script>  
<table>
    <tr>
<%--    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="SAP订单号" Width="100px"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxComboBox runat="server" ID="orderCode">
            <Items>
                <dx:ListEditItem Text="全部" Value="All" Selected="true"/>
            </Items>
        </dx:ASPxComboBox>
    </td>--%>
    
            <td style="width: 60px;">
            <asp:Label ID="Label1" runat="server" Text="合同号"></asp:Label>
        </td>
        <td width="160px">
            <dx:ASPxTextBox ID="txtProjectCode" runat="server" Width="160px">
                <ClientSideEvents TextChanged="function(s,e){
                DataBind(s,e);
            }" />
            </dx:ASPxTextBox>
        </td>

    <td width="60px">
        <dx:ASPxLabel runat="server" ID="ASPxLabel3" Text="生产线" Width="60px"></dx:ASPxLabel>
    </td>
    <td width="160px">
        <dx:ASPxComboBox runat="server" ID="ComPline" Width="160px">
            <Items>
                
            </Items>
            <ClientSideEvents SelectedIndexChanged="function(s,e){
                DataBind(s,e);
            
            }" />
        </dx:ASPxComboBox>
    </td>



    <td style="width: 80px;">
            <asp:Label ID="Label2" runat="server" Text="起始时间"></asp:Label>
    </td>
    <td>
        <dx:ASPxDateEdit ID="cdate1" runat="server" Width="160px" >
            <ClientSideEvents DateChanged="function(s,e){
                DataBind(s,e);
            }" />
            
        </dx:ASPxDateEdit>
    </td>

    <td style="width: 80px;">
            <asp:Label ID="Label3" runat="server" Text="终止时间"></asp:Label>
    </td>
    <td>
        <dx:ASPxDateEdit ID="cdate2" runat="server" Width="160px">
            <ClientSideEvents DateChanged="function(s,e){
                DataBind(s,e);
            
            }" />
        </dx:ASPxDateEdit>

    </td>

    <td style="width: 80px">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="WBS元素">
                        </dx:ASPxLabel>
            </td>
            <td style="width: 180px">     
                <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" ClientInstanceName="panel" OnCallback="ASPxCallbackPanel1_Callback">
                <PanelCollection>
                <dx:PanelContent ID="PanelContent2" runat="server">

                <dx:ASPxGridLookup ID="gridLookupItem" runat="server" KeyFieldName="RMES_ID" MultiTextSeparator="," SelectionMode="Multiple"
                    Width="160px">
                    <GridViewProperties>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"></SettingsBehavior>

                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                    <Settings ShowVerticalScrollBar="True"></Settings>
                    </GridViewProperties>
                    <Columns>
                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" Width="40px" />
                        <dx:GridViewDataColumn Caption="WBS元素" FieldName="WBS_CODE" Width="160px"/>
                    </Columns>
                </dx:ASPxGridLookup>
                </dx:PanelContent>
                </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>

    <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查   询" UseSubmitBehavior="False"
                    OnClick="ASPxButton1_Click"/>
    </td>
    
    <td>
        <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
    </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="AUFNR">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" >
             <ClearFilterButton Visible="True" Text="清除">
            </ClearFilterButton>
            
        </dx:GridViewCommandColumn>

        <dx:GridViewDataTextColumn Caption="" FieldName="RMES_ID" VisibleIndex="0" Width="150px" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="订单号" FieldName="AUFNR" VisibleIndex="1" 
            Width="130px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="WERKS" VisibleIndex="8" 
            Width="80px">
                   
<PropertiesComboBox ValueType="System.String"></PropertiesComboBox>
        </dx:GridViewDataComboBoxColumn>
                   
        <dx:GridViewDataTextColumn Caption="工序步骤" FieldName="VORNR" VisibleIndex="8" 
            Width="80px"/>
        <dx:GridViewDataTextColumn Caption="物料编码" FieldName="SUBMAT" VisibleIndex="2" 
            Width="130px"/>
        <dx:GridViewDataTextColumn Caption="物料名称" FieldName="MATKT" VisibleIndex="3" 
            Width="180px"/>
        <dx:GridViewDataTextColumn Caption="数量" FieldName="MENGE" VisibleIndex="4" 
            Width="90px" CellStyle-HorizontalAlign="Center">
            <CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn Caption="线边库号" FieldName="TLGORT" VisibleIndex="6" 
            Width="80px"/>
        <%--<dx:GridViewDataComboBoxColumn Caption="线边库名称" FieldName="STORE_NAME" VisibleIndex="7" 
            Width="180px"/>--%>
        <dx:GridViewDataTextColumn Caption="来源库" FieldName="SLGORT" VisibleIndex="5" 
            Width="100px"/>
        <dx:GridViewDataTextColumn Caption="消耗批次" FieldName="CHARG" VisibleIndex="9" 
            Width="100px" CellStyle-HorizontalAlign="Center">
<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生成日期" FieldName="WKDT" VisibleIndex="10" 
            Width="120px"/>
        <%--<dx:GridViewDataTextColumn Caption="批号（任务号）" FieldName="BATCH" VisibleIndex="2" 
            Width="150px" CellStyle-HorizontalAlign="Center">  
<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataTextColumn>--%>
        <dx:GridViewDataComboBoxColumn Caption="读取状态" FieldName="PRIND" 
            VisibleIndex="11" Width="100px" CellStyle-HorizontalAlign="Center">
        
<PropertiesComboBox ValueType="System.String"></PropertiesComboBox>

<CellStyle HorizontalAlign="Center"></CellStyle>
        </dx:GridViewDataComboBoxColumn>
        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

</dx:ASPxGridView>


    <br />
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>


    <dx:ReportToolbar ID="ReportToolbar1" runat="server" 
        ReportViewerID="ReportViewer1" ShowDefaultButtons="False" Width="850px">
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
        
        ReportName="Rmes.WebApp.Rmes.Exp.Exp1400.XtraReport2">
    </dx:ReportViewer>
    


</asp:Content>