<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCourseOffering.aspx.cs" Inherits="AddCourseOffering" MasterPageFile="~/HomeMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 115px;
        }
        .auto-style2 {
            width: 187px;
        }
        .auto-style3 {
            width: 123px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
   <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbILMPV1ConnectionString %>" SelectCommand="SELECT [CourseCode] FROM [Course]"></asp:SqlDataSource>
    <br />
    <div style="margin:0 auto 0 350px"><h2>Add CourseOffering</h2></div>
    <fieldset style="width:700px;margin:0 auto;border:2px solid #ccc"> <legend></legend>
   <table style="margin:0 auto">
       <tr style="height:50px">
       <td style="text-align:right"> <asp:Label ID="lblCourseCodeOffering" runat="server" Text="Course Code  "></asp:Label></td>
       <td class="auto-style2"> <asp:DropDownList ID="ddlCourseCodeOffering" runat="server" DataSourceID="SqlDataSource1" DataTextField="CourseCode" DataValueField="CourseCode" Height="20px" Width="127px">
        </asp:DropDownList></td>
      <td class="auto-style1" style="text-align:right">
        <asp:Label ID="lblSemesterCourseOffering" runat="server" Text="Semester "></asp:Label></td>
     <td class="auto-style3"><asp:DropDownList ID="ddlSemesterCourseOffering" runat="server" Height="16px" Width="48px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
        </asp:DropDownList></td>   
      </tr>
    <tr  style="height:50px">
         <td style="text-align:right"><asp:Label ID="lblYearCourseOffering" runat="server" Text="Year  "></asp:Label></td>
         <td class="auto-style2"><asp:TextBox ID="txtYearCourseOffering" runat="server"></asp:TextBox></td>
         <td class="auto-style1"></td> <td class="auto-style3"></td>
    </tr>
    <tr  style="height:50px">
         <td colspan="2"  style="text-align:right"><asp:Button ID="btnAddCourseOffering" runat="server" Height="32px" OnClick="btnAddCourseOffering_Click" Text="Add" Width="101px" /></td>
         <td colspan="2"  style="text-align:left"><asp:Button ID="btnCancelCourseOffering" runat="server" Height="32px" Text="Back" Width="80px" OnClick="btnCancelCourseOffering_Click" /></td>
    </tr>
       </table>
    </fieldset>
</asp:Content>
