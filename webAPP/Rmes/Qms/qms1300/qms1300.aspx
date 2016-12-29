<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="qms1300.aspx.cs" Inherits="Rmes_qms1300" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function getPlineCode() {
        var plineCode = detectDataType.GetValue();
        
    }

</script>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
        Width="100%" KeyFieldName="RMES_ID" OnRowInserting="ASPxGridView1_RowInserting"
        OnRowUpdating="ASPxGridView1_RowUpdating" OnRowDeleting="ASPxGridView1_RowDeleting"
        OnRowValidating="ASPxGridView1_RowValidating" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <Settings ShowFilterRow="True" ShowHeaderFilterButton="false" />
        <SettingsEditing PopupEditFormWidth="600px" PopupEditFormHorizontalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="120px">
                <NewButton Visible="True" Text="新增" />
                <EditButton Visible="True" Text="修改" />
                <DeleteButton Visible="True" Text="删除" />
                <ClearFilterButton Visible="True" />
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINECODE1" VisibleIndex="1"
                Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" Visible="false" VisibleIndex="1"
                Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="数据代码" FieldName="DETECT_CODE" VisibleIndex="3" Settings-AutoFilterCondition="Contains"
                Width="120px" />
            <dx:GridViewDataTextColumn Caption="数据名称" FieldName="DETECT_NAME" VisibleIndex="5" Settings-AutoFilterCondition="Contains"
                Width="180px" />
            <dx:GridViewDataComboBoxColumn Caption="数据类型" FieldName="DETECT_TYPE" VisibleIndex="7" Settings-AutoFilterCondition="Contains"
                Width="80px" CellStyle-HorizontalAlign="Center">
                <PropertiesComboBox ValueType="System.String">
                </PropertiesComboBox>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="标准值" FieldName="DETECT_STANDARD" VisibleIndex="9" Visible="false"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="上限" FieldName="DETECT_MAX" VisibleIndex="11"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="下限" FieldName="DETECT_MIN" VisibleIndex="10"
                Width="80px" />
            <dx:GridViewDataTextColumn Caption="单位" FieldName="DETECT_UNIT" VisibleIndex="17"
                Width="80px" CellStyle-HorizontalAlign="Center">
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="关联类型" FieldName="ASSOCIATION_TYPE" VisibleIndex="97" Visible="false"
                Width="180px" />
            <%--<dx:GridViewDataTextColumn Caption=" " VisibleIndex="101" Width="80%" />--%>
            <dx:GridViewDataTextColumn Caption="工艺路线" FieldName="PRODUCT_SERIES" VisibleIndex="99" Visible="false">
            </dx:GridViewDataTextColumn>
        </Columns>
        <Templates>
            <EditForm>
                <dx:ASPxTextBox ID="txtRmesID" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>'
                    Visible="false" />
                <table>
                    <tr style="height: 10px">
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 15px;">
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 165px">
                            <dx:ASPxComboBox ID="combPline" runat="server" Width="160px" Value='<%# Bind("PLINE_CODE") %>' DropDownStyle="DropDownList" 
                                DataSourceID="sqlPline" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                ValueField="PLINE_CODE" TextField="PLINE_NAME">
                                <%--<ClientSideEvents SelectedIndexChanged="function (s,e){comWorkUnit.PerformCallback(s.lastSuccessValue);}" />--%>
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="生产线不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 10px">
                        </td>
                        <%--<td>
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="工作中心">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="comWorkUnit" ClientInstanceName="comWorkUnit" runat="server" Width="160px" Value='<%# Bind("WORKUNIT_CODE") %>'
                                 OnCallback="comWorkUnit_Callback">
                                
                            </dx:ASPxComboBox>
                        </td>--%>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="工艺路线" Visible="false">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="160px" Text='<%# Bind("PRODUCT_SERIES") %>' Visible="false" 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <%--<ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="工艺路线不能为空！" />
                                </ValidationSettings>--%>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            
                             <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="数据名称">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="texDetectDataName" runat="server" Width="160px" Text='<%# Bind("DETECT_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="数据名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                             <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="检验代码" />
