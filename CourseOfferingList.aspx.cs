using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using System.Configuration;

public partial class DelCourseOffering : System.Web.UI.Page
{
    CourseOfferingBO courseOfferingBO = new CourseOfferingBO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDdlCourseCodeOffering();
            FillYear();
            DisplayRecord();
        }
    }

    public void DisplayRecord()
    {
        try
        {
            DBConnection.conn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE isActive='Yes' ORDER BY Year ASC, Semester ASC, CourseCode ASC", DBConnection.conn);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            gvSearchCourseOffering.DataSourceID = null;
            gvSearchCourseOffering.DataSource = Dt;
            gvSearchCourseOffering.DataBind();
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('"+ex.Message+"');</script>");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
    }

    protected void btnSearchCourseOffering_Click(object sender, EventArgs e)
    {
        try
        {
            //Search by Course Code//
            if (ddlSearchCourseCode.Text != "All" && ddlSearchSemester.Text == "All" && ddlSearchYear.Text == "All")
            {

                SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE isActive='Yes' and CourseCode='" + ddlSearchCourseCode.SelectedItem.Text.Trim() + "' ORDER BY Year ASC, Semester ASC", DBConnection.conn);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                gvSearchCourseOffering.DataSourceID = null;
                gvSearchCourseOffering.DataSource = Dt;
                gvSearchCourseOffering.DataBind();
            }
            //Search by Semester//
            else if (ddlSearchSemester.Text != "All" && ddlSearchCourseCode.Text == "All" && ddlSearchYear.Text == "All")
            {
                SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE isActive='Yes' and Semester=" + Convert.ToInt32(ddlSearchSemester.Text.Trim()) + "ORDER BY Year ASC, CourseCode ASC", DBConnection.conn);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                gvSearchCourseOffering.DataSourceID = null;
                gvSearchCourseOffering.DataSource = Dt;
                gvSearchCourseOffering.DataBind();
            }
            //Search by Year//
            else if (ddlSearchYear.Text != "All" && ddlSearchCourseCode.Text == "All" && ddlSearchSemester.Text == "All")
            {
                SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE isActive='Yes' and Year=" + Convert.ToInt32(ddlSearchYear.Text.Trim()) + "ORDER BY Semester ASC, CourseCode ASC", DBConnection.conn);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                gvSearchCourseOffering.DataSourceID = null;
                gvSearchCourseOffering.DataSource = Dt;
                gvSearchCourseOffering.DataBind();
            }
            //Search by CourseCode and Year//
            else if (ddlSearchCourseCode.Text != "All" && ddlSearchYear.Text != "All" && ddlSearchSemester.Text == "All")
            {
                DBConnection.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.CourseOffering WHERE isActive='Yes' and CourseCode='" + ddlSearchCourseCode.SelectedItem.Text.Trim() +
                    "' AND Year=" + Convert.ToInt32(ddlSearchYear.Text.Trim()), DBConnection.conn);
                Int32 count = (Int32)cmd.ExecuteScalar();
                DBConnection.conn.Close();
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('No matching course offering found');", true);
                }
                else
                {
                    SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE isActive='Yes' and CourseCode='" + ddlSearchCourseCode.SelectedItem.Text.Trim() +
                        "' AND Year=" + Convert.ToInt32(ddlSearchYear.Text.Trim()) + " ORDER BY Semester ASC, CourseCode ASC", DBConnection.conn);
                    DataTable Dt = new DataTable();
                    Adp.Fill(Dt);
                    gvSearchCourseOffering.DataSourceID = null;
                    gvSearchCourseOffering.DataSource = Dt;
                    gvSearchCourseOffering.DataBind();
                }
            }
            //Search by CourseCode and Semester//
            else if (ddlSearchCourseCode.Text != "All" && ddlSearchYear.Text == "All" && ddlSearchSemester.Text != "All")
            {
                DBConnection.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.CourseOffering WHERE isActive='Yes' and CourseCode='" + ddlSearchCourseCode.SelectedItem.Text.Trim() +
                    "' AND Semester=" + Convert.ToInt32(ddlSearchSemester.Text.Trim()), DBConnection.conn);
                Int32 count = (Int32)cmd.ExecuteScalar();
                DBConnection.conn.Close();
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course does not Offering');", true);
                }
                else
                {
                    SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE isActive='Yes' and CourseCode='" + ddlSearchCourseCode.SelectedItem.Text.Trim() + "' AND Semester=" + Convert.ToInt32(ddlSearchSemester.Text.Trim()) + " ORDER BY Year ASC, CourseCode ASC", DBConnection.conn);
                    DataTable Dt = new DataTable();
                    Adp.Fill(Dt);
                    gvSearchCourseOffering.DataSourceID = null;
                    gvSearchCourseOffering.DataSource = Dt;
                    gvSearchCourseOffering.DataBind();
                }
            }
            //Search by Semester and Year//
            else if (ddlSearchCourseCode.Text == "All" && ddlSearchYear.Text != "All" && ddlSearchSemester.Text != "All")
            {
                DBConnection.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.CourseOffering WHERE isActive='Yes' and Semester='" + Convert.ToInt32(ddlSearchSemester.Text.Trim()) +
                    "' AND Year=" + Convert.ToInt32(ddlSearchYear.Text.Trim()), DBConnection.conn);
                Int32 count = (Int32)cmd.ExecuteScalar();
                DBConnection.conn.Close();
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course does not Offering');", true);
                }
                else
                {
                    SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE  (Year='" + Convert.ToInt32(ddlSearchYear.Text.Trim()) + "' AND Semester=" + Convert.ToInt32(ddlSearchSemester.Text.Trim()) + " and isActive='Yes' ) ORDER BY CourseCode ASC ", DBConnection.conn);
                    DataTable Dt = new DataTable();
                    Adp.Fill(Dt);
                    gvSearchCourseOffering.DataSourceID = null;
                    gvSearchCourseOffering.DataSource = Dt;
                    gvSearchCourseOffering.DataBind();
                }
            }
            //Search by CourseCode, Semester and Year//
            else if (ddlSearchCourseCode.Text != "All" && ddlSearchYear.Text != "All" && ddlSearchSemester.Text != "All")
            {
                DBConnection.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.CourseOffering WHERE isActive='Yes' and CourseCode='" + ddlSearchCourseCode.SelectedItem.Text.Trim() +
                    "' AND Semester=" + Convert.ToInt32(ddlSearchSemester.Text.Trim()) +
                    " AND Year=" + Convert.ToInt32(ddlSearchYear.Text.Trim()), DBConnection.conn);
                Int32 count = (Int32)cmd.ExecuteScalar();
                DBConnection.conn.Close();
                if (count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Course does not Offering');", true);
                }
                else
                {
                    SqlDataAdapter Adp = new SqlDataAdapter("SELECT * FROM CourseOffering WHERE isActive='Yes' and CourseCode='" + ddlSearchCourseCode.SelectedItem.Text.Trim() +
                        "' AND Semester=" + Convert.ToInt32(ddlSearchSemester.Text.Trim()) +
                        "AND Year=" + Convert.ToInt32(ddlSearchYear.Text.Trim()) + " ORDER BY CourseCode ASC", DBConnection.conn);
                    DataTable Dt = new DataTable();
                    Adp.Fill(Dt);
                    gvSearchCourseOffering.DataSourceID = null;
                    gvSearchCourseOffering.DataSource = Dt;
                    gvSearchCourseOffering.DataBind();
                }
            }
            else if (ddlSearchYear.Text == "All" && ddlSearchCourseCode.Text == "All" && ddlSearchSemester.Text == "All")
            {
                DisplayRecord();
            }
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('Error in getting course offering');</script>");
        }
    }

    private void FillDdlCourseCodeOffering()
    {
        ddlSearchCourseCode.Items.Clear();
        ddlSearchCourseCode.Items.Add(new ListItem("All", "All"));
        DataTable dtCourseCode = courseOfferingBO.FillCourseCodeOffering();
        foreach (DataRow dr in dtCourseCode.Rows)
        {
            ddlSearchCourseCode.Items.Add(dr["CourseCode"].ToString());
        }
    }

    private void FillYear()
    {
        ddlSearchYear.Items.Clear();
        ddlSearchYear.Items.Add(new ListItem("All", "All"));
        DataTable dtCourseCode = courseOfferingBO.FillYearCourseOffering();
        foreach (DataRow dr in dtCourseCode.Rows)
        {
            ddlSearchYear.Items.Add(dr["Year"].ToString());
        }
    }

    protected void btnDeleteCourseOffering_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkRemove = (LinkButton)sender;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM CourseOffering WHERE CourseCode=@CourseCode";
            cmd.Parameters.Add("@CourseCode", SqlDbType.VarChar).Value = lnkRemove.CommandArgument;
            gvSearchCourseOffering.DataBind();
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        DisplayRecord();
    }

    protected void gvSearchCourseOffering_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gvSearchCourseOffering.SelectedRow;
        Session["CourseCode"] = row.Cells[0].Text;
        Session["Semester"] = row.Cells[1].Text;
        Session["Year"] = row.Cells[2].Text;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Session["CourseCode"].ToString() == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please select course offering');", true);
        }
        else
        {
            Response.Redirect("~/EditCourseOffering.aspx");
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddCourseOffering.aspx");
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UploadCourseOffering.aspx");
    }
    /*  protected void btnDelCourseOffering_Click(object sender, EventArgs e)
      {
          int selectedIndex = gvSearchCourseOffering.SelectedIndex;
          if (selectedIndex >= 0)
          {
              string courseCode = gvSearchCourseOffering.Rows[selectedIndex].Cells[0].Text;
              int semester = int.Parse(gvSearchCourseOffering.Rows[selectedIndex].Cells[1].Text);
              int year = int.Parse(gvSearchCourseOffering.Rows[selectedIndex].Cells[2].Text); 
              CourseOfferingVO courseOfferingVO = new CourseOfferingVO(courseCode,semester,year);

              if (courseOfferingBO.DeleteCourseOffering(courseOfferingVO))
              {
                  Response.Write("<script>alert('Course Offering deleted Successfully');</script>");
              }
              else
              {
                  Response.Write("<script>alert('Course Offering seletion Fail');</script>");
              }
              Response.Redirect("~/CourseOfferingList.aspx");
          }
          else
          {
              Response.Write("<script>alert('Please select a course offering to delete');</script>");
          }
      }*/

    protected void gvSearchCourseOffering_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearchCourseOffering.PageIndex = e.NewPageIndex;
        DisplayRecord();
        gvSearchCourseOffering.DataBind();
    }
}