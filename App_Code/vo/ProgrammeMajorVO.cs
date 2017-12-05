using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeMajor
/// </summary>
public class ProgrammeMajor
{
    private string programmeID;
    private string majorID;

    public ProgrammeMajor(string programmeID, string majorID)
    {
        this.programmeID = programmeID;
        this.majorID = majorID;
    }
    public string ProgrammeID
    {
        get { return programmeID; }
        set { programmeID = value; }
    }
    public string MajorID
    {
        get { return majorID; }
        set { majorID = value; }
    }

}