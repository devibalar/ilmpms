<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" MasterPageFile="~/ILMPMasterPage.master" %>

<asp:Content ID="ErrorPageHead" ContentPlaceHolderID="masterHead" runat="server">

</asp:Content>
 <asp:Content ID="ErrorPageContent" ContentPlaceHolderID="masterContent" runat="server">
  <h2 id="ErrorType" runat="server">Error</h2>
    <br /><br />
    <div style="margin:0 auto"><asp:Label ID="FriendlyErrorMsg" runat="server" Text="Label" Font-Size="Large" style="color: red"></asp:Label>

    <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="false">
        <p>&nbsp;</p>        

        <h4>Detailed Error Message:</h4>
        <p>
            <asp:Label ID="InnerMessage" runat="server" Font-Size="Small" /><br />
        </p>
        <p>
            <asp:Label ID="InnerTrace" runat="server"  />
        </p>
    </asp:Panel>
        </div>
 </asp:Content>