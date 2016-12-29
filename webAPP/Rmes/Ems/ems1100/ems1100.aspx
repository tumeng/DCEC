<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_ems1100" Codebehind="ems1100.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTabControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">

    function GetAssetClass() {
        var assetClass = cmbAssetClass.GetValue();
        clntItemTag.PerformCallback(itemClass);
    }
</script>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName ="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ASSET_CODE" 
    OnRowDeleting="ASPxGridView1_RowDeleting"  
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating" 
    onrowvalidating="ASPxGridView1_RowValidating"  
    SettingsBehavior-AllowFocusedRow="false" 
    onhtmleditformcreated="ASPxGridView1_HtmlEditFormCreated">
    
    <SettingsEditing PopupEditFormWidth="530"/>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="120px">
            <NewButton Visible="True" Text="新增"></NewButton>
            <EditButton Visible="True" Text="修改"></EditButton>
            <DeleteButton Visible="True" Text="删除"></DeleteButton>
            <ClearFilterButton Visible="True"></ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" ></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="ASSET_CODE" Caption="设备代码" Width="80px" VisibleIndex="1"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="ASSET_NAME" Caption="名称" Width="260px" VisibleIndex="2"></dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="ASSET_SPEC" Caption="规格" Width="180px" VisibleIndex="4"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="ASSET_MODEL" Caption="型号" Width="180px" VisibleIndex="5"></dx:GridViewDataTextColumn>
        <dx:GridViewDataComboBoxColumn FieldName="ASSET_CLASS_CODE" Caption="种类" Width="80px" VisibleIndex="6">
                <PropertiesComboBox DataSourceID="SqlAssetClass" ValueField="ASSET_CLASS_CODE" TextField="ASSET_CLASS_NAME" 
                IncrementalFilteringMode="StartsWith"  ValueType="System.String" DropDownStyle="DropDownList" />        
        </dx:GridViewDataComboBoxColumn>
        
         <dx:GridViewDataTextColumn FieldName="ASSET_REMARK" Caption="备注" VisibleIndex="9"></dx:GridViewDataTextColumn>
    </Columns>

    <Templates >

        <EditForm>
            <table>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td style="width:120px" align="right"><dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="设备代码" Width="80px"></dx:ASPxLabel></td>
                    <td style="width:200">
                        <dx:ASPxTextBox ID="txtAssetCode" runat="server" Width="180px"  Text='<%# Bind("ASSET_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>

                    <td style="width:120px" align="left"><dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="设备名称" Width="80px"></dx:ASPxLabel></td>
                    <td style="width:200">
                        <dx:ASPxTextBox ID="txtAssetName" runat="server" Width="180px" Text='<%# Bind("ASSET_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                       <td style="width:120px" align="right">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="设备型号" Width="80px" />
                        </td>
                        <td style="width:200">
                            <dx:ASPxTextBox ID="txtModel" runat="server" Text='<%# Bind("ASSET_MODEL") %>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                Width="180px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
        
                        <td style="width:120px" align="left">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="设备规格" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width:200">
                            <dx:ASPxTextBox ID="txtSpec" runat="server" Text='<%# Bind("ASSET_SPEC") %>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                Width="180px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                </tr>
                <tr>
                    <td style="width:120px" align="right"><dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="设备类别" Width="80px"></dx:ASPxLabel></td>
                    <td style="width:200">
                        <dx:ASPxComboBox ID="txtClass"  ClientInstanceName="cmbAssetClass" EnableClientSideAPI="True"  runat="server" DataSourceID="SqlAssetClass" 
                        TextField="ASSET_CLASS_NAME" ValueField="ASSET_CLASS_CODE" ValueType="System.String" Width="180px" 
                        Value='<%# Bind("ASSET_CLASS_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"> 
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="类别不能为空！" />
                            </ValidationSettings>
     
                         </dx:ASPxComboBox>                        
                    </td>
                    
                    <td style="width:120px" align="left"><dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="备          注" Width="80px"></dx:ASPxLabel></td>
                    <td style="width:200px">
                        <dx:ASPxTextBox ID="txtRemark" runat="server" Width="180px"  Text='<%# Bind("ASSET_REMARK") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>

                </tr>   
                                                                    
                <tr>
                    <td colspan="4" style="text-align:right;">
                        <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"  runat="server"></dx:ASPxGridViewTemplateReplacement>
                        <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"  runat="server"></dx:ASPxGridViewTemplateReplacement>
                    </td>

                </tr>
                <tr><td style="height:30px" colspan="6"></td></tr>
                
            </table>
        </EditForm>
    </Templates>
 
    <SettingsText ConfirmDelete="该设备相关配置将被同时删除，确认继续？" />
    <ClientSideEvents 
        BeginCallback="function(s, e) 
        {
            grid.cpCallbackName = '';
        }"
        EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            callbackValue = grid.cpCallbackValue;
            if(callbackName == 'alert') 
            {
                alert(callbackValue);
            }
        }" 
    /> 
</dx:ASPxGridView>

<asp:SqlDataSource ID="SqlAssetClass" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" ></asp:SqlDataSource>

</asp:Content>