<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Rmes_atpu1500" CodeBehind="atpu1500.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%--功能概述：辅料工具周期更换提醒表维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function filterLsh3() {
            //提交
            var ref = "";
            //butSubmit.SetEnabled(false);
            if (confirm("确认要清除该累计数量吗？")) {
            }
            else { return; }


            ref = 'Commit';
            CallbackSubmit.PerformCallback(ref);
            grid.PerformCallback();
        }
    </script>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        KeyFieldName="XH" OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
        OnCustomCallback="ASPxGridView1_CustomCallback" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
        OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback">
        <SettingsEditing PopupEditFormWidth="530px" />
        <SettingsBehavior ColumnResizeMode="Control" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
                <NewButton Visible="True" Text="新增">
                </NewButton>
                <EditButton Visible="True" Text="修改">
                </EditButton>
                <DeleteButton Visible="True" Text="删除">
                </DeleteButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="序号" Name="XH" FieldName="XH" VisibleIndex="2"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="站点代码" Name="LOCATION_CODE" FieldName="LOCATION_CODE"
                VisibleIndex="3" Width="150px" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="站点名称" Name="LOCATION_NAME" FieldName="LOCATION_NAME"
                VisibleIndex="4" Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="量检辅工具" Name="LJJ" FieldName="LJJ" VisibleIndex="5"
                Width="150px" Settings-AutoFilterCondition="Contains" />
            <dx:GridViewDataTextColumn Caption="周期" Name="CYCLE" FieldName="CYCLE" VisibleIndex="6"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="备注" Name="BZ" FieldName="BZ" VisibleIndex="7"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="累计数量" Name="SL" FieldName="SL" VisibleIndex="8"
                Width="100px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn VisibleIndex="10" Width="120px" Caption=" ">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="ButSubmit" Text="累计数量清零">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="function (s,e){ filterLsh3(); }" />
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 30px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="当前记录号" Visible="False">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxTextBox ID="txtXH" runat="server" Width="140px" Text='<%# Bind("XH") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Visible="False">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <td style="width: 90px">
                        </td>
                        <td style="width: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30px">
                        </td>
                        <td style="width: 90px; text-align: left;">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="站点名称">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxComboBox ID="GridLookupCode" runat="server" DataSourceID="SqlCode" Text='<%# Bind("LOCATION_CODE") %>' TextField="LOCATION_NAME"
                                ValueField="LOCATION_NAME" ValueType="System.String" Width="140px" >
                            </dx:ASPxComboBox>
                            <%--  <dx:ASPxGridLookup ID="GridLookupCode" runat="server" Text='<%# Bind("LOCATION_CODE") %>'
                                ClientInstanceName="GridLookupCode" DataSourceID="SqlCode" KeyFieldName="LOCATION_NAME"
                                MultiTextSeparator="," SelectionMode="Single" TextFormatString="{1}" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                    <dx:GridViewDataColumn FieldName="LOCATION_NAME" Visible="false" />
                                    
                                    <dx:GridViewDataColumn Caption="站点名称" FieldName="LOCATION_NAME" />
                                </Columns>
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="站点信息有误，请重新输入！"
                                    SetFocusOnError="True" ValidateOnLeave="True">
                                    <RequiredField ErrorText="站点不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxGridLookup>--%>
                        </td>
                        <td style="width: 30px">
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="量检辅工具">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtLJJ" runat="server" Text='<%# Bind("LJJ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="150px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px">
                        </td>
                        <tr>
                            <td style="height: 30px">
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="周期">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 140px">
                                <dx:ASPxTextBox ID="txtCYCLE" runat="server" Text='<%# Bind("CYCLE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Width="140px">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                        ValidateOnLeave="True">
                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                        <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                        <RegularExpression ErrorText="必须输入整数！" ValidationExpression="^\+?[0-9][0-9]*$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 1px">
                            </td>
                            <td style="width: 120px">
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="备注">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 180px">
                                <dx:ASPxTextBox ID="txtBZ" runat="server" Text='<%# Bind("BZ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Width="150px">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                        ValidateOnLeave="True">
                                        <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                        <RequiredField ErrorText="不能为空！" IsRequired="False" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width: 1px">
                            </td>
                        </tr>
                    <tr>
                        <td colspan="6" style="height: 30px; text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="height: 30px">
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
        <%--<ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
            grid.PerformCallback();
             
        }" />--%>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
    </dx:ASPxCallback>
</asp:Content>
