<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" MasterPageFile="~/ILMPMasterPage.master"%>
<asp:Content ID="forgotPasswordHead" ContentPlaceHolderID="masterHead" runat="server">
    <link href="/styles/forgotpassword.css" rel="stylesheet" type="text/css" />    
</asp:Content>

  <asp:Content ID="forgotPasswordContent" ContentPlaceHolderID="masterContent" runat="server">
      <div class="container">
        <br /><br /><br /><br /><br /><br /><br />
         <fieldset style="width:500px;margin:0 auto;border:2px solid #ccc;height:350px"> <legend></legend>   
            <h2>Forgot Password</h2>           
            <asp:Label ID="lblUserMessage" runat="server" Text=""></asp:Label>
            <asp:ValidationSummary ID="vsForgotPassword" ValidationGroup="vgForgotPassword" ForeColor="Red" HeaderText="Please fix the following errors" ShowSummary="true" runat="server" />
            <table>
            <tr><td class="auto-style3"><label id="lblUsername" runat="server"><b>Username</b></label></td><td></td></tr>
            <tr>                
                <td class="auto-style3"><asp:TextBox ID="txtUserName" placeholder="Username" runat="server" MaxLength="8" ToolTip="Username" Width="332px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfUserName" ControlToValidate="txtUserName" ValidationGroup="vgForgotPassword" ForeColor="Red"  ToolTip="Please enter username" ErrorMessage="Please enter your username" runat="server" >*</asp:RequiredFieldValidator></td>
            </tr>
           <tr>                                
                <td class="auto-style3">                  
                    <asp:Button ID="btnGetPassword" class="getPwd" runat="server" Text="Email Reset Link" OnClick="btnGetPassword_Click"  ValidationGroup="vgForgotPassword"/>                    
                </td>                            
           </tr>
            <tr>
                <td class="auto-style3">                  
                <asp:Button ID="btnCancel" class="back" runat="server" Text="Back" OnClick="btnCancel_Click" />                    
                </td>   
            </tr>
           </table>            
        </fieldset>           
      </div>
  </asp:Content>
