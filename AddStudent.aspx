<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="AddStudent" MasterPageFile="~/HomeMasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 263px;
        }
        .auto-style3 {
            width: 108px;
        }
        .auto-style4 {
            width: 197px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
    <h3>Add Student</h3>
    <fieldset style="width:700px;margin:0 auto;border:2px solid #ccc"> <legend></legend>
        <table style="margin:0 auto">
            <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblStudentID" runat="server" Text="Student ID "></asp:Label></td>
                <td class="auto-style4"><asp:TextBox ID="txtStudentID" runat="server"></asp:TextBox></td>
                <td style="text-align:right" class="auto-style3"><asp:Label ID="lblEmail" runat="server" Text="Email "></asp:Label></td>
                <td class="auto-style1"><asp:TextBox ID="txtEmail" runat="server" Width="180px"></asp:TextBox></td>                
            </tr>
            <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblFirstName" runat="server" Text="First Name "></asp:Label></td>
                <td class="auto-style4"><asp:TextBox ID="txtFirstName" runat="server" Width="180px"></asp:TextBox></td>
                <td style="text-align:right" class="auto-style3"><asp:Label ID="lblLastName" runat="server" Text="Last Name "></asp:Label></td>
                <td class="auto-style1"><asp:TextBox ID="txtLastName" runat="server" Width="180px"></asp:TextBox></td>             
            </tr>
            <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblProgramme" runat="server" Text="Programme "></asp:Label></td>
                <td class="auto-style4"><asp:DropDownList ID="ddlProgramme" runat="server" Height="16px" Width="131px">
                        <asp:ListItem>BIT</asp:ListItem>
                        <asp:ListItem>GDIT</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align:right" class="auto-style3"><asp:Label ID="lblMajor" runat="server" Text="Major "></asp:Label></td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlMajor" runat="server" Width="119px">
                        <asp:ListItem>NA</asp:ListItem>
                        <asp:ListItem>SD</asp:ListItem>
                        <asp:ListItem>NW</asp:ListItem>
                        <asp:ListItem>IS</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnAddMajor" runat="server" OnClick="btnAddMajor_Click" Text="Add" />
                    <br />
                    <asp:ListBox ID="lbMajor" runat="server" Height="42px" Width="122px"></asp:ListBox>
                    <asp:Button ID="btnRemoveMajor" runat="server" OnClick="btnRemoveMajor_Click" Text="Remove" />
                </td>                         
            </tr>
            <tr style="height:50px">           
                <td colspan="2"  style="text-align:right;"><asp:Button ID="btnAddStudent" runat="server" Height="29px" Text="Add" Width="77px" OnClick="btnAddStudent_Click" /></td>                
                <td colspan="2"  style="text-align:left"><asp:Button ID="btnCancelAddStudent" runat="server" Height="29px" Text="Back" Width="80px" OnClick="btnCancelAddStudent_Click" /></td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
