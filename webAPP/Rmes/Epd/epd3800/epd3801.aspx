<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epd3801.aspx.cs" Inherits="Rmes_epd3801" %>

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
<script type="text/javascript">
    // <![CDATA[
    function AddSelectedItems() {
        MoveSelectedItems(lbAvailable, lbChoosen);
        UpdateButtonState();
    }
    function AddAllItems() {
        MoveAllItems(lbAvailable, lbChoosen);
        UpdateButtonState();
    }
    function RemoveSelectedItems() {
        MoveSelectedItems(lbChoosen, lbAvailable);
        UpdateButtonState();
    }
    function RemoveAllItems() {
        MoveAllItems(lbChoosen, lbAvailable);
        UpdateButtonState();
    }
    function MoveSelectedItems(srcListBox, dstListBox) {
        srcListBox.BeginUpdate();
        dstListBox.BeginUpdate();
        var items = srcListBox.GetSelectedItems();
        for (var i = items.length - 1; i >= 0; i = i - 1) {
            dstListBox.AddItem(items[i].text, items[i].value);
            srcListBox.RemoveItem(items[i].index);
        }
        srcListBox.EndUpdate();
        dstListBox.EndUpdate();
    }
    function MoveAllItems(srcListBox, dstListBox) {
        srcListBox.BeginUpdate();
        var count = srcListBox.GetItemCount();
        for (var i = 0; i < count; i++) {
            var item = srcListBox.GetItem(i);
            dstListBox.AddItem(item.text, item.value);
        }
        srcListBox.EndUpdate();
        srcListBox.ClearItems();
    }
    function UpdateButtonState() {
        btnMoveAllItemsToRight.SetEnabled(lbAvailable.GetItemCount() > 0);
        btnMoveAllItemsToLeft.SetEnabled(lbChoosen.GetItemCount() > 0);
        btnMoveSelectedItemsToRight.SetEnabled(lbAvailable.GetSelectedItems().length > 0);
        btnMoveSelectedItemsToLeft.SetEnabled(lbChoosen.GetSelectedItems().length > 0);
    }
    // ]]> 
    </script>
    <form id="form1" runat="server">
    <div style="float: left">
        <table width="750px">
            <tr style="height: 20px">
                <td style="width: 20px" colspan="4" align="center">
                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="选择站点和员工，确定提交" Font-Size="Medium" Width="400px">
                    </dx:ASPxLabel>
                </td>
                
            </tr>
            <tr style="height: 30px">
                <td style="width: 20px">
                </td>
                <td style="width: 100px; text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="选择生产线">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 300px">
                    <dx:ASPxComboBox ID="comboPlineCode" ClientInstanceName="listPline"  runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList" 
                        ValueField="RMES_ID" TextField="PLINE_NAME" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { filterLocation(); }" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 320px">
                </td>
            </tr>


            <tr style="height: 30px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="选择站点">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="comboStationCode" ClientInstanceName="comboBoxStation" runat="server" Width="280px" Height="25px" DropDownStyle="DropDownList" 
                        OnCallback="comboStationCode_Callback" >
                    </dx:ASPxComboBox>
                </td>
                <td>
                </td>
            </tr>
            
            <tr style="height:230px">
                <td>
                </td>
                <td style="text-align: left;">
                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="选择员工">
                    </dx:ASPxLabel>
                </td>
                <td colspan="2">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                    <td valign="top" style="width: 35%">
                        <div class="BottomPadding">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="待选员工:" />
                        </div>
                        <dx:ASPxListBox ID="ASPxListBox1" runat="server" ClientInstanceName="lbAvailable" ValueField="USER_ID" ValueType="System.String"
                        Width="100%" Height="240px" SelectionMode="CheckColumn"  OnCallback="listBoxUser_Callback" ViewStateMode="Inherit"  TextField="USER_NAME" >
                        <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />
                        </dx:ASPxListBox>
                    </td>
                    <td valign="middle" align="center" style="padding: 10px; width: 10%">
                        <div>
                            <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                                AutoPostBack="False" Text="增加 >" Width="130px" Height="23px" ClientEnabled="False"
                                ToolTip="Add selected items">
                                <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                            </dx:ASPxButton>
                        </div>
                        <div class="TopPadding">
                            <dx:ASPxButton ID="ASPxButton2" runat="server" ClientInstanceName="btnMoveAllItemsToRight"
                                AutoPostBack="False" Text="增加全部 >>" Width="130px" Height="23px" ToolTip="Add all items">
                                <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                            </dx:ASPxButton>
                        </div>
                        <div style="height: 32px">
                        </div>
                        <div>
                            <dx:ASPxButton ID="ASPxButton3" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                                AutoPostBack="False" Text="< 删除" Width="130px" Height="23px" ClientEnabled="False"
                                ToolTip="Remove selected items">
                                <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                            </dx:ASPxButton>
                        </div>
                        <div class="TopPadding">
                            <dx:ASPxButton ID="ASPxButton4" runat="server" ClientInstanceName="btnMoveAllItemsToLeft"
                                AutoPostBack="False" Text="<< 删除全部" Width="130px" Height="23px" ClientEnabled="False"
                                ToolTip="Remove all items">
                                <ClientSideEvents Click="function(s, e) { RemoveAllItems(); }" />
                            </dx:ASPxButton>
                        </div>
                    </td>
                    <td valign="top" style="width: 35%">
                        <div class="BottomPadding">
                            <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="已选员工:" />
                        </div>
                        <dx:ASPxListBox ID="listChosedUser" runat="server" ClientInstanceName="lbChoosen" Width="100%"
                            Height="240px" SelectionMode="CheckColumn">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }">
                            </ClientSideEvents>
                        </dx:ASPxListBox>
                            
                    </td>
                    
                </tr>
            </table>
                    <%--<dx:ASPxListBox ID="listBoxUser" runat="server"  
                        ClientInstanceName="listBoxuserClient" SelectionMode="CheckColumn" 
                        Width="280px" Height="230px"
                        ValueField="USER_ID" ValueType="System.String"   ViewStateMode="Inherit" 
                        oncallback="listBoxUser_Callback" >
                        <Columns>
                            <dx:ListBoxColumn FieldName="USER_ID" Caption="员工代码" Width="40%" />
                            <dx:ListBoxColumn FieldName="USER_NAME" Caption="员工姓名" Width="50%" />
                        </Columns>
                    </dx:ASPxListBox>
                </td>
                <td>--%>
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
            </tr>
        </table>
    </div>
    </form>

</body>

<script type="text/javascript">
    var pline ;

    if (!String.prototype.trim) {
        String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    }

    function filterLocation() {
        pline = listPline.GetValue().toString();

        comboBoxStation.PerformCallback(pline);
        lbAvailable.PerformCallback(pline);
    }

    function checkSubmit() {
//        if (lbChoosen.GetSelectedItems().length == 0 || comboBoxStation.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1) {
        if (lbChoosen.GetItemCount() == 0 || comboBoxStation.GetSelectedIndex() == -1 || listPline.GetSelectedIndex() == -1) {
            alert("请选择站点、员工再提交！");
            return false;
        }
    }

</script>

</html>
