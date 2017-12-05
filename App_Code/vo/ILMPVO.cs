using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPVO
/// </summary>
public class ILMPVO
{
    private int ilmpId;
    private string name;
    private string description;
    private int templateId;
    private int studentId;
    private string active;
    private List<ILMPCourseVO> ilmpCourses = new List<ILMPCourseVO>();
    
	public ILMPVO()
	{		
	}

    public ILMPVO(string name, string description, int studentId, string active)
    {
        this.name = name;
        this.description = description;
        this.studentId = studentId;
        this.active = active;
    }

    public int IlmpId
    {
        get { return ilmpId; }
        set { ilmpId = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public int StudentId
    {
        get { return studentId; }
        set { studentId = value; }
    }

    public string Active
    {
        get { return active; }
        set { active = value; }
    }

    public List<ILMPCourseVO> IlmpCourses
    {
        get { return ilmpCourses; }
        set { ilmpCourses = value; }
    }

    public int TemplateId
    {
        get { return templateId; }
        set { templateId = value; }
    }
}