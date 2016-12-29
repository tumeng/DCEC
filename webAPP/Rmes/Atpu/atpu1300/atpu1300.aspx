<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Theme="Theme1"  CodeBehind="atpu1300.aspx.cs"  Inherits="Rmes.WebApp.Rmes.Atpu.atpu1300.atpu1300" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%--QAD计划查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <tr>
            <td>
               <table>
               <tr>
                 <td>
                           
                        </td>
 <td><dx:ASPxLabel ID="ASPxLabel9" runat="server"  Text="日期:"  /></td>
                        <td><dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Date='<%# theBeginDate %>' Width="120px" >
                            <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true"></CalendarProperties>
                            </dx:ASPxDateEdit></td>
                        <td><dx:ASPxLabel ID="ASPxLabel10" runat="server"  Text="--"  /></td>
                        <td><dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Date='<%# theEndDate %>' Width="120px" >
                            <CalendarProperties ClearButtonText="清空" TodayButtonText="今天" ShowWeekNumbers="true"></CalendarProperties>
                            </dx:ASPxDateEdit></td>
                            <td>
                            </td>
                             <td>
                          <dx:ASPxButton ID="btnQuery" runat="server" Text="查询" UseSubmitBehavior="False" 
                                 onclick="btnQuery_Click"  />
                        </td>
                            </tr>
                           
                    
                </table>
            </td>
        </tr>
        <tr>
        <td>
                &nbsp;</td>
        </tr>

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RPS_PART">
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="50px"> 
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
       
        <dx:GridViewDataTextColumn Caption="SO号" FieldName="RPS_PART" VisibleIndex="1" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="数量" FieldName="RPS_QTY_REQ" VisibleIndex="4" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="工艺地点" FieldName="RPS_SITE" VisibleIndex="5" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="生产线" FieldName="RPS_LINE" VisibleIndex="6" Width="80px" Settings-AutoFilterCondition="Contains">
        </dx:GridViewDataTextColumn>

     
        <dx:GridViewDataDateColumn Caption="时间1" FieldName="RPS_DUE_DATE" VisibleIndex="2" Width="120px" Settings-AutoFilterCondition="Contains">
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewDataDateColumn>
        <dx:GridViewDataDateColumn Caption="时间2" FieldName="RPS_REL_DATE" VisibleIndex="3" Width="120px" Settings-AutoFilterCondition="Contains">
            <CellStyle Wrap="False">
            </CellStyle>
        </dx:GridViewDataDateColumn>
       
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>

</asp:Content>