<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadResult.aspx.cs" Inherits="UploadResult" MasterPageFile="~/HomeMasterPage.master" %>

<asp:Content ID="uploadResultHead" ContentPlaceHolderID="homeMasterHead" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 205px;
        }
        .auto-style2 {
            width: 205px;
            height: 76px;
        }
        .auto-style3 {
            height: 76px;
        }
    </style>
</asp:Content>
<asp:Content ID="uploadResultContent" ContentPlaceHolderID="homeMasterContent" Runat="Server">
    <h3>Upload Results</h3>
    <fieldset style="width:700px;margin:0 auto;border:2px solid #ccc"> <legend></legend>
        <table style="margin:0 auto; width: 599px;">
            <tr>
                <td style="text-align:right" class="auto-style2"><asp:Label ID="lblSemester" runat="server" Text="Semester"></asp:Label></td>
                <td class="auto-style3"><asp:DropDownList ID="ddSemester" runat="server"></asp:DropDownList></td>
                <td style="text-align:right" class="auto-style3"><asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label></td>
                <td class="auto-style3"><asp:DropDownList ID="ddYear" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="auto-style1" style="padding-left:20px"><asp:FileUpload ID="fuResultUpload" runat="server" Width="192px" /> </td>                 
                <td><asp:Button ID="btnUpload" CssClass="upload" runat="server" Text="Upload File" Width="100px" OnClick="btnUpload_Click"/></td>
            </tr>
        </table>
    </fieldset>
</asp:Content>