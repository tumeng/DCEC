<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="atpu1A00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Atpu.atpu1A00.atpu1A00" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
    <%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function initEditLJDM(s, e) {

            var webFileUrl = "?ljdmC=" + ljdm_C.GetValue() + "&opFlag=getEditLJDM";

            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");

            result = xmlHttp.responseText;

            if (result == "") {
                alert("零件代码不存在，请检查数据！");
                ljdm_C.SetFocus();
                ljmc_C.SetValue("");
                return;
            }
            ljmc_C.SetValue(result);
        }
    </script>
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="1">
        <TabPages>
            <dx:TabPage Text="按零件号维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                        OnClick="btnXlsExport_Click" />
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="XH" OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting"
                            OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
                            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnStartRowEditing="ASPxGridView1_StartRowEditing"
                            OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize">
                            <SettingsEditing PopupEditFormWidth="600px" PopupEditFormHeight="200px" />
                            <SettingsBehavior ColumnResizeMode="Control" />
                            <%--<ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = &#39;&#39;;}"
                                EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == &#39;Delete&#39;) 
            {
                alert(theRet);
            }
        }"></ClientSideEvents>--%>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
                                    <NewButton Visible="True" Text="新增">
                                    </NewButton>
                                    <%--<EditButton Visible="true" Text="修改"></EditButton>--%>
                                    <DeleteButton Visible="True" Text="删除">
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="重要零件种类" Name="ZYLJLB" FieldName="ZYLJLB" VisibleIndex="2"
                                    Width="120px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="序号" Name="XH" FieldName="XH" VisibleIndex="3"
                                    Width="80px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件代码" Name="LJDM" FieldName="LJDM" VisibleIndex="4"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件名称" Name="LJMC" FieldName="LJMC" VisibleIndex="5"
                                    Width="200px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="6"
                                    Width="150px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生产线" Name="PLINE_NAME" FieldName="PLINE_NAME"
                                    VisibleIndex="6" Width="150px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生产线" Name="GZDD" FieldName="GZDD" VisibleIndex="6"
                                    Width="150px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零部件重要级别" Name="LJLB" FieldName="LJLB" VisibleIndex="7"
                                    Width="100px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%" />
                            </Columns>
                            <Templates>
                                <EditForm>
                                    <table>
                                        <br />
                                        <tr>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 120px; text-align: left;">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="重要零件种类" />
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxComboBox ID="comboLJZL" EnableClientSideAPI="True" runat="server" ClientInstanceName="LJZL_C"
                                                    Value='<%# theZYLJZL %>' DropDownStyle="DropDownList" ClientEnabled="false" DataSourceID="SqlLJZL"
                                                    ValueField="INTERNAL_CODE" TextField="INTERNAL_NAME" SelectedIndex="0" ValueType="System.String"
                                                    Width="140px" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" ErrorText="重要零件种类不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 120px">
                                            </td>
                                            <td style="width: 180px">
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 120px; text-align: left;">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <%--                        <dx:ASPxComboBox ID="comboPCode" runat="server"  ClientInstanceName="pcode_C" Width="140px" Value='<%# Bind("PLINE_CODE") %>' DataSourceID="sqlPline" ValueField="PLINE_CODE" TextField="PLINE_NAME"
                            DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过10！" ValidationExpression="^.{0,10}$" />
                                <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>--%>
                                                <dx:ASPxComboBox ID="comboPCode" runat="server" Width="140px" Value='<%# Bind("PLINE_CODE") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" DropDownStyle="DropDownList">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 120px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="零件代码">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <%--                        <dx:ASPxComboBox ID="comboLJDM" runat="server" ClientInstanceName="ljdm_C" Width="140px" ValueField="PT_PART" TextField="PT_PART" Value='<%# Bind("LJDM") %>'
                            DataSourceID="SqlDataSource1" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                <RequiredField IsRequired="True" ErrorText="零件代码不能为空！" />
                            </ValidationSettings>
                            <ClientSideEvents SelectedIndexChanged="function(s,e){
                                initEditLJDM(s,e);
                            }" />
                        </dx:ASPxComboBox>--%>
                                                <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" ClientInstanceName="ljdm_C" Text='<%# Bind("LJDM") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="140px">
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                        ValidateOnLeave="True">
                                                        <RequiredField ErrorText="零件代码不能为空！" IsRequired="True" />
                                                    </ValidationSettings>
                                                    <ClientSideEvents TextChanged="function(s,e){
                                initEditLJDM(s,e);
                            }" />
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px">
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="零件名称">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtLJMC" runat="server" ClientInstanceName="ljmc_C" Text='<%# Bind("LJMC") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="140px">
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                        ValidateOnLeave="True">
                                                        <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                                        <RequiredField ErrorText="零件名称不能为空！" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 120px">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="重要级别" Visible="false">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxComboBox ID="comboLJJB" runat="server" Width="140px" Value='<%# Bind("LJLB") %>'
                                                    ValueField="INTERNAL_CODE" TextField="INTERNAL_NAME" SelectedIndex="0" Visible="false"
                                                    DataSourceID="SqlLJJB" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过2！" ValidationExpression="^.{0,2}$" />
                                                        <%--<RequiredField IsRequired="True" ErrorText="零部件重要级别不能为空！" />--%>
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
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
                            <ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                confirm('确认要删除这条记录吗？');
                alert(theRet);
            }
             
        }" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="按零件名称维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                        OnClick="btnXlsExport2_Click" />
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="ASPxGridView2">
                        </dx:ASPxGridViewExporter>
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="RMES_ID" OnRowDeleting="ASPxGridView2_RowDeleting" OnRowInserting="ASPxGridView2_RowInserting"
                            OnRowValidating="ASPxGridView2_RowValidating" OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormWidth="600px" PopupEditFormHeight="120px" />
                            <SettingsBehavior ColumnResizeMode="Control" />
                            <%--<ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = &#39;&#39;;}"
                                EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == &#39;Delete&#39;) 
            {
                alert(theRet);
            }
        }"></ClientSideEvents>--%>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
                                    <NewButton Visible="True" Text="新增">
                                    </NewButton>
                                    <%--<EditButton Visible="true" Text="修改"></EditButton>--%>
                                    <DeleteButton Visible="True" Text="删除">
                                    </DeleteButton>
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="RMES_ID" Name="RMES_ID" FieldName="RMES_ID" VisibleIndex="0"
                                    Width="200px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="重要零件名称" Name="LJMC" FieldName="LJMC" VisibleIndex="2"
                                    Width="200px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1"
                                    Width="150px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%" />
                            </Columns>
                            <Templates>
                                <EditForm>
                                    <table>
                                        <br />
                                        <tr>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 100px; text-align: left;">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxComboBox ID="txtPCode" runat="server" Width="120px" Value='<%# Bind("PLINE_CODE") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" DropDownStyle="DropDownList"
                                                    SelectedIndex="0">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 120px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="重要零件名称">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 200px">
                                                <dx:ASPxTextBox ID="txtName" runat="server" Text='<%# Bind("LJMC") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                                    Width="200px">
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                        ValidateOnLeave="True">
                                                        <RequiredField ErrorText="零件名称不能为空！" IsRequired="True" />
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
                            <ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid2.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid2.cpCallbackName;
            theRet = grid2.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                confirm('确认要删除这条记录吗？');
                alert(theRet);
            }
             
        }" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <asp:SqlDataSource ID="SqlLJZL" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlLJJB" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPline" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
