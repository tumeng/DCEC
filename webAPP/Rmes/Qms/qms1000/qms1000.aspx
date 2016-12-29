<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Qms_Qms1000_qms1000" Codebehind="qms1000.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%; height: 100%; margin: 0; padding: 0;">
        <tr>
            <td style="width: 60%;">
                <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" SettingsBehavior-AllowFocusedNode="true"
                    OnVirtualModeCreateChildren="ASPxTreeList1_VirtualModeCreateChildren" OnVirtualModeNodeCreating="ASPxTreeList1_VirtualModeNodeCreating">
                    <Columns>
                        <dx:TreeListDataColumn FieldName="name" Caption="缺陷树" />
                        <dx:TreeListCommandColumn VisibleIndex="9" Caption="操作" Width="120px">
                            <NewButton Visible="True">
                            </NewButton>
                            <EditButton Visible="True">
                            </EditButton>
                            <DeleteButton Visible="True">
                            </DeleteButton>
                        </dx:TreeListCommandColumn>
                    </Columns>
                    <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
                </dx:ASPxTreeList>
            </td>
            <td style="width: 40%;">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

