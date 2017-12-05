<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewILMP.aspx.cs" MasterPageFile="~/HomeMasterPage.master" Inherits="ViewILMP" %>
<asp:Content ID="ilmpTemplateHead" ContentPlaceHolderID="homeMasterHead" runat="server">
    
    <style type="text/css">
        .auto-style2 {
            width: 233px;
            height: 35px;
        }
        .auto-style4 {
            width: 63px;
            height: 35px;
        }
        .auto-style9 {
            width: 46px;
            height: 35px;
        }
        .auto-style10 {
            width: 55px;
            height: 35px;
        }
        .auto-style11 {
            width: 96px;
        }
        #masterContent_homeMasterContent_gvWorkshop th:last-child,#masterContent_homeMasterContent_gvWorkshop td:last-child {
         display:none;
         }
        .auto-style12 {
            width: 86px;
        }
        .auto-style13 {
            width: 118px;
            height: 35px;
        }
        .auto-style14 {
            width: 265px;
        }
        .auto-style15 {
            width: 126px;
        }
        .auto-style16 {
            width: 68px;
        }        
    </style>
     <link href="/styles/viewilmp.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="ilmpTemplateContent" ContentPlaceHolderID="homeMasterContent" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>  
    <h3 style="margin-left:300px">ILMP</h3>
    <asp:HiddenField ID="hfIlmpId" runat="server" />
    <fieldset style="width:964px; margin-left:10px;border:2px solid #ccc"> <legend style="font-weight:bold"></legend>
        <table border="0" style="width: 960px">
            <tr>
               <td class="auto-style14"> <asp:RadioButtonList ID="rbtnlViewMode" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtnlViewMode_SelectedIndexChanged">
                    <asp:ListItem Selected="true">Overall</asp:ListItem><asp:ListItem>Semesterwise</asp:ListItem>
                </asp:RadioButtonList></td>
            <td style="text-align:right" class="auto-style4"><asp:Label ID="lblSemester" runat="server" Text="Semester"></asp:Label></td><td class="auto-style13">
                <asp:DropDownList ID="ddSemester" runat="server">
                </asp:DropDownList>
                </td>
            <td style="text-align:right" class="auto-style16"><asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label></td><td class="auto-style15">
                <asp:DropDownList ID="ddYear" runat="server">
                    <asp:ListItem Value="-1">Select</asp:ListItem>
                </asp:DropDownList>
                </td>
             <td><asp:Button ID="btnShow" runat="server" Text=" Show " Width="100px" OnClick="btnShow_Click" /></td>   
            </tr>
         </table>
    </fieldset>
    <fieldset style="width:964px;margin-left:10px;border:2px solid #ccc"> <legend style="font-weight:bold">ILMP</legend>
        <table style="width: 960px">
            <tr>
            <td class="auto-style10" style="text-align:right"><asp:Label ID="lblStudentId" runat="server" Text="StudentId" ></asp:Label></td>
            <td class="auto-style9"><asp:TextBox ID="txtStudentId" runat="server" ToolTip="StudentId" Width="100px"  Enabled="false"></asp:TextBox></td>
            <td class="auto-style4" style="text-align:right"><asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></td>
            <td class="auto-style2"><asp:TextBox ID="txtName" runat="server" MaxLength="100"  Width="206px" Enabled="false"></asp:TextBox></td>
            <td style="text-align:right" class="auto-style12"><asp:Label ID="lblActive" runat="server" Text="Active"></asp:Label></td>
            <td><asp:TextBox ID="txtActive" runat="server"  Width="65px" Enabled="false" ></asp:TextBox></td>
            <td style="text-align:right" class="auto-style11"><asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
            <td><asp:TextBox ID="txtDescription" runat="server" Height="45px" Width="172px" Enabled="false" TextMode="MultiLine" ></asp:TextBox></td>
            </tr></table><br />
    <div style="overflow-x:auto; width:962px;margin-left:0px">
  
    <asp:gridview ID="gvIlmp" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4" Width="955px" ForeColor="#333333" GridLines="None" >
        <AlternatingRowStyle BackColor="White" />
    <Columns>      
        <asp:BoundField DataField="CourseCode" HeaderText="CourseCode" SortExpression="CourseCode" ControlStyle-Height="35px"  />
        <asp:BoundField DataField="CourseType" HeaderText="Type" SortExpression="CourseType" ControlStyle-Height="35px"  />
        <asp:BoundField DataField="Semester" HeaderText="Semester" SortExpression="Semester" ControlStyle-Height="35px" />
        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"  ControlStyle-Height="35px" />
        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"  ControlStyle-Height="35px" />
        <asp:BoundField DataField="Credits" HeaderText="Credits" SortExpression="Credits" ControlStyle-Height="35px" />
        <asp:BoundField DataField="Level" HeaderText="Level" SortExpression="Level" ControlStyle-Height="35px" />
        <asp:BoundField DataField="AllPrerequisites" HeaderText="Prerequisites" SortExpression="AllPrerequisites" ControlStyle-Height="35px" />

    </Columns>
        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Height="35px" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:gridview>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbILMPV1ConnectionString %>" SelectCommand="SELECT ic.CourseCode, ic.CourseType, c.Title, c.Credits, c.Level, cp.AllPrerequisites, ic.Semester, ic.Year FROM Ilmp AS i INNER JOIN IlmpCourse AS ic ON i.IlmpID = ic.IlmpID INNER JOIN Course AS c ON c.CourseCode = ic.CourseCode LEFT OUTER JOIN CoursePrerequisite AS cp ON cp.CourseCode = c.CourseCode WHERE (i.IlmpID = @IlmpId) ORDER BY ic.Semester,ic.Year ">
                <SelectParameters>
                    <asp:QueryStringParameter Name="IlmpId" QueryStringField="id" />
                </SelectParameters>
            </asp:SqlDataSource>
    </div>
        
       <asp:GridView ID="gvWorkshop" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="705px" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Workshop">
            <ItemTemplate>
                <asp:label ID="lblWorkshop" runat="server" AppendDataBoundItems="true"></asp:label>
            </ItemTemplate>
            <ControlStyle Height="35px" Width="500px" />
            <ItemStyle Height="35px" Width="500px"  />
        </asp:TemplateField> 
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
        <table style="width:600px"><tr><td style="text-align:center"><asp:Button class="back" ID="btnCancelILMPView" runat="server" Text="Back" Height="25px" Width="80px" OnClick="btnCancelILMPView_Click"  /></td></tr></table>
        
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:dbILMPV1ConnectionString %>" SelectCommand="SELECT w.WorkshopID, w.WorkshopName FROM Ilmp AS i INNER JOIN TemplateCourse AS tc ON tc.TemplateID = i.TemplateId INNER JOIN Workshop AS w ON w.WorkshopID = tc.WorkshopID WHERE (tc.WorkshopID &lt;&gt; 0) AND (i.IlmpID = @IlmpId)">
            <SelectParameters>
                <asp:QueryStringParameter Name="IlmpId" QueryStringField="id" />
            </SelectParameters>
        </asp:SqlDataSource>
        
    </fieldset> </ContentTemplate>
    </asp:UpdatePanel></asp:Content>


