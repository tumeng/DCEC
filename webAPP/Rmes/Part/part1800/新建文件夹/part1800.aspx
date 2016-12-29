<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Part_part1800_part1800"
    StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" CodeBehind="part1800.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--最小包装剩余维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (MCode.GetValue() == null || Pcode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?MCODE=" + MCode.GetValue() + " &Pcode=" + Pcode.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var mcode = "";
            var pcode = "";
            var array1 = result.split(',');
            mcode = array1[0];
            pcode = array1[1];
            if (mcode == "") {
                alert("该零件不存在，请检查数据！");
                MCode.SetFocus();
                MName.SetValue("");
                return;
            }
            if (pcode == "") {
                alert("生产线请选择E或W");
                Pcode.SetFocus();
                Qsite.SetValue("");
                return;
            }
            MName.SetValue(mcode);
            Qsite.SetValue(pcode);

        }
        function Clear() {
            var ref = "";


            if (confirm("确实要全部清零吗？")) {
            }
            else {
                alert("清零操作已取消！");

                return;
            }

            ref = 'Clear';
            CallbackSubmit.PerformCallback(ref);

        }
        function submitRtr(e) {
            var result = "";
            var retStr = "";
            var array = e.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {

                case "Fail":
                    alert(retStr);
                    grid.PerformCallback();
                    return;
                case "OK":
                    alert(retStr);
                    grid.PerformCallback();
                    return;
            }


        }
    </script>
    <table>
        <tr>
            <td>
            </td>
            <td style="width: 90px">
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线">
                </dx:ASPxLabel>
            </td>
            <td style="width: 170px">
                <dx:ASPxComboBox ID="combPcode" runat="server" DataSourceID="sqlPCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="120px" SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnClear" runat="server" Text="置所有数量为零" AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e) { Clear(); }" />
                </dx:ASPxButton>
            </td>
            <td>
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" Width="140px" />
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="MATERIAL_CODE"
        AutoGenerateColumns="False" OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
        OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
        OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomCallback="ASPxGridView1_CustomCallback">
        <SettingsEditing PopupEditFormWidth="550px" PopupEditFormHeight="200px" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                <EditButton Visible="True" />
                <NewButton Visible="True" />
                <DeleteButton Visible="True" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="物料地点" FieldName="QADSITE" VisibleIndex="1" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" VisibleIndex="2"
                Width="120px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="PT_DESC2" VisibleIndex="3" Width="160px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数量" FieldName="MATERIAL_NUM" VisibleIndex="4"
                Width="60px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="4" Width="60px"
                Visible="false" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td style="height: 10px" colspan="6">
                            请先选择生产线和物料代码
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxComboBox ID="txtPCode" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("GZDD") %>' Width="140px" ClientInstanceName="Pcode">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="物料地点" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxComboBox ID="txtQsite" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Value='<%# Bind("QADSITE") %>' Width="140px" ClientInstanceName="Qsite">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 1px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="物料代码" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px">
                            <dx:ASPxGridLookup ID="txtMCode" runat="server" ClientInstanceName="MCode" SelectionMode="Single"
                                KeyFieldName="MATERIAL_CODE" Value='<%# Bind("MATERIAL_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px" DataSourceID="SqlMcode">
                                <GridViewProperties>
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                    <SettingsPager NumericButtonCount="5">
                                    </SettingsPager>
                                </GridViewProperties>
                                <Columns>
                                    <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                    <dx:GridViewDataColumn Caption="物料代码" FieldName="MATERIAL_CODE" />
                                </Columns>
                                <ClientSideEvents TextChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxGridLookup>
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 80px;">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="物料名称" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px;">
                            <dx:ASPxTextBox ID="txtMName" runat="server" ClientInstanceName="MName" Height="19px"
                                Value='<%# Bind("PT_DESC2") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px" ClientEnabled="false">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过200！" ValidationExpression="^.{0,200}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="width: 90px;">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="物料数量" Width="80px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 140px;">
                            <dx:ASPxTextBox ID="txtPNum" runat="server" Text='<%# Bind("MATERIAL_NUM") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                Width="140px">
                                <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                    ValidateOnLeave="True">
                                    <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                    <RequiredField ErrorText="不能为空！" IsRequired="False" />
                                    <RegularExpression ErrorText="物料数量必须为整数！" ValidationExpression="^\+?[1-9][0-9]*$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 1px;">
                            &nbsp;
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
                </table>
            </EditForm>
        </Templates>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sqlPlanCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlMcode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
        OnCallback="ASPxCbSubmit_Callback">
        <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
    </dx:ASPxCallback>
</asp:Content>
