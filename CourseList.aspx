<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseList.aspx.cs" Inherits="CourseList" MasterPageFile="~/HomeMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 83px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
   <h3>Course List</h3>
    <fieldset style="width:700px;margin:0 auto;border:2px solid #ccc"> <legend></legend>
        <table style="margin:0 auto">
            <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblSearchCourseCode" runat="server" Text="Course  Code "></asp:Label></td>
                <td class="auto-style1"><asp:TextBox ID="txtSearchCourseCode"  runat="server"></asp:TextBox></td>
                <td><asp:Label ID="lblSearchTitle" runat="server" Text="Title"></asp:Label></td>
                <td><asp:TextBox ID="txtSearchTitle" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="btnSearchCourse" runat="server" Text="Search" Width="80px" Height="29px" OnClick="btnSearchCourse_Click" /></td>
            </tr>
        </table>
    </fieldset>
         <div style="overflow-x:auto; width:962px;margin-left:0px">
        <asp:GridView ID="gvSearchCourse" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvSearchCourse_PageIndexChanging" PageSize="15"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" ReadOnly="True" SortExpression="CourseCode" ItemStyle-Height="35px"  ControlStyle-Height="35px"/>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" ControlStyle-Height="35px" ItemStyle-Height="35px" />
                <asp:BoundField DataField="Credits" HeaderText="Credits" SortExpression="Credits" ControlStyle-Height="35px" ItemStyle-Height="35px"/>
                <asp:BoundField DataField="Level" HeaderText="Level" SortExpression="Level" ControlStyle-Height="35px" ItemStyle-Height="35px"/>
                <asp:BoundField DataField="ProgrammeID" HeaderText="ProgrammeID" ReadOnly="True" SortExpression="ProgrammeID" ControlStyle-Height="35px" ItemStyle-Height="35px"/>
                <asp:BoundField DataField="MajorID" HeaderText="MajorID" ReadOnly="True" SortExpression="MajorID" ControlStyle-Height="35px" ItemStyle-Height="35px"/>
                <asp:BoundField DataField="CourseType" HeaderText="CourseType" ReadOnly="True" SortExpression="CourseType" ControlStyle-Height="35px" ItemStyle-Height="35px"/>
                <asp:BoundField DataField="AllPrerequisites" HeaderText="AllPrerequisites" SortExpression="AllPrerequisites" ControlStyle-Height="35px" ItemStyle-Height="35px"/>
                <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" ItemStyle-Height="35px"/>
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />           
        </asp:GridView>        
 </div>
                    
    <asp:Button ID="btnAddCourse" runat="server" Text="Add" Width="80px" OnClick="btnAddCourse_Click" />
    <asp:Button ID="btnEditCourse" runat="server" Text="Edit" Width="80px" OnClick="btnEditCourse_Click" />
    <asp:Button ID="btnUploadCourse" runat="server" Text="Upload" Width="80px" OnClick="btnUploadCourse_Click" /> 
        </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnSearchCourse" />
    </Triggers>
    </asp:UpdatePanel>  
</asp:Content>

