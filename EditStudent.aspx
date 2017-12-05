<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditStudent.aspx.cs" Inherits="EditStudent" MasterPageFile="~/HomeMasterPage.master" %>

<asp:Content ID="editStudentHead" ContentPlaceHolderID="homeMasterHead" Runat="Server">
      <style type="text/css">
        .auto-style1 {
              width: 293px;
          }
        .auto-style4 {
              width: 243px;
          }
          .auto-style5 {
              width: 116px;
          }
    </style>
</asp:Content>
<asp:Content ID="editStudentContent" ContentPlaceHolderID="homeMasterContent" Runat="Server">
      <fieldset style="width:900px;margin:0 auto;border:2px solid #ccc">
      <legend>Edit Student</legend>
      <table style="margin:0 auto">
            <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblStudentID" runat="server" Text="Student ID "></asp:Label></td>
                <td class="auto-style4"><asp:TextBox ID="txtStudentID" runat="server" Enabled="False" EnableTheming="True"></asp:TextBox></td>
                <td style="text-align:right" class="auto-style5"><asp:Label ID="lblEmail" runat="server" Text="Email "></asp:Label></td>
                <td class="auto-style1"><asp:TextBox ID="txtEmailID" runat="server" Width="180px"></asp:TextBox></td></tr>
            <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblFirstName" runat="server" Text="FirstName "></asp:Label></td>
                <td class="auto-style4"><asp:TextBox ID="txtFirstName" runat="server" Width="180px"></asp:TextBox></td>
                <td style="text-align:right" class="auto-style5"><asp:Label ID="lblLastName" runat="server" Text="Last Name "></asp:Label></td>
                <td class="auto-style1"><asp:TextBox ID="txtLastName" runat="server"  Width="180px"></asp:TextBox></td>    
            </tr>
            <tr style="height:50px">
            <td style="text-align:right"><asp:Label ID="lblStudentProgramme" runat="server" Text="Programme "></asp:Label></td>
            <td class="auto-style4"><asp:DropDownList ID="ddlStudentProgramme" runat="server" Height="16px" Width="125px">
              </asp:DropDownList></td>
            <td style="text-align:right" class="auto-style5"><asp:Label ID="lblStudentMajor" runat="server" Text="Major "></asp:Label></td>
            <td  class="auto-style1">
                <asp:DropDownList ID="ddlMajor" runat="server" Width="119px">
                    <asp:ListItem>NA</asp:ListItem>
                    <asp:ListItem>SD</asp:ListItem>
                    <asp:ListItem>NW</asp:ListItem>
                    <asp:ListItem>IS</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnAddMajor" runat="server" Text="Add" OnClick="btnAddMajor_Click" />
                <br />
                <asp:ListBox ID="lbMajor" runat="server" Height="42px" Width="122px"></asp:ListBox>
                <asp:Button ID="btnRemoveMajor" runat="server" Text="Remove" OnClick="btnRemoveMajor_Click" />
                </td></tr>
           <tr style="height:50px">
           <td style="text-align:right"><asp:Label ID="lblReason" runat="server" Text="Reason " ></asp:Label></td>
           <td class="auto-style4"><asp:DropDownList ID="ddlStatus" runat="server" Height="16px" Width="125px">
              <asp:ListItem>Studying</asp:ListItem>
              <asp:ListItem>Withdrawn</asp:ListItem>
              <asp:ListItem>Graduated</asp:ListItem>
              <asp:ListItem>Postponed</asp:ListItem>
          </asp:DropDownList></td>
          <td style="text-align:right" class="auto-style5">&nbsp;</td>
          <td  class="auto-style1">&nbsp;</td></tr>
          <tr style="height:50px">           
            <td colspan="2"  style="text-align:right;"><asp:Button ID="btnUpdateStudent" runat="server" Text="Update" Height="25px" Width="80px" OnClick="btnUpdateStudent_Click"  /></td>          
            <td colspan="2"  style="text-align:left"><asp:Button ID="btnCancelStudent" runat="server" Text="Back" Height="25px" Width="80px" OnClick="btnCancelStudent_Click"  /></td>
          </tr>
          </table>                 
  </fieldset>
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("DELETE");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>

