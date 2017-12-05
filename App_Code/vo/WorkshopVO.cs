using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkshopVO
/// </summary>
public class WorkshopVO
{
    private int workshopId;
    private string workshopName;
	public WorkshopVO()
	{
	}
    public WorkshopVO(int workshopId, string workshopName)
    {
        this.workshopId = workshopId;
        this.workshopName = workshopName;
    }
    public int WorkshopId
    {
        get { return workshopId; }
        set { workshopId = value; }
    }
    public string WorkshopName
    {
        get { return workshopName; }
        set { workshopName = value; }
    }

}