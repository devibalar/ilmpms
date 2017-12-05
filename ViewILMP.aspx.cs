using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewILMP : System.Web.UI.Page
{
    ILMPBO ilmpBO = new ILMPBO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String ilmpIdStr = Request.QueryString["id"];
            int ilmpId = int.Parse(ilmpIdStr);
            hfIlmpId.Value = ilmpIdStr;
          
            ILMPVO ilmpVO = ilmpBO.GetILMPDetailsForId(ilmpId);
            populateILMPDetails(ilmpVO);

            populateWorkshop(ilmpId);
            FillSemesterDropDown();
            FillYearDropDown();
            if (rbtnlViewMode.SelectedItem.Text == "Overall")
            {
                ddSemester.Enabled = false;
                ddYear.Enabled = false;
            }
            else
            {
                ddSemester.Enabled = true;
                ddYear.Enabled = true;
            }
            try
            {
                SqlDataSource sqlDataSource = new SqlDataSource();
                sqlDataSource.ID = "SqlDataSource4";
                this.Page.Controls.Add(sqlDataSource);
                sqlDataSource.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbILMPV1ConnectionString"].ConnectionString;
                sqlDataSource.SelectCommand = "SELECT ic.CourseCode, ic.CourseType, c.Title, c.Credits, c.Level, cp.AllPrerequisites, ic.Semester, ic.Year FROM Ilmp AS i " +
                                            "INNER JOIN IlmpCourse AS ic ON i.IlmpID = ic.IlmpID INNER JOIN Course AS c ON c.CourseCode = ic.CourseCode " +
                                            "LEFT OUTER JOIN CoursePrerequisite AS cp ON cp.CourseCode = c.CourseCode " +
                                            "WHERE (i.IlmpID =" + ilmpId + ") ORDER BY ic.Year,ic.Semester";

                gvIlmp.DataSource = sqlDataSource;
                gvIlmp.DataBind();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "Error Page");
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }    
    private void populateILMPDetails(ILMPVO ilmpVO)
    {
        txtStudentId.Text = ilmpVO.StudentId.ToString();
        txtName.Text = ilmpVO.Name;
        txtActive.Text = ilmpVO.Active;
        txtDescription.Text = ilmpVO.Description;
    }
    private void populateWorkshop(int ilmpid)
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        dt1.Columns.Add(new DataColumn("Column1", typeof(string)));
        //Add a Dummy Data on Initial Load
        dr1 = dt1.NewRow();
        dt1.Rows.Add(dr1);
        //Store the DataTable in ViewState
        ViewState["CurrentTableWorkshop"] = dt1;
        //Bind the DataTable to the Grid
        gvWorkshop.DataSource = dt1;
        gvWorkshop.DataBind();
        List<WorkshopVO> workshopnames = ilmpBO.GetILMPWorkshopForId(ilmpid);
        foreach (WorkshopVO workshop in workshopnames)
        {
            AddNewRowToWorkshopGrid(workshop.WorkshopId);
        }
    }
    private void AddNewRowToWorkshopGrid(int workshopId)
    {
        if (ViewState["CurrentTableWorkshop"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableWorkshop"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTableWorkshop"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    // Update the DataRow with the Selected Items    
                    if (i != dtCurrentTable.Rows.Count - 2)
                    {
                    }
                    else
                    {
                        WorkshopBO workshopBO = new WorkshopBO();
                        WorkshopVO workshopVO = workshopBO.GetWorkshopForId(workshopId);
                        dtCurrentTable.Rows[i]["Column1"] = workshopVO.WorkshopName;
                    }
                }

                //Rebind the Grid with the current data
                gvWorkshop.DataSource = dtCurrentTable;
                gvWorkshop.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataInWorkShopGrid();
    }
    private void SetPreviousDataInWorkShopGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableWorkshop"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableWorkshop"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i < dt.Rows.Count - 1)
                    {
                        ((Label)gvWorkshop.Rows[rowIndex].Cells[0].FindControl("lblWorkshop")).Text = dt.Rows[i]["Column1"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void btnCancelILMPView_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ILMPList.aspx");
    }
    private void FillSemesterDropDown()
    {
        SemesterBO semesterBO = new SemesterBO();
        DataSet ds = semesterBO.getAllSemesters();
        ddSemester.DataSource = ds;
        ddSemester.DataTextField = "SemesterID";
        ddSemester.DataValueField = "SemesterID";
        ddSemester.DataBind();
    }
    private void FillYearDropDown()
    {
        ArrayList arr = GetYear();
        foreach (ListItem item in arr)
        {
            ddYear.Items.Add(item);
        }
        ddYear.Text = DateTime.Now.Year.ToString();
    }
    private ArrayList GetYear()
    {
        ArrayList arr = new ArrayList();
        string currentYearStr = DateTime.Now.Year.ToString();
        int currentYear = Int32.Parse(currentYearStr);
        arr.Add(new ListItem((currentYear - 3).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear - 2).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear - 1).ToString(), (currentYear - 1).ToString()));
        arr.Add(new ListItem(currentYearStr, currentYearStr));
        arr.Add(new ListItem((currentYear + 1).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 2).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 3).ToString(), (currentYear + 1).ToString()));


        return arr;
    }
    protected void rbtnlViewMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnlViewMode.SelectedItem.Text == "Overall")
        {
            ddSemester.Enabled = false;
            ddYear.Enabled = false;
        }
        else
        {
            ddSemester.Enabled = true;
            ddYear.Enabled = true;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            String ilmpIdStr = Request.QueryString["id"];
            int ilmpId = int.Parse(ilmpIdStr);
            if (rbtnlViewMode.SelectedItem.Text == "Overall")
            {
                SqlDataSource sqlDataSource = new SqlDataSource();
                sqlDataSource.ID = "SqlDataSource4";
                this.Page.Controls.Add(sqlDataSource);
                sqlDataSource.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbILMPV1ConnectionString"].ConnectionString;
                sqlDataSource.SelectCommand = "SELECT ic.CourseCode, ic.CourseType, c.Title, c.Credits, c.Level, cp.AllPrerequisites, ic.Semester, ic.Year FROM Ilmp AS i " +
                                            "INNER JOIN IlmpCourse AS ic ON i.IlmpID = ic.IlmpID INNER JOIN Course AS c ON c.CourseCode = ic.CourseCode " +
                                            "LEFT OUTER JOIN CoursePrerequisite AS cp ON cp.CourseCode = c.CourseCode " +
                                            "WHERE (i.IlmpID =" + ilmpId + ") ORDER BY ic.Semester,ic.Year";

                gvIlmp.DataSource = sqlDataSource;
                gvIlmp.DataBind();
            }
            else
            {
                SqlDataSource sqlDataSource = new SqlDataSource();
                sqlDataSource.ID = "SqlDataSource3";
                this.Page.Controls.Add(sqlDataSource);
                sqlDataSource.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbILMPV1ConnectionString"].ConnectionString;
                sqlDataSource.SelectCommand = "SELECT ic.CourseCode, ic.CourseType, c.Title, c.Credits, c.Level, cp.AllPrerequisites, ic.Semester, ic.Year FROM Ilmp AS i " +
                                            "INNER JOIN IlmpCourse AS ic ON i.IlmpID = ic.IlmpID INNER JOIN Course AS c ON c.CourseCode = ic.CourseCode " +
                                            "LEFT OUTER JOIN CoursePrerequisite AS cp ON cp.CourseCode = c.CourseCode " +
                                            "WHERE (i.IlmpID =" + ilmpId + ") AND Semester=" + ddSemester.SelectedItem.Text + " AND Year=" + ddYear.SelectedItem.Text + " ORDER BY ic.Semester,ic.Year";

                gvIlmp.DataSource = sqlDataSource;
                gvIlmp.DataBind();
            }
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
    }
}