using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPDaoImpl
/// </summary>
public class ILMPDaoImpl:IILMPDao
{
    public string AddILMP(ILMPVO inilmpVO)
    {
        string status="";
        int ilmpId = 0;
        try
        {
            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.spAddILMP", DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", inilmpVO.StudentId);
            cmd.Parameters.AddWithValue("@Name", inilmpVO.Name);
            cmd.Parameters.AddWithValue("@TemplateId", inilmpVO.TemplateId);
            cmd.Parameters.AddWithValue("@Description", inilmpVO.Description);
            cmd.Parameters.AddWithValue("@Active", inilmpVO.Active);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    ilmpId = int.Parse(reader[0].ToString());
                }
                reader.Close();
                if (ilmpId > 0)
                {
                    if (null != inilmpVO.IlmpCourses && inilmpVO.IlmpCourses.Count > 0)
                    {                       
                        foreach (ILMPCourseVO coursesVO in inilmpVO.IlmpCourses)
                        {
                            String query = "INSERT INTO dbo.IlmpCourse (IlmpId,CourseCode,Semester,Year,CourseType) VALUES(@IlmpId,@CourseCode,@Semester,@Year,@CourseType)";
                            SqlCommand cmd1 = new SqlCommand(query, DBConnection.conn);
                            cmd1.Parameters.AddWithValue("@IlmpId", ilmpId);
                            cmd1.Parameters.AddWithValue("@CourseCode", coursesVO.CourseCode);
                            cmd1.Parameters.AddWithValue("@Semester", coursesVO.Semester);
                            cmd1.Parameters.AddWithValue("@Year", coursesVO.Year);
                            cmd1.Parameters.AddWithValue("@CourseType", coursesVO.CourseType);                           
                            int result = cmd1.ExecuteNonQuery();
                            if (result <= 0)
                            {
                                status = "ILMPCourse creation failed";
                                break;
                            }
                            else
                            {
                                status = "ILMP added successfully";
                            }                          
                        }
                        if (inilmpVO.Active == "Yes")
                        {
                            string query2 = "UPDATE dbo.Ilmp SET Active='Yes' WHERE StudentId=@StudentId AND IlmpID=@IlmpID";
                            SqlCommand cmd2 = new SqlCommand(query2, DBConnection.conn);
                            cmd2.Parameters.AddWithValue("@StudentId", inilmpVO.StudentId);
                            cmd2.Parameters.AddWithValue("@IlmpID", ilmpId);
                            int result = cmd2.ExecuteNonQuery();
                            if (result > 0)
                            {
                                string query3 = "UPDATE dbo.Ilmp SET Active='No' WHERE StudentId=@StudentId AND IlmpID!=@IlmpID";
                                SqlCommand cmd3 = new SqlCommand(query3, DBConnection.conn);
                                cmd3.Parameters.AddWithValue("@StudentId", inilmpVO.StudentId);
                                cmd3.Parameters.AddWithValue("@IlmpID", ilmpId);
                                int result1 = cmd3.ExecuteNonQuery();
                                if (result1 > 0)
                                {
                                    status = "ILMP added successfully";
                                }
                                else
                                {
                                    status = "Error in Ilmp addition";
                                }
                            }
                        }
                    }
                    else
                    {
                        status = "No courses found in the ILMP";
                    }
                }
                else
                {
                    status = "Error in Ilmp addition";
                }
            }
            else{
                status = "Error in ILMP addition";
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }

    public List<ILMPCourseGridVO> GetILMPCoursesForId(int ilmpId)
    {
        List<ILMPCourseGridVO> ilmpCourses = new List<ILMPCourseGridVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT ic.CourseCode,c.Title,c.Credits,c.Level,cp.AllPrerequisites,ic.Semester,ic.Year,ic.Result, ic.CourseType " + //, ct.CourseType  " +
                            " FROM dbo.Ilmp i " +
                            " INNER JOIN dbo.IlmpCourse ic ON i.IlmpID=ic.IlmpID "+
                            " INNER JOIN dbo.Course c ON  c.CourseCode = ic.CourseCode "+
                            " INNER JOIN dbo.StudentMajor sm ON sm.StudentId = i.StudentID "+
                          //  " INNER JOIN dbo.CourseProgramme ct ON ct.ProgrammeID = sm.ProgrammeID and ct.MajorID = sm.MajorID and ct.CourseCode=ic.CourseCode " +
                            " LEFT JOIN dbo.CoursePrerequisite cp ON cp.CourseCode=c.CourseCode "+
                            " WHERE i.IlmpID=@IlmpID and sm.active='Yes' order by ic.year,ic.semester";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@IlmpID", ilmpId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ILMPCourseGridVO ilmpCourse;
                while (reader.Read())
                {
                    ilmpCourse = new ILMPCourseGridVO();
                    ilmpCourse.CourseCode = reader["CourseCode"].ToString();
                    ilmpCourse.Title = reader["Title"].ToString();
                    ilmpCourse.Credits = Int32.Parse(reader["Credits"].ToString());
                    ilmpCourse.Level = Int32.Parse(reader["Level"].ToString());
                    ilmpCourse.Prerequisites = reader["AllPrerequisites"].ToString();
                    ilmpCourse.Semester = Int32.Parse(reader["Semester"].ToString());
                    ilmpCourse.Year = Int32.Parse(reader["Year"].ToString());
                    ilmpCourse.CourseType = reader["CourseType"].ToString();
                    ilmpCourse.Result = reader["Result"].ToString();
                    ilmpCourses.Add(ilmpCourse);
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmpCourses;
    }
    public ILMPVO GetILMPDetailsForId(int ilmpId)
    {
        ILMPVO ilmp = new ILMPVO();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT i.TemplateId, i.IlmpId,i.StudentId,s.FirstName+' '+s.LastName as Name, i.Active, i.Description "
                + " FROM dbo.Ilmp i "
                + " INNER JOIN dbo.Student s ON s.StudentID = i.StudentID WHERE i.IlmpID=@IlmpID";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@IlmpID", ilmpId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ilmp.TemplateId = int.Parse(reader["TemplateId"].ToString());
                    ilmp.IlmpId = int.Parse(reader["IlmpId"].ToString());
                    ilmp.StudentId = int.Parse(reader["StudentId"].ToString());
                    ilmp.Name = reader["Name"].ToString();
                    ilmp.Active = reader["Active"].ToString();
                    ilmp.Description = reader["Description"].ToString();      
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmp;
    }
    public ILMPVO GetILMPCoursesWorkshopForId(int ilmpId)
    {
        ILMPVO ilmpVO = new ILMPVO();
        List<ILMPCourseVO> ilmpCourses = new List<ILMPCourseVO>();
        try
        {
            ilmpVO = GetILMPDetailsForId(ilmpId);
            DBConnection.conn.Open();
            string query = "SELECT ic.Semester,ic.Year, ic.CourseCode FROM dbo.ilmp i"
                            + " LEFT OUTER JOIN dbo.IlmpCourse ic ON ic.IlmpID = i.IlmpID WHERE i.IlmpID=@IlmpID"
                            + " ORDER BY ic.year, ic.Semester";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@IlmpID", ilmpId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ILMPCourseVO ilmpCourse;
                while (reader.Read())
                {
                    ilmpCourse = new ILMPCourseVO();
                    ilmpCourse.Semester = Int32.Parse(reader["Semester"].ToString());
                    ilmpCourse.Year = Int32.Parse(reader["Year"].ToString());
                    ilmpCourse.CourseCode = reader["CourseCode"].ToString();
                    ilmpCourses.Add(ilmpCourse);
                }
                ilmpVO.IlmpCourses = ilmpCourses;
                reader.Close();
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmpVO;
    }

    public List<WorkshopVO> GetILMPWorkshopForId(int ilmpId)
    {
        List<WorkshopVO> workshopNames = new List<WorkshopVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT w.Workshopid,w.Workshopname from dbo.Ilmp i "
                + " inner join dbo.TemplateCourse tc on tc.TemplateID = i.TemplateId "
                + " inner join dbo.Workshop w on w.WorkshopID = tc.WorkshopID "
                + " where tc.WorkshopID!=0 and i.IlmpID=@IlmpID";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@IlmpID", ilmpId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                WorkshopVO workshop ;
                while (reader.Read())
                {
                    workshop = new WorkshopVO();
                    workshop.WorkshopId=int.Parse(reader["Workshopid"].ToString());
                    workshop.WorkshopName=(reader["Workshopname"].ToString());
                    workshopNames.Add(workshop);
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return workshopNames;
    }
    public List<ILMPVO> GetILMPListForStudentId(int studentId)
    {
        List<ILMPVO> ilmps = new List<ILMPVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "select StudentID, IlmpID, Active from dbo.Ilmp where studentid=@StudentId order by Active desc";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
          /*  dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
            {
                sda.Fill(dt);
            }*/
           
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ILMPVO ilmp;
                while (reader.Read())
                {
                    ilmp = new ILMPVO();
                    ilmp.StudentId = int.Parse(reader["StudentID"].ToString());
                    ilmp.IlmpId = int.Parse(reader["IlmpID"].ToString());
                    ilmp.Active = reader["Active"].ToString();
                    ilmps.Add(ilmp);
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmps;
    }
    public Boolean UpdateILMPStatus(int ilmpId, string isActive)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            string query = "UPDATE dbo.Ilmp SET Active=@Active from dbo.Ilmp where ilmpId=@IlmpId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@IlmpId", ilmpId);
            cmd.Parameters.AddWithValue("@Active", isActive);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }
    public Boolean UpdateILMPStatusForStudent(int studentId, string isActive)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            string query = "UPDATE dbo.Ilmp SET Active=@Active WHERE StudentId=@StudentId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@Active", isActive);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }

    public Boolean UpdateILMPStatusForStudent(int studentId, int ilmpId)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            string query = "UPDATE dbo.Ilmp SET Active='Yes' WHERE StudentId=@StudentId AND IlmpID=@IlmpID";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@IlmpID", ilmpId);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                string query1 = "UPDATE dbo.Ilmp SET Active='No' WHERE StudentId=@StudentId AND IlmpID!=@IlmpID";
                SqlCommand cmd1 = new SqlCommand(query1, DBConnection.conn);
                cmd1.Parameters.AddWithValue("@StudentId", studentId);
                cmd1.Parameters.AddWithValue("@IlmpID", ilmpId);
                int result1 = cmd1.ExecuteNonQuery();
                if (result1 > 0)
                {
                    status = true;
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }

    public ILMPDetailsVO GetTemplateProgrammeForIlmpId(int ilmpId)
    {
        ILMPDetailsVO ilmpDetails = new ILMPDetailsVO();
        try
        {
            DBConnection.conn.Open();
            string query = "select it.TemplateId, i.StudentID, sm.ProgrammeID, sm.MajorID, it.TemplateName " +
                            "from dbo.Ilmp i  " +
                            "INNER JOIN dbo.StudentMajor sm on sm.StudentId = i.StudentID " +
                            "INNER JOIN dbo.IlmpTemplate it on it.TemplateID = i.TemplateId " +
                            "WHERE sm.Active='Yes' and i.ilmpid=@IlmpId ";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@IlmpId", ilmpId);           
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ilmpDetails.StudentId = int.Parse(reader["StudentID"].ToString());
                    ilmpDetails.IlmpId = ilmpId;
                    ilmpDetails.TemplateId = int.Parse(reader["TemplateId"].ToString());
                    ilmpDetails.ProgrammeId = reader["ProgrammeID"].ToString();
                    ilmpDetails.MajorId = reader["MajorID"].ToString();
                    ilmpDetails.TemplateName = reader["TemplateName"].ToString();
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmpDetails;
    }

    public int GetActiveIlmpForStudent(int studentId)
    {
        int ilmpId = 0;
        try
        {
            DBConnection.conn.Open();
            string query = "select IlmpID from dbo.Ilmp where studentid=@StudentId and Active='Yes' ";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);         
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ilmpId = int.Parse(reader["IlmpID"].ToString());
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmpId;
    }

    public Boolean UpdateILMP(ILMPVO inilmpVO)
    {
        Boolean status=false;
        try
        {
            DBConnection.conn.Open();
            string query = "UPDATE dbo.Ilmp SET Active=@Active, Description=@Description from dbo.Ilmp where ilmpId=@IlmpId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@IlmpId", inilmpVO.IlmpId);
            cmd.Parameters.AddWithValue("@Description", inilmpVO.Description);
            cmd.Parameters.AddWithValue("@Active", inilmpVO.Active);
            int result = cmd.ExecuteNonQuery();
            
            if (result > 0)
            {
                if (null != inilmpVO.IlmpCourses && inilmpVO.IlmpCourses.Count > 0)
                {
                    foreach (ILMPCourseVO coursesVO in inilmpVO.IlmpCourses)
                    {
                        String query1 = " UPDATE DBO.IlmpCourse SET Semester=@Semester,Year=@Year,Result=@Result WHERE IlmpId=@IlmpId AND CourseCode=@CourseCode";
                        SqlCommand cmd1 = new SqlCommand(query1, DBConnection.conn);
                        cmd1.Parameters.AddWithValue("@IlmpId", inilmpVO.IlmpId);
                        cmd1.Parameters.AddWithValue("@CourseCode", coursesVO.CourseCode);
                        cmd1.Parameters.AddWithValue("@Semester", coursesVO.Semester);
                        cmd1.Parameters.AddWithValue("@Year", coursesVO.Year);
                        cmd1.Parameters.AddWithValue("@Result", coursesVO.Result);
                        // Add other parameters here
                        int result1 = cmd1.ExecuteNonQuery();
                        if (result1 <= 0)
                        {
                            status = false;
                            break;
                        }
                        else
                        {
                            status = true;
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException("Error in updating ILMP");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }
}