<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd4400.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.ep4400.epd4400" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 279px;
        }
        .style7
        {
            width: 111px;
        }
        .style8
        {
            width: 117px;
        }
        .style9
        {
            width: 113px;
        }
        .style10
        {
            width: 112px;
        }
        .style11
        {
            width: 177px;
        }
        .style12
        {
            width: 274px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="PRO"
            OnRowUpdating="ASPxGridView1_RowUpdating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" Width="60px">
                <EditButton Text="修改" Visible="True">
                </EditButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="项目" FieldName="PRO" 
                ShowInCustomizationForm="True" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="成套产品" ShowInCustomizationForm="True" 
                VisibleIndex="3">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="指标台量（面）" FieldName="ZBCT" 
                        ShowInCustomizationForm="True" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="完成台量（面）" FieldName="WCCT" 
                        ShowInCustomizationForm="True" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="上年同期（面）" FieldName="SNCT" 
                        ShowInCustomizationForm="True" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="同期增减（%）" FieldName="TQCT" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="原件产品" ShowInCustomizationForm="True" 
                VisibleIndex="5">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="指标台量（面）" FieldName="ZBYJ" 
                        ShowInCustomizationForm="True" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="完成台量（面）" FieldName="WCYJ" 
                        ShowInCustomizationForm="True" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="上年同期（台）" FieldName="SNYJ" 
                        ShowInCustomizationForm="True" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="同期增减（%）" FieldName="TQYJ" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="长开公司产品" ShowInCustomizationForm="True" 
                VisibleIndex="7">
                <Columns>
                    <dx:GridViewDataTextColumn Caption="指标台量（面）" FieldName="ZBCK" 
                        ShowInCustomizationForm="True" VisibleIndex="0">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="完成台量（面）" FieldName="WCCK" 
                        ShowInCustomizationForm="True" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="上年同期（台）" FieldName="SNCK" 
                        ShowInCustomizationForm="True" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="同期增减（%）" FieldName="TQCK" 
                        ShowInCustomizationForm="True" VisibleIndex="3">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewBandColumn>
        </Columns>
        <Templates>
        <EditForm>
            <center>
                <table>
                    <tr>
                        <td style="height: 30px" colspan="7">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style12">
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="项目" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="pro" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("PRO") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style12">
                            <dx:ASPxLabel ID="ASPxLabel" runat="server" Text="成套产品指标台量" 
                                AssociatedControlID="txtLocationCode" Width="50px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="zbct" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("ZBCT") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style10">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="成套产品完成台量" 
                                AssociatedControlID="txtLocationCode" Width="50px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="wcct" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("WCCT") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style8">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="成套产品上年同期" 
                                AssociatedControlID="txtLocationCode" Width="50px">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="snct" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("SNCT") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                  
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="成套产品同期增减" 
                                AssociatedControlID="txtLocationCode" Width="50px">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left" class="style1">
                            <dx:ASPxTextBox ID="tqct" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("TQCT") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style12">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="元件产品指标台量" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="zbyj" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("ZBYJ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style7">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="元件产品完成台量" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="wcyj" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("WCYJ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style8">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="元件产品上年同期" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="snyj" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("SNYJ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                  
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="元件产品同期增减" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left" class="style1">
                            <dx:ASPxTextBox ID="tqyj" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("TQYJ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style12">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="长开公司产品指标台量" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="zbck" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("ZBCK") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style7">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="长开公司产品完成台量" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="wcck" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("WCCK") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                  
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style8">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="长开公司产品上年同期" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="snck" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("SNCK") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="长开公司产品同期增减" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left" class="style1">
                            <dx:ASPxTextBox ID="tqck" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("TQCK") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                              
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 50px" colspan="7">
                        </td>
                    </tr>
                </table>
            </center>
        </EditForm>
    </Templates>
    </dx:ASPxGridView>
    </div>
    </form>
</body>
</html>
