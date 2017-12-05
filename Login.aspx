<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" MasterPageFile="~/ILMPMasterPage.master" %>

<asp:Content ID="loginHead" ContentPlaceHolderID="masterHead" runat="server">
    <link href="/styles/login.css" rel="stylesheet" type="text/css" />    
</asp:Content>

  <asp:Content ID="loginContent" ContentPlaceHolderID="masterContent" runat="server">
      <div class="container">
        <br /><br /><br />
          <fieldset style="width:500px;margin:0 auto;border:2px solid #ccc;height:350px"> <legend></legend>
            <div><h2>LOGIN</h2></div>
            <asp:Label ID="lblUserMessage" CssClass="userMessage" runat="server" Text=""></asp:Label>
            <asp:ValidationSummary ID="vsLogin" ValidationGroup="vgLogin" HeaderText="Please fix the following errors" ShowSummary="true" runat="server" ForeColor="Red" />
            <table>
            <tr><td class="auto-style3"><label id="lblUsername" runat="server"><b>&nbsp;&nbsp;&nbsp;&nbsp; Username</b></label></td><td></td></tr>
            <tr>                
                <td class="auto-style3" style="margin-left:70px"><asp:TextBox ID="txtUserName" placeholder="Username" MaxLength="8" runat="server" ToolTip="Username" Width="332px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfUserName" ControlToValidate="txtUserName" ValidationGroup="vgLogin" ForeColor="Red"  ToolTip="Please enter username" ErrorMessage="Please enter your username" runat="server" Display="None">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr><td class="auto-style3"><label id="lblPassword" runat="server"><b>&nbsp;&nbsp;&nbsp;&nbsp; Password</b></label></td><td></td></tr>
            <tr>               
                <td class="auto-style3"><asp:TextBox ID="txtPassword" placeholder="Password" MaxLength="25" runat="server" TextMode="Password" ToolTip="Password" Width="331px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfPassword"  ControlToValidate="txtPassword" ValidationGroup="vgLogin" ForeColor="Red"  ToolTip="Please enter password"  ErrorMessage="Please enter your password" runat="server" Display="None">*</asp:RequiredFieldValidator></td>
            </tr>
           
            <tr>                                
                <td class="auto-style3">                 
                    <asp:Button ID="btnLogin" class="submitLogin" runat="server" Text="Login"  OnClick="btnLogin_Click" ValidationGroup="vgLogin" />                    
                </td>                
           </tr>
           <tr>                                
                <td class="auto-style3">                  
                    <asp:Button ID="btnForgotPassword" class="forgotPwd" runat="server" Text="Forgot Password?" PostBackUrl="~/ForgotPassword.aspx" />                    
                </td>                
           </tr>
           </table>            
         </fieldset>
      </div>
  </asp:Content>
