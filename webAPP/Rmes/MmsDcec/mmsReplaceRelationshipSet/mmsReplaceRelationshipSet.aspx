<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mmsReplaceRelationshipSet.aspx.cs" Inherits="Rmes.WebApp.Rmes.MmsDCEC.mmsReplaceRelationshipSet.mmsReplaceRelationshipSet" StylesheetTheme="Theme1" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView.Export" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <table style="width:100%;">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width:100px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="维护日期从">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width:120px">
                            <dx:ASPxDateEdit ID="DTQueryFrom" runat="server" Width="100%">
                            </dx:ASPxDateEdit>
                        </td>
                        <td align="center" style="width:60px">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="到">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width:120px">
                            <dx:ASPxDateEdit ID="DTQueryTo" runat="server" Width="100%">
                            </dx:ASPxDateEdit>
                        </td>
                        <td align="right" style="width:60px">
                            <dx:ASPxCheckBox ID="CheckExpire" runat="server" Text="到期">
                            </dx:ASPxCheckBox>
                        </td>
                        <td style="width:120px">
                        </td>
                        <td style="width:60px">
                        </td>
                        <td style="width:180px">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="CONFIG/SO:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtQrySo" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td align="right">
                            <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="BOM零件:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtQryPart" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td align="right">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="替换零件:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtQryPartRep" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td align="right">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="PE文件:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtQryPe" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <table cellspacing="0">
                                <tr>
                                    <td style="width:120px">
                                        <dx:ASPxButton ID="CmdNew" runat="server" Text="新增" Width="100%">
                                            <ClientSideEvents Click="function(s,e){window.open('mmsReplaceRelationshipSetNew.aspx');}" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxButton ID="CmdExp" runat="server" Text="导出" Width="100%" 
                                            onclick="CmdExp_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxButton ID="cmdQuery" runat="server" onclick="btnQrySingle_Click" 
                                            Text="一对一替换查询" Width="100%">
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxButton ID="cmdQuery2" runat="server" onclick="cmdQuery2_Click" 
                                            Text="多对多替换查询" Width="100%">
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width:120px">
                                        <dx:ASPxButton ID="cmdCopy" runat="server" Text="关系复制" Width="100%">
                                            <ClientSideEvents Click="function(s,e){window.open('mmsReplaceRelationshipCopy.aspx');}" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="SO" 
                    Width="100%" onrowdeleting="ASPxGridView1_RowDeleting"  AutoGenerateColumns="False">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0" Width="60px">
                            <DeleteButton Visible="True"></DeleteButton>
                            <ClearFilterButton Visible="True" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="CONFIG或SO" FieldName="SO" VisibleIndex="1" Width="100px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="原零件" FieldName="OLDPART" VisibleIndex="2" Width="80px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="替换件" FieldName="NEWPART" VisibleIndex="3" Width="80px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="数量" FieldName="SL" VisibleIndex="4" Width="60px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="PE代用文件" FieldName="PEFILE"  VisibleIndex="5" Width="120px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="用户" FieldName="CREATEUSER" VisibleIndex="6" Width="80px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="开始时间" FieldName="USETIME" VisibleIndex="7" Width="135px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="结束时间" FieldName="ENDTIME" VisibleIndex="8" Width="135px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="分组" FieldName="THGROUP" VisibleIndex="9" Width="120px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="维护时间" FieldName="CREATETIME" VisibleIndex="10" Width="140px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="系列" FieldName="XL" VisibleIndex="11" Width="120px" Settings-AutoFilterCondition="Contains"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="SITE" Visible="false"></dx:GridViewDataTextColumn>
                    </Columns>
                    
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>

    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" 
        GridViewID="ASPxGridView1">
    </dx:ASPxGridViewExporter>

    </form>
</body>
</html>
