<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="inv9100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv9100.inv9100" %>

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
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%--不回冲零件设定--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function getOther() {
            if (Pcode.GetValue() == null) {
                return;
            }
            //            Part.SetValue("");
            //            Part.SetText("");
            //            Part.ClearGroup(null);
            var pcode = Pcode.GetValue();
            var qad = QAD.GetValue();

            //            CallbackPanel4.PerformCallback(pcode + "," + qad); CallbackPanel3.PerformCallback(pcode + "," + qad);
        }
        function initEditSeries(s, e) {
            if (Pcode.GetValue() == null) {
                return;
            }
            var webFileUrl = "?PCode=" + Pcode.GetValue() + " &QAD=" + QAD.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            grid.PerformCallback();
        }
        function initEditSeries2(s, e) {
            if (Part.GetValue() == null) {
                return;
            }
            var webFileUrl = "?PART=" + Part.GetValue() + " &QAD=" + QAD.GetValue() + "&opFlag=getEditSeries2";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            if (result == "") {

                alert("请先选择零件号码！");
                Part.SetFocus();
                PName.SetValue("");
            }

            PName.SetValue(result);

        }
    </script>
    <table>
        <tr>
            <td style="width: 800px">
            </td>
            <td style="width: 40px">
                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="生产线" Width="40px" Height="17px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPlineCode" DataSourceID="SqlCode" ValueField="pline_code"
                    TextField="pline_name" runat="server" Width="80px" SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td style="width: 43px">
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="时间:" Width="43px" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="100px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="--" />
            </td>
            <td>
                <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="100px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
            </td>
            <td style="width: 45px">
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="零件号:" Width="45px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="TextPart" runat="server" ValueField="PART" ValueType="System.String"
                    Width="80px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td style="width: 80px">
                <dx:ASPxButton ID="btnQuery" runat="server" Text="删除查询" AutoPostBack="False" Width="80px">
                    <ClientSideEvents Click="function(s,e){
                        grid2.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="width: 800px">
                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="PART"
                    AutoGenerateColumns="False" OnRowInserting="ASPxGridView1_RowInserting" OnRowDeleting="ASPxGridView1_RowDeleting"
                    OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
                    OnCustomCallback="ASPxGridView1_CustomCallback">
                    <SettingsEditing PopupEditFormWidth="550" PopupEditFormHeight="200" />
                    <Columns>
                        <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="70px">
                            <NewButton Visible="True" />
                            <DeleteButton Visible="True" />
                            <ClearFilterButton Visible="True" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="零件" FieldName="PART" VisibleIndex="1" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="零件名称" FieldName="PART_DESC" VisibleIndex="1"
                            Width="130px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="类型" FieldName="PART_TYPE" VisibleIndex="2" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="加入员工" FieldName="EDIT_NAME" VisibleIndex="3"
                            Width="75px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="加入日期" FieldName="EDIT_DATE" VisibleIndex="4"
                            Width="130px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="保管员" FieldName="BGY" VisibleIndex="6" Width="60px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="6" Width="80px"
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

                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px; height: 30px">
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 170px">
                                        <dx:ASPxComboBox ID="txtPCode" runat="server" DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                            Value='<%# Bind("GZDD") %>' Width="120px" ClientInstanceName="Pcode" SelectedIndex="0">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                ValidateOnLeave="True">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                            </ValidationSettings>
                                            <ClientSideEvents SelectedIndexChanged="function(s,e){  initEditSeries(s,e); }" />
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width: 1px">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="保管员">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxGridLookup ID="txtBgy" runat="server" Height="19px" KeyFieldName="BGY" DataSourceID="SqlBgy"
                                            Value='<%# Bind("BGY") %>' Width="120px" OnInit="txtBgy_Init" AllowMouseWheel="true"
                                            AllowUserInput="true" IncrementalFilteringMode="Contains" SelectionMode="Single">
                                            <GridViewProperties>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                <SettingsPager NumericButtonCount="5">
                                                </SettingsPager>
                                            </GridViewProperties>
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                                <dx:GridViewDataColumn Caption="保管员" FieldName="BGY" />
                                            </Columns>
                                        </dx:ASPxGridLookup>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: left">
                                        <dx:ASPxCheckBox ID="chkQAD" ClientInstanceName="QAD" runat="server" Visible="true"
                                            Text="非QAD零件" Value='<%# Bind("PART_TYPE") %>'>
                                            <ClientSideEvents CheckedChanged="function(s,e){ initEditSeries(s,e);}" />
                                        </dx:ASPxCheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 8px;">
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="零件号">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 90px">
                                        <dx:ASPxGridLookup ID="txtPart" runat="server" ClientInstanceName="Part" DataSourceID="SqlPart"
                                            KeyFieldName="PART" Width="120px" Value='<%# Bind("PART") %>' OnInit="txtPart_Init"
                                            TextFormatString="{0}" AllowMouseWheel="true" AllowUserInput="true" IncrementalFilteringMode="Contains"
                                            SelectionMode="Single">
                                            <GridViewProperties>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                                                <SettingsPager NumericButtonCount="5">
                                                </SettingsPager>
                                            </GridViewProperties>
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" Caption=" " />
                                                <dx:GridViewDataColumn Caption="零件号" FieldName="PART" />
                                            </Columns>
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                ValidateOnLeave="True">
                                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                            </ValidationSettings>
                                            <ClientSideEvents TextChanged="function(s,e){ initEditSeries2(s,e);  }" />
                                        </dx:ASPxGridLookup>
                                    </td>
                                    <td style="width: 1px;">
                                        &nbsp;
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="零件名称">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="width: 170px;">
                                        <dx:ASPxTextBox ID="txtPname" ClientInstanceName="PName" runat="server" Text='<%# Bind("PART_DESC") %>'
                                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="120px"
                                            ClientEnabled="false">
                                            <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                ValidateOnLeave="True">
                                                <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                <RequiredField ErrorText="不能为空！" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
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
            </td>
            <td style="width: 450px">
                <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" KeyFieldName="PART"
                    Width="600px" OnCustomCallback="ASPxGridView2_CustomCallback">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="零件" FieldName="PART" VisibleIndex="1" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="零件名称" FieldName="PART_DESC" VisibleIndex="1"
                            Width="100px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="类型" FieldName="PART_TYPE" VisibleIndex="2" Width="80px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="加入员工" FieldName="EDIT_NAME" VisibleIndex="3"
                            Width="75px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="加入日期" FieldName="EDIT_DATE" VisibleIndex="4"
                            Width="130px" Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="删除日期" FieldName="DEL_DATE" VisibleIndex="5" Width="130px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="保管员" FieldName="BGY" VisibleIndex="6" Width="60px"
                            Settings-AutoFilterCondition="Contains">
                            <Settings AutoFilterCondition="Contains"></Settings>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlBgy" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlPart" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server">
    </dx:ASPxGridViewExporter>
</asp:Content>
