using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Query.Dynamic;

public partial class AddCourseOffering : System.Web.UI.Page
{
    CourseOfferingBO courseOfferingBO = new CourseOfferingBO();
    CourseOfferingDaoImpl courseOfferingDaoImpl = new CourseOfferingDaoImpl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillDdlCourseCode();
            txtYearCourseOffering.Text = "" + DateTime.Now.Year;
        }
    }
    protected void btnAddCourseOffering_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtYearCourseOffering.Text == "")
            {
                Response.Write("<script>alert('Please enter year');</script>");
                return;
            }
            int year = 0;
            string courseCode = ddlCourseCodeOffering.SelectedItem.Text;
            int semester = Convert.ToInt32(ddlSemesterCourseOffering.SelectedItem.Text);
            try
            {
                year = int.Parse(txtYearCourseOffering.Text);
                if (year < DateTime.Now.Year)
                {
                    Response.Write("<script>alert('Year should be current year or greater than that');</script>");
                    return;
                }
            }
            catch (ParseException)
            {
                Response.Write("<script>alert('Please enter valid year');</script>");
                return;
            }
            DateTime createdDTM = DateTime.Now;

            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.CourseOffering WHERE CourseCode='" + courseCode + "' AND Semester=" + semester + " AND Year=" + year, DBConnection.conn);
            Int32 count = (Int32)cmd.ExecuteScalar();
            DBConnection.conn.Close();
            if (count > 0)
            {
                Response.Write("<script>alert('Course offering already exists);</script>");
            }
            else
            {
                CourseOfferingVO courseOfferingVO = new CourseOfferingVO(courseCode, semester, year, createdDTM);
                if (courseOfferingBO.AddCourseOffering(courseOfferingVO))
                {
                    Response.Write("<script>alert('Course Offering added successfully');</script>");
                    FillDdlCourseCode();
                }
                else
                {
                    Response.Write("<script>alert('Course Offering addition fail');</script>");
                }
            }
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('Error in adding course offering');</script>");
        }
    }

    private void FillDdlCourseCode()
    {
        ddlCourseCodeOffering.Items.Clear();
        DataTable dtCourseCode = courseOfferingDaoImpl.FillCourseCode();
        foreach (DataRow dr in dtCourseCode.Rows)
        {
            ddlCourseCodeOffering.Items.Add(dr["CourseCode"].ToString());
        }
    }
    protected void btnCancelCourseOffering_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CourseOfferingList.aspx");
    }
}