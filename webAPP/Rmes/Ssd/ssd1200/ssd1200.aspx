<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ssd1200.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd1200.ssd1200" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">

    function changeSeq(s, e) {
        index = e.visibleIndex;
        var buttonID = e.buttonID;
        if (buttonID == "Unlock") {
            ShowPopup();
            return;
        }
        grid.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);
        
             
    }

    function checkSubmit() {
        var ref = "";
       
        if (txtPword.GetValue() == null) {
            alert("请输入密码！");
            Submit.SetEnabled(true);
            return;
        }
        var pword = txtPword.GetValue().toString();
       
        grid.GetValuesOnCustomCallback('Unlock' + '|' + pword , GetDataCallback);

    }
    function ShowPopup() {
        
        var count = grid.GetSelectedRowCount();
        if (count !=1) {
            alert("请选择单条计划");
            return;
        }
        else {
             
            var fieldNames = "PLAN_CODE";
            grid.GetSelectedFieldValues(fieldNames, GetValues);
         
        }
       
//        var pword = txtPword.GetValue().toString();
//        grid.GetValuesOnCustomCallback('Unlock' + '|' + pword, GetDataCallback);
    }

    function GetValues(values) {
        if (values[0][0] == "") {
             
            alert('没有选择任何计划！无法进行操作');
        }
        else {
            popup.Show();
            txtPlanCode.SetValue(values);
            txtPlanCode.SetEnabled(false);
        }
       
    }
    function GetDataCallback(s) {
        var result = "";
        var retStr = "";
        if (s == null) {
            grid.PerformCallback();
            return;
        }
        var array = s.split(',');
        retStr = array[1];
        result = array[0];

        switch (result) {
            case "OK":
                alert(retStr);
                initGridview();
                return;
            case "Fail":
                alert(retStr);
                return;
            case "Password":
                alert(retStr);
                popup.Hide();
                initGridview();
                return;
        }
        grid.PerformCallback();
    }

    String.prototype.endWith = function (endStr) {
        var d = this.length - endStr.length;
        return (d >= 0 && this.lastIndexOf(endStr) == d)
    }

    function OnMoreInfoClick(element, key) {
        callbackPanel.SetContentHtml("");
        popup1.ShowAtElement(element);
        keyValue = key;
    }

    function popup_Shown(s, e) {
        callbackPanel.PerformCallback(keyValue);
    }

    function initGridview() {
        grid.PerformCallback();
    }
    function OnMoreInfoClick1(element, key) {
        callbackPanel1.SetContentHtml("");
        popup12.ShowAtElement(element);
        keyValue = key;
    }
    function popup_Shown1(s, e) {
        callbackPanel1.PerformCallback(keyValue);
    }
    </script>

    <dx:ASPxPopupControl ID="popup1" ClientInstanceName="popup1" runat="server" AllowDragging="true"
                PopupHorizontalAlign="OutsideRight" HeaderText="备注">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <dx:ASPxCallbackPanel ID="callbackPanel" ClientInstanceName="callbackPanel" runat="server"
                            Width="320px" Height="120px"  RenderMode="Table" 
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

    <table style="background-color: #99bbbb; width: 100%;">
        <tr>
        <td style="width: 5px; height: 25px;">
            </td>
            <td style="width: 60px; text-align: left;">
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                </dx:ASPxLabel>
            </td>
            <td style="width: 100px">
                            <dx:ASPxComboBox ID="ASPxComboBoxPline" ClientInstanceName="listPline" runat="server" SelectedIndex="0"
                                Width="100px" AutoPostBack="false">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    grid.PerformCallback();
                                }" />
                            </dx:ASPxComboBox>
                        </td>
            <td style="width: 5px; height: 25px;">
            </td>
            <td style="text-align: left; width: 70px;">
                <label style="font-size: small">
                    开始日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" EditFormatString="yyyy-MM-dd"
                    Width="120px">
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 5px">
            </td>
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
                <dx:ASPxButton ID="ButSubmit" Text="查询计划" Width="90px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            
            <td style="width: auto">
            </td>
        </tr>
    </table>
    <dx:ASPxPopupControl ID="popup12" ClientInstanceName="popup12" runat="server" AllowDragging="true"
                PopupHorizontalAlign="OutsideRight" HeaderText="流水号">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel7" ClientInstanceName="callbackPanel1" runat="server"
                            Width="320px" Height="120px"  RenderMode="Table" 
                            OnCallback="callbackPanel1_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent7" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxMemo ID="ASPxMemo1" runat="server" Text="" Width="100%" Border-BorderStyle="None" ReadOnly="true" Height="120px">
                                                </dx:ASPxMemo>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </dx:PopupControlContentControl>
                </ContentCollection>
                <ClientSideEvents Shown="popup_Shown1" />
            </dx:ASPxPopupControl>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID"
        SettingsPager-Mode="ShowAllRecords" AutoGenerateColumns="False" 
        OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize"
        onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" OnCustomCallback="ASPxGridView1_CustomCallback">
        <Settings ShowHorizontalScrollBar="true" />
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing PopupEditFormWidth="600px" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                Width="60px">
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                        Style="margin-bottom: 0px" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>

            <dx:GridViewCommandColumn Caption="操作" Width="120px" ButtonType="Link">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Confirm" Text="执行计划">
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Cancel" Text="取消计划">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="解锁" Width="60px" ButtonType="Button">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Unlock" Text="解锁">
                    </dx:GridViewCommandColumnCustomButton>
                    
                </CustomButtons>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
            <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" Width="140px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AutoFilterCondition="Contains" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="执行序" FieldName="PLAN_SEQ" Width="60px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" Width="60px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="产品系列" FieldName="PRODUCT_SERIES" Width="100px" Visible="false"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="机型" FieldName="PRODUCT_MODEL" Width="80px"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划数" FieldName="PLAN_QTY" Width="60px" CellStyle-HorizontalAlign="Right"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick1(this, '<%# Container.KeyValue %>')">
                        <%#Eval("PLAN_QTY").ToString()%></a>
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="上线数" FieldName="ONLINE_QTY" Width="70px" CellStyle-HorizontalAlign="Right"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="下线数" FieldName="OFFLINE_QTY" Width="70px" CellStyle-HorizontalAlign="Right"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" Width="100px" Settings-AllowHeaderFilter="True"  Visible="false">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" Width="100px" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="开始日期" FieldName="BEGIN_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
           <dx:GridViewDataDateColumn Caption="结束日期" FieldName="END_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="记账日期" FieldName="ACCOUNT_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="制定时间" FieldName="CREATE_TIME" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
           <dx:GridViewDataTextColumn Caption="客户" FieldName="CUSTOMER_NAME" Width="80px"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划员" FieldName="CREATE_USERNAME" Width="80px"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="编制时间" FieldName="CREATE_TIME" Width="130px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工艺地点" FieldName="ROUNTING_SITE" Width="70px"
                CellStyle-Wrap="False" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <%--            <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" Width="200px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>--%>
            <dx:GridViewDataTextColumn Caption="备注" Width="100px">
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">
                        <%#ConvertFormat(Eval("REMARK").ToString())%></a>
                </DataItemTemplate>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="" FieldName="" Width="50px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="流水号标识" FieldName="SN_FLAG" Width="200px" CellStyle-Wrap="False"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="确认标识" FieldName="CONFIRM_FLAG" Width="200px"
                CellStyle-Wrap="False" Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="BOM标识" FieldName="BOM_FLAG" Width="200px" CellStyle-Wrap="False"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="发料标识" FieldName="ITEM_FLAG" Width="200px" CellStyle-Wrap="False"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="柳汽标识" FieldName="LQ_FLAG" Width="200px" CellStyle-Wrap="False"
                Visible="false" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CONFIRM_FLAG" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RUN_FLAG" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ROUNTING_CODE" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " Width="80%" />
        </Columns>

         <Settings ShowFooter="True" />
         <TotalSummary>
                  <dx:ASPxSummaryItem FieldName="PLAN_CODE" SummaryType="Count" DisplayFormat="计划数={0}"/>
            <dx:ASPxSummaryItem FieldName="PLAN_QTY" SummaryType="Sum" DisplayFormat="总数={0}"/>
            <dx:ASPxSummaryItem FieldName="ONLINE_QTY" SummaryType="Sum" DisplayFormat="上线数={0}"/>
            <dx:ASPxSummaryItem FieldName="OFFLINE_QTY" SummaryType="Sum" DisplayFormat="下线数={0}"/>
        </TotalSummary>
        <SettingsBehavior ColumnResizeMode="Control" />
        <ClientSideEvents CustomButtonClick="function (s,e){
        changeSeq(s,e);
    }" />
    </dx:ASPxGridView>
 <dx:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
        ShowHeader="True" HeaderText="解锁计划" AllowDragging="true" Width="80%">
         
        <ContentCollection>
            <dx:PopupControlContentControl Width="100%">
                <table align="center" style="width: 400px; height: 100px;">
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 100px">
                            <label style="font-size: small">
                                计划代码</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextPlanCode" ClientInstanceName="txtPlanCode" runat="server"
                                Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 100px">
                            <label style="font-size: small">
                                密码</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxPword" ClientInstanceName="txtPword" runat="server" Width="120px" />
                        </td>
                        
                    </tr>
                   
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td colspan="3" align="center">
                            <dx:ASPxButton ID="UpdateButton" ClientInstanceName="Submit" ReplacementType="EditFormUpdateButton"
                                Text="提交" AutoPostBack="false" runat="server" Width="80px">
                                <ClientSideEvents Click="function(s,e) { checkSubmit(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td colspan="3" align="center">
                            <dx:ASPxButton ID="CancelButton" ReplacementType="EditFormCancelButton" Text="取消"
                                AutoPostBack="false" runat="server" Width="80px">
                                <ClientSideEvents Click="function(s,e) { popup.Hide(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <%--<dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                    OnCallback="ASPxCbSubmit_Callback">
                    <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                </dx:ASPxCallback>--%>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
