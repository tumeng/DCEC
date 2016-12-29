<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsConfimedMultiReplaceQuery.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsConfimedMultiReplaceQuery" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>已确认多对多替换查询</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM1" VisibleIndex="0" Width="150px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="2" Width="100px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="替换零件" FieldName="LJDM2" VisibleIndex="3" Width="150px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC1" VisibleIndex="2" Width="100px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="4" Width="60px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="员工" FieldName="YGMC" VisibleIndex="8" Width="60px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="日期" FieldName="LRSJ" VisibleIndex="9" ></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="分组" FieldName="THGROUP" VisibleIndex="19" ></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
    
    </div>
    </form>
</body>
</html>
