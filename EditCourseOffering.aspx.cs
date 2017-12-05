using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditCourseOffering : System.Web.UI.Page
{
    CourseOfferingBO courseOfferingBO = new CourseOfferingBO();
    int oldSemester = 0;
    int oldYear = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getValueCourseOffering();
        }
    }

    public void getValueCourseOffering()
    {
        txtEditCourseCodeOffering.Text = Session["CourseCode"].ToString();
        ddlEditSemesterCourseOffering.Text = Session["Semester"].ToString();
        txtEditYearCourseOffering.Text = Session["Year"].ToString();
        hfSemester.Value = Session["Semester"].ToString();
        hfYear.Value = Session["Year"].ToString();

        //DBConnection.conn.Open();
        //string query = "SELECT Semester, Year FROM CourseOffering WHERE CourseCode='" + txtEditCourseCodeOffering.Text + "'";
        //using (SqlCommand cmd = new SqlCommand(query, DBConnection.conn))
        //{
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        ddlEditSemesterCourseOffering.Text = dr["Semester"].ToString();
        //        txtEditYearCourseOffering.Text = dr["Year"].ToString();
        //        hfSemester.Value = dr["Semester"].ToString();
        //        hfYear.Value = dr["Year"].ToString();
        //    }
        //    dr.Close();
        //}
        //DBConnection.conn.Close();

        DBConnection.conn.Open();
        string query1 = "SELECT Title FROM Course WHERE CourseCode='" + txtEditCourseCodeOffering.Text + "'";
        SqlCommand command = new SqlCommand(query1, DBConnection.conn);
        txtTitle.Text = command.ExecuteScalar().ToString();
        DBConnection.conn.Close();        
    }

    protected void btnCancelCourseOffering_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CourseOfferingList.aspx");
    }

    protected void btnUpdateCourseOffering_Click(object sender, EventArgs e)
    {
        try
        {
            string courseCode = txtEditCourseCodeOffering.Text;
            int semester = Convert.ToInt32(ddlEditSemesterCourseOffering.SelectedValue);
            int year = Convert.ToInt32(txtEditYearCourseOffering.Text);
            DateTime createdDTM = DateTime.Now;

             DBConnection.conn.Open();
             SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.CourseOffering WHERE CourseCode='" + courseCode + "' AND Semester=" + semester + " AND Year=" + year, DBConnection.conn);
             Int32 count = (Int32)cmd.ExecuteScalar();
             DBConnection.conn.Close();
                if (count > 0)
                {
                    Response.Write("<script>alert('Course Offering exists already ');</script>");
                }
                else
                {

                    CourseOfferingVO courseOfferingVO = new CourseOfferingVO(courseCode, semester, year, createdDTM);
                    int oldSem = int.Parse(hfSemester.Value);
                    int oldYear = int.Parse(hfYear.Value);
                    if (courseOfferingBO.UpdateCourseOffering(courseOfferingVO, oldSem, oldYear))
                    {
                        Response.Write("<script>alert('Course updated successfully');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Course updation failed');</script>");
                    }
                }
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('"+ex.Message+"');</script>");
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
        }
    }

    protected void btnDelCourseOffering_Click(object sender, EventArgs e)
    {
        if (txtEditCourseCodeOffering.Text == "")
        {
            Response.Write("<script>alert('Please select a CourseOffering');</script>");
        }
        else
        {
            string courseCode = txtEditCourseCodeOffering.Text;
            CourseOfferingVO courseOfferingVO = new CourseOfferingVO(courseCode);
            courseOfferingVO.Semester = int.Parse(ddlEditSemesterCourseOffering.SelectedItem.Text);
            courseOfferingVO.Year = int.Parse(txtEditYearCourseOffering.Text);
            if (courseOfferingBO.DeleteCourseOffering(courseOfferingVO))
            {
                Response.Write("<script>alert('Course Offering deleted successfully');</script>");
            }
            else
            {
                Response.Write("<script>alert('Course Offering deletion failed');</script>");
            }
            Response.Redirect("~/CourseOfferingList.aspx");
        }
    }
}