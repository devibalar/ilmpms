using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CourseList : System.Web.UI.Page
{
  //  string mainconn = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            populatecourseGrid();
        }
    }

    private void populatecourseGrid()
    {
        try
        {
            DBConnection.conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("spGetListofAllCourses", DBConnection.conn);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@Title", txtSearchTitle.Text));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@CourseCode", txtSearchCourseCode.Text));
            DataSet ds = new DataSet();
            sda.Fill(ds, "Courses");
            gvSearchCourse.DataSource = ds.Tables["Courses"];
            gvSearchCourse.DataBind();
            sda.Dispose();            
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
            ExceptionUtility.LogException(ex, "Error Page");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
    }

    protected void btnSearchCourse_Click(object sender, EventArgs e)
    {
        populatecourseGrid();
    }

    public void gvSearchCourse_SelectedIndexChanged(Object sender, EventArgs e)
    {
       
    }
    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddCourse1.aspx");
    }
    protected void btnEditCourse_Click(object sender, EventArgs e)
    {
        int selectedIndex = gvSearchCourse.SelectedIndex;
        if (selectedIndex != -1)
        {
            string selectedCourseCode = gvSearchCourse.Rows[selectedIndex].Cells[1].Text;
            Response.Redirect("~/EditCourse1.aspx?coursecode=" + selectedCourseCode);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please select a course to edit');", true);
        }
    }
    protected void btnUploadCourse_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UploadCourse.aspx");
    }

    protected void gvSearchCourse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearchCourse.PageIndex = e.NewPageIndex;
        populatecourseGrid();
    }
}