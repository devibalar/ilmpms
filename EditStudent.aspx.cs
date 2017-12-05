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


public partial class EditStudent : System.Web.UI.Page
{
    StudentBO studentBO = new StudentBO();
    StudentMajorBO studentMajorBO = new StudentMajorBO();
    UserBO userBO = new UserBO();
    CourseDaoImpl courseDaoImpl = new CourseDaoImpl();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillComboBox();
            getValueStudent();
        }
    }

    public void getValueStudent()
    {
        txtStudentID.Text = Session["StudentID"].ToString();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT FirstName, LastName, Status FROM Student WHERE StudentID='" + txtStudentID.Text + "'";
            using (SqlCommand cmd = new SqlCommand(query, DBConnection.conn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtFirstName.Text = (string)dr["FirstName"];
                    txtLastName.Text = (string)dr["LastName"];
                    ddlStatus.Text = (string)dr["Status"];
                }
                dr.Close();
            }

            string query1 = "SELECT ProgrammeId FROM StudentMajor WHERE Active='Yes' and StudentID='" + txtStudentID.Text + "'";
            using (SqlCommand cmd = new SqlCommand(query1, DBConnection.conn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ddlStudentProgramme.SelectedItem.Text = (string)dr["ProgrammeId"];
                }
                dr.Close();
            }

            string query3 = "SELECT MajorId FROM StudentMajor WHERE Active='Yes' and StudentID='" + txtStudentID.Text + "'";
            using (SqlDataAdapter cmd = new SqlDataAdapter(query3, DBConnection.conn))
            {
                DataTable dt = new DataTable();
                cmd.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    lbMajor.Items.Add(row["MajorId"].ToString());
                }
            }

            string query2 = "SELECT EmailId FROM IlmpUSer WHERE StudentId='" + txtStudentID.Text + "'";
            using (SqlCommand cmd = new SqlCommand(query2, DBConnection.conn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtEmailID.Text = (string)dr["EmailId"];
                }
                dr.Close();
            }
        }           
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('Error while fetching student details');</script>");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
    }

    protected void btnUpdateStudent_Click(object sender, EventArgs e)
    {
        try
        {
            int studentID = Convert.ToInt32(txtStudentID.Text.Trim());
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string programmeId = ddlStudentProgramme.SelectedValue.Trim();
            string emailID = txtEmailID.Text.Trim();
            string status = ddlStatus.Text.Trim();
            DateTime updatedDTM = DateTime.Now;
            string active = "";

            if (ddlStatus.Text != "Studying")
            {
                active = "No";
            }
            else
            {
                active = "Yes";
            }

            if (!txtEmailID.Text.Trim().EndsWith("@ess.ais.ac.nz"))
            {
                Response.Write("<script>alert('Please enter valid email ess.ais.ac.nz');</script>");
            }
            StudentVO studentVO = new StudentVO(studentID, firstName, lastName, status, updatedDTM);
            UserVO userVO = new UserVO(studentID, emailID, active);

            if (active == "No" || status == "Withdrawn" || status == "Graduated" || status == "Postponed")
            {
                ILMPBO ilmpBO = new ILMPBO();
                bool ilmpUpdationStatus = ilmpBO.UpdateILMPStatusForStudent(studentID, "No");
            }

            if (txtFirstName.Text == "" || txtLastName.Text == "" || txtEmailID.Text == "" || lbMajor.Items.Count == 0)
            {
                Response.Write("<script>alert('Please check First Name, Last Name, Email and Major is filled');</script>");
            }
            else if (studentBO.UpdateStudent(studentVO) && userBO.UpdateUser(userVO))
            {
                DBConnection.conn.Open();
                string query = "DELETE FROM StudentMajor WHERE StudentID=" + studentID;
                SqlCommand cmd2 = new SqlCommand(query, DBConnection.conn);
                cmd2.ExecuteNonQuery();
                DBConnection.conn.Close();
                for (int i = 0; i < lbMajor.Items.Count; i++)
                {
                    ListItem lmajor = lbMajor.Items[i];
                    StudentMajorVO studentMajorVO = new StudentMajorVO(studentID, programmeId, lmajor.Value, active);
                    StudentMajorBO studentMajorBO = new StudentMajorBO();
                    if (studentMajorBO.AddStudent(studentMajorVO))
                    {
                        Response.Write("<script>alert('Student updated successfully');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error in updating student major');</script>");
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Student update Fail');</script>");
            }
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex .Message+ "');</script>");
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('Student updation failed');</script>");
        }
    }

    protected void btnCancelStudent_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StudentList.aspx");
    }

    private void FillComboBox()
    {
        ddlStudentProgramme.Items.Clear();
        //ddlStudentMajor.Items.Clear();

        DataTable dtProgrammeID = courseDaoImpl.FillProgramme();
        foreach (DataRow dr in dtProgrammeID.Rows)
        {
            ddlStudentProgramme.Items.Add(dr["ProgrammeID"].ToString());
        }
       
    }
    protected void btnAddMajor_Click(object sender, EventArgs e)
    {
        lbMajor.Items.Add(this.ddlMajor.Text);
    }
    protected void btnRemoveMajor_Click(object sender, EventArgs e)
    {
        if (lbMajor.SelectedIndex != -1)
        {
            lbMajor.Items.RemoveAt(lbMajor.SelectedIndex);
        }
    }
}