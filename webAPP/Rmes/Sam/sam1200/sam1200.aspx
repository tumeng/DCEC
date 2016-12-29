<%@ Page Language="C#" StylesheetTheme="Theme1" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Rmes_Sam_sam1200_sam1200" Codebehind="sam1200.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" AutoGenerateColumns="False" KeyFieldName="COMPANY_CODE;USER_ID" OnRowUpdating="ASPxGridView1_RowUpdating"
    OnRowInserting="ASPxGridView1_RowInserting" 
    OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated"
    OnRowDeleting="ASPxGridView1_RowDeleting" 
    OnRowValidating="ASPxGridView1_RowValidating"
    OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback">
    <SettingsEditing PopupEditFormWidth="600" PopupEditFormHeight="250" />
        
    <Columns>
        <dx:GridViewCommandColumn VisibleIndex="0" Width="150px" Caption="操作">
            <EditButton Visible="True">
            </EditButton>
            <NewButton Visible="True">
            </NewButton>
            <DeleteButton Visible="True">
            </DeleteButton>
            <CustomButtons>
                <dx:GridViewCommandColumnCustomButton ID="passOri" Text="密码重置">
                </dx:GridViewCommandColumnCustomButton>
            </CustomButtons>
            <ClearFilterButton Visible="True">
            </ClearFilterButton>
        </dx:GridViewCommandColumn>
        <dx:GridViewDataTextColumn FieldName="COMPANY_CODE" VisibleIndex="2" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="USER_ID" VisibleIndex="3" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="用户代码" FieldName="USER_CODE" VisibleIndex="4" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="姓名" FieldName="USER_NAME" VisibleIndex="5" Width="80px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="性别" FieldName="USER_SEX" VisibleIndex="6" Width="60px" CellStyle-HorizontalAlign="Center">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="部门" FieldName="DEPT_NAME" VisibleIndex="7" Width="100px" CellStyle-HorizontalAlign="Center">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="密码" FieldName="USER_PASSWORD" VisibleIndex="7" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="原密码" FieldName="USER_OLD_PASSWORD" VisibleIndex="8" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="授权IP" FieldName="USER_AUTHORIZED_IP" VisibleIndex="9" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="最大登录数" FieldName="USER_MAXNUM" VisibleIndex="10" Width="80px" Visible="false">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="有效" FieldName="VALID_FLAG" VisibleIndex="11" Width="60px" CellStyle-HorizontalAlign="Center">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="锁定" FieldName="LOCK_FLAG" VisibleIndex="12" Width="60px" CellStyle-HorizontalAlign="Center">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="Email" FieldName="USER_EMAIL" VisibleIndex="13" Width="150px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="电话" FieldName="USER_TEL" VisibleIndex="14" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="QQ" FieldName="USER_QQ" VisibleIndex="15" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption="微信" FieldName="USER_WECHAT" VisibleIndex="16" Width="100px">
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Caption=" " VisibleIndex="99" Width="80%"></dx:GridViewDataTextColumn>
    </Columns>
        
    <ClientSideEvents EndCallback="function(s, e) {
            callbackName = grid.cpCallbackName;
            theRet = grid.cpCompanyName;
            if(callbackName == 'Delete') 
            {
                alert(theRet);
            }
            if(callbackName == 'Ori') 
            {
                alert('重置密码成功！');
            }
        }" BeginCallback="function(s, e) {
	        grid.cpCallbackName = '';
        }" />
            
    <Templates>
        <EditForm>
            <center>
                <table width="95%">
                    <br />
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="width: 12%; text-align: left;">
                            <dx:ASPxLabel ID="Label1" runat="server" Text="用户代码" AssociatedControlID="txtUCode">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 39%">
                            <dx:ASPxTextBox ID="txtUCode" EnableClientSideAPI="True" runat="server" Width="100%"
                                Text='<%# Bind("USER_CODE") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="用户代码有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="用户代码字节长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="用户代码不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 8%; text-align: left;">
                            <dx:ASPxLabel ID="Label2" runat="server" Text="姓名" AssociatedControlID="txtUName">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 39%">
                            <dx:ASPxTextBox ID="txtUName" runat="server" Width="100%" Text='<%# Bind("USER_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="姓名有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="姓名字节长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                    <RequiredField IsRequired="True" ErrorText="姓名不能为空！" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label3" runat="server" Text="性别"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td style="width: 10%">
                                        <dx:ASPxRadioButton ID="RadMan" runat="server" Text="男" GroupName="sex" Checked="true">
                                        </dx:ASPxRadioButton>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButton ID="RadWoman" runat="server" Text="女" GroupName="sex">
                                        </dx:ASPxRadioButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label5" runat="server" Text="授权IP" AssociatedControlID="txtIp">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtIp" runat="server" Width="100%" Text='<%# Bind("USER_AUTHORIZED_IP") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="授权IP有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="授权IP格式不正确！" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label6" runat="server" Text="最大登录数" AssociatedControlID="TextBoxMax">
                            </dx:ASPxLabel>
                        </td>
                        <td><%--Text='<%# Bind("USER_MAXNUM") %>'--%>
                            <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" Width="100%" ID="TextBoxMax" Text="500" ClientEnabled="false"
                                ClientInstanceName="MaxNumC"  ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
