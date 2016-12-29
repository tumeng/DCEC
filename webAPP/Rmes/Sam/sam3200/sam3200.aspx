<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Theme1" Inherits="Rmes_Sam_sam3200_sam3200" Title="" Codebehind="sam3200.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="VISIT_TIME">
    <Columns>
        <dx:GridViewDataTextColumn Caption="COMPANY_CODE" FieldName="COMPANY_CODE" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户ID" FieldName="USER_ID" VisibleIndex="0" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户代码" FieldName="USER_CODE" VisibleIndex="1" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户姓名" FieldName="USER_NAME" VisibleIndex="2" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户IP" FieldName="USER_IP" VisibleIndex="3" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="程序名称" FieldName="PROGRAM_NAME" VisibleIndex="4" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataDateColumn Caption="访问日期" FieldName="VISIT_TIME" VisibleIndex="6" Width="120px">
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataTextColumn Caption="访问时间" FieldName="VISIT_TIME" VisibleIndex="5" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>

</asp:Content>
