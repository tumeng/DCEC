<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Part_part1700_part1700"
    StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" CodeBehind="part1700.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
 <%--到货差异调整--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function changeSeq(s, e) {
            if (confirm("进行调整将会修改回冲池数量，是否确认？？")) {
            }
            else {
                alert("调整操作已取消！");

                return;
            }
            var index = e.visibleIndex;
            var buttonID = e.buttonID;
            grid.GetValuesOnCustomCallback(buttonID + '|' + index, GetDataCallback);
            
        }


        function GetDataCallback(s) {
            var result = "";
            var retStr = "";
            if (s == null) {
                grid.PerformCallback();
                return;
            }
            var array = s.split(',');
            retStr = array[1];
            result = array[0];

            switch (result) {
                case "OK":
                    alert(retStr);
                    grid.PerformCallback(); 
                    return;
                case "Fail":
                    alert(retStr);
                    grid.PerformCallback();
                    return;
            }
             
        }

//        String.prototype.endWith = function (endStr) {
//            var d = this.length - endStr.length;
//            return (d >= 0 && this.lastIndexOf(endStr) == d)
//        }

    </script>
    <table style="background-color: #99bbbb; width: 100%;">
        <tr>
            <td style="width: 5px; height: 25px;">
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="生产线选择:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                    ValueField="PLINE_CODE" ValueType="System.String" Width="140px" SelectedIndex="0">
                </dx:ASPxComboBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="零件号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="textMcode" runat="server" Width="140px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="供应商代码:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="textGcode" runat="server" Width="140px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
        </tr>
        <tr>
            <td style="width: 5px; height: 25px;">
            </td>
            <td style="text-align: left; width: 70px;">
                <label style="font-size: small">
                    开始日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="140px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 5px">
            </td>
            <td style="text-align: left; width: 70px">
                <label style="font-size: small">
                    结束日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="140px">
                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                    </CalendarProperties>
                </dx:ASPxDateEdit>
            </td>
            <td>
            </td>
             
            <td style="width: 100px;">
                <dx:ASPxButton ID="ButSubmit" Text="查询" Width="90px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            
           <%-- <td>
                <dx:ASPxButton ID="btnAdjust" runat="server" Text="调整" AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e) { Adjust(); }" />
                </dx:ASPxButton>
            </td>
            <td style="width: auto">
            </td>--%>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="MATERIAL_CODE;MATERIAL_NUM;GZDD;QADSITE;PTP_BUYER"
        SettingsPager-Mode="ShowPager" AutoGenerateColumns="False" OnCustomCallback="ASPxGridView1_CustomCallback"
        OnCustomDataCallback="ASPxGridView1_CustomDataCallback"  >
        <Settings ShowHorizontalScrollBar="true" />
       

        <SettingsEditing PopupEditFormWidth="600px" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                Width="60px">
                <HeaderTemplate>
                    <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="全选/取消全选" ClientSideEvents-CheckedChanged="function(s, e) { grid.SelectAllRowsOnPage(s.GetChecked()); }"
                        Style="margin-bottom: 0px" />
                </HeaderTemplate>
                <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="操作" Width="80px" ButtonType="Button">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Adjust" Text="调整">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
             
            <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
             <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD"  Visible="false" Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="物料代码" FieldName="MATERIAL_CODE" Width="80px"
                Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="供应商代码" FieldName="GYS_CODE" Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="差异数量" FieldName="MATERIAL_NUM" Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="QAD地点" FieldName="QADSITE" Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划员" FieldName="PTP_BUYER" Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="处理标识" FieldName="FLAG" Width="80px" Settings-AutoFilterCondition="Contains">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="开始日期" FieldName="CREATE_TIME" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption=" " Width="80%" />
        </Columns>
         <Settings ShowFooter="True" />
        <SettingsBehavior ColumnResizeMode="Control" />
        <ClientSideEvents CustomButtonClick="function (s,e){
        changeSeq(s,e);
    }" />

    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
   
    
</asp:Content>
