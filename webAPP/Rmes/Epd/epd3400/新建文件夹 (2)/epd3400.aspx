<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_epd3400" StylesheetTheme="Theme1"
    MasterPageFile="~/MasterPage.master" CodeBehind="epd3400.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table>
    <tr>
        <td><%--if (myWindow == null) {OpenAddWindow();}else if (!myWindow.closed) {alert('窗口已打开！');}--%>
            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="新  增" AutoPostBack="false" Width="100px">
                <ClientSideEvents Click=" function(s,e){isOpen(s,e);OpenAddWindow(s,e);  }" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                OnClick="btnXlsExport_Click" />
        </td>
    </tr>
</table>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnRowDeleting="ASPxGridView1_RowDeleting" OnProcessColumnAutoFilter="ASPxGridView1_ProcessColumnAutoFilter">
    <SettingsBehavior ColumnResizeMode="Control" />
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="40px" Caption=" ">
            <DeleteButton Visible="false">
            </DeleteButton>
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false" />
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1"
            Width="200px" Settings-AutoFilterCondition="Contains" Visible="false" />
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINECODE1" VisibleIndex="1"
            Width="200px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="站点代码" FieldName="STATION_CODE1" VisibleIndex="3" Visible="false"
            Width="120px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="5"
            Width="220px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE1" VisibleIndex="7"
            Width="120px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="工位名称" FieldName="LOCATION_NAME" VisibleIndex="9"
            Width="220px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="工位属性" FieldName="LOCATION_FLAG_NAME" VisibleIndex="10"
            Width="120px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="查看工位" FieldName="LOCATION_FLAG1_NAME" VisibleIndex="11"
            Width="120px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
        </dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>
<dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID">
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" Caption=" ">
            <ClearFilterButton Visible="true">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false" />
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1"
            Width="80px" Settings-AutoFilterCondition="Contains" Visible="false" />
        <dx:GridViewDataTextColumn Caption="站点代码" FieldName="STATION" VisibleIndex="3" Width="120px"
            Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION" VisibleIndex="7" Width="120px"
            Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="工位属性" FieldName="LOCATION_FLAG" VisibleIndex="10"
            Width="120px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="操作员工" FieldName="USER_CODE" VisibleIndex="12"
            Width="120px" Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="操作内容" FieldName="FLAG" VisibleIndex="13" Width="120px"
            Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption="操作时间" FieldName="RQSJ" VisibleIndex="14" Width="150px"
            Settings-AutoFilterCondition="Contains" />
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
        </dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>
<script type="text/javascript">

    var myWindow;
    function OpenAddWindow(s,e) {


        myWindow = window.open('epd3401.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=800,height=800,top=150,left=250,alwaysRaised =yes');

    }
    function isOpen(s,e) {
        if (myWindow == null) {
//            OpenAddWindow();
//            return;
        }
        else if (!myWindow.closed) {
            //                window.close();
            alert("窗口已打开！");
        }
    }

</script>
<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>
</asp:Content> 