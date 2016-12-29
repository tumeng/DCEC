<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="qms3200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Qms.qms3200.qms3200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var index;

        function startCallBack(s,e) {
            grid.cpIndex = grid.GetFocusedRowIndex();

        }

        function ShowPopup(s,e) {

            index = e.visibleIndex;
            //grid.UnselectAllRowsOnPage();
            //grid.SelectRows(index,true);
            _errorLabel.SetVisible(false);

            s.GetRowValues(index, 'PLINE_CODE', GetPlineCode);

//            grid.GetValuesOnCustomCallback("workunit,"+index, GetWorkUnitCallback);

//            grid.GetValuesOnCustomCallback("detectitem," + index, GetDetectItemCallback);

          
        }

        function submit(s,e) {
            grid.GetValuesOnCustomCallback("", GetDataCallback);
        }

        function GetDataCallback(value) {
            var s = value;
            if (value == "success") {
                popup.Hide();
                window.location.href = window.location.href;
            }
        }

//        function GetDetectItemCallback(value) {
//            var r = value.split(',');
//        }

        function GetPlineCode(value) {
            var plineCode = value;
            for (var i = 0; i < _combPline.GetItemCount(); i++) {
                var itema = _combPline.GetItem(i);
                if (itema.value == plineCode) {
                    _combPline.SetSelectedIndex(i);
                }
            }
            _comWorkUnit.PerformCallback(plineCode);
            
            grid.GetRowValues(index, 'WORKUNIT_CODE', GetWorkUnit);
        }

        function GetWorkUnit(value) {
            var workUnit = value;
            for (var i = 0; i < _comWorkUnit.GetItemCount(); i++) {
                var itema = _comWorkUnit.GetItem(i);
                if (itema.value == workUnit) {
                    _comWorkUnit.SetSelectedIndex(i);
                }
            }
            _comDetectItem.PerformCallback(workUnit);
            grid.GetRowValues(index, 'DETECT_ITEM_CODE', GetDetectItem);
        }

        function GetDetectItem(value) {
            var detectItem = value;
            for (var i = 0; i < _comDetectItem.GetItemCount(); i++) {
                var itema = _comDetectItem.GetItem(i);
                if (itema.value == detectItem) {
                    _comDetectItem.SetSelectedIndex(i);
                }
            }
            grid.GetRowValues(index, 'ERROR_ITEM_CODE', GetErrorCode);
        }

        function GetErrorCode(value) {
            var errorCode = value;
            _texErrorDataCode.SetText(errorCode);
            grid.GetRowValues(index, 'ERROR_ITEM_NAME', GetErrorName);
        }

        function GetErrorName(value) {
            var errorName = value;
            _texErrorDataName.SetText(errorName);
            grid.GetRowValues(index, 'RMES_ID', GetRmesID)
            
        }

        function GetRmesID(value) {
            var errorName = value;
            _txtRmesID.SetText(errorName);
            _txtRmesID.SetVisible(false);
            popup.Show();
        }
