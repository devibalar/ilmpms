<%@ Page Title="" Language="C#" MasterPageFile="~/HomeMasterPage.master" AutoEventWireup="true" CodeFile="UploadStudent.aspx.cs" Inherits="UploadStudent" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
    <asp:FileUpload ID="fuBulkCourseUpload" runat="server" />
    <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</asp:Content>


