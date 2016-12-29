<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="rept1800_pic.aspx.cs" Inherits="Rmes.WebApp.Rmes.Rept.rept1800.rept1800_pic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div align = "center">
<asp:PlaceHolder ID = "GraphPlaceHolder" runat = "server">  <%--//这个控件是用来放图片的  ImageUrl="~/Rmes/Pub/Images/images/2.jpg"--%>
</asp:PlaceHolder>
<asp:Panel ID = "panel" runat = "server"></asp:Panel>
<asp:ListView ID = "list" runat = "server"></asp:ListView>
 <asp:Image ID="Image1" runat="server"  ClientInstanceName="C_image" />
                      
</div>

</asp:Content>
