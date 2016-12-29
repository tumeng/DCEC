<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsConfimedOneReplaceQuery.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsConfimedOneReplaceQuery" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>已确认一对一替换查询</title>
</head>
<body onunload="closeWin()">
    <form id="form1" runat="server">
    <script type="text/javascript">
        var nextWin = null;
        function closeWin() {
            if (nextWin != null) {
                nextWin.closeWin();
            }
            forClose();
        }
        function forClose() {
            window.opener = null;
            window.open("", "_self");
            window.close();
        }
    </script>
    <div>
    
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn Caption="原零件" FieldName="LJDM1" VisibleIndex="0" Width="150px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="替换零件" FieldName="LJDM2" VisibleIndex="2" Width="150px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="3" Width="100px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="数量" FieldName="ZJSL" VisibleIndex="4" Width="60px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="员工" FieldName="YGMC" VisibleIndex="8" Width="60px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="时间" FieldName="LRSJ" VisibleIndex="9" ></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
    
    </div>
    </form>
</body>
</html>
