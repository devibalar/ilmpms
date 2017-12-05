using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Query.Dynamic;

public partial class EditStudent : System.Web.UI.Page
{
    StudentBO studentBO = new StudentBO();
    StudentDaoImpl studentDaoImpl = new StudentDaoImpl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetAllStudentRecords();
        }
    }

    protected void btnSearchStudent_Click(object sender, EventArgs e)
    {
        try
        {
            int studentId = 0;
            if (txtSearchStudentID.Text.Trim()!="")
              {
                   try
                        {
                            studentId = int.Parse(txtSearchStudentID.Text.Trim());
                        }
                        catch (ParseException)
                        {
                            Response.Write("<script>alert('Please enter valid studentid');</script>");
                            return;
                        }
              }
            if (txtSearchStudentID.Text != "" && ddlSearchProgramme.Text == "All")
            {
                DBConnection.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Student WHERE StudentID LIKE '%" + Convert.ToInt32(txtSearchStudentID.Text.Trim()) + "%' ", DBConnection.conn);
                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count == 0)
                {
                    Response.Write("<script>alert('No  matching student details found ');</script>");
                }
                else
                {
                   
                    SqlDataAdapter Adp = new SqlDataAdapter("SELECT s.StudentID, s.FirstName, s.LastName, s.Status, sm.ProgrammeID, sm.MajorID, i.EmailId, i.Active FROM Student s INNER JOIN StudentMajor sm ON s.StudentID=sm.StudentId LEFT JOIN IlmpUser i ON s.StudentID=i.StudentId WHERE s.StudentID LIKE  '%" + studentId + "%' AND s.StudentID !=0 ORDER BY i.Active DESC, StudentID ASC", DBConnection.conn);
                    DataTable Dt = new DataTable();
                    Adp.Fill(Dt);
                    gvSearchStudent.DataSourceID = null;
                    gvSearchStudent.DataSource = Dt;
                    gvSearchStudent.DataBind();
                }
            }
            else if (txtSearchStudentID.Text == "" && ddlSearchProgramme.Text == "All")
            {
                GetAllStudentRecords();
            }
            else if (txtSearchStudentID.Text == "" && ddlSearchProgramme.Text == "GDIT")
            {
                DBConnection.conn.Open();
                SqlDataAdapter Adp = new SqlDataAdapter("SELECT s.StudentID, s.FirstName, s.LastName, s.Status, sm.ProgrammeID, sm.MajorID, i.EmailId, i.Active FROM Student s INNER JOIN StudentMajor sm ON s.StudentID=sm.StudentId LEFT JOIN IlmpUser i ON s.StudentID=i.StudentId WHERE sm.ProgrammeID='GDIT' AND s.StudentID !=0 ORDER BY i.Active DESC, StudentID ASC", DBConnection.conn);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                gvSearchStudent.DataSourceID = null;
                gvSearchStudent.DataSource = Dt;
                gvSearchStudent.DataBind();
            }
            else if (txtSearchStudentID.Text == "" && ddlSearchProgramme.Text == "BIT")
            {
                DBConnection.conn.Open();
                SqlDataAdapter Adp = new SqlDataAdapter("SELECT s.StudentID, s.FirstName, s.LastName, s.Status, sm.ProgrammeID, sm.MajorID, i.EmailId, i.Active FROM Student s INNER JOIN StudentMajor sm ON s.StudentID=sm.StudentId LEFT JOIN IlmpUser i ON s.StudentID=i.StudentId WHERE sm.ProgrammeID='BIT' AND s.StudentID !=0 ORDER BY i.Active DESC, StudentID ASC", DBConnection.conn);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                gvSearchStudent.DataSourceID = null;
                gvSearchStudent.DataSource = Dt;
                gvSearchStudent.DataBind();
            }
            else if (txtSearchStudentID.Text != "" && ddlSearchProgramme.SelectedValue != "All")
            {
                DBConnection.conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Student s INNER JOIN StudentMajor sm ON s.StudentID=sm.StudentId WHERE s.StudentID=" + Convert.ToInt32(txtSearchStudentID.Text.Trim()) + " AND sm.ProgrammeID='" + ddlSearchProgramme.SelectedValue + "'", DBConnection.conn);
                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count > 0)
                {
                    SqlDataAdapter Adp = new SqlDataAdapter("SELECT s.StudentID, s.FirstName, s.LastName, s.Status, sm.ProgrammeID, sm.MajorID, i.EmailId, i.Active FROM Student s INNER JOIN StudentMajor sm ON s.StudentID=sm.StudentId LEFT JOIN IlmpUser i ON s.StudentID=i.StudentId WHERE s.StudentID LIKE " + int.Parse(txtSearchStudentID.Text.Trim()) + " AND sm.ProgrammeID='" + ddlSearchProgramme.SelectedValue + "' ORDER BY i.Active DESC, StudentID ASC", DBConnection.conn);
                    DataTable Dt = new DataTable();
                    Adp.Fill(Dt);
                    gvSearchStudent.DataSourceID = null;
                    gvSearchStudent.DataSource = Dt;
                    gvSearchStudent.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('No matching student details found, please check StudentID and ProgrammeID');</script>");
                    DataTable dt = new DataTable();
                    gvSearchStudent.DataSource = dt;
                    gvSearchStudent.DataBind();
                }
            }
        }
        catch (SqlException ex)
        {
            Response.Write("<script>alert('Error in searching student details');</script>");
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

    public void gvSearchStudent_SelectedIndexChanged(Object sender, EventArgs e)
    {
        GridViewRow row = gvSearchStudent.SelectedRow;
        Session["StudentID"] = row.Cells[0].Text;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddStudent.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (Session["StudentID"].ToString() == "")
        {
            Response.Write("<script>alert('Please select Student');</script>");
        }
        else
        {
            Response.Redirect("~/EditStudent.aspx");
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UploadStudent.aspx");
    }

    // Get all students, their programme and major details,email, active status from the database 
    private void GetAllStudentRecords()
    {
        try
        {
            DBConnection.conn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter("Select	 distinct" +
                                        " st.StudentID" +
                                        " ,st.FirstName" +
                                        " ,st.LastName" +
                                        " ,sm.ProgrammeID" +
                                        " ,STUFF(( Select ',' + MajorID" +
                                                        " From dbo.StudentMajor sm1 " +
                                        " WHERE sm.StudentID = sm1.StudentID" +
                                        " and sm1.Active = 'Yes'" +
                                        " FOR XML PATH('')), 1, 1, '') AS MajorID" +
                                        " ,iu.EmailId" +
                                        " ,st.Status" +
                                        " ,iu.Active" +
                                " From dbo.Student st" +
                                " left outer join dbo.IlmpUser iu" +
                                " on st.StudentID = iu.StudentId" +
                                " left outer join dbo.StudentMajor sm" +
                                " on st.StudentID = sm.StudentID" +
                                " and sm.Active = 'Yes'" +
                                " where st.StudentID>0 ORDER BY iu.Active DESC, StudentID DESC", DBConnection.conn);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            gvSearchStudent.DataSourceID = null;
            gvSearchStudent.DataSource = Dt;
            gvSearchStudent.DataBind();
            Adp.Dispose();

        }
        catch (SqlException ex)
        {
            Response.Write("<script>alert('Error in searching student details');</script>");
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

    protected void gvSearchStudent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSearchStudent.PageIndex = e.NewPageIndex;
        GetAllStudentRecords();
        gvSearchStudent.DataBind();
    }
}