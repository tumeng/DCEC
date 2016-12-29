<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_ems2200" Title="" Codebehind="ems2200.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">

   
    function getOther() {
        var srlno = cSrlNo.GetText();
        CallbackSubmit.PerformCallback(srlno);
    }

    
    function OnMntType_Changed() {
        var srlno = cSrlNo.GetText();

        var mnttype = cltMaintType.GetValue();
        var rry = mnttype.split('-');
        mnttype = rry[0];

        if (mnttype == 'M01') {
            cmbMntItem_clt.SetEnabled(true);
            cltFault.SetEnabled(false);
            cmbMntItem_clt.PerformCallback(srlno + "," + mnttype);
        }
        else {
            cmbMntItem_clt.SetEnabled(false);
            cltFault.SetEnabled(true);
        }
    
    }

    function submitRtr(strTemp) {
        
        var array = strTemp.split(',');

        cAssetCode.SetText(array[0]);
        cAssetName.SetText(array[1]);
        cAssetSpec.SetText(array[2]);
        cltMntCost.SetText("0");
        
    }

</script>


<asp:SqlDataSource ID="SqlMntType" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" ></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlMntItem" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" 
 SelectCommand="select distinct maint_item_code,maint_item_name from rel_asset_mntitem a where  exists
                (select * from data_asset_detail b,code_asset c  where b.asset_code=c.asset_code and a.asset_class_code=c.asset_class_code and b.serial_number=:srlno)">
                <SelectParameters>
                <asp:Parameter Name="srlno" />
                </SelectParameters>
