using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPGenericTemplate
/// </summary>
public class ILMPGenericTemplateVO
{
    private int templateID;
    private string programmeID;
    private string majorID;
    private string title;
    private List<ILMPGenericTemplateCourseVO> courses = new List<ILMPGenericTemplateCourseVO>();

    public ILMPGenericTemplateVO()
    {
    }

    public ILMPGenericTemplateVO(string programmeID, string majorID, string title)
	{
        this.programmeID = programmeID;
        this.majorID = majorID;
        this.title = title;
	}

    public int TemplateID
    {
        get { return templateID; }
        set { templateID = value; }
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

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public List<ILMPGenericTemplateCourseVO> Courses
    {
        get { return courses; }
        set { courses = value; }
    }

}