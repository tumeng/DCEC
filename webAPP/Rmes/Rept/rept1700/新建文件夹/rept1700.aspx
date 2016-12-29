<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="rept1700.aspx.cs" Inherits="Rmes_Rept_rept1700_rept1700"  StylesheetTheme="Theme1"   %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--单机装配BOM查询--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function initEditSeries(s, e) {
            if (SN.GetValue() == null) {
                return;
            }
            if (SN.GetText().length != 8) {
                alert("流水号长度非法！")
                return;
             }
            var webFileUrl = "?SN=" + SN.GetValue() + " &opFlag=getEditSeries";
            var result = "";
            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
            xmlHttp.open("Post", webFileUrl, false);
            xmlHttp.send("");
            result = xmlHttp.responseText;

            var str1 = "";
            var str2 = "";
            var array1 = result.split('@');

            if (result == "") {
                alert("该流水号不存在！")
            }
            else {
                PModel.SetValue(array1[0]);
                SO.SetValue(array1[1]);
                PlanCode.ClearItems();
                for (var i = 0; i < array1.length - 1; i++) {
                    PlanCode.AddItem(array1[i+2], array1[i+2]);
                }

            }
            

        }

    </script>
    <table>
        <tr>
            <td style="width: 10px">
            </td>
            
           <td>
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="流水号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSN" runat="server" ValueField="SN" ValueType="System.String"
                    Width="120px"  ClientInstanceName="SN" >
                    <ClientSideEvents TextChanged ="function(s,e){
                        initEditSeries(s, e) ;
                        
                    }" />
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="计划号:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtPlanCode" runat="server"   ValueField="SN" ValueType="System.String"
                    Width="120px" ClientInstanceName="PlanCode"   >
                </dx:ASPxComboBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtSO" runat="server" ValueField="plan_so" ValueType="System.String"
                    Width="100px" ClientInstanceName="SO">
                </dx:ASPxTextBox>
            </td>
        </tr>
         
        <tr>
            <td>
            </td>
            
            <td>
                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="机型:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtPModel" runat="server"  ValueType="System.String"
                    Width="120px"  ClientInstanceName="PModel">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="查询内容：" Width="60px">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxComboBox ID="txtChose" runat="server" ValueType="System.String" Width="120px" SelectedIndex="0"
                    ClientInstanceName="Chose" >
                    <Items>
                        <dx:ListEditItem Text="装机清单查询" Value="A" />
                        <dx:ListEditItem Text="标准BOM查询" Value="B" />
                         
                        
                    </Items>
                     
                </dx:ASPxComboBox>
            </td>
            <td></td>
            <td>
                <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();grid2.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table>
    <tr>
    <td>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="True"
        OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="零件代码"  Width="700px" >
          <ClientSideEvents BeginCallback="function(s, e) 
                                {
	                                grid.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                
                                    callbackName =  grid.cpCallbackName;
                                    theRet = grid.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                        alert(theRet);
                                    }
                                    if(callbackName == 'OK') 
                                    {
                                         
                                    }
                                   
                                }" />
         
    </dx:ASPxGridView>
    </td>
    <td style="width: 80%">
                                        <fieldset style="text-align: left;">
                                            <legend><span style="font-size: 10pt; width: auto">
                                                <asp:Label ID="Label5" runat="server" Text="替换零件" Font-Bold="True"></asp:Label></span></legend>
                                            <table width="100%">
                                                <tr>
                                                    <td style="text-align: left">

    <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" AutoGenerateColumns="False"
        Width="300px" KeyFieldName="COMP" OnCustomCallback="ASPxGridView2_CustomCallback">
            <columns>
            <dx:GridViewDataTextColumn Caption="零件" FieldName="LJDM2" VisibleIndex="1" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="替换零件" FieldName="LJDM2" VisibleIndex="2" Width="100px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="工位" FieldName="GWMC" VisibleIndex="3" Width="80px"
                Settings-AutoFilterCondition="Contains">
                <Settings AutoFilterCondition="Contains"></Settings>
            </dx:GridViewDataTextColumn>
             
            <dx:GridViewDataTextColumn VisibleIndex="19" Width="80%" />
        </columns>
    </dx:ASPxGridView>
    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
    </td>
    </tr>
    </table>
    <%--<asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
     --%>
     <asp:SqlDataSource ID="SqlPlanCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
     
</asp:Content>
