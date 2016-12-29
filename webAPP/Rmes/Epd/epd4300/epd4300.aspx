<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd4300.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd4300.epd4300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function DataBind(s,e) {
        workUnit.PerformCallback(s.lastSuccessValue);
    }

</script>


<dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" AutoGenerateColumns="false"  KeyFieldName="RMES_ID"
    OnRowInserting="ASPxGridView1_RowInserting"
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="600px"/>
    <Columns>
        <dx:GridViewCommandColumn Width="100px">
            <NewButton Text="新增" Visible="true"></NewButton>
            <EditButton Text="修改" Visible="true"></EditButton>
            <DeleteButton Text="删除" Visible="true"></DeleteButton>
            <%--<CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="cusbt1" Text="下达计划"></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>--%>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="" FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="" FieldName="COMPANY_CODE" Visible="false"/>
        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" />
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="WORKSHOP_CODE" />
        <dx:GridViewDataComboBoxColumn Caption="工作中心" FieldName="WORKUNIT_CODE" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="线边库代码" FieldName="STORE_CODE" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="线边库名称" FieldName="STORE_NAME" Width="150px"/>
        <dx:GridViewDataComboBoxColumn Caption="库房类型" FieldName="STORE_TYPE" />
        <dx:GridViewDataTextColumn Caption="" VisibleIndex="99" />

    </Columns>
    <%--<ClientSideEvents CustomButtonClick="function (s,e){ ShowPopup(s,e); }" />--%>
    <Templates>
        <EditForm>
        <table>
        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>' Visible="false"/>
        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="160px" Text='<%# Bind("COMPANY_CODE") %>' Visible="false"/>
                <tr style="height: 10px">
                    <td colspan="7">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="comPline" runat="server" Width="160px" Value='<%# Bind("PLINE_CODE") %>'>
                            <ClientSideEvents SelectedIndexChanged="function(s,e){
                                DataBind(s,e);
                            }
                            " />
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="工作中心">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="comWorkUnit" ClientInstanceName="workUnit" runat="server" Width="160px" Value='<%# Bind("WORKUNIT_CODE") %>'
                             OnCallback="WorkUnit_Callback">

                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="线边库代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtMinQTY" runat="server" Width="160px" Text='<%# Bind("STORE_CODE") %>'/>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="线边库名称">
                        
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="160px" Text='<%# Bind("STORE_NAME") %>'/>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="工厂">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="comBatchType" runat="server" Width="160px" Value='<%# Bind("WORKSHOP_CODE") %>'>
                            <Items>
                                <dx:ListEditItem Text="园区" Value="8101" />
                                <dx:ListEditItem Text="基地" Value="8102" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="库房类型">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="160px" Value='<%# Bind("STORE_TYPE") %>'>
                            <Items>
                                <dx:ListEditItem Text="线边库" Value="0" Selected="true"/>
                                <dx:ListEditItem Text="中心仓库" Value="1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>


                <tr style="height: 30px">
                    <td></td>
                    <td colspan="2" style="text-align: center;">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                    </td>
                    <td></td>
                    <td colspan="2" style="text-align: right;">
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
                       
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
        
    </Templates>
</dx:ASPxGridView>

</asp:Content>
