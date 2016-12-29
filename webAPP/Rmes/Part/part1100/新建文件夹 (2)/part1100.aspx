<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="part1100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Part.part1100.part1100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%--JIT计算监控--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script type="text/javascript">
  function submit(s,e) {
      //提交
      if (tabname.GetValue() == null) {
          alert("表名不能为空！")
          return;
      }
      if (receTime.GetText() == null) {
          alert("请填写到货时间！")
          return;
      }
      if (Pcode.GetValue() == null) {
          alert("请选择生产线！")
          return;
      }

      if (confirm("该操作将对" + Pcode.GetText() + "生产线手动提交要料请求，到货时间为：" + receTime.GetText() + "，是否继续？")) {
          butSubmit.SetEnabled(false);
          var webFileUrl = "?PCODE=" + Pcode.GetValue() + "&ONLTIME=" + receTime.GetText() + "&TABNAME=" + tabname.GetValue() + "  &opFlag=submit";
          var result = "";
          var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
          xmlHttp.open("Post", webFileUrl, false);
          xmlHttp.send("");
          result = xmlHttp.responseText;
          var array = result.split(',');
          retStr = array[1];
          result = array[0];

          switch (result) {
              case "OK":
                  alert(retStr);
                  butSubmit.SetEnabled(true);
                  grid2.PerformCallback();
                  return;
              case "Fail":
                  alert(retStr);             
                  butSubmit.SetEnabled(true);     
                  grid2.PerformCallback();
                  return;
          }
          var ref = "";
          var onltime = "";
          var planqty1 = "", planChangeqty1 = "";
          

          onltime = receTime.GetValue("");
          planso = txtPlanSo.GetValue();


      }
      else {
//          alert("提交操作已取消！");
          butSubmit.SetEnabled(true);
          return;
      }

             
        }
        </script>
    <dx:ASPxPageControl runat="server" ID="pageControl" Width="100%" EnableCallBacks="True"
        ActiveTabIndex="1">
        <TabPages>
            <dx:TabPage Text="JIT计算日志查询" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                           <%-- <tr>
                                <td style="height: 41px">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="查询条件">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="表名:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="ComboTabN" runat="server" TextField="SO" ValueField="SO" ValueType="System.String"
                                        Width="140px" SelectedIndex="0" >
                                        <Items>
                                            <dx:ListEditItem Text="三方物流要料" Value="三方物流要料" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="计算时间:">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="120px">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="--">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="120px">
                                        <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                        </CalendarProperties>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnQuery" runat="server" AutoPostBack="False" Text="查询">
                                        <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False"
                            OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="JITFLAG">
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="50px">
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="地点" FieldName="GZDD" VisibleIndex="1" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="计算类型" FieldName="JITFLAG" VisibleIndex="3" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="计算日志" FieldName="JITLOG" VisibleIndex="6" Width="280px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn Caption="计算时间" FieldName="JITTIME" VisibleIndex="3" Width="120px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="要料计算" Visible="true">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <table>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="height: 34px">
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="提交手动执行请求" ForeColor="LightBlue"  Font-Bold="true"  >
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="表名:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                              <dx:ASPxComboBox ID="ComboTabN2" runat="server" TextField="SO" ValueField="SO" ValueType="System.String"
                                                    Width="120px" SelectedIndex="0" ClientInstanceName="tabname" >
                                                    <Items>
                                                        <dx:ListEditItem Text="三方物流要料" Value="三方物流要料" />
                                                    </Items>
                                                </dx:ASPxComboBox>  
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="到货时间:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxDateEdit ID="DateOnlinetime" runat="server" EditFormat="Custom" EditFormatString="yyyy-MM-dd HH:mm:ss" 
                                                    Width="150px" ClientInstanceName="receTime" UseMaskBehavior="True"  >
                                                    <CalendarProperties ClearButtonText="清空" TodayButtonText="今天">
                                                    </CalendarProperties>
   
                                                </dx:ASPxDateEdit>
                                                
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="生产线选择:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="txtPCode" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                                                    ValueField="PLINE_CODE" ValueType="System.String" Width="100px" SelectedIndex="0" ClientInstanceName="Pcode">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td>
                                            </td>
                                             
                                            <td>
                                                <dx:ASPxButton ID="BtnSubmit" runat="server" AutoPostBack="False"  ClientInstanceName="butSubmit" 
                                                    Text="提交请求"> 
                                                   <ClientSideEvents Click="function(s, e) { submit(s,e); }" />
                                                </dx:ASPxButton>
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
                                            <td style="height: 34px">
                                                <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="当前计算控制状态"  ForeColor="LightBlue" Font-Bold="true">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="生产线选择:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxComboBox ID="txtPCode2" runat="server" DataSourceID="SqlCode" TextField="PLINE_NAME"
                                                    ValueField="PLINE_CODE" ValueType="System.String" Width="140px" SelectedIndex="0">
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="BtnUpdate" runat="server" AutoPostBack="False" OnClick="BtnUpdate_Click"
                                                    Text="刷新">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="当前状态:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="textSfzt" runat="server" ReadOnly="True" Width="140px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="计算用户:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="textSfyh" runat="server" ReadOnly="True" Width="100px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="计算时间:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="textSfsj" runat="server" ReadOnly="True" Width="140px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="最后运行时间:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="TextLtime" runat="server" ReadOnly="True" Width="140px">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="预计再上线:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                              <dx:ASPxTextBox ID="TextCount" runat="server" ReadOnly="True" Width="100px"  >
                                                </dx:ASPxTextBox>  
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="台开始上线">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            
                        </table>
                        <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="grid2" runat="server" KeyFieldName="JITUSER"
                            AutoGenerateColumns="False" OnCustomCallback="ASPxGridView2_CustomCallback">
                            <%-- OnCustomCallback="ASPxGridView2_CustomCallback"--%>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Caption=" " Width="50px">
                                    <ClearFilterButton Visible="True">
                                    </ClearFilterButton>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="提交用户" FieldName="JITUSER" VisibleIndex="1" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="状态" FieldName="MANUALFLAG" VisibleIndex="2" Width="80px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="提交时间" FieldName="COMMITTIME" VisibleIndex="3"
                                    Width="200px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="到货时间" FieldName="ONLINETIME" VisibleIndex="4"
                                    Width="200px" Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="计算时间" FieldName="JITTIME" VisibleIndex="5" Width="200px"
                                    Settings-AutoFilterCondition="Contains">
                                    <Settings AutoFilterCondition="Contains"></Settings>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <asp:SqlDataSource ID="SqlCode" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
        ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>"></asp:SqlDataSource>
</asp:Content>
