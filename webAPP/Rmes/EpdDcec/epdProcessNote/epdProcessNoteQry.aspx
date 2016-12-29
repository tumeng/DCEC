<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epdProcessNoteQry.aspx.cs" Inherits="Rmes.WebApp.Rmes.EpdDCEC.epdProcessNote.epdProcessNoteQry" StylesheetTheme="Theme1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
   <%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>装机提示维护查询</title>
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
<table style="width:100%;">
    <tr>
        <td>
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID" Width="100%" >            
    <Columns>
    <dx:GridViewCommandColumn Caption="  " VisibleIndex="0" Width="60px" Visible="True">
            <EditButton Visible="false">
            </EditButton>
            <NewButton Visible="false">
            </NewButton>
            <DeleteButton Visible="false">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="装机提示类型" FieldName="NOTETYPE"   VisibleIndex="1" Width="150px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1" Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工艺路线" FieldName="ROUNTING_REMARK" VisibleIndex="3" Width="100px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" VisibleIndex="4" Width="100px" />
        <dx:GridViewDataTextColumn Caption="组件" FieldName="COMPONENT_CODE" VisibleIndex="5" Width="100px" />
        <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="6" Width="100px" ></dx:GridViewDataTextColumn>
        <%--<dx:GridViewDataTextColumn Caption="工序名称" FieldName="PROCESS_NAME" VisibleIndex="7" Width="150px" ></dx:GridViewDataTextColumn>--%>
        <dx:GridViewDataTextColumn Caption="有效期起始于" FieldName="FROM_DATE" VisibleIndex="16" Width="100px" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="结束于" FieldName="TO_DATE" VisibleIndex="17" Width="100px" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="装机提示内容" FieldName="PROCESS_NOTE" VisibleIndex="18"  Width="500px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="图片" FieldName="PROCESS_PIC" VisibleIndex="19"  Width="500px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"></dx:GridViewDataTextColumn>
        
    </Columns>     
    <Settings ShowHorizontalScrollBar="True" />   
</dx:ASPxGridView>
  <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                        </dx:ASPxGridViewExporter>  
        </td>
    </tr>
</table>
    </div>
    </form>
</body>
</html>
