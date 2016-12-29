<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ssd8000.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd8000.ssd8000" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function OnMoreInfoClick(element, key) {
            callbackPanel.SetContentHtml("");
            popup11.ShowAtElement(element);
            keyValue = key;
        }
        function popup_Shown(s, e) {
            callbackPanel.PerformCallback(keyValue);
        }

    </script>

    <dx:ASPxPopupControl ID="popup11" ClientInstanceName="popup11" runat="server" AllowDragging="true"
        PopupHorizontalAlign="OutsideRight" HeaderText="备注">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server"
                    Width="320px" Height="120px" RenderMode="Table" 
                            OnCallback="callbackPanel_Callback">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent11" runat="server">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <dx:ASPxMemo ID="litText" runat="server" Text="" Width="100%" Border-BorderStyle="None" ReadOnly="true" Height="120px">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents Shown="popup_Shown" />
    </dx:ASPxPopupControl>

    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="btnXlsExportPlan" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False" OnClick="btnXlsExport_Click1">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table style="background-color: #99bbbb; width: 100%;">
        <tr>
            <td style="width: 5px; height: 25px;"></td>
            <td style="text-align: left; width: 70px;">
                <label style="font-size: small">
                    开始日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" EditFormatString="yyyy-MM-dd"
                    Width="120px">
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 5px"></td>
            <td style="text-align: left; width: 70px">
                <label style="font-size: small">
                    结束日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" EditFormatString="yyyy-MM-dd"
                    Width="120px">
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 100px;">
                <dx:ASPxButton ID="queryPlan" Text="查询计划" Width="90px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s, e){
                        grid.PerformCallback();
                        }"/>
                </dx:ASPxButton>
            </td>
            <td colspan="3" style="width: auto"></td>
        </tr>
    </table>

    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID">

        <Settings ShowHorizontalScrollBar="true" />
        <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" ExportMode="All" />
        <SettingsEditing PopupEditFormWidth="600px" />

        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="清除" Width="40px" FixedStyle="Left">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="RMES_ID" FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn Caption="COMPANY_CODE" FieldName="COMPANY_CODE" Visible="false" />

            <%--<dx:GridViewDataComboBoxColumn Caption="生产线代码" VisibleIndex="2" Width="100px" FieldName="PLINE_CODE" Settings-AutoFilterCondition="Contains">
                <PropertiesComboBox DataSourceID="boxPline" ValueField="PLINE_CODE" TextField="PLINE_CODE"
                    IncrementalFilteringMode="StartsWith" ValueType="System.String" />
            </dx:GridViewDataComboBoxColumn>--%>

            <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" VisibleIndex="2" Width="100px" />

            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="3" Width="100px" />

            <dx:GridViewDataTextColumn Caption="计划序" FieldName="PLAN_SEQ" VisibleIndex="0" Width="80px" />

            <%--<dx:GridViewDataComboBoxColumn Caption="计划代码" VisibleIndex="1" Width="120px" FieldName="PLAN_CODE" Settings-AutoFilterCondition="Contains">
                <PropertiesComboBox DataSourceID="boxPlan" ValueField="PLAN_CODE" TextField="PLAN_CODE"
                    IncrementalFilteringMode="StartsWith" ValueType="System.String" />
            </dx:GridViewDataComboBoxColumn>--%>

            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" VisibleIndex="1" Width="120px" />

            <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" Width="120px">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="机型" FieldName="PRODUCT_MODEL" Width="120px">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="系列" FieldName="PRODUCT_SERIES" Width="120px" CellStyle-HorizontalAlign="Center" Visible="false" />

            <dx:GridViewDataTextColumn Caption="批次号" FieldName="PLAN_BATCH" Width="80px" Visible="false" />

            <dx:GridViewDataTextColumn Caption="计划类型" FieldName="PLAN_TYPE" Width="80px" />

            <dx:GridViewDataTextColumn Caption="用户代码" FieldName="USER_CODE" Width="80px" CellStyle-HorizontalAlign="Center" Settings-AllowHeaderFilter="True" />

            <dx:GridViewDataTextColumn Caption="用户姓名" FieldName="CREATE_USERNAME" Width="80px" CellStyle-HorizontalAlign="Center" Settings-AllowHeaderFilter="True" />

            <dx:GridViewDataTextColumn Caption="计划制定日期" FieldName="CREATE_TIME" Width="130px" CellStyle-Wrap="False" />
            <dx:GridViewDataDateColumn Caption="计划开始日期" FieldName="BEGIN_DATE" Width="130px" CellStyle-Wrap="False" />
            <dx:GridViewDataDateColumn Caption="计划结束日期" FieldName="END_DATE" Width="130px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="实际开工时间" FieldName="BEGIN_TIME" Width="130px" CellStyle-Wrap="False" />
            <dx:GridViewDataTextColumn Caption="实际完工时间" FieldName="END_TIME" Width="130px" CellStyle-Wrap="False" />

            <dx:GridViewDataDateColumn Caption="记账日期" FieldName="ACCOUNT_DATE" Width="130px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="计划数量" FieldName="PLAN_QTY" Width="80px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="计划上线数量" FieldName="ONLINE_QTY" Width="100px" CellStyle-HorizontalAlign="Center" />
            <dx:GridViewDataTextColumn Caption="计划下线数量" FieldName="OFFLINE_QTY" Width="100px" CellStyle-HorizontalAlign="Center" />

            <dx:GridViewDataTextColumn Caption="计划确认标识" FieldName="CONFIRM_FLAG" Width="100px" CellStyle-HorizontalAlign="Center" />
            <dx:GridViewDataTextColumn Caption="BOM转换" FieldName="BOM_FLAG" Width="100px" CellStyle-HorizontalAlign="Center" />
            <dx:GridViewDataTextColumn Caption="库房确认标识" FieldName="ITEM_FLAG" Width="100px" CellStyle-HorizontalAlign="Center" />
            <dx:GridViewDataTextColumn Caption="是否分配流水号" FieldName="SN_FLAG" Width="100px" CellStyle-HorizontalAlign="Center" />
            <dx:GridViewDataTextColumn Caption="三方要料" FieldName="THIRD_FLAG" Width="100px" CellStyle-HorizontalAlign="Center" />
            <dx:GridViewDataTextColumn Caption="库房要料" FieldName="STOCK_FLAG" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
            <dx:GridViewDataTextColumn Caption="三方物料接收状态" FieldName="THIRD_RECEIVE_FLAG" Width="120px" CellStyle-HorizontalAlign="Center" Visible="false" />
            <dx:GridViewDataTextColumn Caption="库房发料接收状态" FieldName="STOCK_RECEIVE_FLAG" Width="120px" CellStyle-HorizontalAlign="Center" Visible="false" />
            <dx:GridViewDataTextColumn Caption="生产确认标识" FieldName="RUN_FLAG" Width="100px" CellStyle-HorizontalAlign="Center" />

            <dx:GridViewDataTextColumn Caption="工艺地点" FieldName="ROUNTING_SITE" Width="100px" CellStyle-Wrap="False" />
            <dx:GridViewDataTextColumn Caption="工艺属性" FieldName="ROUNTING_REMARK" Width="100px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="客户代码" FieldName="CUSTOMER_CODE" Width="100px" CellStyle-Wrap="False" Visible="false" />
            <dx:GridViewDataTextColumn Caption="客户名称" FieldName="CUSTOMER_NAME" Width="120px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" Width="100px" CellStyle-Wrap="False">
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                        <%#ConvertFormat(Eval("REMARK").ToString())%></a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>

            <dx:GridViewDataTextColumn Caption="工艺路线代码" FieldName="ROUNTING_CODE" Width="100px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="柳汽标识" FieldName="LQ_FLAG" Width="100px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="改制返修是否转BOM" FieldName="IS_BOM" Width="120px" CellStyle-Wrap="False" />

            <dx:GridViewDataTextColumn Caption="订单号" FieldName="ORDER_CODE" Width="100px" CellStyle-Wrap="False" Visible="false" />

            <dx:GridViewDataTextColumn Caption=" " Width="80%" />
        </Columns>

        <Templates>
            <DetailRow>

                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnXlsExportPlanBOM" runat="server" Text="导出物料清单-EXCEL" UseSubmitBehavior="False" OnClick="btnXlsExport_Click2" />
                        </td>
                        >
                        <td>
                            <dx:ASPxButton ID="btnXlsExportSN" runat="server" Text="导出分配流水-EXCEL" UseSubmitBehavior="False" OnClick="btnXlsExport_Click3" />
                        </td>
                    </tr>
                </table>

                <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true">
                    <TabPages>
                        <dx:TabPage Text="物料清单" Visible="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" Width="1450px" AutoGenerateColumns="False" KeyFieldName="ITEM_CODE"
                                        Settings-ShowGroupPanel="false" SettingsPager-Mode="ShowAllRecords" Settings-ShowHeaderFilterButton="false" Settings-ShowVerticalScrollBar="false"
                                        OnBeforePerformDataSelect="ASPxGridView2_DataSelect" Visible="true">

                                        <Columns>
                                            <dx:GridViewCommandColumn Caption="清除" Width="40px">
                                                <ClearFilterButton Visible="True" />
                                            </dx:GridViewCommandColumn>

                                            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" Width="130px" />
                                            <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" Width="100px" />
                                            <dx:GridViewDataTextColumn Caption="工位代码" FieldName="LOCATION_CODE" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="工序代码" FieldName="PROCESS_CODE" Width="60px" />

                                            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" Width="60px" />
                                            <dx:GridViewDataTextColumn Caption="物料数量" FieldName="ITEM_QTY" Width="60px" />
                                            <dx:GridViewDataTextColumn Caption="物料重要级别代码" FieldName="ITEM_CLASS" Width="100px" />
                                            <dx:GridViewDataTextColumn Caption="物料类型" FieldName="ITEM_TYPE" Width="100px" />

                                            <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="VENDOR_CODE" Width="60px" />
                                            <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="用户ID" FieldName="CREATE_USERID" Width="60px" />
                                        </Columns>
                                    </dx:ASPxGridView>

                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="分配流水" Visible="true">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server">
                                    <dx:ASPxGridView ID="ASPxGridView3" ClientInstanceName="grid3" runat="server" Width="800px" AutoGenerateColumns="False" KeyFieldName="SN"
                                        Settings-ShowGroupPanel="false" SettingsPager-Mode="ShowAllRecords" Settings-ShowHeaderFilterButton="false" Settings-ShowVerticalScrollBar="false"
                                        OnBeforePerformDataSelect="ASPxGridView3_DataSelect">

                                        <Columns>
                                            <dx:GridViewCommandColumn Caption="清除" Width="40px">
                                                <ClearFilterButton Visible="True" />
                                            </dx:GridViewCommandColumn>

                                            <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_CODE" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" Width="120px" />
                                            <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" Width="150px" />
                                            <dx:GridViewDataTextColumn Caption="使用标识" FieldName="SN_FLAG" Width="60px" />
                                            <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" Width="150px" />
                                            <dx:GridViewDataTextColumn Caption="是否有效" FieldName="IS_VALID" Width="60px" />
                                        </Columns>
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>

                    </TabPages>
                </dx:ASPxPageControl>

            </DetailRow>
        </Templates>

    </dx:ASPxGridView>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="ASPxGridView2">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" runat="server" GridViewID="ASPxGridView3">
    </dx:ASPxGridViewExporter>

    <asp:SqlDataSource ID="boxPline" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="boxPlan" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>

</asp:Content>
