using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseDaoImpl
/// </summary>
public class CourseDaoImpl:ICourseDao
{
    /*Get list of all coursecode from course as DataSet
     */
    public DataSet GetAllCourseCode()
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT CourseCode FROM dbo.Course";
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "Course");
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException+": "+e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ds;
    }
    // Get list of all coursecode from course as List
    public List<string> GetAllCourseCodeAsList()
    {
        List<string> courseCodes = new List<string>();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT CourseCode FROM dbo.Course";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    courseCodes.Add(reader["CourseCode"].ToString());
                }
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return courseCodes;
    }
    //Get course details for given course code
    public CourseVO GetCourseDetailsForCourseCode(string incourseCode)
    {
        CourseVO courseVO = new CourseVO();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT Title,Credits,Level,AllPrerequisites,OfferedFrequency,Active from dbo.Course c" +
                            " LEFT JOIN dbo.CoursePrerequisite cp ON cp.CourseCode=c.CourseCode AND Active='Yes'"+
                            " WHERE c.CourseCode = @CourseCode";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", incourseCode);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    courseVO.CourseCode = incourseCode;
                    courseVO.Title = reader["Title"].ToString();
                    courseVO.Credits = Int32.Parse(reader["Credits"].ToString());
                    courseVO.Level = Int32.Parse(reader["Level"].ToString());
                    courseVO.Prerequisites.AllPrerequisites = reader["AllPrerequisites"].ToString();
                    courseVO.OfferedFrequency = reader["OfferedFrequency"].ToString();
                    courseVO.Active = reader["Active"].ToString();
                }
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return courseVO;
    }

   //get course details for given programme and major       
    public ILMPCourseGridVO GetCourseDetailsForTemplate(CourseProgrammeVO courseProgarmmeVO)
    {
        ILMPCourseGridVO ilmpcourseVO = new ILMPCourseGridVO();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT cpgm.CourseType,c.Title,c.Credits,c.Level,cp.AllPrerequisites from dbo.Course c" +
                            " INNER JOIN dbo.CourseProgramme cpgm ON cpgm.CourseCode=c.CourseCode" +
                            " LEFT JOIN dbo.CoursePrerequisite cp ON cp.CourseCode=c.CourseCode " +
                            " WHERE c.CourseCode = @CourseCode AND cpgm.ProgrammeID=@ProgrammeID AND cpgm.MajorID=@MajorID AND c.Active='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", courseProgarmmeVO.CourseCode);
            cmd.Parameters.AddWithValue("@ProgrammeID", courseProgarmmeVO.ProgrammeId);
            cmd.Parameters.AddWithValue("@MajorID", courseProgarmmeVO.MajorId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ilmpcourseVO.CourseType = reader["CourseType"].ToString();
                    ilmpcourseVO.Title = reader["Title"].ToString();
                    ilmpcourseVO.Credits = Int32.Parse(reader["Credits"].ToString());
                    ilmpcourseVO.Level = Int32.Parse(reader["Level"].ToString());
                    ilmpcourseVO.Prerequisites = reader["AllPrerequisites"].ToString();
                }
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmpcourseVO;
    }
    //Get all course code and type for programme and major
    public List<CourseProgrammeVO> GetCourseForPgmMajor(string inprogrammeId, string inmajorid)
    {
        List<CourseProgrammeVO> courses = new List<CourseProgrammeVO>();       
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT CourseCode, CourseType FROM dbo.CourseProgramme WHERE ProgrammeID=@ProgrammeId AND MajorID=@MajorId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@ProgrammeId", inprogrammeId);
            cmd.Parameters.AddWithValue("@MajorId", inmajorid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                CourseProgrammeVO courseProgrammeVO;
                while (reader.Read())
                {
                    courseProgrammeVO = new CourseProgrammeVO();
                    courseProgrammeVO.CourseCode = reader["CourseCode"].ToString();
                    courseProgrammeVO.CourseType = reader["CourseType"].ToString();
                    courses.Add(courseProgrammeVO);
                }
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return courses;
    }
    // Add the course details into course, course prequiiste and course programme tables
    public string AddCourse(CourseVO inCourse)
    {
        string status = "";
        SqlTransaction transaction=null;
        try
        {                    
            DBConnection.conn.Open();
            // Start a local transaction.
            transaction = DBConnection.conn.BeginTransaction(IsolationLevel.ReadCommitted);
            String query = "INSERT INTO dbo.Course (CourseCode, Title, Credits, Level, OfferedFrequency, Active, CreatedDTM, UpdatedDTM) VALUES (@CourseCode,@Title,@Credits, @Level, @OfferedFrequency,@Active,@CreatedDTM,@UpdatedDTM)";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Transaction = transaction;
            cmd.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
            cmd.Parameters.AddWithValue("@Title", inCourse.Title);
            cmd.Parameters.AddWithValue("@Credits", inCourse.Credits);
            cmd.Parameters.AddWithValue("@Level", inCourse.Level);
            cmd.Parameters.AddWithValue("@OfferedFrequency", inCourse.OfferedFrequency);
            cmd.Parameters.AddWithValue("@Active", inCourse.Active);
            cmd.Parameters.AddWithValue("@CreatedDTM", DateTime.Now);
            cmd.Parameters.AddWithValue("@UpdatedDTM", DateTime.Now);
            cmd.Transaction = transaction;
            // Add other parameters here
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                if (inCourse.Program.Count > 0)
                {
                    foreach (CourseProgrammeVO cp in inCourse.Program)
                    {
                        String query1 = "INSERT INTO dbo.CourseProgramme (CourseCode, ProgrammeID, MajorID, CourseType) VALUES (@CourseCode,@ProgrammeID,@MajorID, @CourseType)";
                        SqlCommand cmd1 = new SqlCommand(query1, DBConnection.conn);
                        cmd1.Transaction = transaction;
                        cmd1.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
                        cmd1.Parameters.AddWithValue("@ProgrammeID", cp.ProgrammeId);
                        cmd1.Parameters.AddWithValue("@MajorID", cp.MajorId);
                        cmd1.Parameters.AddWithValue("@CourseType", cp.CourseType);
                        string temp = inCourse.CourseCode + cp.ProgrammeId + cp.MajorId + cp.CourseType;
                        cmd1.Transaction = transaction;
                         int result1 = cmd1.ExecuteNonQuery();
                         if (result1 < 0)
                         {
                             status = "Course Programme addition failed";
                             break;
                         }
                         else
                         {
                             status = "Course added successfully";
                         }
                    }
                }
                if (!status.Contains("failed") && inCourse.Prerequisites.CourseCode != null)
                {
                    String query2 = "INSERT INTO dbo.CoursePrerequisite (CourseCode, PrerequisiteCode, PrerequisiteType, AllPrerequisites) VALUES (@CourseCode,@PrerequisiteCode,@PrerequisiteType, @AllPrerequisites)";
                        SqlCommand cmd2 = new SqlCommand(query2, DBConnection.conn);
                        cmd2.Transaction = transaction;
                        cmd2.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
                        cmd2.Parameters.AddWithValue("@PrerequisiteCode", inCourse.Prerequisites.PrerequisiteCode);
                        cmd2.Parameters.AddWithValue("@PrerequisiteType", inCourse.Prerequisites.Type);
                        cmd2.Parameters.AddWithValue("@AllPrerequisites", inCourse.Prerequisites.AllPrerequisites);
                        cmd2.Transaction = transaction;
                         int result2 = cmd2.ExecuteNonQuery();
                         if (result2 < 0)
                         {
                             status = "Course Prerequisite addition failed";
                         }
                         else
                         {
                             status = "Course added successfully";
                         }
                }
                transaction.Commit();
            }
            else
            {
                status = "Course addition failed";
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            if (e.Number == 2627)
            {
                status = "CourseCode already exists";                
            }
            else
            {
                transaction.Rollback();
                throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
            }
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            transaction.Rollback();
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
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

    //update course details.To allow updation of coursecode, originalCourseCode paramater is used. 
    public string UpdateCourse(CourseVO inCourse, string originalCourseCode)
    {
        string status = "";
        SqlTransaction transaction=null;
        try
        {
            DBConnection.conn.Open();
            transaction = DBConnection.conn.BeginTransaction(IsolationLevel.ReadCommitted);
            String query = "UPDATE dbo.Course SET CourseCode=@CourseCode,Title=@Title,Credits=@Credits, Level=@Level, OfferedFrequency=@OfferedFrequency,Active=@Active,UpdatedDTM=@UpdatedDTM WHERE CourseCode=@OriginalCourseCode";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Transaction = transaction;
            cmd.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
            cmd.Parameters.AddWithValue("@Title", inCourse.Title);
            cmd.Parameters.AddWithValue("@Credits", inCourse.Credits);
            cmd.Parameters.AddWithValue("@Level", inCourse.Level);
            cmd.Parameters.AddWithValue("@OfferedFrequency", inCourse.OfferedFrequency);
            cmd.Parameters.AddWithValue("@Active", inCourse.Active);
            cmd.Parameters.AddWithValue("@UpdatedDTM", inCourse.UpdatedTime);
            cmd.Parameters.AddWithValue("@OriginalCourseCode", originalCourseCode);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                if (inCourse.Program.Count > 0)
                {
                    String query11 = "DELETE FROM dbo.CourseProgramme  WHERE CourseCode=@CourseCode";
                    SqlCommand cmd11 = new SqlCommand(query11, DBConnection.conn);
                    cmd11.Transaction = transaction;
                    cmd11.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
                    int result11 = cmd11.ExecuteNonQuery();
                    if (result11 < 0)
                    {
                        status = "Course Programme updation failed";
                    }
                    else
                    {
                        foreach (CourseProgrammeVO cp in inCourse.Program)
                        {
                            String query1 = "INSERT INTO dbo.CourseProgramme (CourseCode, ProgrammeID, MajorID, CourseType) VALUES (@CourseCode,@ProgrammeID,@MajorID, @CourseType)";
                            SqlCommand cmd1 = new SqlCommand(query1, DBConnection.conn);
                            cmd1.Transaction = transaction;
                            cmd1.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
                            cmd1.Parameters.AddWithValue("@ProgrammeID", cp.ProgrammeId);
                            cmd1.Parameters.AddWithValue("@MajorID", cp.MajorId);
                            cmd1.Parameters.AddWithValue("@CourseType", cp.CourseType);
                            int result1 = cmd1.ExecuteNonQuery();
                            if (result1 < 0)
                            {
                                status = "Course Programme updation failed";
                                break;
                            }
                            else
                            {
                                status = "Course updated successfully";
                            }
                        }
                    }
                }
                if (!status.Contains("failed") && inCourse.Prerequisites != null)
                {
                    string query3 = "SELECT AllPrerequisites from dbo.CoursePrerequisite WHERE CourseCode=@CourseCode";
                    SqlCommand cmd3 = new SqlCommand(query3, DBConnection.conn);
                    cmd3.Transaction = transaction;
                    cmd3.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
                    SqlDataReader reader = cmd3.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string prerequisite = reader["AllPrerequisites"].ToString();
                        }
                        reader.Close();
                        String query2 = "UPDATE dbo.CoursePrerequisite SET AllPrerequisites=@AllPrerequisites WHERE CourseCode=@CourseCode ";
                        SqlCommand cmd2 = new SqlCommand(query2, DBConnection.conn);
                        cmd2.Transaction = transaction;
                        cmd2.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
                        if (inCourse.Prerequisites.AllPrerequisites == null)
                        {
                            inCourse.Prerequisites.AllPrerequisites = "";
                        }
                        cmd2.Parameters.AddWithValue("@AllPrerequisites", inCourse.Prerequisites.AllPrerequisites);
                        int result2 = cmd2.ExecuteNonQuery();
                        if (result2 < 0)
                        {
                            status = "Course Prerequisite updation failed";
                        }
                        else
                        {
                            status = "Course updated successfully";
                        }
                    }
                    else
                    {
                        reader.Close();
                        String query2 = "INSERT INTO dbo.CoursePrerequisite (CourseCode, PrerequisiteCode, PrerequisiteType, AllPrerequisites) VALUES (@CourseCode,@PrerequisiteCode,@PrerequisiteType, @AllPrerequisites)";
                        SqlCommand cmd2 = new SqlCommand(query2, DBConnection.conn);
                        cmd2.Transaction = transaction;
                        cmd2.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
                        cmd2.Parameters.AddWithValue("@PrerequisiteCode", "");
                        cmd2.Parameters.AddWithValue("@PrerequisiteType", "");
                        cmd2.Parameters.AddWithValue("@AllPrerequisites", inCourse.Prerequisites.AllPrerequisites);
                        cmd2.Transaction = transaction;
                        int result2 = cmd2.ExecuteNonQuery();
                        if (result2 < 0)
                        {
                            status = "Course Prerequisite updation failed";
                        }
                        else
                        {
                            status = "Course updated successfully";
                        }
                    }                  
                }
                transaction.Commit();
            }
            else
            {
                status = "Course updation failed";
            }
        }
        catch (SqlException e)
        {
            status = "Course updation failed";
            ExceptionUtility.LogException(e, "Error Page");
            transaction.Rollback();
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            status = "Course updation failed";
            ExceptionUtility.LogException(e, "Error Page");
            transaction.Rollback();
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
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
    // delete the course using coursecode
    public Boolean DeleteCourse(CourseVO inCourse)
    {      
        string courseCode = "";
        try
        {
            DBConnection.conn.Open();
            String query = "DELETE FROM dbo.Course where courseCode=@CourseCode";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", inCourse.CourseCode);
            // Add other parameters here
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                courseCode = inCourse.CourseCode;
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return true;        
    }
    // get list of all programmeid from programme
    public DataTable FillProgramme()
    {
        DataTable dt = null;
         try
        {
            DBConnection.conn.Open();            
            string selectCourseCode = "SELECT ProgrammeID from Programme";
            SqlCommand command = new SqlCommand(selectCourseCode, DBConnection.conn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }           
        return dt;        
    }
    // Get list of all majorid from major
    public DataTable FillMajor()
    {      
        DataTable dt = null;
        try{
            DBConnection.conn.Open();
            string selectCourseCode = "SELECT MajorID from Major";
            SqlCommand command = new SqlCommand(selectCourseCode, DBConnection.conn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
        }
          catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return dt;        
    }
}