using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkshopBO
/// </summary>
public class WorkshopBO
{
    IWorkshopDao workshopDao = new WorkshopDaoImpl();
    public WorkshopVO GetWorkshopForId(int inworkshopId)
    {
       WorkshopVO workshopVO;
        try
        {
           workshopVO = workshopDao.GetWorkshopForId(inworkshopId);
        }
        catch (CustomException e)
        {
            throw e;
        }
       return workshopVO;
    }
}