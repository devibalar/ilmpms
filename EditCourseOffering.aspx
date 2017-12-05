<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMasterPage.master" AutoEventWireup="true" CodeFile="EditCourseOffering.aspx.cs" Inherits="EditCourseOffering" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 125px;
        }
        .auto-style2 {
            width: 126px;
        }
        .auto-style3 {
            width: 127px;
        }
        .auto-style4 {
            width: 224px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
    <br />
    <div style="margin:0 auto 0 350px"><h2>Edit CourseOffering</h2></div>
    <fieldset style="width:700px;margin:0 auto;border:2px solid #ccc"><legend></legend>
    <table style="margin:0 auto">
       <tr style="height:50px">
           <asp:HiddenField ID="hfSemester" runat="server" />
           <asp:HiddenField ID="hfYear" runat="server" />
       <td style="text-align:right"><asp:Label ID="lblCourseCodeOffering" runat="server" Text="Course Code"></asp:Label></td>
        <td class="auto-style2"> <asp:TextBox ID="txtEditCourseCodeOffering" runat="server" Enabled="False"></asp:TextBox> </td> 
        <td> <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label></td> 
        <td class="auto-style4"> <asp:TextBox ID="txtTitle" runat="server" Text="" Enabled="false" Width="200px"></asp:TextBox></td>              
      </tr>
        <tr  style="height:50px">
        <td class="auto-style1" style="text-align:right"><asp:Label ID="lblSemesterCourseOffering" runat="server" Text="Semester "></asp:Label></td>
         <td class="auto-style3"><asp:DropDownList ID="ddlEditSemesterCourseOffering" runat="server">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
        </asp:DropDownList></td>
        <td style="text-align:right"><asp:Label ID="lblYearCourseOffering" runat="server" Text="Year"></asp:Label></td>
         <td class="auto-style4"><asp:TextBox ID="txtEditYearCourseOffering" runat="server"></asp:TextBox></td>
         <td class="auto-style1"></td> <td class="auto-style3"></td>
       </tr>
        <tr  style="height:50px">
            <td colspan="2"  style="text-align:right"><asp:Button ID="btnUpdateCourseOffering" runat="server" Text="Update" Width="80px" OnClick="btnUpdateCourseOffering_Click" /></td>            
             <td colspan="1"  style="text-align:right"><asp:Button ID="btnDelCourseOffering" OnClientClick="javascript:return confirm('Are you sure?');"  runat="server" Text="Delete" OnClick="btnDelCourseOffering_Click" Width="80px" /></td>
            <td colspan="1"  style="text-align:left"> <asp:Button ID="btnCancelCourseOffering" runat="server" Text="Back" Width="80px" OnClick="btnCancelCourseOffering_Click" /></td>
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