<%--                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="数据名称">
                            </dx:ASPxLabel>--%>
                        </td>
                        <td>
                               <dx:ASPxTextBox ID="texDetectDataCode" runat="server" Width="160px" Text='<%# Bind("DETECT_CODE") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <%--<RequiredField IsRequired="True" ErrorText="检验代码不能为空！" />--%>
                                </ValidationSettings>
                            </dx:ASPxTextBox>
<%--                            <dx:ASPxTextBox ID="texDetectDataName" runat="server" Width="160px" Text='<%# Bind("DETECT_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="数据名称不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>--%>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td style="width: 80px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="数据类型" />
                        </td>
                        <td style="width: 165px">
                            <dx:ASPxComboBox ID="detectDataType" runat="server" Width="160px" Value='<%# Bind("DETECT_TYPE") %>' DropDownStyle="DropDownList"  AutoPostBack="false"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" ClientInstanceName="detectDataType">
                                <Items>
                                    <dx:ListEditItem Text="计量型" Value="0" />
                                    <dx:ListEditItem Text="计点型" Value="1" />
                                    <dx:ListEditItem Text="文本型" Value="2" />
                                    <dx:ListEditItem Text="零件条码" Value="3" />
                                    <%--<dx:ListEditItem Text="文件" Value="F" />--%>
                                </Items><%-- var ui =document.getElementById('ctl00_ContentPlaceHolder1_ASPxGridView1_DXPEForm_efnew_ASPxLabel6');ui.style.display='none';   var ui1 =document.getElementById('ctl00_ContentPlaceHolder1_ASPxGridView1_DXPEForm_efnew_ASPxLabel9');ui1.style.display='none'; --%>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) {var lx1=detectDataType.GetSelectedItem().value;if(lx1!='0'){ txtDetectDataUp.SetVisible(false);txtDetectDataDown.SetVisible(false);txtDetectDataStandard1.SetVisible(false);txtDetectDataUnit.SetVisible(false);ASPxLabel91.SetVisible(false);ASPxLabel61.SetVisible(false);ASPxLabel2.SetVisible(false);}else{ txtDetectDataUp.SetVisible(true);txtDetectDataDown.SetVisible(true);txtDetectDataStandard1.SetVisible(false);txtDetectDataUnit.SetVisible(true);ASPxLabel91.SetVisible(true);ASPxLabel61.SetVisible(true);ASPxLabel2.SetVisible(true);txtDetectDataUnit.SetVisible(true);} e.processOnServer = false;}"  />
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="检验类型不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="关联类型" Visible="false">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtDescript" runat="server" Width="160px" Text='<%# Bind("ASSOCIATION_TYPE") %>' Visible="false" >
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="下限" ClientInstanceName="ASPxLabel91">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtDetectDataDown" runat="server" Width="160px" Value='<%# Bind("DETECT_MIN") %>' ClientInstanceName="txtDetectDataDown"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="请输入一个实数" ValidationExpression="^[-\+]?(\d+)?(\.\d+)?$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="上限"  ClientInstanceName="ASPxLabel61"/>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtDetectDataUp" runat="server" Width="160px" Value='<%# Bind("DETECT_MAX") %>' ClientInstanceName="txtDetectDataUp"
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="请输入一个实数" ValidationExpression="^[-\+]?(\d+)?(\.\d+)?$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">

                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="单位" ClientInstanceName="ASPxLabel2" >
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtDetectDataUnit" runat="server" Width="160px" Text='<%# Bind("DETECT_UNIT") %>' ClientInstanceName="txtDetectDataUnit" > 
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="标准值" ClientVisible="false">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtDetectDataStandard" runat="server" Width="160px" Value='<%# Bind("DETECT_STANDARD") %>' ClientInstanceName="txtDetectDataStandard1" 
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"  ClientVisible="false" > 
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="请输入一个实数" ValidationExpression="^[-\+]?(\d+)?(\.\d+)?$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <%-- <tr style="height: 30px">
                   <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="系列代码">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="160px" Text='<%# Bind("PRODUCT_SERIES") %>'>
                            </dx:ASPxTextBox>
                        </td>
                   </tr>--%>
                    <tr style="height: 30px">
                        <td style="text-align: right;" colspan="6">
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
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="sqlPline" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
