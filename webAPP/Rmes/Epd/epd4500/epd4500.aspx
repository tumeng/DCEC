<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd4500.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd4500.epd4500" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style5
        {
            width: 33px;
        }
        .style6
        {
            width: 41px;
        }
        .style7
        {
            width: 43px;
        }
        .style8
        {
            width: 32px;
        }
        .style9
        {
            width: 64px;
        }
        .style10
        {
            width: 52px;
        }
        .style11
        {
            width: 55px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="PRO"
            OnRowUpdating="ASPxGridView1_RowUpdating" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
            Width="1124px">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0">
                    <EditButton Text="修改" Visible="True">
                    </EditButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="全年合计" FieldName="YEAR_TOTAL" 
                    VisibleIndex="10">
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewBandColumn Caption="一季度" VisibleIndex="12">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="1月" FieldName="JAN" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="2月" FieldName="FEB" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="3月" FieldName="MAR" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="小计" FieldName="TOTAL1" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="二季度" VisibleIndex="14">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="4月" FieldName="APR" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="5月" FieldName="MAY" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="6月" FieldName="JUNE" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="小计" FieldName="TOTAL2" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="三季度" VisibleIndex="16">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="7月" FieldName="JULY" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="8月" FieldName="AUG" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="9月" FieldName="SEP" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="小计" FieldName="TOTAL3" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewBandColumn>
                <dx:GridViewBandColumn Caption="四季度" VisibleIndex="18">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="10月" FieldName="OCT" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="11月" FieldName="NOV" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="12月" FieldName="DEC" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="小计" FieldName="TOTAL4" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewBandColumn>
                <dx:GridViewDataTextColumn Caption="项目" FieldName="PRO" VisibleIndex="1" 
                    Width="70px">
                    <HeaderStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager Visible="False">
            </SettingsPager>
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
                        <td style="text-align: left" class="style10">
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
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="全年合计" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="yeartotal" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("YEAR_TOTAL") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style10">
                            <dx:ASPxLabel ID="ASPxLabel" runat="server" Text="1月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="jan" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("JAN") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="2月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="feb" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("FEB") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style11">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="3月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="mar" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("MAR") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                  
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style5">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="小计" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left" class="style8">
                            <dx:ASPxTextBox ID="total1" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("TOTAL1") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style10">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="4月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="apr" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("APR") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                   
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="5月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="may" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("MAY") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style11">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="6月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="june" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("JUNE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                  
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style5">
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="小计" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left" class="style8">
                            <dx:ASPxTextBox ID="total2" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("TOTAL2") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style10">
                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="7月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="july" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("JULY") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="8月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="aug" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("AUG") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                  
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style11">
                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="9月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="sep" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("SEP") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style5">
                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="小计" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left" class="style8">
                            <dx:ASPxTextBox ID="total3" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("TOTAL3") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                              
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style10">
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="10月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="oct" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("OCT") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style9">
                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="11月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="nov" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("NOV") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                  
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style11">
                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="12月" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px" align="left">
                            <dx:ASPxTextBox ID="dec" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("DEC") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="工位代码字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                 
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8px; height: 30px">
                        </td>
                        <td style="text-align: left" class="style5">
                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="小计" AssociatedControlID="txtLocationCode">
                            </dx:ASPxLabel>
                        </td>
                        <td align="left" class="style8">
                            <dx:ASPxTextBox ID="total4" EnableClientSideAPI="True" runat="server" Width="150px"
                                Text='<%# Bind("TOTAL4") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
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
