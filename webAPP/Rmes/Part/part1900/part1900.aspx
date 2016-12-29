<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part1900.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part1900.part1900" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--线边核减控制密码维护--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function initEditSeries(s, e) {
        if (Pcode.GetValue() == null) {
            return;
        }
        var webFileUrl = "?Pcode=" + Pcode.GetValue() + " &opFlag=getEditSeries";
        var result = "";
        var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
        xmlHttp.open("Post", webFileUrl, false);
        xmlHttp.send("");
        result = xmlHttp.responseText;

        var whyh = "";
        var whsj = "";
        var array1 = result.split(',');
        whyh = array1[0];
        whsj = array1[1];
        Whyh.SetValue(whyh);
        Whsj.SetValue(whsj);

    }
  
   
    </script>
    <table>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                    <td style="width:280px">
                        </td>
                        <td style="height: 40px">
                             <span style="font-size: 14pt";>
                            密码信息</span>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width:360px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择生产线:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                                ValueField="PLINE_CODE" ValueType="System.String" Width="140px" Height="30px"  ClientInstanceName="Pcode">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr><td style="height: 10px"></td></tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="最近维护时间:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtWHSJ" runat="server" ReadOnly="True" Width="140px" Height="30px" ClientInstanceName="Whsj"  >
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr><td style="height: 10px"></td></tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="最近维护用户:">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtWHYH" runat="server" ReadOnly="True" Width="140px" Height="30px" ClientInstanceName="Whyh">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr><td style="height: 10px"></td></tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="原密码:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtYMM" runat="server"  Width="140px" Height="30px">
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr><td style="height: 10px"></td></tr>
        <tr>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="新密码:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtXMM" runat="server" Width="140px" Height="30px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr><td style="height: 10px"></td></tr>
        <tr>
            <td>
            </td>
            <td>
                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="确认密码:">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxTextBox ID="txtQRMM" runat="server"  Width="140px" Height="30px">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr><td style="height: 10px"></td></tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
                <dx:ASPxButton ID="btnConfirm" runat="server" AutoPostBack="False" OnClick="btnConfirm_Click"
                    Text="确认" Height="30px" Width="100px" >
                </dx:ASPxButton>
            </td>
        </tr>
        </table> </td> </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource> 
</asp:Content>
