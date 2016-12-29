

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="qms2100.aspx.cs" Inherits="Rmes_qms2100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
 <script type="text/javascript">
     function download() { 
     }
</script>

<dx:ASPxGridView ID="ASPxGridView1" runat="server"  AutoGenerateColumns="False" ClientInstanceName="grid" Width="100%" KeyFieldName="RMES_ID"
    Settings-ShowHeaderFilterButton="false">
    <Settings ShowFilterRow="True" />

    <Columns>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        
        <dx:GridViewDataTextColumn Caption="计划号" FieldName="PLAN_CODE" VisibleIndex="1" Width="140px"/>
        <dx:GridViewDataTextColumn Caption="SN" FieldName="BATCHNO" VisibleIndex="3" Width="160px"/>
          
        <dx:GridViewDataTextColumn Caption="工序号" FieldName="PROCESSCODE" VisibleIndex="5" Width="100px"/>

        <dx:GridViewDataTextColumn Caption="检测项代码" FieldName="ITEMCODE" VisibleIndex="7" Width="120px"/>
        <dx:GridViewDataTextColumn Caption="检测项名称" FieldName="ITEMNAME" VisibleIndex="9" Width="120px"/>
        <dx:GridViewDataTextColumn Caption="检测项描述" FieldName="ITEMDESCRIPTION" VisibleIndex="11" Width="150px"/>

        <dx:GridViewDataTextColumn Caption="最小值" FieldName="MINVALUE" VisibleIndex="6" Width="80px" CellStyle-HorizontalAlign="Center"/><%-- 列居中--%>
        <dx:GridViewDataTextColumn Caption="最大值" FieldName="MAXVALUE" VisibleIndex="7" Width="80px" CellStyle-HorizontalAlign="Center"/>
         <dx:GridViewDataTextColumn Caption="实际采样值" FieldName="CURRENTVALUE" VisibleIndex="7" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataHyperLinkColumn  Caption="下载" FieldName="URL" VisibleIndex="8" Width="120px" CellStyle-HorizontalAlign="Center" >
        <PropertiesHyperLinkEdit   TextFormatString="下载文件{0}" NavigateUrlFormatString="qms2101.aspx?fid={0}" Target="_blank">
                               
                        </PropertiesHyperLinkEdit>

        </dx:GridViewDataHyperLinkColumn>
        
        
        <dx:GridViewDataTextColumn Caption="实际采样是否合格" FieldName="CURRENTRESULT" VisibleIndex="9" Width="100px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="检测项单位" FieldName="UNITNAME" VisibleIndex="10" Width="80px"/>
       
        <dx:GridViewDataTextColumn Caption="检测项类型" FieldName="UNITTYPE" VisibleIndex="23" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="顺序" FieldName="ORDERING" VisibleIndex="25" Width="60px"/>

        
        <dx:GridViewDataTextColumn Caption="检验时间" FieldName="WORK_TIME" VisibleIndex="29" Width="150px"/>
        <dx:GridViewDataComboBoxColumn Caption="作业员" FieldName="USER_ID" VisibleIndex="31" Width="80px"/>
        <dx:GridViewDataTextColumn Caption="时间戳" FieldName="TIMESTAMP1" VisibleIndex="33" Width="150px"/>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
    </dx:ASPxGridView>

</asp:Content>
