<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_mms1400" StylesheetTheme="Theme1"
    MasterPageFile="~/MasterPage.master" CodeBehind="mms1400.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td >
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="新建替换规则" AutoPostBack="false" Width="150px">
                    <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
                </dx:ASPxButton>
            </td>
            <td>  &nbsp;  &nbsp; | &nbsp; &nbsp; </td>
            <td>
                <dx:ASPxButton ID="ASPxButton2" runat="server" Text="物料替换" AutoPostBack="false" Width="150px">
                    <ClientSideEvents Click=" function(s,e){ OpenChangeWindow();}" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        KeyFieldName="RMES_ID" OnRowDeleting="ASPxGridView1_RowDeleting">
        <Columns>
            <dx:GridViewCommandColumn Caption=" " VisibleIndex="0" Width="80px">
                <DeleteButton Visible="True">
                </DeleteButton>
                <ClearFilterButton Visible="true">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="PROJECT_CODE" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="USE_COUNT" Visible="false" />
            <dx:GridViewDataTextColumn Caption="原物料代码" FieldName="ITEM_CODE_FROM" VisibleIndex="5"
                Width="120px" />
            <dx:GridViewDataTextColumn Caption="原物料名称" FieldName="ITEM_NAME_FROM" VisibleIndex="7"
                Width="220px" />
            <dx:GridViewDataTextColumn Caption="替换物料代码" FieldName="ITEM_CODE_TO" VisibleIndex="9"
                Width="120px" />
            <dx:GridViewDataTextColumn Caption="替换物料名称" FieldName="ITEM_NAME_TO" VisibleIndex="11"
                Width="220px" />
            <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREAT_TIME" VisibleIndex="13"
                Width="150px" />
            <dx:GridViewDataTextColumn Caption="创建人" FieldName="USER_NAME" VisibleIndex="15"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="是否启用" FieldName="ENABLE_FLAG" VisibleIndex="17"
                Visible="false" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>

    </dx:ASPxGridView>
    <script type="text/javascript">

        function OpenChangeWindow() {
            window.open('mms1404.aspx', 'changeWindow', 'resizable=yes,scrollbars=yes,width=500,height=350,top=150,left=250');
        }

        function OpenAddWindow() {
            window.open('mms1402.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=500,height=350,top=150,left=250');
        }

    </script>
</asp:Content>
