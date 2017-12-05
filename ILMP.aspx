<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ILMP.aspx.cs" Inherits="ILMP" MasterPageFile="~/HomeMasterPage.master" MaintainScrollPositionOnPostback="true"  EnableEventValidation="false" %>

<asp:Content ID="ilmpTemplateHead" ContentPlaceHolderID="homeMasterHead" runat="server">
    
    <style type="text/css">
        .auto-style2 {
            width: 233px;
            height: 44px;
            margin:0px;
        }
        .auto-style4 {
            width: 63px;
            height: 44px;
        }
        .auto-style6 {
            width: 149px;
        }
        .auto-style7 {
            width: 102px;
        }
        .auto-style9 {
            width: 15px;
            height: 44px;
        }
        .auto-style10 {
            width: 55px;
            height: 44px;
        }
        .auto-style11 {
            width: 96px;
        }
        #masterContent_homeMasterContent_gvWorkshop th:last-child,#masterContent_homeMasterContent_gvWorkshop td:last-child {
         display:none;
         }
        .auto-style12 {
            width: 85px;
        }
        .auto-style13 {
            width: 96px;
            height: 44px;
        }
        .auto-style14 {
            height: 44px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="ilmpTemplateContent" ContentPlaceHolderID="homeMasterContent" runat="server">
      <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>  
    <div><h3>Create ILMP</h3></div>
    <asp:HiddenField ID="hfTemplateId" runat="server" />
    <fieldset style="width:964px; margin-left:10px;border:2px solid #ccc"> <legend style="font-weight:bold">Choose Template</legend>
        <table border="0" style="width: 960px">
            <tr>
             <td colspan="2">
                <asp:RadioButtonList ID="rbtnILMPTemplateType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnILMPTemplateType_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Selected="True">Generic</asp:ListItem>
                    <asp:ListItem>Custom</asp:ListItem>
                </asp:RadioButtonList>
             </td>
                <td style="text-align:right"><asp:Label ID="lblCustomStudentId" runat="server" Text="StudentId"></asp:Label></td>
                <td><asp:TextBox ID="txtCustomStudentId" runat="server" MaxLength="10" OnTextChanged="txtCustomStudentId_TextChanged" AutoPostBack="true" Width="73px"></asp:TextBox></td>  
            </tr>
            <tr>
             <td class="auto-style7" style="text-align:right"><asp:Label ID="lblProgrammeCode" runat="server" Text="Programme"></asp:Label></td>
             <td><asp:DropDownList ID="ddProgramme" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddProgramme_SelectedIndexChanged"></asp:DropDownList></td>
             <td style="text-align:right"><asp:Label ID="lblMajor" runat="server" Text="Major"></asp:Label></td>
             <td class="auto-style12"><asp:DropDownList ID="ddMajor" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddMajor_SelectedIndexChanged"></asp:DropDownList></td>
             <td style="text-align:right"><asp:Label ID="lblTemplateName" runat="server" Text="TemplateName"></asp:Label></td>
             <td class="auto-style6" ><asp:DropDownList ID="ddTemplateName" runat="server" Width="142px"></asp:DropDownList></td>
             <td style="text-align:right"><asp:Label ID="lblStudentMajor" runat="server" Text="Major"></asp:Label></td>
             <td><asp:TextBox ID="txtStudentMajor" runat="server" Enabled ="false" Width="100px"></asp:TextBox></td>        
             <td><asp:Button ID="btnGetTemplate" runat="server" Text="GetTemplate" OnClick="btnGetTemplate_Click" /></td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width:964px;margin-left:10px;border:2px solid #ccc"> <legend style="font-weight:bold">ILMP</legend>
        <table style="width: 960px; height: 64px;">
            <tr>
            <td class="auto-style10" style="text-align:right"><asp:Label ID="lblStudentId" runat="server" Text="StudentId"></asp:Label></td>
            <td class="auto-style9"><asp:TextBox ID="txtStudentId" runat="server" ToolTip="StudentId" Width="100px" OnTextChanged="txtStudentId_TextChanged" AutoPostBack="true"></asp:TextBox></td>
            <td class="auto-style4" style="text-align:right"><asp:Label ID="lblName" runat="server" Text="Name"></asp:Label></td>
            <td class="auto-style2"><asp:TextBox ID="txtName" runat="server" MaxLength="100"  Width="206px"></asp:TextBox></td>
            <td style="text-align:right" class="auto-style13"><asp:Label ID="lblActive" runat="server" Text="Active"></asp:Label></td>
            <td class="auto-style14"><asp:DropDownList ID="ddActive" runat="server"  Width="65px"> 
                 <asp:ListItem>No</asp:ListItem><asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList></td>
            <td style="text-align:right" class="auto-style13"><asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label></td>
            <td class="auto-style14"><asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="45px" Width="206px" TextMode="MultiLine"></asp:TextBox></td>
            </tr></table><br />
    <div style="overflow-x:auto; width:962px;margin-left:0px">
         
    <asp:gridview ID="gvIlmp" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4" Width="900px" ForeColor="#333333" GridLines="None">
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
            <asp:DropDownList ID="ddCourseType" runat="server">
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
        <ControlStyle Width="50px" />
        <ItemStyle Width="50px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Year">
        <ItemTemplate>
            <asp:DropDownList ID="ddYear" runat="server" AppendDataBoundItems="false" OnSelectedIndexChanged="ddYear_SelectedIndexChanged" AutoPostBack="true" >        
            </asp:DropDownList>
        </ItemTemplate>
        <ControlStyle Width="60px" />
        <ItemStyle Width="60px" />
    </asp:TemplateField>   
    <asp:TemplateField HeaderText="Title">
        <ItemTemplate>
            <asp:Label ID="lblTitle" runat="server"  AppendDataBoundItems="true"></asp:Label>           
        </ItemTemplate>
        <ControlStyle Width="350px" />
        <ItemStyle Width="350px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Credits">
        <ItemTemplate>
            <asp:Label ID="lblCredits" runat="server" AppendDataBoundItems="true"></asp:Label>           
        </ItemTemplate>
        <ControlStyle Width="50px" />
        <ItemStyle Width="50px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Level">
        <ItemTemplate>
            <asp:Label ID="lblLevel" runat="server" AppendDataBoundItems="true"></asp:Label>           
        </ItemTemplate>
        <ControlStyle Width="50px" />
        <ItemStyle Width="50px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Prerequisites">
        <ItemTemplate>
            <asp:Label ID="lblPrerequisites" runat="server" AppendDataBoundItems="true"></asp:Label>           
        </ItemTemplate>           
        <ControlStyle Width="180px" />
        <ItemStyle Width="180px" />
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
          
    <asp:GridView ID="gvWorkshop" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="867px" >
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
            
    <div style="margin:0 auto"><asp:Button ID="btnSave" runat="server" Text="Save" Width="150px" OnClick="btnSave_Click" /></div>
            
    </fieldset>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>