using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeWorkshopDaoImpl
/// </summary>
public class ProgrammeWorkshopDaoImpl:IProgrammeWorkshopDao
{
    //Get list of workshop for the given programme and major
    public List<WorkshopVO> GetAllWorkshopForPgmMajor(string inprogrammeId, string inmajorId)
    {
        List<WorkshopVO> workshops = new List<WorkshopVO>();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT w.WorkshopId, w.WorkshopName FROM dbo.Workshop W JOIN dbo.ProgrammeWorkshop pw"+
                            " ON pw.WorkshopID = w.WorkshopId WHERE pw.ProgrammeID=@ProgrammeId AND pw.MajorID=@MajorId";

            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);
            cmd.Parameters.AddWithValue("@ProgrammeId", inprogrammeId);
            cmd.Parameters.AddWithValue("@MajorId", inmajorId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                WorkshopVO workshopVO;
                while (reader.Read())
                {
                    workshopVO = new WorkshopVO();
                    workshopVO.WorkshopId = Int32.Parse(reader[0].ToString());
                    workshopVO.WorkshopName = reader[1].ToString();
                    workshops.Add(workshopVO);
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
        return workshops;
    }
}