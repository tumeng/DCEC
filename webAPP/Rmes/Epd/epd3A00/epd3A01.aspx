<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd3A01.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd3A00.epd3A01" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridLookup" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxClasses" tagprefix="dx" %>

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
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择分装站点、触发站点、装配站点，确定提交" Font-Size="Medium" Width="400px">
                    </dx:ASPxLabel>
                </td>
                
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px">
                </td>
                <td style="width: 80px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 300px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline"  runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList" 
                        ValueField="RMES_ID" TextField="PLINE_NAME" >
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
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择分装站点">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboSUBStation" ClientInstanceName="comboSUBStationC" runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList" 
                        OnCallback="comboSUBStation_Callback" >
                    </dx:ASPxComboBox>
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
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择触发站点">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboCFStation" ClientInstanceName="comboCFStationC" runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList"
                        OnCallback="comboCFStation_Callback" >
                    </dx:ASPxComboBox>
                </td>
                <td >
                </td>
                <td>
                </td>
            </tr>   
            <tr style="height:30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="选择装配站点">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboZPStation" ClientInstanceName="comboZPStationC" runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList"
                        OnCallback="comboZPStation_Callback" >
                    </dx:ASPxComboBox>
                </td>
                <td >
                </td>
                <td>
                </td>
            </tr>   
            <tr style="height:30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="分装总成号">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxTextBox ID="txtPlineCode" ClientInstanceName="txtzch" runat="server" Width="280px" Text='<%# Bind("SUB_ZC") %>' >
                    </dx:ASPxTextBox>
                </td>
                <td  colspan="2"><asp:Label ID="lblMessage" runat="server" Width="100%" ForeColor="#FF0033" Text="" Font-Size="Small" />
                </td>
              
            </tr>   
            <tr style="height:30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="校验方式">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboCFlag" ClientInstanceName="comboCFlagC" runat="server" Width="280px" Height="25px"  DropDownStyle="DropDownList"
                        >
                    </dx:ASPxComboBox> <%--OnCallback="comboCFlag_Callback" --%>
                </td>
                <td >
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
                    <asp:Button ID="butConfirm" runat="server" OnClientClick="return checkSubmit();" onclick="butConfirm_Click" Text="确定" Width="100px" Height="30px" />
                    &nbsp; 
                    <asp:Button ID="ButtonCloseWindow" runat="server" onclick="butCloseWindow_Click" Text="关闭窗口" Width="100px" Height="30px" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script type="text/javascript">
    var pline;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterStation() {
        pline = listPline.GetValue().toString();

        comboSUBStationC.PerformCallback(pline);
        comboCFStationC.PerformCallback(pline);
        comboZPStationC.PerformCallback(pline);
        
    }

    function checkSubmit() {
        if (listPline.GetSelectedIndex() == -1 || comboSUBStationC.GetSelectedIndex() == -1 || comboCFStationC.GetSelectedIndex() == -1 || comboZPStationC.GetSelectedIndex() == -1 ) {
            alert("请选择生产线、分装站点、触发站点、装配站点再提交！");
            return false;
        }
        if (txtzch.GetValue() != null && comboCFlagC.GetSelectedIndex() == -1) {
            alert("请选择校验方式再提交！");
            return false;
        }

    }

</script>
</html>
