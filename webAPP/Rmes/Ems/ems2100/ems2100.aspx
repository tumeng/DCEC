<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_ems2100" Title="" Codebehind="ems2100.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">

    function getOther() {
        var itemCode = cAssetCode.GetText();
        CallbackSubmit.PerformCallback(itemCode);
    }
    
    function submitRtr(strTemp) {
        var resutl = "";
        var retStr = "";
        var array = strTemp.split(',');
        
        retStr = array[1];
        result = array[0];
        cAssetName.SetText(result);
        cAssetModel.SetText(retStr);
        cAssetSpec.SetText(array[2]);
    }
</script>


<asp:SqlDataSource ID="SqlManuf" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlVendor" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" 
 SelectCommand="SELECT vendor_code,vendor_name from code_vendor where type='A' order by vendor_code"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDept" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlUser" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    
<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName ="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ASSET_CODE"
    onrowupdating="ASPxGridView1_RowUpdating"  
    onrowinserting="ASPxGridView1_RowInserting" 
    onhtmleditformcreated="ASPxGridView1_HtmlEditFormCreated" 
    onrowdeleting="ASPxGridView1_RowDeleting" 
    onrowvalidating="ASPxGridView1_RowValidating" 
    onbeforecolumnsortinggrouping="ASPxGridView1_BeforeColumnSortingGrouping">

    <Settings ShowHorizontalScrollBar="true" />

    <SettingsEditing  PopupEditFormWidth="700px" PopupEditFormHeight="420px" />

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" Caption="操作" >
            <EditButton Visible="True" ></EditButton>
            <NewButton Visible="True"></NewButton>
            <DeleteButton Visible="True"></DeleteButton>
            <ClearFilterButton Visible="True"></ClearFilterButton>
        </dx:GridViewCommandColumn>

        <dx:GridViewDataTextColumn  FieldName="COMPANY_CODE"  Visible="false" />
        <dx:GridViewDataTextColumn  Caption="设备代码" FieldName="ASSET_CODE"  VisibleIndex="1"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="设备序列号" FieldName="SERIAL_NUMBER"  VisibleIndex="2"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="设备名称"  FieldName="ASSET_NAME"  VisibleIndex="3"  Width="100px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="设备规格"  FieldName="ASSET_SPEC"  VisibleIndex="4"  Width="100px" Visible="true" />
        <dx:GridViewDataTextColumn   Caption="设备型号" FieldName="ASSET_MODEL"  VisibleIndex="5"  Width="80px" Visible="true" />
            
        <dx:GridViewDataTextColumn  FieldName="VENDOR_CODE"  Visible="false" />
        <dx:GridViewDataTextColumn   Caption="供货商" FieldName="VENDOR_NAME"  VisibleIndex="6"  Width="200px"  Visible="true" />
        <dx:GridViewDataTextColumn   FieldName="MANUFACTURER_CODE"  VisibleIndex="7"  Width="100px"  Visible="false" />
        <dx:GridViewDataTextColumn  Caption="制造商"  FieldName="MANUFACTURER_NAME"  VisibleIndex="8"  Width="200px" Visible="true"/>

        <dx:GridViewDataTextColumn Caption="所属部门" FieldName="RESPONSE_DEPT_CODE" VisibleIndex="9"  Width="100px" Visible="true"/>

        <dx:GridViewDataTextColumn Caption="责任人" FieldName="RESPONSE_PERSON_CODE" VisibleIndex="10"  Width="100px" Visible="true"/>
        <dx:GridViewDataTextColumn Caption="购置日期" FieldName="PURCHASE_DATE" ReadOnly="True" VisibleIndex="11" Width="120px" />
        <dx:GridViewDataTextColumn Caption="采购价格" FieldName="PURCHASE_COST" VisibleIndex="12"  Width="60px"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="保质开始日期" FieldName="WARRANTY_START_DATE" VisibleIndex="13"  Width="120" />

        <dx:GridViewDataTextColumn Caption="保质结束日期" FieldName="WARRANTY_END_DATE" VisibleIndex="14"  Width="120" />
        <dx:GridViewDataTextColumn Caption="条码号" FieldName="BAR_CODE" VisibleIndex="15"  Width="120" />
        <dx:GridViewDataTextColumn Caption="颜色" FieldName="ASSET_COLOR" VisibleIndex="16"  Width="60" />
        <dx:GridViewDataTextColumn Caption="是否启用" FieldName="ACTIVE_FLAG" VisibleIndex="17"  Width="60" Visible="false" />
        <dx:GridViewDataTextColumn Caption="是否启用" FieldName="ACTIVE_FLAG_NAME" VisibleIndex="18"  Width="60" />
        <dx:GridViewDataTextColumn Caption="备注" FieldName="ASSET_REMARK" VisibleIndex="19"  Width="100" />

        <dx:GridViewDataTextColumn Caption=" " Width="30%" VisibleIndex="20"></dx:GridViewDataTextColumn>
    </Columns>
    <ClientSideEvents EndCallback="function(s, e) {
        callbackName = grid.cpCallbackName;
        callbackValue=grid.cpCallbackValue;
                
        if(callbackName == 'alert') 
        {
            alert(theRet);
        }
                
    }" BeginCallback="function(s, e) {
	    grid.cpCallbackName = '';
    }" />            
    <Templates>
        <EditForm>
        <center>
            <table width="95%" >
                <caption>
                    <br />
                    <tr style=" height:30px">
                        <td style="width:15%; text-align: left;">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="设备代码" AssociatedControlID="txtAssetCode" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtAssetCode" runat="server" ClientInstanceName="cAssetCode" 
                                Text='<%# Bind("ASSET_CODE") %>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200">
                                    
                                <ClientSideEvents TextChanged="function(s,e) { getOther(); e.processOnServer = false; }" />
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                    ErrorText="设备代码有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="设备代码字节长度不能超过30！" 
                                        ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="设备代码不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                                

                        </td>
                        <td style="text-align: left; width:15%">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="序列号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtSrlNo" runat="server" Text='<%# Bind("SERIAL_NUMBER") %>'  Width="200" />
                            <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit" OnCallback="ASPxCbSubmit_Callback">
                                <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                            </dx:ASPxCallback>
                        </td>
                    </tr>
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="设备名称" />
                                
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtAssetName" ClientInstanceName="cAssetName" runat="server" Text='<%# Bind("ASSET_NAME") %>'  Width="200" />
                                    
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="设备规格" />
                                
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtAssetSpec" ClientInstanceName="cAssetSpec" runat="server" Text='<%# Bind("ASSET_SPEC") %>' Width="200"  />
                        </td>
                    </tr>
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server"  Text="设备型号" />
                                
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtAssetModel" ClientInstanceName="cAssetModel" runat="server" Text='<%# Bind("ASSET_MODEL") %>'  Width="200" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="供货商" />
                                
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxGridLookup ID="GridLookupVendor" runat="server" AllowMouseWheel="true" 
                                AllowUserInput="true" ClientInstanceName="ClientGridLookupVendor" 
                                DataSourceID="SqlVendor" IncrementalFilteringMode="Contains" 
                                KeyFieldName="VENDOR_CODE" SelectionMode="Single" TextFormatString="{0}{1}" 
                                Value='<%# Bind("VENDOR_CODE") %>' Width="200">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="供货商代码" FieldName="VENDOR_CODE" />
                                    <dx:GridViewDataTextColumn Caption="名称" FieldName="VENDOR_NAME" />
                                </Columns>
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager AllButton-Visible="true" AlwaysShowPager="true" PageSize="8">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" />
                                </GridViewProperties>
                                   
                            </dx:ASPxGridLookup>
                        </td>
                    </tr>

                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="制造商" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxGridLookup ID="GridLookupManuf" runat="server" AllowMouseWheel="true" 
                                AllowUserInput="true" ClientInstanceName="ClientGridLookupManu" 
                                DataSourceID="SqlVendor" IncrementalFilteringMode="Contains" 
                                KeyFieldName="VENDOR_CODE" SelectionMode="Single" TextFormatString="{0}{1}" 
                                Value='<%# Bind("MANUFACTURER_CODE") %>' Width="200">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="制造商代码" FieldName="VENDOR_CODE" />
                                    <dx:GridViewDataTextColumn Caption="名称" FieldName="VENDOR_NAME" />
                                </Columns>
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager AllButton-Visible="true" AlwaysShowPager="true" PageSize="8">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" />
                                </GridViewProperties>
                                    
                            </dx:ASPxGridLookup>
                        </td>
                            
                            
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="所属部门" />
                        </td>
                        <td style="text-align: left">     
                            <dx:ASPxGridLookup ID="GridLookupDept" runat="server" AllowMouseWheel="true" 
                                AllowUserInput="true" ClientInstanceName="ClientGridLookupDept" 
                                DataSourceID="SqlDept" IncrementalFilteringMode="Contains" 
                                KeyFieldName="DEPT_CODE" SelectionMode="Single" TextFormatString="{0}{1}" 
                                Value='<%# Bind("RESPONSE_DEPT_CODE") %>' Width="200">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="部门代码" FieldName="DEPT_CODE" />
                                    <dx:GridViewDataTextColumn Caption="名称" FieldName="DEPT_NAME" />
                                </Columns>
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager AllButton-Visible="true" AlwaysShowPager="true" PageSize="8">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" />
                                </GridViewProperties>
                                   
                            </dx:ASPxGridLookup>
                        </td>
                    </tr>
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" AssociatedControlID="CalPCDate" Text="购置日期" ></dx:ASPxLabel>
                                
                        </td>
                        <td style="text-align: left">
                                
                                                               
                            <dx:ASPxDateEdit ID="CalPCDate" runat="server" Date='<%# thePurchaseDate %>'  EditFormatString="yyyy-MM-dd" 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                    ErrorText="购置日期有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                    <RequiredField ErrorText="购置日期不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                            
                            
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" AssociatedControlID="txtPrice" 
                                Text="采购价格">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtPrice" runat="server" Text='<%# Bind("PURCHASE_COST") %>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="价格有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="必须输入数字！" 
                                        ValidationExpression="^-?(0|\d+)(\.\d+)?$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                            
                    </tr>
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label3" runat="server" Text="保质开始日期">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxDateEdit ID="CalWRTStartDate" runat="server" Date='<%# WrtStartDate %>' 
                                EditFormatString="yyyy-MM-dd"  Width="200" />
                        </td>
                            
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="保质结束日期">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxDateEdit ID="CalWRTEndDate" runat="server" Date='<%# WrtEndDate %>' 
                                EditFormatString="yyyy-MM-dd"  Width="200" />
                        </td>
                    </tr>
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="颜色" />
                        </td>
                        <td style="text-align: left;width:41%">
                            <dx:ASPxTextBox ID="txtColor" runat="server" Text='<%# Bind("ASSET_COLOR") %>'  Width="200" />
                        </td>
                            
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="条码号" />
                        </td>
                        <td style="text-align: left;width:41%">
                            <dx:ASPxTextBox ID="txtBarCode" runat="server" Text='<%# Bind("BAR_CODE") %>'  Width="200" />
                        </td>
                    </tr>
                        
                        
                    <tr>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="责任人" />
                        </td>
                        <td style="text-align: left">
                        <dx:ASPxGridLookup ID="lkpResponsePerson" runat="server" AllowMouseWheel="true" 
                                AllowUserInput="true" ClientInstanceName="ClientGridLookupRsps" 
                                DataSourceID="SqlUser" IncrementalFilteringMode="Contains" 
                                KeyFieldName="USER_ID" SelectionMode="Single" TextFormatString="{0}{1}" 
                                Value='<%# Bind("RESPONSE_PERSON_CODE") %>' Width="200">
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="人员代码" FieldName="USER_ID" />
                                    <dx:GridViewDataTextColumn Caption="姓名" FieldName="USER_NAME" />
                                </Columns>
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager AllButton-Visible="true" AlwaysShowPager="true" PageSize="8">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" />
                                </GridViewProperties>
                                   
                            </dx:ASPxGridLookup>
                        </td>

                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label7" runat="server" Text="投入使用" Visible="true">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxCheckBox ID="chkActiveFlag" runat="server" Checked="true" Visible="true">
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" AssociatedControlID="txtRemark" 
                                Text="备注" />
                        </td>
                        <td colspan="3" style="text-align: left">
                            <dx:ASPxMemo ID="txtRemark" runat="server" ClientInstanceName="cltRemark" Height="30px" Text='<%# Bind("ASSET_REMARK") %>' 
                                    
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"  Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="备注有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="备注字节长度不能超过500！" 
                                        ValidationExpression="^.{0,500}$" />
                                </ValidationSettings>
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" 
                                ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" 
                                ReplacementType="EditFormCancelButton" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </caption>
            </table>
        </center>
        </EditForm>
    </Templates>
</dx:ASPxGridView>
   
</asp:Content>

