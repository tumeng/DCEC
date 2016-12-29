<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="ssd3100.aspx.cs" Inherits="Rmes_ssd3100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1.Export, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function SnTh() {
            var ref = '';
            if (txtOldSn.GetValue() == null) {
                alert("请输入原流水号！");
                return;
            }
            if (txtNewSn.GetValue() == null) {
                alert("请输入原流水号！");
                return;
            }
            if (confirm("该操作将用" + txtNewSn.GetValue() + "替换" + txtOldSn.GetValue() + "，是否继续？")) {
            }
            else {
                alert("操作已取消！");
                return;
            }
            ref = 'TH|' + txtOldSn.GetValue() + '|' + txtNewSn.GetValue();
            ListBoxPanel6.PerformCallback(ref);
        }
        function SnTh1() {
            var ref = '';
            if (txtOldSn.GetValue() == null) {
                alert("请输入原流水号！");
                return;
            }
            if (txtNewSn.GetValue() == null) {
                alert("请输入原流水号！");
                return;
            }
            if (confirm("该操作将用" + txtNewSn.GetValue() + "对调" + txtOldSn.GetValue() + "，是否继续？")) {
            }
            else {
                alert("操作已取消！");
                return;
            }
            ref = 'THD|' + txtOldSn.GetValue() + '|' + txtNewSn.GetValue();
            ListBoxPanel6.PerformCallback(ref);
        }
        function SnThMulti() {
            var ref = "";
            var ids = "";
            var _ids = "";
//            var items = ListBoxLsh.GetSelectedItems();
//            if (items.length == 0) {
//                alert('请导入数据！');
//                return;
//            };


//            for (var i = 0; i < items.length; i++) {
//                ids = ids + items[i].value + ',';
//            }
//            if (ids.endWith(','))
//                _ids = ids.substring(0, ids.length - 1);

            var count = ListBoxLsh.GetItemCount();
            if (count == 0) {
                alert('请导入数据！');
                return;
            }
            if (confirm("是否确认替换")) {
            }
            else {
                alert("操作已取消！");
                return;
            }
            for (var i = 0; i < count; i++) {
                ids = ids + ListBoxLsh.GetItem(i).text + ',';
            }
            _ids = ids;
//            if (ids.endWith(','))
            //                _ids = ids.substring(0, ids.length - 1);
            
            ListBoxPanel6.PerformCallback('MULTI' + '|' + _ids);
        }

        function Import() {
            ListBoxLsh.ClearItems();
//            var File11 = document.getElementById("ctl00$ContentPlaceHolder1$ASPxCallbackPanel6$File1");
//            var ref = File11.value;
//            ListBoxPanel6.PerformCallback(ref);
        }
//        function pop(news) {
//            alert(news);
//        }
    </script>
    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel6" ClientInstanceName="ListBoxPanel6"
        OnCallback="ASPxCallbackPanel6_Callback">
        <ClientSideEvents   BeginCallback="function(s, e) 
                                {
	                                ListBoxPanel6.cpCallbackName = '';
                                }" EndCallback="function(s, e) 
                                {
                                    callbackName = ListBoxPanel6.cpCallbackName;
                                    theRet = ListBoxPanel6.cpCallbackRet;
                                    if(callbackName == 'Fail') 
                                    {
                                      alert(theRet); 
                                    }
                                }" />
        <PanelCollection>
            <dx:PanelContent ID="PanelContent6" runat="server">
                <table style="background-color: #99bbbb; width: 100%">
                    <tr>
                        <td colspan="10" style="width: auto;">
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="针对光板机现象，流水号替换功能，输入新旧流水号，点击替换按钮完成替换。"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 120px;">
                            <asp:Label ID="Label3" runat="server" Text="流水号替换（单台）" Width="120px"></asp:Label>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">
                            <asp:Label ID="Label4" runat="server" Text="原流水号："></asp:Label>
                        </td>
                        <td style="width: 200px;">
                            <dx:ASPxTextBox ID="txtOldSn" ClientInstanceName="txtOldSn" runat="server" Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px;">
                            <asp:Label ID="Label5" runat="server" Text="新流水号："></asp:Label>
                        </td>
                        <td style="width: 200px;">
                            <dx:ASPxTextBox ID="txtNewSn" ClientInstanceName="txtNewSn" runat="server" Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="替换" Width="100px">
                                <ClientSideEvents Click="function (s,e){
                                                                        SnTh();  
                                                                    }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="ASPxButton3" runat="server" AutoPostBack="false" Text="对调" Width="100px">
                                <ClientSideEvents Click="function (s,e){
                                                                        SnTh1();
                                                                    }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="width: 120px;">
                            <asp:Label ID="Label1" runat="server" Text="流水号替换（批量）" Width="120px" ></asp:Label>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td colspan="3" style="width: 200px;">
                            <input id="File1" type="file" accept=".txt" size="20" style="font-size: medium;
                                height: 25px;" alt="请选择文本文件" runat="server" />
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxButton ID="ASPxButton_Import" runat="server" AutoPostBack="true" Text="导入"
                                Width="100px" OnClick="ASPxButton_Import_Click">
                                <ClientSideEvents Click="function (s,e){
                                                                        Import();
                                                                        
                                                                    }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="刷新" UseSubmitBehavior="False">
                                <ClientSideEvents Click="function (s,e){
                                                                        ListBoxLsh.PerformCallback();
                                                                    }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 5px">
                        </td>
                         <td>
                            <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="false" Text="批量替换" Width="100px">
                                <ClientSideEvents Click="function (s,e){
                                                                        SnThMulti();
                                                                    }" />
                            </dx:ASPxButton>
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td colspan="3" style="width: 100px;">
                            <dx:ASPxListBox ID="ASPxListBox1" runat="server" ClientInstanceName="ListBoxLsh"
                                Width="210px" Height="200px" ValueField="SN" 
                                ValueType="System.String" OnCallback="ASPxListBoxUnused_Callback">
                                <Columns>
                                    <dx:ListBoxColumn FieldName="SN" Caption="流水号" Width="100%" />
                                </Columns>
                            </dx:ASPxListBox>
                        </td>
                        <td style="width: 5px">
                        </td>

                        <td style="text-align: right;">
                        </td>
                        <td style="width: auto;">
                        </td>
                    </tr>
                </table>
            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxCallbackPanel>
</asp:Content>
