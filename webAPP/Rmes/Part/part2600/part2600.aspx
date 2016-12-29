<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeBehind="part2600.aspx.cs" Inherits="Rmes_part2600" %>

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
<%--功能概述：现场待处理物料接收--%>
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
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="维护时间:" />
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
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="状态:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="comboStatusQ" ValueField="INTERNAL_CODE" TextField="INTERNAL_NAME" runat="server"
                    DropDownStyle="DropDownList" Width="70px" ClientInstanceName="comboStatusQC">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click">
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="GZDD;ADD_TIME" 
        Settings-ShowFilterRow="false" 
        oncustombuttoncallback="ASPxGridView1_CustomButtonCallback" 
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" 
        oncustombuttoninitialize="ASPxGridView1_CustomButtonInitialize" >
        <SettingsEditing PopupEditFormWidth="570px" PopupEditFormHorizontalAlign="WindowCenter"
            PopupEditFormVerticalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" Caption="操作">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="SURE1" Text="库房已处理">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="SURE2" Text="APU已接收">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="SURE3" Text="APU已取消">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="handleSure" Text="处理">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="40px" Caption="操作">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="printSure" Text="打印">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            
        </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Settings-AutoFilterCondition="Contains"
                Width="120px" Visible="false" />
            <dx:GridViewDataTextColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="2"
                Settings-AutoFilterCondition="Contains" Width="100px" />
            <dx:GridViewDataTextColumn Caption="零件号" FieldName="MATERIAL_CODE" VisibleIndex="3"
                Settings-AutoFilterCondition="Contains" Width="100px" />
            <dx:GridViewDataTextColumn Caption="供应商" FieldName="GYS_CODE" VisibleIndex="4" Settings-AutoFilterCondition="Contains"
                Width="100px" />
            <dx:GridViewDataTextColumn Caption="数量" FieldName="MATERIAL_NUM" VisibleIndex="5"
                Settings-AutoFilterCondition="Contains" Width="40px" />
            <dx:GridViewDataDateColumn Caption="要料时间" FieldName="ADD_TIME" VisibleIndex="6"
                Settings-AutoFilterCondition="Contains" Width="140px" PropertiesDateEdit-EditFormat="DateTime"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="保管员" FieldName="IN_QADC01" VisibleIndex="7"
                Settings-AutoFilterCondition="Contains" Width="80px" />
            <dx:GridViewDataTextColumn Caption="操作工" FieldName="YGMC" VisibleIndex="8"
                Settings-AutoFilterCondition="Contains" Width="80px" />
            <dx:GridViewDataTextColumn Caption="原因" FieldName="REJECT_REASON" VisibleIndex="9"
                Settings-AutoFilterCondition="Contains" Width="100px" />
            <dx:GridViewDataTextColumn Caption="状态" FieldName="HANDLE_FLAG" VisibleIndex="10"
                Settings-AutoFilterCondition="Contains" Width="80px" Visible="false" />
            <dx:GridViewDataTextColumn Caption="状态" FieldName="STATUS_NAME" VisibleIndex="11"
                Settings-AutoFilterCondition="Contains" Width="60px" />
            <dx:GridViewDataTextColumn Caption="库房" FieldName="HANDLE_USER" VisibleIndex="12"
                Settings-AutoFilterCondition="Contains" Width="80px" />
            <dx:GridViewDataDateColumn Caption="处理时间" FieldName="HANDLE_TIME" VisibleIndex="13"
                Settings-AutoFilterCondition="Contains" Width="140px" PropertiesDateEdit-EditFormat="DateTime"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="APU" FieldName="TAKE_USER" VisibleIndex="14"
                Settings-AutoFilterCondition="Contains" Width="80px" />
            <dx:GridViewDataDateColumn Caption="接收时间" FieldName="TAKE_TIME" VisibleIndex="15"
                Settings-AutoFilterCondition="Contains" Width="140px" PropertiesDateEdit-EditFormat="DateTime"
                PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                <PropertiesDateEdit DisplayFormatString="yyyy-MM-dd HH:mm:ss">
                </PropertiesDateEdit>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
           <%-- <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>--%>
        </Columns>
        <%--<ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
           
            if(callbackName == 'refresh') 
            {
                grid2.PerformCallback();
            }
            
        }" />--%>
    </dx:ASPxGridView>
    <%--<table>
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
    </dx:ASPxGridView>--%>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <asp:SqlDataSource ID="SqlGYScode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPL" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
