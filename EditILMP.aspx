<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditILMP.aspx.cs" Inherits="EditILMP" MasterPageFile="~/HomeMasterPage.master" %>

<asp:Content ID="editIlmpHead" ContentPlaceHolderID="homeMasterHead" runat="server">
    <style>
          #masterContent_homeMasterContent_gvWorkshop th:last-child,#masterContent_homeMasterContent_gvWorkshop td:last-child {
         display:none;
         }
    </style>
</asp:Content>
<asp:Content ID="editIlmpContent" ContentPlaceHolderID="homeMasterContent" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
    <ContentTemplate>  <asp:HiddenField ID="hfIlmpId" runat="server" />
         <fieldset style="width:964px;margin-left:10px;border:2px solid #ccc"> <legend style="font-weight:bold">ILMP</legend>
        <table style="width: 960px">
            <tr>
            <td class="auto-style10" style="text-align:right"><asp:Label ID="lblStudentId" runat="server" Text="StudentId"></asp:Label></td>
            <td class="auto-style9"><asp:TextBox ID="txtStudentId" runat="server" ToolTip="StudentId" Width="100px" Enabled="false" ></asp:TextBox></td>
            <td class="auto-style4" style="text-align:right"><asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></td>
            <td class="auto-style2"><asp:TextBox ID="txtName" runat="server" MaxLength="100" Width="206px" Enabled="false" ></asp:TextBox></td>
            <td style="text-align:right" class="auto-style11"><asp:Label ID="lblActive" runat="server" Text="Active"></asp:Label></td>
            <td><asp:DropDownList ID="ddActive" runat="server"  Width="65px"> 
                 <asp:ListItem>No</asp:ListItem><asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList></td>
            <td style="text-align:right" class="auto-style11"><asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
            <td><asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="45px" Width="206px" TextMode="MultiLine" ></asp:TextBox></td>
            </tr></table><br />
            <div style="overflow-x:auto; width:962px;margin-left:0px">
                 <asp:gridview ID="gvIlmpCourse" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4" Width="900px" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
            <Columns>      
            <asp:TemplateField HeaderText="CourseCode">
                <ItemTemplate>
                    <asp:Label ID="lblCourseCode" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="80px" Height="35px" />
                <ItemStyle Width="80px" Height="35px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <asp:DropDownList ID="ddCourseType" runat="server" AppendDataBoundItems="false"  >      
                        <asp:ListItem>COM</asp:ListItem>                       
                        <asp:ListItem>SPN</asp:ListItem>                       
                        <asp:ListItem>ELE</asp:ListItem>                       
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Width="80px" Height="35px" />
                <ItemStyle Width="80px" Height="35px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Semester">
                <ItemTemplate>
                    <asp:DropDownList ID="ddSemester" runat="server" AppendDataBoundItems="false" OnSelectedIndexChanged="ddSemester_SelectedIndexChanged" AutoPostBack="true" >                             
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Width="50px" Height="35px" />
                <ItemStyle Width="50px" Height="35px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:DropDownList ID="ddYear" runat="server" AppendDataBoundItems="false" OnSelectedIndexChanged="ddYear_SelectedIndexChanged" AutoPostBack="true" >        
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Width="60px" Height="35px"/>
                <ItemStyle Width="60px" Height="35px" />
            </asp:TemplateField>   
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="lblTitle" runat="server"  AppendDataBoundItems="true"></asp:Label>           
                </ItemTemplate>
                <ControlStyle Width="300px" Height="35px"/>
                <ItemStyle Width="300px" Height="35px"/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Credits">
                <ItemTemplate>
                    <asp:Label ID="lblCredits" runat="server" AppendDataBoundItems="true"></asp:Label>           
                </ItemTemplate>
                <ControlStyle Width="50px" Height="35px" />
                <ItemStyle Width="50px" Height="35px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Level">
                <ItemTemplate>
                    <asp:Label ID="lblLevel" runat="server" AppendDataBoundItems="true"></asp:Label>           
                </ItemTemplate>
                <ControlStyle Width="40px" Height="35px" />
                <ItemStyle Width="40px" Height="35px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prerequisites">
                <ItemTemplate>
                    <asp:Label ID="lblPrerequisites" runat="server" AppendDataBoundItems="true"></asp:Label>           
                </ItemTemplate>           
                <ControlStyle Width="180px" Height="35px" />
                <ItemStyle Width="180px" Height="35px" />
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Result">
                <ItemTemplate>
                    <asp:DropDownList ID="ddResult" runat="server" AppendDataBoundItems="false"  >
                        <asp:ListItem>NA</asp:ListItem>        
                        <asp:ListItem>F</asp:ListItem>
                        <asp:ListItem>P</asp:ListItem>
                        <asp:ListItem>ENR</asp:ListItem>
                    </asp:DropDownList>        
                </ItemTemplate>           
                <ControlStyle Width="45px" Height="35px" />
                <ItemStyle Width="45" Height="35px" />
            </asp:TemplateField>       
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
            </asp:gridview>
    
            </div>
          
            <asp:GridView ID="gvWorkshop" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="950px" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Workshop">
                    <ItemTemplate>
                        <asp:label ID="lblWorkshop" runat="server" AppendDataBoundItems="true" Text='<%# Eval("Column1")%>'></asp:label>
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
            
            <table><tr style="height:50px">           
            <td colspan="2"  style="text-align:right;"><asp:Button ID="btnUpdate" runat="server" Text="Update ILMP"  Width="100px"  OnClick="btnUpdate_Click" /></td>
            </tr></table>
            </fieldset>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>