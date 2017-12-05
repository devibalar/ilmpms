<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadCourse.aspx.cs" Inherits="UploadCourse" MasterPageFile="~/HomeMasterPage.master"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
     <div class="container"> 
        <fieldset id="fsBulkCourseUpload">
            <legend><b>Bulk Upload</b></legend>
             <asp:FileUpload ID="fuBulkCourseUpload" runat="server" /> 
              &nbsp
        <asp:Button ID="btnCourse" CssClass="upload" runat="server" Text="Upload File" OnClick="BtnUpload_Click"/>
           
        </fieldset>
          </div>  
        <br />
     </asp:Content> 
