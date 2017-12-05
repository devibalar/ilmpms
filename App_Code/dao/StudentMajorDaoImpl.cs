using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for StudentMajorDaoImpl
/// </summary>
public class StudentMajorDaoImpl : IStudentMajorDao
{  
    // Add the programme and major for student in StudentMajor table
    public Boolean AddStudentProgramme(StudentMajorVO inStudentMajor)
    {       
        bool status=false;
            try
            {
                DBConnection.conn.Open();
                String query = "INSERT INTO dbo.StudentMajor (StudentId, ProgrammeId, MajorId, Active) VALUES (@StudentId, @ProgrammeId, @MajorId, @Active)";
                SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
                cmd.Parameters.AddWithValue("@StudentId", inStudentMajor.studentId);
                cmd.Parameters.AddWithValue("@ProgrammeId", inStudentMajor.programmeId);
                cmd.Parameters.AddWithValue("@MajorId", inStudentMajor.majorId);
                cmd.Parameters.AddWithValue("@Active", inStudentMajor.active);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    status = true;
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
            return status;  
    }
    //update studentmajor
    public Boolean UpdateStudentProgramme(StudentMajorVO inStudentMajor)
    {
        bool status = false;
            try
            {
                DBConnection.conn.Open();
                String query = "UPDATE dbo.StudentMajor SET StudentId=@StudentId, ProgrammeId=@ProgrammeId, MajorId=@MajorId, Active=@Active WHERE StudentId=@StudentId";
                SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
                cmd.Parameters.AddWithValue("@StudentId", inStudentMajor.studentId);
                cmd.Parameters.AddWithValue("@ProgrammeId", inStudentMajor.programmeId);
                cmd.Parameters.AddWithValue("@MajorId", inStudentMajor.majorId);
                cmd.Parameters.AddWithValue("@Active", inStudentMajor.active);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    status = true;
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
            return status;        
    }
    // delete from student major
    public Boolean DeleteStudentProgramme(StudentMajorVO inStudentMajor)
    {
        bool status = false;
            try
            {
                DBConnection.conn.Open();
                String query = "DELETE FROM dbo.StudentMajor WHERE StudentId=@StudentId";
                SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
                cmd.Parameters.AddWithValue("@StudentId", inStudentMajor.studentId);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    status = true;
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
            return status;        
    }
    //Get  programme and major for the student
    public StudentMajorVO GetStudentMajor(int studentId)
    {
        StudentMajorVO studentMajorVO = new StudentMajorVO();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT ProgrammeId,MajorId from StudentMajor WHERE StudentId=@StudentId AND Active='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studentMajorVO.ProgrammeID = reader["ProgrammeId"].ToString();
                    studentMajorVO.MajorID = reader["MajorId"].ToString();
                }
                reader.Close();
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return studentMajorVO;
    }

    //Get list of programme and major for the student
    public List<StudentMajorVO> GetStudentMajorList(int studentId)
    {
        List<StudentMajorVO> studentMajorList = new List<StudentMajorVO>();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT ProgrammeId,MajorId from StudentMajor WHERE StudentId=@StudentId AND Active='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                StudentMajorVO studentMajorVO ;
                while (reader.Read())
                {
                    studentMajorVO = new StudentMajorVO();
                    studentMajorVO.ProgrammeID = reader["ProgrammeId"].ToString();
                    studentMajorVO.MajorID = reader["MajorId"].ToString();
                    studentMajorList.Add(studentMajorVO);
                }
                reader.Close();
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + ex.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return studentMajorList;
    }
   /* public void FillStudentID()
    {
        try
        {
            DBConnection.conn.Open();
            string selectCourseCode = "SELECT StudentID from Student WHERE StudentID!=0";
            SqlCommand command = new SqlCommand(selectCourseCode, DBConnection.conn);
            command.ExecuteReader();
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException("Error in adding student major.Please contact system administrator");
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException("Error in adding student major.Please contact system administrator");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }        
    }*/
}