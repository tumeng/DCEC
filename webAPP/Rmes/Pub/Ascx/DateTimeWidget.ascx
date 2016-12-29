<%@ Control Language="C#" AutoEventWireup="true" ClassName="DateTimeWidget" Inherits="Rmes_Pub_Ascx_WebUserControl" Codebehind="DateTimeWidget.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTimer" TagPrefix="dx" %>

<script runat="server">
    protected void Page_Init(object sender, EventArgs e) {
        DateLabel.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ToLongDateString();
    }
</script>
<script type="text/javascript" language="javascript">
// <![CDATA[
    function PrepareTimeValue(value) {
        if (value < 10)
            value = "0" + value;
        return value;
    }
    function UpdateTime(s, e) {
        var dateTime = new Date();
        var timeString = PrepareTimeValue(dateTime.getHours()) + ":" + PrepareTimeValue(dateTime.getMinutes()) + ":" +
            PrepareTimeValue(dateTime.getSeconds());
        timeLabel.SetText(timeString);
    }
// ]]> 
</script>
<center>
<dx:ASPxTimer runat="server" ID="Timer" ClientInstanceName="timer" Interval="1000">
    <ClientSideEvents Init="UpdateTime" Tick="UpdateTime" />
</dx:ASPxTimer>
<div style="height:30px"></div>
<div   >
    <dx:ASPxLabel runat="server" ID="TimeLabel" ClientInstanceName="timeLabel" Font-Bold="true"
        Font-Size="X-Large">
    </dx:ASPxLabel>
</div>
<div style="height:20px"></div>
<div >
    <dx:ASPxLabel runat="server" ID="DateLabel" ClientInstanceName="dateLabel" Font-Size="14px"></dx:ASPxLabel>
</div>
</center>