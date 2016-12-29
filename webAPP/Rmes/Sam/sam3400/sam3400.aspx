<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sam3400.aspx.cs" Inherits="Rmes.WebApp.Rmes.Sam.sam3400.sam3400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                        OnClick="btnXlsExport_Click" />
                </td>
            </tr>
        </table>
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="ROWNUM"
            Width="1200px">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="50px">
                    <ClearFilterButton Visible="True">
                    </ClearFilterButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="物料代码" VisibleIndex="1" FieldName="PT_PART" Width="100px" >
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料名称" VisibleIndex="2" FieldName="PT_DESC2" Width="200px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="英文名称" VisibleIndex="3" FieldName="PT_DESC1" Width="200px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料状态" VisibleIndex="4" FieldName="PT_STATUS" Width="80px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料属性" VisibleIndex="5" Visible="false" FieldName="PT_PHANTOM">
                </dx:GridViewDataTextColumn>
                <%--                <dx:GridViewDataTextColumn Caption="最小包装量" VisibleIndex="6"  Visible="false"
                    FieldName="MIN_PACK_QTY">
                </dx:GridViewDataTextColumn>--%>
                <dx:GridViewDataTextColumn Caption="物料组" VisibleIndex="7" Visible="false" FieldName="PT_GROUP">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                        </dx:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
