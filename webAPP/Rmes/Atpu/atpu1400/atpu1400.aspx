<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Theme1" CodeBehind="atpu1400.aspx.cs"
    Inherits="Rmes.WebApp.Rmes.Atpu.atpu1400.atpu1400" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--铭牌及VEPS更新--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="true"
            ActiveTabIndex="0">
            <TabPages>
                <dx:TabPage Text="铭牌参数查询" Visible="true">
                
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                         <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="计划号:" />
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPlan" runat="server" Width="120px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="查询" UseSubmitBehavior="False"
                                        OnClick="btnCx1_Click" Height="21px" Width="93px" />
                                </td>
                            </tr>
                        </table>
                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" KeyFieldName="FR"
                                Width="100%">
                                <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="50px">
                                        <ClearFilterButton Visible="True">
                                        </ClearFilterButton>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="生产线" VisibleIndex="1" Width="60px" FieldName="PLINE_CODE" Visible="false"
                                        Settings-AutoFilterCondition="Contains">
                                        <PropertiesComboBox DataSourceID="boxpline" ValueField="PLINE_CODE" TextField="PLINE_NAME"
                                            IncrementalFilteringMode="StartsWith" ValueType="System.String" DropDownStyle="DropDownList" />
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataTextColumn Caption="计划号" VisibleIndex="2" Width="120px" FieldName="PLAN_CODE"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="总成" VisibleIndex="3" Width="80px" FieldName="BZSO"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="机型" VisibleIndex="4" Width="80px" FieldName="JX"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="功率" VisibleIndex="5" Width="40px" FieldName="GL"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="转速" VisibleIndex="6" Width="50px" FieldName="ZS"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="气阀进气" VisibleIndex="7" Width="60px" FieldName="QFJQ"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="气阀排气" VisibleIndex="8" Width="60px" FieldName="QFPQ"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="排量" VisibleIndex="9" Width="50px" FieldName="PL"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="怠速" VisibleIndex="10" Width="50px" FieldName="DS"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="系列" VisibleIndex="11" Width="40px" FieldName="XL"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="发火次序" VisibleIndex="12" Width="100px" FieldName="FHCX"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="性能表号" VisibleIndex="13" Width="60px" FieldName="XNBH"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="打印机型" VisibleIndex="14" Width="80px" FieldName="DYJX"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="客户号" VisibleIndex="15" Width="120px" FieldName="KHH"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="固定日期" VisibleIndex="16" Width="100px" FieldName="GD"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="净重量" VisibleIndex="17" Width="50px" FieldName="JZL"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="喷油正时" VisibleIndex="18" Width="60px" FieldName="PYZS"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="额定供油率" VisibleIndex="19" Width="80px" FieldName="EDGYL"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="海拔" VisibleIndex="20" Width="60px" FieldName="HB"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="是否英文" VisibleIndex="21" Width="60px" FieldName="ENYN"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="是否客户号代替总成号" VisibleIndex="22" Width="140px" FieldName="PRSG"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="额定功率二" VisibleIndex="23" Width="80px" FieldName="GL1"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="额定转速二" VisibleIndex="24" Width="80px" FieldName="ZS1"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="备用功率一" VisibleIndex="25" Width="80px" FieldName="BYGL1"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="备用转速一" VisibleIndex="26" Width="80px" FieldName="BYZS1"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="备用功率二" VisibleIndex="27" Width="80px" FieldName="BYGL2"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="备用转速二" VisibleIndex="28" Width="80px" FieldName="BYZS2"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="nox" VisibleIndex="29" Width="60px" FieldName="NOX"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="pm" VisibleIndex="30" Width="60px" FieldName="PM"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="客户规格" VisibleIndex="31" Width="60px" FieldName="KHGG"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="生产许可证" VisibleIndex="32" Width="120px" FieldName="SCXKZ"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="epa" VisibleIndex="34" Width="60px" FieldName="EPA"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="排放阶段" VisibleIndex="35" Width="60px" FieldName="PFJD"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="排放阶段核准号" VisibleIndex="36" Width="140px" FieldName="PFJDHZH"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="FR参数" VisibleIndex="37" Width="60px" FieldName="FR"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="铭牌零件号" VisibleIndex="38" Width="80px" FieldName="MPLJH"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="限值阶段" VisibleIndex="39" Width="80px" FieldName="XZJD"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="对应功率段" VisibleIndex="40" Width="80px" FieldName="DYGLD"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="最大净功率" VisibleIndex="41" Width="70px" FieldName="ZDJGL"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="执行标准" VisibleIndex="42" Width="70px" FieldName="ZXBZ"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="系族名称" VisibleIndex="43" Width="120px" FieldName="XZMC"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="形式核准号豁免理由" VisibleIndex="44" Width="120px" FieldName="XSHZHHMLY"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="后处理装置类型" VisibleIndex="45" Width="100px" FieldName="HCLZZLX"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="最大净功率转速" VisibleIndex="46" Width="100px" FieldName="ZDJGLZS"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                            <asp:SqlDataSource ID="boxpline" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
                                ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
                <dx:TabPage Text="VEPS更新" Visible="true">

                    <ContentCollection>
                        
                        <dx:ContentControl ID="ContentControl1" runat="server">
                        <table>
                            <tr>
                                <td style="width: 10px">
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO号:" />
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtSoQry" runat="server" Width="120px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="查询" UseSubmitBehavior="False"
                                        OnClick="btnCx_Click" Height="21px" Width="93px" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnUpdate" runat="server" Text="手动更新" UseSubmitBehavior="False"
                                        OnClick="btnUpdate_Click" Height="21px" Width="93px" />
                                </td>

                            </tr>
                        </table>
                            <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                                Width="100%" KeyFieldName="SO">
                                <Columns>
                                    <dx:GridViewCommandColumn Caption="  " VisibleIndex="0" Width="50px">
                                        <ClearFilterButton Visible="True" />
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="SO" VisibleIndex="1" Width="100px" FieldName="SO"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="地址值" VisibleIndex="2" Width="100px" FieldName="地址值"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="设定值" VisibleIndex="3" Width="60px" FieldName="设定值"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="版本号" VisibleIndex="4" Width="80px" FieldName="版本号"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="CVOL_ID" VisibleIndex="5" Width="160px" FieldName="CVOL_ID"
                                        Settings-AutoFilterCondition="Contains">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl>
    </div>
    </form>
</body>
</html>
