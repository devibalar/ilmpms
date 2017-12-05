using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeWorkshopBO
/// </summary>
public class ProgrammeWorkshopBO
{
    IProgrammeWorkshopDao programmeWorkshopDao = new ProgrammeWorkshopDaoImpl();
    public List<WorkshopVO> GetAllWorkshopForPgmMajor(string inprogrammeId, string inmajorId)
    {
        List<WorkshopVO> workshops =new List<WorkshopVO>();
        try
        {
            workshops= programmeWorkshopDao.GetAllWorkshopForPgmMajor(inprogrammeId,inmajorId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return workshops;
    }
}