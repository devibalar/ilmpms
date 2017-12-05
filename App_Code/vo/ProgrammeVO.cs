using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Programme
/// </summary>
public class ProgrammeVO
{
    private string programmeId;
    private string programmeName;

	public ProgrammeVO()
	{		
	}
    public string ProgrammeId
    {
        get { return programmeId; }
        set { programmeId = value; }
    }
    public string ProgrammeName
    {
        get { return programmeName; }
        set { programmeName = value; }
    }
}