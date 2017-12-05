using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MajorVO
/// </summary>
public class MajorVO
{
    private string majorID;
    private string majorName;

    public MajorVO()
	{       
	}
    public string MajorID
    {
        get { return majorID; }
        set { majorID = value; }
    }
    public string MajorName
    {
        get { return majorName; }
        set { majorName = value; }
    }
}