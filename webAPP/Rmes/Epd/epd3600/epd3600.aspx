<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="epd3600.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd3600.epd3600" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="连续上传" AutoPostBack="false" Width="100px">
                    <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
                </dx:ASPxButton>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
            </td>
        </tr>
    </table>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        KeyFieldName="RMES_ID" OnRowDeleting="ASPxGridView1_RowDeleting">
        <SettingsBehavior ColumnResizeMode="Control" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="60px">
                <DeleteButton Visible="True" Text="删除">
                </DeleteButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false" />
            <dx:GridViewDataTextColumn Caption="生产线名称" Name="PLINE_NAME" FieldName="PLINE_NAME"
                VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains" Visible="false" />
            <dx:GridViewDataTextColumn Caption="生产线名称" Name="PLINECODE1" FieldName="PLINECODE1"
                VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="工序代码" Name="PROCESS_CODE1" FieldName="PROCESS_CODE1"
                VisibleIndex="2" Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="工序名称" Name="PROCESS_NAME" FieldName="PROCESS_NAME"
                VisibleIndex="3" Width="200px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="文件名称" Name="FILE_NAME" FieldName="FILE_NAME"
                VisibleIndex="6" Width="200px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="产品系列" Name="PRODUCT_SERIES" FieldName="PRODUCT_SERIES"
                VisibleIndex="4" Width="80px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataHyperLinkColumn Caption="文件URL" Name="FILE_URL" FieldName="FILE_URL"
                VisibleIndex="8" Width="500px" PropertiesHyperLinkEdit-Target="_blank" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="文件类型" Name="FILE_TYPE" FieldName="FILE_TYPE"
                VisibleIndex="9" Visible="false" Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="文件类型" Name="FILE_TYPE_NAME" FieldName="FILE_TYPE_NAME"
                VisibleIndex="5" Width="100px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%" />
        </Columns>
    </dx:ASPxGridView>
    <script type="text/javascript">

        function onUploadImgChange(sender) {
            var path = sender.value;
            if (!sender.value.match(/.jpg|.JPG|.png|.PNG/i)) {
                alert('图片格式无效！');
                return false;
            }

            var start = path.lastIndexOf('\\') + 1;
            var len = path.lastIndexOf('.') - start;

            var showFile = path.substr(start, len);

            txtFileName.SetText(showFile);
        }

        function OpenAddWindow() {
            window.open('epd3601.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=800,height=800,top=150,left=250,alwaysRaised =yes');
        }

    </script>
</asp:Content>
