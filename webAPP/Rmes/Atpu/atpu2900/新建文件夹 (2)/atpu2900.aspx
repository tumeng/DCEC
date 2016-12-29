<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Theme1" CodeBehind="atpu2900.aspx.cs"
    Inherits="Rmes.WebApp.Rmes.Atpu.atpu2900.atpu2900" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--功能概述：铭牌参数维护--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script type="text/javascript">
            function initEditSeries(s, e) {
                if (SO.GetValue() == null) {
                    return;
                }
                var webFileUrl = "?SO=" + SO.GetValue() + " &opFlag=getEditSeries";
                var result = "";
                var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
                xmlHttp.open("Post", webFileUrl, false);
                xmlHttp.send("");
                result = xmlHttp.responseText;
                var str1 = "";
                var str2 = "";
                var array1 = result.split('@');
                //             if (result == "") {
                //                 alert("该SO号不存在！")
                //             }

                JX.SetValue(array1[1]);
                GL.SetValue(array1[2]);
                QFJQ.SetValue(array1[3]);
                QFPQ.SetValue(array1[4]);
                PL.SetValue(array1[5]);
                XL.SetValue(array1[6]);
                DS.SetValue(array1[7]);
                FHCX.SetValue(array1[8]);
                XNBH.SetValue(array1[9]);
                DYJX.SetValue(array1[10]);
                ZS.SetValue(array1[11]);
                KHH.SetValue(array1[12]);
                GD.SetValue(array1[13]);


                JZL.SetValue(array1[15]);
                JZL.SetValue(array1[16]);
                EDGYL.SetValue(array1[17]);
                HB.SetValue(array1[18]);

                GL1.SetValue(array1[20]);
                ZS1.SetValue(array1[21]);
                BYGL1.SetValue(array1[22]);
                BYZS1.SetValue(array1[23]);
                BYGL2.SetValue(array1[24]);
                BYZS2.SetValue(array1[25]);
                NOX.SetValue(array1[26]);
                PM.SetValue(array1[27]);
                KHGG.SetValue(array1[28]);
                PEL.SetValue(array1[29]);
                EPA.SetValue(array1[30]);
                PFJD.SetValue(array1[31]);
                PFJDHZH.SetValue(array1[32]);
                FR.SetValue(array1[33]);
                MPLJH.SetValue(array1[34]);
                XZJD.SetValue(array1[35]);
                DYGLD.SetValue(array1[36]);
                ZDJGL.SetValue(array1[37]);
                ZXBZ.SetValue(array1[38]);
                XZMC.SetValue(array1[39]);

                XSHZHHMLY.SetValue(array1[41]);
                HCLZZLX.SetValue(array1[42]);
                SCXKZ.SetValue(array1[43]);
            }

        </script>
        <table>
            <tr>
                <td style="width: 10px">
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="生产线:">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                        ValueField="PLINE_CODE" ValueType="System.String" Width="80px" SelectedIndex="0">
                    </dx:ASPxComboBox>
                </td>
                <td>
                    <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询">
                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
            Width="100%" KeyFieldName="BZSO" OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting"
            OnRowUpdating="ASPxGridView1_RowUpdating" OnRowValidating="ASPxGridView1_RowValidating"
            OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" Settings-ShowHorizontalScrollBar="false">
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
                <dx:GridViewDataTextColumn Caption="生产线" VisibleIndex="1" Width="60px" FieldName="PLINE_CODE"
                    Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="总成" VisibleIndex="3" Width="80px" FieldName="BZSO"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="机型" VisibleIndex="4" Width="80px" FieldName="JX"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="功率" VisibleIndex="5" Width="40px" FieldName="GL"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="转速" VisibleIndex="6" Width="50px" FieldName="ZS"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="气阀进气" VisibleIndex="7" Width="60px" FieldName="QFJQ"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="气阀排气" VisibleIndex="8" Width="60px" FieldName="QFPQ"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="排量" VisibleIndex="9" Width="50px" FieldName="PL"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="怠速" VisibleIndex="10" Width="50px" FieldName="DS"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="系列" VisibleIndex="11" Width="40px" FieldName="XL"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="发火次序" VisibleIndex="12" Width="100px" FieldName="FHCX"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="性能表号" VisibleIndex="13" Width="60px" FieldName="XNBH"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="打印机型" VisibleIndex="14" Width="80px" FieldName="DYJX"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="客户号" VisibleIndex="15" Width="100px" FieldName="KHH"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="固定日期" VisibleIndex="16" Width="100px" FieldName="GD"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="净重量" VisibleIndex="17" Width="50px" FieldName="JZL"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="喷油正时" VisibleIndex="18" Width="60px" FieldName="PYZS"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="额定供油率" VisibleIndex="19" Width="80px" FieldName="EDGYL"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="海拔" VisibleIndex="20" Width="60px" FieldName="HB"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="是否英文" VisibleIndex="21" Width="60px" FieldName="ENYN"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="是否客户号代替总成号" VisibleIndex="22" Width="140px" FieldName="PRSG"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="额定功率二" VisibleIndex="23" Width="80px" FieldName="GL1"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="额定转速二" VisibleIndex="24" Width="80px" FieldName="ZS1"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="备用功率一" VisibleIndex="25" Width="80px" FieldName="BYGL1"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="备用转速一" VisibleIndex="26" Width="80px" FieldName="BYZS1"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="备用功率二" VisibleIndex="27" Width="80px" FieldName="BYGL2"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="备用转速二" VisibleIndex="28" Width="80px" FieldName="BYZS2"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="nox" VisibleIndex="29" Width="60px" FieldName="NOX"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="pm" VisibleIndex="30" Width="60px" FieldName="PM"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="客户规格" VisibleIndex="31" Width="60px" FieldName="KHGG"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产许可证" VisibleIndex="32" Width="100px" FieldName="SCXKZ"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="epa" VisibleIndex="34" Width="60px" FieldName="EPA"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="排放阶段" VisibleIndex="35" Width="60px" FieldName="PFJD"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="排放阶段核准号" VisibleIndex="36" Width="120px" FieldName="PFJDHZH"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="FR参数" VisibleIndex="37" Width="60px" FieldName="FR"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="铭牌零件号" VisibleIndex="38" Width="80px" FieldName="MPLJH"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="限值阶段" VisibleIndex="39" Width="80px" FieldName="XZJD"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="对应功率段" VisibleIndex="40" Width="80px" FieldName="DYGLD"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="最大净功率" VisibleIndex="41" Width="70px" FieldName="ZDJGL"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="执行标准" VisibleIndex="42" Width="70px" FieldName="ZXBZ"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="系族名称" VisibleIndex="43" Width="80px" FieldName="XZMC"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="形式核准号豁免理由" VisibleIndex="44" Width="120px" FieldName="XSHZHHMLY"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="后处理装置类型" VisibleIndex="45" Width="100px" FieldName="HCLZZLX"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="编辑日期" VisibleIndex="45" Width="135px" FieldName="MODIFYDATE"
                    Settings-AutoFilterCondition="Contains">
                    <Settings AutoFilterCondition="Contains"></Settings>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
            <SettingsEditing PopupEditFormWidth="1000px"></SettingsEditing>
            <Templates>
                <EditForm>
                    <table>
                        <tr>
                            <td style="height: 34px">
                                <span style="color: Blue; font-size: 10pt">输入一个SO号，按“回车”键，可带入该SO的相关参数信息</span>
                            </td>
                             
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="总成" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtSO" runat="server" Width="100px" Text='<%# Bind("BZSO") %>'
                                    ClientInstanceName="SO" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ClientSideEvents Init="function(s, e) { initEditSeries(s, e) ;}" />
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="机型" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtJX" runat="server" Width="100px" Text='<%# Bind("JX") %>'
                                    ClientInstanceName="JX" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="系列" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtXL" runat="server" Width="100px" Text='<%# Bind("XL") %>'
                                    ClientInstanceName="XL" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                        <RequiredField IsRequired="True" ErrorText="不能为空！" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="额定功率二" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtGL1" runat="server" Width="100px" Text='<%# Bind("GL1") %>'
                                    ClientInstanceName="GL1" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="排放阶段核准号" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtPFJDHZH" runat="server" Width="100px" Text='<%# Bind("PFJDHZH") %>'
                                    ClientInstanceName="PFJDHZH" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="功率" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtGL" runat="server" Width="100px" Text='<%# Bind("GL") %>'
                                    ClientInstanceName="GL" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="进气" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtQFJQ" runat="server" Width="100px" Text='<%# Bind("QFJQ") %>'
                                    ClientInstanceName="QFJQ" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="排气" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtQFPQ" runat="server" Width="100px" Text='<%# Bind("QFPQ") %>'
                                    ClientInstanceName="QFPQ" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="额定转速二" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtZS1" runat="server" Width="100px" Text='<%# Bind("ZS1") %>'
                                    ClientInstanceName="ZS1" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="形式核准号豁免理由" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtXSHZHHMLY" runat="server" Width="100px" Text='<%# Bind("XSHZHHMLY") %>'
                                    ClientInstanceName="XSHZHHMLY" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="排量" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtPL" runat="server" Width="100px" Text='<%# Bind("PL") %>'
                                    ClientInstanceName="PL" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="怠速" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtDS" runat="server" Width="100px" Text='<%# Bind("DS") %>'
                                    ClientInstanceName="DS" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="发火次序" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtFHCX" runat="server" Width="100px" Text='<%# Bind("FHCX") %>'
                                    ClientInstanceName="FHCX" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="备用功率一" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtBYGL1" runat="server" Width="100px" Text='<%# Bind("BYGL1") %>'
                                    ClientInstanceName="BYGL1" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="后处理装置类型" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtHCLZZLX" runat="server" Width="100px" Text='<%# Bind("HCLZZLX") %>'
                                    ClientInstanceName="HCLZZLX" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="转速" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtZS" runat="server" Width="100px" Text='<%# Bind("ZS") %>'
                                    ClientInstanceName="ZS" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="性能表号" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtXNBH" runat="server" Width="100px" Text='<%# Bind("XNBH") %>'
                                    ClientInstanceName="XNBH" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="打印机型" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtDYJX" runat="server" Width="100px" Text='<%# Bind("DYJX") %>'
                                    ClientInstanceName="DYJX" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="备用转速一" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtBYZS1" runat="server" Width="100px" Text='<%# Bind("BYZS1") %>'
                                    ClientInstanceName="BYZS1" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="生产许可证" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtSCXKZ" runat="server" Width="100px" Text='<%# Bind("SCXKZ") %>'
                                    ClientInstanceName="SCXKZ" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="海拔" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtHB" runat="server" Width="100px" Text='<%# Bind("HB") %>'
                                    ClientInstanceName="HB" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="客户规格" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtKHGG" runat="server" Width="100px" Text='<%# Bind("KHGG") %>'
                                    ClientInstanceName="KHGG" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="客户号" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtKHH" runat="server" Width="100px" Text='<%# Bind("KHH") %>'
                                    ClientInstanceName="KHH" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="固定日期" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtGD" runat="server" Width="100px" Text='<%# Bind("GD") %>'
                                    ClientInstanceName="GD" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="是否客户号代替总成" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxComboBox ID="txtPRSG" runat="server" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Value='<%# Bind("PRSG") %>' Width="100px" DropDownStyle="DropDownList" ClientInstanceName="PRSG">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                        ValidateOnLeave="True">
                                        <RegularExpression ErrorText="长度不能超过3！" ValidationExpression="^.{0,3}$" />
                                    </ValidationSettings>
                                    <Items>
                                        <dx:ListEditItem Text="否" Value="No" />
                                        <dx:ListEditItem Text="是" Value="Yes" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="PEL:" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtPEL" runat="server" Width="100px" Text='<%# Bind("PEL") %>'
                                    ClientInstanceName="PEL" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="喷油正时" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtPYZS" runat="server" Width="100px" Text='<%# Bind("PYZS") %>'
                                    ClientInstanceName="PYZS" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text="排放阶段" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtPFJD" runat="server" Width="100px" Text='<%# Bind("PFJD") %>'
                                    ClientInstanceName="PFJD" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel30" runat="server" Text="备用功率二" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtBYGL2" runat="server" Width="100px" Text='<%# Bind("BYGL2") %>'
                                    ClientInstanceName="BYGL2" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="额定供油率" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtEDGYL" runat="server" Width="100px" Text='<%# Bind("EDGYL") %>'
                                    ClientInstanceName="EDGYL" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel42" runat="server" Text="FR" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtFR" runat="server" Width="100px" Text='<%# Bind("FR") %>'
                                    ClientInstanceName="FR" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="执行标准" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtZXBZ" runat="server" Width="100px" Text='<%# Bind("ZXBZ") %>'
                                    ClientInstanceName="ZXBZ" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel34" runat="server" Text="系族名称" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtXZMC" runat="server" Width="100px" Text='<%# Bind("XZMC") %>'
                                    ClientInstanceName="XZMC" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="备用转速二" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtBYZS2" runat="server" Width="100px" Text='<%# Bind("BYZS2") %>'
                                    ClientInstanceName="BYZS2" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel36" runat="server" Text="铭牌零件号" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtMPLJH" runat="server" Width="100px" Text='<%# Bind("MPLJH") %>'
                                    ClientInstanceName="MPLJH" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="NOX:" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtNOX" runat="server" Width="100px" Text='<%# Bind("NOX") %>'
                                    ClientInstanceName="NOX" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="PM:" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtPM" runat="server" Width="100px" Text='<%# Bind("PM") %>'
                                    ClientInstanceName="PM" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="净重量" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtJZL" runat="server" Width="100px" Text='<%# Bind("JZL") %>'
                                    ClientInstanceName="JZL" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="最大净功率" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtZDJGL" runat="server" Width="100px" Text='<%# Bind("ZDJGL") %>'
                                    ClientInstanceName="ZDJGL" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="对应功率段" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="txtDYGLD" runat="server" Width="100px" Text='<%# Bind("DYGLD") %>'
                                    ClientInstanceName="DYGLD" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="EPA:" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtEPA" runat="server" Width="100px" Text='<%# Bind("EPA") %>'
                                    ClientInstanceName="EPA" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="限值阶段" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxTextBox ID="txtXZJD" runat="server" Width="100px" Text='<%# Bind("XZJD") %>'
                                    ClientInstanceName="XZJD" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Visible="True">
                                    <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                        ErrorDisplayMode="ImageWithTooltip">
                                        <RegularExpression ErrorText="长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </td>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="是否英文" Visible="True">
                                </dx:ASPxLabel>
                            </td>
                            <td style="width: 100px">
                                <dx:ASPxComboBox ID="txtENYN" runat="server" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>"
                                    Value='<%# Bind("ENYN") %>' Width="100px" ClientInstanceName="ENYN">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="输入有误，请重新输入！" SetFocusOnError="True"
                                        ValidateOnLeave="True">
                                        <RegularExpression ErrorText="长度不能超过3！" ValidationExpression="^.{0,3}$" />
                                    </ValidationSettings>
                                    <Items>
                                        <dx:ListEditItem Text="否" Value="No" />
                                        <dx:ListEditItem Text="是" Value="Yes" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 30px; text-align: right;">
                                <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" runat="server" ReplacementType="EditFormUpdateButton" />
                                <dx:ASPxGridViewTemplateReplacement ID="CancelButton" runat="server" ReplacementType="EditFormCancelButton" />
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
        <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
            ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
        <asp:SqlDataSource ID="IfRep" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
            ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
        <asp:SqlDataSource ID="IfEng" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
            ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
