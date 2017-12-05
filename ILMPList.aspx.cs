using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ILMPList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string studentId = Session["StudentId"].ToString();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int selectedIndex = gvILMPList.SelectedIndex;
        if(selectedIndex!=-1)
        {
            string selectedIlmpid = gvILMPList.Rows[selectedIndex].Cells[1].Text;
            Response.Redirect("~/ViewILMP.aspx?id=" + selectedIlmpid);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please select a ILMP ');", true);
        }
    }
}