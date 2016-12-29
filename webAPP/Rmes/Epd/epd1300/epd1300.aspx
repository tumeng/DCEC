<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_epd1300" Codebehind="epd1300.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="STATION_CODE" 
    OnRowDeleting="ASPxGridView1_RowDeleting"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowValidating="ASPxGridView1_RowValidating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
    <SettingsEditing PopupEditFormWidth="560px"/>
    <SettingsBehavior ColumnResizeMode="Control"/>
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

        <dx:GridViewDataTextColumn FieldName="STATION_TYPE_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="PLINE_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>

        <dx:GridViewDataTextColumn Caption="站点代码" Name="STATION_CODE" FieldName="STATION_CODE" VisibleIndex="1" Width="120px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="站点名称" Name="STATION_NAME" FieldName="STATION_NAME" VisibleIndex="2" Width="180px">
        </dx:GridViewDataTextColumn>
        
        <dx:GridViewDataTextColumn Caption="生产线" Name="PLINE_CODE" FieldName="PLINE_CODE" VisibleIndex="4" Width="210px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="站点区域" Name="STATION_AREA_CODE" FieldName="STATION_AREA_CODE" VisibleIndex="5" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工作中心" Name="WORKUNIT_CODE" FieldName="WORKUNIT_CODE" VisibleIndex="5" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
                    
    <Templates>
        <EditForm>
            <table>
            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="150px" Text='<%# Bind("RMES_ID") %>' Visible="false">
                           
                        </dx:ASPxTextBox>
                <tr>
                    <td style="height: 30px" colspan="7">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20px; height: 30px">
                    </td>
                    <td style="width: 80px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="站点代码">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtStationCode" runat="server" Width="150px" Text='<%# Bind("STATION_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 10px">
                    </td>
                    <td style="width: 80px">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="站点名称">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 180px">
                        <dx:ASPxTextBox ID="txtStationName" runat="server" Width="150px" Text='<%# Bind("STATION_NAME") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 10px">
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="生产线">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="dropPlineCode" runat="server" Width="150px" Value='<%# Bind("PLINE_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="工作中心">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="workUnit" runat="server" Width="150px" Text='<%# Bind("WORKUNIT_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px">
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="站点类型">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="dropStaionType" runat="server" Width="150px" Value='<%# Bind("STATION_TYPE_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="站点区域">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="dropStaionArea" runat="server" Width="150px" Value='<%# Bind("STATION_AREA_CODE") %>'
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: right;">
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
                    
    <ClientSideEvents BeginCallback="function(s, e) 
        {
	        grid.cpCallbackName = '';
        }" EndCallback="function(s, e) 
        {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCallbackRet;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
        }" />
</dx:ASPxGridView>

</asp:Content>
