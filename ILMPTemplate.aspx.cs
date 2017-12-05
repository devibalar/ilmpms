using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Query.Dynamic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ILMPTemplate : System.Web.UI.Page
{
    CourseBO courseBO = new CourseBO();
    ProgrammeWorkshopBO programmeWorkshopBO = new ProgrammeWorkshopBO();
    ILMPTemplateBO ilmpTemplateBO = new ILMPTemplateBO();
    bool templateCreated = false;
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
       {
            SetInitialValues();
            btnEditCourse.Visible = false;
            btnEditWorkshop.Visible = false;
       }
       else
       {          
            if (templateCreated)
            {
                btnEditCourse.Visible = true;
                btnEditWorkshop.Visible = true;
            }
       }
    }
    private void SetInitialValues()
    {
        FillProgrammeDropDown();
        FillMajorDropDown();       
        // Set template name from default selection of programme and major
        string programme = ddProgramme.SelectedItem.Value;
        string major = ddMajor.SelectedItem.Value;
        txtTemplateName.Text = programme + "-" + major;        
        SetInitialRowInGrids();
        ChangeTemplateType();
    }
    //to create grid for Courses and workshop with one row added by default 
    private void SetInitialRowInGrids()
    {
        // Add an empty row in Course grid
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
        
        gvCourse.DataSource = dt;
        gvCourse.DataBind();
        gvCourse.Rows[0].Cells[0].Width=30;
        DropDownList ddl1 = (DropDownList)gvCourse.Rows[0].Cells[0].FindControl("ddSemester");
        DropDownList ddl2 = (DropDownList)gvCourse.Rows[0].Cells[1].FindControl("ddYear");
        FillSemesterDropDown(ddl1);
        FillYearDropDown(ddl2, int.Parse(ddl1.SelectedItem.Text));
        DropDownList ddl3 = (DropDownList)gvCourse.Rows[0].Cells[2].FindControl("ddCourseCode");        
        FillCourseCodeDropDown(ddl3);

        //Add an empty row in Workshop grid
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        //Define the Columns
        dt1.Columns.Add(new DataColumn("Column1", typeof(string)));
    
        //Add a Dummy Data on Initial Load
        dr1 = dt1.NewRow();
        dt1.Rows.Add(dr1);

        //Store the DataTable in ViewState
        ViewState["CurrentTableWorkshop"] = dt1;
        //Bind the DataTable to the Grid
        gvWorkshop.DataSource = dt1;
        gvWorkshop.DataBind();
        gvWorkshop.Rows[0].Cells[0].Width = 70;
        DropDownList ddl4 = (DropDownList)gvWorkshop.Rows[0].Cells[0].FindControl("ddWorkshop");
        FillWorkshopDropDown(ddl4);
       
    }
    private void FillMajorDropDown()
    {
        ProgrammeMajorBO programmeMajorBO = new ProgrammeMajorBO();
        DataSet ds = programmeMajorBO.getMajorForProgramme(ddProgramme.SelectedItem.Value);
        ddMajor.DataSource = ds;
        ddMajor.DataTextField = "MajorName";
        ddMajor.DataValueField = "MajorID";
        ddMajor.DataBind();        
        if (ddProgramme.SelectedItem.Text != "BIT")
        {
            ddMajor.Items.Remove("NA");
        }       
    }

    private void FillProgrammeDropDown()
    {
        ProgrammeBO programmeBO = new ProgrammeBO();
        DataSet ds = programmeBO.getAllProgram();
        ddProgramme.DataSource = ds;
        ddProgramme.DataTextField = "ProgrammeName";
        ddProgramme.DataValueField = "ProgrammeID";
        ddProgramme.DataBind();
    }
    //set template name automatically from selected programme and major
    private void SetTemplateName()
    {
        if (ddProgramme.SelectedItem != null && ddProgramme.SelectedItem.Value != "" && ddMajor.SelectedItem != null && ddMajor.SelectedItem.Value != "")
        {
            string programme = ddProgramme.SelectedItem.Value;
            string major = ddMajor.SelectedItem.Value;
            txtTemplateName.Text = programme +"-"+ major;
            List<CourseProgrammeVO> courses = courseBO.GetCourseForPgmMajor(programme, major);           
            for(int i=0;i<gvCourse.Rows.Count;i++)
            {
               // populate course code available for the seleted programme and major
                DropDownList ddl = (DropDownList)gvCourse.Rows[i].Cells[0].FindControl("ddCourseCode");
                ddl.DataSource = courses;
                ddl.DataTextField = "CourseCode";
                ddl.DataValueField = "CourseCode";
                ddl.DataBind();      
            }
        }
    }

    private ArrayList GetYear()
    {
        ArrayList arr = new ArrayList();
        string currentYearStr = DateTime.Now.Year.ToString();
        int currentYear = Int32.Parse(currentYearStr);
        arr.Add(new ListItem(currentYearStr, currentYearStr));
        arr.Add(new ListItem((currentYear + 1).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 2).ToString(), (currentYear + 2).ToString()));
        arr.Add(new ListItem((currentYear + 3).ToString(), (currentYear + 3).ToString()));
        arr.Add(new ListItem((currentYear + 4).ToString(), (currentYear + 4).ToString()));
        arr.Add(new ListItem((currentYear + 5).ToString(), (currentYear + 5).ToString())); 
        return arr;
    }


    private void FillYearDropDown(DropDownList ddl)
    {
        ArrayList arr = GetYear();
        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
    }

    private void FillYearDropDown(DropDownList ddl, int semester)
    {
        CourseOfferingBO courseOfferingBO = new CourseOfferingBO();
        DataSet ds = courseOfferingBO.FillYearForSemester(semester);
        ddl.DataSource = ds;
        ddl.DataTextField = "Year";
        ddl.DataValueField = "Year";
        ddl.DataBind();
        ddl.Items.Insert(0, "Select");
    }

    private void FillSemesterDropDown(DropDownList ddl)
    {
        SemesterBO semesterBO = new SemesterBO();
        DataSet ds = semesterBO.getAllSemesters();
        ddl.DataSource = ds;
        ddl.DataTextField = "SemesterID";
        ddl.DataValueField = "SemesterID";
        ddl.DataBind();
    }
    private void FillCourseCodeDropDown(DropDownList ddl)
    {
        if (rbtnlTemplateType.SelectedItem.Text == "Generic")
        {
            List<CourseProgrammeVO> courses = courseBO.GetCourseForPgmMajor(ddProgramme.SelectedItem.Value, ddMajor.SelectedItem.Value);
            ddl.DataSource = courses;
            ddl.DataTextField = "CourseCode";
            ddl.DataValueField = "CourseCode";
            ddl.DataBind();
        }
        else
        {
            CourseOfferingBO couseOfferingBO = new CourseOfferingBO();
            DataSet ds = couseOfferingBO.GetAllAvailableCourseCode();
            ddl.DataSource = ds;
            ddl.DataTextField = "CourseCode";
            ddl.DataValueField = "CourseCode";
            ddl.DataBind();
        }
        ddl.Items.Insert(0,"Select");
    }
    private void FillCourseCodeDropDown(DropDownList ddl, int semester, int year)
    {
        CourseOfferingBO courseOfferigBO = new CourseOfferingBO();
        string programmeId = ddProgramme.SelectedItem.Value;
        string majorId = ddMajor.SelectedItem.Value;
        if (rbtnlTemplateType.SelectedItem.Text == "Generic")
        {
            List<CourseOfferingVO> coursesOffered = courseOfferigBO.GetOfferedCoursesForPgmMajor(semester, year, programmeId, majorId);
            ddl.DataSource = coursesOffered;
            ddl.DataTextField = "CourseCode";
            ddl.DataValueField = "CourseCode";
            ddl.DataBind();
        }
        else
        {
           // DataSet ds = courseBO.GetAllCourseCode();
            List<CourseOfferingVO> coursesOffered = courseOfferigBO.GetOfferedCoursesForSemester(semester,year);
            ddl.DataSource = coursesOffered;
            ddl.DataTextField = "CourseCode";
            ddl.DataValueField = "CourseCode";
            ddl.DataBind();
        }
        ddl.Items.Insert(0, "Select");
    }
    private void FillWorkshopDropDown(DropDownList ddl)
    {
        List<WorkshopVO> workshops=  programmeWorkshopBO.GetAllWorkshopForPgmMajor(ddProgramme.SelectedItem.Value, ddMajor.SelectedItem.Value);
        ddl.DataSource = workshops;
        ddl.DataTextField = "WorkshopName";
        ddl.DataValueField = "WorkshopId";
        ddl.DataBind();
    }

    private void ChangeTemplateType()
    {
        if (rbtnlTemplateType.SelectedItem.Text == "Generic")
        {
            txtStudentId.Enabled = false;
            txtStudentId.Text = "";
            txtStudentName.Text = "";
        }
        else if (rbtnlTemplateType.SelectedItem.Text == "Custom")
        {
            txtStudentId.Enabled = true;
            txtStudentId.Text = "";
            txtStudentName.Text = "";
            ddMajor.Items.Add("NA");
        }
        SetInitialRowInGrids();

    }
   
    protected void ddProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMajorDropDown();
        SetTemplateName();
        //SetVisibilityForType();
        SetInitialRowInGrids(); // to reset  the course and workshop grids on change
        // populate coursecode available for programme and major in grid dropdown 
    }
    protected void ddMajor_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetTemplateName();
        SetInitialRowInGrids(); // to reset the  course and workshop grids on major change
    }
    protected void rbtnlTemplateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeTemplateType();        
    }
    protected void ddCourseCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).NamingContainer as GridViewRow;
        DropDownList ddl = row.FindControl("ddCourseCode") as DropDownList;
        string selectedCourseCode = ddl.SelectedItem.Value;
       // CourseVO courseVO = courseBO.GetCourseDetailsForCourseCode(selectedCourseCode);
        CourseProgrammeVO coursePgmVO = new CourseProgrammeVO();
        coursePgmVO.CourseCode = selectedCourseCode;
        coursePgmVO.ProgrammeId = ddProgramme.SelectedItem.Value;
        coursePgmVO.MajorId = ddMajor.SelectedItem.Value;
        ILMPCourseGridVO ilmpCourseGridVO =new ILMPCourseGridVO ();
        CourseVO courseVO = new CourseVO();
        if (rbtnlTemplateType.SelectedItem.Text == "Generic")
        {
            ilmpCourseGridVO = courseBO.GetCourseDetailsForTemplate(coursePgmVO);
            (row.FindControl("ddCourseType") as DropDownList).Text = ilmpCourseGridVO.CourseType;

            //(row.FindControl("lblCourseType") as Label).Text = courseVO.;
            (row.FindControl("lblCourseName") as Label).Text = ilmpCourseGridVO.Title;
            (row.FindControl("lblCredits") as Label).Text = ilmpCourseGridVO.Credits.ToString();
            (row.FindControl("lblLevel") as Label).Text = ilmpCourseGridVO.Level.ToString();
            (row.FindControl("lblPrerequisites") as Label).Text = ilmpCourseGridVO.Prerequisites;
        }
        else if (rbtnlTemplateType.SelectedItem.Text == "Custom")
        {
             courseVO = courseBO.GetCourseDetailsForCourseCode(selectedCourseCode);
            (row.FindControl("lblCourseName") as Label).Text = courseVO.Title;
            (row.FindControl("lblCredits") as Label).Text = courseVO.Credits.ToString();
            (row.FindControl("lblLevel") as Label).Text = courseVO.Level.ToString();
            (row.FindControl("lblPrerequisites") as Label).Text = courseVO.Prerequisites.AllPrerequisites;
        }
        #region verify prerequisites
        int totalRows = gvCourse.Rows.Count;
        if (totalRows >= 1)
        {
            string requisite = (row.FindControl("lblPrerequisites") as Label).Text;
            int semester = int.Parse((row.FindControl("ddSemester") as DropDownList).SelectedItem.Text);
            string yearstr = (row.FindControl("ddYear") as DropDownList).SelectedItem.Text;
            int year=0;
            if(yearstr!="Select")
             year = int.Parse(yearstr);
            if (requisite != "" && yearstr != "Select")
            {
                Boolean prerequisiteFound = false;
                int prerequisiteCount = 0;
                if (requisite.Contains("&") && !requisite.Contains("#"))
                {
                    string[] reqArr = requisite.Split('&');
                    prerequisiteCount = reqArr.Length;                    
                }
                else if (requisite.Contains("#")  && requisite.Length <= 6)//one co-rerequite or one co-requisite and one prequisite 
                {
                    prerequisiteCount = 1;
                }
                else if (requisite.Contains("#") & !requisite.Contains("&") && requisite.Length <= 11)  // one co-requisite and one prequisite               
                {                        
                        prerequisiteCount = 2;                    
                }               
                else if (requisite.Contains("&") & requisite.Contains("#")) // multiple prerequisite & one corequisite
                {
                    string[] reqArr = requisite.Split('&');
                    prerequisiteCount = reqArr.Length;
                    foreach (string req in reqArr)
                    {
                        if (req.Contains("#"))
                        {
                            string[] reqArr1 = requisite.Split('#');
                            prerequisiteCount += reqArr1.Length - 1;
                        }
                    }
                }              
                int requisiteFoundCount = 0;
                //  check for prerequisite course completion
                string type = "";
                string message = "";
                if (requisite.Length>6 && requisite.Contains("#")) 
                {
                    message = "Prerequisite/Corequisite not selected in Ilmp";
                }
                else if (!requisite.Contains("#"))
                {
                    message = "Prerequisite not selected in Ilmp";
                }
                else if (requisite.Contains("#") && requisite.Length == 6)
                {
                    message = "Corequisite not selected in Ilmp";
                }
                for (int i = 0; i < totalRows ; i++)
                {
                    string courseCode1 = ((DropDownList)gvCourse.Rows[i].Cells[2].FindControl("ddCourseCode")).SelectedItem.Text;
                    if (requisite.Contains(courseCode1))
                    {
                        prerequisiteFound = true;
                        requisiteFoundCount++; 
                        if (requisite != courseCode1)
                        {                            
                            string requisiteType="";
                            if (requisite.StartsWith(courseCode1))
                            {
                                requisiteType = "pre";                                    
                            }
                            else if ((requisite.Substring(requisite.IndexOf(courseCode1) - 1, requisite.IndexOf(courseCode1)) == "&"))
                            {
                                requisiteType = "pre";
                            }
                            else if (requisite.Contains("#" + courseCode1))
                            {
                                requisiteType = "#";
                            }
                            type += requisiteType;

                            DropDownList ddl1 = (DropDownList)gvCourse.Rows[i].Cells[1].FindControl("ddYear");
                            int prerequisiteYear = int.Parse(ddl1.SelectedItem.Text);
                            if (prerequisiteYear > year)
                            {
                                if (requisiteType == "#")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Corerequisite " + courseCode1 + " should be done in current or previous semester ');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Prerequisite " + courseCode1 + " not completed ');", true);
                                }
                                return;
                            }
                            if (requisiteType == "#")
                            {
                                if (prerequisiteYear == year)
                                {
                                    DropDownList ddl4 = (DropDownList)gvCourse.Rows[i].Cells[0].FindControl("ddSemester");
                                    int prerequisiteSemester = int.Parse(ddl4.SelectedItem.Text);
                                    if (prerequisiteSemester > semester)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Corequisite " + courseCode1 + " should be done in current or previous semester');", true);
                                        return;
                                    }
                                    else
                                    {
                                        //requisiteFoundCount++;
                                        //prerequisiteCount++;
                                    }
                                }
                                else
                                {
                                   // requisiteFoundCount++;
                                    //prerequisiteCount++;
                                }
                            }
                            else
                            {
                                if (prerequisiteYear == year)
                                {
                                    DropDownList ddl4 = (DropDownList)gvCourse.Rows[i].Cells[0].FindControl("ddSemester");
                                    int prerequisiteSemester = int.Parse(ddl4.SelectedItem.Text);
                                    if (prerequisiteSemester >= semester)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Prerequisite " + courseCode1 + " not completed  ');", true);                                        
                                        return;
                                    }
                                    else
                                    {
                                        requisiteFoundCount++;
                                    }
                                }
                            }
                        }
                        else
                        { // single prerequisite
                            DropDownList ddl1 = (DropDownList)gvCourse.Rows[i].Cells[1].FindControl("ddYear");
                            int prerequisiteYear = int.Parse(ddl1.SelectedItem.Text);
                            if (prerequisiteYear > year)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Prerequisite " + courseCode1 + " not completed   ');", true);                               
                                return;
                            }
                            else if (prerequisiteYear == year)
                            {
                                DropDownList ddl4 = (DropDownList)gvCourse.Rows[i].Cells[0].FindControl("ddSemester");
                                int prerequisiteSemester = int.Parse(ddl4.SelectedItem.Text);
                                if (prerequisiteSemester >= semester)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Prerequisite " + courseCode1 + " not completed  ');", true);                                    
                                    return;
                                }
                                else
                                {
                                  //  requisiteFoundCount++;
                                }
                            }
                            else
                            {
                               // requisiteFoundCount++;
                            }
                        }
                    }
                    else
                    {
                      /*  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  Prerequisite not completed  ');", true);
                        // Response.Write("<script>alert(' Prerequisite " + courseCode + " not completed ');</script>");
                        return;*/
                    }
                }
                if (!prerequisiteFound) // works if prerequisite not selected in Grid
                {
                   
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + message + "');", true);
                    return;                    
                }
                if (requisiteFoundCount < prerequisiteCount) // works in case of multiple prerequisites. if one of the prerequisite is not present
                {                  
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('  "+message+"  ');", true);
                    return;
                }

            }
        } 
    #endregion
       
        // add/update the last selected value in view state

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    if (row.RowIndex == i)
                    {
                        DropDownList ddl1 = row.FindControl("ddSemester") as DropDownList;
                        dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                        DropDownList ddl2 = row.FindControl("ddYear") as DropDownList;
                        dtCurrentTable.Rows[i]["Column2"] = ddl2.SelectedItem.Text;
                        dtCurrentTable.Rows[i]["Column3"] = selectedCourseCode;
                        if (rbtnlTemplateType.SelectedItem.Text == "Generic")
                        {
                            dtCurrentTable.Rows[i]["Column4"] = ilmpCourseGridVO.CourseType;
                            dtCurrentTable.Rows[i]["Column5"] = ilmpCourseGridVO.Title;
                            dtCurrentTable.Rows[i]["Column6"] = ilmpCourseGridVO.Credits.ToString();
                            dtCurrentTable.Rows[i]["Column7"] = ilmpCourseGridVO.Level.ToString();
                            dtCurrentTable.Rows[i]["Column8"] = ilmpCourseGridVO.Prerequisites;
                        }
                        else
                        {
                            dtCurrentTable.Rows[i]["Column5"] = courseVO.Title;
                            dtCurrentTable.Rows[i]["Column6"] = courseVO.Credits.ToString();
                            dtCurrentTable.Rows[i]["Column7"] = courseVO.Level.ToString();
                            dtCurrentTable.Rows[i]["Column8"] = courseVO.Prerequisites.AllPrerequisites;
                        }
                    }
                    else
                    {
                        //extract the DropDownList Selected Items
                        DropDownList ddl1 = (DropDownList)gvCourse.Rows[i].Cells[0].FindControl("ddSemester");
                        DropDownList ddl2 = (DropDownList)gvCourse.Rows[i].Cells[1].FindControl("ddYear");
                        DropDownList ddl3 = (DropDownList)gvCourse.Rows[i].Cells[2].FindControl("ddCourseCode");
                        // Update the DataRow with the DDL Selected Items
                        if (ddl1.SelectedItem != null )
                        {
                            dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                        }
                        if (ddl2.SelectedItem != null )
                        {
                            dtCurrentTable.Rows[i]["Column2"] = ddl2.SelectedItem.Text;
                        }
                        if (ddl3.SelectedItem != null )
                        {
                            dtCurrentTable.Rows[i]["Column3"] = ddl3.SelectedItem.Text;
                        }
                        dtCurrentTable.Rows[i]["Column4"] = ((DropDownList)gvCourse.Rows[i].Cells[3].FindControl("ddCourseType")).Text;
                        dtCurrentTable.Rows[i]["Column5"] = ((Label)gvCourse.Rows[i].Cells[4].FindControl("lblCourseName")).Text;
                        dtCurrentTable.Rows[i]["Column6"] = ((Label)gvCourse.Rows[i].Cells[5].FindControl("lblCredits")).Text;
                        dtCurrentTable.Rows[i]["Column7"] = ((Label)gvCourse.Rows[i].Cells[6].FindControl("lbllevel")).Text;
                        dtCurrentTable.Rows[i]["Column8"] = ((Label)gvCourse.Rows[i].Cells[7].FindControl("lblPrerequisites")).Text;
                    }
                   
                }
                //Store the current data to ViewState
                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
    }

    private void AddNewRowToCourseGrid()
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

                for (int i = 0; i < dtCurrentTable.Rows.Count-1 ; i++)
                {                   

                        //extract the DropDownList Selected Items
                        DropDownList ddl1 = (DropDownList)gvCourse.Rows[i].Cells[0].FindControl("ddSemester");
                        DropDownList ddl2 = (DropDownList)gvCourse.Rows[i].Cells[1].FindControl("ddYear");
                        DropDownList ddl3 = (DropDownList)gvCourse.Rows[i].Cells[2].FindControl("ddCourseCode");

                        // Update the DataRow with the DDL Selected Items
                        if (ddl1.SelectedItem != null)
                        {
                            dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                        }
                        if (ddl2.SelectedItem != null)
                        {
                            dtCurrentTable.Rows[i]["Column2"] = ddl2.SelectedItem.Text;
                        }
                        if (ddl3.SelectedItem != null)
                        {
                            dtCurrentTable.Rows[i]["Column3"] = ddl3.SelectedItem.Text;
                        }
                        dtCurrentTable.Rows[i]["Column4"] = ((DropDownList)gvCourse.Rows[i].Cells[3].FindControl("ddCourseType")).Text;
                        dtCurrentTable.Rows[i]["Column5"] = ((Label)gvCourse.Rows[i].Cells[4].FindControl("lblCourseName")).Text;
                        dtCurrentTable.Rows[i]["Column6"] = ((Label)gvCourse.Rows[i].Cells[5].FindControl("lblCredits")).Text;
                        dtCurrentTable.Rows[i]["Column7"] = ((Label)gvCourse.Rows[i].Cells[6].FindControl("lbllevel")).Text;
                        dtCurrentTable.Rows[i]["Column8"] = ((Label)gvCourse.Rows[i].Cells[7].FindControl("lblPrerequisites")).Text;
                    
                }
                //Rebind the Grid with the current data
                gvCourse.DataSource = dtCurrentTable;
                gvCourse.DataBind();
                gvCourse.Rows[0].Cells[0].Width = 30;
                DropDownList ddl4 = (DropDownList)gvCourse.Rows[dtCurrentTable.Rows.Count - 1].Cells[2].FindControl("ddCourseCode");
                FillCourseCodeDropDown(ddl4);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('ViewState is null ');", true);
           // Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataToCourseGrid();
    }
    private void SetPreviousDataToCourseGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks                   
                    DropDownList ddl1 = (DropDownList)gvCourse.Rows[rowIndex].Cells[0].FindControl("ddSemester");
                    FillSemesterDropDown(ddl1);
                    DropDownList ddl2 = (DropDownList)gvCourse.Rows[rowIndex].Cells[1].FindControl("ddYear");
                    FillYearDropDown(ddl2, int.Parse(ddl1.SelectedItem.Text));
                    DropDownList ddl3 = (DropDownList)gvCourse.Rows[rowIndex].Cells[2].FindControl("ddCourseCode");
                   // FillCourseCodeDropDown(ddl3);
                    if (i <= dt.Rows.Count - 1)
                    {
                        ddl1.ClearSelection();
                        string semester = dt.Rows[i]["Column1"].ToString();
                        if (semester != "")
                        {
                            ddl1.Items.FindByText(semester).Selected = true;
                        }
                        
                        ddl2.ClearSelection();
                        string year = dt.Rows[i]["Column2"].ToString();
                        if (year != "")
                        {
                            ddl2.Items.FindByText(year).Selected = true;
                        }

                        if (semester != "Select" && year != "Select" && semester != "" && year != "")
                        {
                            FillCourseCodeDropDown(ddl3, int.Parse(semester), int.Parse(year));
                           
                        }
                        else
                        {
                            FillCourseCodeDropDown(ddl3);

                        }
                        ddl3.ClearSelection();
                        if (dt.Rows[i]["Column3"].ToString() != "")
                        {
                            ddl3.Items.FindByText(dt.Rows[i]["Column3"].ToString()).Selected = true;
                        }
                        ((DropDownList)gvCourse.Rows[i].Cells[3].FindControl("ddCourseType")).Text = dt.Rows[i]["Column4"].ToString();
                        ((Label)gvCourse.Rows[i].Cells[4].FindControl("lblCourseName")).Text = dt.Rows[i]["Column5"].ToString();
                        ((Label)gvCourse.Rows[i].Cells[5].FindControl("lblCredits")).Text = dt.Rows[i]["Column6"].ToString();
                        ((Label)gvCourse.Rows[i].Cells[6].FindControl("lbllevel")).Text = dt.Rows[i]["Column7"].ToString();
                        ((Label)gvCourse.Rows[i].Cells[7].FindControl("lblPrerequisites")).Text = dt.Rows[i]["Column8"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void ddSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).NamingContainer as GridViewRow;
        DropDownList ddl1 = row.FindControl("ddCourseCode") as DropDownList;
        DropDownList ddl2 = row.FindControl("ddSemester") as DropDownList;
        DropDownList ddl3 = row.FindControl("ddYear") as DropDownList;
        string semester = ddl2.SelectedValue;

        if (semester != null && semester != "Select" && ddl3.SelectedItem.Text != null && ddl3.SelectedItem.Text != "Select")
        {
            FillCourseCodeDropDown(ddl1, Int32.Parse(semester), Int32.Parse(ddl3.SelectedItem.Text));
        }
        else if (semester != null && semester != "Select")
        {
            FillYearDropDown(ddl3, int.Parse(semester));
        }
    }
    protected void ddYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).NamingContainer as GridViewRow;
        DropDownList ddl1 = row.FindControl("ddCourseCode") as DropDownList;
        DropDownList ddl2 = row.FindControl("ddSemester") as DropDownList;
        DropDownList ddl3 = row.FindControl("ddYear") as DropDownList;
        string semester = ddl2.SelectedValue;
        if (semester != null && semester != "Select" && ddl3.SelectedItem.Text != null && ddl3.SelectedItem.Text != "Select")
        {
            FillCourseCodeDropDown(ddl1, Int32.Parse(semester), Int32.Parse(ddl3.SelectedItem.Text));
        }
    }
    private void ResetCourseGridAfterDeletion()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks                   
                    DropDownList ddl1 = (DropDownList)gvCourse.Rows[rowIndex].Cells[0].FindControl("ddSemester");
                    DropDownList ddl2 = (DropDownList)gvCourse.Rows[rowIndex].Cells[1].FindControl("ddYear");
                    DropDownList ddl3 = (DropDownList)gvCourse.Rows[rowIndex].Cells[2].FindControl("ddCourseCode");

                    FillSemesterDropDown(ddl1);
                    FillYearDropDown(ddl2, int.Parse(ddl1.SelectedItem.Text));

                    ddl1.ClearSelection();
                    string semester = dt.Rows[i]["Column1"].ToString();
                    if (semester == "")
                        semester = "1";
                    ddl1.Items.FindByText(semester).Selected = true;

                    ddl2.ClearSelection();
                    string year = dt.Rows[i]["Column2"].ToString();
                    if (year == "")
                        year = "Select";
                    ddl2.Items.FindByText(year).Selected = true;

                    if (semester != "Select" && year != "Select")
                    {
                        FillCourseCodeDropDown(ddl3, int.Parse(semester), int.Parse(year));
                        ddl3.ClearSelection();
                        if (dt.Rows[i]["Column3"].ToString() != "")
                        {
                            ddl3.Items.FindByText(dt.Rows[i]["Column3"].ToString()).Selected = true;
                        }
                    }
                   
                    ((DropDownList)gvCourse.Rows[i].Cells[3].FindControl("ddCourseType")).Text = dt.Rows[i]["Column4"].ToString();
                    ((Label)gvCourse.Rows[i].Cells[4].FindControl("lblCourseName")).Text = dt.Rows[i]["Column5"].ToString();
                    ((Label)gvCourse.Rows[i].Cells[5].FindControl("lblCredits")).Text = dt.Rows[i]["Column6"].ToString();
                    ((Label)gvCourse.Rows[i].Cells[6].FindControl("lbllevel")).Text = dt.Rows[i]["Column7"].ToString();
                    ((Label)gvCourse.Rows[i].Cells[7].FindControl("lblPrerequisites")).Text = dt.Rows[i]["Column8"].ToString();
                    
                    rowIndex++;
                }
            }
        }
    }

    private void AddNewRowToWorkshopGrid()
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
                    //extract the DropDownList Selected Items
                    DropDownList ddl1 = (DropDownList)gvWorkshop.Rows[i].Cells[0].FindControl("ddWorkshop");

                    // Update the DataRow with the DDL Selected Items
                    if (ddl1.SelectedItem != null)
                    {
                        dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    }               
                }

                //Rebind the Grid with the current data
                gvWorkshop.DataSource = dtCurrentTable;
                gvWorkshop.DataBind();
                gvWorkshop.Rows[0].Cells[0].Width = 70;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('ViewState is null ');", true);
           // Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataToWorkshopGrid();
    }
    private void SetPreviousDataToWorkshopGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableWorkshop"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableWorkshop"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks                   
                    DropDownList ddl1 = (DropDownList)gvWorkshop.Rows[rowIndex].Cells[0].FindControl("ddWorkshop");
                    FillWorkshopDropDown(ddl1);
                    if (i < dt.Rows.Count - 1)
                    {
                        ddl1.ClearSelection();
                        if (dt.Rows[i]["Column1"].ToString() != null)
                        {
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                        }                        
                    }
                    rowIndex++;
                }
            }
        }
    }

    private void ResetWorkshopGridAfterDeletion()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableWorkshop"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableWorkshop"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks                   
                    DropDownList ddl1 = (DropDownList)gvWorkshop.Rows[rowIndex].Cells[0].FindControl("ddWorkshop");
                    FillWorkshopDropDown(ddl1);
                   
                    ddl1.ClearSelection();
                    if (dt.Rows[i]["Column1"].ToString() != null)
                    {
                        ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                    }                   
                    rowIndex++;
                }
            }
        }       
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        AddNewRowToCourseGrid();
    }
    
    protected void gvCourse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       /* if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "cursor:pointer;");
            e.Row.Attributes.Add("onmouseover", "this.style.textDecoration='underline';");
            e.Row.Attributes.Add("onmouseout", "this.style.textDecoration='none';");
            e.Row.Attributes.Add("onClick", Page.ClientScript.GetPostBackClientHyperlink(this.gvCourse, "Select$" + e.Row.RowIndex));
        }   */
       if (e.Row.RowType == DataControlRowType.DataRow)
        {
          //  e.Row.Attributes.Add("style", "cursor:pointer;");
           // e.Row.Attributes.Add("onmouseover", "this.style.textDecoration='underline';");
            //e.Row.Attributes.Add("onmouseout", "this.style.textDecoration='none';");           
        }
    }
    protected void btnDeleteCourse_Click(object sender, EventArgs e)
    {        
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        if (gvCourse.SelectedIndex != -1)
        {
            if (dt.Rows.Count == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('There should be atleast 1 row in the template ');", true);
             //   Response.Write("<script>alert(' There should be atleast 1 row in the template');</script>");
            }
            else
            {
                dt.Rows[gvCourse.SelectedIndex].Delete();
                //Bind the DataTable to the Grid
                gvCourse.DataSource = dt;
                gvCourse.DataBind();
                gvCourse.Rows[0].Cells[0].Width = 40;
                ViewState["CurrentTable"] = dt;
               // SetPreviousDataToCourseGrid();
                ResetCourseGridAfterDeletion();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please select a row in course to delete ');", true);
            //Response.Write("<script>alert(' Please select a row in course to delete');</script>");
        }           
    }
    protected void btnAddWorkshop_Click(object sender, EventArgs e)
    {
        AddNewRowToWorkshopGrid();
    }
    protected void btnDeleteWorkshop_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["CurrentTableWorkshop"];
        if (gvWorkshop.SelectedIndex != -1)
        {
            if (dt.Rows.Count == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('There should be atleast 1 row in the template ');", true);
                //Response.Write("<script>alert(' There should be atleast 1 row in the template');</script>");
            }
            else
            {
                dt.Rows[gvWorkshop.SelectedIndex].Delete();
                //Bind the DataTable to the Grid
                gvWorkshop.DataSource = dt;
                gvWorkshop.DataBind();
                gvWorkshop.Rows[0].Cells[0].Width = 70;
                ResetWorkshopGridAfterDeletion();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please select a row in course to delete ');", true);
            //Response.Write("<script>alert(' Please select a row in workshop to delete ');</script>");
        }
    }
    protected void btnSaveTemplate_Click(object sender, EventArgs e)
    {
        ILMPTemplateVO ilmpTemplateVO = new ILMPTemplateVO();
        ilmpTemplateVO.ProgrammeId = ddProgramme.SelectedItem.Value;
        ilmpTemplateVO.MajorId = ddMajor.SelectedItem.Value;
        ilmpTemplateVO.TemplateName = txtTemplateName.Text;
        if (rbtnlTemplateType.SelectedItem.Text == "Custom" && txtStudentId.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please enter studentid');", true);
            return;
        }
        if (txtStudentId.Text != "")
        {
            ilmpTemplateVO.StudentId = int.Parse(txtStudentId.Text);
        }
        List<TemplateCourseVO> templateCourses = new List<TemplateCourseVO>();
        if (ViewState["CurrentTable"] != null)
        {
            TemplateCourseVO templateCourseVO;
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {                    
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    DropDownList ddl1 = (DropDownList)gvCourse.Rows[i].Cells[0].FindControl("ddSemester");
                    DropDownList ddl2 = (DropDownList)gvCourse.Rows[i].Cells[1].FindControl("ddYear");
                    DropDownList ddl3 = (DropDownList)gvCourse.Rows[i].Cells[2].FindControl("ddCourseCode");
                    DropDownList ddl4 = (DropDownList)gvCourse.Rows[i].Cells[3].FindControl("ddCourseType");
                  //  int sequenceNumber = i + 1;
                    if (ddl2.SelectedValue == "-1")
                    {
                        templateCourseVO = new TemplateCourseVO(ddl3.SelectedItem.Value, ddl4.SelectedItem.Value);
                    }
                    else
                    {
                        string yearstr = ddl2.SelectedItem.Value;
                        if (yearstr == "Select")
                        {
                            yearstr = "0";
                        }
                        templateCourseVO = new TemplateCourseVO(ddl3.SelectedItem.Value, ddl4.SelectedItem.Value, int.Parse(ddl1.SelectedItem.Value),int.Parse(yearstr),0);
                    }
                    
                    templateCourses.Add(templateCourseVO);
                }
            }
        }
        if (ViewState["CurrentTableWorkshop"] != null)
        {
            TemplateCourseVO templateCourseVO;
            DataTable dt = (DataTable)ViewState["CurrentTableWorkshop"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {                    
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    DropDownList ddl1 = (DropDownList)gvWorkshop.Rows[i].Cells[0].FindControl("ddWorkshop");
                    string workshopId = ddl1.SelectedItem.Value;
                    templateCourseVO = new TemplateCourseVO("","", int.Parse(workshopId)); 
                    templateCourses.Add(templateCourseVO);
                }                
            }
        }
        ilmpTemplateVO.TemplateCourses = templateCourses;
        PrerequisiteBO prerequiisteBO = new PrerequisiteBO();
        # region prerequisite check
        string errormessage = "";
        // fetching each course given in grid
        foreach (TemplateCourseVO tempcourseVO in templateCourses)
        {
            string validationCourseCode = tempcourseVO.CourseCode;
            int validationSemester = tempcourseVO.Semester;
            int validationYear = tempcourseVO.Year;
            string validationAllPrequisite = prerequiisteBO.GetAllPrerequisiteForCourseCode(tempcourseVO.CourseCode);
            List<KeyValuePair<string,string>> prequisiteHash = new List<KeyValuePair<string,string>>();
            String corequisite = "";
            List<string> prerequisuitesList = new List<string>();
            // get list of prerequisite and course code
            if (validationAllPrequisite.Contains("&") && !validationAllPrequisite.Contains("#")) // multiple prerequisites, no corequisite
            {
                string[] reqArr = validationAllPrequisite.Split('&');
                foreach (string requisite in reqArr)
                {
                    prequisiteHash.Add(new KeyValuePair<string, string>(validationCourseCode+","+requisite,"notfound"));
                    prerequisuitesList.Add(requisite);
                }                
            }
            else if (validationAllPrequisite.Contains("#") && validationAllPrequisite.Length <= 6)//one co-rerequite 
            {
                corequisite = validationAllPrequisite.Substring(1);// remove # from prerequisite
            }
            else if (validationAllPrequisite.Contains("#") & !validationAllPrequisite.Contains("&") && validationAllPrequisite.Length <= 11)  // one co-requisite and one prequisite               
            {
                string[] reqArr = validationAllPrequisite.Split('#');
                prequisiteHash.Add(new KeyValuePair<string, string>(validationCourseCode + "," + reqArr[0], "notfound"));
                corequisite = reqArr[1];
                prerequisuitesList.Add(reqArr[0]);
            }
            else if (validationAllPrequisite.Contains("&") & validationAllPrequisite.Contains("#")) // multiple prerequisite & one corequisite
            {
                corequisite = validationAllPrequisite.Substring(validationAllPrequisite.IndexOf("#") + 1);
                string prereq = validationAllPrequisite.Substring(0, validationAllPrequisite.IndexOf("#"));
                string[] reqArr = prereq.Split('&');                
                foreach (string req in reqArr)
                {
                    prequisiteHash.Add(new KeyValuePair<string, string>(validationCourseCode + "," + req, "notfound"));
                    prerequisuitesList.Add(req);
                }
            }
            else if (!validationAllPrequisite.Contains("&") & !validationAllPrequisite.Contains("#") && validationAllPrequisite.Length<=5) // one prerequisite
            {
                prequisiteHash.Add(new KeyValuePair<string, string>(validationCourseCode + "," + validationAllPrequisite, "notfound"));
                prerequisuitesList.Add(validationAllPrequisite); 
            }
            //
            if (templateCourses.Count >= 1)
            {
                if (validationAllPrequisite != "" && validationYear != 0)
                {
                    Boolean corequisiteFound = false;                  
                    //  check for prerequisite present in all courses
                    for (int i = 0; i < templateCourses.Count; i++)
                    {
                        string courseCode1 = templateCourses[i].CourseCode;
                        if (validationAllPrequisite.Contains(courseCode1) && courseCode1!="")
                        {
                            // set prerequisite found in hash                            
                           
                            string requisiteType = "";
                           if(corequisite==courseCode1)
							{
							    corequisiteFound =true;
                                requisiteType = "co";
							}
							else if(prerequisuitesList.Contains(courseCode1))
                            {
                                prequisiteHash.Remove(new KeyValuePair<String, String>(validationCourseCode + "," + courseCode1, "notfound"));
                                prequisiteHash.Add(new KeyValuePair<String, String>(validationCourseCode + "," + courseCode1, "found"));
                                requisiteType="pre";
						    }

                            if(templateCourses[i].Year < validationYear)
							{
							}
							else if(templateCourses[i].Year > validationYear)// // preerquisite is done after course by year - show error message
							{
								if(requisiteType == "pre")
								{
									errormessage = "Prerequisite not completed  for course "+validationCourseCode;
								}
								else if (requisiteType == "co")
								{
									errormessage = "Corequisite should be done in current or previous semester for course "+validationCourseCode;
								}
							}
							else if(templateCourses[i].Year == validationYear) // preerquisite and course are in same year
							{
								if(templateCourses[i].Semester==validationSemester)
								{
									if(requisiteType == "pre")
									{
										errormessage = "Prerequisite not completed  for course "+validationCourseCode;
									}
									else if (requisiteType == "co")
									{// valid corequisite semester and year
									}
								}
                                else if(templateCourses[i].Semester<validationSemester)
                                {
                                    // valid semester and year for co and prerequisites
                                }
                                else if (templateCourses[i].Semester > validationSemester)
                                {
                                    if(requisiteType == "pre")
								{
									errormessage = "Prerequisite not completed  for course "+validationCourseCode;
								}
								else if (requisiteType == "co")
								{
									errormessage = "Corequisite should be done in current or previous semester for course "+validationCourseCode;
								}
                                }
							}							                               
                        }                       
                    }
                    // if courses not found give error message
                    foreach (KeyValuePair<string, string> prerequisite1 in prequisiteHash)
                    {
                        if (prerequisite1.Value == "notfound")
                        {
                            string[] codes = prerequisite1.Key.Split(',');
                            errormessage += "Prerequisite " + codes[1] + " not selected in Ilmp for course " + validationCourseCode;
                        }
                    }
                    if (corequisite != "" && !corequisiteFound)
                    {
                        errormessage += "Corequisite not selected in Ilmp for course " + validationCourseCode;
                    }
                }
            }
        }
        if (errormessage != "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + errormessage + "');", true);
            return;
        }
        #endregion                
        try
        {
            string status = ilmpTemplateBO.AddILMPTemplate(ilmpTemplateVO);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + status + "');", true);
           // Response.Write("<script>alert('" + status + "');</script>");
            if (status.Contains("success"))
            {
               // btnSaveTemplate.Enabled = false;
            }
        }
        catch (CustomException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
           // Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
            Response.Write("<script>alert('" + ex.Message + "');</script>");
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
                txtStudentName.Text = studentName;
                if (studentName == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please enter a valid student id');", true);
                    //Response.Write("<script>alert(' Please enter a valid student id ');</script>");
                }
                else
                {

                }
            }
            catch (ParseException)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please enter a valid student id ');", true);
               // Response.Write("<script>alert(' Please enter a valid student id ');</script>");
            }
            catch (CustomException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
                // Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Error in getting student details ');", true);
                //Response.Write("<script>alert(' Error in getting student details ');</script>");
                ExceptionUtility.LogException(ex, "Error Page");
            }
        }
    }
    protected void ddWorkshop_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((DropDownList)sender).NamingContainer as GridViewRow;
        DropDownList ddl = row.FindControl("ddWorkshop") as DropDownList;
        string selectedCourseCode = ddl.SelectedItem.Text;
        if (ViewState["CurrentTableWorkshop"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTableWorkshop"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                     DropDownList ddl1 = (DropDownList)gvWorkshop.Rows[i].Cells[0].FindControl("ddWorkshop");
                     if (ddl1.SelectedItem != null)
                     {
                         dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                     }
                }
                //Store the current data to ViewState
                ViewState["CurrentTableWorkshop"] = dtCurrentTable;
            }
        }
    }    
}