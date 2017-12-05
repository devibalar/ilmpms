using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentDaoImpl
/// </summary>
public class StudentDaoImpl:IStudentDao 
{
	public StudentDaoImpl()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    // Add student details
    public Boolean AddStudent(StudentVO instudent)
    {
        bool status = false;
        try
        {
            DBConnection.conn.Open();
            String query = "INSERT INTO dbo.Student (StudentID, FirstName, LastName, Status, CreatedDTM, UpdatedDTM) VALUES (@StudentId,@FirstName,@LastName, @Status,@CreatedDate,@UpdatedDate)";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", instudent.StudentId);
            cmd.Parameters.AddWithValue("@FirstName", instudent.FirstName);
            cmd.Parameters.AddWithValue("@LastName", instudent.LastName);
            cmd.Parameters.AddWithValue("@Status", ApplicationConstants.StudentStudyStatus);
            cmd.Parameters.AddWithValue("@CreatedDate", instudent.CreatedDate);
            cmd.Parameters.AddWithValue("@UpdatedDate", instudent.LastUpdatedDate);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            if (e.Number == 2601)
            {
                throw new CustomException("StudentID already exists");
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
    // update student details
    public Boolean UpdateStudent(StudentVO instudent)
    {
        bool status = false;
        try
        {
            DBConnection.conn.Open();
            String query = "UPDATE dbo.Student SET StudentID=@StudentId, FirstName=@FirstName, LastName=@LastName, Status=@Status,UpdatedDTM=@UpdatedDTM WHERE StudentID=@StudentId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", instudent.StudentId);
            cmd.Parameters.AddWithValue("@FirstName", instudent.FirstName);
            cmd.Parameters.AddWithValue("@LastName", instudent.LastName);       
            cmd.Parameters.AddWithValue("@Status", instudent.Status);
            cmd.Parameters.AddWithValue("@UpdatedDTM", DateTime.Now);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                status = true;
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            if (e.Number == 2601)
            {
                throw new CustomException("StudentID already exists");
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
    // delete the given student
    public Boolean DeleteStudent(StudentVO instudent)
    {
        Boolean status = false;
        try
        {
            DBConnection.conn.Open();
            String query = "DELETE FROM dbo.Student WHERE StudentID=@StudentId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", instudent.StudentId);
            cmd.ExecuteNonQuery();
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

  /*  public StudentVO GetStudent(int studentid)
    {
        StudentVO studentVO = new StudentVO();
        try
        {
            DBConnection.conn.Open();
            String query = "select s.studentId,s.firstname,s.lastname,s.ProgrammeCode, s.Reason ,u.emailId,u.active from student s inner join IlmpUser u "
                            + "on s.StudentID = u.StudentId where s.StudentID=@StudentId";

            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentid);
            SqlDataReader reader =  cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studentVO.StudentId = Int32.Parse(reader[0].ToString());
                    studentVO.FirstName = reader[1].ToString();
                    studentVO.LastName = reader[2].ToString();
                    studentVO.ProgrammeCode = reader[3].ToString();
                    studentVO.Status = reader[4].ToString();
                    studentVO.EmailID = reader[5].ToString();
                    studentVO.Active = reader[6].ToString();                  
                }
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return studentVO;
    }*/

    // Get Student full name for the given student id
    public string GetStudentNameForId(int studentid)
    {
        string studentName = "";
        try
        {
            DBConnection.conn.Open();
            String query = "select firstname+' '+lastname as name from student where StudentID=@StudentId";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studentName = reader[0].ToString();        
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
        return studentName;
    }
   /* public string GetAllStudents(int studentid)
    {
        string studentName = "";
        try
        {
            DBConnection.conn.Open();
            String query = "select studentid, firstname+' '+lastname as name from student where active='Yes'";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@StudentId", studentid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studentName = reader[0].ToString();
                }
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return studentName;
    }*/

    // Get all studentid from database except dummy record in student table
    public List<StudentVO> GetAllStudentId()
    {
        List<StudentVO> allStudentId = new List<StudentVO>();
        try
        {            
            DBConnection.conn.Open();            
            string getCourseCode = "SELECT StudentId from Student where StudentId!=0";
            SqlCommand command = new SqlCommand(getCourseCode, DBConnection.conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                StudentVO studentVO = new StudentVO(int.Parse(reader[0].ToString()));
                allStudentId.Add(studentVO);
            }
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(ApplicationConstants.UnhandledException + ": " + e.Message);
        }
        finally
        {
            if(DBConnection.conn!=null)
            {
            DBConnection.conn.Close();
            }
        }
        return allStudentId;
        
    }    
    public List<StudentVO> SearchStudent()
    {
        List<StudentVO> students =  new List<StudentVO>();
        return students;
    }
    // Get list of all students
    public List<string> GetAllStudentIDAsList()
    {
        List<string> studentIds = new List<string>();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT StudentID FROM dbo.Student";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    studentIds.Add(reader["StudentID"].ToString());
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
            throw new CustomException(e.Message);
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return studentIds;
    }
}