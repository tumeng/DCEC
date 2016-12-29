<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="inv9600.aspx.cs" Inherits="Rmes_inv9600" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：临时措施查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td style="width: 45px">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="计划号:" Width="45px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtJHDM" runat="server" ValueField="JHDM" ValueType="System.String"
                    Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td style="width: 45px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="单号:" Width="45px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtDH" runat="server" ValueField="THGROUP" ValueType="System.String"
                    Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        grid2.PerformCallback();
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
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" 
        Width="100%" KeyFieldName="JHDM;THGROUP" 
        OnCustomCallback="ASPxGridView1_CustomCallback" oninit="ASPxGridView1_Init" 
        onpageindexchanged="ASPxGridView1_PageIndexChanged" SettingsText-GroupPanel="BOM更改记录">
        <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" Width="80px">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="计划号" FieldName="JHDM" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="单号" FieldName="THGROUP" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="原零件" FieldName="LJDM1" VisibleIndex="3" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="新零件" FieldName="LJDM2" VisibleIndex="4" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="制单人" FieldName="YGMC" VisibleIndex="5" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            
            <dx:GridViewDataTextColumn Caption="录入时间" FieldName="RQSJ" VisibleIndex="6" Width="150px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="原工位" FieldName="GWMC" VisibleIndex="7" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="新工位" FieldName="GWMC1" VisibleIndex="8" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="新工序" FieldName="GXMC1" VisibleIndex="9" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="10" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="操作" FieldName="OPFLAG" VisibleIndex="11" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn VisibleIndex="19" Width="80%" />
        </Columns>
    </dx:ASPxGridView>
  <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="JHDM;PART;GXDM" 
        OnCustomCallback="ASPxGridView2_CustomCallback" oninit="ASPxGridView2_Init" 
        onpageindexchanged="ASPxGridView2_PageIndexChanged" SettingsText-GroupPanel="装机提示更改记录">
        <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
      <dx:GridViewCommandColumn Caption="操作" Width="80px">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="计划号" FieldName="JHDM" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="JHSO" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="组件" FieldName="PART" VisibleIndex="3" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="装机提示" FieldName="CZTS" VisibleIndex="4" Width="300px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工序代码" FieldName="GXDM" VisibleIndex="5" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="装机图片" FieldName="WJPATH" VisibleIndex="7" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="字体颜色" FieldName="NOTE_COLOR" VisibleIndex="8" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="字体大小" FieldName="NOTE_FONT" VisibleIndex="9" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="9" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="修改人" FieldName="EDIT_USER" VisibleIndex="10" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="修改时间" FieldName="EDIT_DATE" VisibleIndex="11" Width="150px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn VisibleIndex="19" Width="80%" />
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
