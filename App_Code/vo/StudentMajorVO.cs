using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentMajorVO
/// </summary>
public class StudentMajorVO
{
    public int studentId;
    public string programmeId;
    public string majorId;
    public string active;
    public StudentMajorVO()
    {
    }
    public StudentMajorVO(int studentId, string programmeId, string majorId, string active)
    {
        this.studentId = studentId;
        this.programmeId = programmeId;
        this.majorId = majorId;
        this.active = active;
    }

    public int StudentId
    {
        get { return studentId; }
        set { studentId = value; }
    }

    public string ProgrammeID
    {
        get { return programmeId; }
        set { programmeId = value; }
    }

    public string MajorID
    {
        get { return majorId; }
        set { majorId = value; }
    }

    public string Active
    {
        get { return active; }
        set { active = value; }
    }
}