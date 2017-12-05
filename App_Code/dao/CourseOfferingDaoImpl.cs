using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseOfferingDaoImpl
/// </summary>
public class CourseOfferingDaoImpl:ICourseOfferingDao
{
    // Get list of all coursecode in courseoffering
    public DataSet GetAllAvailableCourseCode()
    {          
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT Distinct CourseCode FROM dbo.CourseOffering where IsActive='Yes'";
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "CourseOffering");
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
        return ds;
    }
    // Get all details from course offering
    public List<CourseOfferingVO> GetOfferedCourses()
    {
        List<CourseOfferingVO> coursesOffered = new List<CourseOfferingVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT * from dbo.CourseOffering where isActive='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CourseOfferingVO courseOfferingVO = new CourseOfferingVO();
                    courseOfferingVO.CourseCode = reader["CourseCode"].ToString();
                    courseOfferingVO.CourseCode = reader["Semester"].ToString();
                    courseOfferingVO.CourseCode = reader["Year"].ToString();
                    coursesOffered.Add(courseOfferingVO);
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
        return coursesOffered;
    }
    // Get list of semester for given course code
    public List<int> GetSemesterForCourseCode(string incourseCode)
    {
        List<int> offeredPeriod = new List<int>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT distinct Semester from dbo.CourseOffering WHERE CourseCode='"+incourseCode+"' AND isActive='Yes' ";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", incourseCode);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int semester = Int32.Parse(reader["Semester"].ToString());
                    offeredPeriod.Add(semester);
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
        return offeredPeriod;
    }
    // get list of year for the given course code and semester
    public List<int> GetYearForCourseCode(string incourseCode, int semester)
    {
        List<int> offeredYear = new List<int>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT distinct Year from dbo.CourseOffering where CourseCode=@CourseCode AND Semester=@Semester and isActive='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", incourseCode);
            cmd.Parameters.AddWithValue("@Semester", semester);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int year = Int32.Parse(reader["Year"].ToString());
                    offeredYear.Add(year);                       
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
        return offeredYear;
    }
    // Get the list of coursecode available for the given semester and year
    public List<CourseOfferingVO> GetOfferedCoursesForSemester(int semester, int year)
    {
        List<CourseOfferingVO> coursesOffered = new List<CourseOfferingVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT CourseCode from dbo.CourseOffering where isActive='Yes' AND Semester=@Semester AND Year=@Year";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Semester", semester);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CourseOfferingVO courseOfferingVO = new CourseOfferingVO();
                    courseOfferingVO.CourseCode = reader["CourseCode"].ToString();
                    coursesOffered.Add(courseOfferingVO);
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
        return coursesOffered;
    }
    //Get list of courses available for given programme, major and semester
    public List<CourseOfferingVO> GetOfferedCoursesForPgmMajor(int semester, int year, string programmeId, string majorId)
    {
        List<CourseOfferingVO> coursesOffered = new List<CourseOfferingVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT cp.CourseCode from dbo.CourseProgramme cp inner join  dbo.CourseOffering co " +
                            " on cp.CourseCode=co.CourseCode " +
                            " where cp.ProgrammeID=@ProgrammeId and cp.MajorID=@MajorId and co.Semester=@Semester AND co.Year=@Year and co.IsActive='Yes' ";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Semester", semester);
            cmd.Parameters.AddWithValue("@ProgrammeId", programmeId);
            cmd.Parameters.AddWithValue("@MajorId", majorId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CourseOfferingVO courseOfferingVO = new CourseOfferingVO();
                    courseOfferingVO.CourseCode = reader["CourseCode"].ToString();
                    coursesOffered.Add(courseOfferingVO);
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
        return coursesOffered;
    }
    // add course offering into database
    public Boolean AddCourseOffering(CourseOfferingVO inCourseOffering)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            String query = "INSERT INTO dbo.CourseOffering (CourseCode,Semester, Year,IsActive, CreatedDTM) VALUES (@CourseCode,@Semester,@Year,@IsActive, @CreatedDTM)";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", inCourseOffering.CourseCode);
            cmd.Parameters.AddWithValue("@Semester", inCourseOffering.Semester);
            cmd.Parameters.AddWithValue("@Year", inCourseOffering.Year);
            cmd.Parameters.AddWithValue("@IsActive", "Yes");
            cmd.Parameters.AddWithValue("@CreatedDTM", inCourseOffering.CreatedTime);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            if (e.Number == 2627)
            {                
                throw new CustomException("Duplicate record exists. Please remove dupliactes and try again");
            }
            else if (e.Number == 547)
            {
                throw new CustomException("CourseCode not found in Course. Please add course and try again");
            }
            else
            {
                throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
            }
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
        return status;        
    }
    // delete the given course offering
    public Boolean DeleteCourseOffering(CourseOfferingVO inCourseOffering)
    {        
        string courseCode = "";
        try
        {
            DBConnection.conn.Open();
            String query = "DELETE FROM dbo.CourseOffering WHERE courseCode=@CourseCode AND Semester=@Semester AND Year=@Year";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", inCourseOffering.CourseCode);
            cmd.Parameters.AddWithValue("@Semester", inCourseOffering.Semester);
            cmd.Parameters.AddWithValue("@Year", inCourseOffering.Year);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                courseCode = inCourseOffering.CourseCode;
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
    // Get list of course code from course offering
    public DataTable FillCourseCodeOffering()
    {       
        DataTable dt = null;
        try
        {
            DBConnection.conn.Open();
            string selectCourseCode = "SELECT DISTINCT CourseCode from CourseOffering where isActive='Yes'";
            SqlCommand command = new SqlCommand(selectCourseCode, DBConnection.conn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            DBConnection.conn.Close();
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
    // Get list of all year present in course offering
    public DataTable FillYearCourseOffering()
    {       
        DataTable dt = null;
        try
        {
            DBConnection.conn.Open();
            string selectCourseCode = "SELECT distinct Year from CourseOffering where isActive='Yes'";
            SqlCommand command = new SqlCommand(selectCourseCode, DBConnection.conn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            return dt;
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
    }
    //Get list of all year from course offering for given semester 
    public DataSet FillYearForSemester(int semester)
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT distinct Year from CourseOffering where isActive='Yes' and semester=" + semester;
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "CourseOffering");
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
    // Get list of all course for which course offering is not available
    public DataTable FillCourseCode()
    {        
        DataTable dt = null;
        try
        {
            DBConnection.conn.Open();
            string selectCourseCode = "SELECT CourseCode from Course WHERE Course.CourseCode NOT IN ( SELECT CourseCode FROM CourseOffering where IsActive='Yes')";
            SqlCommand command = new SqlCommand(selectCourseCode, DBConnection.conn);
            command.ExecuteNonQuery();
            dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            DBConnection.conn.Close();
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
    // update course offering
    public Boolean UpdateCourseOffering(CourseOfferingVO inCourseOffering, int oldSemester, int oldYear)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            String query = "UPDATE dbo.CourseOffering SET CourseCode=@CourseCode,Semester=@Semester, Year=@Year, CreatedDTM=@CreatedDTM WHERE CourseCode=@CourseCode and Semester=@oldSemester and Year=@oldYear";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", inCourseOffering.CourseCode);
            cmd.Parameters.AddWithValue("@Semester", inCourseOffering.Semester);
            cmd.Parameters.AddWithValue("@Year", inCourseOffering.Year);
            cmd.Parameters.AddWithValue("@CreatedDTM", DateTime.Now);
            cmd.Parameters.AddWithValue("@oldSemester", oldSemester);
            cmd.Parameters.AddWithValue("@oldYear", oldYear);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            if (e.Number == 2627)
            {                
                throw new CustomException("Duplicate record exists.Cannot Update.");
            }
            else if (e.Number == 547)
            {
                throw new CustomException("CourseCode not found in Course. Please add course and try again");
            }
            else
            {
                throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
            }
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
        return status;        
    }

}