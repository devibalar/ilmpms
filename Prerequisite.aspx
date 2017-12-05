<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prerequisite.aspx.cs" Inherits="Prerequisite" %>
<%@ Register Assembly="ASP.Web.UI.PopupControl" 

    Namespace="ASP.Web.UI.PopupControl"

    TagPrefix="ASPP" %> 
     <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>  
            <h3>Prerequisite</h3>
            <asp:GridView ID="gvPrerequisite" runat="server"  style="margin-left:2px"  Width="976px" PageSize="50" CellPadding="0" ForeColor="#333333" GridLines="None" AutoGenerateSelectButton="true">       
                <Columns>  
                <asp:TemplateField HeaderText="PrerequisiteCode">
                <ItemTemplate>
                    <asp:DropDownList ID="ddPrerequisiteCode" runat="server" AppendDataBoundItems="false" >                               
                        <asp:ListItem Value="-1" Selected="True">Select</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ControlStyle Height="35px" Width="80px" />
                <ItemStyle Height="35px" Width="80px"  />
                </asp:TemplateField> 
                </Columns>
            </asp:GridView>
           <fieldset><legend>
               <asp:Button ID="btnAddPrerequisite" runat="server" Text="Add Prerequisite" style="width:150px;margin-left:5px;margin-right:5px" OnClick="btnAddPrerequisite_Click" />
               <asp:Button ID="btnDeletePrerequisite" runat="server" Text="Delete Prerequisite" style="width:160px;margin-left:5px;margin-right:5px" OnClick="btnDeletePrerequisite_Click"/>               
           </legend></fieldset>
            <fieldset><legend>
               <asp:Button ID="btnSavePrerequisite" runat="server" Text="Apply" style="width:150px;margin-left:5px;margin-right:5px" OnClick="btnSavePrerequisite_Click" />
               <asp:Button ID="btnBack" runat="server" Text="Back" style="width:160px;margin-left:5px;margin-right:5px" OnClick="btnBack_Click"/>               
           </legend></fieldset>
       </ContentTemplate>
       <Triggers>
           <asp:PostBackTrigger ControlID="btnSaveTemplate" />
       </Triggers>
       </asp:UpdatePanel>    

