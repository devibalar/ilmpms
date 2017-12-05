using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Prerequisite
/// </summary>
public class PrerequisiteVO
{
    public string prerequisiteCode;
    public string type;

    public PrerequisiteVO()
    {
    }
    public PrerequisiteVO( string prerequisiteCode, string type)
    {
        this.prerequisiteCode = prerequisiteCode;
        this.type = type;
    }

    public string PrerequisiteCode
    {
        get { return prerequisiteCode; }
        set { prerequisiteCode = value; }
    }

    public string Type
    {
        get { return type; }
        set { type = value; }
    }
}