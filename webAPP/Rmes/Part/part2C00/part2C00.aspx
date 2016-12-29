<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part2C00.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part2C00.part2C00" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%--现场缺料查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table>
    </table>
    <table>
        <tr>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="生产线">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPlineCode" DataSourceID="SqlCode" ValueField="pline_code"
                    TextField="pline_name" runat="server" DropDownStyle="DropDownList" Width="70px"
                    ClientInstanceName="Pcode">
                    
                </dx:ASPxComboBox>
            </td>
           
                <td>
                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="日期:" />
                </td>
                <td>
                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="120px">
                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="到" />
                </td>
                <td>
                    <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="120px">
                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true">
                        </CalendarProperties>
                    </dx:ASPxDateEdit>
                </td>
                <td style="width: 20px;">
                </td>
                <td>
                    <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" AutoPostBack="False" Width="80px">
                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                    </dx:ASPxButton>
                </td>
               
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="ROWID"
        AutoGenerateColumns="False" 
        OnCustomCallback="ASPxGridView1_CustomCallback" 
        oncustombuttoncallback="ASPxGridView1_CustomButtonCallback" >
        <Columns>
            <dx:GridViewCommandColumn Caption="操作" VisibleIndex="0" Width="50px">
                <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="delSure" Text="确认">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="GZDD" VisibleIndex="1" Width="60px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="站点名称" FieldName="ZDMC" VisibleIndex="2"
                Width="100px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="描述" FieldName="NOTE" VisibleIndex="2"
                Width="260px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="状态" FieldName="STATUS" VisibleIndex="3" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="时间" FieldName="RQSJ" VisibleIndex="7"
                Width="140px" Settings-AutoFilterCondition="Contains">
                <PropertiesTextEdit DisplayFormatString="yyyy-MM-dd">
                </PropertiesTextEdit>
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="员工名称" FieldName="YGMC" VisibleIndex="7"
                Width="80px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="零件代码" FieldName="LJDM" VisibleIndex="8" Width="120px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="序号" FieldName="ROWID" VisibleIndex="9" Width="140px" Visible="false"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="是否解决" FieldName="ISDELETE" VisibleIndex="11" Visible="false"
                Width="100px" Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="是否解决" FieldName="ISDELETE1" VisibleIndex="11"
                Width="100px" Settings-AutoFilterCondition="Contains" CellStyle-HorizontalAlign="Center">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
   
</asp:Content>
