<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="qms3000.aspx.cs" Inherits="Rmes.WebApp.Rmes.Qms.qms3000.qms3000" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxSplitter" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeView" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
        <tr>
        <td style="width:5px">
        </td>
        <td style="width:50px">
            <label style="font-size:small">生产线</label>
        </td>
        <td>
            <dx:ASPxComboBox ID="ASPxComboBoxPline" ClientInstanceName="listPline" runat="server" Width="143px" >
                <ClientSideEvents SelectedIndexChanged="function(s,e) { tree.PerformCallback(s.lastSuccessValue); }" />
                <Items>
                    <dx:ListEditItem Text="开关柜生产线" Value="1" Selected="true"/>
                    <dx:ListEditItem Text="断路器生产线" Value="2" />
                </Items>
            </dx:ASPxComboBox>
        </td>
        </tr>
</table>
<dx:ASPxTreeList ID="ASPxTreeList1" runat="server" Width="100%" ClientInstanceName="tree" AutoGenerateColumns="False" KeyFieldName="RMES_ID" ParentFieldName="SEQ_FATHER"
    OnNodeDeleting="ASPxTreeList1_NodeDeleting"
    OnNodeUpdating="ASPxTreeList1_NodeUpdating"
    OnNodeInserting="ASPxTreeList1_NodeInserting"
    OnNodeValidating="ASPxTreeList1_NodeValidating"
    OnCellEditorInitialize="ASPxTreeList1_CellEditorInitialize"
    OnCustomCallback="ASPxTreeList1_CustomCallback">
    <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />
    <SettingsEditing Mode="PopupEditForm" />
    <SettingsPopupEditForm Width="600px" HorizontalAlign="Center" />
    <Columns>
        <dx:TreeListDataColumn FieldName="RMES_ID" Caption="" Visible="false"/>
       
        <dx:TreeListDataColumn FieldName="SEQ_VALUE" Caption="条码序号" VisibleIndex="0" Width="150px"/>
        <dx:TreeListDataColumn FieldName="SEQ_NAME" Caption="序号值" VisibleIndex="1" Width="150px" /> 
        <dx:TreeListCommandColumn VisibleIndex="1" Caption="操作" Width="120px">
            <NewButton Visible="True">
            </NewButton>
            <EditButton Visible="True">
            </EditButton>
            <DeleteButton Visible="True">
            </DeleteButton>
        </dx:TreeListCommandColumn>
        
        <dx:TreeListDataColumn FieldName="SEQ_LEVEL" Caption="层级" VisibleIndex="3" ReadOnly="true" Width="80px" CellStyle-HorizontalAlign="Center" />
        <dx:TreeListComboBoxColumn FieldName="PLINE_CODE" Caption="生产线" VisibleIndex="4" Width="150px">
            <PropertiesComboBox ValueType="System.String">
            </PropertiesComboBox>
        </dx:TreeListComboBoxColumn>
        <dx:TreeListComboBoxColumn FieldName="SEQ_FATHER" Caption="上级代码" VisibleIndex="5" Width="150px">
            <PropertiesComboBox ValueType="System.String">
            </PropertiesComboBox>
        </dx:TreeListComboBoxColumn>
        
        <dx:TreeListDataColumn Caption=" " VisibleIndex="19" Width="20%" />
    </Columns>  
    <%--<Templates>
        <EditForm>
        <dx:ASPxTextBox ID="txtRmesID" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>'
                    Visible="false" />
                <table>
                    <tr style="height: 10px">
                        <td colspan="7">
                        </td>
                    </tr>
                    
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="条码序号" />
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="texSEQValue" runat="server" Width="160px" Text='<%# Bind("SEQ_VALUE") %>'>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="序号值">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="texSEQName" runat="server" Width="160px" Text='<%# Bind("SEQ_NAME") %>'>

                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>

                    
                    
                    <tr style="height: 30px">
                        <td style="width: 15px;">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 165px">
                            <dx:ASPxComboBox ID="combPline" runat="server" Width="160px" Value='<%# Bind("PLINE_CODE") %>'>
                                <ClientSideEvents SelectedIndexChanged="function (s,e){comSEQFather.PerformCallback(s.lastSuccessValue);}" />
                                <Items>
                                    <dx:ListEditItem Text="开关柜生产线" Value="1" />
                                    <dx:ListEditItem Text="断路器生产线" Value="2" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="上级代码">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="comSEQFather" ClientInstanceName="comSEQFather" runat="server" Width="160px" Value='<%# Bind("SEQ_FATHER") %>'
                                 OnCallback="comSEQFather_Callback">
                                
                            </dx:ASPxComboBox>
                        </td>
                        
                        <td style="width: 5px">
                        </td>
                    </tr>


                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="层级">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtSEQLevel" runat="server" Width="160px" Value='<%# Bind("SEQ_LEVEL") %>'>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            
                        </td>
                        <td>
                            
                        </td>
                        <td>
                        </td>
                    </tr>

                    <tr style="height: 30px">
                        <td style="text-align: right;" colspan="6">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dx:ASPxGridViewTemplateReplacement>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td colspan="7">
                        </td>
                    </tr>
                </table>
        </EditForm>
    </Templates>--%>
</dx:ASPxTreeList>

</asp:Content>
