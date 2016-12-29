<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Login_RmesIndex"
    Title="" Codebehind="RmesIndex.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-cn">
<head>
    <title>RMES制造执行系统</title><%--margin: 15px 15px 0px 15px;--%>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <style type="text/css">
        #full
        {
            margin: 0px 0px 0px 0px;
        }
        #topMenu
        {
            z-index: 999;
            margin-left: 0px;
            margin-right: 0px;
            width: auto;
        }
        #fullmain
        {
            margin: 0px;
            margin-top: 5px;
            padding: 0px;
            overflow: visible;
        }
    </style>
    <script type="text/javascript" src="../Pub/Script/jquery.min.js"></script>
</head>
<body style="background-position: 0px 0px; background-image: url(../Pub/images/rmes_login.png);
    background-attachment: fixed; margin: 0px; padding: 0px;height:100%">
    <form id="form1" runat="server">
    <div id="full">
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" Height="100%">
            <PanelCollection>
                <dx:PanelContent runat="server" Height="100%">
                    <div id="topMenu">
                        <dx:ASPxMenu ID="ASPxMenu1" runat="server" AutoSeparators="RootOnly">
                            <ItemStyle DropDownButtonSpacing="10px" />
                            <RootItemSubMenuOffset FirstItemY="2" LastItemY="2" Y="2" />
                            <ItemSubMenuOffset FirstItemX="2" FirstItemY="-2" LastItemX="2" LastItemY="-2" X="2"
                                Y="-2" />
                            <SubMenuStyle GutterWidth="0px" />
                        </dx:ASPxMenu>
                    </div>
                    <div id="fullmain">
                        <iframe id="ifmain" name="ifmain" src="blank.aspx" frameborder="0" width="100%" scrolling="auto"
                            height="500px" marginwidth="0" marginheight="0"></iframe>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    var autoHeight = function () {
        var d1 = $("#ifmain");
        var height = d1.contents().find("body").height() + 35;
        if (height < 600) height = 600;
        if (d1.height == height) return;
        d1.height(height);
    };
    $(document).ready(function () {
        $("#ifmain").bind('load', function () {
            autoHeight();
            $(this).contents().find("body").on('click DOMNodeInserted DOMNodeRemoved', function () {
                autoHeight();
            });
            var currentstr = $("#ASPxRoundPanel1_RPHT").text();
            var pms = currentstr.split(':');
            var gg1 = window.ifmain.location.href;
            var gg2 = $.getQueryStringRegExp('progName', gg1);
            if (gg2 == "") gg2 = "首页";
            var totalstring = pms[0] + ":【" + gg2 + "】";
            $("#ASPxRoundPanel1_RPHT").text(totalstring);
        });

        jQuery.extend({
            setTheme: function (theme) {
                if (!theme) return;
                //将主题代码写入本地cookie，30天过期
                var arr = new Array;
                arr[0] = "DXCurrentThemeASPxperience=";
                arr[1] = escape(theme);

                var Days = 30;
                var exp = new Date();
                exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);

                var theme1 = arr.join("");
                theme1 += "; path=/; expires=" + exp.toGMTString();
                document.cookie = theme1;
                location.reload();
            },
            message: function (a) {
                alert(a);
            },
            getQueryStringRegExp: function (name, url) {
                var reg = new RegExp("(^|\\?|&)" + name + "=([^&]*)(\\s|&|$)", "i");
                if (reg.test(url))
                    return unescape(RegExp.$2.replace(/\+/g, " ")); //现在系统使用了utf8编码，不知为啥。这里不能用escape了，by liuzhy 2013/12/24
                return "";
            },
            setDefaultPage: function (pageURL) {
                var progid = jQuery.getQueryStringRegExp('progCode', pageURL);
                if (progid != "")
                    $.get("RmesIndex.aspx?opt=setdefaultpage&progCode=" + progid, function (data) { if (data) alert(data) });
                else
                    alert("当前页面不支持设为首页！");
            }
        });
    });
        
</script>
