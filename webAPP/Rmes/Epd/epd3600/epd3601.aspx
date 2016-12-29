<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd3601.aspx.cs" Inherits="Rmes_epd3601" %>


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
        <table width="650px">
            <tr style="height: 20px">
                <td style="width: 20px">
                </td>
                <td  colspan="3" align="left">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择生产线、系列、工序，选择上传文件，确定提交" Font-Size="Medium" Width="600px">
                    </dx:ASPxLabel>
                </td>
                
            </tr>
            <tr style="height: 20px">
                <td style="width: 20px" colspan="2">
                </td>
                <td  align="left">
                    <asp:Label ID="lblMessage" runat="server" Width="100%" ForeColor="#FF0033" Text="请选择文件上传..." />
                </td>
                <td ></td>
            </tr>
            <tr style="height: 40px">
                <td style="width: 20px">
                </td>
                <td style="width: 100px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 450px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline"  runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList"
                        ValueField="RMES_ID" TextField="PLINE_NAME" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterProcess(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 30px">
                </td>
            </tr>
            <tr style="height: 40px">
                <td style="width: 20px">
                </td>
                <td style="width: 100px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="选择系列">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 450px">
                    <dx:ASPxComboBox ID="comboSeries" ClientInstanceName="listSeries"  runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList"
                        ValueField="PRODUCT_SERIES" TextField="PRODUCT_SERIES" OnCallback="comboSeries_Callback">
                        
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 30px">
                </td>
            </tr>

            <tr style="height: 40px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择工序">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="ASPxListBoxProcess" ClientInstanceName="listBoxProcess" runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList"
                        OnCallback="listBoxProcess_Callback" >
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 40px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="文件类型">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="ASPxComboFlag" ClientInstanceName="listBoxFlag" SelectedIndex="0" runat="server" Width="280px" Height="25px"  DropDownStyle="DropDownList">
                        <Items>
                            <dx:ListEditItem Text="作业指导书" Value="A" />
<%--                            <dx:ListEditItem Text="质量检查卡" Value="B" />
                            <dx:ListEditItem Text="工艺图纸" Value="C" />--%>
                        </Items>
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>

            <tr style="height: 60px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="选择文件">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <asp:FileUpload ID="fileup" name="fileup" runat="server" Width="280px" Height="25px" onchange="onUploadImgChange(this)" />
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 40px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="修改文件名">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <asp:TextBox ID="textFileName" runat="server" Width="280px" Height="25px"/>
                </td>
                <td>
                </td>
            </tr>

            <tr style="height: 50px">
                <td>
                </td>
                <td align="center">
                    
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="butConfirm" runat="server" OnClientClick="return checkSubmit();" onclick="butConfirm_Click" Text="上传文件" Width="100px" Height="30px" />
                    &nbsp; 
                    <asp:Button ID="ButtonCloseWindow" runat="server" onclick="butCloseWindow_Click" Text="关闭窗口" Width="100px" Height="30px" />

                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>

</body>

<script type="text/javascript">

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterProcess() {
        var pline = listPline.GetValue().toString();
        listBoxProcess.PerformCallback(pline);
        listSeries.PerformCallback(pline);
    }

    function checkSubmit() {
        if (listBoxProcess.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1 || listBoxFlag.GetSelectedIndex() == -1) {
            alert("请选择生产线及工序或文件类型再提交！");
            return false;
        }

        var fileUpload = document.getElementById("fileup")

        if (fileUpload.value == "") {
            alert("请选择上传工艺文件再提交！");
            return false;
        }
    }


    function onUploadImgChange(sender) 
    {
        var path = sender.value;
//        if (!sender.value.match(/.jpg|.JPG|.png|.PNG/i)) {
//            alert('图片格式无效！');
//            return false;
//        }

        filename = path.substr(path.lastIndexOf('\\') + 1);

        var start = path.lastIndexOf('\\') + 1;
        var len = path.lastIndexOf('.') - start;
        
        var showName = filename.substr(0, filename.lastIndexOf('.'));
        
        var showFile = path.substr(start, len);
        
        document.getElementById("textFileName").value = showFile;
    }   

</script>

</html>
