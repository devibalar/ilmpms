<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseListReport.aspx.cs" Inherits="CourseListReport" MasterPageFile="~/HomeMasterPage.master" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="courseListReportHead" ContentPlaceHolderID="homeMasterHead" runat="server">  
    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="courseListReportContent" ContentPlaceHolderID="homeMasterContent" runat="server">
    <asp:ScriptManager id="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <fieldset style="width:964px; margin-left:10px;border:2px solid #ccc"> <legend style="font-weight:bold"></legend>
        <table border="0" style="width: 960px">
          <tr>
            <td style="text-align:right" class="auto-style1"><asp:Label ID="lblSemester" runat="server" Text="Semester"></asp:Label></td><td class="auto-style1">
                <asp:DropDownList ID="ddSemester" runat="server">
                </asp:DropDownList>
                </td>
            <td style="text-align:right" class="auto-style1"><asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label></td><td class="auto-style1">
                <asp:DropDownList ID="ddYear" runat="server">
                </asp:DropDownList>
                </td>
           <td class="auto-style1"><asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" /></td>   
           <td><asp:Button ID="btnExportCSV" runat="server" Text="Export CSV" OnClick="btnExportCSV_Click" /></td>
        </tr>
     </table>

     </fieldset>
     <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="900px">
        <LocalReport ReportPath="Report2.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dbILMPV8DataSetTableAdapters.spStudentPerCourseReportTableAdapter"></asp:ObjectDataSource>
</asp:Content>