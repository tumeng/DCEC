<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="inv2101.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv2100.inv2101" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">

    function GetDataCallback(result) {
        var t = result.split(',');
        popup.Show();


        ItemCode.SetText(t[0]);
        ItemCode.enabled = false;
        ResourceQTY.SetText(t[1]);
        ResourceQTY.enabled = false;
        ResourceStore.SetText(t[2]);
        ResourceStore.SetVisible(false);


        DestinationStore.SetSelectedIndex(0);
        TransQTY.SetText("0");
    }
    function ShowPopup(s, e) {
        var index = e.visibleIndex;
        grid.SelectRows(index);
        grid.GetValuesOnCustomCallback(index, GetDataCallback);
    }

</script>

<table id="Table1" runat="server">
    <tr>
        <td>
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="订单号"></dx:ASPxLabel>
        </td>
        <td>
            
            <dx:ASPxComboBox runat="server" ID="PlanCode" OnSelectedIndexChanged="LineSideStore_SelectedIndexChanged" AutoPostBack="true"></dx:ASPxComboBox>
        </td>
    </tr>
</table>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px"/>
    <Settings ShowHorizontalScrollBar="true"  ShowFilterRow="true"/>
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="1" Width="80px" FixedStyle="Left">
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
            <%--<EditButton Text="移库1" Visible="true"></EditButton>--%>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton Text="移库" ID="trans" ></dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
        </dx:GridViewCommandColumn>

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataComboBoxColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="物料数量" FieldName="ITEM_QTY" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="工厂" FieldName="FACTORY_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="线边库" FieldName="STORE_CODE" VisibleIndex="1" Width="100px"/>
        
        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="工位" FieldName="LOCATION_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
    <ClientSideEvents CustomButtonClick="
        function (s,e){
            ShowPopup(s, e);
        }
    
    " 
    
    />

</dx:ASPxGridView>
<dx:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <table width="600px">
                <tr style="height: 10px">
                    <td colspan="7">
                    </td>
                </tr>
                
                <tr>
                    <td><dx:ASPxTextBox ID="ResourceStore" ClientInstanceName="ResourceStore" runat="server" Width="160px" ReadOnly="true"/></td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="物料代号">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="ItemCode" ClientInstanceName="ItemCode" runat="server" Width="160px" ReadOnly="true"/>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="库存数量">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="ResourceQTY" ClientInstanceName="ResourceQTY" runat="server" Width="160px">
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="目的库">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="DestinationStore" ClientInstanceName="DestinationStore" runat="server">
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="移库数量">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="TransQTY" ClientInstanceName="TransQTY" runat="server" Width="160px">
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>


                <tr style="height: 30px">
                    <td></td>
                    <td colspan="4">
                        <dx:ASPxLabel ID="ErrorLabel" runat="server" Visible="false" ForeColor="Red"></dx:ASPxLabel>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td></td>
                    <td colspan="2" align="center">
                        <dx:ASPxButton ID="UpdateButton" ReplacementType="EditFormUpdateButton" Text="提交" OnClick="UpdateButton_Click"
                            runat="server"></dx:ASPxButton>
                    </td>
                    <td></td>
                    <td colspan="2" align="center">
                        <dx:ASPxButton ID="CancelButton" ReplacementType="EditFormCancelButton" Text="取消"
                            runat="server">
                            <ClientSideEvents Click="function(s,e) { popup.Hide(); }" />    
                        </dx:ASPxButton>
                    </td>
                    <td></td>
                </tr>

                <tr style="height: 30px">
                    <td colspan="7">
                    </td>
                </tr>

            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
</asp:Content>

