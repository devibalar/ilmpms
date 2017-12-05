using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadCourse : System.Web.UI.Page
{
    CourseBO courseBO = new CourseBO();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        string existingCourse = "";
        string failedCourses = "";
        bool courseExists = false;
        List<CourseVO> courseVOList = new List<CourseVO>();
        try
        {          
            ICourseDao courseDao = new CourseDaoImpl();
            List<string> existingCourseCodeList = courseDao.GetAllCourseCodeAsList();
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
                        courseExists = false;
                        if (!string.IsNullOrEmpty(row))
                        {
                            CourseVO courseVO = new CourseVO();
                            List<string> pgmList = new List<string>();
                            string courseType = "COM";
                            int i = 0;
                            List<CourseProgrammeVO> coursePgmVOList = new List<CourseProgrammeVO>();
                            foreach (string cell in row.Split(','))
                            {
                                if (!courseExists)
                                {
                                    string celltemp = "";
                                    if (cell.Contains("\r"))
                                    {
                                        celltemp = cell.Replace("\r", "").Trim();
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
                                                if (existingCourseCodeList.Contains(celltemp))
                                                {
                                                    existingCourse += celltemp + ",";
                                                    courseExists = true;
                                                }
                                                else
                                                {
                                                    courseVO.CourseCode = celltemp;
                                                }
                                                break;
                                            }
                                        case 1:
                                            {
                                                courseVO.Title = celltemp;
                                                break;
                                            }
                                        case 2:
                                            {
                                                if (celltemp.Contains("&"))
                                                {
                                                    foreach (string pgm in celltemp.Split('&'))
                                                    {
                                                        pgmList.Add(pgm.Trim());
                                                    }
                                                }
                                                else
                                                {
                                                    pgmList.Add(celltemp);
                                                }
                                                break;
                                            }
                                        case 3:
                                            {
                                                courseType = celltemp;
                                                break;
                                            }
                                        case 4:
                                            {
                                                if (celltemp.Contains("&"))
                                                {
                                                    CourseProgrammeVO cp;
                                                    foreach (string pgm in pgmList)
                                                    {
                                                        foreach (string mjr in celltemp.Split('&'))
                                                        {
                                                            cp = new CourseProgrammeVO();
                                                            cp.ProgrammeId = pgm.Trim();
                                                            cp.CourseType = courseType.Trim();
                                                            cp.MajorId = mjr.Trim();
                                                            coursePgmVOList.Add(cp);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    CourseProgrammeVO cp;
                                                    foreach (string pgm in pgmList)
                                                    {
                                                        cp = new CourseProgrammeVO();
                                                        cp.ProgrammeId = pgm.Trim();
                                                        cp.CourseType = courseType.Trim();
                                                        cp.MajorId = celltemp.Trim();
                                                        coursePgmVOList.Add(cp);
                                                    }
                                                }
                                                break;
                                            }
                                        case 5:
                                            {
                                                courseVO.Credits = int.Parse(celltemp);
                                                break;
                                            }
                                        case 6:
                                            {
                                                courseVO.Level = int.Parse(celltemp);
                                                break;
                                            }
                                        case 7:
                                            {
                                                courseVO.OfferedFrequency = celltemp;
                                                break;
                                            }
                                        case 8:
                                            {
                                                if (celltemp != "NA")
                                                {
                                                    courseVO.Prerequisites.AllPrerequisites = celltemp.Trim();
                                                    courseVO.Prerequisites.CourseCode = courseVO.CourseCode;
                                                }
                                                break;
                                            }
                                        case 9:
                                            {
                                                string allprerequisite = courseVO.Prerequisites.AllPrerequisites;
                                                if (celltemp != "NA")
                                                {
                                                    if (allprerequisite != null)
                                                    {
                                                        courseVO.Prerequisites.AllPrerequisites = allprerequisite + "#" + celltemp;
                                                    }
                                                    else
                                                    {
                                                        courseVO.Prerequisites.AllPrerequisites = "#" + celltemp;
                                                        courseVO.Prerequisites.CourseCode = courseVO.CourseCode;
                                                    }
                                                }
                                                else
                                                {
                                                    courseVO.Prerequisites.AllPrerequisites = allprerequisite;
                                                }
                                                courseVO.Prerequisites.PrerequisiteCode = "";
                                                courseVO.Prerequisites.Type = "Prerequisite";
                                                break;
                                            }
                                    }
                                }
                                i++;
                            }
                            if (!courseExists)
                            {
                                courseVO.Program = coursePgmVOList;
                                courseVOList.Add(courseVO);
                            }
                        }
                    }
                    headerRowHasBeenSkipped = true;
                }
                if (courseVOList.Count > 0)
                {
                    foreach (CourseVO courseVO in courseVOList)
                    {
                        courseVO.Active = "Yes";
                        string status = courseBO.AddCourse(courseVO);
                        if (status.Contains("fail"))
                        {
                            failedCourses += courseVO.CourseCode;
                        }
                    }
                    string usermessage = " Courses upload completed.";
                    if (failedCourses != "")
                    {
                        usermessage += " FailedCourseList " + failedCourses;
                    }
                    if (existingCourse != "")
                    {
                        usermessage += " Existing courses cannot be added. " + existingCourse;
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + usermessage + "');", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' Uploaded courses exists already');", true);
                }
            }           
        }
            
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            if (ex.Message.Contains("used by another process"))
            {
                Response.Write("<script>alert('CSV file is in use.Please close it and try again');</script>");
            }
            else
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
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
        Response.Redirect("~/CourseList.aspx");
    }
}