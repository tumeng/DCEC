<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="qms3100.aspx.cs" Inherits="Rmes.WebApp.Rmes.Qms.qms3100.qms3100" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript">

    function initListSeries(s,e) {

        listSeries.PerformCallback(s.lastSuccessValue);
    }

    function initListBarSEQ(s,e) {
        var str = "check," + s.lastSuccessValue;
        listBarSEQ.PerformCallback(s.lastSuccessValue);
        grid.PerformCallback(str);
    }

    function initListBarCode(s,e) {
        
        ListBoxBarCode.PerformCallback(s.lastSuccessValue);
        
    }

    function initGridView_add(s,e) {
        var items = ListBoxBarCode.GetSelectedItems();
        if (items.length == 0) return;
        var str="add,";
        for (var i = items.length - 1; i >= 0; i = i - 1) {
            str += items[i].value + ",";
        }
       //str = str.substring(0, s.Length - 1)

        grid.PerformCallback(str);
    }

    function initGridView_update(s,e) {
        var items = ListBoxBarCode.GetSelectedItems();
        if (items.length == 0) return;
        var str = "update,";
        for (var i = items.length - 1; i >= 0; i = i - 1) {
            str += items[i].value + ",";
        }
        //str = str.substring(0, s.Length - 1)

        grid.PerformCallback(str);
    }

    function save(s, e) {
        var buttonID = e.buttonID;
        if (buttonID == "cusbt1") {
            grid.GetValuesOnCustomCallback("update", GetDateCallback);
        }
        else if (buttonID == "cusbt2") {
            grid.GetValuesOnCustomCallback("work", GetDateCallback);
        }  
    }


    function GetDateCallback(result) {
        if (result == "success")
            alert("保存成功！");
        else
            alert(result);

        }
</script>

<table width="1180px">
    <tr>
    <td>
    <table>
        <tr>
        <td style="width:5px">
        </td>
        <td style="width:50px">
            <label style="font-size:small">生产线</label>
        </td>
        <td>
            <dx:ASPxComboBox ID="ASPxComboBoxPline" ClientInstanceName="listPline" runat="server" Width="143px" >
                <ClientSideEvents SelectedIndexChanged="function(s,e) { initListSeries(s,e); }" />
                <Items>
                    <dx:ListEditItem Text="开关柜生产线" Value="1" />
                    <dx:ListEditItem Text="断路器生产线" Value="2" />
                </Items>
            </dx:ASPxComboBox>
        </td>
        <td style="width:5px">
        </td>
        <td style="width:60px">
            <label style="font-size:small">产品系列</label>
        </td>
        <td>
            <dx:ASPxComboBox ID="ASPxComboBoxSeries" ClientInstanceName="listSeries" runat="server" Width="143px" OnCallback="ASPxComboBoxSeries_Callback">
                <ClientSideEvents SelectedIndexChanged="function(s,e) { initListBarSEQ(s,e); }" />
            </dx:ASPxComboBox>
        </td>
        <td style="width:5px">
        </td>
        <td style="width:60px">
            <label style="font-size:small">条码序号</label>
        </td>
        <td>
            <dx:ASPxComboBox ID="ASPxComboBoxBarSEQ" ClientInstanceName="listBarSEQ" runat="server" Width="143px" OnCallback="ASPxComboBoxBarSEQ_Callback">
                <ClientSideEvents SelectedIndexChanged="function(s,e) { initListBarCode(s,e); }" />
            </dx:ASPxComboBox>
        </td>
        <td style="width:5px">
        </td>
        </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td>
        <table>
        <tr>
        <td style="width:5px">
        </td>
        <td colspan="2" rowspan="2">
            <dx:ASPxListBox ID="ASPxListBoxBarCode" runat="server"  ClientInstanceName="ListBoxBarCode" SelectionMode="CheckColumn" Width="450px" Height="450px"
                ValueField="RMES_ID" ValueType="System.String" OnCallback="ASPxListBoxBarCode_Callback">
                <Columns>                                        
                    <dx:ListBoxColumn FieldName="SEQ_VALUE" Caption="条码号" Width="100px"/>
                    <dx:ListBoxColumn FieldName="SEQ_NAME" Caption="检验值" Width="150px"/>
                </Columns>
                
            </dx:ASPxListBox>
        </td>
        <td style="width:5px">
        </td>
        <td>
            <table>
            <tr>
                <td>
                <dx:ASPxButton ID="ASPxBT1" runat="server" Text="增加" Border-BorderStyle="None" AutoPostBack="False">
                    <ClientSideEvents Click="function(s,e) { initGridView_add(s,e); }" />
                </dx:ASPxButton>
                </td>
            </tr>
            <tr>
            <td>
                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="替换" Border-BorderStyle="None" AutoPostBack="False">
                    <ClientSideEvents Click="function(s,e) { initGridView_update(s,e); }" />
                </dx:ASPxButton>
                </td>
            </tr>
            </table>
        </td>
        
        <td colspan="4" rowspan="2" height="450px">
            <dx:ASPxGridView ID="ASPxGridView1" runat="server"  AutoGenerateColumns="False" ClientInstanceName="grid" Width="100%" KeyFieldName="RMES_ID"
                Settings-ShowHeaderFilterButton="false" OnCustomCallback="ASPxGridView1_CustomCallback"
                OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                
                <SettingsBehavior ColumnResizeMode="Control" />
                <Columns>
                <dx:GridViewCommandColumn Width="100px">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="cusbt1" Text="保存"></dx:GridViewCommandColumnCustomButton>
                        <dx:GridViewCommandColumnCustomButton ID="cusbt2" Text="生成条码"></dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn FieldName="RMES_ID" Visible="false"/>
                    <dx:GridViewDataTextColumn FieldName="SERIES_CODE" Visible="false"/>
                    <dx:GridViewDataTextColumn Caption="条码序号-1" FieldName="SEQ_1" VisibleIndex="1" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-2" FieldName="SEQ_2" VisibleIndex="2" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-3" FieldName="SEQ_3" VisibleIndex="3" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-4" FieldName="SEQ_4" VisibleIndex="4" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-5" FieldName="SEQ_5" VisibleIndex="5" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-6" FieldName="SEQ_6" VisibleIndex="6" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-7" FieldName="SEQ_7" VisibleIndex="7" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-8" FieldName="SEQ_8" VisibleIndex="8" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-9" FieldName="SEQ_9" VisibleIndex="9" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-10" FieldName="SEQ_10" VisibleIndex="10" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-11" FieldName="SEQ_11" VisibleIndex="11" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-12" FieldName="SEQ_12" VisibleIndex="12" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-13" FieldName="SEQ_13" VisibleIndex="13" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-14" FieldName="SEQ_14" VisibleIndex="14" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-15" FieldName="SEQ_15" VisibleIndex="15" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-16" FieldName="SEQ_16" VisibleIndex="16" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-17" FieldName="SEQ_17" VisibleIndex="17" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-18" FieldName="SEQ_18" VisibleIndex="18" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-19" FieldName="SEQ_19" VisibleIndex="19" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-20" FieldName="SEQ_20" VisibleIndex="20" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-21" FieldName="SEQ_21" VisibleIndex="21" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="条码序号-22" FieldName="SEQ_22" VisibleIndex="22" Width="100px">
                        <DataItemTemplate>
                            <%# GetCellText(Container)%>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
                </Columns>
                <ClientSideEvents CustomButtonClick="function (s,e){ save(s,e); }" />
            </dx:ASPxGridView>
        </td>
        <td style="width:5px">
        </td>
        </tr>
        </table>
        </td>
    </tr>
</table>



</asp:Content>
