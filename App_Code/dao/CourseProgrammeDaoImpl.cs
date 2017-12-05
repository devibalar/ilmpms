using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseProgrammeDaoImpl
/// </summary>
public class CourseProgrammeDaoImpl:ICourseProgrammeDao
{
    // Get programme and major for the given course code
    public List<CourseProgrammeVO> GetCourseProgrammeForCourseCode(string incourseCode)
    {
        List<CourseProgrammeVO> CourseProgrammeList = new List<CourseProgrammeVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT ProgrammeId,MajorId from CourseProgramme where CourseCode=@CourseCode" ;
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", incourseCode);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                CourseProgrammeVO coursePgmVO ;
                while (reader.Read())
                {
                    coursePgmVO = new CourseProgrammeVO();
                    coursePgmVO.ProgrammeId = reader["ProgrammeId"].ToString();
                    coursePgmVO.MajorId = reader["MajorId"].ToString();
                    CourseProgrammeList.Add(coursePgmVO);
                }
            }           
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException("Error in fetching course ");
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException("Error in fetching course ");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return CourseProgrammeList;
    }
}