<%--                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="最大登录数有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^[0-9]{1,100}$" />
                                    <RequiredField IsRequired="True" ErrorText="最大登录数不能为空！" />
                                </ValidationSettings>--%>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label10" runat="server" Text="电话" AssociatedControlID="txtTel">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtTel" runat="server" Width="100%" Text='<%# Bind("USER_TEL") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="电话有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="电话字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="Label9" runat="server" Text="Email" AssociatedControlID="txtEmail">
                            </dx:ASPxLabel>
                        </td>
                        <td >
                            <dx:ASPxTextBox ID="txtEmail" runat="server" Width="100%" Text='<%# Bind("USER_EMAIL") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="Email有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="Email字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                    <RegularExpression ErrorText="Email格式不正确！" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="部门" AssociatedControlID="txtIp">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <%--<dx:ASPxTextBox ID="txtDept" runat="server" Width="100%" Text='<%# Bind("USER_DEPT_NAME") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="授权IP有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="授权IP格式不正确！" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>--%>
                            <dx:ASPxComboBox ID="txtDept" runat="server" Width="100%" Value='<%# Bind("USER_DEPT_CODE") %>' DropDownStyle="DropDownList"
                            ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                            <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="输入有误，请重新输入！"
                                ErrorDisplayMode="ImageWithTooltip">
                                <RegularExpression ErrorText="长度不能超过15！" ValidationExpression="^.{0,15}$" />
                                <RequiredField IsRequired="True" ErrorText="不能为空！" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="QQ" AssociatedControlID="TextQQ">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox runat="server" EnableClientSideAPI="True" Width="100%" ID="TextQQ"
                                ClientInstanceName="MaxNumC" Text='<%# Bind("USER_QQ") %>' ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="QQ号有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="必须输入正整数！" ValidationExpression="^[0-9]{1,100}$" />
                                    <%--<RequiredField IsRequired="True" ErrorText="最大登录数不能为空！" />--%>
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="微信" AssociatedControlID="txtWeChat">
                            </dx:ASPxLabel>
                        </td>
                        <td>
                            <dx:ASPxTextBox ID="txtWeChat" runat="server" Width="100%" Text='<%# Bind("USER_WECHAT") %>'
                                ValidationSettings-ValidationGroup="<%# Container.ValidationGroup %>">
                                <ValidationSettings SetFocusOnError="True" ValidateOnLeave="True" ErrorText="微信号有误，请重新输入！"
                                    ErrorDisplayMode="ImageWithTooltip">
                                    <RegularExpression ErrorText="微信号字节长度不能超过30！" ValidationExpression="^.{0,30}$" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 2%">
                            &nbsp;
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label7" runat="server" Text="有效"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxCheckBox ID="chValidFlag" runat="server" Checked="true">
                            </dx:ASPxCheckBox>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="Label8" runat="server" Text="锁定"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <dx:ASPxCheckBox ID="chLockFlag" runat="server">
                            </dx:ASPxCheckBox>
                        </td>
                    </tr>
                    <tr style="height: 30px">
                        <td colspan="5" align="right">
                            <dx:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <dx:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                runat="server"></dx:ASPxGridViewTemplateReplacement>
                            <asp:HiddenField ID="HidUserId" runat="server" Value='<%# Bind("USER_ID") %>'></asp:HiddenField>
                        </td>
                    </tr>
                </table>
            </center>
        </EditForm>
    </Templates>
        
</dx:ASPxGridView>

</asp:Content>
