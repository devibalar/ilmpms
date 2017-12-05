<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ILMPListStaff.aspx.cs" Inherits="ILMPListStaff" MasterPageFile="~/HomeMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 204px;
        }
        .auto-style2 {
            width: 137px;
        }
        #masterContent_homeMasterContent_gvStudentILMPList th:last-child,#masterContent_homeMasterContent_gvStudentILMPList td:last-child,
        #masterContent_homeMasterContent_gvStudentILMPList th:nth-last-child(2),#masterContent_homeMasterContent_gvStudentILMPList td:nth-last-child(2),
        #masterContent_homeMasterContent_gvStudentILMPList th:nth-last-child(3),#masterContent_homeMasterContent_gvStudentILMPList td:nth-last-child(3),
        #masterContent_homeMasterContent_gvStudentILMPList th:nth-last-child(4),#masterContent_homeMasterContent_gvStudentILMPList td:nth-last-child(4) {
         display:none;
         }
    </style>

    <script>
     function Confirm() {
         var confirm_value = document.createElement("INPUT");
         confirm_value.type = "hidden";
         confirm_value.name = "confirm_value";
         if (confirm("Do you want to change ILMP activate?")) {
             confirm_value.value = "Yes";
         } else {
             confirm_value.value = "No";
         }
         document.forms[0].appendChild(confirm_value);
     }
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
     <asp:ScriptManager id="ScriptManager1" runat="server">
     </asp:ScriptManager> 
    <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate> <asp:HiddenField ID="HiddenField1" runat="server" />
             <div><h3>Search Student ILMP</h3></div>
            <fieldset style="width:964px; margin-left:10px;border:2px solid #ccc"> <legend style="font-weight:bold">Student ILMP</legend>
            <table border="0" style="width: 960px">
            <tr>
                <td class="auto-style2" style="text-align:right"><asp:Label ID="lblSearchStudentId" runat="server" Text="StudentId"></asp:Label></td>
                <td class="auto-style1"><asp:TextBox ID="txtSearchStudentId" runat="server"></asp:TextBox></td>
                <td><asp:Button ID="btnSearchILMP" runat="server" Text="Search ILMP"  OnClick="btnSearchILMP_Click"/></td>
           </tr>
           </table>
                <asp:GridView ID="gvStudentILMPList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="846px" AutoGenerateSelectButton="true" >
                     <Columns>  
                        <asp:TemplateField HeaderText="StudentId">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentId" runat="server" Text='<%# Eval("Column1")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="80px" Height="35px" />
                            <ItemStyle Width="80px" Height="35px" />
                       </asp:TemplateField>  
                          <asp:TemplateField HeaderText="IlmpId">
                            <ItemTemplate>
                                <asp:Label ID="lblIlmpId" runat="server" Text='<%# Eval("Column2")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="80px" Height="35px" />
                            <ItemStyle Width="80px" Height="35px" />
                       </asp:TemplateField>  
                       <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Column3")%>'></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="80px" Height="35px" />
                            <ItemStyle Width="80px" Height="35px" />
                       </asp:TemplateField>                   
                       <asp:TemplateField HeaderText="ChangeStatus">
                            <ItemTemplate>
                                <asp:Button ID="btnChangeStatus" runat="server" Text='<%# Eval("Column4")%>'  OnClick="btnChangeStatus_Click" />
                            </ItemTemplate>
                            <ControlStyle Width="100px" Height="30px"  />
                            <ItemStyle Width="100px" Height="35px" />
                       </asp:TemplateField>  
                    </Columns>
                    <AlternatingRowStyle BackColor="White" />
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
                <asp:Button ID="btnEditILMP" runat="server" Text="Edit ILMP" OnClick="btnEditILMP_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>