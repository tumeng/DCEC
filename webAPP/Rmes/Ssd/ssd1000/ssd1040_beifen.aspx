<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ssd1040_beifen.aspx.cs" Inherits="Rmes.WebApp.Rmes.Ssd.ssd1040" %>

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

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <script type="text/javascript">

      function initListBom() {
          var strSeries = listSeries.GetValue().toString();
          listBomIndex.PerformCallback(strSeries);
      }

      //        var butSubmit = document.getElementById("ButSubmit");  //提交按钮，通过提交按钮激活与否控制作业过程

      function checkSubmit() {



          var ref = "";

          butSubmit.SetEnabled(false);


          //            if (dateBegin.GetDate() == null) {
          //                alert("请选择开始日期！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }
          //            if (dateEnd.GetDate() == null) {
          //                alert("请选择结束日期！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }

          //            if (listPline.GetValue() == null) {
          //                alert("请选择生产线！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }

          //            if (listProcess.GetValue() == null) {
          //                alert("请选工艺路线！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }

          //            if (txtSeries.GetValue() == null) {
          //                alert("请输入产品系列！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }
          //             
          //            if (txtSO.GetValue() == null) {
          //                alert("请输入SO！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }

          //            if (txtPlanCode.GetValue() == null) {
          //                alert("请输入计划代码！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }

          //            if (txtCustomName.GetValue() == null) {
          //                alert("请输入客户名称！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }

          //            if (txtQty.GetValue() == null) {
          //                alert("请输入计划数量！");
          //                butSubmit.SetEnabled(true);
          //                return;
          //            }
          //            

          //            if (txtSeq.GetValue() == null) {
          //                alert("请输入执行序号！");
          //                 butSubmit.SetEnabled(true);
          //                return;
          //            }


          //            var beginDate = dateBegin.GetDate().toLocaleString();
          //            var pline = listPline.GetValue().toString();
          //            var series = listSeries.GetValue().toString();
          //            var bom = listBomIndex.GetValue().toString();
          //            var tcm = listTcmIndex.GetValue().toString();
          //            var project = listProject.GetValue().toString();
          //            var qty = txtQty.GetValue().toString();
          //            var shift = listShift.GetValue().toString();
          //            var seq = txtSeq.GetValue().toString();
          //            var flag = chkFlag.GetValue().toString();

          //            var remark = "";
          //            if (txtRemark.GetValue() != null) {
          //                remark = txtRemark.GetValue().toString();
          //            }

          //            var item = "";
          //            if (txtItem.GetValue() != null) {
          //                item = txtItem.GetValue().toString();
          //            }

          //            ref = beginDate + "," + pline + "," + series + "," + bom + "," + project + "," + qty + "," + shift + "," + seq + "," + remark + "," + item + "," + tcm + "," + flag;
          CallbackSubmit.PerformCallback(ref);
      }


      function submitRtr(e) {
          var result = "";
          var retStr = "";
          var array = e.split(',');
          retStr = array[1];
          result = array[0];

          switch (result) {
              case "OK":
                  alert(retStr);
                  butSubmit.SetEnabled(true);
                  initGridview();
                  return;
              case "Fail":
                  alert(retStr);
                  butSubmit.SetEnabled(true);
                  return;
          }
      }

      function initGridview() {
          grid.PerformCallback();
      }

      function initSeries(s, e) {
          //Series.PerformCallback(s.lastSuccessValue);
          var webFileUrl = "?SO=" + s.GetValue() + "&opFlag=getEditSeries";

          var result = "";
          var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
          xmlHttp.open("Post", webFileUrl, false);
          xmlHttp.send("");

          result = xmlHttp.responseText;
          var result1 = "";
          var retStr1 = "";
          var array1 = result.split(',');
          retStr1 = array1[1];
          result1 = array1[0];
          if (result1 == "") {
              alert("机型不存在，请检查数据！");
              ComoBoxSO.SetFocus();
              txtRemark.SetValue("");
              txtSeries.SetValue("");
              return;
          }
          txtRemark.SetValue(retStr1);
          txtSeries.SetValue(result1);
      }

      function initEditSeries(s, e) {
          var webFileUrl = "?SO=" + s.GetValue() + "&opFlag=getEditSeries";

          var result = "";
          var xmlHttp = new ActiveXObject("MSXML2.XMLHTTP");
          xmlHttp.open("Post", webFileUrl, false);
          xmlHttp.send("");

          result = xmlHttp.responseText;

          EditSeries.SetValue(result);
      }

      function changeSeq(s, e) {
          index = e.visibleIndex;
          var buttonID = e.buttonID;
          grid.GetValuesOnCustomCallback(buttonID, GetDataCallback);
          //        if (buttonID == "Up") {
          //            grid.GetValuesOnCustomCallback("up", GetDataCallback);
          //        }
          //        else if (buttonID == "Down") {
          //            grid.GetSelectedFieldValues("Down", GetDataCallback);
          //        }
      }

      function GetDataCallback(result) {
          alert(result);
      }
    </script>
    <table style="background-color: #99bbbb; width: 100%">
        <tr>
            <td style="width: 5px">
            </td>
            <td style="width: 100px;">
                <asp:Label ID="Label1" runat="server" Text="打开ExceL文件"></asp:Label>
            </td>
            <td style="width: 120px;">
                <%--<dx:ASPxUploadControl ValidationSettings-AllowedFileExtensions="'.xls','.doc','.jpg'" ID="File2" runat="server" UploadButton-Text="导入" BrowseButton-Text="浏览"></dx:ASPxUploadControl>--%>
                <input id="File1" type="file" accept="application/msexcel" size="20" style="font-size: medium;
                    height: 25px;" alt="请选择Excel文件" runat="server" />
            </td>
            <td style="width: 100px">
                <dx:ASPxButton ID="ASPxButton_Import" runat="server" AutoPostBack="true" Text="导入"
                    OnClick="ASPxButton_Import_Click" Width="100px">
                </dx:ASPxButton>
            </td>
            <td>
                &nbsp; &nbsp; &nbsp; &nbsp;
            </td>
            <td>
                <a href="../../File/销售订单模板.xls">销售订单模板</a>
            </td>

            <td style=" width:auto;"></td>
        </tr>
        </table>
        <table style="background-color: #99bbbb; width: 100%;">
        <tr>
            <td style="width: 5px; height:25px;">
            </td>
            <td style="text-align: left; width:70px;">
                <label style="font-size: small">
                    开始日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" EditFormatString="yyyy-MM-dd"
                    Width="120px">
                                        <ClientSideEvents DateChanged="function(s,e){
                        dateBeginPanel.PerformCallback();
                        dateEndPanel.PerformCallback();
                    }" />
                </dx:ASPxDateEdit>
            </td>
            <td style="width: 5px">
            </td>
            <td style="text-align: left; width: 70px">
                <label style="font-size: small">
                    完成日期</label>
            </td>
            <td style="width: 120px">
                <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" EditFormatString="yyyy-MM-dd"
                    Width="120px">
                </dx:ASPxDateEdit>
            </td>
                        <td style="width: 100px;">
                <dx:ASPxButton ID="ButSubmit" Text="查询计划" Width="80px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        grid.PerformCallback();
                        
                    }" />
                </dx:ASPxButton>

            </td>
                        <td style="width: 100px;">
                <dx:ASPxButton ID="ASPxButton1" Text="生成计划" Width="80px" AutoPostBack="false" runat="server">
                    <ClientSideEvents Click="function(s,e){
                        popup.Show();
                        txtSeries.SetEnabled(false);
                        ComoBoxSO.PerformCallback();
                    }" />
                </dx:ASPxButton>

            </td>
            <td colspan="3" style="width: auto">
            </td>
        </tr>
    </table>
    <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" KeyFieldName="RMES_ID"
        SettingsPager-Mode="ShowAllRecords" AutoGenerateColumns="False" OnInit="ASPxGridView1_Init"
        OnRowValidating="ASPxGridView1_RowValidating" OnRowUpdating="ASPxGridView1_RowUpdating"
        OnCustomCallback="ASPxGridView1_CustomCallback" OnRowDeleting="ASPxGridView1_RowDeleting"
        OnCustomDataCallback="ASPxGridView1_CustomDataCallback" OnCustomButtonInitialize="ASPxGridView1_CustomButtonInitialize"
        OnCommandButtonInitialize="ASPxGridView1_CommandButtonInitialize">
        <Settings ShowHorizontalScrollBar="true" />
        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ExportMode="All" />
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing PopupEditFormWidth="600px" />
        <Columns>
            <dx:GridViewCommandColumn ShowSelectCheckbox="true" SelectButton-Text="选择" Caption="选择"
                Width="60px">
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="调序" Width="80px" ButtonType="Image">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Up">
                        <Image Url="../../Pub/Images/Up.png" Width="15px" ToolTip="计划上调" />
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Down">
                        <Image Url="../../Pub/Images/Down.png" Width="15px" ToolTip="计划下调">
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="操作" Width="90px" ButtonType="Button">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="Confirm" Text="确认计划">
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="Cancel" Text="取消确认">
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewCommandColumn Caption="修改" Width="80px">
                <EditButton Visible="True" Text="修改">
                </EditButton>
                <DeleteButton Visible="True" Text="删除">
                </DeleteButton>
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false" />
            <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" Visible="false" />
            <dx:GridViewDataTextColumn Caption="计划编号" FieldName="PLAN_CODE" Width="140px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AutoFilterCondition="Contains" />
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="执行序" FieldName="PLAN_SEQ" Width="60px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="SO" FieldName="PLAN_SO" Width="60px" CellStyle-HorizontalAlign="Center"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="产品系列" FieldName="PRODUCT_SERIES" Width="100px"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="计划数" FieldName="PLAN_QTY" Width="60px" CellStyle-HorizontalAlign="Right"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle HorizontalAlign="Right">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="生产线" FieldName="PLINE_NAME" Width="100px" Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="开始日期" FieldName="BEGIN_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
           <dx:GridViewDataDateColumn Caption="结束日期" FieldName="END_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="记账日期" FieldName="ACCOUNT_DATE" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataDateColumn Caption="制定时间" FieldName="CREATE_TIME" Width="75px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataTextColumn Caption="备注" FieldName="REMARK" Width="200px" CellStyle-Wrap="False"
                Settings-AllowHeaderFilter="True">
                <Settings AllowHeaderFilter="True"></Settings>
                <CellStyle Wrap="False">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CONFIRM_FLAG" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RUN_FLAG" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption=" " Width="80%" />
        </Columns>
         <Settings ShowFooter="True" />
         <TotalSummary>
            <dx:ASPxSummaryItem FieldName="PLAN_QTY" SummaryType="Sum" DisplayFormat="总数={0}"/>
        </TotalSummary>
        <Templates>
            <EditForm>
                <table>
                    <tr style="height: 10px">
                        <td colspan="7">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="计划代码">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtPlanCode" runat="server" Width="160px" Text='<%# Bind("PLAN_CODE") %>'
                                Enabled="false" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="执行序号">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtPlanSeq" runat="server" Width="160px" Text='<%# Bind("PLAN_SEQ") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="执行序号有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^[0-9]{1,100}$" />
                                    <RequiredField IsRequired="True" ErrorText="执行序号不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="SO">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtPlanSO" ClientInstanceName="EditSO" runat="server" Width="160px"
                                Text='<%# Bind("PLAN_SO") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="产品系列有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="SO不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents TextChanged="function(s,e){
                    initEditSeries(s,e);
                }" />
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="机型">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="productSeries" ClientInstanceName="EditSeries" runat="server"
                                Width="160px" Text='<%# Bind("PRODUCT_SERIES") %>' Enabled="false" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="产品系列有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="产品系列不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 7px">
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="计划开始日期" />
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="ASPxDateBegin" runat="server" Width="100%" Value='<%# Bind("BEGIN_DATE") %>'
                                EditFormatString="yyyy-MM-dd" ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="日期有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RequiredField IsRequired="True" ErrorText="日期不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100px">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="计划数量" ForeColor="Red">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 180px">
                            <dx:ASPxTextBox ID="txtPlanQty" runat="server" Width="160px" Text='<%# Bind("PLAN_QTY") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="计划数量有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^[0-9]{1,100}$" />
                                    <RequiredField IsRequired="True" ErrorText="计划数量不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 50px">
                        <td>
                        </td>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="详细备注信息">
                            </dx:ASPxLabel>
                        </td>
                        <td colspan="4">
                            <dx:ASPxMemo ID="txtRemark" runat="server" Width="100%" Height="50px" Text='<%# Bind("REMARK") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="备注有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                </ValidationSettings>
                            </dx:ASPxMemo>
                            <td>
                            </td>
                    </tr>
                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td colspan="4">
                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="说明：" ForeColor="Red">
                            </dx:ASPxLabel>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            &nbsp; &nbsp; &nbsp; &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td colspan="7">
                        </td>
                    </tr>
                </table>
            </EditForm>
        </Templates>

        <SettingsBehavior ColumnResizeMode="Control" />
        
        <ClientSideEvents CustomButtonClick="function (s,e){
        changeSeq(s,e);
    }" />
    </dx:ASPxGridView>
    <dx:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="popup" runat="server"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton"
        ShowHeader="True" HeaderText="生成计划" AllowDragging="true" Width="80%">
        <ContentCollection>
            <dx:PopupControlContentControl Width="100%">
                <table align="center" style="width: 800px; height:200px;">
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划代码</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextPlanCode" ClientInstanceName="txtPlanCode" runat="server"
                                Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划序</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxSeq" ClientInstanceName="txtSeq" runat="server" Width="120px" />
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                SO</label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ComoBoxSO" runat="server" ClientInstanceName="ComoBoxSO" Width="120px" OnCallback="ComoBoxSO_Callback">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                    initSeries(s,e);
                }"  />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                机型</label>
                        </td>
                        <td>

                                        <dx:ASPxTextBox ID="ASPxTextSeries" ClientInstanceName="txtSeries" runat="server"
                                            Width="120px" />
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                计划数量</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxQty" ClientInstanceName="txtQty" runat="server" Width="120px" />

                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                客户名称</label>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="ASPxTextBoxCustomerName" ClientInstanceName="txtCustomerName" runat="server"
                                Width="120px" />

                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left;">
                            <label style="font-size: small">
                                开始日期</label>
                        </td>
                        <td>
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel2" ClientInstanceName="dateBeginPanel"
                                OnCallback="ASPxCallbackPanel2_Callback">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent1" runat="server">
                                       <dx:ASPxDateEdit ID="ASPxBeginDate" ClientInstanceName="dateBegin" runat="server"
                                EditFormatString="yyyy-MM-dd" Width="120px">
                            </dx:ASPxDateEdit>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                            
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                完成日期</label>
                        </td>
                        <td style="width: 120px">
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel3" ClientInstanceName="dateEndPanel"
                                OnCallback="ASPxCallbackPanel3_Callback">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent3" runat="server">
                                    <dx:ASPxDateEdit ID="ASPxEndDate" ClientInstanceName="dateEnd" runat="server" EditFormatString="yyyy-MM-dd"
                                Width="120px">
                            </dx:ASPxDateEdit>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                            
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                产线</label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="ASPxComboBoxPline" ClientInstanceName="listPline" runat="server"
                                Width="120px" AutoPostBack="false">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){
                                    ProcessPanel.PerformCallback();
                                    ComoBoxSO.PerformCallback();
                                }" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                工艺地点</label>
                        </td>
                        <td>
                            <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel4" ClientInstanceName="ProcessPanel"
                                OnCallback="ASPxCallbackPanel4_Callback">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent4" runat="server">
                                       <dx:ASPxComboBox ID="ASPxComboBoxProcess" ClientInstanceName="listProcess" runat="server"
                                Width="120px">
                            </dx:ASPxComboBox>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                            
                        </td>
                        <td style="width: 5px">
                        </td>
                        <td style="text-align: left; width: 70px">
                            <label style="font-size: small">
                                分装站点</label>
                        </td>
                        <td>
                            <dx:ASPxComboBox ID="FZZD" runat="server" Width="120px" ClientInstanceName="FZZD">
                            
                            </dx:ASPxComboBox>

                        </td>
                        <td style="width: 5px"></td>
                        <td style="text-align: left;">
                            <label style="font-size: small">
                                备注信息</label>
                        </td>
                        <td>
                            
                            <dx:ASPxTextBox ID="ASPxTextBoxRemark" ClientInstanceName="txtRemark" runat="server"
                                Width="120px" />
                        </td>
                        <td style="width: auto">
                        </td>
                    </tr>

                    <tr style="height: 30px">
                        <td>
                        </td>
                        <td colspan="5" align="center">
                            <dx:ASPxButton ID="UpdateButton" ClientInstanceName="butSubmit" ReplacementType="EditFormUpdateButton"
                                Text="提交" AutoPostBack="false" runat="server" Width="80px">
                                <ClientSideEvents Click="function(s,e) { checkSubmit(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                        <td colspan="5" align="center">
                            <dx:ASPxButton ID="CancelButton" ReplacementType="EditFormCancelButton" Text="取消" AutoPostBack="false"
                                runat="server" Width="80px">
                                <ClientSideEvents Click="function(s,e) { popup.Hide(); }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <dx:ASPxCallback ID="ASPxCbSubmit" runat="server" ClientInstanceName="CallbackSubmit"
                    OnCallback="ASPxCbSubmit_Callback">
                    <ClientSideEvents CallbackComplete="function(s, e) { submitRtr(e.result); }" />
                </dx:ASPxCallback>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


</asp:Content>
