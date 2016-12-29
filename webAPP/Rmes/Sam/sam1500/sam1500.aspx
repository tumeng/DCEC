<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Sam_sam1500_sam1500" Title="" Codebehind="sam1500.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxTreeList ID="ASPxTreeList1" runat="server" Width="100%" ClientInstanceName="tree" AutoGenerateColumns="False">
    <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />

    <Columns>
        <dx:TreeListDataColumn FieldName="MENU_NAME" Caption="菜单标题" VisibleIndex="0" Width="150px"/>
        <dx:TreeListCommandColumn VisibleIndex="1" Caption="操作" Width="120px">
            <NewButton Visible="True">
            </NewButton>
            <EditButton Visible="True">
            </EditButton>
            <DeleteButton Visible="True">
            </DeleteButton>
        </dx:TreeListCommandColumn>
        <dx:TreeListDataColumn FieldName="MENU_CODE" Caption="菜单代码" VisibleIndex="2" Width="150px" />
        <dx:TreeListDataColumn FieldName="MENU_NAME_EN" Caption="英文名称" VisibleIndex="3" Width="150px" />
        <dx:TreeListDataColumn FieldName="MENU_LEVEL" Caption="层级" VisibleIndex="4" ReadOnly="true" Width="80px" CellStyle-HorizontalAlign="Center" />
        <dx:TreeListDataColumn FieldName="MENU_INDEX" Caption="索引" VisibleIndex="5" Width="80px" CellStyle-HorizontalAlign="Center" />
        <dx:TreeListCheckColumn FieldName="LEAF_FLAG" Caption="子节点?" VisibleIndex="6" Width="80px" CellStyle-HorizontalAlign="Center">
            <PropertiesCheckEdit ValueType="System.String" ValueChecked="Y" ValueUnchecked="N" />
        </dx:TreeListCheckColumn>
        <dx:TreeListComboBoxColumn FieldName="PROGRAM_CODE" Caption="程序代码" VisibleIndex="7" Width="150px">
            <PropertiesComboBox ValueType="System.String">
            </PropertiesComboBox>
        </dx:TreeListComboBoxColumn>
        <dx:TreeListComboBoxColumn FieldName="MENU_CODE_FATHER" Caption="父菜单代码" VisibleIndex="8" Width="150px">
            <PropertiesComboBox ValueType="System.String">
            </PropertiesComboBox>
        </dx:TreeListComboBoxColumn>
        <dx:TreeListComboBoxColumn FieldName="COMPANY_CODE" Caption="公司代码" VisibleIndex="9" Width="80px" CellStyle-HorizontalAlign="Center">
            <PropertiesComboBox ValueType="System.String">
            </PropertiesComboBox>
        </dx:TreeListComboBoxColumn>
        <dx:TreeListDataColumn Caption=" " VisibleIndex="19" Width="20%" />
    </Columns>        
</dx:ASPxTreeList>

</asp:Content>

