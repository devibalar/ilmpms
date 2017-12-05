using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeWorkshop
/// </summary>
public class ProgrammeWorkshopVO
{
    private string programmeId;
    private string majorId;
    private int workshopId;

	public ProgrammeWorkshopVO()
	{
	}
    public ProgrammeWorkshopVO(string programmeId, string majorId, int workshopId)
    {
        this.programmeId = programmeId;
        this.majorId = majorId;
        this.workshopId = workshopId;
    }
    public string ProgrammeId
    {
        get { return programmeId; }
        set { programmeId = value; }
    }
    public string MajorId
    {
        get { return majorId; }
        set { majorId = value; }
    }
    public int WorkshopId
    {
        get { return workshopId; }
        set { workshopId = value; }
    }
}