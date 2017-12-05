<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ILMPList.aspx.cs" Inherits="ILMPList" MasterPageFile="~/HomeMasterPage.master" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
       <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <asp:UpdatePanel id="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>  
        <h3> ILMP List </h3>
         <fieldset style="width:800px;margin:0 auto;border:2px solid #ccc"> <legend></legend>
    <asp:GridView ID="gvILMPList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="IlmpID" DataSourceID="SqlDataSource1" Width="533px" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" ControlStyle-Height="35" >
            <ControlStyle Height="35px"></ControlStyle>
            </asp:CommandField>
            <asp:BoundField DataField="IlmpID" HeaderText="IlmpID" InsertVisible="False" ReadOnly="True" SortExpression="IlmpID" ControlStyle-Height="35" >
            <ControlStyle Height="35px"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Active" HeaderText="Active" InsertVisible="False" ReadOnly="True" SortExpression="Active" ControlStyle-Height="35" >
            <ControlStyle Height="35px"></ControlStyle>
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#333333" HorizontalAlign="Center" BackColor="#FFCC66" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"/>
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <table style="width:600px"><tr><td style="text-align:center"><asp:Button ID="btnView" runat="server" Text="View ILMP" OnClick="btnView_Click" /></td></tr></table>    
    </fieldset>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:dbILMPV1ConnectionString %>" DeleteCommand="DELETE FROM [Ilmp] WHERE [IlmpID] = @original_IlmpID" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [IlmpID],[Active] FROM [Ilmp] WHERE ([StudentID] = @StudentID) ORDER BY Active desc">
        <DeleteParameters>
            <asp:Parameter Name="original_IlmpID" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="StudentID" SessionField="StudentID" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>