using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadStudent : System.Web.UI.Page
{
    StudentBO studentBO = new StudentBO();
    StudentMajorBO studentMajorBO = new StudentMajorBO();
    UserBO userBO = new UserBO();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string existingStudent = "";
        string failedStudent = "";
        bool studentExists = false;

        try
        {
            List<StudentMajorVO> studentPgmVOList = new List<StudentMajorVO>();
            List<StudentVO> studentVOList = new List<StudentVO>();
            List<UserVO> users = new List<UserVO>();
            UserDaoImpl userDaoImpl = new UserDaoImpl();
            UserVO userVO = null;
            IStudentDao studentDao = new StudentDaoImpl();
            List<string> existingStudentIDList = studentDao.GetAllStudentIDAsList();
            string filepath = Server.MapPath("~/Files/") + Path.GetFileName(fuBulkCourseUpload.PostedFile.FileName);
            if (fuBulkCourseUpload.HasFile)
            {
                fuBulkCourseUpload.SaveAs(filepath);
                string data = File.ReadAllText(filepath);
                Boolean headerRowHasBeenSkipped = false;
                foreach (string row in data.Split('\n'))
                {
                    if (headerRowHasBeenSkipped)
                    {
                        string programme="";
                        studentExists = false;
                        if (!string.IsNullOrEmpty(row))
                        {
                            userVO = new UserVO();
                            StudentVO studentVO = new StudentVO();
                            StudentMajorVO studentMajorVO = new StudentMajorVO();
                            List<string> pgmList = new List<string>();
                            int i = 0;

                            foreach (string cell in row.Split(','))
                            {
                                if (!studentExists)
                                {
                                    string celltemp = "";
                                    if (cell.Contains("\r"))
                                    {
                                        celltemp = cell.Replace("\r", "");
                                    }
                                    else
                                    {
                                        celltemp = cell;
                                    }
                                    celltemp = celltemp.Trim();
                                    switch (i)
                                    {
                                        case 0:
                                            {
                                                if (existingStudentIDList.Contains(celltemp))
                                                {
                                                    existingStudent += celltemp + ",";
                                                    studentExists = true;
                                                }
                                                else
                                                {
                                                    studentVO.StudentId = Convert.ToInt32(celltemp);
                                                    studentMajorVO.studentId = Convert.ToInt32(celltemp); ;
                                                    userVO.StudentID = Convert.ToInt32(celltemp);
                                                }
                                                break;
                                            }
                                        case 1:
                                            {
                                                studentVO.FirstName = celltemp;
                                                break;
                                            }
                                        case 2:
                                            {
                                                studentVO.LastName = celltemp;
                                                break;
                                            }
                                        case 3:
                                            {
                                              /*  if (celltemp.Contains("&"))
                                                {
                                                    foreach (string pgm in celltemp.Split('&'))
                                                    {
                                                        pgmList.Add(pgm);
                                                    }
                                                }
                                                else
                                                {
                                                    pgmList.Add(celltemp);
                                                }*/
                                                programme = celltemp;
                                                break;
                                            }
                                        case 4:
                                            {
                                                if (celltemp.Contains("&"))
                                                {
                                                    StudentMajorVO sm;
                                                   // foreach (string pgm in pgmList)
                                                    //{
                                                        foreach (string mjr in celltemp.Split('&'))
                                                        {
                                                            sm = new StudentMajorVO();
                                                            sm.StudentId = studentMajorVO.StudentId;
                                                            sm.ProgrammeID = programme;
                                                            sm.MajorID = mjr.Trim();
                                                            studentPgmVOList.Add(sm);
                                                        }
                                                   // }
                                                }
                                                else
                                                {
                                                    StudentMajorVO sm;
                                                   // foreach (string pgm in pgmList)
                                                    //{
                                                        sm = new StudentMajorVO();
                                                        sm.StudentId = studentMajorVO.StudentId;
                                                        sm.ProgrammeID = programme;
                                                        sm.MajorID = celltemp;
                                                        studentPgmVOList.Add(sm);
                                                    //}
                                                }
                                                break;
                                            }
                                        case 5:
                                            {
                                                if (!celltemp.EndsWith("@ess.ais.ac.nz"))
                                                {
                                                    existingStudent += celltemp + ",";
                                                    studentExists = true;
                                                }
                                                else
                                                {
                                                    userVO.EmailID = celltemp;
                                                }
                                                break;
                                            }
                                    }
                                }
                                i++;
                            }
                            if (!studentExists)
                            {
                                studentVO.Program = studentPgmVOList;
                                users.Add(userVO);
                                studentVOList.Add(studentVO);
                            }
                        }
                    }
                    headerRowHasBeenSkipped = true;
                }
                if (studentVOList.Count > 0)
                {
                    foreach (StudentVO studentVO in studentVOList)
                    {
                        studentVO.Status = "Studying";
                        studentVO.CreatedDate = DateTime.Now;
                        studentVO.LastUpdatedDate = DateTime.Now;
                        string status = (studentBO.AddStudent(studentVO)).ToString();
                        if (status.Contains("fail"))
                        {
                            failedStudent += studentVO.StudentID;
                        }
                    }

                    foreach (StudentMajorVO studentMajorVO in studentPgmVOList)
                    {
                        studentMajorVO.Active = "Yes";
                        string status = (studentMajorBO.AddStudent(studentMajorVO)).ToString();
                        if (status.Contains("fail"))
                        {
                            failedStudent += studentMajorVO.StudentId;
                        }
                    }
                    userDaoImpl.AddStudentUsers(users);

                    string usermessage = " Students upload completed.";
                    if (failedStudent != "")
                    {
                        usermessage += " FailedStudentList " + failedStudent;
                    }
                    if (existingStudent != "")
                    {
                        usermessage += " Skipped existing student. " + existingStudent;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + usermessage + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StudentList.aspx");
    }
}