<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="qms2300.aspx.cs" Inherits="Rmes.WebApp.Rmes.Qms.qms2300.qms2300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="新  增" AutoPostBack="false" Width="100px">
        <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
    </dx:ASPxButton>
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
                Width="100px" Visible="false" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE1" VisibleIndex="1"
                Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="站点代码" FieldName="STATION_CODE1" VisibleIndex="3"
                Width="100px" Settings-AutoFilterCondition="Contains" Visible="false" />
            <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="5"
                Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="工艺路线" FieldName="PRODUCT_SERIES" VisibleIndex="7"
                Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="检测数据代码" FieldName="DETECT_CODE" VisibleIndex="10"
                Width="120px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="检测数据名称" FieldName="DETECT_NAME" VisibleIndex="11"
                Width="180px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="检测数据类型" FieldName="DETECT_TYPE" VisibleIndex="12"
                Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="标准值" FieldName="DETECT_STANDARD" VisibleIndex="13"
                Width="80px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="上限值" FieldName="DETECT_MAX" VisibleIndex="14"
                Width="80px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="下限值" FieldName="DETECT_MIN" VisibleIndex="15"
                Width="80px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="单位" FieldName="DETECT_UNIT" VisibleIndex="16"
                Width="60px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="顺序" FieldName="DETECT_SEQ" VisibleIndex="17"
                Width="60px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <script type="text/javascript">

        function OpenAddWindow() {
            window.open('qms2301.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=1000,height=800,top=150,left=250');
        }

    </script>
</asp:Content>
