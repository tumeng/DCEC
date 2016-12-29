<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="atpu1C00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Atpu.atpu1C00.atpu1C00" %>

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
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //根据生产线初始化站点
        var pline;
        function filterStation() {
            zdmc_C.ClearItems(); //站点

            pline = comboPLineC.GetValue().toString();

            zdmc_C.PerformCallback(pline);
            
        }
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
        function initEditZD(s, e) {

            var webFileUrl = "?zddmC=" + zdmc_C.GetValue() + "&opFlag=getEditZD";

            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");

            result = xmlHttp.responseText;

            if (result == "") {
                alert("站点名称不存在，请检查数据！");
                zdmc_C.SetFocus();
                zddm_C.SetValue("");
                return;
            }
            zddm_C.SetValue(result);
        }
        function initEditSeries(s, e) {
            if (comboPLineC.GetValue() == null) {
                return;
            }
            var webFileUrl = "?PCode=" + comboPLineC.GetValue() + "&opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;
            grid.PerformCallback();
        }
    </script>
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="0">
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
                            Width="100%" KeyFieldName="GZDD;ZDDM;LJDM" OnRowInserting="ASPxGridView1_RowInserting"
                            OnRowUpdating="ASPxGridView1_RowUpdating" OnCustomCallback="ASPxGridView1_CustomCallback"
                            OnRowValidating="ASPxGridView1_RowValidating" 
                            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" 
                            OnRowDeleting="ASPxGridView1_RowDeleting1">
                            <SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter"
                                PopupEditFormVerticalAlign="WindowCenter" />
<ClientSideEvents BeginCallback="function(s, e) {grid.cpCallbackName = &#39;&#39;;}" EndCallback="function(s, e) 
                                {
                                    callbackName = grid.cpCallbackName;
                                    theRet = grid.cpCallbackRet;
                                    if(callbackName == &#39;Delete&#39;) 
                                    {
                                        alert(theRet);
                                    }
                                }"></ClientSideEvents>
                            <Columns>
                                <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="120px">
                                    <EditButton Visible="FALSE" />
                                    <NewButton Visible="True" />
                                    <DeleteButton Visible="True" />
                                    <ClearFilterButton Visible="True" />
<NewButton Visible="True"></NewButton>

<DeleteButton Visible="True"></DeleteButton>

<ClearFilterButton Visible="True"></ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1"
                                    Width="100px" Settings-AutoFilterCondition="Contains" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="100px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="站点代码" FieldName="ZDDM" VisibleIndex="3" Visible="false"
                                    Settings-AutoFilterCondition="Contains" Width="100px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="站点名称" FieldName="ZDMC" VisibleIndex="4" Settings-AutoFilterCondition="Contains"
                                    Width="150px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM" VisibleIndex="5" Settings-AutoFilterCondition="Contains"
                                    Width="100px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="LJMC" VisibleIndex="6" Visible="false"
                                    Settings-AutoFilterCondition="Contains" Width="150px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件种类" FieldName="PART_ABC" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                                    Width="150px" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件种类" FieldName="PART_ABC_NAME" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                                    Width="150px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>

<SettingsEditing PopupEditFormWidth="530px" PopupEditFormHorizontalAlign="WindowCenter" PopupEditFormVerticalAlign="WindowCenter"></SettingsEditing>

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
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="comboPLine" runat="server" EnableClientSideAPI="True" Value='<%# Bind("GZDD") %>'
                                                    DropDownStyle="DropDownList" DataSourceID="SqlDataSource4" ValueType="System.String"
                                                    Width="150px" ClientInstanceName="comboPLineC" TextField="PLINE_NAME" ValueField="PLINE_CODE"
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                                        <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                                                    </ValidationSettings>
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){  filterStation(s,e); }" />
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="零件代码">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxGridLookup ID="comboLJDM" runat="server" Width="150px" SelectionMode="Single"
                                                    TextFormatString="{1}" KeyFieldName="PT_PART" ValueField="PT_PART" TextField="PT_DESC2"
                                                    Value='<%# Bind("LJDM") %>' DataSourceID="SqlDataSource3" MultiTextSeparator=","
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" />
                                                        <dx:GridViewDataColumn FieldName="PT_PART" Visible="false" />
                                                        <dx:GridViewDataColumn Caption="零件代码" FieldName="PT_PART" />
                                                        <dx:GridViewDataColumn Caption="零件名称" FieldName="PT_DESC2" />
                                                    </Columns>
                                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                                        ValidateOnLeave="True">
                                                        <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                                        <RequiredField ErrorText="零件不能为空！" IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxGridLookup>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="站点名称">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="comboStationCode" runat="server" ClientInstanceName="zdmc_C"
                                                    EnableClientSideAPI="True" Value='<%# Bind("ZDMC") %>' DropDownStyle="DropDownList"
                                                     ValueType="System.String" Width="150px" 
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" 
                                                    oninit="comboStationCode_Init" oncallback="comboStationCode_Callback"><%--DataSourceID="SqlDataSource2"TextField="STATION_NAME"  ValueField="STATION_NAME" --%>
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过50！" ValidationExpression="^.{0,50}$" />
                                                        <RequiredField IsRequired="True" ErrorText="站点代码不能为空！" />
                                                    </ValidationSettings>
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){
                                                            initEditZD(s,e);
                                                        }" />
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td></td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="零件类别">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxComboBox ID="comboLJLB" runat="server" ClientInstanceName="comboLJLB_C"
                                                    EnableClientSideAPI="True" Value='<%# Bind("PART_ABC") %>' DropDownStyle="DropDownList"
                                                    DataSourceID="SqlLJLB" ValueType="System.String" Width="150px" TextField="INTERNAL_NAME"
                                                    ValueField="INTERNAL_CODE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过10！" ValidationExpression="^.{0,10}$" />
                                                        <RequiredField IsRequired="True" ErrorText="零件类别不能为空！" />
                                                    </ValidationSettings>
                                                    <%--<ClientSideEvents SelectedIndexChanged="function(s,e){
                                                            initEditZD(s,e);
                                                        }" />--%>
                                                </dx:ASPxComboBox>
                                            </td>
                                         
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                             <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 90px">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="站点代码" Visible="false" >
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxTextBox ID="txtStationName" runat="server" ClientInstanceName="zddm_C" Text='<%# Bind("ZDDM") %>'
                                                    ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" Width="150px"
                                                    ReadOnly="true" Visible="false">
                                                     
                                                </dx:ASPxTextBox>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                        <td style="height: 10px" colspan="7">
                                        <label id="id1" runat="server" style=" color:Red"> 单件码为A类，批次码为B类，单批次码为D类，其他零件为C类</label>
                                           
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
                            KeyFieldName="PLINE_CODE;ABOM_DESC" OnRowDeleting="ASPxGridView2_RowDeleting"
                            OnRowInserting="ASPxGridView2_RowInserting" OnRowValidating="ASPxGridView2_RowValidating"
                            OnHtmlEditFormCreated="ASPxGridView2_HtmlEditFormCreated">
                            <SettingsEditing PopupEditFormWidth="600px" PopupEditFormHeight="160px" />
                            <SettingsBehavior ColumnResizeMode="Control" />
<ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid2.cpCallbackName = &#39;&#39;;
        }" EndCallback="function(s, e) 
        {
            callbackName = grid2.cpCallbackName;
            theRet = grid2.cpCallbackRet;
            if(callbackName == &#39;Delete&#39;) 
            {
                confirm(&#39;确认要删除这条记录吗？&#39;);
                alert(theRet);
            }
             
        }"></ClientSideEvents>
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
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" VisibleIndex="1" Visible="false"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="1"
                                    Width="100px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件名称" FieldName="ABOM_DESC" VisibleIndex="6"
                                    Settings-AutoFilterCondition="Contains" Width="200px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件种类" FieldName="PART_ABC" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                                    Width="150px" Visible="false">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="零件种类" FieldName="PART_ABC_NAME" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                                    Width="150px">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%" />
                            </Columns>

<SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>

<SettingsEditing PopupEditFormWidth="600px" PopupEditFormHeight="160px"></SettingsEditing>

                            <Templates>
                                <EditForm>
                                    <table>
                                        <br />
                                        <tr>
                                            <td style="width: 30px">
                                            </td>
                                            <td style="width: 100px; text-align: left;">
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="生产线">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 180px">
                                                <dx:ASPxComboBox ID="txtPCode" runat="server" Width="150px" Value='<%# Bind("PLINE_CODE") %>'
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
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="重要零件名称">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 200px">
                                                <dx:ASPxTextBox ID="txtName" runat="server" Text='<%# Bind("ABOM_DESC") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                                    Width="150px">
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
                                            <td style="width: 8px; height: 30px">
                                            </td>
                                            <td style="width: 80px">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="零件类别" >
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                 <dx:ASPxComboBox ID="comboLJLB_NAME" runat="server" ClientInstanceName="comboLJLB_NAME_C"
                                                    EnableClientSideAPI="True" Value='<%# Bind("PART_ABC") %>' DropDownStyle="DropDownList"
                                                    DataSourceID="SqlLJLB" ValueType="System.String" Width="150px" TextField="INTERNAL_NAME"
                                                    ValueField="INTERNAL_CODE" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                                        ErrorDisplayMode="ImageWithTooltip">
                                                        <RegularExpression ErrorText="长度不能超过10！" ValidationExpression="^.{0,10}$" />
                                                        <RequiredField IsRequired="True" ErrorText="零件类别不能为空！" />
                                                    </ValidationSettings>
                                                   
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 1px">
                                            </td>
                                             
                                            <td style="width: 1px">
                                            </td>
                                        </tr>
                                        <tr>
                                        <td style="height: 10px" colspan="7">
                                        <label id="id1" runat="server" style=" color:Red"> 单件码为A类，批次码为B类，单批次码为D类，其他零件为C类</label>
                                           
                                        </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="height: 30px; text-align: right;">
                                                <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" runat="server"
                                                    ReplacementType="EditFormUpdateButton" />
                                                <dx:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement2" runat="server"
                                                    ReplacementType="EditFormCancelButton" />
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
    <asp:SqlDataSource ID="SqlLJLB" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource22" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
