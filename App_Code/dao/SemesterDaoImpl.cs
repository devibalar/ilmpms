using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SemesterDaoImpl
/// </summary>
public class SemesterDaoImpl:ISemesterDao
{
    // Get all semester
    public DataSet getAllSemesters()
    {      
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT SemesterID FROM dbo.Semester";
            //SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "Semester");
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
}