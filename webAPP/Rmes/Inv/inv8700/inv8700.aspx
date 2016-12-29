<%@ Page Title="盘库" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    CodeBehind="inv8700.aspx.cs" Inherits="Rmes.WebApp.Rmes.Inv.inv8700.inv8700" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" 
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>

<%--盘库 --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            if (confirm("此操作将用当前表格中记录替换系统中原库存，确认进行此操作？")) {
                //此处用的是ClientInstanceName来调用方法，即为ID所设的变量，并不是ID
                ListBoxPanel6.PerformCallback();
            }
            else {
                return;
            }
        }
    </script>

    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel6" ClientInstanceName="ListBoxPanel6"
        OnCallback="ASPxCallbackPanel6_Callback">
        <PanelCollection>
            <dx:PanelContent ID="PanelContent6" runat="server">
                <table style="background-color: #99bbbb; width: 100%">
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <%--<asp:FileUpload runat="server"/>--%>
                        <td style="width: 100px;">
                            <asp:Label ID="Label1" runat="server" Text="打开Txt文件:"></asp:Label>
                        </td>
                        <td style="width: 100px;">
                            <%--<dx:ASPxUploadControl ValidationSettings-AllowedFileExtensions="'.xls','.doc','.jpg'" ID="File2" runat="server" UploadButton-Text="导入" BrowseButton-Text="浏览"></dx:ASPxUploadControl>--%>
                            <input id="File1" type="file" accept="application/txt" size="20" style="font-size: medium; 
                                height: 25px;" alt="请选择Txt文件" runat="server" />
                        </td><%--OnClick="ASPxButton_Import_Click"  OnCallback="ASPxCallbackPanel6_Callback"  --%>
                        <td style="width: 100px">
                            <dx:ASPxButton ID="ASPxButton_Import" runat="server" AutoPostBack="true" Text="导入"
                                Width="100px"  OnClick="ASPxButton_Import_Click"  >
