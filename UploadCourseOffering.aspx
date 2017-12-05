<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadCourseOffering.aspx.cs" Inherits="UploadCourseOffering" MasterPageFile="~/HomeMasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <link href="/ILMPApplication/styles/courseoffering.css" rel="stylesheet" type="text/css" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="homeMasterContent" Runat="Server">
     
    <fieldset style="border:2px solid #ccc"> <legend></legend>
        <p style="margin:0 auto;color:red"><b>Course Offering</b></p>
        <div style="text-align:left"> <asp:Label ID="lblUserMessage" runat="server" Text="Note:CourseOfferingErrors.csv will be downloaded when there are conflicts with existing Ilmps"></asp:Label></div>
            <br /><br />
             <asp:FileUpload ID="fsBulkCourseOfferingUpload" runat="server" /> 
              
        <asp:Button ID="BtnUploadFile" class="upload" runat="server" Text="Upload File" OnClick="BtnUpload_Click"/>
 
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </fieldset>
        
        <br />
        <br />
    <div class="searchResult">
        </div>
        <br />
</asp:Content>