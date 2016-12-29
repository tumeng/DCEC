<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd4100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd4100.epd4100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">



    function GetData(s,e) {
        var corbillid = ItemCode.GetText().toUpperCase();
        ItemCode.SetText(corbillid);
        if (corbillid == "") return;
        CallbackSubmit.PerformCallback(corbillid);
    }

    function submitRtr(e) {
        var strRemark1 = "";
        var strRemark2 = "";
        if (e == "error") {
            alert("无此物料，请重新输入！");
            ItemCode.Focus();
        }
        else {
            strRemark1 = e.toString().split(',')[0];
            strRemark2 = e.toString().split(',')[1];
            ItemName.SetText(strRemark1);
            ItemName.SetEnabled(false);
            UnitCode.SetText(strRemark2);
            UnitCode.SetEnabled(false);
        }
        
    }


   
      
</script>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" AutoGenerateColumns="false"  KeyFieldName="RMES_ID"
    OnRowInserting="ASPxGridView1_RowInserting"
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowUpdating="ASPxGridView1_RowUpdating">
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
        <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" />
        <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" />
        <dx:GridViewDataComboBoxColumn Caption="线边库" FieldName="LINESIDE_STORE_CODE" />
        <dx:GridViewDataComboBoxColumn Caption="来源库" FieldName="RESOURCE_STORE" />
        <dx:GridViewDataTextColumn Caption="安全库存" FieldName="MIN_STOCK_QTY" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="拉料周期" FieldName="BATCH_TYPE" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="周期数量" FieldName="STAND_QTY" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="单位" FieldName="UNIT_CODE" Width="100px"/>
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
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="物料代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtItemCode" ClientInstanceName="ItemCode" runat="server" Width="160px" Text='<%# Bind("ITEM_CODE") %>'>
                            <ClientSideEvents TextChanged="function (s,e){
                                GetData(s,e)
                            }" />
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="物料名称" Enabled="false">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox4" ClientInstanceName="ItemName" runat="server" Width="160px" Text='<%# Bind("ITEM_NAME") %>'/>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="安全库存">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtMinQTY" runat="server" Width="160px" Text='<%# Bind("MIN_STOCK_QTY") %>'/>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="线边库">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="comLineSideStock" runat="server" Width="160px" Value='<%# Bind("LINESIDE_STORE_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                            OnDataBinding="comLineSideStock_DataBinding">

                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="线边库不能为空！" />
                            </ValidationSettings>

                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="拉料周期">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                       <dx:ASPxComboBox ID="comBatchType" runat="server" Width="160px" Value='<%# Bind("BATCH_TYPE") %>'>
                            <Items>
                                <dx:ListEditItem Text="天" Value="0"/>
                                <dx:ListEditItem Text="周" Value="1" Selected="true" />
                                <dx:ListEditItem Text="旬" Value="2"  />
                                <dx:ListEditItem Text="月" Value="3"  />
                                <dx:ListEditItem Text="季度" Value="4"  />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="周期数量">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                         <dx:ASPxTextBox ID="ASPxTextBox3" runat="server" Width="160px" Text='<%# Bind("STAND_QTY") %>' />
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>


                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="来源库">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="comResourceStore" runat="server" Width="160px" Value='<%# Bind("RESOURCE_STORE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                            OnDataBinding="comResourceStore_DataBinding">

                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="来源库不能为空！" />
                            </ValidationSettings>

                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="单位" Enabled="false">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ASPxTextBox5" ClientInstanceName="UnitCode" runat="server" Width="160px" Text='<%# Bind("UNIT_CODE") %>'/>
                        <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit" OnCallback="ASPxCbSubmit_Callback">
                                <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                            </dx:ASPxCallback>
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
        
    </Templates>
</dx:ASPxGridView>

</asp:Content>
