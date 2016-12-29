<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsMaterialRequisitionDetail.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsMaterialRequisition.mmsMaterialRequisitionDetail" StylesheetTheme="Theme1" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
    <%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx2" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <tr>
             <td>
                <dx:ASPxButton ID="BtnExport" runat="server" Text="导出到Excel" 
                    AutoPostBack="false" Width="100px" onclick="BtnExport_Click"/>
            </td>
      </tr>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" KeyFieldName="LJDM" AutoGenerateColumns="False" 
     OnHtmlRowCreated="ASPxGridView1_HtmlRowCreated"
     SettingsPager-Mode="ShowAllRecords">
     <%-- <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" ExportMode="All" />--%>
       <TotalSummary>
            <dx:ASPxSummaryItem FieldName="LJDM" SummaryType="Count" DisplayFormat="总数={0}"/>
       </TotalSummary>
        <Settings ShowFooter="True" ShowFilterRow="false"  />
          <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AllowGroup="False" 
                               AllowSort="False" />
                <Columns>
                    <dx:GridViewDataTextColumn Caption="保管员" FieldName="LJBGY"   VisibleIndex="3" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM" VisibleIndex="4" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC" VisibleIndex="5" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="工位" FieldName="LJGW" VisibleIndex="6" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="发料工位" FieldName="FLGW" VisibleIndex="7" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="库位" FieldName="LJDD" VisibleIndex="8" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="LJSL" VisibleIndex="16" Width="60px" ></dx:GridViewDataTextColumn>
                   <%-- <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="9" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="10" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="LJSL" VisibleIndex="16" Width="60px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="替换零件代码" FieldName="THLJDM" VisibleIndex="17" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="原零件代码" FieldName="OLDLJDM" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="站点发料" FieldName="FLGW1" VisibleIndex="19"  Width="80px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="备注" FieldName="BZ" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="供应商" FieldName="LJGYS" VisibleIndex="19"  Width="100px"></dx:GridViewDataTextColumn>--%>
                </Columns> 
                <Settings ShowHorizontalScrollBar="true" />
                <Templates>
                <DetailRow>
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" Width="100%" 
                    ActiveTabIndex="0">
                    <TabPages>
                        <dx:TabPage Text="对应计划明细">
                            <ContentCollection>
                                <dx:ContentControl runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                                    Settings-ShowGroupPanel="false" SettingsPager-Mode="ShowAllRecords"
                                     Settings-ShowHeaderFilterButton="false" Settings-ShowVerticalScrollBar="false"
                                     OnBeforePerformDataSelect="ASPxGridView2_DataSelect">
                    <Columns>
                    <dx:GridViewDataTextColumn Caption="保管员" FieldName="LJBGY"   VisibleIndex="3" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM" VisibleIndex="4" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC" VisibleIndex="5" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="工位" FieldName="LJGW" VisibleIndex="6" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="发料工位" FieldName="FLGW" VisibleIndex="7" Width="100px" />
                    <dx:GridViewDataTextColumn Caption="库位" FieldName="LJDD" VisibleIndex="8" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="9" Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="10" Width="150px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="LJSL" VisibleIndex="16" Width="60px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="替换零件代码" FieldName="THLJDM" VisibleIndex="17" Width="100px" ></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="原零件代码" FieldName="OLDLJDM" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="站点发料" FieldName="FLGW1" VisibleIndex="19"  Width="80px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="备注" FieldName="BZ" VisibleIndex="18"  Width="100px"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="供应商" FieldName="LJGYS" VisibleIndex="19"  Width="100px"></dx:GridViewDataTextColumn>
                    </Columns>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
                 </DetailRow>
                 </Templates>
                 </dx:ASPxGridView>
                   <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
        GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    </form>
</body>
</html>
