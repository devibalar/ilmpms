<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMasterPage.master" AutoEventWireup="true" CodeFile="CourseOfferingList.aspx.cs" Inherits="DelCourseOffering" MaintainScrollPositionOnPostback="true" enableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>  
    <h2 style="margin:0 auto 0 337px; width: 346px;">Course Offering</h2>
    <fieldset style="width:700px;margin:0 auto;border:2px solid #ccc">
    <legend style="font-weight:bold">Search</legend>
    <table>
        <tr>
            <td><asp:Label ID="lblCourseCode" runat="server" Text="Course Code "></asp:Label></td>
            <td><asp:DropDownList ID="ddlSearchCourseCode" runat="server">
            </asp:DropDownList></td>   
            <td><asp:Label ID="lblSemester" runat="server" Text="Semester "></asp:Label></td>
            <td><asp:DropDownList ID="ddlSearchSemester" runat="server">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
            </asp:DropDownList></td>
            <td><asp:Label ID="lblYear" runat="server" Text="Year "></asp:Label></td>
            <td><asp:DropDownList ID="ddlSearchYear" runat="server"></asp:DropDownList></td>         
            <td><asp:Button ID="btnSearchCourseOffering" runat="server" Text="Search" OnClick="btnSearchCourseOffering_Click" /></td>
        </tr>
    </table>
    </fieldset>
     <fieldset style="width:700px;margin:0 auto;border:2px solid #ccc">
         <legend style="font-weight:bold">Result</legend>
        <div style="margin:0 auto">

            <asp:GridView ID="gvSearchCourseOffering" runat="server" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvSearchCourseOffering_PageIndexChanging" AutoGenerateColumns="False" DataKeyNames="CourseCode,Semester,Year" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="gvSearchCourseOffering_SelectedIndexChanged" CellPadding="4" GridLines="None" ForeColor="#333333" Width="686px" PageSize="15" EnableSortingAndPagingCallbacks="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" ReadOnly="True" SortExpression="CourseCode" ControlStyle-Height="35px"  ></asp:BoundField>
                    <asp:BoundField DataField="Semester" HeaderText="Semester" ReadOnly="True" SortExpression="Semester" ControlStyle-Height="35px"  />
                    <asp:BoundField DataField="Year" HeaderText="Year" ReadOnly="True" SortExpression="Year" ControlStyle-Height="35px"  />
                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" ControlStyle-Height="35px"  />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CourseCode") %>'></asp:Label>
                </EmptyDataTemplate>
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbILMPV1ConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [CourseCode], [Semester], [Year] FROM [CourseOffering] ORDER BY [Year], [Semester]">
            </asp:SqlDataSource>
        </div>
    </fieldset>
    <div style="margin:0 auto  0 337px;">
    <fieldset style="width: 916px"><legend></legend>
    <asp:Button ID="btnAdd" runat="server" Text="Add" Width="80px" OnClick="btnAdd_Click" /> 
    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="80px" OnClick="btnEdit_Click"  /> 
    <asp:Button ID="btnUpload" runat="server" Text="Upload" Width="80px" OnClick="btnUpload_Click"  /> 
    
    </fieldset> 
    </div>
    </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>

