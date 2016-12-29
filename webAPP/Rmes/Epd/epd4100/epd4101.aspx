<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="epd4101.aspx.cs" Inherits="Rmes.WebApp.Rmes.Epd.epd4100.epd4101" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


<script type="text/javascript">
    var index;

    function ShowPopup(s,e) {


        var fieldNames = "RMES_ID";
        grid.GetSelectedFieldValues(fieldNames, GetPlanCodeCallback);
        index = e.visibleIndex;
        //grid.UnselectAllRowsOnPage();
        //grid.SelectRows(index,true);

        

    }

    function GetPlanCodeCallback(result) {
        if (result == "")
            return;
        else {
            grid.GetValuesOnCustomCallback(result, GetResultCallback);
            
        }
    }


    function GetResultCallback(result) {
        //        alert(result);
        //        self.location.href = self.location.href;
        if (result == "success")
            alert('拉料成功！');
        else {
            alert('拉料失败！');
            // beginDate.ChangeDate(new Date(result));
            //var returnValue = window.open('ssd4901.aspx?id=' + rids, 'newwin', 'resizable=yes,width=830,height=550,top=120,left=150');
        }
    }

</script>


<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="RMES_ID"
    OnCustomDataCallback="ASPxGridView1_CustomDataCallback">

    <Settings ShowHorizontalScrollBar="true" />
    <SettingsBehavior ColumnResizeMode="Control"/>

    <Columns>

    <dx:GridViewCommandColumn Caption="选择" ShowSelectCheckbox="true" Width="60px" FixedStyle="Left">
             <ClearFilterButton Visible="True">
            </ClearFilterButton>
            
        </dx:GridViewCommandColumn>

    <dx:GridViewCommandColumn>
        <CustomButtons> 
            <dx:GridViewCommandColumnCustomButton Text="拉  料" Visibility="AllDataRows"/>
        </CustomButtons> 
    </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn Caption="" FieldName="RMES_ID" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="" FieldName="COMPANY_CODE" Visible="false"/>
        <dx:GridViewDataTextColumn Caption="物料代码" FieldName="ITEM_CODE" />
        <dx:GridViewDataTextColumn Caption="物料名称" FieldName="ITEM_NAME" />
        <dx:GridViewDataComboBoxColumn Caption="线边库" FieldName="LINESIDE_STORE_CODE" />
        <dx:GridViewDataComboBoxColumn Caption="来源库" FieldName="RESOURCE_STORE" />
        <dx:GridViewDataTextColumn Caption="安全库存" FieldName="MIN_STOCK_QTY" Width="100px"/>
        <dx:GridViewDataComboBoxColumn Caption="拉料周期" FieldName="BATCH_TYPE" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="周期数量" FieldName="STAND_QTY" Width="100px"/>
        <dx:GridViewDataTextColumn Caption="单位" FieldName="UNIT_CODE" Width="100px"/>
        <dx:GridViewDataDateColumn Caption="最后拉料日期" FieldName="LAST_ISSUE_DATE"></dx:GridViewDataDateColumn>           
        <dx:GridViewDataTextColumn Caption="线边库存" FieldName="ITEM_QTY"></dx:GridViewDataTextColumn>

        
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
    <ClientSideEvents CustomButtonClick="function (s,e){ ShowPopup(s,e); }" />
</dx:ASPxGridView>

</asp:Content>