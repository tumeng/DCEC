<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="qms1100.aspx.cs" Inherits="Rmes_qms1100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <%--加载时服务器时间：<%=OracleServerTime%>--%>

<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" Width="100%" KeyFieldName="RMES_ID"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowValidating="ASPxGridView1_RowValidating"
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    Settings-ShowHeaderFilterButton="false" >
    <Settings ShowFilterRow="True" />
    <SettingsEditing PopupEditFormWidth="600px" PopupEditFormHorizontalAlign="WindowCenter" />
    <%--<Settings ShowHorizontalScrollBar="true" />--%>

    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="120px">
            <NewButton Visible="True" Text="新增">
            </NewButton>
            <EditButton Visible="True" Text="修改">
            </EditButton>
            <DeleteButton Visible="True" Text="删除">
            </DeleteButton>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>

        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="生产线代码" FieldName="PLINE_NAME" VisibleIndex="1" Width="160px" CellStyle-HorizontalAlign="Left">
            <Settings AutoFilterCondition="Contains" />  <%-- 支持模糊查询--%>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="产品系列代码" FieldName="PRODUCT_SERIES" VisibleIndex="2" Width="150px" CellStyle-HorizontalAlign="Center"/>
        
        <dx:GridViewDataTextColumn Caption="检测项目代码" FieldName="DETECT_DATA_CODE" VisibleIndex="3" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="检测项目名称" FieldName="DETECT_DATA_NAME" VisibleIndex="4" Width="150px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="检测项目类型" FieldName="DETECT_DATA_TYPE" VisibleIndex="5" Width="80px" CellStyle-HorizontalAlign="Center">
            <DataItemTemplate><%# Container.Text == "0" ? "计量型" : "计点型"%></DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="检测项目标准值" FieldName="DETECT_DATA_STANDARD" VisibleIndex="6" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="检测项目上限" FieldName="DETECT_DATA_UP" VisibleIndex="7" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="检测项目下限" FieldName="DETECT_DATA_DOWN" VisibleIndex="8" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="检测项目单位" FieldName="DETECT_DATA_UNIT" VisibleIndex="9" Width="80px" CellStyle-HorizontalAlign="Center"/>

        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>

    <Templates>
        <EditForm>
        <dx:ASPxTextBox ID="txtRmesID" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>' Visible="false"/>
            <table>
                <tr style="height: 10px">
                    <td colspan="7">
                    </td>
                </tr>

                <tr style="height: 30px">
                    <td style="width: 8px;">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                    <dx:ASPxComboBox ID="plineCode" runat="server" Width="160px" Value='<%# Bind("PLINE_CODE") %>' ></dx:ASPxComboBox>
                    </td>
                    <td style="width: 5px">
                    </td>
                    <td style="width: 100px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="产品系列代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                    <dx:ASPxComboBox ID="productSeries" runat="server" Width="160px" Value='<%# Bind("PRODUCT_SERIES") %>'></dx:ASPxComboBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>



                <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="检测数据代码"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="texDetectDataCode" runat="server" Width="160px" Text='<%# Bind("DETECT_DATA_CODE") %>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="数据检测代码不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="检测数据名称">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="texDetectDataName" runat="server" Width="160px" Text='<%# Bind("DETECT_DATA_NAME") %>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="数据检测名称不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                </tr>

                 <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="检测数据类型"/>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="detectDataType" runat="server" Width="160px" Value='<%# Bind("DETECT_DATA_TYPE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <Items>
                                <dx:ListEditItem Text="计量型" Value="0" />
                                <dx:ListEditItem Text="计点型" Value="1" />
                            </Items>
                            <ValidationSettings>
                                <RequiredField ErrorText="请选择一个类型" IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="检测数据标准值">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtDetectDataStandard" runat="server" Width="160px" Value='<%# Bind("DETECT_DATA_STANDARD") %>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="请输入一个实数" ValidationExpression="^[-\+]?(\d+)?(\.\d+)?$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                </tr>

                 <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="检测数据上限"/>
                    </td>
                    <td>
                        
                        <dx:ASPxTextBox ID="txtDetectDataUp" runat="server" Width="160px" Value='<%# Bind("DETECT_DATA_UP") %>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                    ErrorDisplayMode="ImageWithTooltip">
                               <RegularExpression ErrorText="请输入一个实数" ValidationExpression="^[-\+]?(\d+)?(\.\d+)?$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="检测数据下限">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtDetectDataDown" runat="server" Width="160px" Value='<%# Bind("DETECT_DATA_DOWN") %>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="请输入一个实数" ValidationExpression="^[-\+]?(\d+)?(\.\d+)?$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                </tr>

                <tr style="height: 50px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="数据检测单位">
                        </dx:ASPxLabel>
                    </td>
                    <td >
                        <dx:ASPxTextBox ID="txtDetectDataUnit" runat="server" Width="160px" Text='<%# Bind("DETECT_DATA_UNIT") %>'></dx:ASPxTextBox>
                    </td>
                    <td colspan="4"></td>
                </tr>

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
                    
    <ClientSideEvents 
        BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" 
        EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" 
    />
</dx:ASPxGridView>
 
<%--<table width="100%">
<tr>
    <td style="height: 400px; vertical-align: top">



    </td>
</tr>
</table>--%>

</asp:Content>
