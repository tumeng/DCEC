<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="part2500.aspx.cs" Inherits="Rmes_part2500" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%--功能概述：库存待冲抵物料维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td style="height: 30px">
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="查询条件">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="生产线">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="comPLQ" ValueField="PLINE_CODE" TextField="SHOWTEXT" runat="server"
                    DropDownStyle="DropDownList" Width="70px" ClientInstanceName="Pcode">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="物料代码:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtMatCodeQ" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="供应商:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtGysCodeQ" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="冲抵时间:" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="StartDateQ" runat="server" Width="120px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="--" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="EndDateQ" runat="server" Width="120px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="单据号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtBillCodeQ" runat="server" Width="120px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExportPlan" runat="server" Text="导出" UseSubmitBehavior="False"
                    OnClick="btnXlsExportPlan_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="GZDD;GYS_CODE;MATERIAL_CODE" 
        Settings-ShowFilterRow="false" >
        <SettingsEditing PopupEditFormWidth="570px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Settings-AutoFilterCondition="Contains"
                Width="120px" Visible="false" />
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" VisibleIndex="1"
                Settings-AutoFilterCondition="Contains" Width="120px" />
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption="待冲抵库存" FieldName="MATERIAL_NUM" VisibleIndex="3"
                Settings-AutoFilterCondition="Contains" Width="100px" />
            <dx:GridViewDataDateColumn Caption="上次冲抵时间" FieldName="LAST_HANDLE_TIME" VisibleIndex="6"
                Settings-AutoFilterCondition="Contains" Width="160px" PropertiesDateEdit-EditFormat="DateTime"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="单据号" FieldName="BILL_CODE" VisibleIndex="8" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
           
            if(callbackName == 'refresh') 
            {
                grid2.PerformCallback();
            }
            
        }" />
    </dx:ASPxGridView>
    <table>
        <tr>
            <td style="height: 30px">
                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="冲抵日志">
                </dx:ASPxLabel>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="false"
        Settings-ShowFilterRow="false" KeyFieldName="GZDD;GYS_CODE;MATERIAL_CODE" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" VisibleIndex="1"
                Settings-AutoFilterCondition="Contains" Width="120px" />
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption="当次冲抵数量" FieldName="CUR_HANDLE_NUM" VisibleIndex="3"
                Settings-AutoFilterCondition="Contains" Width="160px" />
            <dx:GridViewDataDateColumn Caption="当次冲抵时间" FieldName="CUR_HANDLE_TIME" VisibleIndex="6"
                Settings-AutoFilterCondition="Contains" Width="160px" PropertiesDateEdit-EditFormat="DateTime"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="单据号" FieldName="BILL_CODE" VisibleIndex="8" Settings-AutoFilterCondition="Contains"
                Width="130px" />
            <dx:GridViewDataTextColumn Caption="操作说明" FieldName="CZSM" VisibleIndex="10" Settings-AutoFilterCondition="Contains"
                Width="80px" Visible="false" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="SqlGYScode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPL" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
