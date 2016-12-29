<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_mms1301" Codebehind="mms1301.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
<dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"  OnClick="btnXlsExport_Click" />


<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="VENDOR_CODE" 
    OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback">
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="80px" Caption=" " >
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="btnDealItem" Text="导入维护">
                </dx:GridViewCommandColumnCustomButton>

            </CustomButtons>
            <ClearFilterButton Visible="True"></ClearFilterButton>
        </dx:GridViewCommandColumn>  
        <dx:GridViewDataTextColumn FieldName="VENDOR_CODE" Caption="客户代码" VisibleIndex="1" Width="80px" />
        <dx:GridViewDataTextColumn FieldName="VENDOR_NAME" Caption="客户名称" VisibleIndex="2" Width="200px">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="VENDOR_TYPE_CODE" Caption="客户类型" VisibleIndex="4"  Visible="true"/>
        <dx:GridViewDataTextColumn FieldName="VENDOR_TYPE_NAME" Caption="客户类型" VisibleIndex="5" Width="80px" />
        <dx:GridViewDataTextColumn FieldName="SAP_TIME" Caption="SAP导入时间" VisibleIndex="12" Width="100"/>

        <dx:GridViewDataTextColumn FieldName="USERNAME" Caption="SAP操作用户" VisibleIndex="14" Width="80"/>
        <dx:GridViewDataTextColumn FieldName="STATUS" Caption="处理状态" VisibleIndex="15" Width="80" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="STATUS_NAME" Caption="处理状态" VisibleIndex="16" Width="80"/>    
        <dx:GridViewDataTextColumn VisibleIndex="99" Width="80%" ></dx:GridViewDataTextColumn>
    </Columns>
        
    <ClientSideEvents EndCallback="function(s, e) {
            callbackName = grid.cpCallbackName;
            callbackValue = grid.cpCallbackValue;
                
            if(callbackName=='dealitem')
            {
                    
                var theUrl='inv1B00.aspx?itemcode='+callbackValue;
                location=theUrl;

            }
                
                else if(callbackName == 'alert') 
            {
                alert(grid.cpCallbackValue);
            }
        }" BeginCallback="function(s, e) {
	        grid.cpCallbackName = '';
        }" />
</dx:ASPxGridView>

</asp:Content>

