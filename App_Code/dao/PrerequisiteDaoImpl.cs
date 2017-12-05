using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PrerequisiteDaoImpl
/// </summary>
public class PrerequisiteDaoImpl:IPrerequisiteDao
{   

    public PrerequisiteDaoImpl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Boolean AddPrerequisite(PrerequisiteVO inPrerequisite)
    {      
      /*  try
        {
            DBConnection.conn.Open();
            String query = "INSERT INTO dbo.Prerequisite (CourseCode, PrerequisiteCode) VALUES (@CourseCode,@PrequisiteCode)";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", inPrerequisite.courseCode);
            cmd.Parameters.AddWithValue("@PrequisiteCode", inPrerequisite.prerequisiteCode);
            // Add other parameters here
            cmd.ExecuteNonQuery();
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
        }*/
        return true;
        
    }

    public Boolean UpdatePrerequisite(PrerequisiteVO inPrerequisite)
    {        
      /*  try
        {
            DBConnection.conn.Open();
            String query = "UPDATE dbo.Prerequisite SET CourseCode=@CourseCode,PrerequisiteCode=@PrerequisiteCode WHERE CourseCode=@CourseCode";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", inPrerequisite.CourseCode);
            cmd.Parameters.AddWithValue("@PrerequisiteCode", inPrerequisite.PrerequisiteCode);
            cmd.ExecuteNonQuery();
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
        }*/
        return true;        
    }

    public Boolean DeletePrerequisite(PrerequisiteVO inPrerequisite)
    {
        /*  try
          {
              DBConnection.conn.Open();
              String query = "DELETE FROM dbo.Prerequisite where CourseCode=@CourseCode";
              SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
              cmd.Parameters.AddWithValue("@CourseCode", inPrerequisite.CourseCode);
              cmd.ExecuteNonQuery();
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
          }*/
        return true;        
    }
    // Get list of all prereuisites
    public DataSet GetAllPrerequisites()
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT PrerequisiteCode FROM dbo.Prerequisite WHERE PrerequisiteType='Prerequisite'";
            //SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "PrerequisiteCode");
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(e.Message);
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
        return ds;
    }
    // Get list of all corequisites
    public DataSet GetAllCorequisites()
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT PrerequisiteCode FROM dbo.Prerequisite WHERE PrerequisiteType='Corequisite'";
            //SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "CorequisiteCode");
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(e.Message);
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
        return ds;
    }
    public string GetAllPrerequisiteForCourseCode(string courseCode)
    {
        string allprerequisite = "";
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT AllPrerequisites FROM dbo.CoursePrerequisite WHERE CourseCode=@CourseCode";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@CourseCode", courseCode);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    allprerequisite = reader["AllPrerequisites"].ToString();
                }
            }
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
            throw new CustomException(e.Message);
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
        return allprerequisite;
    }
}