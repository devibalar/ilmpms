using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditILMP : System.Web.UI.Page
{
    ILMPBO ilmpBO = new ILMPBO();
    CourseBO courseBO = new CourseBO();
    ILMPTemplateBO ilmpTemplateBO = new ILMPTemplateBO();
    ProgrammeWorkshopBO programmeWorkshopBO = new ProgrammeWorkshopBO();
    CourseOfferingBO courseOfferingBO = new CourseOfferingBO();    
    int ilmpId = 0;
    StudentMajorVO studentMajorVO = new StudentMajorVO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String ilmpIdStr = Request.QueryString["id"];
            ilmpId = int.Parse(ilmpIdStr);
            hfIlmpId.Value = ilmpIdStr;
            SetInitialRowInGrids();
        }
    }
    private void populateILMPDetails(ILMPVO ilmpVO)
    {
        txtStudentId.Text = ilmpVO.StudentId.ToString();
        txtName.Text = ilmpVO.Name;
        ddActive.Text = ilmpVO.Active;
        txtDescription.Text = ilmpVO.Description;
    }
    private void SetInitialRowInGrids()
    {
            ILMPVO ilmpVO = new ILMPVO();
            ilmpVO = ilmpBO.GetILMPCoursesWorkshopForId(ilmpId);
            populateILMPDetails(ilmpVO);
            StudentMajorBO studentMajorBO = new StudentMajorBO();
            studentMajorVO = studentMajorBO.GetStudentMajor(ilmpVO.StudentId);
          //  ILMPTemplateVO ilmpTemplateVO = ilmpTemplateBO.GetTemplateForId(ilmpVO.TemplateId);
            if (ilmpVO.IlmpCourses.Count > 0)
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                //Define the Columns
                dt.Columns.Add(new DataColumn("Column1", typeof(string)));
                dt.Columns.Add(new DataColumn("Column2", typeof(string)));
                dt.Columns.Add(new DataColumn("Column3", typeof(string)));
                dt.Columns.Add(new DataColumn("Column4", typeof(string)));
                dt.Columns.Add(new DataColumn("Column5", typeof(string)));
                dt.Columns.Add(new DataColumn("Column6", typeof(string)));
                dt.Columns.Add(new DataColumn("Column7", typeof(string)));
                dt.Columns.Add(new DataColumn("Column8", typeof(string)));
                dt.Columns.Add(new DataColumn("Column9", typeof(string)));
                //Add a Dummy Data on Initial Load
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;
                //Bind the DataTable to the Grid
                gvIlmpCourse.DataSource = dt;
                gvIlmpCourse.DataBind();
                gvIlmpCourse.Rows[0].Cells[0].Width = 40;
             
                // AddNewRowToGrid();

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

                foreach (ILMPCourseVO ilmpCourseVO in ilmpVO.IlmpCourses)
                {
                    AddNewRowToCourseGrid(ilmpCourseVO);
                }
                List<WorkshopVO> workshops = ilmpBO.GetILMPWorkshopForId(ilmpId);
                foreach (WorkshopVO workshopVO in workshops)
                {
                    AddNewRowToWorkshopGrid(workshopVO.WorkshopId);
                }
               /* foreach (TemplateCourseVO templateCourseVO in ilmpTemplateVO.TemplateCourses)
                {
                    if (templateCourseVO.WorkshopId == 0 && templateCourseVO.CourseCode != "")
                    {
                        AddNewRowToCourseGrid(templateCourseVO);
                    }
                    else
                    {
                        AddNewRowToWorkshopGrid(templateCourseVO.WorkshopId);
                    }
                }*/
                if ((gvIlmpCourse.Rows[gvIlmpCourse.Rows.Count - 1].Cells[0].FindControl("lblCourseCode") as Label).Text == "")
                {
                    gvIlmpCourse.Rows[gvIlmpCourse.Rows.Count - 1].Visible = false;
                }
                if ((gvWorkshop.Rows[gvWorkshop.Rows.Count - 1].Cells[0].FindControl("lblWorkshop") as Label).Text == "")
                {
                    gvWorkshop.Rows[gvWorkshop.Rows.Count - 1].Visible = false;
                }
            }
            List<ILMPCourseGridVO> ilmpCourses = ilmpBO.GetILMPCoursesForId(ilmpId);
            for (int i = 0; i < gvIlmpCourse.Rows.Count - 1; i++)
            {
                ((DropDownList)gvIlmpCourse.Rows[i].Cells[2].FindControl("ddSemester")).Text = ilmpCourses[i].Semester.ToString();
                ((DropDownList)gvIlmpCourse.Rows[i].Cells[3].FindControl("ddYear")).Text = ilmpCourses[i].Year.ToString();
                ((DropDownList)gvIlmpCourse.Rows[i].Cells[1].FindControl("ddCourseType")).Text = ilmpCourses[i].CourseType.ToString();
                ((DropDownList)gvIlmpCourse.Rows[i].Cells[1].FindControl("ddResult")).Text = ilmpCourses[i].Result.ToString();           
            }
    }
   
    private void FillWorkshopDropDown(DropDownList ddl)
    {
        List<WorkshopVO> workshops = ilmpBO.GetILMPWorkshopForId(ilmpId);
        ddl.DataSource = workshops;
        ddl.DataTextField = "Workshopname";
        ddl.DataValueField = "Workshopid";
        ddl.DataBind();
    }
    private void FillSemesterDropDown(DropDownList ddl, string courseCode)
    {
        List<int> semesters = courseOfferingBO.GetSemesterForCourseCode(courseCode);
        ddl.Items.Clear();
        ddl.DataSource = semesters;
        ddl.DataBind();
    }
    private void FillYearDropDown(DropDownList ddl, string courseCode, int semester)
    {
        List<int> years = courseOfferingBO.GetYearForCourseCode(courseCode, semester);
        ddl.ClearSelection();
        ddl.DataSource = years;
        ddl.DataBind();
        ddl.Items.Insert(0,"Select");
    }
    private void FillCourseCodeDropDown(DropDownList ddl)
    {
        List<CourseProgrammeVO> courses = courseBO.GetCourseForPgmMajor(studentMajorVO.ProgrammeID, studentMajorVO.MajorID);
        ddl.DataSource = courses;
        ddl.DataTextField = "CourseCode";
        ddl.DataValueField = "CourseCode";
        ddl.DataBind();
    }
    private void AddNewRowToCourseGrid(ILMPCourseVO ilmpCourseVO)
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    if (i != dtCurrentTable.Rows.Count - 2)
                    {
                    }
                    else
                    {
                        DropDownList ddl1 = (DropDownList)gvIlmpCourse.Rows[i].Cells[2].FindControl("ddSemester");
                        DropDownList ddl2 = (DropDownList)gvIlmpCourse.Rows[i].Cells[3].FindControl("ddYear");

                        // Update the DataRow with the DDL Selected Items                                     
                        dtCurrentTable.Rows[i]["Column1"] = ilmpCourseVO.CourseCode;
                        CourseProgrammeVO coursePgmVO = new CourseProgrammeVO();
                        coursePgmVO.CourseCode = ilmpCourseVO.CourseCode;
                        coursePgmVO.ProgrammeId = studentMajorVO.ProgrammeID;
                        coursePgmVO.MajorId = studentMajorVO.MajorID;
                        // for custom template, course may not be in same major selected
                        /*if (.SelectedItem.Text == "Generic")
                        {
                        ILMPCourseGridVO ilmpCourseGridVO = courseBO.GetCourseDetailsForTemplate(coursePgmVO);
                        dtCurrentTable.Rows[i]["Column2"] = ilmpCourseGridVO.CourseType;
                        dtCurrentTable.Rows[i]["Column5"] = ilmpCourseGridVO.Title;
                        dtCurrentTable.Rows[i]["Column6"] = ilmpCourseGridVO.Credits;
                        dtCurrentTable.Rows[i]["Column7"] = ilmpCourseGridVO.Level;
                        dtCurrentTable.Rows[i]["Column8"] = ilmpCourseGridVO.Prerequisites;
                         }
                        else
                        {*/
                            CourseVO courseVO = courseBO.GetCourseDetailsForCourseCode(coursePgmVO.CourseCode);
                            dtCurrentTable.Rows[i]["Column2"] = ilmpCourseVO.CourseType;
                            dtCurrentTable.Rows[i]["Column5"] = courseVO.Title;
                            dtCurrentTable.Rows[i]["Column6"] = courseVO.Credits;
                            dtCurrentTable.Rows[i]["Column7"] = courseVO.Level;
                            dtCurrentTable.Rows[i]["Column8"] = courseVO.Prerequisites.AllPrerequisites;
                        //}

                        FillSemesterDropDown(ddl1, coursePgmVO.CourseCode);
                        if (ddl1.SelectedItem != null && ddl1.SelectedItem.Text != "Select")
                        {
                            dtCurrentTable.Rows[i]["Column3"] = ilmpCourseVO.Semester; //ddl1.SelectedItem.Text;
                            FillYearDropDown(ddl2, coursePgmVO.CourseCode, int.Parse(ddl1.SelectedItem.Text));
                        }
                        if (ddl2.SelectedItem != null)
                        {
                            dtCurrentTable.Rows[i]["Column4"] = ilmpCourseVO.Year; //ddl2.SelectedItem.Text;
                        }
                    }
                }
                //Rebind the Grid with the current data
                gvIlmpCourse.DataSource = dtCurrentTable;
                gvIlmpCourse.DataBind();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('ViewState is null ');", true);
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataInCourseGrid();
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('ViewState is null ');", true);
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataInWorkShopGrid();
    }
    private void SetPreviousDataInCourseGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i < dt.Rows.Count - 1)
                    {
                        DropDownList ddl2 = (DropDownList)gvIlmpCourse.Rows[rowIndex].Cells[2].FindControl("ddSemester");
                        DropDownList ddl3 = (DropDownList)gvIlmpCourse.Rows[rowIndex].Cells[3].FindControl("ddYear");
                        string courseCode = dt.Rows[i]["Column1"].ToString();
                        ((Label)gvIlmpCourse.Rows[rowIndex].Cells[0].FindControl("lblCourseCode")).Text = courseCode;

                        FillSemesterDropDown(ddl2, courseCode);
                        ((DropDownList)gvIlmpCourse.Rows[rowIndex].Cells[1].FindControl("ddCourseType")).Text = dt.Rows[i]["Column2"].ToString();
                        ((DropDownList)gvIlmpCourse.Rows[rowIndex].Cells[2].FindControl("ddSemester")).Text = dt.Rows[i]["Column3"].ToString();
                        if (dt.Rows[i]["Column3"].ToString() != "")
                        {
                            FillYearDropDown(ddl3, courseCode, int.Parse(dt.Rows[i]["Column3"].ToString()));
                            ((DropDownList)gvIlmpCourse.Rows[rowIndex].Cells[3].FindControl("ddYear")).Text = dt.Rows[i]["Column4"].ToString();
                        }
                        ((Label)gvIlmpCourse.Rows[i].Cells[4].FindControl("lblTitle")).Text = dt.Rows[i]["Column5"].ToString();
                        ((Label)gvIlmpCourse.Rows[i].Cells[5].FindControl("lblCredits")).Text = dt.Rows[i]["Column6"].ToString();
                        ((Label)gvIlmpCourse.Rows[i].Cells[6].FindControl("lbllevel")).Text = dt.Rows[i]["Column7"].ToString();
                        ((Label)gvIlmpCourse.Rows[i].Cells[7].FindControl("lblPrerequisites")).Text = dt.Rows[i]["Column8"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
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
    protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        int totalRows = gvIlmpCourse.Rows.Count;
        if (totalRows > 1)
        {
            GridViewRow row = ((DropDownList)sender).NamingContainer as GridViewRow;
            string requisite = (row.FindControl("lblPrerequisites") as Label).Text;
            int semester = int.Parse((row.FindControl("ddSemester") as DropDownList).SelectedItem.Text);
            int year = int.Parse((row.FindControl("ddYear") as DropDownList).SelectedItem.Text);
            if (requisite != "")
            {
                //  check for prerequisite course completion
                for (int i = 0; i < totalRows; i++)
                {
                    string courseCode = ((Label)gvIlmpCourse.Rows[i].Cells[0].FindControl("lblCourseCode")).Text;
                    if (requisite.Contains(courseCode))
                    {
                       /* if (requisite != courseCode)
                        {
                            string requisiteType = requisite.Substring(requisite.IndexOf(courseCode) - 1, requisite.IndexOf(courseCode));
                            DropDownList ddl1 = (DropDownList)gvIlmpCourse.Rows[i].Cells[3].FindControl("ddYear");
                            int prerequisiteYear = int.Parse(ddl1.SelectedItem.Text);
                            if (prerequisiteYear > year)
                            {
                                Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                return;
                            }
                            if (requisiteType == "#")
                            {
                                if (prerequisiteYear == year)
                                {
                                    DropDownList ddl2 = (DropDownList)gvIlmpCourse.Rows[i].Cells[2].FindControl("ddSemester");
                                    int prerequisiteSemester = int.Parse(ddl2.SelectedItem.Text);
                                    if (prerequisiteSemester > semester)
                                    {
                                        Response.Write("<script>alert(' Corequisite " + courseCode + " not completed ');</script>");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (prerequisiteYear == year)
                                {
                                    DropDownList ddl2 = (DropDownList)gvIlmpCourse.Rows[i].Cells[2].FindControl("ddSemester");
                                    int prerequisiteSemester = int.Parse(ddl2.SelectedItem.Text);
                                    if (prerequisiteSemester >= semester)
                                    {
                                        Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DropDownList ddl1 = (DropDownList)gvIlmpCourse.Rows[i].Cells[3].FindControl("ddYear");
                            int prerequisiteYear = int.Parse(ddl1.SelectedItem.Text);
                            if (prerequisiteYear > year)
                            {
                                Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                return;
                            }
                            else if (prerequisiteYear == year)
                            {
                                DropDownList ddl2 = (DropDownList)gvIlmpCourse.Rows[i].Cells[2].FindControl("ddSemester");
                                int prerequisiteSemester = int.Parse(ddl2.SelectedItem.Text);
                                if (prerequisiteSemester >= semester)
                                {
                                    Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                    return;
                                }
                            }
                        }*/
                    }
                }
            }
        }
    }

    protected void ddSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).NamingContainer as GridViewRow;
        Label courseCode = row.FindControl("lblCourseCode") as Label;
        DropDownList ddl2 = row.FindControl("ddSemester") as DropDownList;
        DropDownList ddl3 = row.FindControl("ddYear") as DropDownList;
        string semester = ddl2.SelectedValue;

        if (semester != null && semester != "Select")
        {
            FillYearDropDown(ddl3, courseCode.Text, int.Parse(semester));
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ILMPVO ilmpVO = new ILMPVO();
            ilmpVO.IlmpId = int.Parse(hfIlmpId.Value);
            ilmpVO.Active = ddActive.SelectedItem.Text;
            ilmpVO.Description = txtDescription.Text;
            // description to be added
            List<ILMPCourseVO> ilmpCourses = new List<ILMPCourseVO>();
            ILMPCourseVO ilmpCourseVO;
            for (int i = 0; i < gvIlmpCourse.Rows.Count - 1; i++)
            {
                ilmpCourseVO = new ILMPCourseVO();
                ilmpCourseVO.CourseCode = ((Label)gvIlmpCourse.Rows[i].Cells[0].FindControl("lblCourseCode")).Text;
                DropDownList ddl1 = (DropDownList)gvIlmpCourse.Rows[i].Cells[1].FindControl("ddCourseType");
                DropDownList ddl2 = (DropDownList)gvIlmpCourse.Rows[i].Cells[2].FindControl("ddSemester");
                DropDownList ddl3 = (DropDownList)gvIlmpCourse.Rows[i].Cells[3].FindControl("ddYear");
                DropDownList ddl4 = (DropDownList)gvIlmpCourse.Rows[i].Cells[3].FindControl("ddResult");
                // type to be added if required
                ilmpCourseVO.Semester = int.Parse(ddl2.SelectedItem.Text);
                if (ddl3.SelectedItem.Text != null && ddl3.SelectedItem.Text != "Select")
                {
                    ilmpCourseVO.Year = int.Parse(ddl3.SelectedItem.Text);
                }
                ilmpCourseVO.Result = ddl4.SelectedItem.Text;
                ilmpCourses.Add(ilmpCourseVO);
            }
            ilmpVO.IlmpCourses = ilmpCourses;
            Boolean status = ilmpBO.UpdateILMP(ilmpVO);
            if (status)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' ILMP updated successfully ');", true);
                //Response.Write("<script>alert(' ILMP updated successfully ');</script>");
                // btnUpdate.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Error in updating ILMP ');", true);
                //Response.Write("<script>alert(' Error in updating ILMP ');</script>");
                //btnUpdate.Enabled = true;
            }
        }
        catch (CustomException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + " ');", true);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ILMPListStaff.aspx");
    }
}