</asp:SqlDataSource>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName ="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="ASSET_CODE"
    onrowupdating="ASPxGridView1_RowUpdating"  
    onrowinserting="ASPxGridView1_RowInserting" 
    onhtmleditformcreated="ASPxGridView1_HtmlEditFormCreated" 
    onrowdeleting="ASPxGridView1_RowDeleting" 
    onrowvalidating="ASPxGridView1_RowValidating" 
    onbeforecolumnsortinggrouping="ASPxGridView1_BeforeColumnSortingGrouping">

    <Settings ShowHorizontalScrollBar="true" />

    <SettingsEditing  PopupEditFormWidth="700px" PopupEditFormHeight="330px" />

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="100px" Caption="操作" >
            <EditButton Visible="True" ></EditButton>
            <NewButton Visible="True"></NewButton>
            <DeleteButton Visible="True"></DeleteButton>
            <ClearFilterButton Visible="True"></ClearFilterButton>
        </dx:GridViewCommandColumn>
                               
        <dx:GridViewDataTextColumn  FieldName="COMPANY_CODE"  Visible="false" />
        <dx:GridViewDataTextColumn  Caption="设备序列号" FieldName="SERIAL_NUMBER"  VisibleIndex="1"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="维护类别" FieldName="MAINT_TYPE"  VisibleIndex="2"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="维护项目" FieldName="MAINT_ITEM" Visible="false" />
        <dx:GridViewDataTextColumn  Caption="维护项目" FieldName="MAINT_ITEM_NAME"  VisibleIndex="3"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="服务单位" FieldName="MAINT_SERVICE_UNIT"  VisibleIndex="4"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="服务人员" FieldName="MAINT_SERVICE_PERSON"  VisibleIndex="5"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="维护开始日期" FieldName="MAINT_START_DATE"  VisibleIndex="6"  Width="120px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="维护结束日期" FieldName="MAINT_END_DATE"  VisibleIndex="7"  Width="120px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="维护结果" FieldName="MAINT_RESULT"  VisibleIndex="8"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="维护费用" FieldName="MAINT_COST"  VisibleIndex="9"  Width="80px" Visible="true" />
            
            
        <dx:GridViewDataTextColumn  Caption="设备代码" FieldName="ASSET_CODE"  VisibleIndex="10"  Width="80px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="设备名称"  FieldName="ASSET_NAME"  VisibleIndex="11"  Width="100px" Visible="true" />
        <dx:GridViewDataTextColumn  Caption="设备规格"  FieldName="ASSET_SPEC"  VisibleIndex="12"  Width="100px" Visible="true" />
        <dx:GridViewDataTextColumn   Caption="设备型号" FieldName="ASSET_MODEL"  VisibleIndex="13"  Width="80px" Visible="true" />
            
           

        <dx:GridViewDataTextColumn Caption="所属部门" FieldName="RESPONSE_DEPT_CODE" VisibleIndex="9"  Width="100px" Visible="true"/>

        <dx:GridViewDataTextColumn Caption="责任人" FieldName="RESPONSE_PERSON_CODE" VisibleIndex="10"  Width="100px" Visible="true"/>
        <dx:GridViewDataTextColumn Caption="故障描述" FieldName="FAULT_SCRIPT" VisibleIndex="19"  Width="100" />
        <dx:GridViewDataTextColumn Caption="备注" FieldName="MAINT_REMARK" VisibleIndex="20"  Width="100" />
            
            
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
                        <td style="text-align: left; width:15%">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="序列号" AssociatedControlID="txtSrlNo"/>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtSrlNo" ClientInstanceName="cSrlNo" runat="server" Text='<%# Bind("SERIAL_NUMBER") %>'  Width="200"  
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ClientSideEvents  TextChanged="function(s,e) { getOther(); e.processOnServer = false; }" />
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                    ErrorText="设备序列编号有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="设备序列编号字节长度不能超过30！" 
                                        ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="设备序列编号不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                </dx:ASPxTextBox>
                            <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit" OnCallback="ASPxCbSubmit_Callback">
                                <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                            </dx:ASPxCallback>
                        </td>
                        
                        <td style="width:15%; text-align: left;">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="设备代码"  />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtAssetCode" ClientInstanceName="cAssetCode" runat="server"    Text='<%# Bind("ASSET_CODE") %>' Width="200">  
                            </dx:ASPxTextBox>
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
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server"  Text="设备型号" />
                                
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtAssetSpec" ClientInstanceName="cAssetSpec" runat="server" Text='<%# Bind("ASSET_SPEC") %>' Width="200"  />
                        </td>
                    </tr>
                    <tr style=" height:30px">
                            
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="维护类别" /> 
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxGridLookup ID="lkpMaintType" runat="server" AllowMouseWheel="true" 
                                AllowUserInput="true" ClientInstanceName="cltMaintType" 
                                DataSourceID="SqlMntType" IncrementalFilteringMode="Contains" 
                                KeyFieldName="INTERNAL_CODE" SelectionMode="Single" TextFormatString="{0}-{1}" 
                                Value='<%# Bind("MAINT_TYPE") %>' Width="200" >
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="类别编号"  FieldName="INTERNAL_CODE" />
                                    <dx:GridViewDataTextColumn Caption="名称" FieldName="INTERNAL_NAME" />
                                </Columns>
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager AllButton-Visible="true" AlwaysShowPager="true" PageSize="8">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" />
                                </GridViewProperties>
                                <ClientSideEvents ValueChanged="function(s,e) { OnMntType_Changed(); e.processOnServer = false; }" />
                            </dx:ASPxGridLookup>
                                
                            <%--<dx:ASPxCallback ID="cbkMntType" runat="server" ClientInstanceName="cbkMntType_clt" OnCallback="cbkMntType_Callback">--%>
                                
                            <%--</dx:ASPxCallback>--%>
                        </td>
                            
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server"  Text="维护项目" />
                                
                        </td>
                        <td style="text-align: left">
                                
                            <dx:ASPxComboBox ID="cmbMntItem" ClientInstanceName="cmbMntItem_clt" runat="server" DataSourceID="SqlMntItem" Value='<%# Bind("MAINT_ITEM") %>'
                                ValueField="maint_item_code"  TextField="maint_item_name" Width="200" EnableSynchronization="False" OnCallback="cmbMntItem_Callback" >
                                </dx:ASPxComboBox>
                        </td>
                    </tr>

                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="维护单位" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtSrvUnit" runat="server" Text='<%# Bind("MAINT_SERVICE_UNIT") %>' Width="200"  />
                        </td>
                            
                            
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="实施人" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtSrvPerson" runat="server" Text='<%# Bind("MAINT_SERVICE_PERSON") %>' Width="200"  />
                        </td>
                    </tr>
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" AssociatedControlID="CalMntStartDate" Text="维护开始日期" ></dx:ASPxLabel>
                                
                        </td>
                        <td style="text-align: left">
                        
                            <dx:ASPxDateEdit ID="calMntStartDate" runat="server" Date='<%# theMntStartDate %>'  EditFormatString="yyyy-MM-dd" 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                    ErrorText="维护开始日期有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                    <RequiredField ErrorText="维护开始日期不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" AssociatedControlID="CalMntEndDate" Text="维护结束日期" ></dx:ASPxLabel>
                        </td>
                        <td style="text-align: left">
                        
                            <dx:ASPxDateEdit ID="calMntEndDate" runat="server" Date='<%# theMntEndDate %>'  EditFormatString="yyyy-MM-dd" 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" 
                                    ErrorText="维护结束日期有误，请重新输入！" SetFocusOnError="True" ValidateOnLeave="True">
                                        
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="维护结果" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtMntResult" runat="server" Text='<%# Bind("MAINT_RESULT") %>' Width="200"  />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" AssociatedControlID="txtMntCost"  Text="维护费用" />
                                
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxTextBox ID="txtMntCost" ClientInstanceName="cltMntCost" runat="server" Text='<%# Bind("MAINT_COST") %>' 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="false" ErrorText="维护费用有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="必须输入数字！" 
                                        ValidationExpression="^-?(0|\d+)(\.\d+)?$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                            
                    </tr>
                        
                    <tr style=" height:30px">
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" AssociatedControlID="txtRemark" 
                                Text="备    注" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxMemo ID="txtRemark" runat="server" ClientInstanceName="cltRemark" Height="50px" Text='<%# Bind("MAINT_REMARK") %>' 
                                    
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"  Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="备注有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="备注字节长度不能超过500！" 
                                        ValidationExpression="^.{0,500}$" />
                                </ValidationSettings>
                            </dx:ASPxMemo>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" AssociatedControlID="txtRemark" 
                                Text="故障描述" />
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxMemo ID="txtFault" runat="server" ClientInstanceName="cltFault" Height="50px" Text='<%# Bind("FAULT_SCRIPT") %>' 
                                    
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"  Width="200">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="故障描述有误，请重新输入！" 
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RegularExpression ErrorText="故障描述字节长度不能超过500！" 
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

