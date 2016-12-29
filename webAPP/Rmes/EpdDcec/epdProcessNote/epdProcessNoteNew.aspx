<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="epdProcessNoteNew.aspx.cs" Inherits="Rmes.WebApp.Rmes.EpdDcec.epdProcessNote.epdProcessNoteNew" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxRoundPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPanel" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxUploadControl" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallbackPanel" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxCallback" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>装机提示维护</title>
</head>
<body>
    <form id="form1" runat="server" >
    <script type="text/javascript">

        function openPic() {
            if (CmbPlineType.GetValue() == null) {
                alert("请先选择生产线！");
                return;
            }
            //window.open('ssd2422.aspx', 'bzBOM', 'resizable=yes,scrollbars=yes,width=800,height=500,top=150,left=250');
            var Pics = "";
            var cnt = ListFiles.GetItemCount();
            for (i = 0; i < cnt; i++) {
                var itema = ListFiles.GetItem(i);
                //ttt = itema.text + ';' + itema.value;  ../epdProcessNote/
                Pics += itema.value + "|";
            }
           
//            var result = "";
//            var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
//            xmlHttp.open("Post", Pics, false);
//            xmlHttp.send("");
            //CallbackSubmit.PerformCallback(Pics);
//            result = xmlHttp.responseText;
//            Pics = Pics + result;
            Pics ="N|"+ Pics + CmbPlineType.GetSelectedItem().value;
            
            
            window.open('/Rmes/EpdDcec/epdProcessNote/epdPic.aspx?Pic=' + Pics);
            //window.open('../epdPic.aspx?opFlag=add&rmesId=');
            //window.open('epdPic.aspx');
            //location.href = "epdPic.aspxs";
        }

        function FEmpNo_KeyDown(s, e) {
            if (e.htmlEvent.keyCode == 13) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                s.filterStrategy.Filtering();
            }
        }

        function getPlineCode() {
            var plineCode = CmbPlineType.GetSelectedItem().value;
            CmbRoutingRemark.PerformCallback(plineCode);
            CmbSO.PerformCallback(plineCode);
            ProcessCode.PerformCallback(plineCode);
        }
        function preview() {
            CallbackPanel.PerformCallback();
        }
        function upload() {
            ASPxUploadControl1.UploadFile();
            ListFiles.AddItem(ASPxUploadControl1.GetText());
        }
        function checkData() {
            if (CmbType.GetText() == "") { alert("请选择装机提示类型!"); return false; }

            var type = CmbType.GetSelectedItem().value;
            if (type == 'A') {
                if (CmbRoutingRemark.GetText() == "") { alert('请选择工艺路线!'); return false; }
                if (CmbSO.GetText() == "") { alert('请选择SO!'); return false; }
                if (ProcessCode.GetText() == "") { alert('请选择工序!'); return false; }
            }
            if (type == 'B') {
                if (CmbRoutingRemark.GetText() == "") { alert('请选择工艺路线!'); return false; }
                if (ProcessCode.GetText() == "") { alert('请选择工序!'); return false; }
            }
            if (type == 'C') {
                if (CmbSO.GetText() == "") { alert('请选择SO!'); return false; }
                if (ProcessCode.GetText() == "") { alert('请选择工序!'); return false; }
            }
            if (type == 'D') {
                if (CmbComponet.GetText() == "") { alert('请选择组件!'); return false; }
                if (ProcessCode.GetText() == "") { alert('请选择工序!'); return false; }
            }
            if (type == 'E') {
                if (CmbComponet.GetText() == "") { alert('请选择组件!'); return false; }
                if (CmbSO.GetText() == "") { alert('请选择SO!'); return false; }
                if (ProcessCode.GetText() == "") { alert('请选择工序!'); return false; }
                if (DateFrom.GetText() == "") { alert('请选择有效期开始日期!'); return false; }
            }

            if (TxtProcessNote.GetText() == "") {
                alert('请填写工序提示内容!');
                return false;
            }
            else
                return true;
        }
        function checkDataEdit() {
            if (TxtProcessNote.GetText() == "") {
                alert('请填写工序提示内容!');
                return false;
            }
            else
                return true;
        }
        function SetEnable() {
            var type = CmbType.GetSelectedItem().value;

            CmbRoutingRemark.SetEnabled(false);
            CmbSO.SetEnabled(false);
            ProcessCode.SetEnabled(false);
            CmbComponet.SetEnabled(false);
            DateFrom.SetEnabled(false);
            DateTo.SetEnabled(false);

            if (type == 'A') {
                CmbRoutingRemark.SetEnabled(true);
                CmbSO.SetEnabled(true);
                ProcessCode.SetEnabled(true);
            }
            if (type == 'B') {
                CmbRoutingRemark.SetEnabled(true);
                ProcessCode.SetEnabled(true);
            }
            if (type == 'C') {
                CmbSO.SetEnabled(true);
                ProcessCode.SetEnabled(true);
            }
            if (type == 'D') {
                CmbComponet.SetEnabled(true);
                ProcessCode.SetEnabled(true);
            }
            if (type == 'E') {
                CmbComponet.SetEnabled(true);
                CmbSO.SetEnabled(true);
                ProcessCode.SetEnabled(true);
                DateFrom.SetEnabled(true);
                DateTo.SetEnabled(true);
            }
        }
    </script>
    <div>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" HeaderText="新增" Width="100%">
            <PanelCollection>
