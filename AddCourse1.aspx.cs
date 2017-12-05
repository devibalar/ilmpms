using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddCourse1 : System.Web.UI.Page
{
    CourseBO courseBO = new CourseBO();
    PrerequisiteBO prerequisiteBO = new PrerequisiteBO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetInitialValues();
        }
    }
    private void SetInitialValues()
    {
       // FillProgrammeDropDown();
       // FillMajorDropDown();
        FillCorequisiteDropDown();
        FillPrerequisiteDropDown();
        EnablePgmMajor();
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

    private void FillCorequisiteDropDown()
    {
        PrerequisiteBO prerequisiteBO = new PrerequisiteBO();
        DataSet ds = prerequisiteBO.GetAllCorequisites();
        ddCorequisite.DataSource = ds;
        ddCorequisite.DataTextField = "PrerequisiteCode";
        ddCorequisite.DataValueField = "PrerequisiteCode";
        ddCorequisite.DataBind();
        ddCorequisite.Items.Insert(0,"Select");
    }

    protected void btnAddCourse_Click(object sender, EventArgs e)
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
            string active = "Yes";
            DateTime createdDTM = DateTime.Now;
            DateTime updatedDTM = DateTime.Now;
            string prerequisite = "";
            List<CourseProgrammeVO> coursePgm = new List<CourseProgrammeVO>();

            CourseVO courseVO = new CourseVO(courseCode, title, credits, level, offeredFrequency, active, createdDTM, updatedDTM);
            // get listbox items and set in hidden field
            if (lbPrerequisite.Items.Count > 0)
            {
                string tempprequisite = "";
                for (int i = 0; i < lbPrerequisite.Items.Count; i++)
                {
                    tempprequisite+= lbPrerequisite.Items[i].Value;
                    if (i != lbPrerequisite.Items.Count-1)
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
            /*if (hfPrerequisite.Value.Trim() == "" && ddCorequisite.SelectedItem.Text == "Select")
            {
            }
            else if (hfPrerequisite.Value.Trim() != "" && ddCorequisite.SelectedItem.Text != "Select")
            {
                //allPrequisite = hfPrerequisite.Value.Substring(1);
                allPrequisite += "#" + ddCorequisite.SelectedItem.Text;
            }
            else if (hfPrerequisite.Value.Trim() != "Select")
            {
                /*if (hfPrerequisite.Value.Length > 5)
                    allPrequisite = hfPrerequisite.Value.Substring(1);
                else*/
                  //  allPrequisite = hfPrerequisite.Value;
           /* }
            else if (ddCorequisite.SelectedItem.Text != "Select")
            {
                allPrequisite += "#" + ddCorequisite.SelectedItem.Text;
            } */
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
            string status = courseBO.AddCourse(courseVO);
            Response.Write("<script>alert('" + status + "');</script>");
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {           
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('Error in adding course');</script>");
        }
    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
      /*  pup.ShowPopupWindow();
        SetInitialValuesInPrerequisite();
        if (hfPrerequisite.Value.Trim() != "")
        {
           string prerequisite = hfPrerequisite.Value.Trim().Substring(1);
            string[] prerequisites = Regex.Split(hfPrerequisite.Value.Trim(), "&");
            foreach(var prereq in prerequisites)
            {
                if (prereq!="")
                AddNewRowToPrerequisiteGrid(prereq);
            }
        }*/
    }
  /*  protected void MycloseWindow(object sender, EventArgs e)
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
    }
    protected void btnAddPrerequisite_Click(object sender, EventArgs e)
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
                //Response.Write("<script>alert(' There should be atleast 1 row in the template');</script>");
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
            //Response.Write("<script>alert(' Please select a row in workshop to delete ');</script>");
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
                    allPrerequisite += "&"+ddl1.SelectedItem.Value;
                }
            }
            hfPrerequisite.Value = allPrerequisite;
        }
    }
    private void SetInitialValuesInPrerequisite()
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
            // Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataToPrerequisiteGrid();
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
            // Response.Write("ViewState is null");
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
    }
    */
    protected void rbtnGDIT_CheckedChanged(object sender, EventArgs e)
    {
        EnablePgmMajor();
    }
    protected void rbtnBIT_CheckedChanged(object sender, EventArgs e)
    {
        EnablePgmMajor();
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
    protected void ddPrerequisite_SelectedIndexChanged(object sender, EventArgs e)
    {
      /*  if (hfPrerequisite.Value == "" && ddPrerequisite.SelectedItem.Text!="Select")
        {
            hfPrerequisite.Value = "&"+ddPrerequisite.SelectedItem.Text;
        }*/
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CourseList.aspx");
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