<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd3500.aspx.cs" Inherits="Rmes_epd3500" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
        OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowDeleting="ASPxGridView1_RowDeleting" OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsBehavior ColumnResizeMode="Control"/>
        <SettingsEditing  Mode="PopupEditForm" PopupEditFormWidth="600px" PopupEditFormHorizontalAlign="WindowCenter" />
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="100">
                <NewButton Visible="true"/>
                <EditButton Visible="true"/>
                <DeleteButton Visible="true"/>
                <ClearFilterButton Visible="true"/>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataComboBoxColumn Caption="工作中心编码" FieldName="WORKUNIT_CODE" 
                VisibleIndex="1" Width="200px" >
<PropertiesComboBox ValueType="System.String"></PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataTextColumn Caption="公司" FieldName="COMPANY_CODE" VisibleIndex="2" Width="200px" />
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_CODE" VisibleIndex="3" Width="200px" />
            <dx:GridViewDataTextColumn Caption="工序代码" FieldName="PROCESS_CODE" VisibleIndex="4" Width="100px" />
            <dx:GridViewDataTextColumn Caption="工序名称" FieldName="PROCESS_NAME" VisibleIndex="5" Width="200px" />
            <dx:GridViewDataTextColumn Caption="工时" FieldName="PROCESS_MANHOUR" VisibleIndex="6" Width="100px" />
            <dx:GridViewDataComboBoxColumn Caption="车间" FieldName="WORKSHOP_CODE" 
                VisibleIndex="7" Width="100px" >
<PropertiesComboBox ValueType="System.String"></PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="QAD工序代码" FieldName="PROCESS_CODE_SAP" VisibleIndex="8" Width="100px" />
            <dx:GridViewDataTextColumn Caption="本地编码" FieldName="PROCESS_CODE_ORG" VisibleIndex="9" Width="100px" />
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%" />
        </Columns>
        <SettingsText ConfirmDelete="确定要删除么？" />
        <Templates>
            <EditForm>
                <dx:ASPxTextBox ID="txtRmesID" runat="server" Width="160px" Text='<%# Bind("RMES_ID") %>'
                    Visible="false" />
                
                               <table>
                                
                                <tr>
                                    <td style="height:10px" colspan="7"></td>                                    
                                </tr>
                                
                                <tr>
                                    <td style="width:8px; height:30px"></td>
                                    
                                    <td style="width:80px">
                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="工作中心编码">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td style="width:170px">
                                        <dx:ASPxComboBox ID="WorkUnit" runat="server" Width="160px" Value='<%# Bind("WORKUNIT_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" OnDataBinding="WorkUnit_DataBinding">
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    
                                    <td style="width:1px"></td>

                                    <td style="width:90px">
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="车间">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td style="width:180px">
                                        <dx:ASPxComboBox ID="WorkShop" runat="server" Width="160px" Value='<%# Bind("WORKSHOP_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <Items>
                                                <dx:ListEditItem Text="园区" Value="8101" />
                                                <dx:ListEditItem Text="基地" Value="8102" />
                                                
                                            </Items>
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    
                                    <td style="width:1px"></td>
                                    
                                   </tr>

                                   <tr>
                                   <td style="width:8px; height:30px"></td>
                                    <td style="width:90px">
                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="QAD工序代码">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td style="width:180px">
                                        <dx:ASPxTextBox ID="txtSAP" runat="server" Width="160px" Text='<%# Bind("PROCESS_CODE_SAP") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    
                                    <td style="width:1px"></td>
                                

                                    <td style="width:90px">
                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="本地编码">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td style="width:180px">
                                        <dx:ASPxTextBox ID="txtORG" runat="server" Width="160px" Text='<%# Bind("PROCESS_CODE_ORG") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    
                                    <td style="width:1px"></td>
                                </tr>
                                
                                <tr>
                                <td style="height:30px"></td>
                                    
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="生产线">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="txtPline" runat="server" Width="160px" Value='<%# Bind("PLINE_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" OnDataBinding="Pline_DataBinding">
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="height:30px"></td>
                                    
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="工序代码">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtProcessCode" runat="server" Width="160px" Value='<%# Bind("PROCESS_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="height:30px"></td>
                                    </tr>
                                    <tr>
                                    <td style="width:8px; height:30px"></td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="工序名称">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtProcessName" runat="server" Width="160px" Value='<%# Bind("PROCESS_NAME") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorText="输入有误，请重新输入！" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="height:30px"></td>
                                    
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="工时">
                                            </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtHour" runat="server" Width="160px" Value='<%# Bind("PROCESS_MANHOUR") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                            <ValidationSettings SetFocusOnError="True"  ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                                <RegularExpression ValidationExpression="^.{0,15}$" ErrorText="工时请输入数字"/>
                                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="height:30px"></td>
                                    </tr>
                                    <tr>
                                    <td style="width:8px; height:30px"></td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td></td>                                    
                                    
                                   <tr>
                                    
                           
                                    <td></td>

                                </tr>
                                
                            
                
                    <%--<tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="合同号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                        <dx:ASPxTextBox ID="projectCode" runat="server" Width="160px" Value='<%# s1 %>'></dx:ASPxTextBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="产品型号">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                        <dx:ASPxTextBox ID="productSeries" runat="server" Width="160px" Value='<%# s2 %>'></dx:ASPxTextBox>
                        </td>
                        <td style="width: 7px">
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
</asp:Content>
