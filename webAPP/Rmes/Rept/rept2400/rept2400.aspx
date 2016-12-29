<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept2400.aspx.cs" Inherits="Rmes_Rept_rept2400_rept2400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%--改制扫描确认--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function changeSeq(s, e) {
            index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index);

        }
        function changeSeq2(s, e) {
            index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid2.GetValuesOnCustomCallback(buttonID + '|' + index);

        }
    </script>
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" ClientInstanceName="PCode"
                    SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td style="text-align: left; width: 60px;">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="维护时间:" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="100px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="--" />
            </td>
            <td style="width: 100px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="100px" ClientInstanceName="DateEdit2">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="发动机流水号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSN" runat="server" ValueField="SN" ValueType="System.String"
                    Width="100px">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="状态:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtFlag" runat="server" ValueType="System.String" Width="100px" SelectedIndex=0>
                    <Items>
                        <dx:ListEditItem Text="未处理" Value="N" />
                        <dx:ListEditItem Text="已处理" Value="Y" />
                    </Items>
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询" Width="80px">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();grid2.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <fieldset style="text-align: left;">
        <legend><span style="font-size: 10pt; width: auto">
            <asp:Label ID="Label5" runat="server" Text="改制采集数据确认" Font-Bold="True"></asp:Label></span></legend>
        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
            OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize"
            OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="RMES_ID">
             <ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
 
            if(callbackName == 'refresh') 
            {
                grid.PerformCallback();
            }
            
        }" />
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                    VisibleIndex="0" Width="60px">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                            Style="margin-bottom: 0px" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn Caption="处理 " Width="100px" ButtonType="Button" VisibleIndex="0">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="Treated" Text="取消确认">
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="Untreated" Text="确认">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="RMES_ID" FieldName="RMES_ID" Visible="false"
                    Width="100px" Settings-AutoFilterCondition="Contains">
                    <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="1" Width="100px"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="项目名称" FieldName="DETECT_NAME" VisibleIndex="2"
                    Width="200px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="测量值" FieldName="DETECT_VALUE" VisibleIndex="3"
                    Width="220px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="站点" FieldName="STATION_NAME" VisibleIndex="4"
                    Width="100px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="扫描时间" FieldName="WORK_TIME" VisibleIndex="5"
                    Width="140px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="检测标识" FieldName="DETECT_FLAG" VisibleIndex="6"
                    Width="80px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工艺路线" FieldName="PRODUCT_SERIES" VisibleIndex="7"
                    Width="120px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="项目代码" FieldName="DETECT_CODE" VisibleIndex="9"
                    Width="100px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                </dx:GridViewDataTextColumn>
            </Columns>
            <ClientSideEvents CustomButtonClick="function(s, e){
            changeSeq(s, e);  
            }" />
        </dx:ASPxGridView>
    </fieldset>
    <fieldset style="text-align: left;">
        <legend><span style="font-size: 10pt; width: auto">
            <asp:Label ID="Label1" runat="server" Text="改制扫描条码确认" Font-Bold="True"></asp:Label></span></legend>
        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
            OnCustomDataCallback="ASPxGridView2_CustomDataCallback" OnCustomButtonInitialize="ASPxGridView2_CustomButtonInitialize"
            OnCustomCallback="ASPxGridView2_CustomCallback" KeyFieldName="RMES_ID">
            <ClientSideEvents BeginCallback="function(s, e) {grid2.cpCallbackName = '';}" EndCallback="function(s, e) 
        {
            callbackName = grid2.cpCallbackName;
            theRet = grid2.cpCallbackRet;
 
            if(callbackName == 'refresh') 
            {
                grid2.PerformCallback();
            }
            
        }" />
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                    VisibleIndex="0" Width="60px">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid2.SelectAllRowsOnPage(s.GetChecked()); }"
                            Style="margin-bottom: 0px" />
                    </HeaderTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn Caption="处理 " Width="100px" ButtonType="Button" VisibleIndex="0">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="Treated2" Text="取消确认">
                        </dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="Untreated2" Text="确认">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="RMES_ID" FieldName="RMES_ID" Visible="false"
                    Width="100px" Settings-AutoFilterCondition="Contains">
                    <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="流水号" FieldName="SN" VisibleIndex="1" Width="100px"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="计划代码" FieldName="PLAN_CODE" VisibleIndex="2"
                    Width="120px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" VisibleIndex="3"
                    Width="100px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" VisibleIndex="4"
                    Width="120px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="VENDOR_CODE" VisibleIndex="5"
                    Width="120px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="站点名称" FieldName="STATION_NAME" VisibleIndex="7"
                    Width="120px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="计算时间" FieldName="CREATE_TIME" VisibleIndex="9"
                    Width="140px" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                </dx:GridViewDataTextColumn>
            </Columns>
            <ClientSideEvents CustomButtonClick="function(s, e){
            changeSeq2(s, e);  
            }" />
        </dx:ASPxGridView>
    </fieldset>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlCode2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
