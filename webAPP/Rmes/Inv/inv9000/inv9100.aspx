<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Inv9100" Title="" Codebehind="inv9100.aspx.cs" %>

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
            <dx:TreeListDataColumn FieldName="项目名称" Caption="项目名称" VisibleIndex="0" Width="100px"/>
            <dx:TreeListCommandColumn VisibleIndex="1" Caption="操作" Width="80px">
                <%--<NewButton Visible="True">
                </NewButton>--%>
                <EditButton Visible="True">
                </EditButton>
                <%--<DeleteButton Visible="True">
                </DeleteButton>--%>
            </dx:TreeListCommandColumn>
            
            <%--<dx:TreeListDataColumn FieldName="TEAM_CODE" Visible="false" />--%>
            
            <dx:TreeListDataColumn FieldName="项目代号" Caption="项目代号" VisibleIndex="2" Width="100px" />
            <dx:TreeListDataColumn FieldName="装入序号" Caption="装入序号" VisibleIndex="3" Width="30px" />
            <dx:TreeListDataColumn FieldName="工艺序号" Caption="工艺序号" VisibleIndex="4" Width="30px" CellStyle-HorizontalAlign="Center" />
            <dx:TreeListDataColumn FieldName="合同号" Caption="合同号" VisibleIndex="5" Width="60px" CellStyle-HorizontalAlign="Center" />
            <dx:TreeListDataColumn FieldName="工作号" Caption="工作号" VisibleIndex="6" Width="70px" />
            <dx:TreeListDataColumn FieldName="批次" Caption="批次" VisibleIndex="7" Width="30px" />
            <dx:TreeListDataColumn FieldName="合并数量" Caption="合并数量" VisibleIndex="8" Width="30px" />
            <dx:TreeListDataColumn FieldName="实际数量" Caption="实际数量" VisibleIndex="9" Width="30px" />
            <dx:TreeListDataColumn FieldName="组件代号" Caption="组件代号" VisibleIndex="10" Width="70px" />
            <dx:TreeListDataColumn FieldName="工艺路线" Caption="工艺路线" VisibleIndex="11" Width="300px" />
            <%--<dx:TreeListDataColumn FieldName="产品型号" Caption="产品型号" VisibleIndex="12" Width="70px" />--%>
            <dx:TreeListDataColumn FieldName="送入单位" Caption="送入单位" VisibleIndex="13" Width="60px" />
            <%--<dx:TreeListDataColumn FieldName="物料名称" Caption="物料名称" VisibleIndex="14" Width="70px" />
            <dx:TreeListDataColumn FieldName="毛坯尺寸" Caption="毛坯尺寸" VisibleIndex="15" Width="70px" />
            <dx:TreeListDataColumn FieldName="零件归属名称" Caption="零件归属名称" VisibleIndex="16" Width="70px" />
            <dx:TreeListDataColumn FieldName="零件版本" Caption="零件版本" VisibleIndex="17" Width="70px" />
            <dx:TreeListDataColumn FieldName="更改单号" Caption="更改单号" VisibleIndex="18" Width="70px" />
            <dx:TreeListDataColumn FieldName="派工单号" Caption="派工单号" VisibleIndex="19" Width="70px" />

            <dx:TreeListDataColumn Caption=" " VisibleIndex="99" Width="5%" />--%>

            <dx:TreeListComboBoxColumn FieldName="TEAM_CODE" Caption="对应班组" VisibleIndex="20" Width="50px">
                <PropertiesComboBox ValueType="System.String">
                </PropertiesComboBox>
            </dx:TreeListComboBoxColumn>

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

