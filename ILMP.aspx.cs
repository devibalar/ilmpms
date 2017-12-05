using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Query.Dynamic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ILMP : System.Web.UI.Page
{
    ProgrammeBO programmeBO = new ProgrammeBO();
    ILMPTemplateBO ilmpTemplateBO = new ILMPTemplateBO();
    CourseBO courseBO = new CourseBO();
    CourseOfferingBO courseOfferingBO = new CourseOfferingBO();
    bool prerequisiteFailed = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {           
            SetInitialRow();
           // btnSave.Enabled = false;
        }
    }
    private void SetInitialRow()
    {
        txtCustomStudentId.Enabled = false;
        ddMajor.Enabled = true;        
        FillProgrammeDropDown();
        FillMajorDropDown();
        FillTemplateNameDropDown();
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
        ddl.Items.Insert(0, "Select");
    }
    private void FillMajorDropDown()
    {        
        DataSet ds = ilmpTemplateBO.GetAllMajorForProgramme(ddProgramme.SelectedItem.Value);
        ddMajor.ClearSelection();
        ddMajor.DataSource = ds;
        ddMajor.DataTextField = "MajorID";
        ddMajor.DataValueField = "MajorID";
        ddMajor.DataBind();
        ddMajor.Items.Insert(0, "Select");
    }
    private void FillProgrammeDropDown()
    {
        DataSet ds = ilmpTemplateBO.GetAllProgrammeInTemplate();
        ddProgramme.ClearSelection();
        ddProgramme.DataSource = ds;
        ddProgramme.DataTextField = "ProgrammeName";
        ddProgramme.DataValueField = "ProgrammeID";
        ddProgramme.DataBind();
        ddProgramme.Items.Insert(0, "Select");
    }
    private void FillTemplateNameDropDown()
    {
        string programme = ddProgramme.SelectedItem.Value;
        string major = ddMajor.SelectedItem.Value;
        int studentId = 0;
        if (rbtnILMPTemplateType.SelectedItem.Text == "Custom" && txtCustomStudentId.Text.Trim()!="")
        {           
            studentId = int.Parse(txtCustomStudentId.Text.Trim());
            DataSet ds = ilmpTemplateBO.GetAllTemplateNameForStudent(studentId);
            ddTemplateName.DataSource = ds;
            ddTemplateName.DataTextField = "TemplateName";
            ddTemplateName.DataValueField = "TemplateName";
            ddTemplateName.DataBind();
        }
        if (rbtnILMPTemplateType.SelectedItem.Text == "Generic")
        {
            // get templatename from ilmpgenerictemplate 
            DataSet ds = ilmpTemplateBO.GetAllTemplateName(programme, major, studentId);
            ddTemplateName.DataSource = ds;
            ddTemplateName.DataTextField = "TemplateName";
            ddTemplateName.DataValueField = "TemplateName";
            ddTemplateName.DataBind();
        }
    }
    protected void btnGetTemplate_Click(object sender, EventArgs e)
    {        
        ILMPTemplateVO ilmpTemplateVO = new ILMPTemplateVO();
        ILMPTemplateVO inilmpTemplateVO = new ILMPTemplateVO();
        if (ddProgramme.SelectedItem != null && ddProgramme.SelectedItem.Text != "" &&
            ddMajor.SelectedItem != null && ddMajor.SelectedItem.Text != "" &&
            ddTemplateName.SelectedItem != null && ddTemplateName.SelectedItem.Text != "")
        {
            inilmpTemplateVO.ProgrammeId = ddProgramme.SelectedItem.Value;
            inilmpTemplateVO.MajorId = ddMajor.SelectedItem.Value;
            inilmpTemplateVO.TemplateName = ddTemplateName.SelectedItem.Value;
            if (txtCustomStudentId.Text != null && txtCustomStudentId.Text!="")
            {
                inilmpTemplateVO.StudentId = int.Parse(txtCustomStudentId.Text);
            }
            if (rbtnILMPTemplateType.SelectedItem.Text == "Custom")
            {
                ilmpTemplateVO = ilmpTemplateBO.GetCutomTemplate(inilmpTemplateVO);
            }
            else
            {
                ilmpTemplateVO = ilmpTemplateBO.GetTemplate(inilmpTemplateVO);
            }
            hfTemplateId.Value = ilmpTemplateVO.TemplateId.ToString();
            if (ilmpTemplateVO.TemplateCourses.Count > 0)
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
                //Add a Dummy Data on Initial Load
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;
                //Bind the DataTable to the Grid
                gvIlmp.DataSource = dt;
                gvIlmp.DataBind();
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

                foreach (TemplateCourseVO templateCourseVO in ilmpTemplateVO.TemplateCourses)
                {
                    if (templateCourseVO.WorkshopId == 0 && templateCourseVO.CourseCode != "")
                    {
                        AddNewRowToCourseGrid(templateCourseVO);
                    }
                    else
                    {
                        AddNewRowToWorkshopGrid(templateCourseVO.WorkshopId);
                    }
                }
                if ((gvIlmp.Rows[gvIlmp.Rows.Count - 1].Cells[0].FindControl("lblCourseCode") as Label).Text == "")
                {
                    gvIlmp.Rows[gvIlmp.Rows.Count - 1].Visible = false;
                }
                if ((gvWorkshop.Rows[gvWorkshop.Rows.Count - 1].Cells[0].FindControl("lblWorkshop") as Label).Text == "")
                {
                    gvWorkshop.Rows[gvWorkshop.Rows.Count - 1].Visible = false;
                }
                btnSave.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' There are no courses found in this template.Please choose different template ');", true);
                //Response.Write("<script>alert(' There are no courses found in this template.Please choose different template ');</script>");
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Please select Programme, Major and Title  ');", true);
            //Response.Write("<script>alert(' Please select Programme, Major and Title ');</script>");
        }
    }
    private void AddNewRowToCourseGrid(TemplateCourseVO templateCourseVO)
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
                        DropDownList ddl1 = (DropDownList)gvIlmp.Rows[i].Cells[2].FindControl("ddSemester");
                        DropDownList ddl2 = (DropDownList)gvIlmp.Rows[i].Cells[3].FindControl("ddYear");

                        // Update the DataRow with the DDL Selected Items                                     
                        dtCurrentTable.Rows[i]["Column1"] = templateCourseVO.CourseCode;
                        CourseProgrammeVO coursePgmVO = new CourseProgrammeVO();
                        coursePgmVO.CourseCode = templateCourseVO.CourseCode;
                        coursePgmVO.ProgrammeId = ddProgramme.SelectedItem.Value;
                        coursePgmVO.MajorId = ddMajor.SelectedItem.Value;
                        ILMPCourseGridVO ilmpCourseGridVO = new ILMPCourseGridVO();
                        // for custom template, course may not be in same major selected
                        if (rbtnILMPTemplateType.SelectedItem.Text == "Generic")
                        {
                            ilmpCourseGridVO = courseBO.GetCourseDetailsForTemplate(coursePgmVO);
                            dtCurrentTable.Rows[i]["Column2"] = ilmpCourseGridVO.CourseType;
                            dtCurrentTable.Rows[i]["Column5"] = ilmpCourseGridVO.Title;
                            dtCurrentTable.Rows[i]["Column6"] = ilmpCourseGridVO.Credits;
                            dtCurrentTable.Rows[i]["Column7"] = ilmpCourseGridVO.Level;
                            dtCurrentTable.Rows[i]["Column8"] = ilmpCourseGridVO.Prerequisites;
                        }
                        else
                        {
                            CourseVO courseVO = courseBO.GetCourseDetailsForCourseCode(coursePgmVO.CourseCode);
                            dtCurrentTable.Rows[i]["Column2"] = "COM";
                            dtCurrentTable.Rows[i]["Column5"] = courseVO.Title;
                            dtCurrentTable.Rows[i]["Column6"] = courseVO.Credits;
                            dtCurrentTable.Rows[i]["Column7"] = courseVO.Level;
                            dtCurrentTable.Rows[i]["Column8"] = courseVO.Prerequisites.AllPrerequisites;
                        }
                      
                        // fills the semester dropdown from courseoffering. If there is any courseoffering, by default first value coming from db will be selected in semester dropdown
                        FillSemesterDropDown(ddl1, coursePgmVO.CourseCode);
                        // checks whether courseoffering is present by checking semster default selected value
                        if (ddl1.SelectedItem != null && ddl1.SelectedItem.Text != "Select")
                        {
                            dtCurrentTable.Rows[i]["Column3"] = templateCourseVO.Semester; //set the selected semester of dropdown to semester from ILMP template
                            FillYearDropDown(ddl2, coursePgmVO.CourseCode, int.Parse(ddl1.SelectedItem.Text));
                        }
                        if (ddl2.SelectedItem != null)
                        {
                            dtCurrentTable.Rows[i]["Column4"] = templateCourseVO.Year;//set the selected year of dropdown to year from ILMP template
                        }
                    }
                }
                //Rebind the Grid with the current data
                gvIlmp.DataSource = dtCurrentTable;
                gvIlmp.DataBind();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' ViewState is null ');", true);
           // Response.Write("ViewState is null");
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' ViewState is null ');", true);
           // Response.Write("ViewState is null");
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
                        DropDownList ddl2 = (DropDownList)gvIlmp.Rows[rowIndex].Cells[2].FindControl("ddSemester");
                        DropDownList ddl3 = (DropDownList)gvIlmp.Rows[rowIndex].Cells[3].FindControl("ddYear"); 
                        string courseCode = dt.Rows[i]["Column1"].ToString();
                        ((Label)gvIlmp.Rows[rowIndex].Cells[0].FindControl("lblCourseCode")).Text = courseCode;

                        FillSemesterDropDown(ddl2,courseCode);
                        ((DropDownList)gvIlmp.Rows[rowIndex].Cells[1].FindControl("ddCourseType")).Text = dt.Rows[i]["Column2"].ToString();
                        ((DropDownList)gvIlmp.Rows[rowIndex].Cells[2].FindControl("ddSemester")).Text = dt.Rows[i]["Column3"].ToString();
                        if (dt.Rows[i]["Column3"].ToString() != "")
                        {
                            // In BIT course, semester and year might not be present in all cases. Populate default values for semester and year in this case
                            if(int.Parse(dt.Rows[i]["Column3"].ToString())==0)
                            {
                                FillYearDropDown(ddl3, courseCode, int.Parse(((DropDownList)gvIlmp.Rows[rowIndex].Cells[2].FindControl("ddSemester")).Text));
                            }
                            else{
                                FillYearDropDown(ddl3, courseCode, int.Parse(dt.Rows[i]["Column3"].ToString()));
                            }
                            ((DropDownList)gvIlmp.Rows[rowIndex].Cells[3].FindControl("ddYear")).Text = dt.Rows[i]["Column4"].ToString();
                        }
                        ((Label)gvIlmp.Rows[i].Cells[4].FindControl("lblTitle")).Text = dt.Rows[i]["Column5"].ToString();
                        ((Label)gvIlmp.Rows[i].Cells[5].FindControl("lblCredits")).Text = dt.Rows[i]["Column6"].ToString();
                        ((Label)gvIlmp.Rows[i].Cells[6].FindControl("lbllevel")).Text = dt.Rows[i]["Column7"].ToString();
                        ((Label)gvIlmp.Rows[i].Cells[7].FindControl("lblPrerequisites")).Text = dt.Rows[i]["Column8"].ToString();
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
 
    protected void ddSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).NamingContainer as GridViewRow;
        Label courseCode = row.FindControl("lblCourseCode") as Label;
        DropDownList ddl2 = row.FindControl("ddSemester") as DropDownList;
        DropDownList ddl3 = row.FindControl("ddYear") as DropDownList;
        string semesterStr = ddl2.SelectedValue;

        if (semesterStr != null && semesterStr != "Select")
        {
            FillYearDropDown(ddl3, courseCode.Text, int.Parse(semesterStr));
        }

        int totalRows = gvIlmp.Rows.Count;
        if (totalRows > 1)
        {
            string requisite = (row.FindControl("lblPrerequisites") as Label).Text;
            string semesterstr = (row.FindControl("ddSemester") as DropDownList).SelectedItem.Text;
            string yearstr = (row.FindControl("ddYear") as DropDownList).SelectedItem.Text;
            if (semesterstr != "Select" && yearstr != "Select")
            {
                int semester = int.Parse(semesterstr);
                int year = int.Parse(yearstr);
                if (requisite != "")
                {
                    //  check for prerequisite course completion
                    for (int i = 0; i < totalRows - 1; i++)
                    {
                        string courseCode1 = ((Label)gvIlmp.Rows[i].Cells[0].FindControl("lblCourseCode")).Text;
                        if (requisite.Contains(courseCode1))
                        {
                            if (requisite != courseCode1)
                            {
                                string requisiteType = requisite.Substring(requisite.IndexOf(courseCode1) - 1, requisite.IndexOf(courseCode1));
                                DropDownList ddl1 = (DropDownList)gvIlmp.Rows[i].Cells[3].FindControl("ddYear");
                                int prerequisiteYear = int.Parse(ddl1.SelectedItem.Text);
                                if (prerequisiteYear > year)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Prerequisite " + courseCode1 + " not completed ');", true);
                                    //Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                    return;
                                }
                                if (requisiteType == "#")
                                {
                                    if (prerequisiteYear == year)
                                    {
                                        DropDownList ddl4 = (DropDownList)gvIlmp.Rows[i].Cells[2].FindControl("ddSemester");
                                        int prerequisiteSemester = int.Parse(ddl4.SelectedItem.Text);
                                        if (prerequisiteSemester > semester)
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Corequisite " + courseCode1 + " not completed ');", true);
                                            //Response.Write("<script>alert(' Corequisite " + courseCode + " not completed ');</script>");
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (prerequisiteYear == year)
                                    {
                                        DropDownList ddl4 = (DropDownList)gvIlmp.Rows[i].Cells[2].FindControl("ddSemester");
                                        int prerequisiteSemester = int.Parse(ddl4.SelectedItem.Text);
                                        if (prerequisiteSemester >= semester)
                                        {
                                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Prerequisite " + courseCode1 + " not completed  ');", true);
                                            //Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DropDownList ddl1 = (DropDownList)gvIlmp.Rows[i].Cells[3].FindControl("ddYear");
                                int prerequisiteYear = int.Parse(ddl1.SelectedItem.Text);
                                if (prerequisiteYear > year)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Prerequisite " + courseCode1 + " not completed   ');", true);
                                    // Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                    return;
                                }
                                else if (prerequisiteYear == year)
                                {
                                    DropDownList ddl4 = (DropDownList)gvIlmp.Rows[i].Cells[2].FindControl("ddSemester");
                                    int prerequisiteSemester = int.Parse(ddl4.SelectedItem.Text);
                                    if (prerequisiteSemester >= semester)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Prerequisite " + courseCode1 + " not completed  ');", true);
                                        // Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        } 
    }   

    protected void btnSave_Click(object sender, EventArgs e)
    {
       ILMPVO ilmpVO = new ILMPVO();
       try
       {
           ilmpVO.Name = txtName.Text;
           if (txtStudentId.Text.Trim() != "")
           {
               try
               {
                   ilmpVO.StudentId = Int32.Parse(txtStudentId.Text);
               }
               catch (ParseException)
               {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' StudentId is not valid ');", true);
                   // Response.Write("<script>alert('StudentId is not valid');</script>");
                   return;
               }
           }
           else
           {
               ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please enter student id');", true);
               // Response.Write("<script>alert('Please enter student id');</script>");
               return;
           }
           ilmpVO.Active = ddActive.SelectedItem.Text;
           ilmpVO.Description = txtDescription.Text;
           List<ILMPCourseVO> templateCourses = new List<ILMPCourseVO>();
           if (ViewState["CurrentTable"] != null)
           {
               DataTable dt = (DataTable)ViewState["CurrentTable"];

               // storing the grid values in KeyValue Pair because every semester will have multiple entries, it should be consolidated to store in database
               if (dt.Rows.Count > 0)
               {
                   ILMPCourseVO ilmpCourseVO = new ILMPCourseVO();
                   for (int i = 0; i < dt.Rows.Count - 1; i++)
                   {
                       ilmpCourseVO = new ILMPCourseVO();
                       Label courseCode = (Label)gvIlmp.Rows[i].Cells[0].FindControl("lblCourseCode");
                       DropDownList ddl1 = (DropDownList)gvIlmp.Rows[i].Cells[1].FindControl("ddCourseType");
                       DropDownList ddl2 = (DropDownList)gvIlmp.Rows[i].Cells[2].FindControl("ddSemester");
                       DropDownList ddl3 = (DropDownList)gvIlmp.Rows[i].Cells[3].FindControl("ddYear");
                       string courseType = ddl1.SelectedItem.Text;
                       int semester = Int32.Parse(ddl2.SelectedItem.Text);
                       int year = 0;
                       if (ddl3.SelectedItem.Text != null && ddl3.SelectedItem.Text != "Select")
                       {
                           year = Int32.Parse(ddl3.SelectedItem.Text);
                       }
                       ilmpCourseVO.CourseCode = courseCode.Text;
                       ilmpCourseVO.Semester = semester;
                       ilmpCourseVO.Year = year;
                       ilmpCourseVO.CourseType = courseType;
                       templateCourses.Add(ilmpCourseVO);
                   }
                   ilmpVO.IlmpCourses = templateCourses;
                   ilmpVO.TemplateId = int.Parse(hfTemplateId.Value);
                   ILMPBO ilmpBO = new ILMPBO();
                   string status = ilmpBO.AddILMP(ilmpVO);
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + status + "');", true);
                   //Response.Write("<script>alert('" + status + "');</script>");
                   /* if (status.Contains("success"))
                    {
                      //  btnSave.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }*/
               }
               else
               {
                   ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('There are no courses added in ILMP');", true);
                   // Response.Write("<script>alert('There are no courses added in ILMP');</script>");
               }
           }
       }
       catch (CustomException ex)
       {
           ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
       }
    }
    
    protected void ddProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        string programmeCode = ddProgramme.SelectedItem.Value;
        string major = ddMajor.SelectedItem.Value;
        if (programmeCode != null && programmeCode != "Select")
        {
            FillMajorDropDown();
        }
        if (programmeCode != null && programmeCode != "Select" && major != null && major != "Select")
        {
            FillTemplateNameDropDown();
        }
         
    }
    protected void ddMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string programmeCode = ddProgramme.SelectedItem.Value;
        string major = ddMajor.SelectedItem.Value;
        if (programmeCode != null && major != null)
        {
            FillTemplateNameDropDown();
        }
    }
    protected void rbtnILMPTemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnILMPTemplateType.SelectedItem.Text == "Generic")
        {
            txtCustomStudentId.Enabled = false;
            txtStudentId.Text = "";
            txtCustomStudentId.Text = "";
            txtName.Text = "";
            FillTemplateNameDropDown();
            ddMajor.Enabled = true;
        }
        else if (rbtnILMPTemplateType.SelectedItem.Text == "Custom")
        {
            txtCustomStudentId.Enabled = true;
            txtStudentId.Text = "";
            txtCustomStudentId.Text = "";
            txtName.Text = "";
            FillTemplateNameDropDown();
        }
    }
    protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       // int totalRows = gvIlmp.Rows.Count;
        
    }
    
    protected void txtCustomStudentId_TextChanged(object sender, EventArgs e)
    {
        StudentBO studentBO = new StudentBO();
        StudentMajorBO studentmajorBO = new StudentMajorBO();
        int studentId = 0;
        if (txtCustomStudentId.Text.Trim() != "")
        {
            try
            {
                studentId = int.Parse(txtCustomStudentId.Text.Trim());
                string studentName = studentBO.GetStudentNameForId(studentId);
                if (studentName != "")
                {
                    List<StudentMajorVO> stmajorList = studentmajorBO.GetStudentMajorList(studentId);
                    string major = "";
                    foreach (StudentMajorVO studentMajorVO in stmajorList)
                    {
                        major += "&" + studentMajorVO.MajorID;
                    }
                    if (major.StartsWith("&"))
                    {
                        major = major.Substring(1);
                    }
                    txtStudentMajor.Text = major;                   
                    ddMajor.Enabled = false;
                    txtStudentId.Text = studentId.ToString();
                }
                txtName.Text = studentName;
                if (studentName == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Please enter a valid student id');", true);
                   // Response.Write("<script>alert(' Please enter a valid student id ');</script>");
                    return;
                }
                else
                {
                    FillTemplateNameDropDown();
                }
            }
            catch (ParseException)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Please enter a valid student id ');", true);
                //Response.Write("<script>alert(' Please enter a valid student id ');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(' Error in getting student details ');</script>");
                ExceptionUtility.LogException(ex, "Error Page");
            }
        }
        else
        {
            txtStudentId.Text = "";
            txtName.Text = "";
        }
    }
    protected void txtStudentId_TextChanged(object sender, EventArgs e)
    {
        StudentBO studentBO = new StudentBO();
        int studentId = 0;
        if (txtStudentId.Text.Trim() != "")
        {
            try
            {
                studentId = int.Parse(txtStudentId.Text.Trim());
                string studentName = studentBO.GetStudentNameForId(studentId);
                txtName.Text = studentName;
                if (studentName == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Please enter a valid student id ');", true);
                   // Response.Write("<script>alert(' Please enter a valid student id ');</script>");
                }
            }
            catch (ParseException)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Please enter a valid student id ');", true);
                //Response.Write("<script>alert(' Please enter a valid student id ');</script>");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Error in getting student details ');", true);
                //Response.Write("<script>alert(' Error in getting student details ');</script>");
                ExceptionUtility.LogException(ex, "Error Page");
            }
        }
        else
        {
            txtStudentId.Text = "";
            txtName.Text = "";
        }
    }
}