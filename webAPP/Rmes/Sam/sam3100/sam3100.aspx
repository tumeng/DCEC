<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Theme1"  Inherits="Rmes_Sam_sam3100_sam3100" Title="" Codebehind="sam3100.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<asp:Content ID="Content1"  ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<table cellpadding="0" cellspacing="0" style="margin-bottom: 0px">
    <tr style="height: 20px; vertical-align:bottom">
        <td style="padding-left: 4px; width:100px"; align="left" >
            <dx:ASPxButton ID="btnPdfExport" runat="server" Text="导出至PDF" UseSubmitBehavior="False" Wrap="False"  OnClick="btnPdfExport_Click" />
        </td>
        <td style="padding-left: 4px;width:95%"  align="left">
            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出至XLS" UseSubmitBehavior="False" OnClick="btnXlsExport_Click" />
        </td>
    </tr>
</table>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" KeyFieldName="COMPANY_CODE;SESSION_CODE" runat="server" AutoGenerateColumns="False">
    <Columns>
        <dx:GridViewDataTextColumn Caption="COMPANY_CODE" FieldName="COMPANY_CODE" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="会话" FieldName="SESSION_CODE" VisibleIndex="0" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户ID" FieldName="USER_ID" VisibleIndex="1" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户代码" FieldName="USER_CODE" VisibleIndex="2" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户姓名" FieldName="USER_NAME" VisibleIndex="3" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户IP" FieldName="USER_IP" VisibleIndex="4" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="登录日期" FieldName="LOGIN_TIME" VisibleIndex="5" Width="120px">
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="登录时间" FieldName="LOGIN_TIME" VisibleIndex="6" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>                        
</dx:ASPxGridView>

<dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>

</asp:Content>

