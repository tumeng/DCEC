<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" StylesheetTheme="Theme1"
    AutoEventWireup="true" CodeBehind="atpu2000.aspx.cs" Inherits="Rmes_atpu2000" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--功能概述：喷漆类型维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (Pcode.GetValue() == null && Pcode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?SO=" + Pcode.GetValue() + "&opFlag=getEditSeries";

            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");

            result = xmlHttp.responseText;
            var result1 = "";
            var retStr1 = "";
            var array1 = result.split(',');
            retStr1 = array1[1];
            result1 = array1[0];
            if (result1 == "") {
                alert("机型不存在，请检查数据！");
                Pcode.SetFocus();
                listAcross.SetValue("");
                return;
            }
            listAcross.SetValue(result1);
        }
    </script>
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true"
        ActiveTabIndex="0">
        <TabPages>
            <dx:TabPage Text="机型修改" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="SO" OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
                            OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
                            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = &#39;&#39;;}"
                                EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == &#39;Delete&#39;) 
            {
                alert(theRet);
            }
        }"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                    <EditButton Visible="True">
                                    </EditButton>
                                    <NewButton Visible="True">
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="SO号" FieldName="SO" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="喷漆程序" FieldName="TYPE_PQBAK" VisibleIndex="2"
                                    Settings-AutoFilterCondition="Contains" Width="100px" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" VisibleIndex="3" Width="100px"
                                    Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="机型" FieldName="JX" VisibleIndex="4" Width="100px"
                                    Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter"></SettingsEditing>
                            <Templates>
                                <EditForm>
                                    <table>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SO号">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxGridLookup ID="txtSO" runat="server" Value='<%# Bind("SO") %>' KeyFieldName="SO"
                                                    DropDownStyle="DropDownList" DataSourceID="SqlSO" ValueType="System.String"
                                                    Width="140px" ClientInstanceName="Pcode" 
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                                    oninit="txtSO_Init">
                                                    <GridViewProperties>
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                        <SettingsPager NumericButtonCount="5">
                                                        </SettingsPager>
                                                    </GridViewProperties>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                                        <dx:GridViewDataColumn Caption="SO号" FieldName="SO" />
                                                    </Columns>
                                                    <ClientSideEvents TextChanged="function(s,e){ initEditSeries(s,e);  }" />
                                                </dx:ASPxGridLookup>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="机型">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtJX" ClientInstanceName="listAcross" runat="server" Text='<%# Bind("JX") %>'
                                                    ClientEnabled="false" Width="150px" AutoPostBack="false">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="喷漆程序">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="txtPQBAK" runat="server" Width="140px" Text='<%# Bind("TYPE_PQBAK") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="备注">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtREMARK" runat="server" Width="150px" Text='<%# Bind("REMARK") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="False" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px; text-align: right;" colspan="6">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" colspan="7">
                                            </td>
                                        </tr>
                                    </table>
                                </EditForm>
                            </Templates>
                            <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="新机型修改" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="SO" OnRowInserting="ASPxGridView2_RowInserting" OnRowDeleting="ASPxGridView2_RowDeleting"
                            OnRowUpdating="ASPxGridView2_RowUpdating" OnRowValidating="ASPxGridView2_RowValidating"
                            OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated">
                            <%--<SettingsBehavior AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True"></SettingsBehavior>--%>
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter"></SettingsEditing>
                            <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
                            <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = &#39;&#39;;}"
                                EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == &#39;Delete&#39;) 
            {
                alert(theRet);
            }
        }"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                    <EditButton Visible="True">
                                    </EditButton>
                                    <NewButton Visible="True">
                                    </NewButton>
                                    <DeleteButton Visible="True">
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="SO号" FieldName="SO" VisibleIndex="1" Width="120px"
                                    Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="喷漆程序" FieldName="TYPE_PQBAK" VisibleIndex="2"
                                    Settings-AutoFilterCondition="Contains" Width="100px" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" VisibleIndex="3" Width="100px"
                                    Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="机型" FieldName="JX" VisibleIndex="4" Width="100px"
                                    Settings-AutoFilterCondition="Contains" >
<Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Templates>
                                <EditForm>
                                    <table>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SO号">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxGridLookup ID="txtSO2" runat="server" ClientInstanceName="Pcode2" SelectionMode="Single"
                                                    KeyFieldName="SO" Value='<%# Bind("SO") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                                    Width="140px" DataSourceID="SqlSO2" oninit="txtSO2_Init">
                                                    <GridViewProperties>
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                        <SettingsPager NumericButtonCount="5">
                                                        </SettingsPager>
                                                    </GridViewProperties>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                                        <dx:GridViewDataColumn Caption="SO号" FieldName="SO" />
                                                    </Columns>
                                                    <ClientSideEvents TextChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                                                </dx:ASPxGridLookup>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="机型">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtJX" ClientInstanceName="listAcross" runat="server" Text='<%# Bind("JX") %>'
                                                    ClientEnabled="false" Width="150px" AutoPostBack="false">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="喷漆程序">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="txtPQBAK" runat="server" Width="140px" Text='<%# Bind("TYPE_PQBAK") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="备注">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtREMARK" runat="server" Width="150px" Text='<%# Bind("REMARK") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="False" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px; text-align: right;" colspan="6">
                                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                                    runat="server"></dx:ASPxGridViewTemplateReplacement>
                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" colspan="7">
                                            </td>
                                        </tr>
                                    </table>
                                </EditForm>
                            </Templates>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <asp:SqlDataSource ID="SqlSO" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlSO2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
