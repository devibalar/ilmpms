using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkshopDaoImpl
/// </summary>
public class WorkshopDaoImpl:IWorkshopDao
{
    //Get workshop details for the given workshopid
    public WorkshopVO GetWorkshopForId(int inworkshopId)
    {
        WorkshopVO workshopVO = new WorkshopVO();
        try
        {
            DBConnection.conn.Open();
            string query = "SELECT WorkshopID,WorkshopName FROM dbo.Workshop WHERE WorkshopID=@WorkshopId";           
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@WorkshopId", inworkshopId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    workshopVO.WorkshopId = Int32.Parse(reader["WorkshopID"].ToString());
                    workshopVO.WorkshopName = reader["WorkshopName"].ToString();
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
        return workshopVO;
    }
}