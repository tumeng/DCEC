<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epdQualityQA.aspx.cs" Inherits="Rmes.WebApp.Rmes.EpdDCEC.epdQualityQA.epdQualityQA" StylesheetTheme="Theme1" %>
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
            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="新增" Width="120px">
                <ClientSideEvents Click="function(s,e){window.open('../epdQualityQA/epdQualityQANew.aspx?opFlag=add&rmesId=');}" />
            </dx:ASPxButton>
        </td>
    </tr>
    <tr>
        <td>
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID" 
    OnRowDeleting="ASPxGridView1_RowDeleting" Width="100%" 
                oncustombuttoncallback="ASPxGridView1_CustomButtonCallback">
    <SettingsEditing PopupEditFormWidth="800px" />               
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="40px">
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="Edit" Text="修改"></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <%--<DeleteButton Visible="True"></DeleteButton>--%>
            <ClearFilterButton Visible="True"></ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="40px">
            <%--<CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="GridViewCommandColumnCustomButton1" Text="修改"></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>--%>
            <DeleteButton Visible="True"></DeleteButton>
            <%--<ClearFilterButton Visible="True"></ClearFilterButton>--%>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="质量问题类型" FieldName="NOTETYPE"   VisibleIndex="1" Width="150px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="2" Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工艺路线" FieldName="ROUNTING_REMARK" VisibleIndex="3" Width="100px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" VisibleIndex="4" Width="100px" />
        <dx:GridViewDataTextColumn Caption="组件" FieldName="COMPONENT_CODE" VisibleIndex="5" Width="100px" />
        <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="6" Width="100px" ></dx:GridViewDataTextColumn>
        <%--<dx:GridViewDataTextColumn Caption="工序名称" FieldName="PROCESS_NAME" VisibleIndex="7" Width="150px" ></dx:GridViewDataTextColumn>--%>
        <dx:GridViewDataTextColumn Caption="有效期起始于" FieldName="FROM_DATE" VisibleIndex="16" Width="100px" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="结束于" FieldName="TO_DATE" VisibleIndex="17" Width="100px" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="质量问题" FieldName="QA_QUESTION" VisibleIndex="18"  Width="500px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="质量问题答案" FieldName="QA_ANSWER" VisibleIndex="19"  Width="500px"></dx:GridViewDataTextColumn>
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
                window.open('../epdQualityQA/epdQualityQANew.aspx?opFlag=edit&rmesId='+rmesId);
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
