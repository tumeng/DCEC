<%@ Page Language="C#" AutoEventWireup="true" Inherits="RmesLogin" CodeBehind="RmesLogin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Rmes系统登录</title>
    <script type="text/javascript" language="javascript" src="Rmes/Pub/Script/jquery.min.js"></script>
</head>
<body style="overflow: hidden; background-position: left;
    background-image: url(Rmes/Pub/images/rmes_login.png); background-color: Silver;
    margin: 0; text-align: left;">
    <form id="form1" runat="server">
    <table width="100%">
        <tr style="height: 220px">
            <td style="width: 30%">
            </td>
            <td style="width: 350px">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table width="350px" style="border-collapse: collapse; height: 220px;">
                    <tr style="height: 30px;">
                        <td style="width: 20px; background-position: right; background-image: url(Rmes/Pub/images/barLeft.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                            rowspan="8">
                        </td>
                        <td style="background-position: center; background-image: url(Rmes/Pub/images/barBody.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                            colspan="2">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Rmes/Pub/images/logo2.png" Visible="False" />
                        </td>
                        <td style="width: 20px; background-position: right; background-image: url(Rmes/Pub/images/barRight.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                            rowspan="8">
                        </td>
                    </tr>
                    <tr>
                        <td style="background-position: center; background-image: url(Rmes/Pub/images/barBody.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                            colspan="2">
                            <asp:Label ID="Label1" runat="server" Text="公司" Width="50px" Font-Size="13pt" ForeColor="White"
                                BackColor="Transparent" Font-Names="微软雅黑"></asp:Label>
                            <asp:DropDownList ID="DropDownListPline" runat="server" DataSourceID="SqlDataSource1"
                                DataTextField="COMPANY_NAME" DataValueField="COMPANY_CODE" BackColor="Transparent"
                                Width="212px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="text-align: left; background-position: center; background-image: url(Rmes/Pub/images/barBody.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                            colspan="2">
                            <asp:Label ID="Label2" runat="server" Text="用户" Width="50px" Font-Size="13pt" ForeColor="White"
                                BackColor="Transparent" Font-Names="微软雅黑"></asp:Label>
                            <asp:TextBox ID="TxtEmployeeCode" runat="server" Width="205px" BackColor="White"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="text-align: left; background-position: center; background-image: url(Rmes/Pub/images/barBody.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                            colspan="2">
                            <asp:Label ID="Label3" runat="server" Text="密码" Width="50px" Font-Size="13pt" ForeColor="White"
                                BackColor="Transparent" Font-Names="微软雅黑"></asp:Label>
                            <asp:TextBox ID="TxtPassword" runat="server" Width="205px" BackColor="White" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 10px; background-position: center; background-image: url(Rmes/Pub/images/bar.png);
                        border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;">
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr style="vertical-align: bottom">
                        <td align="center" style="width: 180px; text-align: left; background-position: center;
                            background-image: url(Rmes/Pub/images/barBody.png); border-top-style: none; border-right-style: none;
                            border-left-style: none; border-bottom-style: none;">
                            <asp:Label ID="Label4" runat="server" Text="© 2016 北京机械工业自动化所" Font-Size="10pt" ForeColor="Black"></asp:Label>
                        </td>
                        <td align="center" style="background-position: center 50%; background-image: url(Rmes/Pub/images/barBody.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; text-align: left;
                            border-bottom-style: none; vertical-align: top" rowspan="2">
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Rmes/Pub/images/riamb.png" />
                        </td>
                    </tr>
                    <tr style="vertical-align: top">
                        <td align="center" style="width: 180px; text-align: left; background-position: center;
                            background-image: url(Rmes/Pub/images/barBody.png); border-top-style: none; border-right-style: none;
                            border-left-style: none; border-bottom-style: none;">
                            <asp:Label ID="Label5" runat="server" Text="保留所有权利." Font-Size="10pt" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="text-align: left; background-position: center; background-image: url(Rmes/Pub/images/barBody.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;">
                            <%--<a href="http://192.168.112.144:8082"><span style="font-size:12px">工作站</span></a>--%>&nbsp;&nbsp;&nbsp;
                            <%--<a href="http://192.168.112.144:8081"><span style="font-size:12px">物料计算</span></a>--%>
                        </td>
                        <td align="center" style="background-position: center 50%; background-image: url(Rmes/Pub/images/barBody.png);
                            border-top-style: none; border-right-style: none; border-left-style: none; text-align: left;
                            border-bottom-style: none">
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="width: 100px; border-collapse: collapse; height: 220px">
                    <tr>
                        <td align="center" rowspan="2" style="background-position: right 50%; background-image: url(Rmes/Pub/images/barLeft.png);
                            vertical-align: top; width: 20px; border-top-style: none; border-right-style: none;
                            border-left-style: none; text-align: center; border-bottom-style: none">
                        </td>
                        <td style="background-position: right 50%; background-image: url(Rmes/Pub/images/barBody.png);
                            vertical-align: bottom; border-top-style: none; border-right-style: none; border-left-style: none;
                            text-align: center; border-bottom-style: none">
                            <asp:ImageButton ID="ButtonConfirm" OnClientClick="GetReturnFlag();return false;"
                                ToolTip="登录" runat="server" ImageUrl="~/Rmes/Pub/images/logIn.png" Width="50px"
                                ForeColor="Transparent" />
                        </td>
                        <td rowspan="2" style="background-position: right 50%; background-image: url(Rmes/Pub/images/barRight.png);
                            vertical-align: bottom; width: 20px; border-top-style: none; border-right-style: none;
                            border-left-style: none; text-align: center; border-bottom-style: none">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="background-position: right 50%; background-image: url(Rmes/Pub/images/barBody.png);
                            vertical-align: top; border-top-style: none; border-right-style: none; border-left-style: none;
                            text-align: center; border-bottom-style: none">
                            <asp:ImageButton ID="ButtonCancel" OnClientClick="ButtonCancel_onclick();return false;"
                                ToolTip="取消" runat="server" ImageUrl="~/Rmes/Pub/images/exit.png" Width="50px"
                                ForeColor="Transparent" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" SelectCommand="select * from code_company order by company_code ">
    </asp:SqlDataSource>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#TxtEmployeeCode').focus();
    });
    var ButtonCancel_onclick = function () {
        $('#TxtEmployeeCode').val('');
        $('#TxtPassword').val('');
        $('#TxtEmployeeCode').focus();
        return false;
    }

    //实现页面不刷新回掉功能
    //2013-12-24修改，现在各种现代浏览器均可正确登录了。by liuzhy
    var GetReturnFlag = function() {
        var usercode = $('#TxtEmployeeCode');
        var userpass = $('#TxtPassword');
        if (usercode.val() == '') {
            alert("用户代码不能为空！");
            usercode.focus();
            return false;
        }
        if (userpass.val() == "") {
            alert("密码不能为空！");
            userpass.focus();
            return false;
        }
        $.get(
                            "./RmesLogin.aspx",
                            { method: "login",
                                companycode: $('#DropDownListPline option:selected').val(),
                                usercode: usercode.val().toUpperCase(),
                                password: userpass.val(),
                                companyname: $('#DropDownListPline option:selected').text()
                            },
                            function (data) {
                                GetReturnFlagFromServer(data);
                            }
                        );
    }
    var GetReturnFlagFromServer = function(theReturnStr) {
        switch (theReturnStr) {
            case "0":
                window.location = 'http://' + window.location.host + '/Rmes/Login/RmesIndex.aspx?progCode=rmesIndex&progName=系统登录';
                break;
            case "10":
                window.location = 'http://' + window.location.host + '/Rmes/Sam/sam2400/sam2400.aspx?progCode=sam2400&progName=用户密码维护';
                break;
            case "1":
                alert("用户不存在！");
                break;
            case "2":
                alert("密码错误！");
                break;
            case "3":
                alert("用户无效！");
                break;
            case "4":
                alert("限制IP，不允许登录！");
                break;
            case "5":
                alert("当前用户超过最大登录数！");
                break;
            case "6":
                alert("系统登录用户超过最大限制！");
                break;
            case "7":
                alert("三次登录失败，该账户已经被锁定！");
                break;
            case "9":
                alert("当前用户和所登录生产线不匹配！");
                break;
            default:
                alert("系统错误！");
                break;
        }
        return false;
    }
</script>
