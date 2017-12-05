using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IWorkshopDao
/// </summary>
public interface IWorkshopDao
{
    WorkshopVO GetWorkshopForId(int inworkshopId);
}