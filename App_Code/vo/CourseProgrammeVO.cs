using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseProgrammeVO
/// </summary>
public class CourseProgrammeVO
{
    private string courseCode;
    private string programmeId;
    private string majorId;
    private string courseType;
	public CourseProgrammeVO()
	{		
	}
    public CourseProgrammeVO(string programmeId, string majorId, string courseType)
    {
        this.programmeId = programmeId;
        this.majorId = majorId;
        this.courseType = courseType;
    }
    public CourseProgrammeVO(string courseCode, string programmeId, string majorId, string courseType)
    {
        this.courseCode = courseCode;
        this.programmeId = programmeId;
        this.majorId = majorId;
        this.courseType = courseType;
    }
    public string CourseCode
    {
        get { return courseCode; }
        set { courseCode = value; }
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
    public string CourseType
    {
        get { return courseType; }
        set { courseType = value; }
    }
}