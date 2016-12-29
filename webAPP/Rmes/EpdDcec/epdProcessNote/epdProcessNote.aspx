<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epdProcessNote.aspx.cs" Inherits="Rmes.WebApp.Rmes.EpdDCEC.epdProcessNote.epdProcessNote" StylesheetTheme="Theme1" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>
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
        <td>
            <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="新增" Width="120px">
                            <ClientSideEvents Click="function(s,e){window.open('../epdProcessNote/epdProcessNoteNew.aspx?opFlag=add&rmesId=');}" />
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="BtnUpdate" runat="server" Text="全部更新" Width="120px" OnClick="BtnUpdate_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="单条组件更新" Width="120px" OnClick="BtnUpdate1_Click">
                        </dx:ASPxButton>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <%--            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="新增" Width="120px">
                <ClientSideEvents Click="function(s,e){window.open('../epdProcessNote/epdProcessNoteNew.aspx?opFlag=add&rmesId=');}" />
            </dx:ASPxButton>--%>
        </td>
    </tr>
    <tr>
        <td>
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID" 
    OnRowDeleting="ASPxGridView1_RowDeleting" Width="100%" 
                oncustombuttoncallback="ASPxGridView1_CustomButtonCallback">
    <SettingsEditing PopupEditFormWidth="800px" />
    <Columns>
        <dx:GridViewCommandColumn  VisibleIndex="0" ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
            Width="60px">

            <SelectButton Text="选择">
            </SelectButton>
            <HeaderStyle HorizontalAlign="Center" />
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="40px">
            <%--<DeleteButton Visible="True"></DeleteButton>--%>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="Edit" Text="修改"></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <ClearFilterButton Visible="True"></ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="40px">
            <DeleteButton Visible="True"></DeleteButton>
<%--            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="GridViewCommandColumnCustomButton1" Text="修改"></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <ClearFilterButton Visible="True"></ClearFilterButton>--%>
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
    <ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            rmesId = grid.cpRmesId;
            if(callbackName == 'Edit') 
            {
                window.open('../epdProcessNote/epdProcessNoteNew.aspx?opFlag=edit&rmesId='+rmesId);
            }
        }" />
</dx:ASPxGridView>
        </td>
    </tr>
</table>
    </div>
    </form>
</body>
</html>