</script>

    
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="RMES_ID" 
        OnRowInserting="ASPxGridView1_RowInserting"
        OnRowUpdating="ASPxGridView1_RowUpdating" 
        OnRowDeleting="ASPxGridView1_RowDeleting"
        OnRowValidating="ASPxGridView1_RowValidating" 
        OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="false" />
        <SettingsEditing PopupEditFormWidth="600px" PopupEditFormHorizontalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="120px">
                <NewButton Visible="True" Text="新增" />
                <EditButton Visible="true" Text="修改"></EditButton>
                <%--<CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="update" Visibility="AllDataRows" Text="修改" />
                </CustomButtons> --%>
                <DeleteButton Visible="True" Text="删除" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1" Width="150px" />
            <dx:GridViewDataComboBoxColumn Caption="工作中心" FieldName="WORKUNIT_CODE" VisibleIndex="2" Width="150px" />
            <dx:GridViewDataTextColumn Caption="检验项目代码" FieldName="DETECT_ITEM_CODE" VisibleIndex="3" Width="120px" />
            <dx:GridViewDataTextColumn Caption="检验项目名称" FieldName="DETECT_ITEM_NAME" VisibleIndex="4" Width="120px" />
            <dx:GridViewDataTextColumn Caption="不合格代码" FieldName="ERROR_ITEM_CODE" VisibleIndex="5" Width="120px"  />
            <dx:GridViewDataTextColumn Caption="不合格原因" FieldName="ERROR_ITEM_NAME" VisibleIndex="6" Width="120px" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%" />
        </Columns>
        <Templates>
            <EditForm>
                <dx:ASPxTextBox ID="txtRmesID" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>' Visible="false" />
                
                <table>
                    <tr style="height: 10px">
                        <td colspan="7">
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
                            <dx:ASPxComboBox ID="combPline" runat="server" Width="160px" Value='<%# Bind("PLINE_CODE") %>' DataSourceID="sqlPline">
                                <ClientSideEvents SelectedIndexChanged="function (s,e){comWorkUnit.PerformCallback(s.lastSuccessValue);}" 
                                 Init="function (s,e){comWorkUnit.PerformCallback(s.lastSuccessValue);}"
                                 />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="工作中心">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="comWorkUnit" ClientInstanceName="comWorkUnit" runat="server" Width="160px" Value='<%# Bind("WORKUNIT_CODE") %>'
                                 OnCallback="comWorkUnit_Callback">
                                <ClientSideEvents SelectedIndexChanged="function (s,e){comDetectItem.PerformCallback(s.lastSuccessValue);}" />
                            </dx:ASPxComboBox>
                        </td>
                        
                        <td style="width: 5px">
                        </td>
                    </tr>

                    <tr style="height: 30px">
                        <td style="width: 15px;">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="质检项目">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 165px">
                            <dx:ASPxComboBox ID="comDetectItem" ClientInstanceName="comDetectItem" runat="server" Width="160px" Value='<%# Bind("DETECT_ITEM_CODE") %>' OnCallback="comDetectItem_Callback"
                             ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="质检项目不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            
                        </td>
                        <td>
                           
                        </td>
                        
                        <td style="width: 5px">
                        </td>
                    </tr>

                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="不合格代码" />
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="texErrorDataCode" runat="server" Width="160px" Text='<%# Bind("ERROR_ITEM_CODE") %>'  
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="不合格原因">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="texErrorDataName" runat="server" Width="160px" Text='<%# Bind("ERROR_ITEM_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="不合格原因不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
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
        </Templates>

        <ClientSideEvents 
        BeginCallback="function(s,e) 
        {
	       startCallBack(s,e);
        }" 
        EndCallback="function(s,e) 
        {
            
        }" 
    />
    </dx:ASPxGridView>

    <%--<dx:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ShowHeader="False">

    <ContentCollection>
        <dx:PopupControlContentControl>
            <table width="600px">
                <tr>
                    <td colspan="7"><dx:ASPxTextBox ID="_txtRmesID" ClientInstanceName="_txtRmesID" runat="server" Width="160px"/></td>
                </tr>
                <tr style="height: 30px">
                        <td style="width: 15px;">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 165px">
                            <dx:ASPxComboBox ID="_combPline" ClientInstanceName="_combPline" runat="server" Width="160px"  DataSourceID="sqlPline" ValueField="PLINE_CODE" TextField="PLINE_NAME">
                                <ClientSideEvents SelectedIndexChanged="function (s,e){_comWorkUnit.PerformCallback(s.lastSuccessValue);}" 
                                 />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="工作中心">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="_comWorkUnit" ClientInstanceName="_comWorkUnit" runat="server" Width="160px" 
                                 OnCallback="_comWorkUnit_Callback">
                                <ClientSideEvents SelectedIndexChanged="function (s,e){_comDetectItem.PerformCallback(s.lastSuccessValue);}" />
                            </dx:ASPxComboBox>
                        </td>
                        
                        <td style="width: 5px">
                        </td>
                    </tr>

                    <tr style="height: 30px">
                        <td style="width: 15px;">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="质检项目">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 165px">
                            <dx:ASPxComboBox ID="_comDetectItem" ClientInstanceName="_comDetectItem" runat="server" Width="160px" OnCallback="_comDetectItem_Callback">
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <td>
                            
                        </td>
                        <td>
                           
                        </td>
                        
                        <td style="width: 5px">
                        </td>
                    </tr>

                    <tr style="height: 30px">
                        <td style="width: 15px;">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="不合格代码" />
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="_texErrorDataCode" ClientInstanceName="_texErrorDataCode" runat="server" Width="160px">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="不合格原因">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="_texErrorDataName" ClientInstanceName="_texErrorDataName" runat="server" Width="160px">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>

                    <tr style="height: 30px">
                    <td style="width: 15px;"></td>
                    <td colspan="5">
                        <dx:ASPxLabel ID="ErrorLabel" ClientInstanceName="_errorLabel" runat="server" ForeColor="Red"></dx:ASPxLabel>
                    </td>
                    
                    <td>
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 15px;"></td>
                    <td colspan="2" align="center">
                        <dx:ASPxButton ID="UpdateButton" ReplacementType="EditFormUpdateButton" 
                            Text="提交" AutoPostBack="false"
                            runat="server">
                            <ClientSideEvents Click="function(s,e){submit(s,e);}" />
                           
                        </dx:ASPxButton>
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
</dx:ASPxPopupControl>--%>
    
    <asp:SqlDataSource ID="sqlPline" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>

</asp:Content>
