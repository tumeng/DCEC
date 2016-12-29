<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_epd1500" Title=""  Codebehind="epd1500.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">
// <![CDATA[
    function OnPlineChanged(Pline_Code) {
        Station_Code.PerformCallback(Pline_Code.GetValue().toString());           
    }
// ]]> 
</script>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;PLINE_CODE;STATION_SPECIAL_CODE"
    OnRowUpdating="ASPxGridView1_RowUpdating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowInserting="ASPxGridView1_RowInserting"
    OnRowValidating="ASPxGridView1_RowValidating">
    <SettingsBehavior ColumnResizeMode="Control"/>
    <SettingsEditing PopupEditFormWidth="600px" />
        
    <Columns>
        <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="100px">
            <EditButton Visible="True">
            </EditButton>
            <NewButton Visible="True">
            </NewButton>
            <DeleteButton Visible="True">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
            
        <dx:GridViewDataTextColumn Caption="公司代码" FieldName="COMPANY_CODE" VisibleIndex="1" Width="400px" Visible="False">
        </dx:GridViewDataTextColumn>            
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="2" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="特殊站点代码" FieldName="STATION_SPECIAL_CODE" VisibleIndex="3" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="特殊站点名称" FieldName="STATION_SPECIAL_NAME" VisibleIndex="4" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="对应站点" FieldName="STATION_NAME" VisibleIndex="5" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
        
    <Templates>
        <EditForm>
            <center>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td style="width: 100px; text-align: left">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="生产线" AssociatedControlID="PlineCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="PlineCombo" runat="server" OnLoad="ASPxComboBox1_Load" EnableClientSideAPI="True"
                                DataSourceID="SqlDataSource1" ValueType="System.String" Width="150px" OnInit="ASPxComboBox1_Init"
                                TextField="pline_name" ValueField="rmes_id" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {OnPlineChanged(s); }" />
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 100px; text-align: left">
                            <dx:ASPxLabel ID="Label4" runat="server" Text="特殊站点代码" AssociatedControlID="txtSpecialCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="txtSpecialCode" runat="server" Text='<%# Bind("STATION_SPECIAL_CODE") %>'
                                Width="150px" EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="特殊站点代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="特殊站点代码不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 100px; text-align: left">
                            <dx:ASPxLabel ID="Label5" runat="server" Text="特殊站点名称" AssociatedControlID="txtSpecialName">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 230px" align="left">
                            <dx:ASPxTextBox ID="txtSpecialName" runat="server" Width="200px" Text='<%# Bind("STATION_SPECIAL_NAME") %>'
                                EnableClientSideAPI="True" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="特殊站点名称字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField IsRequired="True" ErrorText="特殊站点名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 100px; text-align: left">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="对应站点" AssociatedControlID="StationCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="StationCombo" runat="server" EnableClientSideAPI="True" ValueType="System.String"
                                Width="150px" TextField="STATION_CODE" ValueField="STATION_CODE" TextFormatString="{1}"
                                DataSourceID="SqlDataSource2" ClientInstanceName="Station_Code" OnCallback="StationCombo_Callback"
                                DropDownStyle="DropDown" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="对应站点不能为空！" />
                                </ValidationSettings>
                                <Columns>
                                    <dx:ListBoxColumn FieldName="Station_Code" Caption="站点代码" Width="100px" />
                                    <dx:ListBoxColumn FieldName="Station_Name" Caption="站点名称" Width="130px" />
                                </Columns>
                            </dx:ASPxComboBox>
                        </td>
                        <td colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="right">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px" colspan="7">
                        </td>
                    </tr>
                </table>
            </center>
        </EditForm>
    </Templates>
</dx:ASPxGridView>
    
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
    ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
    ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
        
</asp:Content>

