<%@ Page Language="C#" AutoEventWireup="true" Inherits="Rmes_Pub_CommonQuery_CommonQueryMore" Codebehind="CommonQueryMore.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
<body>
    <form id="form1" runat="server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:oracle %>"
            ProviderName="<%$ ConnectionStrings:oracle.ProviderName %>" >
        </asp:SqlDataSource>
        查询方式:<asp:DropDownList ID="DropDownList1" runat="server" Width="93px">

        </asp:DropDownList>
        &nbsp;
        <asp:TextBox ID="TextBoxQuery" runat="server"></asp:TextBox><asp:Button ID="ButtonQuery" runat="server" Text="查询" OnClick="ButtonQuery_Click" /><br />
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" CellPadding="4"  DataSourceID="SqlDataSource1"
            ForeColor="#333333" GridLines="None" >
            
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <Columns>
            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxOne" runat="server" />
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
        <asp:CheckBox ID="CheckBoxAll" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBoxAll_CheckedChanged"
            Text="全选" />
        <asp:Button ID="ButtonConfirm" runat="server" Text="确定" OnClick="ButtonConfirm_Click" />
        <asp:Button ID="ButtonCancel" runat="server" Text="取消" OnClick="ButtonCancel_Click" />
    </form>
</body>
</html>
