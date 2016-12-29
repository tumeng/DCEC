<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="atpu1D00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Atpu.atpu1D00.atpu1D00" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="1">
        <TabPages>
            <dx:TabPage Text="总成零件属性维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            Width="100%" KeyFieldName="ABOM_COMP" OnRowInserting="ASPxGridView1_RowInserting"
                            OnRowDeleting="ASPxGridView1_RowDeleting" OnRowUpdating="ASPxGridView1_RowUpdating"
                            OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="True" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="零件代码" FieldName="ABOM_COMP" VisibleIndex="1"
                                    Settings-AutoFilterCondition="Contains" Width="150px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="PT_DESC2" VisibleIndex="2" Settings-AutoFilterCondition="Contains"
                                    Width="150px" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生效日期" FieldName="RQBEGIN" VisibleIndex="3" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="失效日期" FieldName="RQEND" VisibleIndex="4" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="维护日期" FieldName="ZDRQ" VisibleIndex="5" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="是否显示总成" FieldName="SFXS" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                                    Width="100px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="是否作为总成回冲" FieldName="SFHC" VisibleIndex="8" Settings-AutoFilterCondition="Contains"
                                    Width="150px" CellStyle-HorizontalAlign="Center">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="是否回冲总成" FieldName="SFZC" VisibleIndex="9" Settings-AutoFilterCondition="Contains"
                                    Width="150px" CellStyle-HorizontalAlign="Center">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
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
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="零件代码">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="comboLJDM" runat="server" ClientInstanceName="ljdm_C" Width="150px"
                                                    ValueField="PT_PART" TextField="PT_PART" Value='<%# Bind("ABOM_COMP") %>' DataSourceID="SqlDataSource3"
                                                    DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过20！" ValidationExpression="^.{0,20}$" />
                                                        <RequiredField IsRequired="True" ErrorText="零件代码不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <asp:Label ID="Label7" runat="server" Text="是否有效"></asp:Label>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxCheckBox ID="chValidFlag" runat="server">
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 90px">
                                                <asp:Label ID="Label3" runat="server" Text="是否显示总成"></asp:Label>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxCheckBox ID="chZCFlag" runat="server">
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <%--<td style="width: 1px">
                                            </td>
                                            <td style="width: 80px">
                                                <asp:Label ID="Label4" runat="server" Text="是否回冲总成"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <dx:ASPxCheckBox ID="chHCZCFlag" runat="server">
                                                </dx:ASPxCheckBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 100px">
                                                <asp:Label ID="Label2" runat="server" Text="是否作为总成回冲"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <dx:ASPxCheckBox ID="chHCFlag" runat="server">
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 80px">
                                                <asp:Label ID="Label4" runat="server" Text="是否回冲总成"></asp:Label>
                                            </td>
                                            <td style="width: 100px">
                                                <dx:ASPxCheckBox ID="chHCZCFlag" runat="server">
                                                </dx:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生效日期">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxDateEdit ID="dateStart" runat="server" EditFormatString="yyyy-MM-dd" Date='<%# theBeginDate %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="150px">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" ErrorText="生效日期不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 90px">
                                                <asp:Label ID="Label1" runat="server" Text="失效日期"></asp:Label>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxDateEdit ID="dateEnd" runat="server" EditFormatString="yyyy-MM-dd" Date='<%# theEndDate %>'
                                                    Width="150px">
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
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
            <dx:TabPage Text="分装总成维护" Visible="true">
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
                            KeyFieldName="ZCDM" 
                            OnRowInserting="ASPxGridView2_RowInserting"
                            OnRowDeleting="ASPxGridView2_RowDeleting"
                            OnRowValidating="ASPxGridView2_RowValidating" >
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <%--<EditButton Visible="True" />--%>
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="总成零件代码" FieldName="ZCDM" VisibleIndex="1"
                                    Width="120px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="SCXDM" VisibleIndex="2" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="发动机系列" FieldName="FDJXL" VisibleIndex="3" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsEditing PopupEditFormWidth="330px" PopupEditFormHorizontalAlign="WindowCenter"
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
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="总成零件代码">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="txtZCDM" runat="server" Width="140px" Text='<%# Bind("ZCDM") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="总成零件代码不存在，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="生产线代码">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="comboSCXDM" runat="server" Width="140px"
                                                    ValueField="SCXDM" TextField="SCXDM" Value='<%# Bind("SCXDM") %>' 
                                                    DropDownStyle="DropDownList" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                    <Items>
                                                        <dx:ListEditItem Text="CL" Value="CL" />
                                                        <dx:ListEditItem Text="ISDE_E" Value="ISDE_E" />
                                                    </Items>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="发动机系列">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="txtFDJXL" runat="server" Width="140px" Text='<%# Bind("FDJXL") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" 
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" colspan="7">
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
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
   
</asp:Content>
