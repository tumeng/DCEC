<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" Inherits="Rmes_qms1200" Codebehind="qms1200.aspx.cs" %>

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
        <dx:GridViewCommandColumn VisibleIndex="0" Caption="操作" Width="100px">
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
        <dx:GridViewDataTextColumn Caption="产品系列代码" FieldName="SERIES_CODE" VisibleIndex="2" Width="150px" CellStyle-HorizontalAlign="Center"/>

        <dx:GridViewDataTextColumn Caption="检测项目代码" FieldName="QUALITY_ITEM_CODE" VisibleIndex="3" Width="80px" CellStyle-HorizontalAlign="Center"/>
        <dx:GridViewDataTextColumn Caption="检测项目名称" FieldName="QUALITY_ITEM_NAME" VisibleIndex="4" Width="150px" CellStyle-HorizontalAlign="Center"/>

        <dx:GridViewDataTextColumn Caption="检测项目描述" FieldName="QUALITY_ITEM_DESC" VisibleIndex="5" Width="180px" CellStyle-HorizontalAlign="Center"/><%-- 列居中--%>
        
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
                    <dx:ASPxComboBox ID="productSeries" runat="server" Width="160px" Value='<%# Bind("SERIES_CODE") %>'></dx:ASPxComboBox>
                    </td>
                    <td style="width: 7px">
                    </td>
                </tr>



                <tr style="height: 30px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="检测项目代码"/>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="texDetectDataCode" runat="server" Width="160px" Text='<%# Bind("QUALITY_ITEM_CODE") %>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="数据检测代码不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="检测项目名称">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="texDetectDataName" runat="server" Width="160px" Text='<%# Bind("QUALITY_ITEM_NAME") %>'
                        ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True"
                                    ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="数据检测名称不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                </tr>


                <tr style="height: 50px">
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="详细备注信息">
                        </dx:ASPxLabel>
                    </td>
                    <td colspan="4" >
                        <dx:ASPxMemo ID="txtRemark" runat="server" Width="100%" Height="50px" Text='<%# Bind("QUALITY_ITEM_DESC") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="备注有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="备注字节长度不能超过500！" ValidationExpression="^.{0,500}$" />
                            </ValidationSettings>
                        </dx:ASPxMemo>
                    <td></td>
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
