using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPTemplateVO
/// </summary>
public class ILMPTemplateVO
{
    private int templateId;
    private string programmeId;
    private string majorId;    
    private string templateName;
    private int studentId;
    private List<TemplateCourseVO> templateCourses = new List<TemplateCourseVO>();

	public ILMPTemplateVO()
	{		
	}
    public ILMPTemplateVO(string programmeId, string majorId, string templateName, int studentId)
    {
        this.programmeId = programmeId;
        this.majorId = majorId;
        this.templateName = templateName;
        this.studentId = studentId;
    }
    public ILMPTemplateVO(string programmeId, string majorId, string templateName,int studentId, List<TemplateCourseVO> templateCourses)
    {
        this.programmeId = programmeId;
        this.majorId = majorId;
        this.templateName = templateName;
        this.studentId = studentId;
        this.templateCourses = templateCourses;
    }
    public int TemplateId
    {
        get { return templateId; }
        set { templateId = value; }
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
    public string TemplateName
    {
        get { return templateName; }
        set { templateName = value; }
    }
    public int StudentId
    {
        get { return studentId; }
        set { studentId = value; }
    }
    public List<TemplateCourseVO> TemplateCourses
    {
        get { return templateCourses; }
        set { templateCourses = value; }
    }  
}