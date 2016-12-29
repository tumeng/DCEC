<%@ Control Language="C#" ClassName="CalendarWidget" AutoEventWireup="true" Inherits="Rmes_Pub_Ascx_WebUserControl" Codebehind="CalendarWidget.ascx.cs" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<script runat="server">
    protected void Page_Init(object sender, EventArgs e) {
        Calendar.SelectedDate = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
    }
</script>
<dx:ASPxCalendar runat="server" ID="Calendar" ShowClearButton="false" ShowHeader="false"
    ShowTodayButton="false" ShowWeekNumbers="false" HighlightToday="false" Width="100%">
    <Border BorderStyle="None" />
</dx:ASPxCalendar>


