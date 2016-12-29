<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Rmes.WebApp.Rmes.EpdDcec.test.test" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:100%;">
        <tr>
            <td style="width:35%">
                <dx:ASPxListBox ID="ListNode" ClientInstanceName="ListNode" runat="server" 
                    Height="450px" Width="100%" oninit="ListNode_Init">
                    <Columns>
                        <dx:ListBoxColumn FieldName="event_id" Name="事件ID" Width="30%" />
                        <dx:ListBoxColumn FieldName="event_name" Name="事件描述" Width="70%" />
                    </Columns>
                </dx:ASPxListBox>
            </td>
            <td style="width:55%">
                <dx:ASPxCallbackPanel ID="PanelNode" ClientInstanceName="PanelNode" 
                    runat="server" Width="100%" Height="450px" oncallback="PanelNode_Callback">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </td>
            <td></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
