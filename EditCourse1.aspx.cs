using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditCourse1 : System.Web.UI.Page
{
    CourseBO courseBO = new CourseBO();
    PrerequisiteBO prerequisiteBO = new PrerequisiteBO();
    CourseProgrammeBO courseProgrammeBO = new CourseProgrammeBO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String coursecode = Request.QueryString["coursecode"];
            hfCourseCode.Value = coursecode;
            SetInitialValues();
            CourseVO courseVO = courseBO.GetCourseDetailsForCourseCode(coursecode);
            txtCourseCode.Text = courseVO.CourseCode;
            txtOfferedFrequency.Text = courseVO.OfferedFrequency;
            txtTitle.Text = courseVO.Title;
            ddlLevel.SelectedValue = courseVO.Level.ToString();
            ddlCredits.SelectedValue = courseVO.Credits.ToString();
            ddActive.SelectedValue = courseVO.Active;
            string allPrequisites = "";
            allPrequisites = courseVO.Prerequisites.AllPrerequisites;
            // seperating the prerequisites and corequisite if it is present for the selected course
            if (allPrequisites != "")
            {
               // hfPrerequisite.Value = allPrequisites;
                string prerequisites = "";
                if (allPrequisites.Contains("#"))
                {
                    int pos = allPrequisites.IndexOf("#");
                    string corequisite = allPrequisites.Substring(pos + 1, 5);// 5 comes from length of any corequisite coursecode which is 5 
                    ddCorequisite.SelectedValue = corequisite;
                    if (allPrequisites.Length > 6)
                    {
                        prerequisites = allPrequisites.Substring(0, pos);                       
                    }
                }
                else
                {
                    prerequisites = allPrequisites;
                }
                if (prerequisites != "")
                {
                    if (prerequisites.Contains("&"))
                    {
                        string[] prerequisiteArr = prerequisites.Split('&');
                        if (prerequisiteArr.Length > 0)
                        {
                            for (int count = 0; count < prerequisiteArr.Length; count++)
                            {
                                lbPrerequisite.Items.Add(prerequisiteArr[count]);
                            }
                        }
                    }
                    else
                    {
                        lbPrerequisite.Items.Add(prerequisites);
                        ddPrerequisite.SelectedItem.Value = prerequisites;
                    }
                }
            }
            List<CourseProgrammeVO> courseProgrammeList = courseProgrammeBO.GetCourseProgrammeForCourseCode(coursecode);
            foreach (CourseProgrammeVO coursePgmVO in courseProgrammeList)
            {
                if (coursePgmVO.ProgrammeId == "BIT")
                {
                    rbtnBIT.Checked = true;
                    cblBITMajor.Items.FindByValue(coursePgmVO.MajorId).Selected = true;
                }
                else if (coursePgmVO.ProgrammeId == "GDIT")
                {
                    rbtnGDIT.Checked = true;
                    cblGDITMajor.Items.FindByValue(coursePgmVO.MajorId).Selected = true;
                }
            }
        }
    }
    private void SetInitialValues()
    {
        FillPrerequisiteDropDown();
        FillCorequisiteDropDown();
    }
      private void FillPrerequisiteDropDown()
      {
          PrerequisiteBO prerequisiteBO = new PrerequisiteBO();
          DataSet ds = prerequisiteBO.GetAllPrerequisites();
          ddPrerequisite.DataSource = ds;
          ddPrerequisite.DataTextField = "PrerequisiteCode";
          ddPrerequisite.DataValueField = "PrerequisiteCode";
          ddPrerequisite.DataBind();
          ddPrerequisite.Items.Insert(0, "Select");
      }
      
    private void FillCorequisiteDropDown()
    {
        PrerequisiteBO prerequisiteBO = new PrerequisiteBO();
        DataSet ds = prerequisiteBO.GetAllCorequisites();
        ddCorequisite.DataSource = ds;
        ddCorequisite.DataTextField = "PrerequisiteCode";
        ddCorequisite.DataValueField = "PrerequisiteCode";
        ddCorequisite.DataBind();
        ddCorequisite.Items.Insert(0, "Select");
    }
    private void EnablePgmMajor()
    {
        if (rbtnGDIT.Checked)
        {
            cblGDITMajor.Enabled = true;
        }
        else
        {
            cblGDITMajor.Enabled = false;
            foreach (ListItem li in cblGDITMajor.Items)
            {
                li.Selected = false;
            }
        }
        if (rbtnBIT.Checked)
        {
            cblBITMajor.Enabled = true;
        }
        else
        {
            cblBITMajor.Enabled = false;
            foreach (ListItem li in cblBITMajor.Items)
            {
                li.Selected = false;
            }
        }
    }
    protected void btnEditPrerequisite_Click(object sender, EventArgs e)
    {
     /*   pup.ShowPopupWindow();
        SetInitialValuesInPrerequisite();
        if (hfPrerequisite.Value.Trim() != "")
        {
            string prerequisite = hfPrerequisite.Value.Trim().Substring(1);
            string[] prerequisites = Regex.Split(hfPrerequisite.Value.Trim(), "&");
            foreach (var prereq in prerequisites)
            {
                if (prereq != "")
                    AddNewRowToPrerequisiteGrid(prereq);
            }
        }*/
    }

   /* private void SetInitialValuesInPrerequisite()
    {
        //Add an empty row in Workshop grid
        DataTable dt = new DataTable();
        DataRow dr = null;
        //Define the Columns
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));

        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        //Bind the DataTable to the Grid
        gvPrerequisite.DataSource = dt;
        gvPrerequisite.DataBind();
        gvPrerequisite.Rows[0].Cells[0].Width = 30;
        DropDownList ddl1 = (DropDownList)gvPrerequisite.Rows[0].Cells[0].FindControl("ddPopupPrerequisite");
        FillPrerequisiteDropDown(ddl1);
    }
    private void AddNewRowToPrerequisiteGrid()
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
                    //extract the DropDownList Selected Items
                    DropDownList ddl1 = (DropDownList)gvPrerequisite.Rows[i].Cells[0].FindControl("ddPopupPrerequisite");

                    // Update the DataRow with the DDL Selected Items
                    if (ddl1.SelectedItem != null)
                    {
                        dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;
                    }
                }

                //Rebind the Grid with the current data
                gvPrerequisite.DataSource = dtCurrentTable;
                gvPrerequisite.DataBind();
                gvPrerequisite.Rows[0].Cells[0].Width = 30;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('ViewState is null ');", true);
        }
        //Set Previous Data on Postbacks
        SetPreviousDataToPrerequisiteGrid();
    }
    private void AddNewRowToPrerequisiteGrid(string prereq)
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
                        //extract the DropDownList Selected Items
                        DropDownList ddl1 = (DropDownList)gvPrerequisite.Rows[i].Cells[0].FindControl("ddPopupPrerequisite");

                        // Update the DataRow with the DDL Selected Items
                        if (ddl1.SelectedItem != null)
                        {
                            dtCurrentTable.Rows[i]["Column1"] = prereq;
                        }
                    }
                }
                //Rebind the Grid with the current data
                gvPrerequisite.DataSource = dtCurrentTable;
                gvPrerequisite.DataBind();
                gvPrerequisite.Rows[0].Cells[0].Width = 30;
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('ViewState is null ');", true);
        }
        //Set Previous Data on Postbacks
        SetPreviousDataToPrerequisiteGrid();
    }
    private void SetPreviousDataToPrerequisiteGrid()
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
                    DropDownList ddl1 = (DropDownList)gvPrerequisite.Rows[rowIndex].Cells[0].FindControl("ddPopupPrerequisite");
                    FillPrerequisiteDropDown(ddl1);
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
    }*/
    private void FillPrerequisiteDropDown(DropDownList ddl)
    {
        PrerequisiteBO prerequisiteBO = new PrerequisiteBO();
        DataSet ds = prerequisiteBO.GetAllPrerequisites();
        ddl.DataSource = ds;
        ddl.DataTextField = "PrerequisiteCode";
        ddl.DataValueField = "PrerequisiteCode";
        ddl.DataBind();
        ddl.Items.Insert(0, "Select");
    }
   /* protected void MycloseWindow(object sender, EventArgs e)
    {
        string allPrerequisite = "";
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    DropDownList ddl1 = (DropDownList)gvPrerequisite.Rows[i].Cells[0].FindControl("ddPopupPrerequisite");
                    allPrerequisite += "&" + ddl1.SelectedItem.Value;
                }
            }
            hfPrerequisite.Value = allPrerequisite;
        }
    }*/
   /* protected void btnAddPrerequisite_Click(object sender, EventArgs e)
    {
        AddNewRowToPrerequisiteGrid();
    }
    protected void btnDeletePrerequisite_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        if (gvPrerequisite.SelectedIndex != -1)
        {
            if (dt.Rows.Count == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('There should be atleast 1 row in prerequisite');", true);
            }
            else
            {
                dt.Rows[gvPrerequisite.SelectedIndex].Delete();
                //Bind the DataTable to the Grid
                gvPrerequisite.DataSource = dt;
                gvPrerequisite.DataBind();
                gvPrerequisite.Rows[0].Cells[0].Width = 70;
                ResetPrerequisiteGridAfterDeletion();
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please select a row in prerequisite to delete ');", true);
        }
    }
    protected void btnSavePrerequisite_Click(object sender, EventArgs e)
    {
        string allPrerequisite = "";
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Set the Previous Selected Items on Each DropDownList on Postbacks
                    DropDownList ddl1 = (DropDownList)gvPrerequisite.Rows[i].Cells[0].FindControl("ddPopupPrerequisite");
                    allPrerequisite += "&" + ddl1.SelectedItem.Value;
                }
            }
            hfPrerequisite.Value = allPrerequisite;
            if (allPrerequisite.StartsWith("&"))
            {
                allPrerequisite = allPrerequisite.Substring(1);
                txtPrerequisite.Text = allPrerequisite;
            }
        }
    }
    private void ResetPrerequisiteGridAfterDeletion()
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
                    DropDownList ddl1 = (DropDownList)gvPrerequisite.Rows[rowIndex].Cells[0].FindControl("ddPopupPrerequisite");
                    FillPrerequisiteDropDown(ddl1);

                    ddl1.ClearSelection();
                    if (dt.Rows[i]["Column1"].ToString() != null)
                    {
                        ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                    }
                    rowIndex++;
                }
            }
        }
    }*/
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CourseList.aspx");
    }
    protected void rbtnGDIT_CheckedChanged(object sender, EventArgs e)
    {
        EnablePgmMajor();
    }
    protected void rbtnBIT_CheckedChanged(object sender, EventArgs e)
    {
        EnablePgmMajor();
    }
    protected void btnUpdateCourse_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCourseCode.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please enter course code');", true);
                return;
            }
            else if (txtTitle.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please enter title');", true);
                return;
            }
            string courseCode = txtCourseCode.Text;
            string title = txtTitle.Text;
            int credits = Convert.ToInt32(ddlCredits.Text);
            int level = Convert.ToInt32(ddlLevel.Text);
            string offeredFrequency = txtOfferedFrequency.Text;
            string active = ddActive.SelectedItem.Text;
            DateTime updatedDTM = DateTime.Now;
            string prerequisite = "";
            List<CourseProgrammeVO> coursePgm = new List<CourseProgrammeVO>();
            string originalCourseCode = hfCourseCode.Value;
            CourseVO courseVO = new CourseVO(courseCode, title, credits, level, offeredFrequency, active, updatedDTM);
            // get listbox items and set in hidden field
            if (lbPrerequisite.Items.Count > 0)
            {
                string tempprequisite = "";
                for (int i = 0; i < lbPrerequisite.Items.Count; i++)
                {
                    tempprequisite += lbPrerequisite.Items[i].Value;
                    if (i != lbPrerequisite.Items.Count - 1)
                    {
                        tempprequisite += "&";
                    }
                }
                hfPrerequisite.Value = tempprequisite;
            }
            if (ddCorequisite.SelectedItem.Text != "Select")
            {
                hfPrerequisite.Value += "#" + ddCorequisite.SelectedItem.Text;
            }

            string allPrequisite = hfPrerequisite.Value;
          /*  if (hfPrerequisite.Value.Trim() == "" && ddCorequisite.SelectedItem.Text == "Select")
            {
                // both prerequisite and corerequisite not selected
            }
            else if (hfPrerequisite.Value.Trim().Length == 6 && hfPrerequisite.Value.Trim().StartsWith("#") && ddCorequisite.SelectedItem.Text == "Select")
            {
                // only corerequiiste was present previously and is removed now
                hfPrerequisite.Value = "";
                allPrequisite = "";
            }
            else if ((hfPrerequisite.Value.Trim() != "" && ddCorequisite.SelectedItem.Text != "Select")
                    || (hfPrerequisite.Value.Trim() != "Select" && ddCorequisite.SelectedItem.Text != "Select"))
            {
                // both prerequisite and corequisite are selected
                allPrequisite = hfPrerequisite.Value;
                allPrequisite += "#" + ddCorequisite.SelectedItem.Text;
            }
            else if (hfPrerequisite.Value.Trim() != "Select" && ddCorequisite.SelectedItem.Text == "Select")
            {
                // only one prerequiiste is selected
                if (hfPrerequisite.Value.StartsWith("&"))
                    allPrequisite = hfPrerequisite.Value.Substring(1);
                else
                    allPrequisite = hfPrerequisite.Value;
            }
            else if (ddCorequisite.SelectedItem.Text != "Select" && (hfPrerequisite.Value.Trim() == "" || hfPrerequisite.Value.Trim() == "Select"))
            {
                // only corequisiteis selected
                allPrequisite += "#" + ddCorequisite.SelectedItem.Text;
            }*/
            if (allPrequisite != "")
            {
                if (allPrequisite.StartsWith("&"))
                    allPrequisite = allPrequisite.Substring(1);
                CoursePrerequisiteVO prerequisiteVO = new CoursePrerequisiteVO(courseCode, prerequisite, "Prerequisite", allPrequisite);
                courseVO.Prerequisites = prerequisiteVO;
            }
            if (rbtnBIT.Checked)
            {
                foreach (ListItem cb in cblBITMajor.Items)
                {
                    if (cb.Selected)
                    {
                        CourseProgrammeVO coursePgmVO = new CourseProgrammeVO();
                        coursePgmVO.CourseCode = courseCode;
                        coursePgmVO.ProgrammeId = rbtnBIT.Text;
                        coursePgmVO.MajorId = cb.Value;
                        coursePgmVO.CourseType = ddCourseType.SelectedItem.Text;
                        coursePgm.Add(coursePgmVO);
                    }
                }
            }
            if (rbtnGDIT.Checked)
            {
                foreach (ListItem cb in cblGDITMajor.Items)
                {
                    if (cb.Selected)
                    {
                        CourseProgrammeVO coursePgmVO = new CourseProgrammeVO();
                        coursePgmVO.CourseCode = courseCode;
                        coursePgmVO.ProgrammeId = rbtnGDIT.Text;
                        coursePgmVO.MajorId = cb.Value;
                        coursePgmVO.CourseType = ddCourseType.SelectedItem.Text;
                        coursePgm.Add(coursePgmVO);
                    }
                }
            }
            if (coursePgm.Count >= 1)
            {
                courseVO.Program = coursePgm;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please select atleast one programme and major');", true);
                return;
            }
            string status = courseBO.UpdateCourse(courseVO, originalCourseCode);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + status + "');", true);
            // Response.Write("<script>alert('" + status + "');</script>");
        }
        catch (CustomException ex)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
            // Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Unhandled Exception:" + ex.Message + "');", true);
            //Response.Write("<script>alert('" + ex.Message + "');</script>");
        }

    }
    protected void btnRemovePrerequisite_Click(object sender, EventArgs e)
    {
        if (lbPrerequisite.SelectedIndex != -1)
        {
            lbPrerequisite.Items.RemoveAt(lbPrerequisite.SelectedIndex);
        }
    }
    protected void btnAddMore_Click1(object sender, EventArgs e)
    {
        lbPrerequisite.Items.Add(this.ddPrerequisite.Text);
    }
}