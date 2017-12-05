using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeDaoImpl
/// </summary>
public class ProgrammeDaoImpl:IProgrammeDao
{
    // Get list of all programme
    public DataSet getAllProgram()
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT ProgrammeName,ProgrammeID FROM dbo.Programme";
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "Programme");
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