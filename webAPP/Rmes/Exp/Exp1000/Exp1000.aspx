<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Exp1000.aspx.cs" Inherits="Rmes.WebApp.Rmes.Exp.Exp1000" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--<table>
    <tr>
        <td style="width: 40px;">
            <asp:Label ID="Label1" runat="server" Text="生产线"></asp:Label>
        </td>
        <td>
            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server">
            </dx:ASPxComboBox>
        </td>



        <td>
            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查询数据" OnClick="ASPxButton1_Click"/>
        </td>
        <td align="center">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td>
            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="导出数据-EXCEL" UseSubmitBehavior="False"
                OnClick="btnXlsExport_Click" />
        </td>
        <td align="center">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>--%>


<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsDetail ShowDetailRow="true" AllowOnlyOneMasterRowExpanded="true" ExportMode="All" />
    <SettingsEditing PopupEditFormWidth="600px"/>
    <SettingsBehavior ColumnResizeMode="Control"/>
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px" >
             <ClearFilterButton Visible="True" Text="清除">
            </ClearFilterButton>
            
        </dx:GridViewCommandColumn>


        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="合同号" FieldName="PROJECT_CODE" VisibleIndex="1" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="订单号" FieldName="ORDER_CODE" VisibleIndex="2" Width="100px"/>

        <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="3" Width="180px"/>

        <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" VisibleIndex="3" Width="130px" CellStyle-HorizontalAlign="Center" SortIndex="1" SortOrder="Descending">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        
        <dx:GridViewDataTextColumn Caption="项目代号" FieldName="PLAN_SO" VisibleIndex="7" Width="150px">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="项目名称" FieldName="PLAN_SO_NAME" VisibleIndex="9" Width="230px">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn Caption="计划数" FieldName="PLAN_QTY" VisibleIndex="11" Width="50px" CellStyle-HorizontalAlign="Center"/>

        
        <dx:GridViewDataComboBoxColumn Caption="班组" FieldName="TEAM_CODE" VisibleIndex="13" Width="150px"/>
        
        <dx:GridViewDataComboBoxColumn Caption="物料确认" FieldName="ITEM_FLAG" VisibleIndex="15" Width="70px" CellStyle-HorizontalAlign="Center" Settings-AllowHeaderFilter="True">
        </dx:GridViewDataComboBoxColumn>
        
        <dx:GridViewDataComboBoxColumn Caption="执行状态" FieldName="RUN_FLAG" VisibleIndex="17" Width="70px" CellStyle-HorizontalAlign="Center" Settings-AllowHeaderFilter="True">
        </dx:GridViewDataComboBoxColumn>
        
        <dx:GridViewDataDateColumn Caption="开始日期" FieldName="BEGIN_DATE" VisibleIndex="19" Width="75px" CellStyle-Wrap="False" />

        <dx:GridViewDataTextColumn Caption="执行序" FieldName="PLAN_SEQUENCE" VisibleIndex="20" Width="45px" CellStyle-HorizontalAlign="Center"/>
        
        

        <dx:GridViewDataTextColumn Caption="上线数" FieldName="ONLINE_QTY" VisibleIndex="23" Width="50px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="下线数" FieldName="OFFLINE_QTY" VisibleIndex="25" Width="50px" CellStyle-HorizontalAlign="Center"/>

        <dx:GridViewDataTextColumn Caption="批次号" FieldName="PLAN_BATCH" VisibleIndex="27" Width="50px" CellStyle-HorizontalAlign="Center"/>
        
        <dx:GridViewDataTextColumn Caption="编制时间" FieldName="CREATE_TIME" VisibleIndex="29" Width="130px"  CellStyle-Wrap="False"/>

        
        <dx:GridViewDataTextColumn Caption="结束日期" FieldName="END_DATE" VisibleIndex="31" Width="75px" CellStyle-Wrap="False"/>
        
        <dx:GridViewDataTextColumn Caption="开工时间" FieldName="BEGIN_TIME" VisibleIndex="33" Width="130px" CellStyle-Wrap="False"/>
        <dx:GridViewDataTextColumn Caption="完工时间" FieldName="END_TIME" VisibleIndex="35" Width="130px" CellStyle-Wrap="False"/>

        <dx:GridViewDataTextColumn Caption="记账日期" FieldName="ACCOUNT_TIME" VisibleIndex="37" Width="130px" CellStyle-Wrap="False"/>

        

        <dx:GridViewDataTextColumn Caption="客户编号" FieldName="CUSTOMER_CODE" VisibleIndex="45" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="组件代号" FieldName="PRODUCT_MODEL" VisibleIndex="46" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="产品系列" FieldName="PRODUCT_SERIES" VisibleIndex="49" Width="160px" Settings-AllowHeaderFilter="True"/>

        <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" VisibleIndex="51" Width="150px" CellStyle-Wrap="False"/>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

    <Templates>
        <DetailRow>
            <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" Width="1350px" AutoGenerateColumns="False" KeyFieldName="PLAN_CODE"
                Settings-ShowGroupPanel="false" SettingsPager-Mode="ShowAllRecords" Settings-ShowHeaderFilterButton="false" Settings-ShowVerticalScrollBar="false"
                OnBeforePerformDataSelect="ASPxGridView2_DataSelect">

                <Settings VerticalScrollableHeight="120"></Settings>
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="  " ShowSelectCheckbox="FALSE" Width="40px" FixedStyle="Left">
                        <ClearFilterButton Visible="True">
                        </ClearFilterButton>
                    </dx:GridViewCommandColumn>

                    <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
                    <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" Visible="false"/>
                    
                    <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="1" Width="150px"/>
                    <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" VisibleIndex="2" Width="250px"/>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="3" Width="60px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataComboBoxColumn Caption="收料状态" FieldName="FLAG" VisibleIndex="4" Width="60px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataComboBoxColumn Caption="分配班组" FieldName="TEAM_CODE" VisibleIndex="5" Width="150px" CellStyle-HorizontalAlign="Center"/>
                    
                    <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" VisibleIndex="6" Width="150px" CellStyle-HorizontalAlign="Center"/>
                    
                    <dx:GridViewDataTextColumn Caption="零件重要级别" FieldName="ITEM_CLASS_CODE" VisibleIndex="7" Width="60px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="供应商" FieldName="VENDOR_CODE" VisibleIndex="8" Width="60px"/>
                    
                    <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="9" Width="60px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="工序名称" FieldName="PROCESS_NAME" VisibleIndex="9" Width="160px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="序号" FieldName="PROCESS_SEQUENCE" VisibleIndex="10" Width="50px" CellStyle-HorizontalAlign="Center"/>

                    <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="11" Width="180px"/>
                    <dx:GridViewDataTextColumn Caption="标准机器工时" FieldName="STANDARD_MACHINE_WORKTIME" VisibleIndex="12" Width="150px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="机器工时单位" FieldName="MACHINE_WORKTIME_UNIT" VisibleIndex="13" Width="150px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="标准人工工时" FieldName="STANDARD_MAN_WORKTIME" VisibleIndex="14" Width="150px" CellStyle-HorizontalAlign="Center"/>           
                    <dx:GridViewDataTextColumn Caption="人工工时单位" FieldName="MAN_WORKTIME_UNIT" VisibleIndex="15" Width="150px" CellStyle-HorizontalAlign="Center"/>

                    <%--<dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" VisibleIndex="1" Width="120px" FixedStyle="Left"/>
                    <dx:GridViewDataComboBoxColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="2" Width="150px" FixedStyle="Left"/>
                    <dx:GridViewDataComboBoxColumn Caption="班组" FieldName="TEAM_CODE" VisibleIndex="3" Width="120px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="工序" FieldName="PROCESS_CODE" VisibleIndex="4" Width="60px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="序号" FieldName="PROCESS_SEQUENCE" VisibleIndex="5" Width="50px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="6" Width="150px"/>
                    <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" VisibleIndex="7" Width="250px"/>
                    <dx:GridViewDataTextColumn Caption="数量" FieldName="ITEM_QTY" VisibleIndex="8" Width="60px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="零件重要级别" FieldName="ITEM_CLASS_CODE" VisibleIndex="9" Width="60px" CellStyle-HorizontalAlign="Center"/>
                    <dx:GridViewDataTextColumn Caption="供应商" FieldName="VENDOR_CODE" VisibleIndex="10" Width="60px"/>
                    <dx:GridViewDataTextColumn Caption="创建时间" FieldName="CREATE_TIME" VisibleIndex="11" Width="150px" CellStyle-HorizontalAlign="Center"/>--%>
                </Columns>
            </dx:ASPxGridView>

                   
        </DetailRow>
    </Templates>

</dx:ASPxGridView>

<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
</dx:ASPxGridViewExporter>
</asp:Content>
