<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Inv9200" Title="" Codebehind="inv9200.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" Width="99%" ClientInstanceName="tree" SettingsBehavior-AllowSort="false"
         AutoGenerateColumns="False">
        <SettingsBehavior AllowFocusedNode="True" />
        <SettingsBehavior ColumnResizeMode="Control"/>
        <Columns>
            <dx:TreeListDataColumn FieldName="WORKUNIT_NAMECN" Caption="名称" VisibleIndex="0" Width="120px"/>
            <dx:TreeListCommandColumn VisibleIndex="1" Caption="操作" Width="80px">
                <NewButton Visible="True">
                </NewButton>
                <EditButton Visible="True">
                </EditButton>
                <%--<DeleteButton Visible="True">
                </DeleteButton>--%>
            </dx:TreeListCommandColumn>
            
            <%--<dx:TreeListDataColumn FieldName="TEAM_CODE" Visible="false" />--%>
            
            <dx:TreeListDataColumn FieldName="WORKUNIT_CODE" Caption="代号" VisibleIndex="2" Width="80px" />
            <dx:TreeListDataColumn FieldName="WORKUNIT_NAMEEN" Caption="英文名称" VisibleIndex="3" Width="120px" />
            <dx:TreeListDataColumn FieldName="WORKUNIT_AREA" Caption="物理位置描述" VisibleIndex="4" Width="120px" />
            <dx:TreeListDataColumn FieldName="LINESIDE_WHCODE" Caption="线边库区域码" VisibleIndex="5" Width="80px" />
            <dx:TreeListDataColumn FieldName="WORKUNIT_PARENTCODE" Caption="上级单元" VisibleIndex="6" Width="80px" CellStyle-HorizontalAlign="Center" />
            <dx:TreeListDataColumn FieldName="ISLEAF" Caption="最小单元标示" VisibleIndex="7" Width="80px" CellStyle-HorizontalAlign="Center" />
            <dx:TreeListDataColumn FieldName="ISWORKSTATION" Caption="工作站标示" VisibleIndex="8" Width="80px" />
            <dx:TreeListDataColumn FieldName="WORKSHOP_CODE" Caption="所属车间代码" VisibleIndex="9" Width="80px" />
            <dx:TreeListDataColumn FieldName="WORKSHOP_NAME" Caption="所属车间名称" VisibleIndex="10" Width="120px" />
        </Columns>
        
        <%--<Templates>
            <EditForm>
                <table width="400px">
                    <tr>
                        <td>
                            &nbsp; 
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="工艺序号"></asp:Label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtPID" runat="server" Width="100px" Text='<%# Bind("工艺序号") %>' />
                        </td>
                        <td>
                            &nbsp; 
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="分配班组"></asp:Label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="dropTeamCode" runat="server" Width="100px" Value='<%# Bind("TEAM_CODE") %>'>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                            &nbsp; 
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="right">
                            <dx:ASPxTreeListTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxTreeListTemplateReplacement>
                            <dx:ASPxTreeListTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxTreeListTemplateReplacement>
                        </td>
                        <td>
                            &nbsp; 
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>--%>

    </dx:ASPxTreeList>
</asp:Content>

