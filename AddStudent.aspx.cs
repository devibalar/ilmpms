using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Query.Dynamic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddStudent : System.Web.UI.Page
{
    StudentBO studentBO = new StudentBO();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void Clear()
    {
        txtStudentID.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
    }

    protected void btnAddStudent_Click(object sender, EventArgs e)
    {
        try
        {
            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Student WHERE StudentID =" + Convert.ToInt32(txtStudentID.Text.Trim()), DBConnection.conn);
            Int32 count = (Int32)cmd.ExecuteScalar();
            DBConnection.conn.Close();
            if (count > 0)
            {
                Response.Write("<script>alert('StudentID already exists.');</script>");
            }

            else if (!txtEmail.Text.Trim().EndsWith("@ess.ais.ac.nz"))
            {
                Response.Write("<script>alert('Please enter valid email (e.g. stud001@ess.ais.ac.nz)');</script>");
            }

            DBConnection.conn.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM dbo.IlmpUser WHERE EmailId ='" + txtEmail.Text.Trim() + "'", DBConnection.conn);
            Int32 count1 = (Int32)cmd1.ExecuteScalar();
            DBConnection.conn.Close();
            if (count1 > 0)
            {
                Response.Write("<script>alert('Email already exists, please check email Student');</script>");
            }
            else
            {
                bool valid = ValidateControls();
                if (valid)
                {
                    int studentID = 0;
                    try
                    {
                        studentID = Int32.Parse(txtStudentID.Text);
                    }
                    catch (ParseException)
                    {
                        Response.Write("<script>alert('Please enter valid studentid');</script>");
                    }
                    string firstName = txtFirstName.Text;
                    string lastName = txtLastName.Text;
                    string status = "Studying";
                    string emailID = txtEmail.Text;
                    DateTime createdDTM = DateTime.Now;
                    DateTime updatedDTM = DateTime.Now;

                    StudentVO studentVO = new StudentVO(studentID, firstName, lastName, status, createdDTM, updatedDTM);

                    UserVO userVO = new UserVO();
                    userVO.StudentID = studentID;
                    userVO.StaffID = 0;
                    userVO.EmailID = emailID;
                    userVO.Role = ApplicationConstants.StudentRole;

                    if (lbMajor.Items.Count == 0)
                    {
                        Response.Write("<script>alert('Please choose major');</script>");
                    }

                    else if (studentBO.AddStudent(studentVO))
                    {
                        for (int i = 0; i < lbMajor.Items.Count; i++)
                        {
                            ListItem lmajor = lbMajor.Items[i];
                            StudentMajorVO studentMajorVO = new StudentMajorVO(studentID, ddlProgramme.SelectedItem.Value, lmajor.Value, "Yes");

                            StudentMajorBO studentMajorBO = new StudentMajorBO();
                            if (studentMajorBO.AddStudent(studentMajorVO))
                            {
                                Response.Write("<script>alert('Student added successfully');</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('Error in adding student major');</script>");
                            }
                        }
                        UserBO userBO = new UserBO();
                        userBO.AddUser(userVO);
                    }
                    else
                    {
                        Response.Write("<script>alert('Error in adding student');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Please enter all student details');</script>");
                }
            }
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error in adding student');</script>");
            ExceptionUtility.LogException(ex, "Error Page");
        }
        Clear();
    }

    protected void btnCancelAddStudent_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StudentList.aspx");
    }

    private bool ValidateControls()
    {
        bool valid = false;
        if (txtStudentID.Text == null || txtStudentID.Text.Trim() == "" ||
            txtFirstName.Text == null || txtFirstName.Text.Trim() == "" ||
            txtLastName.Text == null || txtLastName.Text.Trim() == "" ||
            ddlProgramme.SelectedItem == null || ddlProgramme.SelectedItem.Text.Trim() == "" ||
            ddlMajor.SelectedItem == null || ddlProgramme.SelectedItem.Text.Trim() == "")
        {
            valid = false;
        }
        else
        {
            valid = true;
        }
        return valid;
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