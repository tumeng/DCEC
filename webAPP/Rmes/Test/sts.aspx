<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="sts.aspx.cs" Inherits="Rmes.WebApp.Rmes.Test.sts" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">
    function DataBind(s, e) {
        station.PerformCallback(s.lastSuccessValue);
        stationSub.PerformCallback(s.lastSuccessValue);
    }
</script>
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowInserting="ASPxGridView1_RowInserting"
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowDeleting="ASPxGridView1_RowDeleting">
<SettingsEditing PopupEditFormWidth="600px"/>
<Settings ShowHorizontalScrollBar="true" />
<Columns>
    <dx:GridViewCommandColumn Width="100px">
        <NewButton Text="新增" Visible="true"></NewButton>
        <EditButton Text="修改" Visible="true"></EditButton>
        <DeleteButton Text="删除" Visible="true"></DeleteButton>
    </dx:GridViewCommandColumn>
    <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
    <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>
    <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1" Width="180px"/>
    <dx:GridViewDataComboBoxColumn Caption="取点站点" FieldName="STATION_CODE" VisibleIndex="2" Width="100px"/>
    <dx:GridViewDataComboBoxColumn Caption="分装站点" FieldName="STATION_CODE_SUB" VisibleIndex="3" Width="100px"/>
    <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
</Columns>
<Templates>
    <EditForm>
        <table>
                <tr style="height: 10px">
                    <td colspan="7">
                    </td>
                </tr>
                <dx:ASPxTextBox ID="txtRmesID" runat="server" Text='<%# Bind("RMES_ID") %>' Visible="false"></dx:ASPxTextBox>
                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="ComboBoxPline" runat="server" Value='<%# Bind("PLINE_CODE") %>'
                            OnDataBinding="ComboBoxPline_DataBinding">
                            <ClientSideEvents SelectedIndexChanged="function (s,e){
                                DataBind(s,e);
                            }" />
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="取点站点">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxComboBox ID="ASPxComboBoxStation" ClientInstanceName="station" runat="server"  Value='<%# Bind("STATION_CODE") %>'
                            OnCallback="ASPxComboBoxStation_Callback" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="取点站点有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="取点站点不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="分装站点">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ASPxComboBoxStationSub" ClientInstanceName="stationSub" runat="server" Value='<%# Bind("STATION_CODE_SUB") %>'
                            OnCallback="ASPxComboBoxStationSub_Callback" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="分装站点有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="分装站点不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td></td>
                    <td colspan="4">
                        
                    </td>
                    <td style="text-align: right;">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                            runat="server"></dx:ASPxGridViewTemplateReplacement>
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
