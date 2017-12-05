using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IProgrammeWorkshopDao
/// </summary>
public interface IProgrammeWorkshopDao
{
    List<WorkshopVO> GetAllWorkshopForPgmMajor(string programmeId, string majorId);
}