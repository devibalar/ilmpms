<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ILMPTemplate.aspx.cs" Inherits="ILMPTemplate"  MasterPageFile="~/HomeMasterPage.master"  MaintainScrollPositionOnPostBack="true"%>

<asp:Content ID="ilmpTemplateHead" ContentPlaceHolderID="homeMasterHead" runat="server">
     <link href="/styles/ilmptemplate.css" rel="stylesheet" type="text/css" />       
     <style type="text/css">
         .auto-style1 {
             width: 220px;
         }
         .auto-style4 {
             width: 54px;
         }
         #masterContent_homeMasterContent_gvWorkshop th:last-child,#masterContent_homeMasterContent_gvWorkshop td:last-child {
         display:none;
         }
         .auto-style6 {
             height: 35px;
             width: 274px;
         }
         .auto-style7 {
             height: 35px;
             width: 129px;
         }
         .auto-style8 {
             height: 35px;
             width: 209px;
         }
     </style>
</asp:Content>
<asp:Content ID="ilmpTemplateContent" ContentPlaceHolderID="homeMasterContent" runat="server"> 
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>        
            <div><h3>Create ILMP Template</h3></div>      
        <fieldset style="width:964px; margin-left:10px;border:2px solid #ccc"><legend></legend>                                               
        <table style="margin:0 auto">
        <tr style="height:50px;" >
            <td colspan="2"><asp:RadioButtonList ID="rbtnlTemplateType" runat="server" RepeatDirection="Horizontal" Height="17px" Width="256px" OnSelectedIndexChanged="rbtnlTemplateType_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Selected="True">Generic</asp:ListItem>
                <asp:ListItem>Custom</asp:ListItem>
            </asp:RadioButtonList></td>
        </tr>
        <tr style="height:50px">
            <td style="width:80px;text-align:right"><asp:Label ID="lblProgramme" runat="server" Text="Programme"></asp:Label></td>
            <td class="auto-style8" ><asp:DropDownList ID="ddProgramme" runat="server" OnSelectedIndexChanged="ddProgramme_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
            <td class="auto-style4" style="text-align:right"><asp:Label ID="lblMajor" runat="server" Text="Major"></asp:Label></td>
            <td class="auto-style6"><asp:DropDownList ID="ddMajor" runat="server" OnSelectedIndexChanged="ddMajor_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
            <td class="auto-style1" style="text-align:right"><asp:Label ID="lblTemplateName" runat="server" Text="TemplateName"></asp:Label></td>
            <td style="width:300px;height:35px"><asp:TextBox ID="txtTemplateName" runat="server" Width="179px"></asp:TextBox></td>            
        </tr>      
        <tr  style="height:50px">
            <td style="width:80px;text-align:right"><asp:Label ID="lblStudentId" runat="server" Text="StudentId"></asp:Label></td>
            <td class="auto-style8"><asp:TextBox style="width:150px" ID="txtStudentId" runat="server" OnTextChanged="txtStudentId_TextChanged" AutoPostBack="true"></asp:TextBox></td>
            <td class="auto-style4" style="text-align:right"><asp:Label ID="lblStudentName" runat="server" Text="StudentName"></asp:Label></td>
            <td class="auto-style6"><asp:TextBox style="width:150px" ID="txtStudentName" runat="server"></asp:TextBox></td>
           
        </tr>      
        </table></fieldset> <asp:HiddenField ID="hfIlmpTemplateId" runat="server" /> 
    <div> 
        <asp:gridview ID="gvCourse" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvCourse_RowDataBound" style="margin-left:2px"  Width="976px" PageSize="50" CellPadding="0" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="true">
            <AlternatingRowStyle BackColor="White" />
        <Columns> 
             <asp:TemplateField HeaderText="Semester">
                <ItemTemplate>
                    <asp:DropDownList ID="ddSemester" runat="server" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="ddSemester_SelectedIndexChanged">                               
                        <asp:ListItem Value="-1" Selected="True">Select</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="80px" />
                <ItemStyle Height="35px" Width="80px"  />
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:DropDownList ID="ddYear" runat="server" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="ddYear_SelectedIndexChanged">                               
                        <asp:ListItem Value="-1" Selected="True">Select</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="80px" />
                <ItemStyle Height="35px" Width="80px"  />
            </asp:TemplateField>      
            <asp:TemplateField HeaderText="CourseCode">
                <ItemTemplate>
                    <asp:DropDownList ID="ddCourseCode" runat="server" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="ddCourseCode_SelectedIndexChanged">                               
                        <asp:ListItem Value="-1" Selected="True">Select</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="80px" />
                <ItemStyle Height="35px" Width="80px"  />
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Type" >
                <ItemTemplate>
                    <asp:DropDownList ID="ddCourseType" runat="server" AppendDataBoundItems="false">  
                        <asp:ListItem>COM</asp:ListItem>
                        <asp:ListItem>SPN</asp:ListItem>
                        <asp:ListItem>ELE</asp:ListItem>                             
                    </asp:DropDownList>                    
                </ItemTemplate>
                <ControlStyle Height="35px" Width="80px" />
                <ItemStyle Height="35px" Width="80px"  />
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Course Name">
                <ItemTemplate>
                    <asp:Label ID="lblCourseName" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="200px" />
                <ItemStyle Height="35px" Width="80px"  />
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Credits">
                <ItemTemplate>
                    <asp:Label ID="lblCredits" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="50px" />
                <ItemStyle Height="35px" Width="50px"  />
            </asp:TemplateField>  
            <asp:TemplateField HeaderText="Level">
                <ItemTemplate>
                    <asp:Label ID="lblLevel" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="50px" />
                <ItemStyle Height="35px" Width="50px"  />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Pre-requisite(s)">
                <ItemTemplate>
                    <asp:Label ID="lblPrerequisites" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="80px" />
                <ItemStyle Height="35px" Width="80px"  />
            </asp:TemplateField>                                                                  
        </Columns>
            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#333333" HorizontalAlign="Center" BackColor="#FFCC66" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"  />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:gridview>
        
        </div>
       <fieldset><legend>
           <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" style="width:150px;margin-left:5px;margin-right:5px"  OnClick="btnAddCourse_Click" />
           <asp:Button ID="btnDeleteCourse" runat="server" Text="Delete Course" style="width:150px;margin-left:5px;margin-right:5px" OnClick="btnDeleteCourse_Click" OnClientClick="return confirm('Are you sure you want delete');"/>
           <asp:Button ID="btnEditCourse" runat="server" Text="Edit Course" style="width:150px;margin-left:5px;margin-right:5px" />
        </legend></fieldset>
  
        <asp:GridView ID="gvWorkshop" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  AutoGenerateSelectButton="true" CellSpacing="1" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
             <asp:TemplateField HeaderText="Workshop">
                <ItemTemplate>
                    <asp:DropDownList ID="ddWorkshop" runat="server" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="ddWorkshop_SelectedIndexChanged" >                               
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="350px" />
                <ItemStyle Height="35px" Width="350px"  />
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

        <fieldset><legend>
           <asp:Button ID="btnAddWorkshop" runat="server" Text="Add Workshop" style="width:150px;margin-left:5px;margin-right:5px" OnClick="btnAddWorkshop_Click" />
           <asp:Button ID="btnDeleteWorkshop" runat="server" Text="Delete Workshop" style="width:160px;margin-left:5px;margin-right:5px" OnClick="btnDeleteWorkshop_Click"/>
           <asp:Button ID="btnEditWorkshop" runat="server" Text="Edit Workshop" style="width:150px;margin-left:5px;margin-right:5px" />
        </legend></fieldset>
            <br />
        <div style="align-content:center"><asp:Button ID="btnSaveTemplate" runat="server" Text="Save Template" style="width:150px;margin-left:5px;margin-right:5px" OnClick="btnSaveTemplate_Click"  /> </div>                                                                       
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveTemplate" />
        </Triggers>
        </asp:UpdatePanel>
</asp:Content>