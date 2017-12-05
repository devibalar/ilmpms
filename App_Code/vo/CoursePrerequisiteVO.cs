using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CoursePrerequisite
/// </summary>
public class CoursePrerequisiteVO
{    
    private string courseCode;
    private string prerequisiteCode;
    private string type;
    private string allprerequisites;
	public CoursePrerequisiteVO()
	{		
	}
    public CoursePrerequisiteVO(string courseCode, string prerequisiteCode)
    {
        this.courseCode = courseCode;
        this.prerequisiteCode = prerequisiteCode;
    }

    public CoursePrerequisiteVO(string courseCode, string prerequisiteCode, string type, string allprerequisites)
    {
        this.courseCode = courseCode;
        this.prerequisiteCode = prerequisiteCode;
        this.type = type;
        this.allprerequisites = allprerequisites;
    }

    public string CourseCode
    {
        get { return courseCode; }
        set { courseCode = value; }
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
    public string AllPrerequisites
    {
        get { return allprerequisites; }
        set { allprerequisites = value; }
    }
}