<dx:PanelContent runat="server" >
            <table width="100%">
                <tr>
                     <dx:ASPxTextBox ID="txtpline" ClientInstanceName="ctxtpline" runat="server" Visible="false"></dx:ASPxTextBox>
                    <td colspan="6" ></td>
                    <td ></td>
                    <td ></td>
                </tr>
                <tr>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 10%">
                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="工艺提示类型：">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 17%">
                        <dx:ASPxComboBox ID="cmbType" runat="server" ClientInstanceName="CmbType" ValueType="System.String" Width="100%" DropDownStyle="DropDownList">
                            
                            <ClientSideEvents SelectedIndexChanged="function(s,e) { SetEnable(); e.processOnServer = false; }" />
                            <Items>
                            <dx:ListEditItem Text="组件+工序" Value="D" />
                                <dx:ListEditItem Text="SO+工序" Value="C" />
                                <dx:ListEditItem Text="组件+工序+SO" Value="E" />
                                <dx:ListEditItem Text="工艺路线+工序" Value="B" />
                                <dx:ListEditItem Text="工艺路线+工序+SO" Value="A" />
                                
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 6%">&nbsp;</td>
                    <td style="width: 10%">
                        &nbsp;</td>
                    <td style="width: 17%">
                        &nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                    <td style="width: 10%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="生产线：">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 17%">
                        <dx:ASPxComboBox ID="cmbPlineType" runat="server"  DropDownStyle="DropDownList"
                            ClientInstanceName="CmbPlineType" OnInit="cmbPlineType_Init" 
                             Width="100%">
                            <ClientSideEvents SelectedIndexChanged="function(s,e) { getPlineCode(); e.processOnServer = false; }" />
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 6%">
                    </td>
                    <td style="width: 10%">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="工艺路线：">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 17%">
                        <dx:ASPxComboBox ID="cmbRoutingRemark" runat="server"  DropDownStyle="DropDownList"
                            ClientInstanceName="CmbRoutingRemark" OnCallback="cmbRoutingRemark_Callback" 
                            ValueType="System.String" Width="100%" ClientEnabled="false">
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%">
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="SO:"></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbSO" ClientInstanceName="CmbSO" runat="server" ValueField="PT_PART_ALL" ValueType="System.String"
                            EnableCallbackMode="true" CallbackPageSize="10" Width="100%"   
                            onitemrequestedbyvalue="cmbSO_ItemRequestedByValue" TextFormatString="{0}" IncrementalFilteringMode="Contains"  IncrementalFilteringDelay="100000"
                            onitemsrequestedbyfiltercondition="cmbSO_ItemsRequestedByFilterCondition" 
                            DropDownStyle="DropDown">
                            <ClientSideEvents KeyDown="FEmpNo_KeyDown" />
                        </dx:ASPxComboBox> 
                    </td>
                    <td>&nbsp;</td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="组件："></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbComponet" ClientInstanceName="CmbComponet" runat="server" ValueField="PT_PART_ALL" ValueType="System.String"
                            EnableCallbackMode="true" CallbackPageSize="10" Width="100%" IncrementalFilteringMode="Contains"  IncrementalFilteringDelay="100000"
                            onitemrequestedbyvalue="cmbComponet_ItemRequestedByValue" TextFormatString="{0}"
                            onitemsrequestedbyfiltercondition="cmbComponet_ItemsRequestedByFilterCondition" 
                            DropDownStyle="DropDown">
                            <ClientSideEvents KeyDown="FEmpNo_KeyDown" />
                        </dx:ASPxComboBox> 
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="工序："></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbProcessCode" runat="server"  DropDownStyle="DropDownList"
                            ClientInstanceName="ProcessCode" OnCallback="cmbProcessCode_Callback" 
                            ValueType="System.String" Width="100%">
                        </dx:ASPxComboBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="有效日期开始于："></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="DateFrom" ClientInstanceName="DateFrom" runat="server" 
                            Width="100%" ClientEnabled="False" EditFormat="Custom" 
                            EditFormatString="yyyy-MM-dd"></dx:ASPxDateEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="有效日期结束于："></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="DateTo" ClientInstanceName="DateTo" runat="server" 
                            Width="100%" ClientEnabled="False" EditFormat="Custom" 
                            EditFormatString="yyyy-MM-dd"></dx:ASPxDateEdit>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>                   
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr >
                    <td></td>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="装机提示："></dx:ASPxLabel>
                    </td>
                    <td></td>
                    <td ></td>
                    <td ></td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="图片："></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td colspan="3">
                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" ClientInstanceName="CallbackPanel" runat="server" Width="100%" 
                            OnCallback="ASPxCallbackPanel1_Callback">
                            <PanelCollection>
                                <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxMemo ID="txtProcessNote" runat="server" ClientId="TxtProcessNote" ClientInstanceName="TxtProcessNote"
                                        Height="300px" Width="100%">
                                    </dx:ASPxMemo>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                        <br />
                    </td>
                    <td ></td>
                    <td colspan="3">
                        <dx:ASPxListBox ID="ListFiles" runat="server" Height="300px" Width="100%" 
                            ClientInstanceName="ListFiles" >   
                        </dx:ASPxListBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxColorEdit ID="cmbColor" runat="server"></dx:ASPxColorEdit>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbFont" runat="server" ValueType="System.String" Width="100%" DropDownStyle="DropDownList">
                            <Items>
                                <dx:ListEditItem Text="10" Value="10" />
                                <dx:ListEditItem Text="20" Value="20" />
                                <dx:ListEditItem Text="30" Value="30" />
                                <dx:ListEditItem Text="40" Value="40" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                        <td >
                            <dx:ASPxButton ID="btnPreview" width="100%" runat="server" Text="预览">
                                <ClientSideEvents Click=" function(s,e){ preview();e.processOnServer = false;}" />
                            </dx:ASPxButton>
                    </td>
                    <td></td>
                    <td colspan="2">
                        <dx:ASPxUploadControl ID="ASPxUploadControl1" ClientInstanceName="ASPxUploadControl1" FileUploadMode="OnPageLoad" runat="server" Width="100%" 
                            OnFileUploadComplete="ASPxUploadControl1_FileUploadComplete">
                            <BrowseButton Text="浏览..."></BrowseButton>
                        </dx:ASPxUploadControl>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnUpload" runat="server" Text="上传" Width="100%"  style="height: 25px">
                            <ClientSideEvents Click=" function(s,e){ upload();e.processOnServer = false;}"></ClientSideEvents>
                        </dx:ASPxButton>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td ></td>
                    <td ></td>
                    <td colspan="3"></td>
                    <td >&nbsp;</td>
                    <td ></td>
                    <td ></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="9">
                        <table width="100%">
                        <tr>
                    <td align="right">
                        <dx:ASPxButton ID="btnConfirm" runat="server" Text="确定" Width="20%" OnClick="Confirm_Click">
                            <ClientSideEvents Click=" function(s,e){if(!checkData())e.processOnServer = false;}"></ClientSideEvents>
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnConfirmEdit" runat="server" OnClick="ConfirmEdit_Click" Text="确定" Width="20%">
                            <ClientSideEvents Click=" function(s,e){if(!checkDataEdit())e.processOnServer = false;}"></ClientSideEvents>
                        </dx:ASPxButton>
                    </td>
                    <td style="width:10px"></td>
                    <td  align="left">
                        <dx:ASPxButton ID="btnClose" runat="server" Text="关闭" Width="20%">
                            <ClientSideEvents Click=" function(s,e){window.opener.location.href='../epdProcessNote/epdProcessNote.aspx';this.close();}"></ClientSideEvents>
                        </dx:ASPxButton>
                    </td>  
                        <td >
                            <dx:ASPxButton ID="ASPxButton1"  width="20%" runat="server" Text="预览"
                                AutoPostBack="false">  <%--OnClick="ASPxButton1_Click"--%>
                               <%-- <ClientSideEvents Click=" function(s,e){ previewP();}" />--%>
                                  <%--<ClientSideEvents Click="function(s,e){window.open('../epdProcessNote/epdPic.aspx');}" />--%>
                                    <ClientSideEvents Click="function test(s,e){openPic();}" />
                            </dx:ASPxButton>
                    </td>                                
                        </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td ></td>
                    <td colspan="6" >
                        &nbsp;</td>
                    <td ></td>
                    <td></td>
                </tr>
            </table>
                </dx:PanelContent>
</PanelCollection>
        </dx:ASPxRoundPanel>
    </div>


    <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit" OnCallback="ASPxCbSubmit_Callback">
   
</dx:ASPxCallback>

    </form>
</body>
</html>