<%--                                <ClientSideEvents Click="function (s,e){
                                                                        Import();
                                                                        
                                                                    }" />--%>
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="ASPxButton_Confirm" runat="server" Text="确认库存" Width="100px">
                                <ClientSideEvents Click="function (s,e){ confirmDelete(); }" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>

    <table>
        <tr>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="60px" ClientInstanceName="PCode">
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxButton ID="queryBill" runat="server" Text="查询">
                    <ClientSideEvents Click="function(s, e){
                        grid.PerformCallback();
                        }"/>
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ROWID" ClientInstanceName="grid"
        AutoGenerateColumns="False"
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared"
        OnRowDeleting="ASPxGridView1_RowDeleting"
        OnRowUpdating="ASPxGridView1_RowUpdating"
        onhtmleditformcreated="ASPxGridView1_HtmlEditFormCreated">
        <Settings ShowHorizontalScrollBar="true" />
        <SettingsEditing PopupEditFormWidth="530px" />
        <SettingsBehavior ColumnResizeMode="Control"/>  
        <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" ExportMode="All" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
                <EditButton Visible="True" Text="修改">
                </EditButton>
                <DeleteButton Visible="True" Text="删除">
                </DeleteButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="ROWID" FieldName="ROWID" VisibleIndex="1" Width="100px" Visible="false"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="100px" Visible="true"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" VisibleIndex="2" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" VisibleIndex="3" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <%--<dx:GridViewDataTextColumn Caption="SUM" FieldName="SUM" VisibleIndex="3" Width="100px" Visible="false"
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="JHDM" VisibleIndex="3" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="入出库时间" FieldName="GZRQ" VisibleIndex="4" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="操作员工" FieldName="YGMC" VisibleIndex="5" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="入出" FieldName="RC" VisibleIndex="6" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="入出库类型" FieldName="RKLX" VisibleIndex="7" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="从" FieldName="SOURCEPLACE" VisibleIndex="8" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="到" FieldName="DESTINATION" VisibleIndex="9" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="批次" FieldName="BATCHID" VisibleIndex="10" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="车号" FieldName="CH" VisibleIndex="10" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="RKDATE" VisibleIndex="10" Width="100px" 
                CellStyle-HorizontalAlign="Center">
            </dx:GridViewDataTextColumn>
        </Columns>

         <Settings ShowFooter="True" />
         <TotalSummary>
            <%--Count计算个数，什么类型都可以；Sum计算和，必须数字才能计算 --%>
            <dx:ASPxSummaryItem FieldName="SO" SummaryType="Count" DisplayFormat="SO={0}"/>
            <dx:ASPxSummaryItem FieldName="GHTM" SummaryType="Count" DisplayFormat="数量={0}"/>
        </TotalSummary>

        <Templates>
            <EditForm>
                <table>
                    <tr style="height: 10px">
                        <td colspan="7">
                        </td>
                        <td style="width: 180px">
                           <dx:ASPxTextBox ID="ROWID" runat="server" Width="160px" Text='<%# Bind("ROWID") %>' Visible="false" Enabled="false" />
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                           <dx:ASPxTextBox ID="txtGzdd" runat="server" Width="160px" Text='<%# Bind("GZDD") %>' Visible="true" Enabled="false" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="SO">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtSO" runat="server" Width="160px" Text='<%# Bind("SO") %>' Enabled="true">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="流水号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtGhtm" runat="server" Width="160px" Text='<%# Bind("GHTM") %>' Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="计划代码">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtJhdm" runat="server" Width="160px" Text='<%# Bind("JHDM") %>' Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="入出库时间" />
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="txtGzrq" runat="server" Width="100%" Value='<%# Bind("GZRQ") %>' EditFormatString="yyyy-MM-dd" Enabled="false">
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="操作员工" />
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtYgmc" runat="server" Width="160px" Text='<%# Bind("YGMC") %>' Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="入出">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtRc" runat="server" Width="160px" Text='<%# Bind("RC") %>' Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="入出库类型">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxComboBox ID="txtRklx" runat="server" Value='<%# Bind("RKLX") %>' Enabled="true">
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="从"  AssociatedControlID="PlineCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="txtSourcePlace" runat="server" Width="160px" Text='<%# Bind("SOURCEPLACE") %>' Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="到"  AssociatedControlID="RountingsiteCombo">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtDestination" runat="server" Width="160px" Text='<%# Bind("DESTINATION") %>' Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="批次"  AssociatedControlID="ASPxComboBoxAcross1">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="txtBatchID" runat="server" Width="160px" Text='<%# Bind("BATCHID") %>' Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="车号">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtCH" runat="server" Width="160px" Text='<%# Bind("CH") %>' Enabled="false" >
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="扫描时间">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtRKDate" runat="server" Width="160px" Text='<%# Bind("RKDATE") %>' Enabled="false" >
                            </dx:ASPxTextBox>
                        </td>
                        <td>
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
                    <tr style="height: 30px">
                        <td colspan="7">
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="fpSpDetail_detail" ClientInstanceName="grid2" runat="server"
                    AutoGenerateColumns="False" Settings-ShowGroupPanel="false" KeyFieldName="GHTM" Settings-ShowFilterRow="false"
                    SettingsPager-Mode="ShowAllRecords" Settings-ShowHeaderFilterButton="false" Settings-ShowVerticalScrollBar="false"
                    Width="100%" OnBeforePerformDataSelect="fpSpDetail_detail_BeforePerformDataSelect">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="SO" FieldName="SO" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="流水号" FieldName="GHTM" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="入出库时间" FieldName="GZRQ" Width="120px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="操作员工" FieldName="YGMC" Width="70px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="入/出" FieldName="RC" Width="50px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="入出库类型" FieldName="RKLX" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="从" FieldName="SOURCEPLACE" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="到" FieldName="DESTINATION" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="批次" FieldName="BATCHID" Width="100px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="车号" FieldName="CH" Width="70px">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="RKDATE" Width="120px">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
    </dx:ASPxGridView>

     <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>

</asp:Content>
