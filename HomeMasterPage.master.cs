using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomeMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {     
        if (Session["Role"] != null && Session["Role"].ToString()==ApplicationConstants.StudentRole)
        {
            this.liCourse.Visible = false;
            this.liCourseOffering.Visible = false;
            this.liReports.Visible = false;
            this.liStudent.Visible = false;
            this.liSemesterResult.Visible = false;
            this.liILMPList.Visible = true;
            this.liILMPTemplate.Visible = false;
            this.liILMPCreation.Visible = false;
            this.liILMPListStaff.Visible = false;
            this.hdnStudentId.Value = Session["StudentID"].ToString();
        }
        else if (Session["Role"] != null && Session["Role"].ToString() == ApplicationConstants.StaffRole)
        {
            this.liCourse.Visible = true;
            this.liCourseOffering.Visible = true;
            this.liReports.Visible = true;
            this.liStudent.Visible = true;
            this.liSemesterResult.Visible = true;
            this.liILMPList.Visible = false;
            this.liILMPTemplate.Visible = true;
            this.liILMPCreation.Visible = true;
            this.liILMPListStaff.Visible = true;
        }
        else
        {
           // Server.Transfer("~/ErrorPage.aspx?msg=403&handler=customErrors%20section%20-%20Web.config");
        }
    }

    protected void aLogout_ServerClick(object sender, EventArgs e)
    {      
        string redirectPage = "~/Login.aspx";
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect(redirectPage);
    }

    protected void liIlmp_ServerClick(object sender, EventArgs e)
    {
        if (Session["Role"] != null && Session["Role"].ToString() == ApplicationConstants.StudentRole)
        {
            string redirectPage = "~/ILMPList.aspx";
            Response.Redirect(redirectPage);
        }
    }
}
