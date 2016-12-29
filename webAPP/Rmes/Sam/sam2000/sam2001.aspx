<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sam2001.aspx.cs" Inherits="Rmes_sam2001" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridLookup" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 70px;
            height: 31px;
        }
        .style3
        {
            width: 300px;
            height: 31px;
        }
        .style4
        {
            height: 31px;
        }
        .style5
        {
            width: 25px;
        }
        .style6
        {
            width: 25px;
            height: 31px;
        }
        .style7
        {
            width: 70px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float: left">
        <table width="450px">
            <tr style="height: 20px">
                <td style="width: 20px" colspan="4" align="center">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择用户菜单权限和生产线权限，确定提交" Font-Size="Medium"
                        Width="400px">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td class="style6">
                </td>
                <td style="text-align: left;" class="style2">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择用户">
                    </dx:ASPxLabel>
                </td>
                <td class="style3">
                    <dx:ASPxComboBox ID="comboUser" ClientInstanceName="listUser" runat="server" Width="300px"
                        Height="25px" ValueField="USER_ID" TextField="USER_NAME">
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterStation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" class="style5">
                    &nbsp;
                </td>
                <td class="style7">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="选择生产线">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <%--<dx:ASPxComboBox ID="ASPxComboPline" ClientInstanceName="listPline"  runat="server" Width="280px" Height="25px"
                        ValueField="PLINE_CODE" TextField="PLINE_NAME" OnCallback="comboPline_Callback"   >
                        
                    </dx:ASPxComboBox>--%>
                    <dx:ASPxGridLookup ID="GridLookupCode" runat="server" ClientInstanceName="listPline"
                        DataSourceID="SqlCode" KeyFieldName="PLINE_CODE" MultiTextSeparator="," SelectionMode="Multiple"
                        TextFormatString="{1}" Width="300px" Height="25px">
                        <%--ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>" --%>
                        <GridViewProperties >
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" ></SettingsBehavior>
                        </GridViewProperties>
                        <Columns>
                            <dx:GridViewCommandColumn Caption=" " ShowSelectCheckbox="True" Width="300px" />
                            <dx:GridViewDataColumn FieldName="PLINE_CODE" Visible="false"  />
                            <dx:GridViewDataColumn Caption="生产线代码" FieldName="PLINE_CODE" Width="100px" />
                            <dx:GridViewDataColumn Caption="生产线名称" FieldName="PLINE_NAME" Width="200px"  />
                        </Columns>
                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ErrorText="生产线信息有误，请重新输入！"
                            SetFocusOnError="True" ValidateOnLeave="True">
                            <RequiredField ErrorText="生产线不能为空！" IsRequired="True" />
                        </ValidationSettings>
                    </dx:ASPxGridLookup>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr style="height: 230px">
                <td class="style5">
                </td>
                <td style="text-align: left;" class="style7">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择菜单">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxListBox ID="ASPxListBoxProgram" runat="server" ClientInstanceName="listBoxProgram"
                        SelectionMode="CheckColumn" Width="300px" Height="330px" ValueField="PROGRAM_CODE"
                        ValueType="System.String" OnCallback="listBoxProgram_Callback" ViewStateMode="Inherit">
                        <Columns>
                            <dx:ListBoxColumn FieldName="PROGRAM_CODE" Caption="菜单代码" Width="30%" />
                            <dx:ListBoxColumn FieldName="PROGRAM_NAME" Caption="菜单名称" Width="70%" />
                        </Columns>
                    </dx:ASPxListBox>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 50px">
                <td class="style5">
                </td>
                <td class="style7">
                </td>
                <td style="text-align: left;">
                    <asp:Button ID="butConfirm" runat="server" OnClientClick="return checkSubmit();"
                        OnClick="butConfirm_Click" Text="确定" Width="100px" Height="30px" />
                    &nbsp;
                    <asp:Button ID="ButtonCloseWindow" runat="server" OnClick="butCloseWindow_Click"
                        Text="关闭窗口" Width="100px" Height="30px" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</body>
<script type="text/javascript">
    var user;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterStation() {
        user = listUser.GetValue().toString();


        listBoxProgram.PerformCallback(user);

    }

    function checkSubmit() {
        if (listBoxProgram.GetSelectedItems().length == 0 || listUser.GetSelectedIndex() == -1 ) {
            alert("请选择菜单、生产线再提交！");
            return false;
        }
    }

</script>
</html>
