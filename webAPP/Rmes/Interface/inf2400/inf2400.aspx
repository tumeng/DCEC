<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inf2400.aspx.cs" Inherits="Rmes.WebApp.Rmes.Interface.inf2400.inf2400" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table>
    <tr>
    <td>
        <dx:ASPxLabel runat="server" ID="ASPxLabel1" Text="成品号"></dx:ASPxLabel>
    </td>
    <td>
        <dx:ASPxTextBox runat="server" ID="txtPlanSO" Width="180px"></dx:ASPxTextBox>    
    </td>
    
    <td style="width: 60px;">
            <asp:Label ID="Label2" runat="server" Text="起始时间"></asp:Label>
    </td>
    <td>
        <dx:ASPxDateEdit ID="cdate1" runat="server" Width="160px" />
    </td>

    <td style="width: 60px;">
            <asp:Label ID="Label3" runat="server" Text="终止时间"></asp:Label>
    </td>
    <td>
        <dx:ASPxDateEdit ID="cdate2" runat="server" Width="160px" />

    </td>

    <td>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查   询" UseSubmitBehavior="False"
                    OnClick="ASPxButton1_Click"/>
    </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ID">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>

    <Columns>
    <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" >
             <ClearFilterButton Visible="True" Text="清除">
            </ClearFilterButton>
            
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="" FieldName="ID" VisibleIndex="0" Width="150px" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="订单号" FieldName="AUFNR" VisibleIndex="1" Width="160px"/>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="WERKS" VisibleIndex="2" Width="160px"/>
                   
        <dx:GridViewDataTextColumn Caption="工序步骤" FieldName="VORNR" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="实际加工工作中心" FieldName="ARBPL" VisibleIndex="5" Width="180px"/>
        <dx:GridViewDataTextColumn Caption="工序机器工时" FieldName="VGW02" VisibleIndex="6" Width="180px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工序人工工时" FieldName="VGW03" VisibleIndex="7" Width="250px"/>
        <dx:GridViewDataComboBoxColumn Caption="工序是否完成" FieldName="OPFLG" VisibleIndex="7" Width="250px"/>
        <dx:GridViewDataTextColumn Caption="工序产量" FieldName="LMNGA" VisibleIndex="7" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="报废产量" FieldName="XMNGA" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="员工信息" FieldName="MUSER" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="批号" FieldName="BATCH" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="预备存时间戳" FieldName="SERIAL" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>  
        <dx:GridViewDataComboBoxColumn Caption="读取状态" FieldName="PRIND" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>
        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>

</asp:Content>