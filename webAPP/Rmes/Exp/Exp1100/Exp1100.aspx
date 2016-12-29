<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Exp1100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Exp.Exp1100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td style="width: 40px;">
            <asp:Label ID="Label1" runat="server" Text="生产线"></asp:Label>
        </td>
        <td>
            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="160px">
            </dx:ASPxComboBox>
        </td>

        <td style="width: 40px;">
            <asp:Label ID="Label2" runat="server" Text="合同号"></asp:Label>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtProjectCode" runat="server" Width="160px"></dx:ASPxTextBox>
        </td>

        <td style="width: 40px;">
            <asp:Label ID="Label3" runat="server" Text="图号"></asp:Label>
        </td>
        <td>
            <dx:ASPxTextBox ID="txtPlanSO" runat="server" Width="160px"></dx:ASPxTextBox>
        </td>

        <td>
            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查询数据" OnClick="ASPxButton1_Click"/>
        </td>
        <td align="center">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td>
            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                OnClick="btnXlsExport_Click" />
        </td>
        <td align="center">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table> 

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>

    <Columns>

        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" >
             <ClearFilterButton Visible="True" Text="清除">
            </ClearFilterButton>
            
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="订单号" FieldName="ORDER_CODE" Width="160px"  VisibleIndex="1"/>
        <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" Width="160px" VisibleIndex="2"/>
        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="3" Width="150px"/>           
        <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="4" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" VisibleIndex="5" Width="150px"/>
        <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="6" Width="60px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工厂" FieldName="FACTORY" VisibleIndex="7" Width="60px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="来源库" FieldName="RESOURCE_STORE" VisibleIndex="8" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="现编库" FieldName="STORE_CODE" VisibleIndex="9" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工艺" FieldName="PROCESS_CODE" VisibleIndex="10" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_SEQUENCE" VisibleIndex="11" Width="80px" CellStyle-HorizontalAlign="Center"/>                        
        <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" VisibleIndex="12" Width="150px" CellStyle-HorizontalAlign="Center"/>
                    
        <dx:GridViewDataTextColumn Caption="零件重要级别" FieldName="ITEM_CLASS_CODE" VisibleIndex="13" Width="100px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="供应商" FieldName="VENDOR_CODE" VisibleIndex="14" Width="60px"/>
                    
        

        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>



</dx:ASPxGridView>
<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>

</asp:Content>
