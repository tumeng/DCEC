<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgressBarTest.aspx.cs" Inherits="Rmes.WebApp.Rmes.Test.ProgressBarTest" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" /> 
</head>


<body>
    <form id="form1" method="post" runat="server">
    <div>
        <dx:ASPxProgressBar ID="p" runat="server" Width="100%" Height="40px"></dx:ASPxProgressBar>
    </div>
    </form>
</body>
</html>
