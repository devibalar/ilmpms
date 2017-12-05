using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Query.Dynamic;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ILMPListStaff : System.Web.UI.Page
{
    ILMPBO ilmpBO = new ILMPBO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            btnEditILMP.Enabled = false;
        }
    }
   
    protected void btnSearchILMP_Click(object sender, EventArgs e)
    {
        string studentIdStr = txtSearchStudentId.Text.Trim();
        if (studentIdStr == "")
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Please enter studentid ');", true);
        }
        else
        {
            int studentId = 0;
            try
            {
                studentId = int.Parse(studentIdStr);
            }
            catch (ParseException)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please enter valid studentid');", true);
                return;
            }
           
           List<ILMPVO> ilmps = ilmpBO.GetILMPListForStudentId(studentId);
           
            //temp
            DataTable dt = new DataTable();
            
            DataRow dr = null;
            //Define the Columns
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));

            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;
            //Bind the DataTable to the Grid

            gvStudentILMPList.DataSource = dt;
            gvStudentILMPList.DataBind();
            gvStudentILMPList.Rows[0].Cells[0].Width = 40;
            if (ilmps.Count > 0)
            {
                btnEditILMP.Enabled = true;
                foreach (ILMPVO ilmp in ilmps)
                {
                    AddNewRowToStudentILMPGrid(ilmp);
                } // to hide the last empty row in grid
                if ((gvStudentILMPList.Rows[gvStudentILMPList.Rows.Count - 1].Cells[0].FindControl("lblStudentId") as Label).Text == "")
                {
                    gvStudentILMPList.Rows[gvStudentILMPList.Rows.Count - 1].Visible = false;
                }
            }
        }
    }
    private void AddNewRowToStudentILMPGrid(ILMPVO ilmpVO)
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
                        // to skip the last empty row in grid from populating data
                    }
                    else
                    {
                        dtCurrentTable.Rows[i]["Column1"] = ilmpVO.StudentId;
                        dtCurrentTable.Rows[i]["Column2"] = ilmpVO.IlmpId;
                        dtCurrentTable.Rows[i]["Column3"] = ilmpVO.Active;
                        string changestatus = "";
                        if (ilmpVO.Active == "Yes")
                        {
                            changestatus = "DeActivate";
                        }
                        else{
                            changestatus = "Activate";
                        }
                        dtCurrentTable.Rows[i]["Column4"] = changestatus;
                    }
                }
                //Rebind the Grid with the current data
                gvStudentILMPList.DataSource = dtCurrentTable;
                gvStudentILMPList.DataBind();
                gvStudentILMPList.Rows[0].Cells[0].Width = 40;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousDataInGrid();
    }
    private void SetPreviousDataInGrid()
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
                        ((Label)gvStudentILMPList.Rows[i].Cells[0].FindControl("lblStudentId")).Text = dt.Rows[i]["Column1"].ToString();
                        ((Label)gvStudentILMPList.Rows[i].Cells[1].FindControl("lblIlmpId")).Text = dt.Rows[i]["Column2"].ToString();
                        ((Label)gvStudentILMPList.Rows[i].Cells[2].FindControl("lblActive")).Text = dt.Rows[i]["Column3"].ToString();
                        ((Button)gvStudentILMPList.Rows[i].Cells[3].FindControl("btnChangeStatus")).Text = dt.Rows[i]["Column4"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void btnChangeStatus_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((Button)sender).NamingContainer as GridViewRow;
            string isActive = (row.FindControl("lblActive") as Label).Text;
            Button btnStatus = (row.FindControl("btnChangeStatus") as Button);
            int studentId = int.Parse((row.FindControl("lblStudentId") as Label).Text);
            int ilmpId = int.Parse((row.FindControl("lblIlmpId") as Label).Text);
            string updateActive = "";
            if (isActive == "Yes")
            {
                updateActive = "No";
            }
            else
            {
                updateActive = "Yes";
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i < dt.Rows.Count - 1 && row.RowIndex != i)
                            {
                                // check whether anyother ILMP is currently active, if yes confirm with the user and interchange their status
                                string existingStatus = dt.Rows[i]["Column4"].ToString();
                                if (existingStatus == "DeActivate")
                                {
                                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "confirm('You are overriding the existing pay grade " + gradename.ToString() + ". Do you want to Override " + gradename.ToString() + " ?');", true);

                                    //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Confirmation", "if(confirm('Are you sure to change active ILMP?') == true){  document.getElementById('txtValue').value ='YES';}else{document.getElementById('txtValue').value ='NO';}", true);
                                    // string confirmValue = Request.Form["confirm_value"];
                                    // ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('Selected ILMP will be made Active');", true);
                                    //string hidden = HiddenField1.Value;
                                    // get confirmation from user, activate selected ILMP, DeActivate existing active ILMP
                                    string existingActiveILMP = dt.Rows[i]["Column2"].ToString();
                                    bool status1 = ilmpBO.UpdateILMPStatus(int.Parse(existingActiveILMP), "No");
                                    if (status1)
                                    {
                                        (gvStudentILMPList.Rows[i].Cells[4].FindControl("btnChangeStatus") as Button).Text = "Activate";
                                        (gvStudentILMPList.Rows[i].Cells[3].FindControl("lblActive") as Label).Text = "No";
                                    }
                                    // bool status2 = ilmpBO.UpdateILMPStatus(ilmpId, updateActive);
                                }
                            }
                        }
                    }
                }

            }
            bool status = ilmpBO.UpdateILMPStatus(ilmpId, updateActive);
            if (status)
            {
                if (isActive == "Yes")
                {
                    btnStatus.Text = "Activate";
                }
                else
                {
                    btnStatus.Text = "DeActivate";
                }
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            if (row.RowIndex == i)
                            {
                                dtCurrentTable.Rows[i]["Column1"] = studentId;
                                dtCurrentTable.Rows[i]["Column2"] = ilmpId;
                                dtCurrentTable.Rows[i]["Column3"] = updateActive;
                                dtCurrentTable.Rows[i]["Column4"] = btnStatus.Text;
                            }
                            else
                            {
                                dtCurrentTable.Rows[i]["Column1"] = ((Label)gvStudentILMPList.Rows[i].Cells[1].FindControl("lblStudentId")).Text;
                                dtCurrentTable.Rows[i]["Column2"] = ((Label)gvStudentILMPList.Rows[i].Cells[2].FindControl("lblIlmpId")).Text;
                                dtCurrentTable.Rows[i]["Column3"] = ((Label)gvStudentILMPList.Rows[i].Cells[3].FindControl("lblActive")).Text;
                                dtCurrentTable.Rows[i]["Column4"] = ((Button)gvStudentILMPList.Rows[i].Cells[4].FindControl("btnChangeStatus")).Text;
                            }
                        }
                        //Store the current data to ViewState
                        ViewState["CurrentTable"] = dtCurrentTable;
                        gvStudentILMPList.DataSource = null;
                        gvStudentILMPList.DataSource = dtCurrentTable;
                        gvStudentILMPList.DataBind();
                        gvStudentILMPList.Rows[0].Cells[0].Width = 40;
                    }
                }
            }
            if ((gvStudentILMPList.Rows[gvStudentILMPList.Rows.Count - 1].Cells[0].FindControl("lblStudentId") as Label).Text == "")
            {
                gvStudentILMPList.Rows[gvStudentILMPList.Rows.Count - 1].Visible = false;
            }
        }
        catch(CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
    }
    protected void btnEditILMP_Click(object sender, EventArgs e)
    {
        int selectedIndex = gvStudentILMPList.SelectedIndex;
        if (selectedIndex != -1)
        {
            string selectedIlmpid = (gvStudentILMPList.Rows[selectedIndex].Cells[1].FindControl("lblIlmpId") as Label).Text;
            Response.Redirect("~/EditILMP.aspx?id=" + selectedIlmpid);
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please select a ILMP to edit');", true);
        }
    }
}