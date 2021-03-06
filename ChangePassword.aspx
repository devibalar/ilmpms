﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" MasterPageFile="~/HomeMasterPage.master" %>
<asp:Content ID="changePasswordHead" ContentPlaceHolderID="homeMasterHead" runat="server">
    <link href="/styles/changepassword.css" rel="stylesheet" type="text/css" />      
</asp:Content>

  <asp:Content ID="changePasswordContent" ContentPlaceHolderID="homeMasterContent" runat="server">
      <div class="container">
        <br /><br /><br />
            <fieldset style="width:500px;margin:0 auto;border:2px solid #ccc;height:350px"> <legend></legend>          
            <h2>Change Password</h2>           
            <asp:Label ID="lblUserMessage" runat="server" Text=""></asp:Label>
             <asp:ValidationSummary ID="vsChangePassword" ValidationGroup="vgChangePassword" HeaderText="Please fix the following errors" ForeColor="Red" ShowSummary="true" runat="server" />
            <table style="margin:0 auto">                      
                
            <tr><td><label id="lblNewPassword" runat="server"><b>New Password</b></label></td><td></td></tr>
            <tr>                
                <td><asp:TextBox ID="txtNewPassword" placeholder="New Password" TextMode="Password" runat="server" MaxLength="25" ToolTip="New Password" Width="332px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfNewPassword" ControlToValidate="txtNewPassword" ValidationGroup="vgChangePassword" ForeColor="Red"  ToolTip="Please enter new password" ErrorMessage="Please enter new password" runat="server" >*</asp:RequiredFieldValidator></td>
                <td></td>               
            </tr>            
            <tr><td><label id="lblConfirmNewPassword"  runat="server"><b>Confirm Password</b></label></td><td></td></tr>
            <tr class="trconfirmPassword">             
                <td><asp:TextBox ID="txtConfirmPassword" placeholder="Confirm Password" TextMode="Password" runat="server" MaxLength="25" ToolTip="Confirm Password" Width="332px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfConfirmPassword" ControlToValidate="txtConfirmPassword" ValidationGroup="vgChangePassword" ForeColor="Red"  ToolTip="Please enter confirmation password" ErrorMessage="Please enter confirmation password" runat="server" >*</asp:RequiredFieldValidator></td>               
                <td><asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" ValidationGroup="vgChangePassword" ForeColor="Red" ErrorMessage="Confirmation password should match new password" Operator="Equal" Display="None"></asp:CompareValidator></td>                
            </tr>
           <tr>                                
                <td>                  
                    <asp:Button ID="btnChangePassword" class="changePwd" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" ValidationGroup="vgChangePassword" Width="332px" />                    
                </td>                
           </tr>
           </table>            
         </fieldset>
      </div>
  </asp:Content>
