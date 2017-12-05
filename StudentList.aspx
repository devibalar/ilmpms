<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMasterPage.master" AutoEventWireup="true" CodeFile="StudentList.aspx.cs" Inherits="EditStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
    <fieldset class="">
     <legend>SearchSearch</legend>
    <div>
        <asp:Label ID="lblSearchStudentID" runat="server" Text="Student ID  "></asp:Label>&nbsp&nbsp
        <asp:TextBox ID="txtSearchStudentID" runat="server"></asp:TextBox>
        &nbsp&nbsp&nbsp&nbsp
        <asp:Label ID="lblSearchProgramme" runat="server" Text="Programme  "></asp:Label>&nbsp&nbsp
        <asp:DropDownList ID="ddlSearchProgramme" runat="server">
            <asp:ListItem>All</asp:ListItem>
            <asp:ListItem>BIT</asp:ListItem>
            <asp:ListItem>GDIT</asp:ListItem>
        </asp:DropDownList>&nbsp&nbsp&nbsp&nbsp&nbsp
        <asp:Button ID="btnSearchStudent" runat="server" Text="Search" OnClick="btnSearchStudent_Click" />
    </div>
</fieldset>
    <p></p>
  <fieldset>
      <legend></legend>
      <div>
          <asp:GridView ID="gvSearchStudent" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged = "gvSearchStudent_SelectedIndexChanged" CellPadding="4" GridLines="None" ForeColor="#333333" Width="976px" AllowPaging="True" AllowSorting="True"  OnPageIndexChanging="gvSearchStudent_PageIndexChanging">
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                  <asp:BoundField DataField="StudentID" HeaderText="StudentID" SortExpression="StudentID"  ControlStyle-Height="35px"/>
                  <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName"   ControlStyle-Height="35px"/>
                  <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName"  ControlStyle-Height="35px" />                  
                  <asp:BoundField DataField="ProgrammeID" HeaderText="ProgrammeID" SortExpression="ProgrammeID"  ControlStyle-Height="35px" />
                  <asp:BoundField DataField="MajorID" HeaderText="MajorID" SortExpression="MajorID"  ControlStyle-Height="35px" />
                  <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId"  ControlStyle-Height="35px"/>
                  <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"  ControlStyle-Height="35px" />
                  <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active"  ControlStyle-Height="35px" />
                  <asp:CommandField ButtonType="Button" ShowSelectButton="True"  ControlStyle-Height="30px" />
              </Columns>
              <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
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
  </fieldset>
    <p></p>
  <asp:Button ID="btnAdd" runat="server" Text="Add" Width="80px" OnClick="btnAdd_Click"/>&nbsp &nbsp&nbsp
    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="80px" OnClick="btnEdit_Click" style="height: 26px" />&nbsp &nbsp&nbsp
    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="80px" OnClick="btnUpload_Click" />&nbsp &nbsp&nbsp
</asp:Content>

