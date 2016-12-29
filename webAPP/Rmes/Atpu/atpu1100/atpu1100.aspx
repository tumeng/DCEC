<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="atpu1100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Atpu.atpu1100.atpu1100" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--FR参数查询界面--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False"  KeyFieldName="FR"
            Width="100%">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="50px">
           
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn Caption="FR组" VisibleIndex="1" 
                    Width="80px" FieldName="FR" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="机型" VisibleIndex="2" 
                    Width="120px" FieldName="JX" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="排量" VisibleIndex="3" 
                    Width="60px" FieldName="PL" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="系列" VisibleIndex="4" 
                    Width="60px" FieldName="XL" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="最大净功率" VisibleIndex="5" 
                    Width="80px" FieldName="ZDJGL" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="怠速" VisibleIndex="6" 
                    Width="60px" FieldName="DS" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="执行标准" VisibleIndex="7" 
                    Width="80px" FieldName="ZXBZ" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="型式豁免" VisibleIndex="8" 
                    Width="60px" FieldName="XSHZHMLY" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="生产许可证" VisibleIndex="9" 
                    Width="120px" FieldName="SCXKZH" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="气阀冷态" VisibleIndex="10" 
                    Width="80px" FieldName="QFLTJX" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="海拔" VisibleIndex="11" 
                    Width="60px" FieldName="HB" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="后处理" VisibleIndex="12" 
                    Width="60px" FieldName="HCLZZLX" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="性能表号" VisibleIndex="13" 
                    Width="60px" FieldName="XNBH" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="净重量" VisibleIndex="14" 
                    Width="60px" FieldName="JZL" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="功率" VisibleIndex="15" 
                    Width="60px" FieldName="GL" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="转速" VisibleIndex="16" 
                    Width="60px" FieldName="ZS" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="限制阶段" VisibleIndex="17" 
                    Width="100px" FieldName="XZJD" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="额定供油率" VisibleIndex="18" 
                    Width="80px" FieldName="EDGYL" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="排放阶段核准号" VisibleIndex="19" 
                    Width="140px" FieldName="XSHZH" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="对应功率段" VisibleIndex="20" 
                    Width="100px" FieldName="DYGLD" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="额定功率二" VisibleIndex="21" 
                    Width="80px" FieldName="EDGL2" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="额定转速二" VisibleIndex="22" 
                    Width="80px" FieldName="EDZS2" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="备用功率1" VisibleIndex="23" 
                    Width="80px" FieldName="BYGL1" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="备用功率2" VisibleIndex="24" 
                    Width="80px" FieldName="BYGL2" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="备用转速2" VisibleIndex="25" 
                    Width="80px" FieldName="BYZS2" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="系族名称" VisibleIndex="26" 
                    Width="120px" FieldName="XZMC" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="工作地点" VisibleIndex="27" 
                    Width="60px" FieldName="GZDD" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="操作员" VisibleIndex="28" 
                    Width="60px" FieldName="YG" Settings-AutoFilterCondition="Contains">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
    </div>
    </form>
</body>
</html>
