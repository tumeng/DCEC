<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsOneReplace.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDcec.mmsReplaceRelationshipUse.mmsOneReplace" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">  
    <table width="100%" border="1"><tr><td>
        <table style="width:100%;" cellspacing="0"  cellpadding="0" >
            <tr>
                <td style="width:5%;" align="left">
                    &nbsp;</td>
                <td style="width:20%;" align="left">
                    &nbsp;</td>
                <td style="width:15%" align="left">
                    &nbsp;</td>
                <td style="width:20%" align="right">
                    &nbsp;</td>
                <td style="width:15%">
                    &nbsp;</td>
                <td style="width:10%" align="right">
                    &nbsp;</td>
                <td style="width:10%">
                    &nbsp;</td>
                <td style="width:5%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width:5%;" align="left">
                    &nbsp;</td>
                <td style="width:20%;" align="left">
                    <asp:Label ID="Label8" runat="server" Text="计划："></asp:Label>
                </td>
                <td style="width:15%" align="left">
                    <asp:Label ID="LabPlanCode" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width:20%" align="right">
                    <asp:Label ID="Label9" runat="server" Text="SO："></asp:Label>
                </td>
                <td style="width:15%">
                    <asp:Label ID="LabSO" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width:10%" align="right">
                    &nbsp;</td>
                <td style="width:10%">
                    &nbsp;</td>
                <td style="width:5%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td  align="left">&nbsp;</td>
                <td  align="left">
                    &nbsp;</td>
                <td  align="left">
                    &nbsp;</td>
                <td  align="right">
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td align="right">
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td  align="left"></td>
                <td  align="left">
                    <asp:Label ID="Label1" runat="server" Text="原零件："></asp:Label>
                </td>
                <td  align="left">
                    <asp:Label ID="LabOldPart" runat="server" Text="Label"></asp:Label>
                </td>
                <td  align="right">
                    <asp:Label ID="Lab4" runat="server" Text="替换零件："></asp:Label>
                </td>
                <td >
                    <asp:Label ID="LabNewPart" runat="server" Text="Label"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="Label3" runat="server" Text="工位："></asp:Label>
                </td>
                <td >
                    <asp:Label ID="LabLocation" runat="server" Text="Label"></asp:Label>
                </td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td  align="left">&nbsp;</td>
                <td  align="left">
                    &nbsp;</td>
                <td  align="left">
                    &nbsp;</td>
                <td  align="right">
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td align="right">
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="当天计划里该零件总数："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabSumNum" runat="server" Text="Label"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="Label6" runat="server" Text="当天已替换该零件数量："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabNum" runat="server" Text="Label"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="Label7" runat="server" Text="比例："></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabRate" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
                <td colspan="6" align="center">
                    <dx:ASPxButton ID="BtnSave" runat="server" Text="保存" onclick="BtnSave_Click">
                    </dx:ASPxButton>
                </td>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="style1">
                    </td>
                <td colspan="6" align="center" class="style1">
                    <asp:Label ID="LabPlineCode" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td align="center" class="style1">
                    </td>
            </tr>
        </table>
        </td></tr></table>
    </form>
</body>
</html>
