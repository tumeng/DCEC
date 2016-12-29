<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Pub_CommonQuery_CommonQuerySingle" Theme="Theme1" Codebehind="CommonQuerySingle.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="content.css" type="text/css" rel="stylesheet" /> 
    <title>通用查询选择</title>
<script language="javascript" type="text/javascript">
// <!CDATA[

function Select1_onclick() {

}

function ButtonQuery_onclick() {

}

// ]]>
</script>
</head>
<body onload="initC();">
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>" ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" >
        </asp:SqlDataSource>
        <asp:Label ID="Label1" runat="server" Text="查询方式:"></asp:Label>
        <asp:DropDownList ID="DropDownList1" runat="server" Width="93px"></asp:DropDownList>
        <asp:TextBox ID="TextBoxQuery" runat="server"></asp:TextBox>
        <asp:Button ID="ButtonQuery" runat="server" Text="查询" OnClick="ButtonQuery_Click" /><br />
    </div>
    <div style="overflow-x: scroll; width:1500px" id="dvBody">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowDataBound="GridView1_RowDataBound">
            
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:RadioButton  ID="RadioButtonOne" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>           
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        </div>
        <asp:Button ID="ButtonConfirm" runat="server" Text="确定" OnClick="ButtonConfirm_Click" />
        <asp:Button ID="ButtonCancel" runat="server" Text="取消" OnClick="ButtonCancel_Click" />
    </form>
</body>
</html>
     <script type="text/javascript">
        var last = null;//最后访问的RadioButton的ID
        function judge(obj)
        {
        if(last == null)
            {
                last = obj.id;
                //alert(last);
            }
            else
            {
                var lo = document.getElementById(last);
                lo.checked = "";
                //alert(last + "  " + lo.checked);
                last = obj.name;
            }
            obj.checked = "checked";
        }
        
        function initC(){
//        window.scrollbars=1;
        }
    </script>
