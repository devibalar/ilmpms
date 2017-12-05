<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddCourse1.aspx.cs" Inherits="AddCourse1" MasterPageFile="~/HomeMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <style type="text/css">
        #masterContent_homeMasterContent_gvPrerequisite th:last-child,#masterContent_homeMasterContent_gvPrerequisite td:last-child {
         display:none;
         }</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
   <h3>Add Course</h3>
    <asp:HiddenField ID="hfPrerequisite" runat="server" />
    <fieldset style="width:900px;margin:0 auto;border:2px solid #ccc"> <legend></legend>
	    <table style="margin:0 auto 0 0px; width: 875px;">
		    <tr style="height:50px">
			    <td style="text-align:right"><asp:Label ID="lblCourseCode" runat="server" Text="Course Code"></asp:Label></td>
                <td><asp:TextBox ID="txtCourseCode" runat="server"></asp:TextBox></td>
                <td style="text-align:right"><asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label></td>
                <td><asp:TextBox ID="txtTitle" runat="server" Width="250px"></asp:TextBox></td>
		    </tr>         
            <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblLevel" runat="server" Text="Level"></asp:Label></td>
                <td><asp:DropDownList ID="ddlLevel" runat="server" Height="16px" Width="57px">
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align:right"><asp:Label ID="lblCredits" runat="server" Text="Credits"></asp:Label></td>
                <td><asp:DropDownList ID="ddlCredits" runat="server" Height="16px" Width="60px">
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                </asp:DropDownList></td>
           </tr>
           <tr style="height:50px">
                <td style="text-align:right"><asp:Label ID="lblOfferedFrequency" runat="server" Text="Offered Frequency"></asp:Label></td>
                <td><asp:TextBox ID="txtOfferedFrequency" runat="server" Width="174px"></asp:TextBox></td>
               <td style="text-align:right"><asp:Label ID="lblCourseType" runat="server" Text="CourseType"></asp:Label></td>
                <td><asp:DropDownList ID="ddCourseType" runat="server"  Width="60px">
                    <asp:ListItem>COM</asp:ListItem>
                    <asp:ListItem>SPN</asp:ListItem>
                    <asp:ListItem>ELE</asp:ListItem>
                </asp:DropDownList></td>
           </tr>
            <tr>
                <td style="text-align:right">
                    <asp:CheckBox ID="rbtnGDIT" Text="GDIT" runat="server" AutoPostBack="true"  OnCheckedChanged="rbtnGDIT_CheckedChanged" />
                </td>
                <td>
                   <asp:CheckBoxList ID="cblGDITMajor" runat="server">
                        <asp:ListItem>SD</asp:ListItem>
                        <asp:ListItem>IS</asp:ListItem>
                        <asp:ListItem>NW</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
                <td style="text-align:right"> 
                    <asp:CheckBox ID="rbtnBIT" Text="BIT" runat="server" AutoPostBack="true" OnCheckedChanged="rbtnBIT_CheckedChanged" />
                </td>
                <td>
                    <asp:CheckBoxList ID="cblBITMajor" runat="server">
                        <asp:ListItem>SD</asp:ListItem>
                        <asp:ListItem>IS</asp:ListItem>
                        <asp:ListItem>NW</asp:ListItem>
                    </asp:CheckBoxList>
                </td>              
            </tr>              
            <tr>                
                <td style="text-align:right"><asp:Label ID="lblCorequisite" runat="server" Text="Corequisite"></asp:Label></td>
                <td><asp:DropDownList ID="ddCorequisite" runat="server"  Width="105px"></asp:DropDownList></td>
                <td style="text-align:right"><asp:Label ID="lblPrerequisite" runat="server" Text="Prerequisite"></asp:Label></td>
                <td><asp:DropDownList ID="ddPrerequisite" runat="server"  Width="104px" OnSelectedIndexChanged="ddPrerequisite_SelectedIndexChanged">
                    </asp:DropDownList><asp:Button ID="btnAddMore" runat="server" Text="Add Prerequisite" OnClick="btnAddMore_Click1"/>
                <br />
                    <asp:ListBox ID="lbPrerequisite" runat="server" Height="42px" Width="122px"></asp:ListBox>
                    <asp:Button ID="btnRemovePrerequisite" runat="server" OnClick="btnRemovePrerequisite_Click" Text="Remove" />
                </td>
            </tr>                        
            <tr  style="height:50px">
                <td colspan="2"  style="text-align:right"><asp:Button ID="btnAddCourse" runat="server" OnClick="btnAddCourse_Click" Text="Add" Width="68px" /></td>
                 <td colspan="2"  style="text-align:left"><asp:Button ID="btnBack" runat="server"  Text="Back" Width="68px" OnClick="btnBack_Click" /></td>
            </tr>
	    </table>
    </fieldset>       
</asp:Content>