<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="atpu1E01.aspx.cs" Inherits="Rmes.WebApp.Rmes.Atpu.atpu1E00.atpu1E01" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <table width="850px">
            <tr style="height: 20px">
                <td colspan="2"></td>
                <td colspan="3" align="left">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择生产线、站点，录入事件代码、名称，确定提交" Font-Size="Medium" Width="400px">
                    </dx:ASPxLabel>
                </td>
                
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px">
                </td>
                <td style="width: 80px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 300px">
                    <dx:ASPxComboBox ID="comboPLine" runat="server" EnableClientSideAPI="True"  DropDownStyle="DropDownList" 
                        DataSourceID="SqlDataSource4" ValueType="System.String" Width="250px" ClientInstanceName="comboPLineC"
                        TextField="PLINE_NAME" ValueField="PLINE_CODE" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStation(); }" />
                        </dx:ASPxComboBox>
                </td>
                <td style="width: 150px">
                </td>
                <td style="width: 300px">
                </td>
            </tr>


            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="站点名称">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboStationName" runat="server" ClientInstanceName="zdmc_C" EnableClientSideAPI="True"  DropDownStyle="DropDownList" 
                        ValueType="System.String" Width="250px" OnCallback="comboStationName_Callback" 
                        TextField="STATION_NAME" ValueField="STATION_CODE" >
                        </dx:ASPxComboBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="事件代码">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtSJDM" runat="server" ClientInstanceName="sjdm_C" Text='<%# Bind("SJDM") %>' 
                                Width="250px" >
                            </dx:ASPxTextBox>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>    
            <tr style="height:30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="事件名称">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtSJMC" runat="server" ClientInstanceName="sjmc_C" Text='<%# Bind("SJMC") %>' 
                                Width="250px" >
                            </dx:ASPxTextBox>
                </td>
                <td valign="middle" align="center" style="padding: 10px; width: 10%">
                        
                    </td>
                <td>
                </td>
            </tr>   
               
                    
            <tr style="height: 50px">
                <td>
                </td>
                <td>
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="Button1" runat="server" OnClientClick="return checkSubmit();" onclick="butConfirm_Click" Text="确定" Width="100px" Height="30px" />
                    &nbsp; 
                    <asp:Button ID="Button2" runat="server" onclick="butCloseWindow_Click" Text="关闭窗口" Width="100px" Height="30px" />

                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>

    </div>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
    </form>
</body>
<script type="text/javascript">
    var pline;
//    var stationCodeL;
    function filterStation() {
        pline = comboPLineC.GetValue().toString();

        zdmc_C.PerformCallback(pline);
    }
    function checkSubmit() {
        if (comboPLineC.GetSelectedIndex() == -1 || zdmc_C.GetSelectedIndex() == -1 || sjdm_C.GetValue().toString() == "" || sjmc_C.GetValue().toString() == "") {
            alert("请选择生产线、站点、录入事件代码、事件名称再提交！");
            return false;
        }

    }

</script>
</html>
