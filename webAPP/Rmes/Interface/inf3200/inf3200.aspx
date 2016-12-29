<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inf3200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Interface.inf3200.inf3200" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>
    <tr>
    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="SAP订单号"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxTextBox runat="server" ID="txtOrderCode" Width="180px"></dx:ASPxTextBox>    
    </td>
    
    <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查   询" UseSubmitBehavior="False"
                    OnClick="ASPxButton1_Click"/>
    </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="AUFNR">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0">
            <ClearFilterButton Text="清除" Visible="true" ></ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="订单号" FieldName="AUFNR" VisibleIndex="1" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="WERKS" VisibleIndex="2" Width="160px"/>
         <dx:GridViewDataTextColumn Caption="产品编码" FieldName="MATNR" VisibleIndex="3" Width="150px"/>           
        <dx:GridViewDataTextColumn Caption="工序步骤" FieldName="VORNR" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="工序名称" FieldName="LTXA1" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="工序控制码" FieldName="STEUS" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="工作中心" FieldName="ARBPL" VisibleIndex="5" Width="180px"/>
        <dx:GridViewDataTextColumn Caption="生产数量" FieldName="MGVRG" VisibleIndex="5" Width="180px"/>
        <dx:GridViewDataTextColumn Caption="工序机器工时" FieldName="VGW02" VisibleIndex="6" Width="180px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工序人工工时" FieldName="VGW03" VisibleIndex="7" Width="250px"/>
        <dx:GridViewDataComboBoxColumn Caption="重启工序" FieldName="OPFLG" VisibleIndex="7" Width="250px"/>
        
        <dx:GridViewDataTextColumn Caption="批号" FieldName="BATCH" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="预备存时间戳" FieldName="SERIAL" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>  
        <dx:GridViewDataComboBoxColumn Caption="读取状态" FieldName="PRIND" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>
        
        <dx:GridViewDataDateColumn Caption="工序开始日期" FieldName="SSAVD" VisibleIndex="12" ></dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn Caption="工序开始时间" FieldName="SSAVZ" VisibleIndex="12" ></dx:GridViewDataDateColumn>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>

</asp:Content>