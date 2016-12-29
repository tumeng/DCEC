<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Sam_sam2400_sam2400" Title="" Codebehind="sam2400.aspx.cs" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="<%=Page.ResolveUrl("~/Rmes/Pub/Script/RmesGlobal.js")%>"></script>

    <script type="text/javascript" language="javascript">
        var newpasstemp = "";
        function OnOldPassValidation(s, e)
        {
            var oldpass = e.value;
            if (oldpass == null)
                return;
        }
        function OnNewPassValidation(s, e)
        {
            var newpass = e.value;
            if (newpass == null)
                return;
            //            if(isDate(newpass)==false)
            //            {
            //               e.isValid=false;
            ////               e.ErrorText="密码格式有误！";
            //               }
            newpasstemp = newpass;
        }
        function OnNewPassSureValidation(s, e)
        {
            var newpasssure = e.value;
            if (newpasssure == null)
                return;
            if (newpasssure != newpasstemp)
                e.isValid = false;
        }
    </script>

    <br />
    <br />
    <br />
    <table width="600px" align="center">
        <tr>
            <td style="background-image: url(<%=Page.ResolveUrl("~/Rmes/Pub/images/Re_Out.png")%>);
                background-position: center top; background-repeat: no-repeat; width: 100%; height: 270px"
                align="center">
                <span style="font-size: 11pt">
                    <br />
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                </span>
                <br />
                <table width="80%">
                    <tr>
                        <td style="text-align: left; width: 13%;">
                            <asp:Label ID="Label1" runat="server" Text="用户代码" Width="70px" Font-Size="13pt" ForeColor="White"
                                BackColor="Transparent" Font-Names="微软雅黑"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtUserCode" runat="server" ReadOnly="true" Width="140px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 13%;">
                            <asp:Label ID="Label2" runat="server" Text="姓名" Width="70px" Font-Size="13pt" ForeColor="White"
                                BackColor="Transparent" Font-Names="微软雅黑"></asp:Label>
                        </td>
                        <td style="width: 50%; text-align: left">
                            <asp:TextBox ID="txtUserName" runat="server" ReadOnly="true" Width="140px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%; height: 28px; text-align: left">
                            <dx:ASPxLabel runat="server" ID="LabelOldPass" AssociatedControlID="TextBoxOldPass"
                                Text="原密码" Width="70px" Font-Size="13pt" ForeColor="White" BackColor="Transparent"
                                Font-Names="微软雅黑" />
                        </td>
                        <td style="width: 50%; height: 28px; text-align: left">
                            <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" Width="160px" ID="TextBoxOldPass"
                                ClientInstanceName="OldPass" OnValidation="TextBoxOldPass_Validation" Password="True">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="原密码有误，请重新输入！">
                                    <RequiredField IsRequired="True" ErrorText="原密码不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents Validation="OnOldPassValidation" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%; height: 28px; text-align: left">
                            <dx:ASPxLabel runat="server" ID="LabelNewPass" AssociatedControlID="TextBoxNewPass"
                                Text="新密码" Width="70px" Font-Size="13pt" ForeColor="White" BackColor="Transparent"
                                Font-Names="微软雅黑" />
                        </td>
                        <td style="width: 50%; height: 28px; text-align: left">
                            <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" Width="160px" ID="TextBoxNewPass"
                                ClientInstanceName="NewPass" Password="True" OnValidation="TextBoxNewPass_Validation">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="密码格式不对！">
                                    <RegularExpression ErrorText="密码长度5－20位！" ValidationExpression="^.{5,20}$" />
                                    <RequiredField IsRequired="True" ErrorText="新密码不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents Validation="OnNewPassValidation" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%; height: 28px; text-align: left">
                            <dx:ASPxLabel runat="server" ID="ASPxLabel1" AssociatedControlID="TextBoxNewPassSure"
                                Text="确认密码" Width="70px" Font-Size="13pt" ForeColor="White" BackColor="Transparent"
                                Font-Names="微软雅黑" />
                        </td>
                        <td style="width: 50%; height: 28px; text-align: left">
                            <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" Width="160px" ID="TextBoxNewPassSure"
                                ClientInstanceName="NewPassSure" OnValidation="TextBoxNewPassSure_Validation"
                                Password="True">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="确认密码应一致！">
                                    <RequiredField IsRequired="True" ErrorText="确认密码不能为空！" />
                                </ValidationSettings>
                                <ClientSideEvents Validation="OnNewPassSureValidation" />
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%">
                        </td>
                        <td style="width: 50%; text-align: left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 83px; background-color: Transparent;">
                                        <dx:ASPxButton ID="s" runat="server" ForeColor="Transparent" OnClick="btnSure_Click"
                                            Width="50px">
                                            <Image Url="~/Rmes/Pub/images/logIn.png" Width="50px" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnCancel" runat="server" AutoPostBack="False" CausesValidation="False">
                                            <Image Url="~/Rmes/Pub/images/exit.png" Width="50px" />
                                            <ClientSideEvents Click="function(s, e) { ASPxClientEdit.ClearEditorsInContainerById('clientContainer') }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

