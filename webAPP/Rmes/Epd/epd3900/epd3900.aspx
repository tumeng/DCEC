<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="epd3900.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd3900.epd3900" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Printing.v11.1.Core, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding: 3px 3px 2px 3px">
        <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true"
            ActiveTabIndex="1">
            <TabPages>
                <dx:TabPage Text="跨线模式定义" Visible="true">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" KeyFieldName="ALINE_CODE"
                                SettingsPager-PageSize="13" OnRowValidating="ASPxGridView2_RowValidating" OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated"
                                OnRowDeleting="ASPxGridView2_RowDeleting" OnRowUpdating="ASPxGridView2_RowUpdating"
                                OnRowInserting="ASPxGridView2_RowInserting">
                                <SettingsBehavior ColumnResizeMode="Control" />
                                <SettingsEditing PopupEditFormWidth="650px" />
                                <Settings VerticalScrollableHeight="305"></Settings>
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="  " VisibleIndex="0" Width="100px" Visible="True">
                                        <EditButton Visible="true">
                                        </EditButton>
                                        <NewButton Visible="true">
                                        </NewButton>
                                        <DeleteButton Visible="true">
                                        </DeleteButton>
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="公司代码" FieldName="COMPANY_CODE" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="跨线模式代码" FieldName="ALINE_CODE" VisibleIndex="1"
                                        Width="100px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="跨线模式名称" FieldName="ALINE_NAME" VisibleIndex="2"
                                        Width="150px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" VisibleIndex="3"
                                        Width="150px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="生产线名称 " FieldName="PLINE_NAME" VisibleIndex="4"
                                        Width="80px">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Templates>
                                    <EditForm>
                                        <table>
                                            <tr>
                                                <td style="height: 10px" colspan="7">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px">
                                                </td>
                                                <td style="width: 70px; text-align: left">
                                                    <dx:ASPxLabel ID="Label3" runat="server" Text="生产线" AssociatedControlID="ASPxComboBox1">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 180px;" align="left">
                                                    <dx:ASPxComboBox ID="ASPxComboBox1" EnableClientSideAPI="True" runat="server" DataSourceID="SqlDataSource1"
                                                        ValueType="System.String" Value='<%# Bind("PLINE_ID") %>' Width="150px" TextField="PLINE_NAME"
                                                        ValueField="RMES_ID" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
                                                <td style="width: 90px; text-align: left">
                                                    <dx:ASPxLabel ID="Label1" runat="server" Text="跨线模式代码" AssociatedControlID="txtAlineCode">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 180px;" align="left">
                                                    <dx:ASPxTextBox ID="txtAlineCode" EnableClientSideAPI="True" runat="server" Width="150px"
                                                        Text='<%# Bind("ALINE_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                                            <RegularExpression ErrorText="跨线模式代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                                            <RequiredField IsRequired="True" ErrorText="跨线模式代码不能为空！" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="width: 1px">
                                                </td>
                                                <td style="width: 90px; text-align: left">
                                                    <dx:ASPxLabel ID="Label2" runat="server" Text="跨线模式名称" AssociatedControlID="txtAlineName">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td style="width: 180px;" align="left">
                                                    <dx:ASPxTextBox ID="txtAlineName" EnableClientSideAPI="True" runat="server" Width="150px"
                                                        Text='<%# Bind("ALINE_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                        <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                                            <RegularExpression ErrorText="跨线模式名称字节长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                                            <RequiredField IsRequired="True" ErrorText="跨线模式名称不能为空！" />
                                                        </ValidationSettings>
                                                    </dx:ASPxTextBox>
                                                </td>
                                                <td style="width: 1px">
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
                                    </EditForm>
                                </Templates>
                            </dx:ASPxGridView>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
                                ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="跨线工位工序关系维护" Visible="true">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="新增跨线模式" AutoPostBack="false"
                                            Width="130px">
                                            <ClientSideEvents Click=" function(s,e){ OpenAddWindow();}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
                                        </dx:ASPxGridViewExporter>
                                        <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                                            OnClick="btnXlsExport_Click" />
                                    </td>
                                </tr>
                            </table>
                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
                                SettingsPager-PageSize="13" OnRowDeleting="ASPxGridView1_RowDeleting">
                                <SettingsBehavior ColumnResizeMode="Control" />
                                <Settings VerticalScrollableHeight="305"></Settings>
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Width="50px" Caption=" ">
                                        <EditButton Visible="false">
                                        </EditButton>
                                        <DeleteButton Visible="True">
                                        </DeleteButton>
                                        <ClearFilterButton Visible="true">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
                                    <dx:GridViewDataTextColumn Caption="跨线模式代码" FieldName="ALINE_CODE" VisibleIndex="1"
                                        Width="100px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="跨线模式名称" FieldName="ALINE_NAME" VisibleIndex="2"
                                        Width="100px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="原生产线代码" FieldName="PLINE_CODE_OLD" VisibleIndex="3"
                                        Width="100px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="原生产线名称" FieldName="PLINE_NAME_OLD" VisibleIndex="4"
                                        Width="120px" Settings-AutoFilterCondition="Contains" Visible="false" />
                                    <dx:GridViewDataTextColumn Caption="目标生产线代码" FieldName="PLINE_CODE_NEW" VisibleIndex="7"
                                        Width="100px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="目标生产线名称" FieldName="PLINE_NAME_NEW" VisibleIndex="8"
                                        Width="120px" Settings-AutoFilterCondition="Contains" Visible="false" />
                                    <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" VisibleIndex="10"
                                        Width="100px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="工位名称" FieldName="LOCATION_NAME" VisibleIndex="11"
                                        Width="150px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="工序代码" FieldName="PROCESS_CODE" VisibleIndex="5"
                                        Width="100px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="工序名称" FieldName="PROCESS_NAME" VisibleIndex="6"
                                        Width="180px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption="工位顺序" FieldName="LOCATION_SEQUENCE" VisibleIndex="17"
                                        Width="120px" Settings-AutoFilterCondition="Contains" />
                                    <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
    <script type="text/javascript">
        function OpenAddWindow() {
            window.open('epd3901.aspx', 'addWindow', 'resizable=yes,scrollbars=yes,width=500,height=350,top=150,left=250');
        }
    </script>
</asp:Content>
