using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeMajorDaoImpl
/// </summary>
public class ProgrammeMajorDaoImpl:IProgrammeMajorDao
{
    // Get list of all Major for the given programme
    public DataSet getMajorForProgramme(string inProgrammeId)
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT m.MajorName, m.MajorID FROM dbo.ProgrammeMajor pm INNER JOIN dbo.Major m ON m.MajorID=pm.MajorID WHERE pm.ProgrammeId='"+inProgrammeId+"'";         
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);            
            cmd.Fill(ds, "Major");
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